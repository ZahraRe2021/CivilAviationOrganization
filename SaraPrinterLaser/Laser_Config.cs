using SaraPrinterLaser.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaraPrinterLaser
{
    public partial class Laser_Config : Form
    {
        public Laser_Config()
        {
            InitializeComponent();
        }
        private void Laser_Config_Load(object sender, EventArgs e)
        {
            LaserConfigClass LaserSetting = new LaserConfigClass();
            LaserSetting = LaserConfigClass.load();

            txtXcenter.Text = LaserSetting.Xcenter.ToString();
            txtYcenter.Text = LaserSetting.Ycenter.ToString();
            txtMarginX.Text = LaserSetting.XMargin.ToString();
            txtMarginY.Text = LaserSetting.YMargin.ToString();
            txtRatio.Text = LaserSetting.Ratio.ToString();
            txtBrightness.Text = LaserSetting.Brightness.ToString();
            txtContrast.Text = LaserSetting.Contrast.ToString();
            txtPointTime.Text = LaserSetting.PointTime.ToString();
            txtminLowGrayPt.Text = LaserSetting.minLowGrayPt.ToString();
            txtDpi.Text = LaserSetting.dpi.ToString();
            txtRotate.Text = LaserSetting.RotateAngle.ToString();



            chbDisableMarkLowGray.Checked = LaserSetting.blDisableMarkLowGray;
            chkInvert.Checked = LaserSetting.blInvert;
            chkGray.Checked = LaserSetting.blGray;
            chkBrightness.Checked = LaserSetting.blBrightness;
            chkDither.Checked = LaserSetting.blDither;
            chkBidirectional.Checked = LaserSetting.blBidirectional;
            chkYscan.Checked = LaserSetting.blYscan;
            chkDrill.Checked = LaserSetting.blDrill;
            chkPower.Checked = LaserSetting.blPower;
            chkOffSetPT.Checked = LaserSetting.blOffSetPT;
            chkOPtimize.Checked = LaserSetting.blOptimize;
            chkDynamic.Checked = LaserSetting.blDynamic;
            chkfixDPI.Checked = LaserSetting.blfixDPI;
            chkDPIfixedWidth.Checked = LaserSetting.blDPIfixedWidth;
            chkDPIfixedHeight.Checked = LaserSetting.blDPIfixedHeight;

            if (chbDisableMarkLowGray.Checked)
            {
                txtminLowGrayPt.Enabled = true;
            }
            else
            {
                txtminLowGrayPt.Enabled = false;
            }


            if (chkBrightness.Checked)
            {
                txtBrightness.Enabled = true;
            }
            else
            {
                txtBrightness.Enabled = false;
            }
            if (chkDrill.Checked)
            {
                txtPointTime.Enabled = true;
            }
            else
            {
                txtPointTime.Enabled = false;
            }
            if (chkfixDPI.Checked)
            {
                txtDpi.Enabled = true;
            }
            else
            {
                txtDpi.Enabled = false;
            }
        }
        private void chbDisableMarkLowGray_CheckedChanged(object sender, EventArgs e)
        {
            if (chbDisableMarkLowGray.Checked)
            {

                txtminLowGrayPt.Enabled = true;
            }
            else
            {
                txtminLowGrayPt.Text = "0";
                txtminLowGrayPt.Enabled = false;
            }
        }
        private void chkBrightness_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBrightness.Checked)
            {
                txtBrightness.Enabled = true;
            }
            else
            {

                txtBrightness.Enabled = false;
            }
        }
        private void chkfixDPI_CheckedChanged(object sender, EventArgs e)
        {
            if (chkfixDPI.Checked)
            {
                txtDpi.Enabled = true;
            }
            else
            {
                txtDpi.Enabled = false;
            }
        }
        private void chkDrill_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDrill.Checked)
            {
                txtPointTime.Enabled = true;
            }
            else
            {
                txtPointTime.Enabled = false;
            }
        }
        private void btnSabt_Click_1(object sender, EventArgs e)
        {
            SaveParameter();
        }
        private void btnDefualt_Click(object sender, EventArgs e)
        {
            txtXcenter.Text ="0";
            txtYcenter.Text ="0";
            txtRatio.Text ="1";
            txtBrightness.Text ="0";
            txtContrast.Text ="0";
            txtminLowGrayPt.Text ="90";
            txtPointTime.Text ="0.1";
            txtDpi.Text ="600";



            chbDisableMarkLowGray.Checked =true;
            chkInvert.Checked =false;
            chkGray.Checked =false;
            chkBrightness.Checked =false;
            chkDither.Checked =false;
            chkBidirectional.Checked =false;
            chkYscan.Checked =false;
            chkDrill.Checked =false;
            chkPower.Checked =false;
            chkOffSetPT.Checked =false;
            chkOPtimize.Checked =false;


            chkDynamic.Checked =false;
            chkDPIfixedWidth.Checked =true;
            chkDPIfixedHeight.Checked =true;
            chkfixDPI.Checked =true;
            SaveParameter();
        }
        public void SaveParameter()
        {
            try
            {
                LaserConfigClass LaserSetting = new LaserConfigClass();

                bool flgWrongSetting = false;
                LaserSetting.Xcenter = 0;
                LaserSetting.Ycenter = 0;

                LaserSetting.XMargin = 0;
                LaserSetting.YMargin = 0;

                LaserSetting.Ratio = 0;
                LaserSetting.Brightness = 0;
                LaserSetting.Contrast = 0;
                LaserSetting.minLowGrayPt = 0;
                LaserSetting.PointTime = 0;
                LaserSetting.dpi = 0;
                LaserSetting.bmpScanAttr = 0;
                LaserSetting.bmpttrib = 0;

                LaserSetting.blInvert = false;
                LaserSetting.blGray = false;
                LaserSetting.blDither = false;
                LaserSetting.blBidirectional = false;
                LaserSetting.blYscan = false;
                LaserSetting.blPower = false;
                LaserSetting.blOffSetPT = false;
                LaserSetting.blOptimize = false;
                LaserSetting.blDynamic = false;
                LaserSetting.blDPIfixedWidth = false;
                LaserSetting.blDPIfixedHeight = false;
                LaserSetting.blBrightness = false;
                LaserSetting.blDrill = false;
                LaserSetting.blfixDPI = false;
                if (double.Parse(txtXcenter.Text) > 50) flgWrongSetting = true;
                if (double.Parse(txtYcenter.Text) > 25) flgWrongSetting = true;
                if (double.Parse(txtRatio.Text) != 1) flgWrongSetting = true;
                if (double.Parse(txtBrightness.Text) > 1 || double.Parse(txtBrightness.Text) < -1) flgWrongSetting = true;
                if (double.Parse(txtContrast.Text) > 1 || double.Parse(txtContrast.Text) < -1) flgWrongSetting = true;
                if (int.Parse(txtContrast.Text) > 150 && int.Parse(txtContrast.Text) < 90) flgWrongSetting = true;
                if (double.Parse(txtRotate.Text) > 360 && double.Parse(txtRotate.Text) <= 0) flgWrongSetting = true;

                if (!flgWrongSetting)
                {


                    LaserSetting.Xcenter = double.Parse(txtXcenter.Text);
                    LaserSetting.Ycenter = double.Parse(txtYcenter.Text);

                    LaserSetting.XMargin = double.Parse(txtMarginX.Text);
                    LaserSetting.YMargin = double.Parse(txtMarginY.Text);

                    LaserSetting.Ratio = double.Parse(txtRatio.Text);
                    LaserSetting.Brightness = double.Parse(txtBrightness.Text);
                    LaserSetting.Contrast = double.Parse(txtContrast.Text);
                    LaserSetting.minLowGrayPt = int.Parse(txtminLowGrayPt.Text);
                    LaserSetting.PointTime = double.Parse(txtPointTime.Text);
                    LaserSetting.dpi = int.Parse(txtDpi.Text);
                    LaserSetting.RotateAngle = double.Parse(txtRotate.Text);

                    if (chbDisableMarkLowGray.Checked) { LaserSetting.blDisableMarkLowGray = true; }
                    if (chkInvert.Checked) { LaserSetting.bmpScanAttr += Laser.BMPSCAN_INVERT; LaserSetting.blInvert = true; }
                    if (chkGray.Checked) { LaserSetting.bmpScanAttr += Laser.BMPSCAN_GRAY; LaserSetting.blGray = true; }
                    if (chkBrightness.Checked) { LaserSetting.bmpScanAttr += Laser.BMPSCAN_LIGHT; LaserSetting.blBrightness = true; }
                    if (chkDither.Checked) { LaserSetting.bmpScanAttr += Laser.BMPSCAN_DITHER; LaserSetting.blDither = true; }
                    if (chkBidirectional.Checked) { LaserSetting.bmpScanAttr += Laser.BMPSCAN_BIDIR; LaserSetting.blBidirectional = true; }
                    if (chkYscan.Checked) { LaserSetting.bmpScanAttr += Laser.BMPSCAN_YDIR; LaserSetting.blYscan = true; }
                    if (chkDrill.Checked) { LaserSetting.bmpScanAttr += Laser.BMPSCAN_DRILL; LaserSetting.blDrill = true; }
                    if (chkPower.Checked) { LaserSetting.bmpScanAttr += Laser.BMPSCAN_POWER; LaserSetting.blPower = true; }
                    if (chkOffSetPT.Checked) { LaserSetting.bmpScanAttr += Laser.BMPSCAN_OFFSETPT; LaserSetting.blOffSetPT = true; }
                    if (chkOPtimize.Checked) { LaserSetting.bmpScanAttr += Laser.BMPSCAN_OPTIMIZE; LaserSetting.blOptimize = true; }


                    if (chkDynamic.Checked) { LaserSetting.bmpttrib += Laser.BMPATTRIB_DYNFILE; LaserSetting.blDynamic = true; }
                    if (chkDPIfixedWidth.Checked) { LaserSetting.bmpttrib += Laser.BMPATTRIB_IMPORTFIXED_WIDTH; LaserSetting.blDPIfixedWidth = true; }
                    if (chkDPIfixedHeight.Checked) { LaserSetting.bmpttrib += Laser.BMPATTRIB_IMPORTFIXED_HEIGHT; LaserSetting.blDPIfixedHeight = true; }
                    if (chkfixDPI.Checked) { LaserSetting.bmpttrib += Laser.BMPATTRIB_IMPORTFIXED_DPI; LaserSetting.blfixDPI = true; }

                    LaserConfigClass.Save(LaserSetting);
                    MessageBox.Show("ثبت شد.", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("اعداد وارد شده خارج از محدوده مباشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("اعداد وارد شده خارج از محدوده مباشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
