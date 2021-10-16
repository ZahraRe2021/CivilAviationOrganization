namespace SaraPrinterLaser.LayoutDesignFolder
{
    partial class frmUltraLightLicenceCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUltraLightLicenceCard));
            this.cmbDataSource = new System.Windows.Forms.ComboBox();
            this.BtnEnterDatabase = new Telerik.WinControls.UI.RadButton();
            this.CbActiveDatabase = new System.Windows.Forms.CheckBox();
            this.btnSinglePrint = new Telerik.WinControls.UI.RadButton();
            this.PibSignatureOfHolder = new System.Windows.Forms.PictureBox();
            this.lblDateOfExpiry = new System.Windows.Forms.Label();
            this.dtpDateOfBrith = new System.Windows.Forms.DateTimePicker();
            this.lblDateOfBrith = new System.Windows.Forms.Label();
            this.lblNationality = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pbPersonalPic = new System.Windows.Forms.PictureBox();
            this.PbCardBottomLayout = new System.Windows.Forms.PictureBox();
            this.PbCardTopLayout = new System.Windows.Forms.PictureBox();
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblDateOfIssue = new System.Windows.Forms.Label();
            this.lblAuthority = new System.Windows.Forms.Label();
            this.txtNationality = new System.Windows.Forms.TextBox();
            this.txtDateOfBrith = new System.Windows.Forms.TextBox();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtAuthority = new System.Windows.Forms.TextBox();
            this.RtxtRemarks = new System.Windows.Forms.RichTextBox();
            this.dtpDateOfExpiry = new System.Windows.Forms.DateTimePicker();
            this.RtxtRatings = new System.Windows.Forms.RichTextBox();
            this.lblRemrks = new System.Windows.Forms.Label();
            this.lblRatings = new System.Windows.Forms.Label();
            this.lblPrintQuntity = new Telerik.WinControls.UI.RadLabel();
            this.txtPrintNumber = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.BtnEnterDatabase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSinglePrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibSignatureOfHolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonalPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbCardBottomLayout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbCardTopLayout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPrintQuntity)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbDataSource
            // 
            this.cmbDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataSource.FormattingEnabled = true;
            this.cmbDataSource.Location = new System.Drawing.Point(957, 465);
            this.cmbDataSource.Name = "cmbDataSource";
            this.cmbDataSource.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbDataSource.Size = new System.Drawing.Size(237, 21);
            this.cmbDataSource.TabIndex = 119;
            // 
            // BtnEnterDatabase
            // 
            this.BtnEnterDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.BtnEnterDatabase.Font = new System.Drawing.Font("B Titr", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.BtnEnterDatabase.ForeColor = System.Drawing.Color.White;
            this.BtnEnterDatabase.Location = new System.Drawing.Point(857, 466);
            this.BtnEnterDatabase.Name = "BtnEnterDatabase";
            this.BtnEnterDatabase.Size = new System.Drawing.Size(94, 21);
            this.BtnEnterDatabase.TabIndex = 126;
            this.BtnEnterDatabase.Text = "ورود پایگاه داده";
            // 
            // CbActiveDatabase
            // 
            this.CbActiveDatabase.AutoSize = true;
            this.CbActiveDatabase.Font = new System.Drawing.Font("B Nazanin", 15F);
            this.CbActiveDatabase.Location = new System.Drawing.Point(1028, 427);
            this.CbActiveDatabase.Name = "CbActiveDatabase";
            this.CbActiveDatabase.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CbActiveDatabase.Size = new System.Drawing.Size(166, 33);
            this.CbActiveDatabase.TabIndex = 125;
            this.CbActiveDatabase.Text = "فعال سازی پایگاه داده";
            this.CbActiveDatabase.UseVisualStyleBackColor = true;
            this.CbActiveDatabase.CheckedChanged += new System.EventHandler(this.CbActiveDatabase_CheckedChanged);
            // 
            // btnSinglePrint
            // 
            this.btnSinglePrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnSinglePrint.Font = new System.Drawing.Font("B Titr", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnSinglePrint.ForeColor = System.Drawing.Color.White;
            this.btnSinglePrint.Location = new System.Drawing.Point(1200, 427);
            this.btnSinglePrint.Name = "btnSinglePrint";
            this.btnSinglePrint.Size = new System.Drawing.Size(101, 60);
            this.btnSinglePrint.TabIndex = 124;
            this.btnSinglePrint.Text = "چاپ";
            this.btnSinglePrint.ThemeName = "ControlDefault";
            this.btnSinglePrint.Click += new System.EventHandler(this.BtnSinglePrint_Click);
            // 
            // PibSignatureOfHolder
            // 
            this.PibSignatureOfHolder.Image = ((System.Drawing.Image)(resources.GetObject("PibSignatureOfHolder.Image")));
            this.PibSignatureOfHolder.Location = new System.Drawing.Point(445, 342);
            this.PibSignatureOfHolder.Name = "PibSignatureOfHolder";
            this.PibSignatureOfHolder.Size = new System.Drawing.Size(173, 53);
            this.PibSignatureOfHolder.TabIndex = 117;
            this.PibSignatureOfHolder.TabStop = false;
            this.PibSignatureOfHolder.DoubleClick += new System.EventHandler(this.PibSignatureOfHolder_DoubleClick);
            this.PibSignatureOfHolder.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PibSignatureOfHolder_MouseMove);
            // 
            // lblDateOfExpiry
            // 
            this.lblDateOfExpiry.AutoSize = true;
            this.lblDateOfExpiry.BackColor = System.Drawing.Color.Transparent;
            this.lblDateOfExpiry.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblDateOfExpiry.Location = new System.Drawing.Point(44, 325);
            this.lblDateOfExpiry.Name = "lblDateOfExpiry";
            this.lblDateOfExpiry.Size = new System.Drawing.Size(140, 21);
            this.lblDateOfExpiry.TabIndex = 114;
            this.lblDateOfExpiry.Text = "Date of expiry:";
            this.lblDateOfExpiry.DoubleClick += new System.EventHandler(this.LblDateOfExpiry_DoubleClick);
            this.lblDateOfExpiry.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LblDateOfExpiry_MouseMove);
            // 
            // dtpDateOfBrith
            // 
            this.dtpDateOfBrith.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateOfBrith.Location = new System.Drawing.Point(190, 295);
            this.dtpDateOfBrith.Name = "dtpDateOfBrith";
            this.dtpDateOfBrith.Size = new System.Drawing.Size(234, 20);
            this.dtpDateOfBrith.TabIndex = 113;
            // 
            // lblDateOfBrith
            // 
            this.lblDateOfBrith.AutoSize = true;
            this.lblDateOfBrith.BackColor = System.Drawing.Color.Transparent;
            this.lblDateOfBrith.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblDateOfBrith.Location = new System.Drawing.Point(44, 235);
            this.lblDateOfBrith.Name = "lblDateOfBrith";
            this.lblDateOfBrith.Size = new System.Drawing.Size(124, 21);
            this.lblDateOfBrith.TabIndex = 112;
            this.lblDateOfBrith.Text = "Date of brith:";
            this.lblDateOfBrith.DoubleClick += new System.EventHandler(this.LblDateOfBrith_DoubleClick);
            this.lblDateOfBrith.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LblDateOfBrith_MouseMove);
            // 
            // lblNationality
            // 
            this.lblNationality.AutoSize = true;
            this.lblNationality.BackColor = System.Drawing.Color.Transparent;
            this.lblNationality.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblNationality.Location = new System.Drawing.Point(44, 265);
            this.lblNationality.Name = "lblNationality";
            this.lblNationality.Size = new System.Drawing.Size(115, 21);
            this.lblNationality.TabIndex = 110;
            this.lblNationality.Text = "Nationality: ";
            this.lblNationality.DoubleClick += new System.EventHandler(this.LblNationality_DoubleClick);
            this.lblNationality.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LblNationality_MouseMove);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(44, 205);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(74, 22);
            this.lblName.TabIndex = 106;
            this.lblName.Text = "Name: ";
            this.lblName.DoubleClick += new System.EventHandler(this.LblName_DoubleClick);
            this.lblName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LblName_MouseMove);
            // 
            // pbPersonalPic
            // 
            this.pbPersonalPic.Image = ((System.Drawing.Image)(resources.GetObject("pbPersonalPic.Image")));
            this.pbPersonalPic.Location = new System.Drawing.Point(482, 156);
            this.pbPersonalPic.Name = "pbPersonalPic";
            this.pbPersonalPic.Size = new System.Drawing.Size(164, 160);
            this.pbPersonalPic.TabIndex = 104;
            this.pbPersonalPic.TabStop = false;
            this.pbPersonalPic.DoubleClick += new System.EventHandler(this.PbPersonalPic_DoubleClick);
            this.pbPersonalPic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PbPersonalPic_MouseMove);
            // 
            // PbCardBottomLayout
            // 
            this.PbCardBottomLayout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PbCardBottomLayout.Image = ((System.Drawing.Image)(resources.GetObject("PbCardBottomLayout.Image")));
            this.PbCardBottomLayout.Location = new System.Drawing.Point(661, 18);
            this.PbCardBottomLayout.Name = "PbCardBottomLayout";
            this.PbCardBottomLayout.Size = new System.Drawing.Size(640, 403);
            this.PbCardBottomLayout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbCardBottomLayout.TabIndex = 103;
            this.PbCardBottomLayout.TabStop = false;
            this.PbCardBottomLayout.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PbCardBottomLayout_MouseMove);
            // 
            // PbCardTopLayout
            // 
            this.PbCardTopLayout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PbCardTopLayout.Image = ((System.Drawing.Image)(resources.GetObject("PbCardTopLayout.Image")));
            this.PbCardTopLayout.Location = new System.Drawing.Point(15, 18);
            this.PbCardTopLayout.Name = "PbCardTopLayout";
            this.PbCardTopLayout.Size = new System.Drawing.Size(640, 403);
            this.PbCardTopLayout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbCardTopLayout.TabIndex = 102;
            this.PbCardTopLayout.TabStop = false;
            this.PbCardTopLayout.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PbCardTopLayout_MouseMove);
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblNumber.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumber.Location = new System.Drawing.Point(44, 175);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(90, 22);
            this.lblNumber.TabIndex = 127;
            this.lblNumber.Text = "Number:";
            this.lblNumber.DoubleClick += new System.EventHandler(this.LblNumber_DoubleClick);
            this.lblNumber.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LblNumber_MouseMove);
            // 
            // lblDateOfIssue
            // 
            this.lblDateOfIssue.AutoSize = true;
            this.lblDateOfIssue.BackColor = System.Drawing.Color.Transparent;
            this.lblDateOfIssue.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblDateOfIssue.Location = new System.Drawing.Point(44, 295);
            this.lblDateOfIssue.Name = "lblDateOfIssue";
            this.lblDateOfIssue.Size = new System.Drawing.Size(131, 21);
            this.lblDateOfIssue.TabIndex = 128;
            this.lblDateOfIssue.Text = "Date of Issue:";
            this.lblDateOfIssue.DoubleClick += new System.EventHandler(this.LblDateOfIssue_DoubleClick);
            this.lblDateOfIssue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LblDateOfIssue_MouseMove);
            // 
            // lblAuthority
            // 
            this.lblAuthority.AutoSize = true;
            this.lblAuthority.BackColor = System.Drawing.Color.Transparent;
            this.lblAuthority.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblAuthority.Location = new System.Drawing.Point(44, 365);
            this.lblAuthority.Name = "lblAuthority";
            this.lblAuthority.Size = new System.Drawing.Size(91, 21);
            this.lblAuthority.TabIndex = 129;
            this.lblAuthority.Text = "Authority";
            this.lblAuthority.DoubleClick += new System.EventHandler(this.LblAuthority_DoubleClick);
            this.lblAuthority.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LblAuthority_MouseMove);
            // 
            // txtNationality
            // 
            this.txtNationality.Location = new System.Drawing.Point(190, 265);
            this.txtNationality.Name = "txtNationality";
            this.txtNationality.Size = new System.Drawing.Size(234, 20);
            this.txtNationality.TabIndex = 132;
            // 
            // txtDateOfBrith
            // 
            this.txtDateOfBrith.Location = new System.Drawing.Point(190, 235);
            this.txtDateOfBrith.Name = "txtDateOfBrith";
            this.txtDateOfBrith.Size = new System.Drawing.Size(234, 20);
            this.txtDateOfBrith.TabIndex = 133;
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(190, 175);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(234, 20);
            this.txtNumber.TabIndex = 134;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(190, 205);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(234, 20);
            this.txtName.TabIndex = 135;
            // 
            // txtAuthority
            // 
            this.txtAuthority.Location = new System.Drawing.Point(190, 365);
            this.txtAuthority.Name = "txtAuthority";
            this.txtAuthority.Size = new System.Drawing.Size(234, 20);
            this.txtAuthority.TabIndex = 136;
            // 
            // RtxtRemarks
            // 
            this.RtxtRemarks.Location = new System.Drawing.Point(720, 80);
            this.RtxtRemarks.Name = "RtxtRemarks";
            this.RtxtRemarks.Size = new System.Drawing.Size(522, 86);
            this.RtxtRemarks.TabIndex = 137;
            this.RtxtRemarks.Text = "";
            // 
            // dtpDateOfExpiry
            // 
            this.dtpDateOfExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateOfExpiry.Location = new System.Drawing.Point(190, 325);
            this.dtpDateOfExpiry.Name = "dtpDateOfExpiry";
            this.dtpDateOfExpiry.Size = new System.Drawing.Size(234, 20);
            this.dtpDateOfExpiry.TabIndex = 138;
            // 
            // RtxtRatings
            // 
            this.RtxtRatings.Location = new System.Drawing.Point(720, 229);
            this.RtxtRatings.Name = "RtxtRatings";
            this.RtxtRatings.Size = new System.Drawing.Size(522, 128);
            this.RtxtRatings.TabIndex = 139;
            this.RtxtRatings.Text = "";
            // 
            // lblRemrks
            // 
            this.lblRemrks.AutoSize = true;
            this.lblRemrks.BackColor = System.Drawing.Color.Transparent;
            this.lblRemrks.Font = new System.Drawing.Font("Arial Black", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemrks.Location = new System.Drawing.Point(693, 45);
            this.lblRemrks.Name = "lblRemrks";
            this.lblRemrks.Size = new System.Drawing.Size(109, 24);
            this.lblRemrks.TabIndex = 140;
            this.lblRemrks.Text = "REMARKS:";
            this.lblRemrks.DoubleClick += new System.EventHandler(this.LblRemrks_DoubleClick);
            this.lblRemrks.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LblRemrks_MouseMove);
            // 
            // lblRatings
            // 
            this.lblRatings.AutoSize = true;
            this.lblRatings.BackColor = System.Drawing.Color.Transparent;
            this.lblRatings.Font = new System.Drawing.Font("Arial Black", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRatings.Location = new System.Drawing.Point(693, 184);
            this.lblRatings.Name = "lblRatings";
            this.lblRatings.Size = new System.Drawing.Size(100, 24);
            this.lblRatings.TabIndex = 141;
            this.lblRatings.Text = "RATINGS:";
            this.lblRatings.DoubleClick += new System.EventHandler(this.LblRatings_DoubleClick);
            this.lblRatings.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LblRatings_MouseMove);
            // 
            // lblPrintQuntity
            // 
            this.lblPrintQuntity.Font = new System.Drawing.Font("B Nazanin", 15F);
            this.lblPrintQuntity.Location = new System.Drawing.Point(946, 427);
            this.lblPrintQuntity.Name = "lblPrintQuntity";
            this.lblPrintQuntity.Size = new System.Drawing.Size(76, 34);
            this.lblPrintQuntity.TabIndex = 142;
            this.lblPrintQuntity.Text = "تعداد چاپ";
            // 
            // txtPrintNumber
            // 
            this.txtPrintNumber.Location = new System.Drawing.Point(857, 433);
            this.txtPrintNumber.Name = "txtPrintNumber";
            this.txtPrintNumber.Size = new System.Drawing.Size(83, 20);
            this.txtPrintNumber.TabIndex = 143;
            this.txtPrintNumber.Text = "1";
            // 
            // frmUltraLightLicenceCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1317, 505);
            this.Controls.Add(this.lblPrintQuntity);
            this.Controls.Add(this.txtPrintNumber);
            this.Controls.Add(this.lblRatings);
            this.Controls.Add(this.lblRemrks);
            this.Controls.Add(this.RtxtRatings);
            this.Controls.Add(this.dtpDateOfExpiry);
            this.Controls.Add(this.RtxtRemarks);
            this.Controls.Add(this.txtAuthority);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.txtDateOfBrith);
            this.Controls.Add(this.txtNationality);
            this.Controls.Add(this.lblAuthority);
            this.Controls.Add(this.lblDateOfIssue);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.cmbDataSource);
            this.Controls.Add(this.BtnEnterDatabase);
            this.Controls.Add(this.CbActiveDatabase);
            this.Controls.Add(this.btnSinglePrint);
            this.Controls.Add(this.PibSignatureOfHolder);
            this.Controls.Add(this.lblDateOfExpiry);
            this.Controls.Add(this.dtpDateOfBrith);
            this.Controls.Add(this.lblDateOfBrith);
            this.Controls.Add(this.lblNationality);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.pbPersonalPic);
            this.Controls.Add(this.PbCardBottomLayout);
            this.Controls.Add(this.PbCardTopLayout);
            this.Name = "frmUltraLightLicenceCard";
            this.Text = "frmUltraLightLicenceCard";
            this.Load += new System.EventHandler(this.FrmUltraLightLicenceCard_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmUltraLightLicenceCard_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.BtnEnterDatabase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSinglePrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PibSignatureOfHolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonalPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbCardBottomLayout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbCardTopLayout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPrintQuntity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbDataSource;
        private Telerik.WinControls.UI.RadButton BtnEnterDatabase;
        private System.Windows.Forms.CheckBox CbActiveDatabase;
        private Telerik.WinControls.UI.RadButton btnSinglePrint;
        private System.Windows.Forms.PictureBox PibSignatureOfHolder;
        private System.Windows.Forms.Label lblDateOfExpiry;
        private System.Windows.Forms.DateTimePicker dtpDateOfBrith;
        private System.Windows.Forms.Label lblDateOfBrith;
        private System.Windows.Forms.Label lblNationality;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.PictureBox pbPersonalPic;
        private System.Windows.Forms.PictureBox PbCardBottomLayout;
        private System.Windows.Forms.PictureBox PbCardTopLayout;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblDateOfIssue;
        private System.Windows.Forms.Label lblAuthority;
        private System.Windows.Forms.TextBox txtNationality;
        private System.Windows.Forms.TextBox txtDateOfBrith;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtAuthority;
        private System.Windows.Forms.RichTextBox RtxtRemarks;
        private System.Windows.Forms.DateTimePicker dtpDateOfExpiry;
        private System.Windows.Forms.RichTextBox RtxtRatings;
        private System.Windows.Forms.Label lblRemrks;
        private System.Windows.Forms.Label lblRatings;
        private Telerik.WinControls.UI.RadLabel lblPrintQuntity;
        private System.Windows.Forms.TextBox txtPrintNumber;
    }
}