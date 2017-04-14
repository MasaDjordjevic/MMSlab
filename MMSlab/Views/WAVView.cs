using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace MMSlab.Views
{
    public partial class WAVView : UserControl, IView
    {
        #region unused
        public Bitmap Bitmap { get; set; }
        public double Zoom { get; set; }

        public string SelectedChannel { get; }
        #endregion

        public new void BringToFront()
        {
            base.BringToFront();
        }

        public WAVView()
        {
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Zoom = 1;

            InitializeComponent();
            this.pictureBox1.ImageLocation = @"..\..\waveimg.png";
            this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private string fileName;
        private int channelsNo;
        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
                this.channelsNo = getChannelsNumber(value);
                if (channelsNo == 2)
                {
                    this.lollipopTextBox2.Visible = true;
                }
            }
        }

        public static object TitleContainer { get; private set; }

        private static int getChannelsNumber(string fileName)
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                int chunkID = reader.ReadInt32();
                int fileSize = reader.ReadInt32();
                int riffType = reader.ReadInt32();
                int fmtID = reader.ReadInt32();
                int fmtSize = reader.ReadInt32();
                int fmtCode = reader.ReadInt16();
                int channels = reader.ReadInt16();
                return channels;
            }
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer(this.FileName);
            player.Play();
        }

        private void buttonPlayLimited_Click(object sender, EventArgs e)
        {
            List<int> channels = new List<int>();
            try
            {
                channels.Add(Convert.ToInt32(this.lollipopTextBox1.Text));
                if (this.channelsNo == 2)
                    channels.Add(Convert.ToInt32(this.lollipopTextBox2.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter a number");
                this.lollipopTextBox1.Text = "";
                return;
            }

            byte[] limitedWav = this.limitBytes(channels);

            using (Stream s = new MemoryStream(limitedWav))
            {               
                SoundPlayer player = new SoundPlayer(s);
                player.Play();
            }

        }

        private byte[] limitBytes(List<int> channelsLimits)
        {
            byte[] wav = File.ReadAllBytes(this.fileName);

            int bitsPerSample = BitConverter.ToInt16(wav, 34);
            int dataSize = BitConverter.ToInt32(wav, 40);

            int bytesPerChannel = bitsPerSample / 8 / this.channelsNo;


            for (int i = 44; i < 44 + dataSize; i++)
            {
                int limit = channelsLimits[(i * bytesPerChannel) % channelsLimits.Count];
                if (wav[i] > limit)
                {
                    wav[i] = (byte)limit;
                }
            }

            return wav;


        }


    }
}
