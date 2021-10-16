namespace SaraPrinterLaser
{
    partial class UserManage
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.chbActive = new Telerik.WinControls.UI.RadCheckBox();
            this.btnSabt = new Telerik.WinControls.UI.RadButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDiscription = new Telerik.WinControls.UI.RadTextBoxControl();
            this.txtConfirmPass = new Telerik.WinControls.UI.RadTextBox();
            this.txtPassword = new Telerik.WinControls.UI.RadTextBox();
            this.txtUserName = new Telerik.WinControls.UI.RadTextBox();
            this.ManageUser = new System.Windows.Forms.TabControl();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chbActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSabt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmPass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName)).BeginInit();
            this.ManageUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabPage1.Controls.Add(this.cmbRole);
            this.tabPage1.Controls.Add(this.chbActive);
            this.tabPage1.Controls.Add(this.btnSabt);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtDiscription);
            this.tabPage1.Controls.Add(this.txtConfirmPass);
            this.tabPage1.Controls.Add(this.txtPassword);
            this.tabPage1.Controls.Add(this.txtUserName);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(415, 319);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "AddUser";
            // 
            // cmbRole
            // 
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Location = new System.Drawing.Point(8, 101);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(131, 21);
            this.cmbRole.TabIndex = 3;
            // 
            // chbActive
            // 
            this.chbActive.Location = new System.Drawing.Point(307, 235);
            this.chbActive.Name = "chbActive";
            this.chbActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chbActive.Size = new System.Drawing.Size(74, 18);
            this.chbActive.TabIndex = 5;
            this.chbActive.Text = "فعال سازی ";
            this.chbActive.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chbActive_ToggleStateChanged);
            // 
            // btnSabt
            // 
            this.btnSabt.Location = new System.Drawing.Point(185, 259);
            this.btnSabt.Name = "btnSabt";
            this.btnSabt.Size = new System.Drawing.Size(110, 24);
            this.btnSabt.TabIndex = 6;
            this.btnSabt.Text = "ثبت";
            this.btnSabt.Click += new System.EventHandler(this.btnSabt_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(184, 104);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "سطح دسترسی :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(301, 134);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "توضیحات کاربر :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(145, 74);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "تایید گذرواژه :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 44);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "گذرواژه :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 8);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "نام کاربری :";
            // 
            // txtDiscription
            // 
            this.txtDiscription.Location = new System.Drawing.Point(8, 134);
            this.txtDiscription.Name = "txtDiscription";
            this.txtDiscription.Size = new System.Drawing.Size(287, 119);
            this.txtDiscription.TabIndex = 4;
            // 
            // txtConfirmPass
            // 
            this.txtConfirmPass.Location = new System.Drawing.Point(8, 72);
            this.txtConfirmPass.Name = "txtConfirmPass";
            this.txtConfirmPass.PasswordChar = '*';
            this.txtConfirmPass.Size = new System.Drawing.Size(131, 20);
            this.txtConfirmPass.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(8, 42);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(131, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(8, 6);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(131, 20);
            this.txtUserName.TabIndex = 0;
            // 
            // ManageUser
            // 
            this.ManageUser.Controls.Add(this.tabPage1);
            this.ManageUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ManageUser.Location = new System.Drawing.Point(0, 0);
            this.ManageUser.Name = "ManageUser";
            this.ManageUser.SelectedIndex = 0;
            this.ManageUser.Size = new System.Drawing.Size(423, 345);
            this.ManageUser.TabIndex = 0;
            // 
            // UserManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 345);
            this.Controls.Add(this.ManageUser);
            this.Name = "UserManage";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "مدیریت نام کاربری";
            this.Load += new System.EventHandler(this.UserManage_Load);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chbActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSabt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmPass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName)).EndInit();
            this.ManageUser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cmbRole;
        private Telerik.WinControls.UI.RadCheckBox chbActive;
        private Telerik.WinControls.UI.RadButton btnSabt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadTextBoxControl txtDiscription;
        private Telerik.WinControls.UI.RadTextBox txtConfirmPass;
        private Telerik.WinControls.UI.RadTextBox txtPassword;
        private Telerik.WinControls.UI.RadTextBox txtUserName;
        private System.Windows.Forms.TabControl ManageUser;
    }
}
