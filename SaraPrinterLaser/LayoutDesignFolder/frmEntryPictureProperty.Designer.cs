namespace SaraPrinterLaser.LayoutDesignFolder
{
    partial class frmEntryPictureProperty
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
            this.btnExit = new Telerik.WinControls.UI.RadButton();
            this.cmbDatabaseRow = new System.Windows.Forms.ComboBox();
            this.lblDatabaseRow = new System.Windows.Forms.Label();
            this.cmbLaserPen = new System.Windows.Forms.ComboBox();
            this.btnChangeParameter = new Telerik.WinControls.UI.RadButton();
            this.lblParameter = new System.Windows.Forms.Label();
            this.txtYPos = new System.Windows.Forms.TextBox();
            this.txtXpos = new System.Windows.Forms.TextBox();
            this.lblYPos = new System.Windows.Forms.Label();
            this.lblXPos = new System.Windows.Forms.Label();
            this.btnSabt = new Telerik.WinControls.UI.RadButton();
            this.btnDefualt = new Telerik.WinControls.UI.RadButton();
            this.lblFont = new System.Windows.Forms.Label();
            this.txtImageAddressPath = new System.Windows.Forms.TextBox();
            this.btnEntryPicture = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChangeParameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSabt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDefualt)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(422, 202);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(84, 34);
            this.btnExit.TabIndex = 93;
            this.btnExit.Text = "خروج ";
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // cmbDatabaseRow
            // 
            this.cmbDatabaseRow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatabaseRow.FormattingEnabled = true;
            this.cmbDatabaseRow.Location = new System.Drawing.Point(12, 166);
            this.cmbDatabaseRow.Name = "cmbDatabaseRow";
            this.cmbDatabaseRow.Size = new System.Drawing.Size(332, 21);
            this.cmbDatabaseRow.TabIndex = 104;
            // 
            // lblDatabaseRow
            // 
            this.lblDatabaseRow.AutoSize = true;
            this.lblDatabaseRow.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabaseRow.Location = new System.Drawing.Point(350, 163);
            this.lblDatabaseRow.Name = "lblDatabaseRow";
            this.lblDatabaseRow.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblDatabaseRow.Size = new System.Drawing.Size(156, 24);
            this.lblDatabaseRow.TabIndex = 103;
            this.lblDatabaseRow.Text = "نام ستون پایگاه داده:";
            // 
            // cmbLaserPen
            // 
            this.cmbLaserPen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLaserPen.FormattingEnabled = true;
            this.cmbLaserPen.Location = new System.Drawing.Point(12, 122);
            this.cmbLaserPen.Name = "cmbLaserPen";
            this.cmbLaserPen.Size = new System.Drawing.Size(282, 21);
            this.cmbLaserPen.TabIndex = 102;
            // 
            // btnChangeParameter
            // 
            this.btnChangeParameter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnChangeParameter.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeParameter.ForeColor = System.Drawing.Color.White;
            this.btnChangeParameter.Location = new System.Drawing.Point(422, 117);
            this.btnChangeParameter.Name = "btnChangeParameter";
            this.btnChangeParameter.Size = new System.Drawing.Size(84, 34);
            this.btnChangeParameter.TabIndex = 96;
            this.btnChangeParameter.Text = "پارامتر لیزر";
            this.btnChangeParameter.Click += new System.EventHandler(this.BtnChangeParameter_Click);
            // 
            // lblParameter
            // 
            this.lblParameter.AutoSize = true;
            this.lblParameter.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParameter.Location = new System.Drawing.Point(300, 119);
            this.lblParameter.Name = "lblParameter";
            this.lblParameter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblParameter.Size = new System.Drawing.Size(115, 24);
            this.lblParameter.TabIndex = 101;
            this.lblParameter.Text = "پامترهای لیزر:";
            // 
            // txtYPos
            // 
            this.txtYPos.Location = new System.Drawing.Point(258, 83);
            this.txtYPos.Name = "txtYPos";
            this.txtYPos.Size = new System.Drawing.Size(153, 20);
            this.txtYPos.TabIndex = 100;
            this.txtYPos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtYPos_KeyPress);
            // 
            // txtXpos
            // 
            this.txtXpos.Location = new System.Drawing.Point(258, 47);
            this.txtXpos.Name = "txtXpos";
            this.txtXpos.Size = new System.Drawing.Size(153, 20);
            this.txtXpos.TabIndex = 99;
            this.txtXpos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtXpos_KeyPress);
            // 
            // lblYPos
            // 
            this.lblYPos.AutoSize = true;
            this.lblYPos.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYPos.Location = new System.Drawing.Point(417, 81);
            this.lblYPos.Name = "lblYPos";
            this.lblYPos.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblYPos.Size = new System.Drawing.Size(89, 24);
            this.lblYPos.TabIndex = 98;
            this.lblYPos.Text = "موقعیت Y:";
            // 
            // lblXPos
            // 
            this.lblXPos.AutoSize = true;
            this.lblXPos.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXPos.Location = new System.Drawing.Point(417, 45);
            this.lblXPos.Name = "lblXPos";
            this.lblXPos.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblXPos.Size = new System.Drawing.Size(89, 24);
            this.lblXPos.TabIndex = 97;
            this.lblXPos.Text = "موقعیت X:";
            // 
            // btnSabt
            // 
            this.btnSabt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnSabt.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSabt.ForeColor = System.Drawing.Color.White;
            this.btnSabt.Location = new System.Drawing.Point(12, 202);
            this.btnSabt.Name = "btnSabt";
            this.btnSabt.Size = new System.Drawing.Size(84, 34);
            this.btnSabt.TabIndex = 94;
            this.btnSabt.Text = "ثبت";
            this.btnSabt.Click += new System.EventHandler(this.BtnSabt_Click);
            // 
            // btnDefualt
            // 
            this.btnDefualt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnDefualt.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefualt.ForeColor = System.Drawing.Color.White;
            this.btnDefualt.Location = new System.Drawing.Point(210, 202);
            this.btnDefualt.Name = "btnDefualt";
            this.btnDefualt.Size = new System.Drawing.Size(84, 34);
            this.btnDefualt.TabIndex = 92;
            this.btnDefualt.Text = "پیش فرض";
            this.btnDefualt.Click += new System.EventHandler(this.BtnDefualt_Click);
            // 
            // lblFont
            // 
            this.lblFont.AutoSize = true;
            this.lblFont.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFont.Location = new System.Drawing.Point(398, 9);
            this.lblFont.Name = "lblFont";
            this.lblFont.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblFont.Size = new System.Drawing.Size(108, 24);
            this.lblFont.TabIndex = 90;
            this.lblFont.Text = "آدرس تصویر:";
            // 
            // txtImageAddressPath
            // 
            this.txtImageAddressPath.Location = new System.Drawing.Point(48, 11);
            this.txtImageAddressPath.Name = "txtImageAddressPath";
            this.txtImageAddressPath.Size = new System.Drawing.Size(344, 20);
            this.txtImageAddressPath.TabIndex = 105;
            // 
            // btnEntryPicture
            // 
            this.btnEntryPicture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnEntryPicture.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEntryPicture.Location = new System.Drawing.Point(12, 11);
            this.btnEntryPicture.Name = "btnEntryPicture";
            this.btnEntryPicture.Size = new System.Drawing.Size(30, 20);
            this.btnEntryPicture.TabIndex = 106;
            this.btnEntryPicture.Text = "...";
            this.btnEntryPicture.UseVisualStyleBackColor = false;
            this.btnEntryPicture.Click += new System.EventHandler(this.BtnEntryPicture_Click);
            // 
            // frmEntryPictureProperty
            // 
            this.AcceptButton = this.btnSabt;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(246)))), ((int)(((byte)(146)))));
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(518, 258);
            this.ControlBox = false;
            this.Controls.Add(this.btnEntryPicture);
            this.Controls.Add(this.txtImageAddressPath);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.cmbDatabaseRow);
            this.Controls.Add(this.lblDatabaseRow);
            this.Controls.Add(this.cmbLaserPen);
            this.Controls.Add(this.btnChangeParameter);
            this.Controls.Add(this.lblParameter);
            this.Controls.Add(this.txtYPos);
            this.Controls.Add(this.txtXpos);
            this.Controls.Add(this.lblYPos);
            this.Controls.Add(this.lblXPos);
            this.Controls.Add(this.btnSabt);
            this.Controls.Add(this.btnDefualt);
            this.Controls.Add(this.lblFont);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmEntryPictureProperty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "فرم مشخصات عکس ";
            this.Load += new System.EventHandler(this.FrmEntryPictureProperty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChangeParameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSabt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDefualt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnExit;
        private System.Windows.Forms.ComboBox cmbDatabaseRow;
        private System.Windows.Forms.Label lblDatabaseRow;
        private System.Windows.Forms.ComboBox cmbLaserPen;
        private Telerik.WinControls.UI.RadButton btnChangeParameter;
        private System.Windows.Forms.Label lblParameter;
        private System.Windows.Forms.TextBox txtYPos;
        private System.Windows.Forms.TextBox txtXpos;
        private System.Windows.Forms.Label lblYPos;
        private System.Windows.Forms.Label lblXPos;
        private Telerik.WinControls.UI.RadButton btnSabt;
        private Telerik.WinControls.UI.RadButton btnDefualt;
        private System.Windows.Forms.Label lblFont;
        private System.Windows.Forms.TextBox txtImageAddressPath;
        private System.Windows.Forms.Button btnEntryPicture;
    }
}