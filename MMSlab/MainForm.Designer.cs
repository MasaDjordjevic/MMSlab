namespace MMSlab
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ycbcrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightnessFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guassianBlurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guassianBlurInplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeDetectonHorisontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shiftAndScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.win32CoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downsampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seamCravingResizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.historamToggle = new LollipopToggleText();
            this.textBoxPlaceholder1 = new MMSlab.Controls.TextBoxPlaceholder();
            this.progressBar = new LollipopProgressBar();
            this.shiftAndScaleInputs1 = new MMSlab.Controls.ShiftAndScaleInputs();
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ycbcrToolStripMenuItem,
            this.brightnessFilterToolStripMenuItem,
            this.guassianBlurToolStripMenuItem,
            this.guassianBlurInplaceToolStripMenuItem,
            this.toolStripMenuItem1,
            this.contrastToolStripMenuItem,
            this.edgeDetectonHorisontalToolStripMenuItem,
            this.waterToolStripMenuItem,
            this.shiftAndScaleToolStripMenuItem});
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.filtersToolStripMenuItem.Text = "Filters";
            // 
            // ycbcrToolStripMenuItem
            // 
            this.ycbcrToolStripMenuItem.CheckOnClick = true;
            this.ycbcrToolStripMenuItem.Name = "ycbcrToolStripMenuItem";
            this.ycbcrToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.ycbcrToolStripMenuItem.Size = new System.Drawing.Size(301, 26);
            this.ycbcrToolStripMenuItem.Text = "YcbCr";
            this.ycbcrToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ycbcrToolStripMenuItem_CheckedChanged);
            this.ycbcrToolStripMenuItem.Click += new System.EventHandler(this.ycbcrToolStripMenuItem_Click);
            // 
            // brightnessFilterToolStripMenuItem
            // 
            this.brightnessFilterToolStripMenuItem.Name = "brightnessFilterToolStripMenuItem";
            this.brightnessFilterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.brightnessFilterToolStripMenuItem.Size = new System.Drawing.Size(301, 26);
            this.brightnessFilterToolStripMenuItem.Text = "Brightness filter";
            this.brightnessFilterToolStripMenuItem.Click += new System.EventHandler(this.brightnessFilterToolStripMenuItem_Click);
            // 
            // guassianBlurToolStripMenuItem
            // 
            this.guassianBlurToolStripMenuItem.Name = "guassianBlurToolStripMenuItem";
            this.guassianBlurToolStripMenuItem.Size = new System.Drawing.Size(301, 26);
            this.guassianBlurToolStripMenuItem.Text = "Guassian Blur";
            this.guassianBlurToolStripMenuItem.Click += new System.EventHandler(this.guassialBlurToolStripMenuItem_Click);
            // 
            // guassianBlurInplaceToolStripMenuItem
            // 
            this.guassianBlurInplaceToolStripMenuItem.Name = "guassianBlurInplaceToolStripMenuItem";
            this.guassianBlurInplaceToolStripMenuItem.Size = new System.Drawing.Size(301, 26);
            this.guassianBlurInplaceToolStripMenuItem.Text = "Guassian Blur inplace";
            this.guassianBlurInplaceToolStripMenuItem.Click += new System.EventHandler(this.guassianBlurInplaceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.CheckOnClick = true;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.toolStripMenuItem1.Size = new System.Drawing.Size(301, 26);
            this.toolStripMenuItem1.Text = "Gussian Blur 2x2";
            this.toolStripMenuItem1.CheckedChanged += new System.EventHandler(this.toolStripMenuItem1_CheckedChanged);
            // 
            // contrastToolStripMenuItem
            // 
            this.contrastToolStripMenuItem.Name = "contrastToolStripMenuItem";
            this.contrastToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.contrastToolStripMenuItem.Size = new System.Drawing.Size(301, 26);
            this.contrastToolStripMenuItem.Text = "Contrast";
            this.contrastToolStripMenuItem.Click += new System.EventHandler(this.contrastToolStripMenuItem_Click);
            // 
            // edgeDetectonHorisontalToolStripMenuItem
            // 
            this.edgeDetectonHorisontalToolStripMenuItem.Name = "edgeDetectonHorisontalToolStripMenuItem";
            this.edgeDetectonHorisontalToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.edgeDetectonHorisontalToolStripMenuItem.Size = new System.Drawing.Size(301, 26);
            this.edgeDetectonHorisontalToolStripMenuItem.Text = "Edge detecton horisontal";
            this.edgeDetectonHorisontalToolStripMenuItem.Click += new System.EventHandler(this.edgeDetectonHorisontalToolStripMenuItem_Click);
            // 
            // waterToolStripMenuItem
            // 
            this.waterToolStripMenuItem.Name = "waterToolStripMenuItem";
            this.waterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.waterToolStripMenuItem.Size = new System.Drawing.Size(301, 26);
            this.waterToolStripMenuItem.Text = "Water";
            this.waterToolStripMenuItem.Click += new System.EventHandler(this.waterToolStripMenuItem_Click);
            // 
            // shiftAndScaleToolStripMenuItem
            // 
            this.shiftAndScaleToolStripMenuItem.Name = "shiftAndScaleToolStripMenuItem";
            this.shiftAndScaleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.shiftAndScaleToolStripMenuItem.Size = new System.Drawing.Size(301, 26);
            this.shiftAndScaleToolStripMenuItem.Text = "Shift and Scale";
            this.shiftAndScaleToolStripMenuItem.Click += new System.EventHandler(this.shiftAndScaleToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.win32CoreToolStripMenuItem,
            this.downsampleToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // win32CoreToolStripMenuItem
            // 
            this.win32CoreToolStripMenuItem.Checked = true;
            this.win32CoreToolStripMenuItem.CheckOnClick = true;
            this.win32CoreToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.win32CoreToolStripMenuItem.Name = "win32CoreToolStripMenuItem";
            this.win32CoreToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.win32CoreToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.win32CoreToolStripMenuItem.Text = "Win32 core";
            this.win32CoreToolStripMenuItem.Click += new System.EventHandler(this.win32CoreToolStripMenuItem_Click);
            // 
            // downsampleToolStripMenuItem
            // 
            this.downsampleToolStripMenuItem.Name = "downsampleToolStripMenuItem";
            this.downsampleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.downsampleToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.downsampleToolStripMenuItem.Text = "Downsample";
            this.downsampleToolStripMenuItem.Click += new System.EventHandler(this.downsampleToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem2,
            this.filtersToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1505, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.seamCravingResizeToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(47, 24);
            this.toolStripMenuItem2.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // seamCravingResizeToolStripMenuItem
            // 
            this.seamCravingResizeToolStripMenuItem.Name = "seamCravingResizeToolStripMenuItem";
            this.seamCravingResizeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
            this.seamCravingResizeToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.seamCravingResizeToolStripMenuItem.Text = "Seam craving resize";
            this.seamCravingResizeToolStripMenuItem.Click += new System.EventHandler(this.seamCravingResizeToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 720);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(1505, 25);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusLabel
            // 
            this.statusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(151, 20);
            this.statusLabel.Text = "toolStripStatusLabel1";
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Location = new System.Drawing.Point(1363, 0);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBar1.Maximum = 20;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(143, 56);
            this.trackBar1.SmallChange = 10;
            this.trackBar1.TabIndex = 4;
            this.trackBar1.TickFrequency = 2;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Value = 10;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.listView1.Location = new System.Drawing.Point(1344, 33);
            this.listView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(161, 687);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // historamToggle
            // 
            this.historamToggle.AutoSize = true;
            this.historamToggle.EllipseBorderColor = "#3b73d1";
            this.historamToggle.EllipseColor = "#508ef5";
            this.historamToggle.Location = new System.Drawing.Point(395, 1);
            this.historamToggle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.historamToggle.Name = "historamToggle";
            this.historamToggle.Size = new System.Drawing.Size(185, 19);
            this.historamToggle.TabIndex = 6;
            this.historamToggle.Text = "show histograms";
            this.historamToggle.UseVisualStyleBackColor = true;
            this.historamToggle.Visible = false;
            this.historamToggle.CheckedChanged += new System.EventHandler(this.historamToggle_CheckedChanged);
            // 
            // textBoxPlaceholder1
            // 
            this.textBoxPlaceholder1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPlaceholder1.ForeColor = System.Drawing.Color.Gray;
            this.textBoxPlaceholder1.Location = new System.Drawing.Point(300, 2);
            this.textBoxPlaceholder1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxPlaceholder1.MaxLength = 3;
            this.textBoxPlaceholder1.Name = "textBoxPlaceholder1";
            this.textBoxPlaceholder1.Placeholder = "Weight";
            this.textBoxPlaceholder1.PlaceholderColor = System.Drawing.Color.Gray;
            this.textBoxPlaceholder1.Size = new System.Drawing.Size(55, 23);
            this.textBoxPlaceholder1.TabIndex = 5;
            this.textBoxPlaceholder1.Text = "Weight";
            this.textBoxPlaceholder1.Tooltip = "press a to apply, r to redo";
            this.textBoxPlaceholder1.Visible = false;
            this.textBoxPlaceholder1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxPlaceholder1_KeyUp);
            // 
            // progressBar
            // 
            this.progressBar.BGColor = "#508ef5";
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar.Location = new System.Drawing.Point(0, 28);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1505, 5);
            this.progressBar.TabIndex = 2;
            this.progressBar.Text = "lollipopProgressBar1";
            this.progressBar.Value = 0;
            // 
            // shiftAndScaleInputs1
            // 
            this.shiftAndScaleInputs1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.shiftAndScaleInputs1.BackColor = System.Drawing.Color.Transparent;
            this.shiftAndScaleInputs1.Location = new System.Drawing.Point(1053, 1);
            this.shiftAndScaleInputs1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.shiftAndScaleInputs1.Name = "shiftAndScaleInputs1";
            this.shiftAndScaleInputs1.Size = new System.Drawing.Size(312, 28);
            this.shiftAndScaleInputs1.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1505, 745);
            this.Controls.Add(this.shiftAndScaleInputs1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.historamToggle);
            this.Controls.Add(this.textBoxPlaceholder1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "MMS lab";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ycbcrToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brightnessFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guassianBlurToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contrastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private LollipopProgressBar progressBar;
        private System.Windows.Forms.ToolStripMenuItem win32CoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guassianBlurInplaceToolStripMenuItem;
        private System.Windows.Forms.TrackBar trackBar1;
        private Controls.TextBoxPlaceholder textBoxPlaceholder1;
        private LollipopToggleText historamToggle;
        private System.Windows.Forms.ToolStripMenuItem edgeDetectonHorisontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem downsampleToolStripMenuItem;
        private Controls.ShiftAndScaleInputs shiftAndScaleInputs1;
        private System.Windows.Forms.ToolStripMenuItem shiftAndScaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seamCravingResizeToolStripMenuItem;
    }
}

