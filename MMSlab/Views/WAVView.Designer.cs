namespace MMSlab.Views
{
    partial class WAVView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WAVView));
            this.buttonPlay = new LollipopButton();
            this.buttonPlayLimited = new LollipopButton();
            this.lollipopTextBox1 = new LollipopTextBox();
            this.lollipopTextBox2 = new LollipopTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPlay
            // 
            this.buttonPlay.BackColor = System.Drawing.Color.Transparent;
            this.buttonPlay.BGColor = "#00695c";
            this.buttonPlay.FontColor = "#ffffff";
            this.buttonPlay.Location = new System.Drawing.Point(309, 42);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(67, 44);
            this.buttonPlay.TabIndex = 2;
            this.buttonPlay.Text = "play";
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonPlayLimited
            // 
            this.buttonPlayLimited.BackColor = System.Drawing.Color.Transparent;
            this.buttonPlayLimited.BGColor = "#00695c";
            this.buttonPlayLimited.FontColor = "#ffffff";
            this.buttonPlayLimited.Location = new System.Drawing.Point(500, 42);
            this.buttonPlayLimited.Name = "buttonPlayLimited";
            this.buttonPlayLimited.Size = new System.Drawing.Size(91, 44);
            this.buttonPlayLimited.TabIndex = 3;
            this.buttonPlayLimited.Text = "play limited";
            this.buttonPlayLimited.Click += new System.EventHandler(this.buttonPlayLimited_Click);
            // 
            // lollipopTextBox1
            // 
            this.lollipopTextBox1.FocusedColor = "#009688 ";
            this.lollipopTextBox1.FontColor = "#999999";
            this.lollipopTextBox1.IsEnabled = true;
            this.lollipopTextBox1.Location = new System.Drawing.Point(393, 55);
            this.lollipopTextBox1.MaxLength = 3;
            this.lollipopTextBox1.Multiline = false;
            this.lollipopTextBox1.Name = "lollipopTextBox1";
            this.lollipopTextBox1.ReadOnly = false;
            this.lollipopTextBox1.Size = new System.Drawing.Size(37, 24);
            this.lollipopTextBox1.TabIndex = 4;
            this.lollipopTextBox1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.lollipopTextBox1.UseSystemPasswordChar = false;
            // 
            // lollipopTextBox2
            // 
            this.lollipopTextBox2.FocusedColor = "#009688 ";
            this.lollipopTextBox2.FontColor = "#999999";
            this.lollipopTextBox2.IsEnabled = true;
            this.lollipopTextBox2.Location = new System.Drawing.Point(445, 55);
            this.lollipopTextBox2.MaxLength = 3;
            this.lollipopTextBox2.Multiline = false;
            this.lollipopTextBox2.Name = "lollipopTextBox2";
            this.lollipopTextBox2.ReadOnly = false;
            this.lollipopTextBox2.Size = new System.Drawing.Size(37, 24);
            this.lollipopTextBox2.TabIndex = 5;
            this.lollipopTextBox2.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.lollipopTextBox2.UseSystemPasswordChar = false;
            this.lollipopTextBox2.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(30, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(245, 86);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // WAVView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lollipopTextBox2);
            this.Controls.Add(this.lollipopTextBox1);
            this.Controls.Add(this.buttonPlayLimited);
            this.Controls.Add(this.buttonPlay);
            this.Name = "WAVView";
            this.Size = new System.Drawing.Size(838, 268);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private LollipopButton buttonPlay;
        private LollipopButton buttonPlayLimited;
        private LollipopTextBox lollipopTextBox1;
        private LollipopTextBox lollipopTextBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
