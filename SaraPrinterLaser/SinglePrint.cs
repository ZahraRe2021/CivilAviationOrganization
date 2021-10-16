using SaraPrinterLaser.bl;
using SaraPrinterLaser.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SaraPrinterLaser
{
    public partial class SinglePrint : Form
    {
        public SinglePrint()
        {
            InitializeComponent();
        }

        private void SinglePrint_Load(object sender, EventArgs e)
        {
            string TempFolder = Path.GetTempPath();

            string PictureTopPath = TempFolder + "LaserPrinterImagetmp\\PictureTop.bmp";
            string WithoutPictureBoxPictureTopPath = TempFolder + "LaserPrinterImagetmp\\WithoutPictureBoxPictureTop.bmp";
            string WithPictureBoxPictureTopPath = TempFolder + "LaserPrinterImagetmp\\WithPictureBoxPictureTop.bmp";

            string PictureBottomPath = TempFolder + "LaserPrinterImagetmp\\PictureBottom.bmp";
            string WithoutPictureBoxPictureBottom = TempFolder + "LaserPrinterImagetmp\\WithoutPictureBoxPictureBottom.bmp";
            string WithPictureBoxPictureBottom = TempFolder + "LaserPrinterImagetmp\\WithPictureBoxPictureBottom.bmp";


            this.Cursor = Cursors.WaitCursor;

            if (File.Exists(PictureTopPath))
                pictureBoxTop.ImageLocation = PictureTopPath;
            if (File.Exists(PictureBottomPath))
                pictureBoxBottom.ImageLocation = PictureBottomPath;

            FileWork.readstate();
            if (FileWork.stateMashin == FileWork.StateOfmashin.Ready || FileWork.stateMashin == FileWork.StateOfmashin.Printed) btnCleaning.Visible = false;
            else btnCleaning.Visible = true;


            if (!Config.RFIDState) { chbRFID.Visible = false; chbRFID.Checked = false; }

            if (!Config.RotateState)
            {
                chkRotate.Visible = false;
                chkRotate.Checked = false;
                //btnPenEditBottom.Enabled = false;
                //label1.Enabled = false;
                //cmbPenSelectBottom.Enabled = false;
                pictureBoxBottom.Image = null;
                //pictureBoxBottom.Enabled = false;
            }

            string[] MachineConfig = FileWork.ReadAllSecureConfig();
            if (MachineConfig[3] == "1")
            {
                rbxHolder1.Visible = false; rbxHolder1.IsChecked = false;
                rbxHolder2.Visible = false; rbxHolder2.IsChecked = false;
            }
            else if (MachineConfig[3] == "2")
            {
                rbxHolder1.Visible = true; rbxHolder1.IsChecked = true;
                rbxHolder2.Visible = true; rbxHolder2.IsChecked = true;
            }

            PenAddToCombo(cmbPenSelectTop);
            PenAddToCombo(cmbPenSelectBottom);

            PenAddToCombo(CmbPicturePenTop);
            PenAddToCombo(CmbPicturePenBottom);
            this.Cursor = Cursors.Arrow;
        }
        private void chkRotate_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnPenEditBottom_Click(object sender, EventArgs e)
        {
            PenManagement Pen = new PenManagement();
            Pen.ShowDialog();
        }

        private void btnPenEditTop_Click(object sender, EventArgs e)
        {
            PenManagement Pen = new PenManagement();
            Pen.ShowDialog();
        }

        private void cmbPenSelectTop_Click(object sender, EventArgs e)
        {
            PenAddToCombo(cmbPenSelectTop);
        }

        private void cmbPenSelectBottom_Click(object sender, EventArgs e)
        {
            PenAddToCombo(cmbPenSelectBottom);
        }

        private void PenAddToCombo(ComboBox CmbPerpose)
        {
            DirectoryInfo d = new DirectoryInfo(@"Pen");
            FileInfo[] Files = d.GetFiles("*.pen");
            CmbPerpose.Items.Clear();
            foreach (FileInfo file in Files)
                CmbPerpose.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
            CmbPerpose.SelectedItem = CmbPerpose.Items[0];
        }


        private void chkCr_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkCr.Checked)
            {
                cbEnableTrack1.Enabled = true;
                cbEnableTrack2.Enabled = true;
                cbEnableTrack3.Enabled = true;
                cbMoveToRejectBox.Enabled = true;
            }
            else
            {
                txtCR1.Enabled = false;
                txtCR2.Enabled = false;
                txtCR3.Enabled = false;
                cbEnableTrack1.Enabled = false;
                cbEnableTrack2.Enabled = false;
                cbEnableTrack3.Enabled = false;

                cbEnableTrack1.Checked = false;
                cbEnableTrack2.Checked = false;
                cbEnableTrack3.Checked = false;

                cbMoveToRejectBox.Enabled = false;
                cbMoveToRejectBox.Checked = false;
            }
        }

        private void label3_LocationChanged(object sender, EventArgs e)
        {
            label3.Location = new Point(215, 232);
        }

        private void label1_LocationChanged(object sender, EventArgs e)
        {
            label1.Location = new Point(215, 232);
        }

        private void btnSinglePrint_Click(object sender, EventArgs e)
        {
            try
            {
                btnSinglePrint.Enabled = false;
                FileWork.readstate();
                if (FileWork.stateMashin == FileWork.StateOfmashin.Ready || FileWork.stateMashin == FileWork.StateOfmashin.Printed)
                {

                    int holder = 1;
                    string[] MachineConfig = FileWork.ReadAllSecureConfig();
                    if (MachineConfig[3] == "1")
                    {
                        holder = 2;
                    }
                    else if (MachineConfig[3] == "2")
                    {
                        if (rbxHolder2.IsChecked)
                            holder = 2;
                    }


                    try
                    {
                        if (int.Parse(txtPrintNumber.Text) > 0)
                        {
                            bool TrackCheckFail = false;


                            if (!TrackCheckFail)
                            {


                                PrintingPage prt = new PrintingPage(chkCr.Checked, false, false, false, cbMoveToRejectBox.Checked, chkRotate.Checked, chbRFID.Checked, holder, txtCR1.Text, txtCR2.Text, txtCR3.Text, cmbPenSelectTop.SelectedItem.ToString(), CmbPicturePenTop.SelectedItem.ToString(), CmbPicturePenBottom.SelectedItem.ToString(), cmbPenSelectBottom.SelectedItem.ToString(), int.Parse(txtPrintNumber.Text));
                                prt.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("لطفا اطلاعات مغناطیس را به درستی وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                        else
                            MessageBox.Show("لطفا تعداد چاپ را تعیین نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        string aa = ex.ToString();

                        MessageBox.Show("لطفا تعداد چاپ را تعیین نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                    MessageBox.Show("پرینتر در حال پرینت می باشد.لطفاصبر کنید.", "اعلام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSinglePrint.Enabled = true;
            }
            catch (Exception)
            {


            }
        }

        private void btnCleaning_Click(object sender, EventArgs e)
        {
            Config.OpenAllPortExceptLaser();
        Retry:
            Config.StutDispenserSensorVariable.flgGetDispenserSensorStatus = false;
            Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusSucsess = false;
            Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusRetry = false;
            Config.SendDispenserSensorStatus();
            while (!Config.StutDispenserSensorVariable.flgGetDispenserSensorStatus) { }
            while (!Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusSucsess) { }
            if (Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusSucsess)
            {
                if (!Config.StutDispenserSensorVariable.flgDispenserSensor_EndWayCardDetector)
                {
                    FileWork.changeState(FileWork.StateOfmashin.Ready);
                    bl.FileWork.ClearAnswer();
                    //Worker.myjob.Status = job.StatusList.printed;
                    Config.StutDispenserSensorVariable.flgGetDispenserSensorStatus = false;
                    Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusSucsess = false;
                    Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusRetry = false;
                    MessageBox.Show("دستگاه با موفقیت پاکسازی شد و آماده چاپ می باشد", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCleaning.Visible = false;
                }
                else
                    MessageBox.Show("لطفا خط شخصی سازی را بررسی نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusRetry)
                goto Retry;
            Config.CloseAllPortExceptLaser();
        }

        private void txtPrintNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void radLabel1_Click(object sender, EventArgs e)
        {

        }

        private void txtCR3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCR2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCR1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbPenSelectBottom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CmbPicturePenBottom_Click(object sender, EventArgs e)
        {
            PenAddToCombo(CmbPicturePenBottom);
        }

        private void CmbPicturePenTop_Click(object sender, EventArgs e)
        {
            PenAddToCombo(CmbPicturePenTop);
        }

        private void CmbPicturePenTop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CbEnableTrack1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEnableTrack1.Checked)
                txtCR1.Enabled = true;
            else
                txtCR1.Enabled = false;
        }

        private void CbEnableTrack2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEnableTrack2.Checked)
                txtCR2.Enabled = true;
            else
                txtCR2.Enabled = false;
        }

        private void CbEnableTrack3_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEnableTrack3.Checked)
                txtCR3.Enabled = true;
            else
                txtCR3.Enabled = false;
        }

        private void CbMoveToRejectBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CmbPenSelectTop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
