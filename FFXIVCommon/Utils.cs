using System;
using System.Linq;

namespace FFXIV
{
    public static class Utils
    {
        public static Double GetUnixTime()
        {
            return DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
        public static Double GetUnixTime(this DateTime dt)
        {
            return dt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
        public static DateTime GetTimeFromUnix(Double unix)
        {
            return new DateTime(1970, 1, 1).AddSeconds(unix);
        }
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public static byte[] AsciiStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Select(x => (Byte)Char.GetNumericValue(hex, x))
                             .ToArray();
        }
        public static UInt16 ToUInt16(this Single val)
        {
            return (UInt16)(0x8000 + val * 32.767f);
        }
        public static Single ToSingle(this UInt16 val)
        {
            return (Single)((val - 0x8000) / 32.767f);
        }
        public static DateTime ToEorzeaTime(this DateTime date)
        {
            var multiplier = 3600 / 175.0f;
            var epochTicks = date.ToUniversalTime().Ticks - (new DateTime(1970, 1, 1).Ticks);
            var eorzeaTicks = Math.Round(epochTicks * multiplier);
            return new DateTime((Int32)eorzeaTicks);
        }
    }
}
