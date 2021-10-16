namespace SaraPrinterLaser
{
    partial class EnterData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterData));
            this.flpItems = new System.Windows.Forms.FlowLayoutPanel();
            this.txtSheetName = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.btnOpenFile = new Telerik.WinControls.UI.RadButton();
            this.btnCheck = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblSave = new System.Windows.Forms.Label();
            this.lblSheetName = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblClm = new Telerik.WinControls.UI.RadLabel();
            this.lblPictureFormat = new System.Windows.Forms.Label();
            this.cmbPictureFormat = new System.Windows.Forms.ComboBox();
            this.btnPictureFormat = new Telerik.WinControls.UI.RadButton();
            this.txtPictureFolderPath = new System.Windows.Forms.TextBox();
            this.btnEnterPictureFolder = new Telerik.WinControls.UI.RadButton();
            this.lblPictureFolderPath = new System.Windows.Forms.Label();
            this.PbarCount = new Telerik.WinControls.UI.RadProgressBar();
            this.tmrProgressBar = new System.Windows.Forms.Timer(this.components);
            this.SaveData = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblClm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPictureFormat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEnterPictureFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbarCount)).BeginInit();
            this.SuspendLayout();
            // 
            // flpItems
            // 
            this.flpItems.Location = new System.Drawing.Point(12, 140);
            this.flpItems.Name = "flpItems";
            this.flpItems.Size = new System.Drawing.Size(574, 185);
            this.flpItems.TabIndex = 0;
            // 
            // txtSheetName
            // 
            this.txtSheetName.Location = new System.Drawing.Point(121, 35);
            this.txtSheetName.Name = "txtSheetName";
            this.txtSheetName.Size = new System.Drawing.Size(347, 20);
            this.txtSheetName.TabIndex = 3;
            this.txtSheetName.Text = "Book1";
            this.txtSheetName.Visible = false;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(121, 9);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(347, 20);
            this.txtAddress.TabIndex = 4;
            // 
            // nudCount
            // 
            this.nudCount.Location = new System.Drawing.Point(12, 331);
            this.nudCount.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(574, 20);
            this.nudCount.TabIndex = 10;
            this.nudCount.Visible = false;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnOpenFile.Font = new System.Drawing.Font("B Titr", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnOpenFile.ForeColor = System.Drawing.Color.White;
            this.btnOpenFile.Location = new System.Drawing.Point(477, 9);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(110, 20);
            this.btnOpenFile.TabIndex = 100;
            this.btnOpenFile.Text = "باز کردن فایل";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnCheck.Font = new System.Drawing.Font("B Titr", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(477, 35);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(110, 20);
            this.btnCheck.TabIndex = 100;
            this.btnCheck.Text = "بررسی اطلاعات";
            this.btnCheck.Visible = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnSave.Font = new System.Drawing.Font("B Titr", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(476, 114);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 20);
            this.btnSave.TabIndex = 101;
            this.btnSave.Text = "ذخیره";
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(121, 114);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(347, 20);
            this.txtName.TabIndex = 102;
            this.txtName.Text = "Book1";
            this.txtName.Visible = false;
            // 
            // lblSave
            // 
            this.lblSave.AutoSize = true;
            this.lblSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSave.Location = new System.Drawing.Point(27, 120);
            this.lblSave.Name = "lblSave";
            this.lblSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblSave.Size = new System.Drawing.Size(88, 13);
            this.lblSave.TabIndex = 103;
            this.lblSave.Text = "اسم پایگاه داده";
            this.lblSave.Visible = false;
            // 
            // lblSheetName
            // 
            this.lblSheetName.AutoSize = true;
            this.lblSheetName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSheetName.Location = new System.Drawing.Point(60, 42);
            this.lblSheetName.Name = "lblSheetName";
            this.lblSheetName.Size = new System.Drawing.Size(52, 13);
            this.lblSheetName.TabIndex = 104;
            this.lblSheetName.Text = "نام شیت";
            this.lblSheetName.Visible = false;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblAddress.Location = new System.Drawing.Point(52, 12);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblAddress.Size = new System.Drawing.Size(63, 13);
            this.lblAddress.TabIndex = 105;
            this.lblAddress.Text = "آدرس فایل";
            this.lblAddress.Click += new System.EventHandler(this.lblAddress_Click);
            // 
            // lblClm
            // 
            this.lblClm.Location = new System.Drawing.Point(477, 150);
            this.lblClm.Name = "lblClm";
            this.lblClm.Size = new System.Drawing.Size(2, 2);
            this.lblClm.TabIndex = 106;
            this.lblClm.Visible = false;
            // 
            // lblPictureFormat
            // 
            this.lblPictureFormat.AutoSize = true;
            this.lblPictureFormat.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblPictureFormat.Location = new System.Drawing.Point(45, 95);
            this.lblPictureFormat.Name = "lblPictureFormat";
            this.lblPictureFormat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPictureFormat.Size = new System.Drawing.Size(70, 13);
            this.lblPictureFormat.TabIndex = 107;
            this.lblPictureFormat.Text = "فرمت عکس";
            this.lblPictureFormat.Visible = false;
            // 
            // cmbPictureFormat
            // 
            this.cmbPictureFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPictureFormat.FormattingEnabled = true;
            this.cmbPictureFormat.Items.AddRange(new object[] {
            "bmp",
            "jpg",
            "jpeg",
            "gif",
            "tga",
            "png",
            "tif",
            "tiff"});
            this.cmbPictureFormat.Location = new System.Drawing.Point(121, 87);
            this.cmbPictureFormat.Name = "cmbPictureFormat";
            this.cmbPictureFormat.Size = new System.Drawing.Size(347, 21);
            this.cmbPictureFormat.TabIndex = 108;
            this.cmbPictureFormat.Visible = false;
            // 
            // btnPictureFormat
            // 
            this.btnPictureFormat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnPictureFormat.Font = new System.Drawing.Font("B Titr", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnPictureFormat.ForeColor = System.Drawing.Color.White;
            this.btnPictureFormat.Location = new System.Drawing.Point(477, 88);
            this.btnPictureFormat.Name = "btnPictureFormat";
            this.btnPictureFormat.Size = new System.Drawing.Size(110, 20);
            this.btnPictureFormat.TabIndex = 102;
            this.btnPictureFormat.Text = "بررسی فرمت";
            this.btnPictureFormat.Visible = false;
            this.btnPictureFormat.Click += new System.EventHandler(this.btnPictureFormat_Click);
            // 
            // txtPictureFolderPath
            // 
            this.txtPictureFolderPath.Location = new System.Drawing.Point(121, 61);
            this.txtPictureFolderPath.Name = "txtPictureFolderPath";
            this.txtPictureFolderPath.ReadOnly = true;
            this.txtPictureFolderPath.Size = new System.Drawing.Size(347, 20);
            this.txtPictureFolderPath.TabIndex = 109;
            this.txtPictureFolderPath.Visible = false;
            // 
            // btnEnterPictureFolder
            // 
            this.btnEnterPictureFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnEnterPictureFolder.Font = new System.Drawing.Font("B Titr", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnEnterPictureFolder.ForeColor = System.Drawing.Color.White;
            this.btnEnterPictureFolder.Location = new System.Drawing.Point(477, 61);
            this.btnEnterPictureFolder.Name = "btnEnterPictureFolder";
            this.btnEnterPictureFolder.Size = new System.Drawing.Size(110, 20);
            this.btnEnterPictureFolder.TabIndex = 101;
            this.btnEnterPictureFolder.Text = "ورود پوشه عکس";
            this.btnEnterPictureFolder.Visible = false;
            this.btnEnterPictureFolder.Click += new System.EventHandler(this.btnEnterPictureFolder_Click);
            // 
            // lblPictureFolderPath
            // 
            this.lblPictureFolderPath.AutoSize = true;
            this.lblPictureFolderPath.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPictureFolderPath.Location = new System.Drawing.Point(10, 68);
            this.lblPictureFolderPath.Name = "lblPictureFolderPath";
            this.lblPictureFolderPath.Size = new System.Drawing.Size(102, 13);
            this.lblPictureFolderPath.TabIndex = 110;
            this.lblPictureFolderPath.Text = "آدرس پوشه عکس";
            this.lblPictureFolderPath.Visible = false;
            // 
            // PbarCount
            // 
            this.PbarCount.Location = new System.Drawing.Point(12, 357);
            this.PbarCount.Name = "PbarCount";
            this.PbarCount.Size = new System.Drawing.Size(575, 24);
            this.PbarCount.TabIndex = 111;
            this.PbarCount.Text = "pbar";
            ((Telerik.WinControls.UI.RadProgressBarElement)(this.PbarCount.GetChildAt(0))).Text = "pbar";
            ((Telerik.WinControls.UI.ProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.LightGreen;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.LightGreen;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.LightGreen;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.LightGreen;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(0))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.UI.UpperProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(1))).BackColor2 = System.Drawing.Color.Green;
            ((Telerik.WinControls.UI.UpperProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(1))).BackColor3 = System.Drawing.Color.Green;
            ((Telerik.WinControls.UI.UpperProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(1))).BackColor4 = System.Drawing.Color.Green;
            ((Telerik.WinControls.UI.UpperProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.Green;
            ((Telerik.WinControls.UI.UpperProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(1))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // tmrProgressBar
            // 
            this.tmrProgressBar.Enabled = true;
            this.tmrProgressBar.Interval = 1;
            this.tmrProgressBar.Tick += new System.EventHandler(this.TmrProgressBar_Tick);
            // 
            // SaveData
            // 
            this.SaveData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SaveData_DoWork);
            // 
            // EnterData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 393);
            this.Controls.Add(this.PbarCount);
            this.Controls.Add(this.lblPictureFolderPath);
            this.Controls.Add(this.btnEnterPictureFolder);
            this.Controls.Add(this.txtPictureFolderPath);
            this.Controls.Add(this.btnPictureFormat);
            this.Controls.Add(this.cmbPictureFormat);
            this.Controls.Add(this.lblPictureFormat);
            this.Controls.Add(this.lblClm);
            this.Controls.Add(this.lblSave);
            this.Controls.Add(this.lblSheetName);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.nudCount);
            this.Controls.Add(this.txtSheetName);
            this.Controls.Add(this.flpItems);
            this.Controls.Add(this.txtAddress);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EnterData";
            this.Text = "ورود اطلاعات";
            this.Load += new System.EventHandler(this.EnterData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblClm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPictureFormat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEnterPictureFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbarCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpItems;
        private System.Windows.Forms.TextBox txtSheetName;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.NumericUpDown nudCount;
        private Telerik.WinControls.UI.RadButton btnOpenFile;
        private Telerik.WinControls.UI.RadButton btnCheck;
        private Telerik.WinControls.UI.RadButton btnSave;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblSave;
        private System.Windows.Forms.Label lblSheetName;
        private System.Windows.Forms.Label lblAddress;
        private Telerik.WinControls.UI.RadLabel lblClm;
        private System.Windows.Forms.Label lblPictureFormat;
        private System.Windows.Forms.ComboBox cmbPictureFormat;
        private Telerik.WinControls.UI.RadButton btnPictureFormat;
        private System.Windows.Forms.TextBox txtPictureFolderPath;
        private Telerik.WinControls.UI.RadButton btnEnterPictureFolder;
        private System.Windows.Forms.Label lblPictureFolderPath;
        private Telerik.WinControls.UI.RadProgressBar PbarCount;
        public System.Windows.Forms.Timer tmrProgressBar;
        private System.ComponentModel.BackgroundWorker SaveData;
    }
}