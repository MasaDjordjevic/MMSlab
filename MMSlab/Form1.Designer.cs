namespace MMSlab
{
    partial class Form1
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
            this.lollipopToggle1 = new LollipopToggle();
            this.SuspendLayout();
            // 
            // lollipopToggle1
            // 
            this.lollipopToggle1.AutoSize = true;
            this.lollipopToggle1.EllipseBorderColor = "#3b73d1";
            this.lollipopToggle1.EllipseColor = "#508ef5";
            this.lollipopToggle1.Location = new System.Drawing.Point(86, 35);
            this.lollipopToggle1.Name = "lollipopToggle1";
            this.lollipopToggle1.Size = new System.Drawing.Size(47, 19);
            this.lollipopToggle1.TabIndex = 0;
            this.lollipopToggle1.Text = "lollipopToggle1";
            this.lollipopToggle1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 425);
            this.Controls.Add(this.lollipopToggle1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LollipopToggle lollipopToggle1;
    }
}

