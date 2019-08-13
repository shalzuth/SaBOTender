using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace FFXIV.Network
{
    public static class Launcher
    {
        private static string UserAgent { get { return $"SQEXAuthor/2.0.0(Windows 6.2; ja-jp; {MakeComputerId()})"; } }
        private static string GameVersion = "2019.07.10.0001.0000"; // C:\Program Files (x86)\SquareEnix\FINAL FANTASY XIV - A Realm Reborn\game\ffxivgame.ver
        private static string MakeComputerId()
        {
            var hashString = Environment.MachineName + Environment.UserName + Environment.OSVersion + Environment.ProcessorCount;
            using (var sha1 = HashAlgorithm.Create("SHA1"))
            {
                var bytes = new Byte[5];
                Array.Copy(sha1.ComputeHash(Encoding.Unicode.GetBytes(hashString)), 0, bytes, 1, 4);
                var checkSum = (Byte)(-(bytes[1] + bytes[2] + bytes[3] + bytes[4]));
                bytes[0] = checkSum;
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
        private static string GenerateFrontierReferer()
        {
            var langCode = "en-gb"; // ja, en-gb, de, fr
            var formattedTime = DateTime.UtcNow.ToString("yyyy-MM-dd-HH");
            return $"https://frontier.ffxiv.com/version_5_0_win/index.html?rc_lang={langCode}&time={formattedTime}";
        }
        public static String DownloadAsLauncher(string url)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("User-Agent", UserAgent);
                client.Headers.Add(HttpRequestHeader.Referer, GenerateFrontierReferer());
                return client.DownloadString(url);
            }
        }
        public static Boolean GetGateStatus()
        {
            var data = DownloadAsLauncher($"https://frontier.ffxiv.com/worldStatus/gate_status.json?{Utils.GetUnixTime()}");
            return data == "{\"status\":1}";
        }

        private static string GetStored()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("User-Agent", UserAgent);
                var reply = client.DownloadString("https://ffxiv-login.square-enix.com/oauth/ffxivarr/login/top?lng=en&rgn=3&isft=0&issteam=0");
                var regex = new Regex(@"\t<\s*input .* name=""_STORED_"" value=""(?<stored>.*)"">");
                return regex.Matches(reply)[0].Groups["stored"].Value;
            }
        }
        public class OauthLoginResult
        {
            public string SessionId { get; set; }
            public int Region { get; set; }
            public bool TermsAccepted { get; set; }
            public bool Playable { get; set; }
            public int MaxExpansion { get; set; }
        }
        public static OauthLoginResult OauthLogin(string username, string password, string otp = "")
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("User-Agent", UserAgent);
                client.Headers.Add("Referer", "https://ffxiv-login.square-enix.com/oauth/ffxivarr/login/top?lng=en&rgn=3&isft=0&issteam=0");
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                byte[] response =
                client.UploadValues("https://ffxiv-login.square-enix.com/oauth/ffxivarr/login/login.send", new NameValueCollection()
                {
                    { "_STORED_", GetStored() },
                    { "sqexid", username },
                    { "password", password },
                    { "otppw", otp }
                });

                var reply = System.Text.Encoding.UTF8.GetString(response);

                var regex = new Regex(@"window.external.user\(""login=auth,ok,(?<launchParams>.*)\);");
                var matches = regex.Matches(reply);

                if (matches.Count == 0)
                    throw new Exception("Could not log in to oauth. Result: " + reply);

                var launchParams = matches[0].Groups["launchParams"].Value.Split(',');

                return new OauthLoginResult
                {
                    SessionId = launchParams[1],
                    Region = int.Parse(launchParams[5]),
                    TermsAccepted = launchParams[3] != "0",
                    Playable = launchParams[9] != "0",
                    MaxExpansion = int.Parse(launchParams[13])
                };
            }
        }
        private static readonly string[] FilesToHash =
        {
            "ffxivboot.exe",
            "ffxivboot64.exe",
            "ffxivlauncher.exe",
            "ffxivlauncher64.exe",
            "ffxivupdater.exe",
            "ffxivupdater64.exe",
        };
        private static string GetFileHash(string file)
        {
            var bytes = File.ReadAllBytes(file);
            var hash = new SHA1Managed().ComputeHash(bytes);
            var hashstring = string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
            var length = new System.IO.FileInfo(file).Length;
            return length + "/" + hashstring;
        }
        private static string GetBootVersionHash()
        {
            var result = "";
            for (var i = 0; i < FilesToHash.Length; i++)
            {
                result += $"{FilesToHash[i]}/{GetFileHash(Path.Combine(@"C:\Program Files (x86)\SquareEnix\FINAL FANTASY XIV - A Realm Reborn\boot", FilesToHash[i]))}";

                if (i != FilesToHash.Length - 1)
                    result += ",";
            }
            return result;
        }
        public static (string Uid, bool NeedsUpdate) RegisterSession(OauthLoginResult loginResult)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("X-Hash-Check", "enabled");
                client.Headers.Add("User-Agent", "FFXIV PATCH CLIENT");
                client.Headers.Add("Referer", $"https://ffxiv-login.square-enix.com/oauth/ffxivarr/login/top?lng=en&rgn={loginResult.Region}");
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                var url = $"https://patch-gamever.ffxiv.com/http/win32/ffxivneo_release_game/{GameVersion}/{loginResult.SessionId}";

                var result = client.UploadString(url, GetBootVersionHash());
                if (client.ResponseHeaders.AllKeys.Contains("X-Patch-Unique-Id"))
                {
                    var sid = client.ResponseHeaders["X-Patch-Unique-Id"];
                    return (sid, result != string.Empty);
                }
            }
            return ("", false);
        }
    }
}
