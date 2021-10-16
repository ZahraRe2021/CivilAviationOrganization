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
    public partial class DbConfig : Telerik.WinControls.UI.RadForm
    {
        public DbConfig()
        {
            InitializeComponent();
        }

        private void btnsabt_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text==txtPassConfirm.Text)
            {
                Settings tanzim = new Settings();
                tanzim.ConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Jet OLEDB:Database Password={1};",txtFileAdress.Text,txtPassword.Text);
                dl.DataAccessClass.ConnectionString = tanzim.ConnectionString;
                tanzim.Save();
                MessageBox.Show("ثبت با موفقیت انجام شد.");
                txtPassword.Text = "";
                txtPassConfirm.Text = "";
                this.Close();
            }
            else
            {
                txtPassword.Text = "";
                txtPassConfirm.Text = "";
                MessageBox.Show("رمز عبور یکسان نیست");
            }
            
            

        }
    }
}
