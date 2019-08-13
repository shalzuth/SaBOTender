using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using SharpPcap.WinPcap;
using System;
using System.Collections.Generic;

namespace FFXIVSniffer
{
    public static class Sniffer
    {
        public static WinPcapDevice device;
        static CaptureFileWriterDevice captureFileWriter;
        public delegate void PacketParserDelegate(PacketWrapper packet);
        public static PacketParserDelegate PacketProcessor;
        static Object QueueLock = new Object();

        public static Queue<PacketWrapper> Packets = new Queue<PacketWrapper>();
        public static void Online()
        {
            device = WinPcapDeviceList.Instance[1];
            device.OnPacketArrival += Device_OnPacketArrival;
            device.Open(DeviceMode.Normal);
            captureFileWriter = new CaptureFileWriterDevice("test.pcap");
            device.StartCapture();
        }
        private static void Device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            Packet rawPacket;
            try
            {
                rawPacket = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

            }
            catch
            {
                Console.WriteLine("parse packet fail " + e.Packet.LinkLayerType);
                rawPacket = new UdpPacket(1, 1);
                return;
            }
            //var ethernetPacket = (EthernetPacket)Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            if (rawPacket.PayloadPacket?.PayloadPacket is TcpPacket)
            {
                var ip = (IPPacket)rawPacket.PayloadPacket;
                var tcp = (TcpPacket)rawPacket.PayloadPacket.PayloadPacket;
                if (ip.SourceAddress.ToString().StartsWith("204.2.229.") || ip.DestinationAddress.ToString().StartsWith("204.2.229."))
                //if (ip.SourceAddress.ToString().StartsWith("192") || ip.DestinationAddress.ToString().StartsWith("192"))
                {
                    if (tcp.PayloadData.Length < 4)
                        return;
                    if (captureFileWriter != null)
                        captureFileWriter.Write(rawPacket.Bytes);
                    var s2c = !ip.SourceAddress.ToString().StartsWith("192");
                    var isLobby = s2c ? tcp.SourcePort == 54994 : tcp.DestinationPort == 54994; ;
                    var channel = isLobby ? "Lobby" : "Game";
                    var p = new PacketWrapper
                    {
                        S2C = s2c,
                        Channel = channel,
                        Bytes = tcp.PayloadData
                    };
                    PacketProcessor(p);
                }
            }
        }

        public static void Cleanup()
        {
            try
            {
                if (device != null && device.Opened)
                    device.Close();
            }
            catch { }
        }
    }
}
