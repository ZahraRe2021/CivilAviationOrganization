using SaraPrinterLaser.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaraPrinterLaser
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtUserName.Text) && !String.IsNullOrWhiteSpace(txtPassword.Text))
            {
                string errorLogin = "login problem ";
                if (bl.Auth.CheckUser(txtUserName.Text, txtPassword.Text, ref errorLogin))
                {
                    Settings tanzim = new Settings();
                    tanzim.UserName = bl.Auth.UserName;
                    tanzim.PassWord = bl.Auth.Password;
                    tanzim.Role = bl.Auth.Role;
                    bl.Auth.UsersID = new dl.Users().GetUserID(bl.Auth.UserName, bl.Auth.Password);
                    tanzim.UsersID = bl.Auth.UsersID;
                    tanzim.IsRemmeber = false;

                    if (chbRemmeber.Checked)
                    {
                        tanzim.IsRemmeber = true;

                    }

                    tanzim.Save();
                    this.DialogResult = DialogResult.OK;
                    this.Close();


                }
                else
                {
                    lblResault.Text = errorLogin;
                }
            }
            else
            {
                lblResault.Text = "لطفا  نام کاربری و رمز عبور را وارد  کنید !!!";

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://www.sarahardware.com/");
            Process.Start(sInfo);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
