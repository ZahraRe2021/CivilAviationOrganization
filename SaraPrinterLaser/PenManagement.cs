using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using SaraPrinterLaser.Hardware;
using System.IO;

namespace SaraPrinterLaser
{
    public partial class PenManagement : Telerik.WinControls.UI.RadForm
    {
        public PenManagement()
        {
            InitializeComponent();
        }

        private void radTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSabt_Click(object sender, EventArgs e)
        {
            LaserPen resalt = new LaserPen(Convert.ToInt32(txtpennumber2.Text), Convert.ToInt32(txtmarkloop2.Text), Convert.ToDouble(txtmarkspeed2.Text),
               Convert.ToDouble(txtpowerrate2.Text), Convert.ToDouble(txtcurrent2.Text), Convert.ToInt32(txtfreqency2.Text), Convert.ToDouble(txtqpulsewidth2.Text),
               Convert.ToInt32(txtstarttc2.Text), Convert.ToInt32(txtlaserofftc2.Text), Convert.ToInt32(txtendtc2.Text), Convert.ToInt32(txtpolytc2.Text),
               Convert.ToDouble(txtjumpsleep2.Text), Convert.ToInt32(txtjumppostc2.Text), Convert.ToInt32(txtjumpdisttc2.Text), Convert.ToDouble(txtendjump2.Text),
               Convert.ToDouble(txtaccdist2.Text), Convert.ToDouble(txtpointtime2.Text), chkpulsepointmode2.Checked, Convert.ToInt32(txtpulsenumber2.Text), Convert.ToDouble(txtflyspeed2.Text));

            bl.FileWork.WritePen(txtPenname.Text, resalt);
            DirectoryInfo d = new DirectoryInfo(@"Pen");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.pen"); //Getting Text files

            cmbPenName.Items.Clear();
            foreach (FileInfo file in Files)
            {
                cmbPenName.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
            }

            MessageBox.Show("ثبت با موفقیت انجام شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();

        }

        private void PenManagement_Load(object sender, EventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(@"Pen");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.pen"); //Getting Text files

            foreach (FileInfo file in Files)
            {
                cmbPenName.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
            }
        }

        private void cmbPenName_SelectedValueChanged(object sender, EventArgs e)
        {

            LaserPen readparam = bl.FileWork.readPen(cmbPenName.SelectedItem.ToString());
            txtpennumber.Text = readparam.nPenNo.ToString();
            txtaccdist.Text = readparam.dAccDist.ToString();
            txtcurrent.Text = readparam.dCurrent.ToString();
            txtendcomp.Text = readparam.dEndComp.ToString();
            txtendtc.Text = readparam.nEndTC.ToString();
            txtflyspeed.Text = readparam.dFlySpeed.ToString();
            txtfreqency.Text = readparam.nFreq.ToString();
            txtjumpdisttc.Text = readparam.nJumpDistTC.ToString();
            txtjumpsleep.Text = readparam.dJumpSpeed.ToString();
            txtjupmpostc.Text = readparam.nJumpPosTC.ToString();
            txtlaserofftc.Text = readparam.nLaserOffTC.ToString();
            txtmarkloop.Text = readparam.nMarkLoop.ToString();
            txtmarkspeed.Text = readparam.dMarkSpeed.ToString();
            txtpointtime.Text = readparam.dPointTime.ToString();
            txtpolytc.Text = readparam.nPolyTC.ToString();
            txtpowerrate.Text = readparam.dPowerRatio.ToString();
            txtpulsenumber.Text = readparam.nPulseNum.ToString();
            txtqpulsewidth.Text = readparam.dQPulseWidth.ToString();
            txtstarttc.Text = readparam.nStartTC.ToString();
            chkpulsepointmode.Checked = readparam.bPulsePointMode;

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            LaserPen resalt = new LaserPen(Convert.ToInt32(txtpennumber.Text), Convert.ToInt32(txtmarkloop.Text), Convert.ToDouble(txtmarkspeed.Text),
              Convert.ToDouble(txtpowerrate.Text), Convert.ToDouble(txtcurrent.Text), Convert.ToInt32(txtfreqency.Text), Convert.ToDouble(txtqpulsewidth.Text),
              Convert.ToInt32(txtstarttc.Text), Convert.ToInt32(txtlaserofftc.Text), Convert.ToInt32(txtendtc.Text), Convert.ToInt32(txtpolytc.Text),
              Convert.ToDouble(txtjumpsleep.Text), Convert.ToInt32(txtjupmpostc.Text), Convert.ToInt32(txtjumpdisttc.Text), Convert.ToDouble(txtendcomp.Text),
              Convert.ToDouble(txtaccdist.Text), Convert.ToDouble(txtpointtime.Text), chkpulsepointmode.Checked, Convert.ToInt32(txtpulsenumber.Text), Convert.ToDouble(txtflyspeed.Text));

            bl.FileWork.WritePen(cmbPenName.SelectedItem.ToString(), resalt);
            DirectoryInfo d = new DirectoryInfo(@"Pen");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.pen"); //Getting Text files


            MessageBox.Show("ثبت با موفقیت انجام شد","خطا",MessageBoxButtons.OK,MessageBoxIcon.Information);
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            if (cmbPenName.SelectedItem != null)
            {
                DirectoryInfo d = new DirectoryInfo(@"Pen");//Assuming Test is your Folder
                FileInfo[] Files = d.GetFiles("*.pen"); //Getting Text files
                for (int i = 0; i < Files.Length; i++)
                {
                    if (Files[i].Name == cmbPenName.SelectedItem.ToString() + ".pen")
                    {
                        Files[i].Delete();
                        Files[i].Refresh();
                        break;
                    }
                }
                cmbPenName.Items.Clear();
                FileInfo[] Files2 = d.GetFiles("*.pen"); //Getting Text files
                foreach (FileInfo file in Files2)
                {
                    cmbPenName.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
                }
                if (cmbPenName.Items.Count > 0)
                    cmbPenName.SelectedIndex = 0;
                if (cmbPenName.Items.Count == 0)
                    cmbPenName.Text = "";

                MessageBox.Show("قلم با موفقیت حذف گردید.", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("لطفا قلم مورد نظر را انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtpointtime2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtstarttc2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
