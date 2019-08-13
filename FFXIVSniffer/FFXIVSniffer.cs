using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using FFXIV.Packets;


namespace FFXIVSniffer
{
    public partial class FFXIVSnifferForm : Form
    {
        public BindingList<PacketWrapper> Packets = new BindingList<PacketWrapper>();
        DummyClient Parser = new DummyClient();
        private System.ComponentModel.Design.ByteViewer ByteViewer = new System.ComponentModel.Design.ByteViewer();
        public FFXIVSnifferForm()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            ByteViewer.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            ByteViewer.Dock = DockStyle.Fill;
            leftSide.Controls.Add(ByteViewer);
            Sniffer.PacketProcessor = test;
            var snifferThread = new Thread(() =>
            {
                Sniffer.Online();

            });
            snifferThread.IsBackground = false;
            snifferThread.Start();
        }
        public static FFXIV.Blowfish Key;
        public void test(PacketWrapper p)
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                if (PacketsView.DataSource == null)
                    PacketsView.DataSource = Packets;
                Parser.Stream = new MemoryStream(p.Bytes);
                var packets = Parser.Test(p);
                foreach (var packet in packets)
                {
                    var packetWrapper = new PacketWrapper();
                    packetWrapper.Channel = p.Channel;
                    packetWrapper.S2C = p.S2C;
                    packetWrapper.Bytes = p.Bytes;
                    packetWrapper.Packet = packet;
                    packetWrapper.PacketType = packet.GetType().Name;
                    Packets.Add(packetWrapper);
                }
            }));
        }

        private void FFXIVSnifferForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Sniffer.Cleanup();
        }
        private void PacketsView_SelectionChanged(object sender, EventArgs e)
        {
            var packet = (Packet)PacketsView.SelectedRows[0].Cells["Packet"].Value;
            ByteViewer.SetBytes(packet.RawBytes);
            PacketContents.Text = packet.ToString();

        }
    }
}
