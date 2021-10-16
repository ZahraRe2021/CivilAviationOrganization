namespace SaraPrinterLaser
{
    partial class ConnectingDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectingDevice));
            this.btnEnter = new Telerik.WinControls.UI.RadButton();
            this.timerCheckDevice = new System.Windows.Forms.Timer(this.components);
            this.lblResult = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.pbxLaser = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnEnter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLaser)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnter
            // 
            this.btnEnter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnEnter.Font = new System.Drawing.Font("B Titr", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnEnter.ForeColor = System.Drawing.Color.White;
            this.btnEnter.Location = new System.Drawing.Point(316, 368);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(96, 38);
            this.btnEnter.TabIndex = 80;
            this.btnEnter.Text = "ورود به نرم افزار";
            this.btnEnter.Visible = false;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // timerCheckDevice
            // 
            this.timerCheckDevice.Tick += new System.EventHandler(this.timerCheckDevice_Tick);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("B Titr", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(190)))), ((int)(((byte)(255)))));
            this.lblResult.Location = new System.Drawing.Point(54, 315);
            this.lblResult.Name = "lblResult";
            this.lblResult.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblResult.Size = new System.Drawing.Size(301, 33);
            this.lblResult.TabIndex = 81;
            this.lblResult.Text = "نرم افزار در حال اتصال به دستگاه می باشد";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Enabled = false;
            this.linkLabel1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.Location = new System.Drawing.Point(24, 368);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(207, 19);
            this.linkLabel1.TabIndex = 82;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://www.sarahardware.com/";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Enabled = false;
            this.linkLabel2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.LinkColor = System.Drawing.Color.Blue;
            this.linkLabel2.Location = new System.Drawing.Point(24, 387);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(173, 19);
            this.linkLabel2.TabIndex = 83;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "info@Sarahardware.com";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // pbxLaser
            // 
            this.pbxLaser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbxLaser.Location = new System.Drawing.Point(12, 12);
            this.pbxLaser.Name = "pbxLaser";
            this.pbxLaser.Size = new System.Drawing.Size(400, 300);
            this.pbxLaser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLaser.TabIndex = 0;
            this.pbxLaser.TabStop = false;
            // 
            // ConnectingDevice
            // 
            this.AcceptButton = this.btnEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(431, 418);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.pbxLaser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConnectingDevice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "اتصال به دستگاه";
            this.Load += new System.EventHandler(this.ConnectingDevice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnEnter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLaser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxLaser;
        private Telerik.WinControls.UI.RadButton btnEnter;
        private System.Windows.Forms.Timer timerCheckDevice;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
    }
}