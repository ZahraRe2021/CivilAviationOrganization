namespace SaraPrinterLaser
{
    partial class ChangePasswordPage
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
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.txtnewpass = new Telerik.WinControls.UI.RadTextBox();
            this.txtconferm = new Telerik.WinControls.UI.RadTextBox();
            this.txtoldpass = new Telerik.WinControls.UI.RadTextBox();
            this.btnOk = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnewpass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtconferm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtoldpass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(17, 11);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(46, 18);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "رمز قدیم";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(17, 35);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(46, 18);
            this.radLabel2.TabIndex = 1;
            this.radLabel2.Text = "رمز جدبد";
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(17, 59);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(45, 18);
            this.radLabel3.TabIndex = 2;
            this.radLabel3.Text = "تکرار رمز";
            // 
            // txtnewpass
            // 
            this.txtnewpass.Location = new System.Drawing.Point(78, 35);
            this.txtnewpass.Name = "txtnewpass";
            this.txtnewpass.PasswordChar = '*';
            this.txtnewpass.Size = new System.Drawing.Size(180, 20);
            this.txtnewpass.TabIndex = 1;
            // 
            // txtconferm
            // 
            this.txtconferm.Location = new System.Drawing.Point(78, 61);
            this.txtconferm.Name = "txtconferm";
            this.txtconferm.PasswordChar = '*';
            this.txtconferm.Size = new System.Drawing.Size(180, 20);
            this.txtconferm.TabIndex = 2;
            // 
            // txtoldpass
            // 
            this.txtoldpass.Location = new System.Drawing.Point(78, 12);
            this.txtoldpass.Name = "txtoldpass";
            this.txtoldpass.PasswordChar = '*';
            this.txtoldpass.Size = new System.Drawing.Size(180, 20);
            this.txtoldpass.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(98, 87);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(131, 49);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "ثبت";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // ChangePasswordPage
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 170);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtconferm);
            this.Controls.Add(this.txtoldpass);
            this.Controls.Add(this.txtnewpass);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radLabel1);
            this.Name = "ChangePasswordPage";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "تغییر گذر واژه";
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnewpass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtconferm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtoldpass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadTextBox txtnewpass;
        private Telerik.WinControls.UI.RadTextBox txtconferm;
        private Telerik.WinControls.UI.RadTextBox txtoldpass;
        private Telerik.WinControls.UI.RadButton btnOk;
    }
}
