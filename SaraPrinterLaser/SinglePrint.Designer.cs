namespace SaraPrinterLaser
{
    partial class SinglePrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SinglePrint));
            this.CardPrewiew = new System.Windows.Forms.TabControl();
            this.TopCard = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.CmbPicturePenTop = new System.Windows.Forms.ComboBox();
            this.btnPenEditTop = new Telerik.WinControls.UI.RadButton();
            this.cmbPenSelectTop = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBoxTop = new System.Windows.Forms.PictureBox();
            this.BottomCard = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.CmbPicturePenBottom = new System.Windows.Forms.ComboBox();
            this.btnPenEditBottom = new Telerik.WinControls.UI.RadButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPenSelectBottom = new System.Windows.Forms.ComboBox();
            this.pictureBoxBottom = new System.Windows.Forms.PictureBox();
            this.btnSinglePrint = new Telerik.WinControls.UI.RadButton();
            this.txtCR3 = new System.Windows.Forms.TextBox();
            this.txtCR2 = new System.Windows.Forms.TextBox();
            this.chkCr = new System.Windows.Forms.CheckBox();
            this.txtCR1 = new System.Windows.Forms.TextBox();
            this.chbRFID = new System.Windows.Forms.CheckBox();
            this.rbxHolder1 = new Telerik.WinControls.UI.RadRadioButton();
            this.rbxHolder2 = new Telerik.WinControls.UI.RadRadioButton();
            this.chkRotate = new System.Windows.Forms.CheckBox();
            this.GBPrint = new System.Windows.Forms.GroupBox();
            this.GBDataWrite = new System.Windows.Forms.GroupBox();
            this.cbMoveToRejectBox = new System.Windows.Forms.CheckBox();
            this.cbEnableTrack3 = new System.Windows.Forms.CheckBox();
            this.cbEnableTrack2 = new System.Windows.Forms.CheckBox();
            this.cbEnableTrack1 = new System.Windows.Forms.CheckBox();
            this.txtPrintNumber = new System.Windows.Forms.TextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.btnCleaning = new Telerik.WinControls.UI.RadButton();
            this.CardPrewiew.SuspendLayout();
            this.TopCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPenEditTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTop)).BeginInit();
            this.BottomCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPenEditBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSinglePrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbxHolder1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbxHolder2)).BeginInit();
            this.GBPrint.SuspendLayout();
            this.GBDataWrite.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCleaning)).BeginInit();
            this.SuspendLayout();
            // 
            // CardPrewiew
            // 
            this.CardPrewiew.Controls.Add(this.TopCard);
            this.CardPrewiew.Controls.Add(this.BottomCard);
            this.CardPrewiew.Location = new System.Drawing.Point(12, 12);
            this.CardPrewiew.Name = "CardPrewiew";
            this.CardPrewiew.SelectedIndex = 0;
            this.CardPrewiew.Size = new System.Drawing.Size(515, 320);
            this.CardPrewiew.TabIndex = 0;
            // 
            // TopCard
            // 
            this.TopCard.BackColor = System.Drawing.Color.Gainsboro;
            this.TopCard.Controls.Add(this.label2);
            this.TopCard.Controls.Add(this.CmbPicturePenTop);
            this.TopCard.Controls.Add(this.btnPenEditTop);
            this.TopCard.Controls.Add(this.cmbPenSelectTop);
            this.TopCard.Controls.Add(this.label3);
            this.TopCard.Controls.Add(this.pictureBoxTop);
            this.TopCard.Location = new System.Drawing.Point(4, 22);
            this.TopCard.Name = "TopCard";
            this.TopCard.Padding = new System.Windows.Forms.Padding(3);
            this.TopCard.Size = new System.Drawing.Size(507, 294);
            this.TopCard.TabIndex = 0;
            this.TopCard.Text = "نمایش روی کارت";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.label2.Location = new System.Drawing.Point(287, 260);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(155, 18);
            this.label2.TabIndex = 85;
            this.label2.Text = "قلم عکس روی کارت:\r\n";
            // 
            // CmbPicturePenTop
            // 
            this.CmbPicturePenTop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPicturePenTop.FormattingEnabled = true;
            this.CmbPicturePenTop.Location = new System.Drawing.Point(96, 261);
            this.CmbPicturePenTop.Name = "CmbPicturePenTop";
            this.CmbPicturePenTop.Size = new System.Drawing.Size(113, 21);
            this.CmbPicturePenTop.TabIndex = 84;
            this.CmbPicturePenTop.SelectedIndexChanged += new System.EventHandler(this.CmbPicturePenTop_SelectedIndexChanged);
            this.CmbPicturePenTop.Click += new System.EventHandler(this.CmbPicturePenTop_Click);
            // 
            // btnPenEditTop
            // 
            this.btnPenEditTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnPenEditTop.Font = new System.Drawing.Font("B Titr", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnPenEditTop.ForeColor = System.Drawing.Color.White;
            this.btnPenEditTop.Location = new System.Drawing.Point(6, 234);
            this.btnPenEditTop.Name = "btnPenEditTop";
            this.btnPenEditTop.Size = new System.Drawing.Size(84, 48);
            this.btnPenEditTop.TabIndex = 82;
            this.btnPenEditTop.Text = "ویرایش قلم";
            this.btnPenEditTop.Click += new System.EventHandler(this.btnPenEditTop_Click);
            // 
            // cmbPenSelectTop
            // 
            this.cmbPenSelectTop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPenSelectTop.FormattingEnabled = true;
            this.cmbPenSelectTop.Location = new System.Drawing.Point(96, 234);
            this.cmbPenSelectTop.Name = "cmbPenSelectTop";
            this.cmbPenSelectTop.Size = new System.Drawing.Size(113, 21);
            this.cmbPenSelectTop.TabIndex = 4;
            this.cmbPenSelectTop.SelectedIndexChanged += new System.EventHandler(this.CmbPenSelectTop_SelectedIndexChanged);
            this.cmbPenSelectTop.Click += new System.EventHandler(this.cmbPenSelectTop_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.label3.Location = new System.Drawing.Point(266, 237);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(176, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "قلم مورد نظر روی کارت:";
            this.label3.LocationChanged += new System.EventHandler(this.label3_LocationChanged);
            // 
            // pictureBoxTop
            // 
            this.pictureBoxTop.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxTop.Image")));
            this.pictureBoxTop.Location = new System.Drawing.Point(83, 6);
            this.pictureBoxTop.Name = "pictureBoxTop";
            this.pictureBoxTop.Size = new System.Drawing.Size(333, 213);
            this.pictureBoxTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTop.TabIndex = 0;
            this.pictureBoxTop.TabStop = false;
            // 
            // BottomCard
            // 
            this.BottomCard.BackColor = System.Drawing.Color.Gainsboro;
            this.BottomCard.Controls.Add(this.label4);
            this.BottomCard.Controls.Add(this.CmbPicturePenBottom);
            this.BottomCard.Controls.Add(this.btnPenEditBottom);
            this.BottomCard.Controls.Add(this.label1);
            this.BottomCard.Controls.Add(this.cmbPenSelectBottom);
            this.BottomCard.Controls.Add(this.pictureBoxBottom);
            this.BottomCard.Location = new System.Drawing.Point(4, 22);
            this.BottomCard.Name = "BottomCard";
            this.BottomCard.Padding = new System.Windows.Forms.Padding(3);
            this.BottomCard.Size = new System.Drawing.Size(507, 294);
            this.BottomCard.TabIndex = 1;
            this.BottomCard.Text = "نمایش پشت کارت";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.label4.Location = new System.Drawing.Point(287, 260);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(157, 18);
            this.label4.TabIndex = 83;
            this.label4.Text = "قلم عکس پشت کارت";
            // 
            // CmbPicturePenBottom
            // 
            this.CmbPicturePenBottom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPicturePenBottom.FormattingEnabled = true;
            this.CmbPicturePenBottom.Location = new System.Drawing.Point(96, 261);
            this.CmbPicturePenBottom.Name = "CmbPicturePenBottom";
            this.CmbPicturePenBottom.Size = new System.Drawing.Size(119, 21);
            this.CmbPicturePenBottom.TabIndex = 82;
            this.CmbPicturePenBottom.Click += new System.EventHandler(this.CmbPicturePenBottom_Click);
            // 
            // btnPenEditBottom
            // 
            this.btnPenEditBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnPenEditBottom.Font = new System.Drawing.Font("B Titr", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnPenEditBottom.ForeColor = System.Drawing.Color.White;
            this.btnPenEditBottom.Location = new System.Drawing.Point(6, 234);
            this.btnPenEditBottom.Name = "btnPenEditBottom";
            this.btnPenEditBottom.Size = new System.Drawing.Size(84, 48);
            this.btnPenEditBottom.TabIndex = 81;
            this.btnPenEditBottom.Text = "ویرایش قلم";
            this.btnPenEditBottom.Click += new System.EventHandler(this.btnPenEditBottom_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.label1.Location = new System.Drawing.Point(266, 237);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(183, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "قلم مورد نظر پشت کارت:";
            this.label1.LocationChanged += new System.EventHandler(this.label1_LocationChanged);
            // 
            // cmbPenSelectBottom
            // 
            this.cmbPenSelectBottom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPenSelectBottom.FormattingEnabled = true;
            this.cmbPenSelectBottom.Location = new System.Drawing.Point(96, 234);
            this.cmbPenSelectBottom.Name = "cmbPenSelectBottom";
            this.cmbPenSelectBottom.Size = new System.Drawing.Size(119, 21);
            this.cmbPenSelectBottom.TabIndex = 1;
            this.cmbPenSelectBottom.SelectedIndexChanged += new System.EventHandler(this.cmbPenSelectBottom_SelectedIndexChanged);
            this.cmbPenSelectBottom.Click += new System.EventHandler(this.cmbPenSelectBottom_Click);
            // 
            // pictureBoxBottom
            // 
            this.pictureBoxBottom.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxBottom.Image")));
            this.pictureBoxBottom.Location = new System.Drawing.Point(83, 6);
            this.pictureBoxBottom.Name = "pictureBoxBottom";
            this.pictureBoxBottom.Size = new System.Drawing.Size(333, 213);
            this.pictureBoxBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBottom.TabIndex = 0;
            this.pictureBoxBottom.TabStop = false;
            // 
            // btnSinglePrint
            // 
            this.btnSinglePrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnSinglePrint.Font = new System.Drawing.Font("B Titr", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnSinglePrint.ForeColor = System.Drawing.Color.White;
            this.btnSinglePrint.Location = new System.Drawing.Point(12, 465);
            this.btnSinglePrint.Name = "btnSinglePrint";
            this.btnSinglePrint.Size = new System.Drawing.Size(84, 34);
            this.btnSinglePrint.TabIndex = 80;
            this.btnSinglePrint.Text = "چاپ";
            this.btnSinglePrint.Click += new System.EventHandler(this.btnSinglePrint_Click);
            // 
            // txtCR3
            // 
            this.txtCR3.Enabled = false;
            this.txtCR3.Location = new System.Drawing.Point(6, 92);
            this.txtCR3.Name = "txtCR3";
            this.txtCR3.Size = new System.Drawing.Size(206, 20);
            this.txtCR3.TabIndex = 102;
            this.txtCR3.TextChanged += new System.EventHandler(this.txtCR3_TextChanged);
            // 
            // txtCR2
            // 
            this.txtCR2.Enabled = false;
            this.txtCR2.Location = new System.Drawing.Point(6, 66);
            this.txtCR2.Name = "txtCR2";
            this.txtCR2.Size = new System.Drawing.Size(206, 20);
            this.txtCR2.TabIndex = 101;
            this.txtCR2.TextChanged += new System.EventHandler(this.txtCR2_TextChanged);
            // 
            // chkCr
            // 
            this.chkCr.AutoSize = true;
            this.chkCr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCr.Location = new System.Drawing.Point(211, 16);
            this.chkCr.Name = "chkCr";
            this.chkCr.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkCr.Size = new System.Drawing.Size(128, 18);
            this.chkCr.TabIndex = 81;
            this.chkCr.Text = "اطلاعات مغناطیسی";
            this.chkCr.UseVisualStyleBackColor = true;
            this.chkCr.CheckedChanged += new System.EventHandler(this.chkCr_CheckedChanged_1);
            // 
            // txtCR1
            // 
            this.txtCR1.Enabled = false;
            this.txtCR1.Location = new System.Drawing.Point(6, 40);
            this.txtCR1.Name = "txtCR1";
            this.txtCR1.Size = new System.Drawing.Size(206, 20);
            this.txtCR1.TabIndex = 100;
            this.txtCR1.TextChanged += new System.EventHandler(this.txtCR1_TextChanged);
            // 
            // chbRFID
            // 
            this.chbRFID.AutoSize = true;
            this.chbRFID.Location = new System.Drawing.Point(64, 19);
            this.chbRFID.Name = "chbRFID";
            this.chbRFID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chbRFID.Size = new System.Drawing.Size(87, 17);
            this.chbRFID.TabIndex = 5;
            this.chbRFID.Text = "بررسی RFID";
            this.chbRFID.UseVisualStyleBackColor = true;
            // 
            // rbxHolder1
            // 
            this.rbxHolder1.Location = new System.Drawing.Point(96, 65);
            this.rbxHolder1.Name = "rbxHolder1";
            this.rbxHolder1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbxHolder1.Size = new System.Drawing.Size(55, 18);
            this.rbxHolder1.TabIndex = 2;
            this.rbxHolder1.TabStop = false;
            this.rbxHolder1.Text = "مخزن 1";
            // 
            // rbxHolder2
            // 
            this.rbxHolder2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rbxHolder2.Location = new System.Drawing.Point(96, 89);
            this.rbxHolder2.Name = "rbxHolder2";
            this.rbxHolder2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbxHolder2.Size = new System.Drawing.Size(55, 18);
            this.rbxHolder2.TabIndex = 2;
            this.rbxHolder2.Text = "مخزن 2";
            this.rbxHolder2.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // chkRotate
            // 
            this.chkRotate.AutoSize = true;
            this.chkRotate.Location = new System.Drawing.Point(78, 42);
            this.chkRotate.Name = "chkRotate";
            this.chkRotate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkRotate.Size = new System.Drawing.Size(73, 17);
            this.chkRotate.TabIndex = 3;
            this.chkRotate.Text = "چاپ دو رو";
            this.chkRotate.UseVisualStyleBackColor = true;
            this.chkRotate.CheckedChanged += new System.EventHandler(this.chkRotate_CheckedChanged);
            // 
            // GBPrint
            // 
            this.GBPrint.Controls.Add(this.rbxHolder1);
            this.GBPrint.Controls.Add(this.chbRFID);
            this.GBPrint.Controls.Add(this.chkRotate);
            this.GBPrint.Controls.Add(this.rbxHolder2);
            this.GBPrint.Location = new System.Drawing.Point(12, 338);
            this.GBPrint.Name = "GBPrint";
            this.GBPrint.Size = new System.Drawing.Size(163, 121);
            this.GBPrint.TabIndex = 89;
            this.GBPrint.TabStop = false;
            this.GBPrint.Text = "چاپ";
            // 
            // GBDataWrite
            // 
            this.GBDataWrite.Controls.Add(this.cbMoveToRejectBox);
            this.GBDataWrite.Controls.Add(this.cbEnableTrack3);
            this.GBDataWrite.Controls.Add(this.cbEnableTrack2);
            this.GBDataWrite.Controls.Add(this.cbEnableTrack1);
            this.GBDataWrite.Controls.Add(this.chkCr);
            this.GBDataWrite.Controls.Add(this.txtCR1);
            this.GBDataWrite.Controls.Add(this.txtCR2);
            this.GBDataWrite.Controls.Add(this.txtCR3);
            this.GBDataWrite.Location = new System.Drawing.Point(178, 338);
            this.GBDataWrite.Name = "GBDataWrite";
            this.GBDataWrite.Size = new System.Drawing.Size(345, 121);
            this.GBDataWrite.TabIndex = 90;
            this.GBDataWrite.TabStop = false;
            this.GBDataWrite.Text = "اطلاعات نوار مغناطیس";
            // 
            // cbMoveToRejectBox
            // 
            this.cbMoveToRejectBox.AutoSize = true;
            this.cbMoveToRejectBox.Checked = true;
            this.cbMoveToRejectBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMoveToRejectBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMoveToRejectBox.Location = new System.Drawing.Point(12, 16);
            this.cbMoveToRejectBox.Name = "cbMoveToRejectBox";
            this.cbMoveToRejectBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbMoveToRejectBox.Size = new System.Drawing.Size(200, 18);
            this.cbMoveToRejectBox.TabIndex = 106;
            this.cbMoveToRejectBox.Text = "انتقال کارت معیوب به ریجکت باکس";
            this.cbMoveToRejectBox.UseVisualStyleBackColor = true;
            this.cbMoveToRejectBox.CheckedChanged += new System.EventHandler(this.CbMoveToRejectBox_CheckedChanged);
            // 
            // cbEnableTrack3
            // 
            this.cbEnableTrack3.AutoSize = true;
            this.cbEnableTrack3.Enabled = false;
            this.cbEnableTrack3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cbEnableTrack3.Location = new System.Drawing.Point(214, 88);
            this.cbEnableTrack3.Name = "cbEnableTrack3";
            this.cbEnableTrack3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbEnableTrack3.Size = new System.Drawing.Size(125, 18);
            this.cbEnableTrack3.TabIndex = 105;
            this.cbEnableTrack3.Text = "فعال سازی نوار سه";
            this.cbEnableTrack3.UseVisualStyleBackColor = true;
            this.cbEnableTrack3.CheckedChanged += new System.EventHandler(this.CbEnableTrack3_CheckedChanged);
            // 
            // cbEnableTrack2
            // 
            this.cbEnableTrack2.AutoSize = true;
            this.cbEnableTrack2.Enabled = false;
            this.cbEnableTrack2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cbEnableTrack2.Location = new System.Drawing.Point(222, 64);
            this.cbEnableTrack2.Name = "cbEnableTrack2";
            this.cbEnableTrack2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbEnableTrack2.Size = new System.Drawing.Size(117, 18);
            this.cbEnableTrack2.TabIndex = 104;
            this.cbEnableTrack2.Text = "فعال سازی نوار دو";
            this.cbEnableTrack2.UseVisualStyleBackColor = true;
            this.cbEnableTrack2.CheckedChanged += new System.EventHandler(this.CbEnableTrack2_CheckedChanged);
            // 
            // cbEnableTrack1
            // 
            this.cbEnableTrack1.AutoSize = true;
            this.cbEnableTrack1.Enabled = false;
            this.cbEnableTrack1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cbEnableTrack1.Location = new System.Drawing.Point(218, 40);
            this.cbEnableTrack1.Name = "cbEnableTrack1";
            this.cbEnableTrack1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbEnableTrack1.Size = new System.Drawing.Size(121, 18);
            this.cbEnableTrack1.TabIndex = 103;
            this.cbEnableTrack1.Text = "فعال سازی نوار یک";
            this.cbEnableTrack1.UseVisualStyleBackColor = true;
            this.cbEnableTrack1.CheckedChanged += new System.EventHandler(this.CbEnableTrack1_CheckedChanged);
            // 
            // txtPrintNumber
            // 
            this.txtPrintNumber.Location = new System.Drawing.Point(380, 476);
            this.txtPrintNumber.Name = "txtPrintNumber";
            this.txtPrintNumber.Size = new System.Drawing.Size(63, 20);
            this.txtPrintNumber.TabIndex = 88;
            this.txtPrintNumber.Text = "1";
            this.txtPrintNumber.TextChanged += new System.EventHandler(this.txtPrintNumber_TextChanged);
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(449, 476);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(63, 21);
            this.radLabel1.TabIndex = 86;
            this.radLabel1.Text = "تعداد چاپ";
            this.radLabel1.Click += new System.EventHandler(this.radLabel1_Click);
            // 
            // btnCleaning
            // 
            this.btnCleaning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnCleaning.Font = new System.Drawing.Font("B Titr", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCleaning.ForeColor = System.Drawing.Color.White;
            this.btnCleaning.Location = new System.Drawing.Point(184, 465);
            this.btnCleaning.Name = "btnCleaning";
            this.btnCleaning.Size = new System.Drawing.Size(84, 34);
            this.btnCleaning.TabIndex = 81;
            this.btnCleaning.Text = "پاکسازی";
            this.btnCleaning.Visible = false;
            this.btnCleaning.Click += new System.EventHandler(this.btnCleaning_Click);
            // 
            // SinglePrint
            // 
            this.AcceptButton = this.btnSinglePrint;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 518);
            this.Controls.Add(this.btnCleaning);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.txtPrintNumber);
            this.Controls.Add(this.btnSinglePrint);
            this.Controls.Add(this.GBDataWrite);
            this.Controls.Add(this.GBPrint);
            this.Controls.Add(this.CardPrewiew);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "SinglePrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "چاپ";
            this.Load += new System.EventHandler(this.SinglePrint_Load);
            this.CardPrewiew.ResumeLayout(false);
            this.TopCard.ResumeLayout(false);
            this.TopCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPenEditTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTop)).EndInit();
            this.BottomCard.ResumeLayout(false);
            this.BottomCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPenEditBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSinglePrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbxHolder1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbxHolder2)).EndInit();
            this.GBPrint.ResumeLayout(false);
            this.GBPrint.PerformLayout();
            this.GBDataWrite.ResumeLayout(false);
            this.GBDataWrite.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCleaning)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl CardPrewiew;
        private System.Windows.Forms.TabPage TopCard;
        private System.Windows.Forms.PictureBox pictureBoxTop;
        private System.Windows.Forms.TabPage BottomCard;
        private System.Windows.Forms.ComboBox cmbPenSelectBottom;
        private System.Windows.Forms.PictureBox pictureBoxBottom;
        private Telerik.WinControls.UI.RadButton btnSinglePrint;
        private System.Windows.Forms.TextBox txtCR3;
        private System.Windows.Forms.TextBox txtCR2;
        private System.Windows.Forms.CheckBox chkCr;
        private System.Windows.Forms.TextBox txtCR1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbRFID;
        private Telerik.WinControls.UI.RadRadioButton rbxHolder1;
        private Telerik.WinControls.UI.RadRadioButton rbxHolder2;
        private System.Windows.Forms.CheckBox chkRotate;
        private System.Windows.Forms.GroupBox GBPrint;
        private System.Windows.Forms.GroupBox GBDataWrite;
        private System.Windows.Forms.ComboBox cmbPenSelectTop;
        private System.Windows.Forms.Label label3;
        private Telerik.WinControls.UI.RadButton btnPenEditBottom;
        private Telerik.WinControls.UI.RadButton btnPenEditTop;
        private System.Windows.Forms.TextBox txtPrintNumber;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadButton btnCleaning;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CmbPicturePenTop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CmbPicturePenBottom;
        private System.Windows.Forms.CheckBox cbEnableTrack3;
        private System.Windows.Forms.CheckBox cbEnableTrack2;
        private System.Windows.Forms.CheckBox cbEnableTrack1;
        private System.Windows.Forms.CheckBox cbMoveToRejectBox;
    }
}