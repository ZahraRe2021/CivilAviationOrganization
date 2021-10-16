namespace SaraPrinterLaser.LayoutDesignFolder
{
    partial class frmEntryTextProperty
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
            this.lblFont = new System.Windows.Forms.Label();
            this.lblTextChangeFont = new System.Windows.Forms.Label();
            this.btnSabt = new Telerik.WinControls.UI.RadButton();
            this.btnDefualt = new Telerik.WinControls.UI.RadButton();
            this.btnChangeFont = new Telerik.WinControls.UI.RadButton();
            this.lblXPos = new System.Windows.Forms.Label();
            this.lblYPos = new System.Windows.Forms.Label();
            this.txtXpos = new System.Windows.Forms.TextBox();
            this.txtYPos = new System.Windows.Forms.TextBox();
            this.lblParameter = new System.Windows.Forms.Label();
            this.btnChangeParameter = new Telerik.WinControls.UI.RadButton();
            this.cmbLaserPen = new System.Windows.Forms.ComboBox();
            this.lblDatabaseRow = new System.Windows.Forms.Label();
            this.cmbDatabaseRow = new System.Windows.Forms.ComboBox();
            this.btnExit = new Telerik.WinControls.UI.RadButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTextDirecion = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnSabt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDefualt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChangeFont)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChangeParameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFont
            // 
            this.lblFont.AutoSize = true;
            this.lblFont.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFont.Location = new System.Drawing.Point(336, 17);
            this.lblFont.Name = "lblFont";
            this.lblFont.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblFont.Size = new System.Drawing.Size(83, 24);
            this.lblFont.TabIndex = 0;
            this.lblFont.Text = "فونت متن:";
            // 
            // lblTextChangeFont
            // 
            this.lblTextChangeFont.AutoSize = true;
            this.lblTextChangeFont.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextChangeFont.Location = new System.Drawing.Point(12, 17);
            this.lblTextChangeFont.Name = "lblTextChangeFont";
            this.lblTextChangeFont.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTextChangeFont.Size = new System.Drawing.Size(103, 24);
            this.lblTextChangeFont.TabIndex = 2;
            this.lblTextChangeFont.Text = "Text Font:";
            // 
            // btnSabt
            // 
            this.btnSabt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnSabt.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSabt.ForeColor = System.Drawing.Color.White;
            this.btnSabt.Location = new System.Drawing.Point(16, 245);
            this.btnSabt.Name = "btnSabt";
            this.btnSabt.Size = new System.Drawing.Size(84, 34);
            this.btnSabt.TabIndex = 80;
            this.btnSabt.Text = "ثبت";
            this.btnSabt.Click += new System.EventHandler(this.BtnSabt_Click);
            // 
            // btnDefualt
            // 
            this.btnDefualt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnDefualt.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefualt.ForeColor = System.Drawing.Color.White;
            this.btnDefualt.Location = new System.Drawing.Point(214, 245);
            this.btnDefualt.Name = "btnDefualt";
            this.btnDefualt.Size = new System.Drawing.Size(84, 34);
            this.btnDefualt.TabIndex = 79;
            this.btnDefualt.Text = "پیش فرض";
            this.btnDefualt.Click += new System.EventHandler(this.BtnDefualt_Click);
            // 
            // btnChangeFont
            // 
            this.btnChangeFont.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnChangeFont.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeFont.ForeColor = System.Drawing.Color.White;
            this.btnChangeFont.Location = new System.Drawing.Point(426, 12);
            this.btnChangeFont.Name = "btnChangeFont";
            this.btnChangeFont.Size = new System.Drawing.Size(84, 34);
            this.btnChangeFont.TabIndex = 81;
            this.btnChangeFont.Text = "تغییر فونت";
            this.btnChangeFont.Click += new System.EventHandler(this.BtnChangeFont_Click);
            // 
            // lblXPos
            // 
            this.lblXPos.AutoSize = true;
            this.lblXPos.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXPos.Location = new System.Drawing.Point(421, 63);
            this.lblXPos.Name = "lblXPos";
            this.lblXPos.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblXPos.Size = new System.Drawing.Size(89, 24);
            this.lblXPos.TabIndex = 82;
            this.lblXPos.Text = "موقعیت X:";
            // 
            // lblYPos
            // 
            this.lblYPos.AutoSize = true;
            this.lblYPos.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYPos.Location = new System.Drawing.Point(421, 99);
            this.lblYPos.Name = "lblYPos";
            this.lblYPos.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblYPos.Size = new System.Drawing.Size(89, 24);
            this.lblYPos.TabIndex = 83;
            this.lblYPos.Text = "موقعیت Y:";
            // 
            // txtXpos
            // 
            this.txtXpos.Location = new System.Drawing.Point(262, 65);
            this.txtXpos.Name = "txtXpos";
            this.txtXpos.Size = new System.Drawing.Size(153, 20);
            this.txtXpos.TabIndex = 84;
            this.txtXpos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtXpos_KeyPress);
            // 
            // txtYPos
            // 
            this.txtYPos.Location = new System.Drawing.Point(262, 101);
            this.txtYPos.Name = "txtYPos";
            this.txtYPos.Size = new System.Drawing.Size(153, 20);
            this.txtYPos.TabIndex = 85;
            this.txtYPos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtYPos_KeyPress);
            // 
            // lblParameter
            // 
            this.lblParameter.AutoSize = true;
            this.lblParameter.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParameter.Location = new System.Drawing.Point(304, 162);
            this.lblParameter.Name = "lblParameter";
            this.lblParameter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblParameter.Size = new System.Drawing.Size(115, 24);
            this.lblParameter.TabIndex = 86;
            this.lblParameter.Text = "پامترهای لیزر:";
            // 
            // btnChangeParameter
            // 
            this.btnChangeParameter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnChangeParameter.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeParameter.ForeColor = System.Drawing.Color.White;
            this.btnChangeParameter.Location = new System.Drawing.Point(426, 160);
            this.btnChangeParameter.Name = "btnChangeParameter";
            this.btnChangeParameter.Size = new System.Drawing.Size(84, 34);
            this.btnChangeParameter.TabIndex = 82;
            this.btnChangeParameter.Text = "پارامتر لیزر";
            this.btnChangeParameter.Click += new System.EventHandler(this.BtnChangeParameter_Click);
            // 
            // cmbLaserPen
            // 
            this.cmbLaserPen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLaserPen.FormattingEnabled = true;
            this.cmbLaserPen.Location = new System.Drawing.Point(16, 165);
            this.cmbLaserPen.Name = "cmbLaserPen";
            this.cmbLaserPen.Size = new System.Drawing.Size(282, 21);
            this.cmbLaserPen.TabIndex = 87;
            // 
            // lblDatabaseRow
            // 
            this.lblDatabaseRow.AutoSize = true;
            this.lblDatabaseRow.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabaseRow.Location = new System.Drawing.Point(354, 206);
            this.lblDatabaseRow.Name = "lblDatabaseRow";
            this.lblDatabaseRow.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblDatabaseRow.Size = new System.Drawing.Size(156, 24);
            this.lblDatabaseRow.TabIndex = 88;
            this.lblDatabaseRow.Text = "نام ستون پایگاه داده:";
            // 
            // cmbDatabaseRow
            // 
            this.cmbDatabaseRow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatabaseRow.FormattingEnabled = true;
            this.cmbDatabaseRow.Location = new System.Drawing.Point(16, 209);
            this.cmbDatabaseRow.Name = "cmbDatabaseRow";
            this.cmbDatabaseRow.Size = new System.Drawing.Size(332, 21);
            this.cmbDatabaseRow.TabIndex = 89;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(64)))), ((int)(((byte)(146)))));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(426, 245);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(84, 34);
            this.btnExit.TabIndex = 80;
            this.btnExit.Text = "خروج ";
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(428, 127);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 90;
            this.label1.Text = "جهت متن:";
            // 
            // cmbTextDirecion
            // 
            this.cmbTextDirecion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTextDirecion.FormattingEnabled = true;
            this.cmbTextDirecion.Items.AddRange(new object[] {
            "راست به چپ",
            "چپ به راست"});
            this.cmbTextDirecion.Location = new System.Drawing.Point(262, 129);
            this.cmbTextDirecion.Name = "cmbTextDirecion";
            this.cmbTextDirecion.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbTextDirecion.Size = new System.Drawing.Size(153, 21);
            this.cmbTextDirecion.TabIndex = 91;
            // 
            // frmEntryTextProperty
            // 
            this.AcceptButton = this.btnSabt;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(246)))), ((int)(((byte)(146)))));
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(522, 301);
            this.ControlBox = false;
            this.Controls.Add(this.cmbTextDirecion);
            this.Controls.Add(this.label1);
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
            this.Controls.Add(this.btnChangeFont);
            this.Controls.Add(this.btnSabt);
            this.Controls.Add(this.btnDefualt);
            this.Controls.Add(this.lblTextChangeFont);
            this.Controls.Add(this.lblFont);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmEntryTextProperty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "فرم مشخصات متن";
            this.Load += new System.EventHandler(this.FrmEntryTextProperty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnSabt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDefualt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChangeFont)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChangeParameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFont;
        private System.Windows.Forms.Label lblTextChangeFont;
        private Telerik.WinControls.UI.RadButton btnSabt;
        private Telerik.WinControls.UI.RadButton btnDefualt;
        private Telerik.WinControls.UI.RadButton btnChangeFont;
        private System.Windows.Forms.Label lblXPos;
        private System.Windows.Forms.Label lblYPos;
        private System.Windows.Forms.TextBox txtXpos;
        private System.Windows.Forms.TextBox txtYPos;
        private System.Windows.Forms.Label lblParameter;
        private Telerik.WinControls.UI.RadButton btnChangeParameter;
        private System.Windows.Forms.ComboBox cmbLaserPen;
        private System.Windows.Forms.Label lblDatabaseRow;
        private System.Windows.Forms.ComboBox cmbDatabaseRow;
        private Telerik.WinControls.UI.RadButton btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTextDirecion;
    }
}