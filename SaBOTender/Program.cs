using FFXIV.Network;
using FFXIV;
using System;

namespace SaBOTender
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = Launcher.OauthLogin("accountname", "password");
            var uid = Launcher.RegisterSession(result);
            var lobby = new LobbyClient(LobbyClient.Datacenter.NACrystal, uid.Uid, (Byte)result.MaxExpansion);
            Console.WriteLine("press any key to exit");
            Console.ReadLine();
        }
    }
}
