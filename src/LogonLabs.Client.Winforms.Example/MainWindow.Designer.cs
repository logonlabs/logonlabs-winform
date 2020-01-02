namespace LogonLabs.Client.Winforms.Example
{
    partial class MainWindow
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
            this.logonLabsControl1 = new LogonLabs.Client.WinForms.LogonLabsControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.logonLabsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // logonLabsControl1
            // 

            this.logonLabsControl1.AppId = null;
            this.logonLabsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logonLabsControl1.Location = new System.Drawing.Point(0, 35);
            this.logonLabsControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.logonLabsControl1.Name = "logonLabsControl1";
            this.logonLabsControl1.Provider = null;
            this.logonLabsControl1.Size = new System.Drawing.Size(1200, 657);
            this.logonLabsControl1.TabIndex = 0;
            this.logonLabsControl1.UseDestinationMode = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logonLabsMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1200, 35);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // logonLabsMenu
            // 
            this.logonLabsMenu.Name = "logonLabsMenu";
            this.logonLabsMenu.Size = new System.Drawing.Size(215, 29);
            this.logonLabsMenu.Text = "Choose Logon Provider";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.logonLabsControl1);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainWindow";
            this.Text = "LogonLabs Winforms Client Example";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WinForms.LogonLabsControl logonLabsControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem logonLabsMenu;
    }
}

