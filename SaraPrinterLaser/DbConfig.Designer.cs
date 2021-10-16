namespace SaraPrinterLaser
{
    partial class DbConfig
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
            this.txtFileAdress = new Telerik.WinControls.UI.RadTextBox();
            this.txtPassword = new Telerik.WinControls.UI.RadTextBox();
            this.txtPassConfirm = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.btnsabt = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileAdress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassConfirm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnsabt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFileAdress
            // 
            this.txtFileAdress.Location = new System.Drawing.Point(90, 12);
            this.txtFileAdress.Name = "txtFileAdress";
            this.txtFileAdress.Size = new System.Drawing.Size(319, 20);
            this.txtFileAdress.TabIndex = 0;
            this.txtFileAdress.Text = "printer.accdb";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(90, 53);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(319, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.Text = "A988b988";
            // 
            // txtPassConfirm
            // 
            this.txtPassConfirm.Location = new System.Drawing.Point(90, 80);
            this.txtPassConfirm.Name = "txtPassConfirm";
            this.txtPassConfirm.PasswordChar = '*';
            this.txtPassConfirm.Size = new System.Drawing.Size(319, 20);
            this.txtPassConfirm.TabIndex = 2;
            this.txtPassConfirm.Text = "A988b988";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(12, 14);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(55, 18);
            this.radLabel1.TabIndex = 3;
            this.radLabel1.Text = "آدرس فایل";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(12, 55);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(39, 18);
            this.radLabel2.TabIndex = 4;
            this.radLabel2.Text = "گذرواژه";
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(12, 82);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(62, 18);
            this.radLabel3.TabIndex = 5;
            this.radLabel3.Text = "تایید گذرواژه";
            // 
            // btnsabt
            // 
            this.btnsabt.Location = new System.Drawing.Point(159, 106);
            this.btnsabt.Name = "btnsabt";
            this.btnsabt.Size = new System.Drawing.Size(164, 49);
            this.btnsabt.TabIndex = 6;
            this.btnsabt.Text = "ثبت";
            this.btnsabt.Click += new System.EventHandler(this.btnsabt_Click);
            // 
            // DbConfig
            // 
            this.AcceptButton = this.btnsabt;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 189);
            this.Controls.Add(this.btnsabt);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.txtPassConfirm);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtFileAdress);
            this.Name = "DbConfig";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "تنظیمات پایگاه داده";
            ((System.ComponentModel.ISupportInitialize)(this.txtFileAdress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassConfirm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnsabt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox txtFileAdress;
        private Telerik.WinControls.UI.RadTextBox txtPassword;
        private Telerik.WinControls.UI.RadTextBox txtPassConfirm;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadButton btnsabt;
    }
}
