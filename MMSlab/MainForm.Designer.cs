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
            this.contrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.win32CoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new LollipopProgressBar();
            this.textBoxPlaceholder1 = new MMSlab.Controls.TextBoxPlaceholder();
            this.guassianBlurInplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ycbcrToolStripMenuItem,
            this.brightnessFilterToolStripMenuItem,
            this.guassianBlurToolStripMenuItem,
            this.guassianBlurInplaceToolStripMenuItem,
            this.contrastToolStripMenuItem});
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.filtersToolStripMenuItem.Text = "Filters";
            // 
            // ycbcrToolStripMenuItem
            // 
            this.ycbcrToolStripMenuItem.CheckOnClick = true;
            this.ycbcrToolStripMenuItem.Name = "ycbcrToolStripMenuItem";
            this.ycbcrToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.ycbcrToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.ycbcrToolStripMenuItem.Text = "YcbCr";
            this.ycbcrToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ycbcrToolStripMenuItem_CheckedChanged);
            // 
            // brightnessFilterToolStripMenuItem
            // 
            this.brightnessFilterToolStripMenuItem.Name = "brightnessFilterToolStripMenuItem";
            this.brightnessFilterToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.brightnessFilterToolStripMenuItem.Text = "Brightness filter";
            this.brightnessFilterToolStripMenuItem.Click += new System.EventHandler(this.brightnessFilterToolStripMenuItem_Click);
            // 
            // guassianBlurToolStripMenuItem
            // 
            this.guassianBlurToolStripMenuItem.Name = "guassianBlurToolStripMenuItem";
            this.guassianBlurToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.guassianBlurToolStripMenuItem.Text = "Guassian Blur";
            this.guassianBlurToolStripMenuItem.Click += new System.EventHandler(this.guassialBlurToolStripMenuItem_Click);
            // 
            // contrastToolStripMenuItem
            // 
            this.contrastToolStripMenuItem.Name = "contrastToolStripMenuItem";
            this.contrastToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.contrastToolStripMenuItem.Text = "Contrast";
            this.contrastToolStripMenuItem.Click += new System.EventHandler(this.contrastToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.win32CoreToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // win32CoreToolStripMenuItem
            // 
            this.win32CoreToolStripMenuItem.Checked = true;
            this.win32CoreToolStripMenuItem.CheckOnClick = true;
            this.win32CoreToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.win32CoreToolStripMenuItem.Name = "win32CoreToolStripMenuItem";
            this.win32CoreToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.win32CoreToolStripMenuItem.Text = "Win32 core";
            this.win32CoreToolStripMenuItem.Click += new System.EventHandler(this.win32CoreToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.filtersToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1129, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 583);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1129, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusLabel
            // 
            this.statusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(118, 17);
            this.statusLabel.Text = "toolStripStatusLabel1";
            // 
            // progressBar
            // 
            this.progressBar.BGColor = "#508ef5";
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar.Location = new System.Drawing.Point(0, 24);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1129, 4);
            this.progressBar.TabIndex = 2;
            this.progressBar.Text = "lollipopProgressBar1";
            this.progressBar.Value = 10;
            // 
            // textBoxPlaceholder1
            // 
            this.textBoxPlaceholder1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPlaceholder1.ForeColor = System.Drawing.Color.Gray;
            this.textBoxPlaceholder1.Location = new System.Drawing.Point(205, 2);
            this.textBoxPlaceholder1.MaxLength = 3;
            this.textBoxPlaceholder1.Name = "textBoxPlaceholder1";
            this.textBoxPlaceholder1.Placeholder = "Weight";
            this.textBoxPlaceholder1.PlaceholderColor = System.Drawing.Color.Gray;
            this.textBoxPlaceholder1.Size = new System.Drawing.Size(42, 20);
            this.textBoxPlaceholder1.TabIndex = 3;
            this.textBoxPlaceholder1.Text = "Weight";
            this.textBoxPlaceholder1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxPlaceholder1_KeyUp);
            // 
            // guassianBlurInplaceToolStripMenuItem
            // 
            this.guassianBlurInplaceToolStripMenuItem.Name = "guassianBlurInplaceToolStripMenuItem";
            this.guassianBlurInplaceToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.guassianBlurInplaceToolStripMenuItem.Text = "Guassian Blur inplace";
            this.guassianBlurInplaceToolStripMenuItem.Click += new System.EventHandler(this.guassianBlurInplaceToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 605);
            this.Controls.Add(this.textBoxPlaceholder1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MMS lab";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
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
        private Controls.TextBoxPlaceholder textBoxPlaceholder1;
        private System.Windows.Forms.ToolStripMenuItem win32CoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guassianBlurInplaceToolStripMenuItem;
    }
}

