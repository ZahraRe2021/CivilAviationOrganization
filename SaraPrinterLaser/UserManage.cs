using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace SaraPrinterLaser
{
    public partial class UserManage : Telerik.WinControls.UI.RadForm
    {
        public UserManage()
        {
            InitializeComponent();
        }

        private void chbActive_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {

        }

        private void UserManage_Load(object sender, EventArgs e)
        {
            DataSet dSet = new dl.Role().RoleIdName();
            cmbRole.DisplayMember = "Name";
            cmbRole.ValueMember = "ID";
            cmbRole.DataSource = dSet.Tables[0];
        }

        private void btnSabt_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == txtConfirmPass.Text)
            {
                int isactive = 0;
                if (chbActive.Checked)
                {
                    isactive = 1;
                }

                new dl.Users().InsertUser(txtUserName.Text, txtPassword.Text, txtDiscription.Text, Convert.ToInt32(cmbRole.SelectedValue), isactive);

                txtPassword.Text = "";
                txtConfirmPass.Text = "";
                txtDiscription.Text = "";
                txtUserName.Text = "";
                MessageBox.Show("ثبت انجام شد", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();




            }
            else
            {
                txtPassword.Text = "";
                txtConfirmPass.Text = "";
                MessageBox.Show("رمز را با دقت وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }

        }
    }
}
