using SaraPrinterLaser.Properties;
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
    public partial class ChangePasswordPage : Telerik.WinControls.UI.RadForm
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtnewpass.Text == txtconferm.Text)
            {

                string errorLogin = "login problem ";
                Settings tanzim = new Settings();
                if (bl.Auth.CheckUser(tanzim.UserName, txtoldpass.Text, ref errorLogin))
                {

                    new dl.Users().UpdatePass(tanzim.UserName, txtnewpass.Text);
                    MessageBox.Show("رمز با موفقیت تغییر کرد");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("رمز اشتباه است");
                }

            }
            else
            {
                MessageBox.Show(" رمز و تکرار رمز یکسان نیست.");
            }
        }
    }
}
