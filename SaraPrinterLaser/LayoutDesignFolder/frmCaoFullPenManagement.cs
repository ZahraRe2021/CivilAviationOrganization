using SaraPrinterLaser.Hardware;
using System;
using System.IO;
using System.Windows.Forms;

namespace SaraPrinterLaser.LayoutDesignFolder
{
    public partial class frmCaoFullPenManagement : Telerik.WinControls.UI.RadForm
    {
        public frmCaoFullPenManagement()
        {
            InitializeComponent();
        }

        private void radTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSabt_Click(object sender, EventArgs e)
        {
            int bmpttrib = 0, bmpScanAttr = 0;

            if (chbInvert2.Checked) bmpScanAttr += Laser.BMPSCAN_INVERT;
            if (chbGray2.Checked) bmpScanAttr += Laser.BMPSCAN_GRAY;
            if (chbLighten2.Checked) bmpScanAttr += Laser.BMPSCAN_LIGHT;
            if (chbDither2.Checked) bmpScanAttr += Laser.BMPSCAN_DITHER;
            if (chbBidirectional2.Checked) bmpScanAttr += Laser.BMPSCAN_BIDIR;
            if (chbYscan2.Checked) bmpScanAttr += Laser.BMPSCAN_YDIR;
            if (chbDrillMode2.Checked) bmpScanAttr += Laser.BMPSCAN_DRILL;
            if (chbPower2.Checked) bmpScanAttr += Laser.BMPSCAN_POWER;
            if (chbOffSetPT2.Checked) bmpScanAttr += Laser.BMPSCAN_OFFSETPT;
            if (chbOptimize2.Checked) bmpScanAttr += Laser.BMPSCAN_OPTIMIZE;


            if (chbDynamicFile2.Checked) bmpttrib += Laser.BMPATTRIB_DYNFILE;
            if (chbblDPIfixedWidth2.Checked) bmpttrib += Laser.BMPATTRIB_IMPORTFIXED_WIDTH;
            if (chbblDPIfixedHeight2.Checked) bmpttrib += Laser.BMPATTRIB_IMPORTFIXED_HEIGHT;
            if (chbEnableDpi2.Checked) bmpttrib += Laser.BMPATTRIB_IMPORTFIXED_DPI;



            CAOspecificCardLaserPen resalt = new CAOspecificCardLaserPen(txtPenname.Text, Convert.ToInt32(txtpennumber2.Text), Convert.ToInt32(txtmarkloop2.Text), Convert.ToDouble(txtmarkspeed2.Text),
               Convert.ToDouble(txtpowerrate2.Text), Convert.ToDouble(txtcurrent2.Text), Convert.ToInt32(txtfreqency2.Text), Convert.ToDouble(txtqpulsewidth2.Text),
               Convert.ToInt32(txtstarttc2.Text), Convert.ToInt32(txtlaserofftc2.Text), Convert.ToInt32(txtendtc2.Text), Convert.ToInt32(txtpolytc2.Text),
               Convert.ToDouble(txtjumpsleep2.Text), Convert.ToInt32(txtjumppostc2.Text), Convert.ToInt32(txtjumpdisttc2.Text), Convert.ToDouble(txtendjump2.Text),
               Convert.ToDouble(txtaccdist2.Text), Convert.ToDouble(txtpointtime2.Text), chkpulsepointmode2.Checked, Convert.ToInt32(txtpulsenumber2.Text), Convert.ToDouble(txtflyspeed2.Text), Convert.ToInt32(cbAlignment2.SelectedItem.ToString()), Convert.ToInt32(txtdpi2.Text), Convert.ToInt32(txtDisableLowGrayPoint2.Text), bmpScanAttr, bmpttrib, Convert.ToDouble(txtBrightness2.Text), Convert.ToDouble(txtContrast2.Text), Convert.ToDouble(txtDrillMode2.Text), Convert.ToDouble(txtRatio2.Text), chbInvert2.Checked, chbGray2.Checked, chbDither2.Checked, chbBidirectional2.Checked, chbYscan2.Checked, chbDisableMarkLowGray2.Checked, chbPower2.Checked, chbOffSetPT2.Checked, chbOptimize2.Checked, chbblDPIfixedWidth2.Checked, chbblDPIfixedHeight2.Checked, chbLighten2.Checked, chbDrillMode2.Checked, chbEnableDpi2.Checked, chbDynamicFile2.Checked, cbHatchFile2.Checked);

            new dl.ParametersSave().SaveParameters(resalt);
            //bl.FileWork.WriteCAOspecificCardLaserPen(txtPenname.Text, resalt);
            //DirectoryInfo d = new DirectoryInfo(@"CAO Specific Card Laser Pen");//Assuming Test is your Folder
            //FileInfo[] Files = d.GetFiles("*.CAOPEN"); //Getting Text files

            //cmbPenName.Items.Clear();
            //foreach (FileInfo file in Files)
            //{
            //    cmbPenName.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
            //}

            MessageBox.Show(StatusClass.Message_SucsessOpration, StatusClass.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();

        }

        private void PenManagement_Load(object sender, EventArgs e)
        {


            foreach (string item in new dl.ParametersSave().GetPennames())
            {
                cmbPenName.Items.Add(item);
            }
            
            cbAlignment2.SelectedItem = cbAlignment2.Items[0];
        }

        private void cmbPenName_SelectedValueChanged(object sender, EventArgs e)
        {

            CAOspecificCardLaserPen readparam = new dl.ParametersSave().ParametersLoad(cmbPenName.SelectedItem.ToString());
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

            foreach (var item in cbAlignment.Items)
            {
                if (item.ToString().Trim() == readparam.nAlign.ToString().Trim())
                    cbAlignment.SelectedItem = cbAlignment.Items[readparam.nAlign];
            }
            txtdpi.Text = readparam.ndpi.ToString();
            txtDisableLowGrayPoint.Text = readparam.nminLowGrayPt.ToString();
            txtBrightness.Text = readparam.dBrightness.ToString();
            txtContrast.Text = readparam.dContrast.ToString();
            txtDrillMode.Text = readparam.dSettingPointTime.ToString();
            txtRatio.Text = readparam.dRatio.ToString();

            chbEnableDpi.Checked = readparam.blfixDPI;
            chbblDPIfixedWidth.Checked = readparam.blDPIfixedWidth;
            chbblDPIfixedHeight.Checked = readparam.blDPIfixedHeight;
            chbDisableMarkLowGray.Checked = readparam.blDisableMarkLowGray;
            chbLighten.Checked = readparam.blBrightness;
            chbDrillMode.Checked = readparam.blDrill;
            chbGray.Checked = readparam.blGray;
            chbBidirectional.Checked = readparam.blBidirectional;
            chbYscan.Checked = readparam.blYscan;
            chbDither.Checked = readparam.blDither;
            chbPower.Checked = readparam.blPower;
            chbOffSetPT.Checked = readparam.blOffSetPT;
            chbOptimize.Checked = readparam.blOptimize;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int bmpttrib = 0, bmpScanAttr = 0;

            if (chbInvert.Checked) bmpScanAttr += Laser.BMPSCAN_INVERT;
            if (chbGray.Checked) bmpScanAttr += Laser.BMPSCAN_GRAY;
            if (chbLighten.Checked) bmpScanAttr += Laser.BMPSCAN_LIGHT;
            if (chbDither.Checked) bmpScanAttr += Laser.BMPSCAN_DITHER;
            if (chbBidirectional.Checked) bmpScanAttr += Laser.BMPSCAN_BIDIR;
            if (chbYscan.Checked) bmpScanAttr += Laser.BMPSCAN_YDIR;
            if (chbDrillMode.Checked) bmpScanAttr += Laser.BMPSCAN_DRILL;
            if (chbPower.Checked) bmpScanAttr += Laser.BMPSCAN_POWER;
            if (chbOffSetPT.Checked) bmpScanAttr += Laser.BMPSCAN_OFFSETPT;
            if (chbOptimize.Checked) bmpScanAttr += Laser.BMPSCAN_OPTIMIZE;


            if (chbDynamicFile.Checked) bmpttrib += Laser.BMPATTRIB_DYNFILE;
            if (chbblDPIfixedWidth.Checked) bmpttrib += Laser.BMPATTRIB_IMPORTFIXED_WIDTH;
            if (chbblDPIfixedHeight.Checked) bmpttrib += Laser.BMPATTRIB_IMPORTFIXED_HEIGHT;
            if (chbEnableDpi.Checked) bmpttrib += Laser.BMPATTRIB_IMPORTFIXED_DPI;


            CAOspecificCardLaserPen resalt = new CAOspecificCardLaserPen(cmbPenName.SelectedItem.ToString(), Convert.ToInt32(txtpennumber.Text), Convert.ToInt32(txtmarkloop.Text), Convert.ToDouble(txtmarkspeed.Text),
               Convert.ToDouble(txtpowerrate.Text), Convert.ToDouble(txtcurrent.Text), Convert.ToInt32(txtfreqency.Text), Convert.ToDouble(txtqpulsewidth.Text),
               Convert.ToInt32(txtstarttc.Text), Convert.ToInt32(txtlaserofftc.Text), Convert.ToInt32(txtendtc.Text), Convert.ToInt32(txtpolytc.Text),
               Convert.ToDouble(txtjumpsleep.Text), Convert.ToInt32(txtjupmpostc.Text), Convert.ToInt32(txtjumpdisttc.Text), Convert.ToDouble(txtendcomp.Text),
               Convert.ToDouble(txtaccdist.Text), Convert.ToDouble(txtpointtime.Text), chkpulsepointmode.Checked, Convert.ToInt32(txtpulsenumber.Text), Convert.ToDouble(txtflyspeed.Text), Convert.ToInt32(cbAlignment.SelectedItem), Convert.ToInt32(txtdpi.Text), Convert.ToInt32(txtDisableLowGrayPoint.Text), bmpScanAttr, bmpttrib, Convert.ToDouble(txtBrightness.Text), Convert.ToDouble(txtContrast.Text), Convert.ToDouble(txtDrillMode.Text), Convert.ToDouble(txtRatio.Text), chbInvert.Checked, chbGray.Checked, chbDither.Checked, chbBidirectional.Checked, chbYscan.Checked, chbDisableMarkLowGray.Checked, chbPower.Checked, chbOffSetPT.Checked, chbOptimize.Checked, chbblDPIfixedWidth.Checked, chbblDPIfixedHeight.Checked, chbLighten.Checked, chbDrillMode.Checked, chbEnableDpi.Checked, chbDynamicFile.Checked, cbHatchFile.Checked);

            //bl.FileWork.WriteCAOspecificCardLaserPen(cmbPenName.SelectedItem.ToString(), resalt);
            //DirectoryInfo d = new DirectoryInfo(@"CAO Specific Card Laser Pen");//Assuming Test is your Folder
            //FileInfo[] Files = d.GetFiles("*.CAOPEN"); //Getting Text files
            new dl.ParametersSave().ParametersUpdate(resalt, cmbPenName.SelectedItem.ToString());


            MessageBox.Show(StatusClass.Message_SucsessOpration, StatusClass.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                DirectoryInfo d = new DirectoryInfo(@"CAO Specific Card Laser Pen");//Assuming Test is your Folder
                FileInfo[] Files = d.GetFiles("*.CAOPEN"); //Getting Text files
                for (int i = 0; i < Files.Length; i++)
                {
                    if (Files[i].Name == cmbPenName.SelectedItem.ToString() + ".CAOPEN")
                    {
                        Files[i].Delete();
                        Files[i].Refresh();
                        break;
                    }
                }
                cmbPenName.Items.Clear();
                FileInfo[] Files2 = d.GetFiles("*.CAOPEN"); //Getting Text files
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

        private void EditPen_Click(object sender, EventArgs e)
        {

        }

        private void NewPen_Click(object sender, EventArgs e)
        {

        }
    }
}
