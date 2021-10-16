using SaraPrinterLaser.bl;
using SaraPrinterLaser.dl;
using SaraPrinterLaser.Hardware;
using SaraPrinterLaser.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using EncryptionClass;

namespace SaraPrinterLaser
{
    public partial class DatabasePrint : Form
    {
        readonly bl.PrintList PrintAttrib = new PrintList();
        private static PixelFormat PxFormat = PixelFormat.Format8bppIndexed;
        private static Bitmap newImage = new Bitmap(2023, 1276, PxFormat);
        string BottomImageLocationPath = "";
        Panel TopPanel = null;
        Panel BottomPanel = null;
        private readonly bool flgNoPicExist;
        private int EntityName;
        #region Events
        public DatabasePrint(Panel Top, Panel Bottom)
        {
            InitializeComponent();
            TopPanel = Top;
            BottomPanel = Bottom;

            string TempFolder = Path.GetTempPath();
            System.IO.DirectoryInfo di2 = new DirectoryInfo(Path.GetTempPath() + "Base64Bitmap");
            foreach (FileInfo file in di2.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di2.GetDirectories())
            {
                dir.Delete(true);
            }

            System.IO.DirectoryInfo di = new DirectoryInfo(Path.GetTempPath() + "LaserPrinterImagetmp");
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            string PictureTopPath = TempFolder + "LaserPrinterImagetmp\\PictureTop.bmp";
            string WithoutPictureBoxPictureTopPath = TempFolder + "LaserPrinterImagetmp\\WithoutPictureBoxPictureTop.bmp";
            string WithPictureBoxPictureTopPath = TempFolder + "LaserPrinterImagetmp\\WithPictureBoxPictureTop.bmp";

            string PictureBottomPath = TempFolder + "LaserPrinterImagetmp\\PictureBottom.bmp";
            string WithoutPictureBoxPictureBottom = TempFolder + "LaserPrinterImagetmp\\WithoutPictureBoxPictureBottom.bmp";
            string WithPictureBoxPictureBottom = TempFolder + "LaserPrinterImagetmp\\WithPictureBoxPictureBottom.bmp";

            //Control[] controls = new Control[TopPanel.Controls.Count];
            //for (int i = 0; i < TopPanel.Controls.Count; i++)
            //{
            //    controls[i] = CopyControlNotPanel(TopPanel.Controls[i]);
            //}
            //    new GetImageFromPanel(controls, WithoutPictureBoxPictureTopPath).Show();


            //bmpTop[0].Save(WithoutPictureBoxPictureTopPath, ImageFormat.Bmp);
            //bmpTop[1].Save(WithPictureBoxPictureTopPath, ImageFormat.Bmp);
            //bmpTop[0].Dispose();
            //bmpTop[1].Dispose();


            //var bmpBottom = SplitPictureControl(BottomPanel);
            //ExportPicture(BottomPanel).Save(PictureBottomPath, ImageFormat.Bmp);
            //bmpBottom[0].Save(WithoutPictureBoxPictureBottom, ImageFormat.Bmp);
            //bmpBottom[1].Save(WithPictureBoxPictureBottom, ImageFormat.Bmp);
            //bmpBottom[0].Dispose();
            //bmpBottom[1].Dispose();



            SavePanel(TopPanel, PictureTopPath, WithoutPictureBoxPictureTopPath);
            SavePanel(BottomPanel, PictureBottomPath, WithoutPictureBoxPictureBottom);



            PicbRo.ImageLocation = PictureTopPath;
            PicbRo.SizeMode = PictureBoxSizeMode.StretchImage;


            picbZir.ImageLocation = PictureBottomPath;
            picbZir.SizeMode = PictureBoxSizeMode.StretchImage;

            BottomImageLocationPath = PictureBottomPath;



            if (!flgNoPicExist)
            {
                Laser.SaveEntLibToFile(Path.GetTempPath() + "LaserPrinterImagetmp\\PictureTop.ezd");
            }


        }

        private void SavePanel(Panel InputPanel, string CompeletePicture, string WithoutPicture)
        {

            ExportPicture(InputPanel).Save(CompeletePicture, ImageFormat.Bmp);
            Control[] tmpControl = new Control[0];
            int arrayCnt = 0;
            // var bmpTop = SplitPictureControl(TopPanel);

            foreach (Control item in InputPanel.Controls)
            {
                if (item.GetType() == typeof(PictureBox))
                {
                    arrayCnt++;
                    Array.Resize<Control>(ref tmpControl, arrayCnt);
                    tmpControl[arrayCnt - 1] = item;
                    InputPanel.Controls.Remove(item);

                }
            }
            ExportPicture(InputPanel).Save(WithoutPicture, ImageFormat.Bmp);

            foreach (Control item in tmpControl)
            {
                InputPanel.Controls.Add(item);
            }

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
        private void DatabasePrint_Load(object sender, EventArgs e)
        {
            Settings tanzim = new Settings();
            dl.DataAccessClass.ConnectionString = tanzim.ConnectionString;
            PrintAttrib.Selection = new Dictionary<string, string>();

            picbZir.Enabled = false;
            cmbPenZir.Enabled = false;
            btnPenEditBottom.Enabled = false;
            //picbZir.Image = null;
            FileWork.readstate();
            if (FileWork.stateMashin == FileWork.StateOfmashin.Ready || FileWork.stateMashin == FileWork.StateOfmashin.Printed) btnCleaning.Visible = false;
            else btnCleaning.Visible = true;

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
            if (!Hardware.Config.RFIDState) chbRFID.Visible = false;
            cbxDataSource.Items.Clear();
            DataSet dset = new dl.InfoModel().ListOfName();
            cbxDataSource.DataSource = dset.Tables[0];
            cbxDataSource.ValueMember = "ID";
            cbxDataSource.DisplayMember = "InfoName";
            cbxDataSource.SelectedIndex = -1;
            if (cbxDataSource.Items.Count > 0)
                cbxDataSource.SelectedItem = cbxDataSource.Items[0];
            cbxDataList.Items.Clear();
            cbxDataList.Enabled = false;
            chkCr.Enabled = false;
            chkCr.Checked = false;

            cbxTrack1.Enabled = false;
            cbxTrack2.Enabled = false;
            cbxTrack3.Enabled = false;
            cbEnableTrack1.Checked = false;
            cbEnableTrack2.Checked = false;
            cbEnableTrack3.Checked = false;
            cbxFieldList.Items.Clear();
            flpDataMacth.Controls.Clear();
            if (Config.RotateState)
            {
                chkRotate.Checked = true;
                chkRotate.Visible = true;
                //  picbZir.Image = null;
                picbZir.Enabled = true;
                cmbPenZir.Enabled = true;
                cmbPictureBottomPen.Enabled = true;
                btnPenEditBottom.Enabled = true;
                label8.Enabled = true;
            }
            string[] AvailableObject = TopReadData(MakeXML(TopPanel).OuterXml, SideOfCard.TopCard);
            foreach (var item in AvailableObject)
                cbxFieldList.Items.Add(item);

            if (picbZir.Enabled == true)
            {
                AvailableObject = null;
                AvailableObject = TopReadData(MakeXML(BottomPanel).OuterXml, SideOfCard.BottomCard);
                foreach (var item in AvailableObject)
                    cbxFieldList.Items.Add(item);

            }

            //cbxLayoutZirNew.SelectedIndex = -1;
            DirectoryInfo d = new DirectoryInfo(@"Pen");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.pen"); //Getting Text files
            cmbPenRo.Items.Clear();
            cmbPenZir.Items.Clear();
            foreach (FileInfo file in Files)
            {
                cmbPenRo.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
                cmbPenZir.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
                cmbPictureTOPPen.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
                cmbPictureBottomPen.Items.Add(Path.GetFileNameWithoutExtension(file.Name));

            }
            cmbPenRo.SelectedItem = cmbPenRo.Items[0];
            cmbPenZir.SelectedItem = cmbPenZir.Items[0];
            cmbPictureTOPPen.SelectedItem = cmbPictureTOPPen.Items[0];
            cmbPictureBottomPen.SelectedItem = cmbPictureBottomPen.Items[0];

        }
        private void cbxDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPrint.Enabled = true;
            cbxDataList.Enabled = true;
            chkCr.Enabled = true;

            if (!chbCounterField.Checked)
            {
                cbxDataList.Items.Clear();
                cbxTrack1.Items.Clear();
                cbxTrack2.Items.Clear();
                cbxTrack3.Items.Clear();

                cbxDataList.SelectedIndex = -1;
                cbxTrack1.SelectedIndex = -1;
                cbxTrack2.SelectedIndex = -1;
                cbxTrack3.SelectedIndex = -1;

                int aaa;
                if (cbxDataSource.SelectedIndex != -1 && int.TryParse(cbxDataSource.SelectedValue.ToString(), out aaa))
                {
                    string FieldList = new dl.InfoModel().InfoDataByID(Convert.ToInt32(cbxDataSource.SelectedValue));
                    txtPictureFolderPath.Text = new dl.InfoModel().InfoPictureFolderPathByID(Convert.ToInt32(cbxDataSource.SelectedValue));
                    txtPictureFormat.Text = new dl.InfoModel().InfoPictureFormatByID(Convert.ToInt32(cbxDataSource.SelectedValue));
                    string[] fields = FieldList.Split('ß');
                    foreach (var item in fields)
                        cbxDataList.Items.Add(item);
                }
                if (cbxDataList.Items.Count > 0 && cbxDataList.Enabled == true)
                    cbxDataList.SelectedItem = cbxDataList.Items[0];
            }
            ActiveChCR();
        }
        public static Bitmap ExportPicture(Panel ctl)
        {
            newImage = GetControlImage(ctl);
            Bitmap scale = GrayScale(newImage);
            scale = scale.Clone(new Rectangle(0, 0, ctl.Width, ctl.Height), PxFormat);
            scale.SetResolution(200, 200);
            newImage.Dispose();
            return scale;

        }
        private void cmbPenRo_Click(object sender, EventArgs e)
        {
            cmbPenRo.Items.Clear();
            DirectoryInfo d = new DirectoryInfo(@"Pen");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.pen"); //Getting Text files

            foreach (FileInfo file in Files)
                cmbPenRo.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
            cmbPenRo.SelectedItem = cmbPenRo.Items[0];
        }
        private void cmbPenZir_Click(object sender, EventArgs e)
        {
            cmbPenZir.Items.Clear();
            DirectoryInfo d = new DirectoryInfo(@"Pen");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.pen"); //Getting Text files
            foreach (FileInfo file in Files)
                cmbPenZir.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
            cmbPenZir.SelectedItem = cmbPenZir.Items[0];
        }
        private void radButton2_Click(object sender, EventArgs e)
        {
            PenManagement Pen = new PenManagement();
            Pen.ShowDialog();
        }
        private void cbxDataSource_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dset = new dl.InfoModel().ListOfName();
                cbxDataSource.DataSource = dset.Tables[0];
                cbxDataSource.ValueMember = "ID";
                cbxDataSource.DisplayMember = "InfoName";
                cbxDataSource.SelectedItem = cbxDataSource.Items[0];
            }
            catch (Exception)
            {


            }

        }
        private void btnAddFieldData_Click(object sender, EventArgs e)
        {
            bool RepetedName = false;
            try
            {
                if (cbxDataList.SelectedItem.ToString().Trim().Contains("شمارنده"))
                {
                    if (cbxFieldList.SelectedItem.ToString().Contains("عکس") || cbxFieldList.SelectedItem.ToString().Contains("بارکد"))
                        MessageBox.Show("بر روی بارکد و عکس نمیتوان شمارنده چاپ کرد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        if (cbxDataList.SelectedItem.ToString().Length > 0 && cbxFieldList.SelectedItem.ToString().Length > 0)
                        {
                            try
                            {
                                PrintAttrib.Selection.Add(cbxFieldList.SelectedItem.ToString(), cbxDataList.SelectedItem.ToString());
                            }
                            catch (Exception)
                            {

                                MessageBox.Show("آیتم با نام تکراری وجود دارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                RepetedName = true;
                            }

                            if (!RepetedName)
                            {
                                Button btnnew = new Button();
                                btnnew.Text = string.Format("{0} --> {1}", cbxDataList.SelectedItem.ToString(), cbxFieldList.SelectedItem.ToString());
                                btnnew.BackColor = Color.Green;
                                btnnew.ForeColor = Color.Gold;
                                //btnnew.AutoSize = true; 
                                btnnew.Width = flpDataMacth.Width - 20;
                                btnnew.Click += Btnnew_Click;
                                btnnew.Height = 40;
                                //btnnew.BackColor = Color.Yellow;
                                flpDataMacth.Controls.Add(btnnew);
                                cbxFieldList.Items.Remove(cbxFieldList.SelectedItem);
                                if (cbxFieldList.Items.Count > 0)
                                    cbxFieldList.SelectedItem = cbxFieldList.Items[0];
                            }
                        }
                    }
                }
                else
                {
                    if (cbxDataList.SelectedItem.ToString().Length > 0 && cbxFieldList.SelectedItem.ToString().Length > 0)
                    {
                        try
                        {
                            PrintAttrib.Selection.Add(cbxFieldList.SelectedItem.ToString(), cbxDataList.SelectedItem.ToString());
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("آیتم با نام تکراری وجود دارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            RepetedName = true;
                        }

                        if (!RepetedName)
                        {
                            Button btnnew = new Button();
                            btnnew.Text = string.Format("{0} --> {1}", cbxDataList.SelectedItem.ToString(), cbxFieldList.SelectedItem.ToString());
                            btnnew.BackColor = Color.Green;
                            btnnew.ForeColor = Color.Gold;
                            //btnnew.AutoSize = true; 
                            btnnew.Width = flpDataMacth.Width - 20;
                            btnnew.Click += Btnnew_Click;
                            btnnew.Height = 40;
                            //btnnew.BackColor = Color.Yellow;
                            flpDataMacth.Controls.Add(btnnew);
                            cbxFieldList.Items.Remove(cbxFieldList.SelectedItem);
                            if (cbxFieldList.Items.Count > 0)
                                cbxFieldList.SelectedItem = cbxFieldList.Items[0];
                        }
                    }
                }

            }
            catch (Exception)
            {

                MessageBox.Show("لطفا آیتم های مورد نظر را بدرستی انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Btnnew_Click(object sender, EventArgs e)
        {
            bool trueRemove = false;
            int counter = 0;
            if (MessageBox.Show("آیا مطمئن هستید این ارتباط حذف شود؟", "اختیار", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                flpDataMacth.Controls.Remove(((sender) as Button));
                string[] strlist = Regex.Split(((sender) as Button).Text, "-->");
                strlist[1] = strlist[1].Trim();
                do
                {
                    trueRemove = PrintAttrib.Selection.Remove(strlist[1]);
                    counter++;
                    Thread.Sleep(1);
                } while (!trueRemove && counter < 2000);
                if (trueRemove)
                    cbxFieldList.Items.Add(strlist[1]);
                else
                    MessageBox.Show("آیتم به درستی حذف نشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void chbCounterField_CheckedChanged(object sender, EventArgs e)
        {

            cbxDataList.Items.Remove("شمارنده");

            if (chbCounterField.Checked)
            {
                chkCr.Enabled = false;
                chkCr.Checked = false;
                cbEnableTrack1.Enabled = false;
                cbEnableTrack2.Enabled = false;
                cbEnableTrack3.Enabled = false;
                cbEnableTrack1.Checked = false;
                cbEnableTrack2.Checked = false;
                cbEnableTrack3.Checked = false;
                cbMoveToRejectBox.Enabled = false;
                cbMoveToRejectBox.Checked = false;
                //  cbxDataSource.Enabled = false;
                try
                {
                    cbxDataList.Items.Clear();

                }
                catch (Exception) { }

                cbxDataList.Items.Add("شمارنده");
                cbxDataList.SelectedItem = cbxDataList.Items[0];
                //if (chkCr.Checked)
                //{
                //    cbxTrack1.Items.Add("شمارنده");
                //    cbxTrack2.Items.Add("شمارنده");
                //    cbxTrack3.Items.Add("شمارنده");

                //    string FieldList = new dl.InfoModel().InfoDataByID(Convert.ToInt32(cbxDataSource.SelectedValue));
                //    string[] fields = FieldList.Split('ß');
                //    foreach (var item in fields)
                //    {
                //        cbxTrack1.Items.Add(item);
                //        cbxTrack2.Items.Add(item);
                //        cbxTrack3.Items.Add(item);
                //    }

                //    cbxTrack1.SelectedItem = cbxTrack1.Items[0];
                //    cbxTrack2.SelectedItem = cbxTrack2.Items[0];
                //    cbxTrack3.SelectedItem = cbxTrack3.Items[0];
                //}

                txtCounterFix.Enabled = true;
                txtCounterMax.Enabled = true;
                txtCounterMin.Enabled = true;
                cbxDataList.Enabled = true;

            }
            else
            {
                chkCr.Enabled = true;

                ////   cbxDataSource.Enabled = true;
                //if (chkCr.Checked)
                //{

                //    cbxTrack1.Enabled = true;
                //    cbxTrack2.Enabled = true;
                //    cbxTrack3.Enabled = true;
                //    try
                //    {
                //        cbxTrack1.Items.Clear();
                //        cbxTrack2.Items.Clear();
                //        cbxTrack3.Items.Clear();
                //    }
                //    catch (Exception) { }
                //    string FieldList = new dl.InfoModel().InfoDataByID(Convert.ToInt32(cbxDataSource.SelectedValue));
                //    string[] fields = FieldList.Split('ß');
                //    foreach (var item in fields)
                //    {
                //        cbxTrack1.Items.Add(item);
                //        cbxTrack2.Items.Add(item);
                //        cbxTrack3.Items.Add(item);
                //    }
                //    cbxTrack1.SelectedItem = cbxTrack1.Items[0];
                //    cbxTrack2.SelectedItem = cbxTrack2.Items[0];
                //    cbxTrack3.SelectedItem = cbxTrack3.Items[0];
                //}
                //else
                //{
                //    try
                //    {
                //        cbxTrack1.Enabled = false;
                //        cbxTrack2.Enabled = false;
                //        cbxTrack3.Enabled = false;
                //        cbxTrack1.Items.Clear();
                //        cbxTrack2.Items.Clear();
                //        cbxTrack3.Items.Clear();
                //    }
                //    catch (Exception) { }
                //}
                int aaa;
                if (cbxDataSource.SelectedIndex != -1 && int.TryParse(cbxDataSource.SelectedValue.ToString(), out aaa))
                {
                    string FieldList = new dl.InfoModel().InfoDataByID(Convert.ToInt32(cbxDataSource.SelectedValue));
                    string[] fields = FieldList.Split('ß');
                    foreach (var item in fields)
                        cbxDataList.Items.Add(item);
                }
                cbxDataList.SelectedItem = cbxDataList.Items[0];
                txtCounterFix.Enabled = false;
                txtCounterMax.Enabled = false;
                txtCounterMin.Enabled = false;
            }
        }
        private void chkRotate_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkRotate.Checked)
            {
                picbZir.Enabled = true;
                cmbPenZir.Enabled = true;
                cmbPictureBottomPen.Enabled = true;
                btnPenEditBottom.Enabled = true;
                picbZir.ImageLocation = BottomImageLocationPath;

            }
            else
            {
                picbZir.Enabled = false;
                cmbPenZir.Enabled = false;
                btnPenEditBottom.Enabled = false;
                cmbPictureBottomPen.Enabled = false;
                picbZir.Image = null;


            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {

            btnPrint.Enabled = false;
            FileWork.readstate();
            if (FileWork.stateMashin == FileWork.StateOfmashin.Ready || FileWork.stateMashin == FileWork.StateOfmashin.Printed)
            {
                PrintAttrib.FixIncrimentActive = chbCounterField.Checked;
                PrintAttrib.FixIncriment = txtCounterFix.Text;
                PrintAttrib.startIncriment = Convert.ToInt32(txtCounterMin.Text);
                PrintAttrib.EndIncriment = Convert.ToInt32(txtCounterMax.Text);
                PrintAttrib.DataPrintID = Convert.ToInt32(cbxDataSource.SelectedValue);
                if (cbxDataSource.SelectedIndex != -1)
                {
                    PrintAttrib.SelectData = true;

                }

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
                string Cr1 = "", Cr2 = "", Cr3 = "";

                if (cbxTrack1.SelectedIndex != -1) Cr1 = cbxTrack1.SelectedItem.ToString();
                if (cbxTrack2.SelectedIndex != -1) Cr2 = cbxTrack2.SelectedItem.ToString();
                if (cbxTrack3.SelectedIndex != -1) Cr3 = cbxTrack3.SelectedItem.ToString();
                if (!String.IsNullOrWhiteSpace(txtPictureFolderPath.Text))
                {
                    PrintAttrib.PictureFolderPath = txtPictureFolderPath.Text;
                    PrintAttrib.PictureFormat = txtPictureFormat.Text;
                }

                bool Track1 = false, Track2 = false, Track3 = false, TrackCheckFail = false;

                if (chkCr.Checked)
                {
                    if (cbEnableTrack1.Checked)
                    {
                        if (cbxTrack1.SelectedIndex == -1)
                            TrackCheckFail = true;
                        else
                            Track1 = true;
                    }
                    if (cbEnableTrack2.Checked)
                    {
                        if (cbxTrack2.SelectedIndex == -1)
                            TrackCheckFail = true;
                        else
                            Track2 = true;
                    }
                    if (cbEnableTrack3.Checked)
                    {
                        if (cbxTrack3.SelectedIndex == -1)
                            TrackCheckFail = true;
                        else
                            Track3 = true;
                    }
                }

                if (!TrackCheckFail)
                {
                    PrintingPageSeries prt = new PrintingPageSeries(TopPanel, BottomPanel, chkCr.Checked,
                                                                chkRotate.Checked,
                                                                Track1, Track2, Track3, cbMoveToRejectBox.Checked,
                                                                chbRFID.Checked,
                                                                holder,
                                                                Cr1, Cr2, Cr3,
                                                                cmbPenRo.SelectedItem.ToString(),
                                                                cmbPenZir.SelectedItem.ToString(),
                                                                cmbPictureTOPPen.SelectedItem.ToString(),
                                                                cmbPictureBottomPen.SelectedItem.ToString(),
                                                                PrintAttrib);

                    prt.ShowDialog();
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    MessageBox.Show("لطفا اطلاعات مغناطیس را به درستی وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
                MessageBox.Show("پرینتر در حال پرینت می باشد.لطفا کنید.", "اعلام", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnPrint.Enabled = true;
        }
        private void chkCr_CheckedChanged(object sender, EventArgs e)
        {
            ActiveChCR();
        }
        private void BtnEnterDatabase_Click(object sender, EventArgs e)
        {
            EnterData frm = new EnterData();
            frm.ShowDialog();
        }
        private void cbxTrack1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PrintAttrib.Selection.ContainsKey("CR1"))
            {
                PrintAttrib.Selection.Remove("CR1");
            }
            if (cbxTrack1.SelectedIndex != -1)
            {
                PrintAttrib.Selection.Add("CR1", cbxTrack1.SelectedItem.ToString());

            }
        }
        private void cbxTrack2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PrintAttrib.Selection.ContainsKey("CR2"))
            {
                PrintAttrib.Selection.Remove("CR2");
            }
            if (cbxTrack2.SelectedIndex != -1)
            {
                PrintAttrib.Selection.Add("CR2", cbxTrack2.SelectedItem.ToString());

            }
        }
        private void cbxTrack3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PrintAttrib.Selection.ContainsKey("CR3"))
            {
                PrintAttrib.Selection.Remove("CR3");
            }
            if (cbxTrack3.SelectedIndex != -1)
            {
                PrintAttrib.Selection.Add("CR3", cbxTrack3.SelectedItem.ToString());

            }
        }
        #endregion
        #region Functions
        private void ActiveChCR()
        {

            if (chbCounterField.Checked)
            {
                chkCr.Checked = false;
                chkCr.Enabled = false;
                cbxTrack1.Items.Clear();
                cbxTrack2.Items.Clear();
                cbxTrack3.Items.Clear();
                cbxTrack1.SelectedIndex = -1;
                cbxTrack2.SelectedIndex = -1;
                cbxTrack3.SelectedIndex = -1;
                cbxTrack1.Text = "";
                cbxTrack2.Text = "";
                cbxTrack3.Text = "";
                cbxTrack1.Enabled = false;
                cbxTrack1.Enabled = false;
                cbxTrack1.Enabled = false;
                cbEnableTrack1.Checked = false;
                cbEnableTrack2.Checked = false;
                cbEnableTrack3.Checked = false;
                cbMoveToRejectBox.Checked = false;
                cbMoveToRejectBox.Enabled = false;
            }
            else
            {

                if (chkCr.Checked)
                {
                    string FieldList = new dl.InfoModel().InfoDataByID(Convert.ToInt32(cbxDataSource.SelectedValue));
                    string[] fields = FieldList.Split('ß');
                    cbEnableTrack1.Enabled = true;
                    cbEnableTrack2.Enabled = true;
                    cbEnableTrack3.Enabled = true;
                    cbMoveToRejectBox.Enabled = true;

                    if (cbEnableTrack1.Checked)
                    {
                        cbxTrack1.Enabled = true;
                        foreach (var item in fields)
                        {
                            cbxTrack1.Items.Add(item);
                        }
                        if (cbxTrack1.Items.Count > 0 && cbxTrack1.Enabled && cbEnableTrack1.Enabled) cbxTrack1.SelectedItem = cbxTrack1.Items[0];
                    }
                    else
                    {
                        cbxTrack1.Enabled = false;
                        cbxTrack1.Items.Clear();
                    }


                    if (cbEnableTrack2.Checked)
                    {
                        cbxTrack2.Enabled = true;
                        foreach (var item in fields)
                        {
                            cbxTrack2.Items.Add(item);
                        }
                        if (cbxTrack2.Items.Count > 0 && cbxTrack2.Enabled && cbEnableTrack2.Enabled) cbxTrack2.SelectedItem = cbxTrack2.Items[0];
                    }
                    else
                    {
                        cbxTrack2.Enabled = false;
                        cbxTrack2.Items.Clear();
                    }

                    if (cbEnableTrack3.Checked)
                    {
                        cbxTrack3.Enabled = true;
                        foreach (var item in fields)
                        {
                            cbxTrack3.Items.Add(item);
                        }
                        if (cbxTrack3.Items.Count > 0 && cbxTrack3.Enabled && cbEnableTrack3.Enabled) cbxTrack3.SelectedItem = cbxTrack3.Items[0];
                    }
                    else
                    {
                        cbxTrack3.Enabled = false;
                        cbxTrack3.Items.Clear();
                    }





                }
                else
                {
                    try
                    {

                        cbxTrack1.Items.Clear();
                        cbxTrack2.Items.Clear();
                        cbxTrack3.Items.Clear();

                        cbxTrack1.SelectedIndex = -1;
                        cbxTrack2.SelectedIndex = -1;
                        cbxTrack3.SelectedIndex = -1;
                        cbxTrack1.Text = "";
                        cbxTrack2.Text = "";
                        cbxTrack3.Text = "";
                        cbxTrack1.Enabled = false;
                        cbxTrack2.Enabled = false;
                        cbxTrack3.Enabled = false;
                        cbEnableTrack1.Enabled = false;
                        cbEnableTrack2.Enabled = false;
                        cbEnableTrack3.Enabled = false;
                        cbMoveToRejectBox.Enabled = false;
                        cbMoveToRejectBox.Checked = false;
                    }
                    catch (Exception) { }
                }
            }
            //if (chkCr.Checked)
            //{
            //    if (chbCounterField.Checked)
            //    {
            //        cbxTrack1.Enabled = true;
            //        cbxTrack2.Enabled = true;
            //        cbxTrack3.Enabled = true;
            //        try
            //        {
            //            cbxTrack1.Items.Clear();
            //            cbxTrack2.Items.Clear();
            //            cbxTrack3.Items.Clear();
            //        }
            //        catch (Exception) { }
            //        cbxTrack1.Items.Add("شمارنده");
            //        cbxTrack2.Items.Add("شمارنده");
            //        cbxTrack3.Items.Add("شمارنده");

            //        string FieldList = new dl.InfoModel().InfoDataByID(Convert.ToInt32(cbxDataSource.SelectedValue));
            //        string[] fields = FieldList.Split('ß');
            //        foreach (var item in fields)
            //        {
            //            cbxTrack1.Items.Add(item);
            //            cbxTrack2.Items.Add(item);
            //            cbxTrack3.Items.Add(item);
            //        }

            //        cbxTrack1.SelectedItem = cbxTrack1.Items[0];
            //        cbxTrack2.SelectedItem = cbxTrack2.Items[0];
            //        cbxTrack3.SelectedItem = cbxTrack3.Items[0];
            //    }
            //    else
            //    {
            //        cbxTrack1.Enabled = true;
            //        cbxTrack2.Enabled = true;
            //        cbxTrack3.Enabled = true;
            //        try
            //        {
            //            cbxTrack1.Items.Clear();
            //            cbxTrack2.Items.Clear();
            //            cbxTrack3.Items.Clear();
            //        }
            //        catch (Exception) { }
            //        string FieldList = new dl.InfoModel().InfoDataByID(Convert.ToInt32(cbxDataSource.SelectedValue));
            //        string[] fields = FieldList.Split('ß');
            //        foreach (var item in fields)
            //        {
            //            cbxTrack1.Items.Add(item);
            //            cbxTrack2.Items.Add(item);
            //            cbxTrack3.Items.Add(item);
            //        }
            //    }
            //    if (cbxTrack1.Items.Count > 0 && cbxTrack1.Enabled) cbxTrack1.SelectedItem = cbxTrack1.Items[0];
            //    if (cbxTrack2.Items.Count > 0 && cbxTrack2.Enabled) cbxTrack2.SelectedItem = cbxTrack2.Items[0];
            //    if (cbxTrack3.Items.Count > 0 && cbxTrack3.Enabled) cbxTrack3.SelectedItem = cbxTrack3.Items[0];
            //}
            //else
            //{
            //    try
            //    {

            //        cbxTrack1.Items.Clear();
            //        cbxTrack2.Items.Clear();
            //        cbxTrack3.Items.Clear();

            //        cbxTrack1.SelectedIndex = -1;
            //        cbxTrack2.SelectedIndex = -1;
            //        cbxTrack3.SelectedIndex = -1;
            //        cbxTrack1.Text = "";
            //        cbxTrack2.Text = "";
            //        cbxTrack3.Text = "";
            //        cbxTrack1.Enabled = false;
            //        cbxTrack2.Enabled = false;
            //        cbxTrack3.Enabled = false;
            //    }
            //    catch (Exception) { }
            //}
        }
        public enum SideOfCard
        {
            TopCard, BottomCard
        }
        private string[] TopReadData(string XMLSelectedValue, SideOfCard CardSide)
        {
            string[] DataRead = new string[0] { };
            string[] DataName = new string[0] { };


            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(XMLSelectedValue);
            XmlNode xnList = xDoc.SelectSingleNode("SaraHardwareCompanyLaserPrinter");
            foreach (XmlNode xn in xnList)
            {
                string CntrlType = xn["Type"].InnerText;
                string CntrlsName = xn["Name"].InnerText;
                string CntrlsText = xn["Text"].InnerText;

                if (CntrlType == "System.Windows.Forms.PictureBox")
                {
                    Array.Resize<string>(ref DataRead, DataRead.Length + 1);
                    if (CardSide == SideOfCard.TopCard) DataRead[DataRead.Length - 1] = CntrlsName + "=" + "_(عکس_روی کارت)_";
                    else DataRead[DataRead.Length - 1] = CntrlsName + "=" + "_(عکس_زیر کارت)_";
                }
                else if (CntrlsName.Contains("BC_") && CntrlType == "DevExpress.XtraEditors.BarCodeControl")
                {
                    Array.Resize<string>(ref DataRead, DataRead.Length + 1);
                    if (CardSide == SideOfCard.TopCard) DataRead[DataRead.Length - 1] = CntrlsText + "=" + CntrlsName + "=" + "_(بارکد_روی کارت)_";
                    else DataRead[DataRead.Length - 1] = CntrlsText + "=" + CntrlsName + "=" + "_(بارکد_زیر کارت)_";
                }
                else if (CntrlsName.Contains("Lbl_") && CntrlType == "System.Windows.Forms.Label")
                {
                    Array.Resize<string>(ref DataRead, DataRead.Length + 1);
                    if (CardSide == SideOfCard.TopCard) DataRead[DataRead.Length - 1] = CntrlsText + "=" + CntrlsName + "=" + "_(متن_روی کارت)_";
                    else DataRead[DataRead.Length - 1] = CntrlsText + "=" + CntrlsName + "=" + "_(متن_زیر کارت)_";
                }
                else if (CntrlsName.Contains("Otxt_") && CntrlType == "CustomControl.OrientAbleTextControls.OrientedTextLabel")
                {
                    Array.Resize<string>(ref DataRead, DataRead.Length + 1);
                    if (CardSide == SideOfCard.TopCard) DataRead[DataRead.Length - 1] = CntrlsText + "=" + CntrlsName + "=" + "_(متن چرخشی_روی کارت)_";
                    else DataRead[DataRead.Length - 1] = CntrlsText + "=" + CntrlsName + "=" + "_(متن چرخشی_زیر کارت)_";
                }
            }
            //  if (CardSide == SideOfCard.TopCard) xDoc.Save("Temp\\XmlPrintRo.xml");
            // else xDoc.Save("Temp\\XmlPrintZir.xml");
            return DataRead;
        }
        #endregion


        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Stop();
        }

        private void cbxLayoutRo_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void btnPenEditTop_Click(object sender, EventArgs e)
        {
            PenManagement frm = new PenManagement();
            frm.ShowDialog();
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
        public static Control CopyControlNotPanel(Control Ctrl)
        {
            Control CopiedCtrlPanel = new Control();

            try
            {
                switch (Ctrl.GetType().ToString())
                {
                    case "System.Windows.Forms.PictureBox":
                        {
                            try
                            {
                                PictureBox PasteControlPictureBox = new PictureBox();
                                PictureBox CopiedControlPictureBox = Ctrl as PictureBox;
                                PasteControlPictureBox.Name = CopiedControlPictureBox.Name;
                                PasteControlPictureBox.Text = CopiedControlPictureBox.Text;
                                PasteControlPictureBox.Font = CopiedControlPictureBox.Font;
                                PasteControlPictureBox.Location = CopiedControlPictureBox.Location;
                                PasteControlPictureBox.BackgroundImageLayout = CopiedControlPictureBox.BackgroundImageLayout;
                                PasteControlPictureBox.Size = CopiedControlPictureBox.Size;
                                PasteControlPictureBox.BackColor = CopiedControlPictureBox.BackColor;
                                PasteControlPictureBox.ForeColor = CopiedControlPictureBox.ForeColor;
                                PasteControlPictureBox.RightToLeft = CopiedControlPictureBox.RightToLeft;
                                PasteControlPictureBox.Cursor = CopiedControlPictureBox.Cursor;
                                PasteControlPictureBox.AllowDrop = CopiedControlPictureBox.AllowDrop;
                                PasteControlPictureBox.Enabled = CopiedControlPictureBox.Enabled;
                                PasteControlPictureBox.TabIndex = CopiedControlPictureBox.TabIndex;
                                PasteControlPictureBox.Visible = CopiedControlPictureBox.Visible;
                                PasteControlPictureBox.CausesValidation = CopiedControlPictureBox.CausesValidation;
                                PasteControlPictureBox.Anchor = CopiedControlPictureBox.Anchor;
                                PasteControlPictureBox.Dock = CopiedControlPictureBox.Dock;
                                PasteControlPictureBox.Margin = CopiedControlPictureBox.Margin;
                                PasteControlPictureBox.Padding = CopiedControlPictureBox.Padding;
                                PasteControlPictureBox.MaximumSize = CopiedControlPictureBox.MaximumSize;
                                PasteControlPictureBox.MinimumSize = CopiedControlPictureBox.MinimumSize;
                                PasteControlPictureBox.UseWaitCursor = CopiedControlPictureBox.UseWaitCursor;
                                PasteControlPictureBox.BorderStyle = CopiedControlPictureBox.BorderStyle;
                                PasteControlPictureBox.WaitOnLoad = CopiedControlPictureBox.WaitOnLoad;
                                PasteControlPictureBox.SizeMode = CopiedControlPictureBox.SizeMode;
                                PasteControlPictureBox.ImageLocation = CopiedControlPictureBox.ImageLocation;
                                PasteControlPictureBox.Image = CopiedControlPictureBox.Image;
                                return PasteControlPictureBox;
                            }
                            catch (Exception)
                            {
                            }
                        }
                        break;

                    case "System.Windows.Forms.Label": //Label
                        {

                            Label PasteControlLabel = new Label();
                            Label CopiedLabel = Ctrl as Label;
                            PasteControlLabel.Name = CopiedLabel.Name;
                            PasteControlLabel.Text = CopiedLabel.Text;
                            PasteControlLabel.Font = CopiedLabel.Font;

                            PasteControlLabel.Location = CopiedLabel.Location;
                            PasteControlLabel.Size = CopiedLabel.Size;
                            PasteControlLabel.BackColor = CopiedLabel.BackColor;
                            PasteControlLabel.ForeColor = CopiedLabel.ForeColor;
                            PasteControlLabel.RightToLeft = CopiedLabel.RightToLeft;
                            PasteControlLabel.Cursor = CopiedLabel.Cursor;
                            PasteControlLabel.AllowDrop = CopiedLabel.AllowDrop;
                            PasteControlLabel.Enabled = CopiedLabel.Enabled;
                            PasteControlLabel.TabIndex = CopiedLabel.TabIndex;
                            PasteControlLabel.Visible = CopiedLabel.Visible;
                            PasteControlLabel.CausesValidation = CopiedLabel.CausesValidation;
                            PasteControlLabel.Anchor = CopiedLabel.Anchor;
                            PasteControlLabel.Dock = CopiedLabel.Dock;
                            PasteControlLabel.Margin = CopiedLabel.Margin;
                            PasteControlLabel.Padding = CopiedLabel.Padding;
                            PasteControlLabel.MaximumSize = CopiedLabel.MaximumSize;
                            PasteControlLabel.MinimumSize = CopiedLabel.MinimumSize;
                            PasteControlLabel.UseWaitCursor = CopiedLabel.UseWaitCursor;

                            PasteControlLabel.BorderStyle = CopiedLabel.BorderStyle;
                            PasteControlLabel.FlatStyle = CopiedLabel.FlatStyle;
                            PasteControlLabel.Image = CopiedLabel.Image;
                            PasteControlLabel.ImageAlign = CopiedLabel.ImageAlign;
                            PasteControlLabel.ImageIndex = CopiedLabel.ImageIndex;
                            PasteControlLabel.ImageKey = CopiedLabel.ImageKey;
                            PasteControlLabel.TextAlign = CopiedLabel.TextAlign;
                            PasteControlLabel.UseMnemonic = CopiedLabel.UseMnemonic;
                            PasteControlLabel.AutoEllipsis = CopiedLabel.AutoEllipsis;
                            PasteControlLabel.UseCompatibleTextRendering = CopiedLabel.UseCompatibleTextRendering;
                            PasteControlLabel.AutoSize = CopiedLabel.AutoSize;
                            return PasteControlLabel;


                        }
                        break;

                    case "CustomControl.OrientAbleTextControls.OrientedTextLabel":
                        {
                            CustomControl.OrientAbleTextControls.OrientedTextLabel PasteControlOrientedTextLabel = new CustomControl.OrientAbleTextControls.OrientedTextLabel();
                            CustomControl.OrientAbleTextControls.OrientedTextLabel CopiedControlOrientedTextLabel = Ctrl as CustomControl.OrientAbleTextControls.OrientedTextLabel;
                            PasteControlOrientedTextLabel.Name = CopiedControlOrientedTextLabel.Name;
                            PasteControlOrientedTextLabel.Text = CopiedControlOrientedTextLabel.Text;
                            PasteControlOrientedTextLabel.Font = CopiedControlOrientedTextLabel.Font;
                            PasteControlOrientedTextLabel.Location = CopiedControlOrientedTextLabel.Location;
                            PasteControlOrientedTextLabel.Size = CopiedControlOrientedTextLabel.Size;
                            PasteControlOrientedTextLabel.BackColor = CopiedControlOrientedTextLabel.BackColor;
                            PasteControlOrientedTextLabel.ForeColor = CopiedControlOrientedTextLabel.ForeColor;
                            PasteControlOrientedTextLabel.RightToLeft = CopiedControlOrientedTextLabel.RightToLeft;
                            PasteControlOrientedTextLabel.Cursor = CopiedControlOrientedTextLabel.Cursor;
                            PasteControlOrientedTextLabel.AllowDrop = CopiedControlOrientedTextLabel.AllowDrop;
                            PasteControlOrientedTextLabel.Enabled = CopiedControlOrientedTextLabel.Enabled;
                            PasteControlOrientedTextLabel.TabIndex = CopiedControlOrientedTextLabel.TabIndex;
                            PasteControlOrientedTextLabel.Visible = CopiedControlOrientedTextLabel.Visible;
                            PasteControlOrientedTextLabel.CausesValidation = CopiedControlOrientedTextLabel.CausesValidation;
                            PasteControlOrientedTextLabel.Anchor = CopiedControlOrientedTextLabel.Anchor;
                            PasteControlOrientedTextLabel.Dock = CopiedControlOrientedTextLabel.Dock;
                            PasteControlOrientedTextLabel.Margin = CopiedControlOrientedTextLabel.Margin;
                            PasteControlOrientedTextLabel.Padding = CopiedControlOrientedTextLabel.Padding;
                            PasteControlOrientedTextLabel.MaximumSize = CopiedControlOrientedTextLabel.MaximumSize;
                            PasteControlOrientedTextLabel.MinimumSize = CopiedControlOrientedTextLabel.MinimumSize;
                            PasteControlOrientedTextLabel.UseWaitCursor = CopiedControlOrientedTextLabel.UseWaitCursor;

                            PasteControlOrientedTextLabel.BorderStyle = CopiedControlOrientedTextLabel.BorderStyle;
                            PasteControlOrientedTextLabel.FlatStyle = CopiedControlOrientedTextLabel.FlatStyle;
                            PasteControlOrientedTextLabel.Image = CopiedControlOrientedTextLabel.Image;
                            PasteControlOrientedTextLabel.ImageAlign = CopiedControlOrientedTextLabel.ImageAlign;
                            PasteControlOrientedTextLabel.ImageIndex = CopiedControlOrientedTextLabel.ImageIndex;
                            PasteControlOrientedTextLabel.ImageKey = CopiedControlOrientedTextLabel.ImageKey;
                            PasteControlOrientedTextLabel.RotationAngle = CopiedControlOrientedTextLabel.RotationAngle;
                            PasteControlOrientedTextLabel.TextAlign = CopiedControlOrientedTextLabel.TextAlign;
                            PasteControlOrientedTextLabel.TextDirection = CopiedControlOrientedTextLabel.TextDirection;
                            PasteControlOrientedTextLabel.TextOrientation = CopiedControlOrientedTextLabel.TextOrientation;
                            PasteControlOrientedTextLabel.UseMnemonic = CopiedControlOrientedTextLabel.UseMnemonic;
                            PasteControlOrientedTextLabel.AutoEllipsis = CopiedControlOrientedTextLabel.AutoEllipsis;
                            PasteControlOrientedTextLabel.UseCompatibleTextRendering = CopiedControlOrientedTextLabel.UseCompatibleTextRendering;
                            PasteControlOrientedTextLabel.AutoSize = CopiedControlOrientedTextLabel.AutoSize;
                            return PasteControlOrientedTextLabel;
                        }

                    case "DevExpress.XtraEditors.BarCodeControl":
                        {
                            DevExpress.XtraEditors.BarCodeControl PasteControlBarcode = new DevExpress.XtraEditors.BarCodeControl();
                            DevExpress.XtraEditors.BarCodeControl CopiedControlBarcode = Ctrl as DevExpress.XtraEditors.BarCodeControl;
                            PasteControlBarcode.Name = CopiedControlBarcode.Name;
                            PasteControlBarcode.Text = CopiedControlBarcode.Text;
                            PasteControlBarcode.Font = CopiedControlBarcode.Font;
                            PasteControlBarcode.Location = CopiedControlBarcode.Location;
                            PasteControlBarcode.Size = CopiedControlBarcode.Size;
                            PasteControlBarcode.BackColor = CopiedControlBarcode.BackColor;
                            PasteControlBarcode.ForeColor = CopiedControlBarcode.ForeColor;
                            PasteControlBarcode.RightToLeft = CopiedControlBarcode.RightToLeft;
                            PasteControlBarcode.BackgroundImageLayout = CopiedControlBarcode.BackgroundImageLayout;
                            PasteControlBarcode.Cursor = CopiedControlBarcode.Cursor;
                            PasteControlBarcode.AllowDrop = CopiedControlBarcode.AllowDrop;
                            PasteControlBarcode.Enabled = CopiedControlBarcode.Enabled;
                            PasteControlBarcode.TabIndex = CopiedControlBarcode.TabIndex;
                            PasteControlBarcode.Visible = CopiedControlBarcode.Visible;
                            PasteControlBarcode.CausesValidation = CopiedControlBarcode.CausesValidation;
                            PasteControlBarcode.Anchor = CopiedControlBarcode.Anchor;
                            PasteControlBarcode.Dock = CopiedControlBarcode.Dock;
                            PasteControlBarcode.Margin = CopiedControlBarcode.Margin;
                            PasteControlBarcode.Padding = CopiedControlBarcode.Padding;
                            PasteControlBarcode.MaximumSize = CopiedControlBarcode.MaximumSize;
                            PasteControlBarcode.MinimumSize = CopiedControlBarcode.MinimumSize;
                            PasteControlBarcode.UseWaitCursor = CopiedControlBarcode.UseWaitCursor;

                            PasteControlBarcode.BorderStyle = CopiedControlBarcode.BorderStyle;
                            PasteControlBarcode.HorizontalAlignment = CopiedControlBarcode.HorizontalAlignment;
                            PasteControlBarcode.HorizontalTextAlignment = CopiedControlBarcode.HorizontalTextAlignment;
                            PasteControlBarcode.VerticalAlignment = CopiedControlBarcode.VerticalAlignment;
                            PasteControlBarcode.VerticalTextAlignment = CopiedControlBarcode.VerticalTextAlignment;
                            PasteControlBarcode.AutoModule = CopiedControlBarcode.AutoModule;
                            PasteControlBarcode.ImeMode = CopiedControlBarcode.ImeMode;
                            PasteControlBarcode.Module = CopiedControlBarcode.Module;
                            PasteControlBarcode.Orientation = CopiedControlBarcode.Orientation;
                            PasteControlBarcode.ShowText = CopiedControlBarcode.ShowText;
                            PasteControlBarcode.Symbology = CopiedControlBarcode.Symbology;
                            PasteControlBarcode.TabStop = CopiedControlBarcode.TabStop;
                            PasteControlBarcode.AllowHtmlTextInToolTip = CopiedControlBarcode.AllowHtmlTextInToolTip;
                            PasteControlBarcode.ShowToolTips = CopiedControlBarcode.ShowToolTips;
                            PasteControlBarcode.ToolTip = CopiedControlBarcode.ToolTip;
                            PasteControlBarcode.ToolTipIconType = CopiedControlBarcode.ToolTipIconType;
                            PasteControlBarcode.ToolTipTitle = CopiedControlBarcode.ToolTipTitle;

                            return PasteControlBarcode;
                        }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("شیء مورد نظر به درستی کپی نشده است لطفا دوباره سعی نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return CopiedCtrlPanel;
        }

        public static Panel CopyControl(Control Ctrl)
        {
            Panel CopiedCtrlPanel = new Panel();
            CopiedCtrlPanel.Size = new Size(674, 425);
            CopiedCtrlPanel.ForeColor = SystemColors.ControlText;
            CopiedCtrlPanel.BackColor = Color.White;
            try
            {
                switch (Ctrl.GetType().ToString())
                {
                    case "System.Windows.Forms.PictureBox":
                        {
                            try
                            {
                                PictureBox PasteControlPictureBox = new PictureBox();
                                PictureBox CopiedControlPictureBox = Ctrl as PictureBox;
                                PasteControlPictureBox.Name = CopiedControlPictureBox.Name;
                                PasteControlPictureBox.Text = CopiedControlPictureBox.Text;
                                PasteControlPictureBox.Font = CopiedControlPictureBox.Font;
                                PasteControlPictureBox.Location = CopiedControlPictureBox.Location;
                                PasteControlPictureBox.BackgroundImageLayout = CopiedControlPictureBox.BackgroundImageLayout;
                                PasteControlPictureBox.Size = CopiedControlPictureBox.Size;
                                PasteControlPictureBox.BackColor = CopiedControlPictureBox.BackColor;
                                PasteControlPictureBox.ForeColor = CopiedControlPictureBox.ForeColor;
                                PasteControlPictureBox.RightToLeft = CopiedControlPictureBox.RightToLeft;
                                PasteControlPictureBox.Cursor = CopiedControlPictureBox.Cursor;
                                PasteControlPictureBox.AllowDrop = CopiedControlPictureBox.AllowDrop;
                                PasteControlPictureBox.Enabled = CopiedControlPictureBox.Enabled;
                                PasteControlPictureBox.TabIndex = CopiedControlPictureBox.TabIndex;
                                PasteControlPictureBox.Visible = CopiedControlPictureBox.Visible;
                                PasteControlPictureBox.CausesValidation = CopiedControlPictureBox.CausesValidation;
                                PasteControlPictureBox.Anchor = CopiedControlPictureBox.Anchor;
                                PasteControlPictureBox.Dock = CopiedControlPictureBox.Dock;
                                PasteControlPictureBox.Margin = CopiedControlPictureBox.Margin;
                                PasteControlPictureBox.Padding = CopiedControlPictureBox.Padding;
                                PasteControlPictureBox.MaximumSize = CopiedControlPictureBox.MaximumSize;
                                PasteControlPictureBox.MinimumSize = CopiedControlPictureBox.MinimumSize;
                                PasteControlPictureBox.UseWaitCursor = CopiedControlPictureBox.UseWaitCursor;
                                PasteControlPictureBox.BorderStyle = CopiedControlPictureBox.BorderStyle;
                                PasteControlPictureBox.WaitOnLoad = CopiedControlPictureBox.WaitOnLoad;
                                PasteControlPictureBox.SizeMode = CopiedControlPictureBox.SizeMode;
                                PasteControlPictureBox.ImageLocation = CopiedControlPictureBox.ImageLocation;
                                PasteControlPictureBox.Image = CopiedControlPictureBox.Image;
                                CopiedCtrlPanel.Controls.Add(PasteControlPictureBox);
                            }
                            catch (Exception)
                            {
                            }
                        }
                        break;

                    case "System.Windows.Forms.Label": //Label
                        {

                            Label PasteControlLabel = new Label();
                            Label CopiedLabel = Ctrl as Label;
                            PasteControlLabel.Name = CopiedLabel.Name;
                            PasteControlLabel.Text = CopiedLabel.Text;
                            PasteControlLabel.Font = CopiedLabel.Font;

                            PasteControlLabel.Location = CopiedLabel.Location;
                            PasteControlLabel.Size = CopiedLabel.Size;
                            PasteControlLabel.BackColor = CopiedLabel.BackColor;
                            PasteControlLabel.ForeColor = CopiedLabel.ForeColor;
                            PasteControlLabel.RightToLeft = CopiedLabel.RightToLeft;
                            PasteControlLabel.Cursor = CopiedLabel.Cursor;
                            PasteControlLabel.AllowDrop = CopiedLabel.AllowDrop;
                            PasteControlLabel.Enabled = CopiedLabel.Enabled;
                            PasteControlLabel.TabIndex = CopiedLabel.TabIndex;
                            PasteControlLabel.Visible = CopiedLabel.Visible;
                            PasteControlLabel.CausesValidation = CopiedLabel.CausesValidation;
                            PasteControlLabel.Anchor = CopiedLabel.Anchor;
                            PasteControlLabel.Dock = CopiedLabel.Dock;
                            PasteControlLabel.Margin = CopiedLabel.Margin;
                            PasteControlLabel.Padding = CopiedLabel.Padding;
                            PasteControlLabel.MaximumSize = CopiedLabel.MaximumSize;
                            PasteControlLabel.MinimumSize = CopiedLabel.MinimumSize;
                            PasteControlLabel.UseWaitCursor = CopiedLabel.UseWaitCursor;

                            PasteControlLabel.BorderStyle = CopiedLabel.BorderStyle;
                            PasteControlLabel.FlatStyle = CopiedLabel.FlatStyle;
                            PasteControlLabel.Image = CopiedLabel.Image;
                            PasteControlLabel.ImageAlign = CopiedLabel.ImageAlign;
                            PasteControlLabel.ImageIndex = CopiedLabel.ImageIndex;
                            PasteControlLabel.ImageKey = CopiedLabel.ImageKey;
                            PasteControlLabel.TextAlign = CopiedLabel.TextAlign;
                            PasteControlLabel.UseMnemonic = CopiedLabel.UseMnemonic;
                            PasteControlLabel.AutoEllipsis = CopiedLabel.AutoEllipsis;
                            PasteControlLabel.UseCompatibleTextRendering = CopiedLabel.UseCompatibleTextRendering;
                            PasteControlLabel.AutoSize = CopiedLabel.AutoSize;

                            CopiedCtrlPanel.Controls.Add(PasteControlLabel);
                        }
                        break;

                    case "CustomControl.OrientAbleTextControls.OrientedTextLabel":
                        {
                            CustomControl.OrientAbleTextControls.OrientedTextLabel PasteControlOrientedTextLabel = new CustomControl.OrientAbleTextControls.OrientedTextLabel();
                            CustomControl.OrientAbleTextControls.OrientedTextLabel CopiedControlOrientedTextLabel = Ctrl as CustomControl.OrientAbleTextControls.OrientedTextLabel;
                            PasteControlOrientedTextLabel.Name = CopiedControlOrientedTextLabel.Name;
                            PasteControlOrientedTextLabel.Text = CopiedControlOrientedTextLabel.Text;
                            PasteControlOrientedTextLabel.Font = CopiedControlOrientedTextLabel.Font;
                            PasteControlOrientedTextLabel.Location = CopiedControlOrientedTextLabel.Location;
                            PasteControlOrientedTextLabel.Size = CopiedControlOrientedTextLabel.Size;
                            PasteControlOrientedTextLabel.BackColor = CopiedControlOrientedTextLabel.BackColor;
                            PasteControlOrientedTextLabel.ForeColor = CopiedControlOrientedTextLabel.ForeColor;
                            PasteControlOrientedTextLabel.RightToLeft = CopiedControlOrientedTextLabel.RightToLeft;
                            PasteControlOrientedTextLabel.Cursor = CopiedControlOrientedTextLabel.Cursor;
                            PasteControlOrientedTextLabel.AllowDrop = CopiedControlOrientedTextLabel.AllowDrop;
                            PasteControlOrientedTextLabel.Enabled = CopiedControlOrientedTextLabel.Enabled;
                            PasteControlOrientedTextLabel.TabIndex = CopiedControlOrientedTextLabel.TabIndex;
                            PasteControlOrientedTextLabel.Visible = CopiedControlOrientedTextLabel.Visible;
                            PasteControlOrientedTextLabel.CausesValidation = CopiedControlOrientedTextLabel.CausesValidation;
                            PasteControlOrientedTextLabel.Anchor = CopiedControlOrientedTextLabel.Anchor;
                            PasteControlOrientedTextLabel.Dock = CopiedControlOrientedTextLabel.Dock;
                            PasteControlOrientedTextLabel.Margin = CopiedControlOrientedTextLabel.Margin;
                            PasteControlOrientedTextLabel.Padding = CopiedControlOrientedTextLabel.Padding;
                            PasteControlOrientedTextLabel.MaximumSize = CopiedControlOrientedTextLabel.MaximumSize;
                            PasteControlOrientedTextLabel.MinimumSize = CopiedControlOrientedTextLabel.MinimumSize;
                            PasteControlOrientedTextLabel.UseWaitCursor = CopiedControlOrientedTextLabel.UseWaitCursor;

                            PasteControlOrientedTextLabel.BorderStyle = CopiedControlOrientedTextLabel.BorderStyle;
                            PasteControlOrientedTextLabel.FlatStyle = CopiedControlOrientedTextLabel.FlatStyle;
                            PasteControlOrientedTextLabel.Image = CopiedControlOrientedTextLabel.Image;
                            PasteControlOrientedTextLabel.ImageAlign = CopiedControlOrientedTextLabel.ImageAlign;
                            PasteControlOrientedTextLabel.ImageIndex = CopiedControlOrientedTextLabel.ImageIndex;
                            PasteControlOrientedTextLabel.ImageKey = CopiedControlOrientedTextLabel.ImageKey;
                            PasteControlOrientedTextLabel.RotationAngle = CopiedControlOrientedTextLabel.RotationAngle;
                            PasteControlOrientedTextLabel.TextAlign = CopiedControlOrientedTextLabel.TextAlign;
                            PasteControlOrientedTextLabel.TextDirection = CopiedControlOrientedTextLabel.TextDirection;
                            PasteControlOrientedTextLabel.TextOrientation = CopiedControlOrientedTextLabel.TextOrientation;
                            PasteControlOrientedTextLabel.UseMnemonic = CopiedControlOrientedTextLabel.UseMnemonic;
                            PasteControlOrientedTextLabel.AutoEllipsis = CopiedControlOrientedTextLabel.AutoEllipsis;
                            PasteControlOrientedTextLabel.UseCompatibleTextRendering = CopiedControlOrientedTextLabel.UseCompatibleTextRendering;
                            PasteControlOrientedTextLabel.AutoSize = CopiedControlOrientedTextLabel.AutoSize;
                            CopiedCtrlPanel.Controls.Add(PasteControlOrientedTextLabel);
                        }
                        break;
                    case "DevExpress.XtraEditors.BarCodeControl":
                        {
                            DevExpress.XtraEditors.BarCodeControl PasteControlBarcode = new DevExpress.XtraEditors.BarCodeControl();
                            DevExpress.XtraEditors.BarCodeControl CopiedControlBarcode = Ctrl as DevExpress.XtraEditors.BarCodeControl;
                            PasteControlBarcode.Name = CopiedControlBarcode.Name;
                            PasteControlBarcode.Text = CopiedControlBarcode.Text;
                            PasteControlBarcode.Font = CopiedControlBarcode.Font;
                            PasteControlBarcode.Location = CopiedControlBarcode.Location;
                            PasteControlBarcode.Size = CopiedControlBarcode.Size;
                            PasteControlBarcode.BackColor = CopiedControlBarcode.BackColor;
                            PasteControlBarcode.ForeColor = CopiedControlBarcode.ForeColor;
                            PasteControlBarcode.RightToLeft = CopiedControlBarcode.RightToLeft;
                            PasteControlBarcode.BackgroundImageLayout = CopiedControlBarcode.BackgroundImageLayout;
                            PasteControlBarcode.Cursor = CopiedControlBarcode.Cursor;
                            PasteControlBarcode.AllowDrop = CopiedControlBarcode.AllowDrop;
                            PasteControlBarcode.Enabled = CopiedControlBarcode.Enabled;
                            PasteControlBarcode.TabIndex = CopiedControlBarcode.TabIndex;
                            PasteControlBarcode.Visible = CopiedControlBarcode.Visible;
                            PasteControlBarcode.CausesValidation = CopiedControlBarcode.CausesValidation;
                            PasteControlBarcode.Anchor = CopiedControlBarcode.Anchor;
                            PasteControlBarcode.Dock = CopiedControlBarcode.Dock;
                            PasteControlBarcode.Margin = CopiedControlBarcode.Margin;
                            PasteControlBarcode.Padding = CopiedControlBarcode.Padding;
                            PasteControlBarcode.MaximumSize = CopiedControlBarcode.MaximumSize;
                            PasteControlBarcode.MinimumSize = CopiedControlBarcode.MinimumSize;
                            PasteControlBarcode.UseWaitCursor = CopiedControlBarcode.UseWaitCursor;

                            PasteControlBarcode.BorderStyle = CopiedControlBarcode.BorderStyle;
                            PasteControlBarcode.HorizontalAlignment = CopiedControlBarcode.HorizontalAlignment;
                            PasteControlBarcode.HorizontalTextAlignment = CopiedControlBarcode.HorizontalTextAlignment;
                            PasteControlBarcode.VerticalAlignment = CopiedControlBarcode.VerticalAlignment;
                            PasteControlBarcode.VerticalTextAlignment = CopiedControlBarcode.VerticalTextAlignment;
                            PasteControlBarcode.AutoModule = CopiedControlBarcode.AutoModule;
                            PasteControlBarcode.ImeMode = CopiedControlBarcode.ImeMode;
                            PasteControlBarcode.Module = CopiedControlBarcode.Module;
                            PasteControlBarcode.Orientation = CopiedControlBarcode.Orientation;
                            PasteControlBarcode.ShowText = CopiedControlBarcode.ShowText;
                            PasteControlBarcode.Symbology = CopiedControlBarcode.Symbology;
                            PasteControlBarcode.TabStop = CopiedControlBarcode.TabStop;
                            PasteControlBarcode.AllowHtmlTextInToolTip = CopiedControlBarcode.AllowHtmlTextInToolTip;
                            PasteControlBarcode.ShowToolTips = CopiedControlBarcode.ShowToolTips;
                            PasteControlBarcode.ToolTip = CopiedControlBarcode.ToolTip;
                            PasteControlBarcode.ToolTipIconType = CopiedControlBarcode.ToolTipIconType;
                            PasteControlBarcode.ToolTipTitle = CopiedControlBarcode.ToolTipTitle;
                            CopiedCtrlPanel.Controls.Add(PasteControlBarcode);
                        }
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("شیء مورد نظر به درستی کپی نشده است لطفا دوباره سعی نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return CopiedCtrlPanel;
        }
        Bitmap[] SplitPictureControl(Panel Ctrl)
        {
            Bitmap[] TwoType = new Bitmap[2];

            int j = 0;
            Panel ExceptPicture = new Panel();
            Panel OnlyPicture = new Panel();

            ExceptPicture.Size = new Size(674, 425);
            ExceptPicture.ForeColor = SystemColors.ControlText;
            ExceptPicture.BackColor = Color.White;

            OnlyPicture.Size = new Size(674, 425);
            OnlyPicture.ForeColor = SystemColors.ControlText;
            OnlyPicture.BackColor = Color.White;


            foreach (Control item in Ctrl.Controls)
            {

                if (item.GetType() == typeof(System.Windows.Forms.PictureBox))
                {
                    //Panel tmp = new Panel();
                    //tmp.Size = new Size(674, 425);
                    //tmp = CopyControl(item);
                    //j = 0;
                    //for (int i = 0; i < tmp.Controls.Count; i++)
                    //{
                    //    if (tmp.Controls[i].Name == item.Name)
                    //        j = i;
                    //}
                    //if (tmp.Controls.Count > 0)
                    //    OnlyPicture.Controls.Add(tmp.Controls[j]);
                    //tmp.Dispose();
                    OnlyPicture.Controls.Add(CopyControlNotPanel(item));
                }
                else
                {



                    ExceptPicture.Controls.Add(CopyControlNotPanel(item));



                    //j = 0;
                    //for (int i = 0; i < tmp.Controls.Count; i++)
                    //{
                    //    if (tmp.Controls[i].Name == item.Name)
                    //        j = i;
                    //}
                    //if (tmp.Controls.Count > 0)
                    //    ExceptPicture.Controls.Add(tmp.Controls[j]);
                    //tmp.Dispose();


                    //ExceptPicture.Controls.Add(item);
                    //var TmpImage = GetControlImage(ExceptPicture);
                    //var scale = GrayScale(TmpImage);
                    //TwoType[0] = scale.Clone(new Rectangle(0, 0, scale.Width, scale.Height), PxFormat);
                    //TwoType[0].SetResolution(200, 200);

                    //TmpImage.Dispose();
                    //scale.Dispose();
                    //ExceptPicture.Dispose();
                    //ExceptPicture = new Panel();
                    //Panel tmp = new Panel();
                    //tmp.Size = new Size(674, 425);
                    //tmp = CopyControl(item);
                    //j = 0;
                    //for (int i = 0; i < tmp.Controls.Count; i++)
                    //{
                    //    if (tmp.Controls[i].Name == item.Name)
                    //        j = i;
                    //}
                    //if (tmp.Controls.Count > 0)
                    //    ExceptPicture.Controls.Add(tmp.Controls[j]);
                    //tmp.Dispose();
                }
            }

            newImage = GetControlImage(ExceptPicture);
            TwoType[0] = GrayScale(newImage);
            TwoType[0] = TwoType[0].Clone(new Rectangle(0, 0, ExceptPicture.Width, ExceptPicture.Height), PxFormat);
            TwoType[0].SetResolution(200, 200);
            TwoType[0].Save(@"C:\Users\MEHRDA~1\AppData\Local\Temp\LaserPrinterImagetmp\1.bmp", ImageFormat.Bmp);
            newImage.Dispose();



            newImage = GetControlImage(OnlyPicture);
            TwoType[1] = GrayScale(newImage);
            TwoType[1] = TwoType[1].Clone(new Rectangle(0, 0, OnlyPicture.Width, OnlyPicture.Height), PxFormat);
            TwoType[1].SetResolution(200, 200);
            TwoType[1].Save(@"C:\Users\MEHRDA~1\AppData\Local\Temp\LaserPrinterImagetmp\2.bmp", ImageFormat.Bmp);
            newImage.Dispose();


            //TwoType[1] = GetControlImage(OnlyPicture);
            //TwoType[1].SetResolution(200, 200);


            return TwoType;
        }

        public static Bitmap GrayScale(Bitmap Bmp)
        {
            int rgb;
            Color c;

            for (int y = 0; y < Bmp.Height; y++)
                for (int x = 0; x < Bmp.Width; x++)
                {
                    c = Bmp.GetPixel(x, y);
                    rgb = (int)((c.R + c.G + c.B) / 3);
                    Bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            return Bmp;
        }
        private static Bitmap GetControlImage(Panel ctl)
        {
            Bitmap bm = new Bitmap(ctl.Width, ctl.Height);
            ctl.DrawToBitmap(bm, new Rectangle(0, 0, ctl.Width, ctl.Height));

            return bm;
        }
        public static double[] convertPxTomm(Point Pixellocation)
        {
            const int DPI = 200;
            double[] Location = new double[2];
            double XPos = 0, YPos = 0;
            XPos = (Pixellocation.X * 25.4) / DPI;
            YPos = (Pixellocation.Y * 25.4) / DPI;
            Location[0] = XPos;
            Location[1] = YPos;
            return Location;
        }
        public static Point ConvermmtoPx(double XmmLocation, double YmmLocation)
        {
            const int DPI = 200;
            Point Location = new Point();
            double XPos = 0, YPos = 0;
            XPos = (XmmLocation * DPI) / 25.4;
            YPos = (YmmLocation * DPI) / 25.4;
            Location.X = (int)Math.Ceiling(XPos);
            Location.Y = (int)Math.Ceiling(YPos);
            return Location;
        }
        public static double[] WithDPIconvertPxTomm(Size Pixellocation, float DPIX, float DPIY)
        {

            double[] Location = new double[2];
            double XPos = 0, YPos = 0;
            XPos = (Pixellocation.Width * 25.4) / DPIX;
            YPos = (Pixellocation.Height * 25.4) / DPIY;
            Location[0] = XPos;
            Location[1] = YPos;
            return Location;
        }
        public static Size WithDPIConvermmtoPx(double XmmLocation, double YmmLocation, float DPIX, float DPIY)
        {

            Size Location = new Size();
            double XPos = 0, YPos = 0;
            XPos = (XmmLocation * DPIX) / 25.4;
            YPos = (YmmLocation * DPIY) / 25.4;
            Location.Width = (int)Math.Ceiling(XPos);
            Location.Height = (int)Math.Ceiling(YPos);
            return Location;
        }
        public static string ImageToBase64(string Path)
        {

            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        public static System.Drawing.Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

        public static XmlDocument MakeXML(Panel PanelControl)
        {
            //Control Variable Definition
            DevExpress.XtraEditors.BarCodeControl BarcodeControl = new DevExpress.XtraEditors.BarCodeControl();
            CustomControl.OrientAbleTextControls.OrientedTextLabel OrientedTextLabel = new CustomControl.OrientAbleTextControls.OrientedTextLabel();
            PictureBox PictureBoxControl = new PictureBox();
            Label LabelControl = new Label();
            bool flgSomethingWorng = false;
            // Write down the XML declaration
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);

            // Create the root element
            XmlElement rootNode = xmlDoc.CreateElement("SaraHardwareCompanyLaserPrinter");
            xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);
            xmlDoc.AppendChild(rootNode);

            //----------------------PropertyVariable-------------------------------------------
            try
            {
                #region PropertyVariable
                foreach (Control p in PanelControl.Controls)
                {
                    string Names = p.Name;//OK
                    string types = p.GetType().ToString();//OK
                    string LocationX = p.Location.X.ToString();//OK
                    string LocationY = p.Location.Y.ToString();//OK
                    string sizeWidth = p.Width.ToString();//OK
                    string sizeHegiht = p.Height.ToString();//OK
                    string Texts = p.Text.ToString();//OK
                    string Fonts = FontToString(p.Font);//OK
                    string RightToLeft = p.RightToLeft.ToString();//OK
                    string BackColors = p.BackColor.Name.ToString();//OK
                    string Forecolors = p.ForeColor.Name.ToString();//OK
                    string BackgroundImageLayout = p.BackgroundImageLayout.ToString();
                    string Cursor = p.Cursor.ToString();
                    string AllowDrop = p.AllowDrop.ToString();
                    string Enable = p.Enabled.ToString();
                    string TabIndex = p.TabIndex.ToString();
                    string Visible = p.Visible.ToString();
                    string CausesValidation = p.CausesValidation.ToString();
                    string Anchor = p.Anchor.ToString();
                    string Dock = p.Dock.ToString();
                    string Margin = p.Margin.ToString();
                    string Padding = p.Padding.ToString();
                    string MaximumSizeWidth = p.MaximumSize.Width.ToString();
                    string MaximumSizeHeight = p.MaximumSize.Height.ToString();
                    string MinimumSizeWidth = p.MinimumSize.Width.ToString();
                    string MinimumSizeHeight = p.MinimumSize.Height.ToString();
                    string UseWaitCursor = p.UseWaitCursor.ToString();

                    //barcode Variables
                    string BCBoarderStyle = "";
                    string BCHorizontalAlignment = "";
                    string BCHorizontalTextAlignment = "";
                    string BCLookAndFeel = "";
                    string BCVerticalAlignment = "";
                    string BCVerticalTextAlignment = "";
                    string BCAutoModule = "";
                    string BCImeMode = "";
                    string BCModule = "";
                    string BCOrientation = "";
                    string BCShowText = "";
                    string BCSymbology = "";
                    string BCtabstop = "";
                    string BCbinaryData = "";
                    string BCAllowHtmlTextInToolTip = "";
                    string BCShowToolTips = "";
                    string BCToolTip = "";
                    string BCToolTipIconType = "";
                    string BCToolTipTitle = "";

                    //PictureBox Variable
                    string PBBoarderStyle = "";
                    string PBWaitOnLoad = "";
                    string PBSizeMode = "";
                    string PBImageLocation = "";

                    //Label Variable
                    string lblLabelBoarderStyle = "";
                    string lblFlatStyle = "";
                    string lblImageAlign = "";
                    string lblImageIndex = "";
                    string lblimageKey = "";
                    string lblTextAlign = "";
                    string lblUseMnemonic = "";
                    string lblAutoEllipsis = "";
                    string lblUseCompatibleTextRendering = "";
                    string lblAutoSize = "";
                    //OrientedTextLabel
                    string OrLblBorderStyle = "";
                    string OrLblFlatStyle = "";
                    string OrLblImageAlign = "";
                    string OrLblImageIndex = "";
                    string OrLblImageKey = "";
                    string OrLblRotationAngle = "";
                    string OrLblTextAlign = "";
                    string OrLblTextDirection = "";
                    string OrLblTextOrientation = "";
                    string OrLblUseMnemonic = "";
                    string OrLblAutoEllipsis = "";
                    string OrLblUseCompatibleTextRendering = "";
                    string OrLblAutoSize = "";

                    #endregion
                    #region Fill PictureBox and Barcode and Label Propert

                    switch (p.GetType().ToString())
                    {
                        case "DevExpress.XtraEditors.BarCodeControl":
                            BarcodeControl = (DevExpress.XtraEditors.BarCodeControl)p;
                            BCBoarderStyle = BarcodeControl.BorderStyle.ToString();
                            BCHorizontalAlignment = BarcodeControl.HorizontalAlignment.ToString();
                            BCHorizontalTextAlignment = BarcodeControl.HorizontalTextAlignment.ToString();
                            BCLookAndFeel = BarcodeControl.LookAndFeel.ToString();
                            BCVerticalAlignment = BarcodeControl.VerticalAlignment.ToString();
                            BCVerticalTextAlignment = BarcodeControl.VerticalTextAlignment.ToString();
                            BCAutoModule = BarcodeControl.AutoModule.ToString();
                            BCImeMode = BarcodeControl.ImeMode.ToString();
                            BCModule = BarcodeControl.Module.ToString();
                            BCOrientation = BarcodeControl.Orientation.ToString();
                            BCShowText = BarcodeControl.ShowText.ToString();
                            BCSymbology = BarcodeControl.Symbology.ToString();
                            BCtabstop = BarcodeControl.TabStop.ToString();
                            BCbinaryData = BarcodeControl.BinaryData.ToString();
                            BCAllowHtmlTextInToolTip = BarcodeControl.AllowHtmlTextInToolTip.ToString();
                            BCShowToolTips = BarcodeControl.ShowToolTips.ToString();
                            BCToolTip = BarcodeControl.ToolTip.ToString();
                            BCToolTipIconType = BarcodeControl.ToolTipIconType.ToString();
                            BCToolTipTitle = BarcodeControl.ToolTipTitle.ToString();

                            break;

                        case "System.Windows.Forms.PictureBox":
                            PictureBoxControl = (PictureBox)p;
                            PBBoarderStyle = PictureBoxControl.BorderStyle.ToString();
                            PBWaitOnLoad = PictureBoxControl.WaitOnLoad.ToString();
                            PBSizeMode = PictureBoxControl.SizeMode.ToString();
                            break;


                        case "System.Windows.Forms.Label":
                            LabelControl = (Label)p;
                            lblLabelBoarderStyle = LabelControl.BorderStyle.ToString();
                            lblFlatStyle = LabelControl.FlatStyle.ToString();
                            lblImageAlign = LabelControl.ImageAlign.ToString();
                            lblImageIndex = LabelControl.ImageIndex.ToString();
                            lblimageKey = LabelControl.ImageKey.ToString();
                            lblTextAlign = LabelControl.TextAlign.ToString();
                            lblUseMnemonic = LabelControl.UseMnemonic.ToString();
                            lblAutoEllipsis = LabelControl.AutoEllipsis.ToString();
                            lblUseCompatibleTextRendering = LabelControl.UseCompatibleTextRendering.ToString();
                            lblAutoSize = LabelControl.AutoSize.ToString();
                            break;
                        case "CustomControl.OrientAbleTextControls.OrientedTextLabel":
                            OrientedTextLabel = (CustomControl.OrientAbleTextControls.OrientedTextLabel)p;
                            OrLblBorderStyle = OrientedTextLabel.BorderStyle.ToString();
                            OrLblFlatStyle = OrientedTextLabel.FlatStyle.ToString();
                            OrLblImageAlign = OrientedTextLabel.ImageAlign.ToString();
                            OrLblImageIndex = OrientedTextLabel.ImageIndex.ToString();
                            OrLblImageKey = OrientedTextLabel.ImageKey.ToString();
                            OrLblRotationAngle = OrientedTextLabel.RotationAngle.ToString();
                            OrLblTextAlign = OrientedTextLabel.TextAlign.ToString();
                            OrLblTextDirection = OrientedTextLabel.TextDirection.ToString();
                            OrLblTextOrientation = OrientedTextLabel.TextOrientation.ToString();
                            OrLblUseMnemonic = OrientedTextLabel.UseMnemonic.ToString();
                            OrLblAutoEllipsis = OrientedTextLabel.AutoEllipsis.ToString();
                            OrLblUseCompatibleTextRendering = OrientedTextLabel.UseCompatibleTextRendering.ToString();
                            OrLblAutoSize = OrientedTextLabel.AutoSize.ToString();
                            break;
                        default:
                            break;
                    }


                    #endregion
                    #region Convert Picture To Base64
                    PictureBox pic = p as PictureBox; //cast control into PictureBox
                    byte[] bytes = null;
                    string PicBitMapImages = "";
                    if (pic != null) //if it is pictureBox, then it will not be null.
                    {
                        if (pic.Image != null)
                        {
                            Bitmap img = new Bitmap(pic.Image);
                            bytes = imageToByteArray(img);
                            PicBitMapImages = Convert.ToBase64String(bytes);
                        }
                    }
                    #endregion
                    #region CreateXmlElement
                    // Create a new <Category> element and add it to the root node
                    XmlElement parentNode = xmlDoc.CreateElement("AllObjectParent");
                    // Set attribute name and value!
                    parentNode.SetAttribute("ID", p.GetType().ToString());
                    xmlDoc.DocumentElement.PrependChild(parentNode);

                    // Create the required nodes
                    XmlElement CntrlType = xmlDoc.CreateElement("Type");//OK
                    XmlElement CntrlName = xmlDoc.CreateElement("Name");//OK
                    XmlElement CntrlText = xmlDoc.CreateElement("Text");//OK
                    XmlElement CntrlFonts = xmlDoc.CreateElement("Fonts");//OK
                    XmlElement CntrlLocationX = xmlDoc.CreateElement("LocationX");//OK
                    XmlElement CntrlLocationY = xmlDoc.CreateElement("LocationY");//OK
                    XmlElement CntrlSizeWidth = xmlDoc.CreateElement("SizeWidth");//OK
                    XmlElement CntrlSizeHegith = xmlDoc.CreateElement("SizeHeight");//OK
                    XmlElement CntrlPictureImage = xmlDoc.CreateElement("PictureImage");
                    XmlElement CntrlBackColor = xmlDoc.CreateElement("BackColor");//OK
                    XmlElement CntrlForeColor = xmlDoc.CreateElement("ForeColor");//OK
                    XmlElement CntrlRightToLeft = xmlDoc.CreateElement("RightToLeft");//OK
                    XmlElement CntrlBackgroundImageLayout = xmlDoc.CreateElement("BackgroundImageLayout");
                    XmlElement CntrlCursor = xmlDoc.CreateElement("Cursor");
                    XmlElement CntrlAllowDrop = xmlDoc.CreateElement("AllowDrop");
                    XmlElement CntrlEnable = xmlDoc.CreateElement("Enable");
                    XmlElement CntrlTabIndex = xmlDoc.CreateElement("TabIndex");
                    XmlElement CntrlVisible = xmlDoc.CreateElement("Visible");
                    XmlElement CntrlCausesValidation = xmlDoc.CreateElement("CausesValidation");
                    XmlElement CntrlAnchor = xmlDoc.CreateElement("Anchor");
                    XmlElement CntrlDock = xmlDoc.CreateElement("Dock");
                    XmlElement CntrlMargin = xmlDoc.CreateElement("Margin");
                    XmlElement CntrlPadding = xmlDoc.CreateElement("Padding");
                    XmlElement CntrlMaximumSizeWidth = xmlDoc.CreateElement("MaximumSizeWidth");
                    XmlElement CntrlMaximumSizeHeight = xmlDoc.CreateElement("MaximumSizeHeight");
                    XmlElement CntrlMinimumSizeWidth = xmlDoc.CreateElement("MinimumSizeWidth");
                    XmlElement CntrlMinimumSizeHeight = xmlDoc.CreateElement("MinimumSizeHeight");
                    XmlElement CntrlUseWaitCursor = xmlDoc.CreateElement("UseWaitCursor");

                    XmlElement CntrlBCBoarderStyle = xmlDoc.CreateElement("BCBoarderStyle");
                    XmlElement CntrlBCHorizontalAlignment = xmlDoc.CreateElement("BCHorizontalAlignment");
                    XmlElement CntrlBCHorizontalTextAlignment = xmlDoc.CreateElement("BCHorizontalTextAlignment");
                    XmlElement CntrlBCLookAndFeel = xmlDoc.CreateElement("BCLookAndFeel");
                    XmlElement CntrlBCVerticalAlignment = xmlDoc.CreateElement("BCVerticalAlignment");
                    XmlElement CntrlBCVerticalTextAlignment = xmlDoc.CreateElement("BCVerticalTextAlignment");
                    XmlElement CntrlBCAutoModule = xmlDoc.CreateElement("BCAutoModule");
                    XmlElement CntrlBCImeMode = xmlDoc.CreateElement("BCImeMode");
                    XmlElement CntrlBCModule = xmlDoc.CreateElement("BCModule");
                    XmlElement CntrlBCOrientation = xmlDoc.CreateElement("BCOrientation");
                    XmlElement CntrlBCShowText = xmlDoc.CreateElement("BCShowText");
                    XmlElement CntrlBCSymbology = xmlDoc.CreateElement("BCSymbology");
                    XmlElement CntrlBCtabstop = xmlDoc.CreateElement("BCtabstop");
                    XmlElement CntrlBCbinaryData = xmlDoc.CreateElement("BCbinaryData");
                    XmlElement CntrlBCAllowHtmlTextInToolTip = xmlDoc.CreateElement("BCAllowHtmlTextInToolTip");
                    XmlElement CntrlBCShowToolTips = xmlDoc.CreateElement("BCShowToolTips");
                    XmlElement CntrlBCToolTip = xmlDoc.CreateElement("BCToolTip");
                    XmlElement CntrlBCToolTipIconType = xmlDoc.CreateElement("BCToolTipIconType");
                    XmlElement CntrlBCToolTipTitle = xmlDoc.CreateElement("BCToolTipTitle");

                    XmlElement CntrlPBBoarderStyle = xmlDoc.CreateElement("PBBoarderStyle");
                    XmlElement CntrlPBWaitOnLoad = xmlDoc.CreateElement("PBWaitOnLoad");
                    XmlElement CntrlPBSizeMode = xmlDoc.CreateElement("PBSizeMode");
                    XmlElement CntrlPBImageLocation = xmlDoc.CreateElement("PBImageLocation");

                    XmlElement CntrllblLabelBoarderStyle = xmlDoc.CreateElement("lblLabelBoarderStyle");
                    XmlElement CntrllblFlatStyle = xmlDoc.CreateElement("lblFlatStyle");
                    XmlElement CntrllblImageAlign = xmlDoc.CreateElement("lblImageAlign");
                    XmlElement CntrllblImageIndex = xmlDoc.CreateElement("lblImageIndex");
                    XmlElement CntrllblimageKey = xmlDoc.CreateElement("lblimageKey");
                    XmlElement CntrllblTextAlign = xmlDoc.CreateElement("lblTextAlign");
                    XmlElement CntrllblUseMnemonic = xmlDoc.CreateElement("lblUseMnemonic");
                    XmlElement CntrllblAutoEllipsis = xmlDoc.CreateElement("lblAutoEllipsis");
                    XmlElement CntrllblUseCompatibleTextRendering = xmlDoc.CreateElement("lblUseCompatibleTextRendering");
                    XmlElement CntrllblAutoSize = xmlDoc.CreateElement("lblAutoSize");

                    XmlElement CntrlOrLblBorderStyle = xmlDoc.CreateElement("OrLblBorderStyle");
                    XmlElement CntrlOrLblFlatStyle = xmlDoc.CreateElement("OrLblFlatStyle");
                    XmlElement CntrlOrLblImageAlign = xmlDoc.CreateElement("OrLblImageAlign");
                    XmlElement CntrlOrLblImageIndex = xmlDoc.CreateElement("OrLblImageIndex");
                    XmlElement CntrlOrLblImageKey = xmlDoc.CreateElement("OrLblImageKey");
                    XmlElement CntrlOrLblRotationAngle = xmlDoc.CreateElement("OrLblRotationAngle");
                    XmlElement CntrlOrLblTextAlign = xmlDoc.CreateElement("OrLblTextAlign");
                    XmlElement CntrlOrLblTextDirection = xmlDoc.CreateElement("OrLblTextDirection");
                    XmlElement CntrlOrLblTextOrientation = xmlDoc.CreateElement("OrLblTextOrientation");
                    XmlElement CntrlOrLblUseMnemonic = xmlDoc.CreateElement("OrLblUseMnemonic");
                    XmlElement CntrlOrLblAutoEllipsis = xmlDoc.CreateElement("OrLblAutoEllipsis");
                    XmlElement CntrlOrLblUseCompatibleTextRendering = xmlDoc.CreateElement("OrLblUseCompatibleTextRendering");
                    XmlElement CntrlOrLblAutoSize = xmlDoc.CreateElement("OrLblAutoSize");

                    //For Grid
                    XmlElement gridrowsBackColor = xmlDoc.CreateElement("gridsrowsBackColor");
                    XmlElement gridAlternaterowsBackColor = xmlDoc.CreateElement("gridsAlternaterowsBackColor");
                    XmlElement gridheaderColor = xmlDoc.CreateElement("gridsheaderColor");
                    // For Grid
                    //XmlElement nodePanelWidth = xmlDoc.CreateElement("panelWidth");
                    //XmlElement nodePanelHeight = xmlDoc.CreateElement("panelHeight");
                    // retrieve the text 

                    DataGridView dgvs = p as DataGridView; //cast control into PictureBox

                    if (dgvs != null) //if it is pictureBox, then it will not be null.
                    {
                        BackColors = dgvs.BackgroundColor.Name;
                        Forecolors = dgvs.ForeColor.Name;
                    }
                    #endregion
                    #region Fill XmlTest

                    XmlText XmlTextCntrlType = xmlDoc.CreateTextNode(types);
                    XmlText XmlTextCntrlNames = xmlDoc.CreateTextNode(Names);
                    XmlText XmlTextCntrlText = xmlDoc.CreateTextNode(Texts);
                    XmlText XmlTextCntrlFont = xmlDoc.CreateTextNode(Fonts);
                    XmlText XmlTextCntrlLocationX = xmlDoc.CreateTextNode(LocationX);
                    XmlText XmlTextCntrlLocationY = xmlDoc.CreateTextNode(LocationY);
                    XmlText XmlTextCntrlSizeWidth = xmlDoc.CreateTextNode(sizeWidth);
                    XmlText XmlTextCntrlSizeHeight = xmlDoc.CreateTextNode(sizeHegiht);
                    XmlText XmlTextCntrlPictureImage = xmlDoc.CreateTextNode(PicBitMapImages);
                    XmlText XmlTextCntrlBackColor = xmlDoc.CreateTextNode(BackColors);
                    XmlText XmlTextCntrlForeColor = xmlDoc.CreateTextNode(Forecolors);
                    XmlText XmlTextCntrlRightToLeft = xmlDoc.CreateTextNode(RightToLeft);
                    XmlText XmlTextCntrlBackgroundImageLayout = xmlDoc.CreateTextNode(BackgroundImageLayout);
                    XmlText XmlTextCntrlCursor = xmlDoc.CreateTextNode(Cursor);
                    XmlText XmlTextCntrlAllowDrop = xmlDoc.CreateTextNode(AllowDrop);
                    XmlText XmlTextCntrlEnable = xmlDoc.CreateTextNode(Enable);
                    XmlText XmlTextCntrlTabIndex = xmlDoc.CreateTextNode(TabIndex);
                    XmlText XmlTextCntrlVisible = xmlDoc.CreateTextNode(Visible);
                    XmlText XmlTextCntrlCausesValidation = xmlDoc.CreateTextNode(CausesValidation);
                    XmlText XmlTextCntrlAnchor = xmlDoc.CreateTextNode(Anchor);
                    XmlText XmlTextCntrlDock = xmlDoc.CreateTextNode(Dock);
                    XmlText XmlTextCntrlMargin = xmlDoc.CreateTextNode(Margin);
                    XmlText XmlTextCntrlPadding = xmlDoc.CreateTextNode(Padding);
                    XmlText XmlTextCntrlMaximumSizeWidth = xmlDoc.CreateTextNode(MaximumSizeWidth);
                    XmlText XmlTextCntrlMaximumSizeHeight = xmlDoc.CreateTextNode(MaximumSizeHeight);
                    XmlText XmlTextCntrlMinimumSizeWidth = xmlDoc.CreateTextNode(MinimumSizeWidth);
                    XmlText XmlTextCntrlMinimumSizeHeight = xmlDoc.CreateTextNode(MinimumSizeHeight);
                    XmlText XmlTextCntrlUseWaitCursor = xmlDoc.CreateTextNode(UseWaitCursor);


                    XmlText XmlTextCntrlBCBoarderStyle = xmlDoc.CreateTextNode(BCBoarderStyle);
                    XmlText XmlTextCntrlBCHorizontalAlignment = xmlDoc.CreateTextNode(BCHorizontalAlignment);
                    XmlText XmlTextCntrlBCHorizontalTextAlignment = xmlDoc.CreateTextNode(BCHorizontalTextAlignment);
                    XmlText XmlTextCntrlBCLookAndFeel = xmlDoc.CreateTextNode(BCLookAndFeel);
                    XmlText XmlTextCntrlBCVerticalAlignment = xmlDoc.CreateTextNode(BCVerticalAlignment);
                    XmlText XmlTextCntrlBCVerticalTextAlignment = xmlDoc.CreateTextNode(BCVerticalTextAlignment);
                    XmlText XmlTextCntrlBCAutoModule = xmlDoc.CreateTextNode(BCAutoModule);
                    XmlText XmlTextCntrlBCImeMode = xmlDoc.CreateTextNode(BCImeMode);
                    XmlText XmlTextCntrlBCModule = xmlDoc.CreateTextNode(BCModule);
                    XmlText XmlTextCntrlBCOrientation = xmlDoc.CreateTextNode(BCOrientation);
                    XmlText XmlTextCntrlBCShowText = xmlDoc.CreateTextNode(BCShowText);
                    XmlText XmlTextCntrlBCSymbology = xmlDoc.CreateTextNode(BCSymbology);
                    XmlText XmlTextCntrlBCtabstop = xmlDoc.CreateTextNode(BCtabstop);
                    XmlText XmlTextCntrlBCbinaryData = xmlDoc.CreateTextNode(BCbinaryData);
                    XmlText XmlTextCntrlBCAllowHtmlTextInToolTip = xmlDoc.CreateTextNode(BCAllowHtmlTextInToolTip);
                    XmlText XmlTextCntrlBCShowToolTips = xmlDoc.CreateTextNode(BCShowToolTips);
                    XmlText XmlTextCntrlBCToolTip = xmlDoc.CreateTextNode(BCToolTip);
                    XmlText XmlTextCntrlBCToolTipIconType = xmlDoc.CreateTextNode(BCToolTipIconType);
                    XmlText XmlTextCntrlBCToolTipTitle = xmlDoc.CreateTextNode(BCToolTipTitle);

                    XmlText XmlTextCntrlPBBoarderStyle = xmlDoc.CreateTextNode(PBBoarderStyle);
                    XmlText XmlTextCntrlPBWaitOnLoad = xmlDoc.CreateTextNode(PBWaitOnLoad);
                    XmlText XmlTextCntrlPBSizeMode = xmlDoc.CreateTextNode(PBSizeMode);
                    XmlText XmlTextCntrlPBImageLocation = xmlDoc.CreateTextNode(PBImageLocation);

                    XmlText XmlTextCntrllblLabelBoarderStyle = xmlDoc.CreateTextNode(lblLabelBoarderStyle);
                    XmlText XmlTextCntrllblFlatStyle = xmlDoc.CreateTextNode(lblFlatStyle);
                    XmlText XmlTextCntrllblImageAlign = xmlDoc.CreateTextNode(lblImageAlign);
                    XmlText XmlTextCntrllblImageIndex = xmlDoc.CreateTextNode(lblImageIndex);
                    XmlText XmlTextCntrllblimageKey = xmlDoc.CreateTextNode(lblimageKey);
                    XmlText XmlTextCntrllblTextAlign = xmlDoc.CreateTextNode(lblTextAlign);
                    XmlText XmlTextCntrllblUseMnemonic = xmlDoc.CreateTextNode(lblUseMnemonic);
                    XmlText XmlTextCntrllblAutoEllipsis = xmlDoc.CreateTextNode(lblAutoEllipsis);
                    XmlText XmlTextCntrllblUseCompatibleTextRendering = xmlDoc.CreateTextNode(lblUseCompatibleTextRendering);
                    XmlText XmlTextCntrllblAutoSize = xmlDoc.CreateTextNode(lblAutoSize);



                    XmlText XmlTextCntrlOrLblBorderStyle = xmlDoc.CreateTextNode(OrLblBorderStyle);
                    XmlText XmlTextCntrlOrLblFlatStyle = xmlDoc.CreateTextNode(OrLblFlatStyle);
                    XmlText XmlTextCntrlOrLblImageAlign = xmlDoc.CreateTextNode(OrLblImageAlign);
                    XmlText XmlTextCntrlOrLblImageIndex = xmlDoc.CreateTextNode(OrLblImageIndex);
                    XmlText XmlTextCntrlOrLblImageKey = xmlDoc.CreateTextNode(OrLblImageKey);
                    XmlText XmlTextCntrlOrLblRotationAngle = xmlDoc.CreateTextNode(OrLblRotationAngle);
                    XmlText XmlTextCntrlOrLblTextAlign = xmlDoc.CreateTextNode(OrLblTextAlign);
                    XmlText XmlTextCntrlOrLblTextDirection = xmlDoc.CreateTextNode(OrLblTextDirection);
                    XmlText XmlTextCntrlOrLblTextOrientation = xmlDoc.CreateTextNode(OrLblTextOrientation);
                    XmlText XmlTextCntrlOrLblUseMnemonic = xmlDoc.CreateTextNode(OrLblUseMnemonic);
                    XmlText XmlTextCntrlOrLblAutoEllipsis = xmlDoc.CreateTextNode(OrLblAutoEllipsis);
                    XmlText XmlTextCntrlOrLblUseCompatibleTextRendering = xmlDoc.CreateTextNode(OrLblUseCompatibleTextRendering);
                    XmlText XmlTextCntrlOrLblAutoSize = xmlDoc.CreateTextNode(OrLblAutoSize);


                    //Grid
                    XmlText ctlGridrowsBackColor = xmlDoc.CreateTextNode("");
                    XmlText ctlGridAlternaterowsBackColor = xmlDoc.CreateTextNode("");
                    XmlText ctlGridheaderColor = xmlDoc.CreateTextNode("");

                    if (dgvs != null) //if it is pictureBox, then it will not be null.
                    {
                        ctlGridrowsBackColor = xmlDoc.CreateTextNode(dgvs.BackgroundColor.Name);
                        ctlGridAlternaterowsBackColor = xmlDoc.CreateTextNode(dgvs.AlternatingRowsDefaultCellStyle.BackColor.Name);
                        ctlGridheaderColor = xmlDoc.CreateTextNode(dgvs.ColumnHeadersDefaultCellStyle.BackColor.Name);
                    }

                    XmlText txtPanelWidth = xmlDoc.CreateTextNode(PanelControl.Width.ToString());
                    XmlText txtPanelHeight = xmlDoc.CreateTextNode(PanelControl.Height.ToString());
                    #endregion
                    #region PutChildToParents


                    //GRid
                    PanelControl.Controls.GetChildIndex(p);
                    // append the nodes to the parentNode without the value
                    parentNode.AppendChild(CntrlType);
                    parentNode.AppendChild(CntrlName);
                    parentNode.AppendChild(CntrlText);
                    parentNode.AppendChild(CntrlFonts);
                    parentNode.AppendChild(CntrlLocationX);
                    parentNode.AppendChild(CntrlLocationY);
                    parentNode.AppendChild(CntrlSizeWidth);
                    parentNode.AppendChild(CntrlSizeHegith);
                    parentNode.AppendChild(CntrlPictureImage);
                    parentNode.AppendChild(CntrlBackColor);
                    parentNode.AppendChild(CntrlForeColor);
                    parentNode.AppendChild(CntrlRightToLeft);
                    parentNode.AppendChild(CntrlBackgroundImageLayout);
                    parentNode.AppendChild(CntrlCursor);
                    parentNode.AppendChild(CntrlAllowDrop);
                    parentNode.AppendChild(CntrlEnable);
                    parentNode.AppendChild(CntrlTabIndex);
                    parentNode.AppendChild(CntrlVisible);
                    parentNode.AppendChild(CntrlCausesValidation);
                    parentNode.AppendChild(CntrlAnchor);
                    parentNode.AppendChild(CntrlDock);
                    parentNode.AppendChild(CntrlMargin);
                    parentNode.AppendChild(CntrlPadding);
                    parentNode.AppendChild(CntrlMaximumSizeWidth);
                    parentNode.AppendChild(CntrlMaximumSizeHeight);
                    parentNode.AppendChild(CntrlMinimumSizeWidth);
                    parentNode.AppendChild(CntrlMinimumSizeHeight);
                    parentNode.AppendChild(CntrlUseWaitCursor);

                    parentNode.AppendChild(CntrlBCBoarderStyle);
                    parentNode.AppendChild(CntrlBCHorizontalAlignment);
                    parentNode.AppendChild(CntrlBCHorizontalTextAlignment);
                    parentNode.AppendChild(CntrlBCLookAndFeel);
                    parentNode.AppendChild(CntrlBCVerticalAlignment);
                    parentNode.AppendChild(CntrlBCVerticalTextAlignment);
                    parentNode.AppendChild(CntrlBCAutoModule);
                    parentNode.AppendChild(CntrlBCImeMode);
                    parentNode.AppendChild(CntrlBCModule);
                    parentNode.AppendChild(CntrlBCOrientation);
                    parentNode.AppendChild(CntrlBCShowText);
                    parentNode.AppendChild(CntrlBCSymbology);
                    parentNode.AppendChild(CntrlBCtabstop);
                    parentNode.AppendChild(CntrlBCbinaryData);
                    parentNode.AppendChild(CntrlBCAllowHtmlTextInToolTip);
                    parentNode.AppendChild(CntrlBCShowToolTips);
                    parentNode.AppendChild(CntrlBCToolTip);
                    parentNode.AppendChild(CntrlBCToolTipIconType);
                    parentNode.AppendChild(CntrlBCToolTipTitle);

                    parentNode.AppendChild(CntrlPBBoarderStyle);
                    parentNode.AppendChild(CntrlPBWaitOnLoad);
                    parentNode.AppendChild(CntrlPBSizeMode);
                    parentNode.AppendChild(CntrlPBImageLocation);

                    parentNode.AppendChild(CntrllblLabelBoarderStyle);
                    parentNode.AppendChild(CntrllblFlatStyle);
                    parentNode.AppendChild(CntrllblImageAlign);
                    parentNode.AppendChild(CntrllblImageIndex);
                    parentNode.AppendChild(CntrllblimageKey);
                    parentNode.AppendChild(CntrllblTextAlign);
                    parentNode.AppendChild(CntrllblUseMnemonic);
                    parentNode.AppendChild(CntrllblAutoEllipsis);
                    parentNode.AppendChild(CntrllblUseCompatibleTextRendering);
                    parentNode.AppendChild(CntrllblAutoSize);

                    parentNode.AppendChild(CntrlOrLblBorderStyle);
                    parentNode.AppendChild(CntrlOrLblFlatStyle);
                    parentNode.AppendChild(CntrlOrLblImageAlign);
                    parentNode.AppendChild(CntrlOrLblImageIndex);
                    parentNode.AppendChild(CntrlOrLblImageKey);
                    parentNode.AppendChild(CntrlOrLblRotationAngle);
                    parentNode.AppendChild(CntrlOrLblTextAlign);
                    parentNode.AppendChild(CntrlOrLblTextDirection);
                    parentNode.AppendChild(CntrlOrLblTextOrientation);
                    parentNode.AppendChild(CntrlOrLblUseMnemonic);
                    parentNode.AppendChild(CntrlOrLblAutoEllipsis);
                    parentNode.AppendChild(CntrlOrLblUseCompatibleTextRendering);
                    parentNode.AppendChild(CntrlOrLblAutoSize);

                    //for Grid
                    parentNode.AppendChild(gridrowsBackColor);
                    parentNode.AppendChild(gridAlternaterowsBackColor);
                    parentNode.AppendChild(gridheaderColor);
                    //grid
                    // save the value of the fields into the nodes


                    CntrlType.AppendChild(XmlTextCntrlType);
                    CntrlName.AppendChild(XmlTextCntrlNames);
                    CntrlText.AppendChild(XmlTextCntrlText);
                    CntrlFonts.AppendChild(XmlTextCntrlFont);
                    CntrlLocationX.AppendChild(XmlTextCntrlLocationX);
                    CntrlLocationY.AppendChild(XmlTextCntrlLocationY);
                    CntrlSizeWidth.AppendChild(XmlTextCntrlSizeWidth);
                    CntrlSizeHegith.AppendChild(XmlTextCntrlSizeHeight);
                    CntrlPictureImage.AppendChild(XmlTextCntrlPictureImage);
                    CntrlBackColor.AppendChild(XmlTextCntrlBackColor);
                    CntrlForeColor.AppendChild(XmlTextCntrlForeColor);
                    CntrlRightToLeft.AppendChild(XmlTextCntrlRightToLeft);
                    CntrlBackgroundImageLayout.AppendChild(XmlTextCntrlBackgroundImageLayout);
                    CntrlCursor.AppendChild(XmlTextCntrlCursor);
                    CntrlAllowDrop.AppendChild(XmlTextCntrlAllowDrop);
                    CntrlEnable.AppendChild(XmlTextCntrlEnable);
                    CntrlTabIndex.AppendChild(XmlTextCntrlTabIndex);
                    CntrlVisible.AppendChild(XmlTextCntrlVisible);
                    CntrlCausesValidation.AppendChild(XmlTextCntrlCausesValidation);
                    CntrlAnchor.AppendChild(XmlTextCntrlAnchor);
                    CntrlDock.AppendChild(XmlTextCntrlDock);
                    CntrlMargin.AppendChild(XmlTextCntrlMargin);
                    CntrlPadding.AppendChild(XmlTextCntrlPadding);
                    CntrlMaximumSizeWidth.AppendChild(XmlTextCntrlMaximumSizeWidth);
                    CntrlMaximumSizeHeight.AppendChild(XmlTextCntrlMaximumSizeHeight);
                    CntrlMinimumSizeWidth.AppendChild(XmlTextCntrlMinimumSizeWidth);
                    CntrlMinimumSizeHeight.AppendChild(XmlTextCntrlMinimumSizeHeight);
                    CntrlUseWaitCursor.AppendChild(XmlTextCntrlUseWaitCursor);

                    CntrlBCBoarderStyle.AppendChild(XmlTextCntrlBCBoarderStyle);
                    CntrlBCHorizontalAlignment.AppendChild(XmlTextCntrlBCHorizontalAlignment);
                    CntrlBCHorizontalTextAlignment.AppendChild(XmlTextCntrlBCHorizontalTextAlignment);
                    CntrlBCLookAndFeel.AppendChild(XmlTextCntrlBCLookAndFeel);
                    CntrlBCVerticalAlignment.AppendChild(XmlTextCntrlBCVerticalAlignment);
                    CntrlBCVerticalTextAlignment.AppendChild(XmlTextCntrlBCVerticalTextAlignment);
                    CntrlBCAutoModule.AppendChild(XmlTextCntrlBCAutoModule);
                    CntrlBCImeMode.AppendChild(XmlTextCntrlBCImeMode);
                    CntrlBCModule.AppendChild(XmlTextCntrlBCModule);
                    CntrlBCOrientation.AppendChild(XmlTextCntrlBCOrientation);
                    CntrlBCShowText.AppendChild(XmlTextCntrlBCShowText);
                    CntrlBCSymbology.AppendChild(XmlTextCntrlBCSymbology);
                    CntrlBCtabstop.AppendChild(XmlTextCntrlBCtabstop);
                    CntrlBCbinaryData.AppendChild(XmlTextCntrlBCbinaryData);
                    CntrlBCAllowHtmlTextInToolTip.AppendChild(XmlTextCntrlBCAllowHtmlTextInToolTip);
                    CntrlBCShowToolTips.AppendChild(XmlTextCntrlBCShowToolTips);
                    CntrlBCToolTip.AppendChild(XmlTextCntrlBCToolTip);
                    CntrlBCToolTipIconType.AppendChild(XmlTextCntrlBCToolTipIconType);
                    CntrlBCToolTipTitle.AppendChild(XmlTextCntrlBCToolTipTitle);

                    CntrlPBBoarderStyle.AppendChild(XmlTextCntrlPBBoarderStyle);
                    CntrlPBWaitOnLoad.AppendChild(XmlTextCntrlPBWaitOnLoad);
                    CntrlPBSizeMode.AppendChild(XmlTextCntrlPBSizeMode);
                    CntrlPBImageLocation.AppendChild(XmlTextCntrlPBImageLocation);

                    CntrllblLabelBoarderStyle.AppendChild(XmlTextCntrllblLabelBoarderStyle);
                    CntrllblFlatStyle.AppendChild(XmlTextCntrllblFlatStyle);
                    CntrllblImageAlign.AppendChild(XmlTextCntrllblImageAlign);
                    CntrllblImageIndex.AppendChild(XmlTextCntrllblImageIndex);
                    CntrllblimageKey.AppendChild(XmlTextCntrllblimageKey);
                    CntrllblTextAlign.AppendChild(XmlTextCntrllblTextAlign);
                    CntrllblUseMnemonic.AppendChild(XmlTextCntrllblUseMnemonic);
                    CntrllblAutoEllipsis.AppendChild(XmlTextCntrllblAutoEllipsis);
                    CntrllblUseCompatibleTextRendering.AppendChild(XmlTextCntrllblUseCompatibleTextRendering);
                    CntrllblAutoSize.AppendChild(XmlTextCntrllblAutoSize);


                    CntrlOrLblBorderStyle.AppendChild(XmlTextCntrlOrLblBorderStyle);
                    CntrlOrLblFlatStyle.AppendChild(XmlTextCntrlOrLblFlatStyle);
                    CntrlOrLblImageAlign.AppendChild(XmlTextCntrlOrLblImageAlign);
                    CntrlOrLblImageIndex.AppendChild(XmlTextCntrlOrLblImageIndex);
                    CntrlOrLblImageKey.AppendChild(XmlTextCntrlOrLblImageKey);
                    CntrlOrLblRotationAngle.AppendChild(XmlTextCntrlOrLblRotationAngle);
                    CntrlOrLblTextAlign.AppendChild(XmlTextCntrlOrLblTextAlign);
                    CntrlOrLblTextDirection.AppendChild(XmlTextCntrlOrLblTextDirection);
                    CntrlOrLblTextOrientation.AppendChild(XmlTextCntrlOrLblTextOrientation);
                    CntrlOrLblUseMnemonic.AppendChild(XmlTextCntrlOrLblUseMnemonic);
                    CntrlOrLblAutoEllipsis.AppendChild(XmlTextCntrlOrLblAutoEllipsis);
                    CntrlOrLblUseCompatibleTextRendering.AppendChild(XmlTextCntrlOrLblUseCompatibleTextRendering);
                    CntrlOrLblAutoSize.AppendChild(XmlTextCntrlOrLblAutoSize);

                    //for Grid
                    gridrowsBackColor.AppendChild(ctlGridrowsBackColor);
                    gridAlternaterowsBackColor.AppendChild(ctlGridAlternaterowsBackColor);
                    gridheaderColor.AppendChild(ctlGridheaderColor);
                    //grid
                    //nodePanelHeight.AppendChild(txtPanelHeight);
                }

                #endregion

            }
            catch (Exception)
            {
                flgSomethingWorng = true;
                MessageBox.Show("داده ها درست وارد نشده است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (flgSomethingWorng)
                xmlDoc.RemoveAll();
            return xmlDoc;
        }
        private static void putPicturesToEZD(Panel WhichPanel, bool CheckPrinter)
        {
            int EntityName = 0;
            bool flgNoPicExist = false;
            string TempFolder = Path.GetTempPath();
            if (!CheckPrinter)
            {
                if (Laser.ClearLibAllEntity() != Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                {
                    foreach (Control item in WhichPanel.Controls)
                    {
                        if (item.GetType().ToString() == "System.Windows.Forms.PictureBox")
                        {
                            PictureBox tmp = item as PictureBox;
                            string Result = Path.GetTempPath() + "Base64Bitmap\\";
                            if (!Directory.Exists(Result))
                                Directory.CreateDirectory(Result);
                            if (tmp.Image != null)
                            {
                                if (File.Exists(TempFolder + "Base64Bitmap//" + tmp.Name))
                                {
                                    string PicValue = EncryptionClass.EncryptionClass.Decrypt(File.ReadAllText(TempFolder + "Base64Bitmap//" + tmp.Name));
                                    double[] Location = convertPxTomm(tmp.Location);
                                    Image bmp = Base64ToImage(PicValue);
                                    string BmpName = Path.GetTempPath() + "LaserPrinterImagetmp\\" + "PictureTop_" + tmp.Name.Replace(".txt", "");
                                    bmp.Save(BmpName, ImageFormat.Bmp);
                                    float DPIX = bmp.HorizontalResolution;
                                    float DPIY = bmp.VerticalResolution;
                                    double[] PictureSize = WithDPIconvertPxTomm(bmp.Size, DPIX, DPIY);
                                    double NewYLocation = Location[1] + PictureSize[1];

                                    double NewCentreLocationX = Location[0] - 43;
                                    double NewCentreLocationY = (54 - NewYLocation) - 27;
                                    if (Laser.SetPenParam(3, 1, 300, 5, 1, 35000, 10, 10, 100, 300, 0, 4000, 500, 100, 0, 0.01, 0.100, false, 1, 0) != Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                                    {
                                        if (Laser.AddFileToLib(BmpName, "Picture" + EntityName.ToString(), NewCentreLocationX, NewCentreLocationY, 0, 0, 1, 3, false) != Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                                        {
                                            if (Laser.SetBitmapEntParam("Picture" + EntityName.ToString(), BmpName, 0, 0, 0, 0, 0, 600, false, 0) != Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                                                EntityName++;
                                            else
                                            {
                                                MessageBox.Show("لطفا از اتصال دستگاه اطمینان حاصل نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                flgNoPicExist = true;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("لطفا از اتصال دستگاه اطمینان حاصل نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            flgNoPicExist = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("لطفا از اتصال دستگاه اطمینان حاصل نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        flgNoPicExist = true;
                                        break;
                                    }
                                }


                            }
                            else
                            {
                                flgNoPicExist = true;
                                MessageBox.Show("لطفا تصویر مورد نظر را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("لطفا از اتصال دستگاه اطمینان حاصل نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    flgNoPicExist = true;

                }
            }
            else
            {

            }
        }
        private static Font StringToFont(string font)
        {
            string[] parts = font.Split(':');
            if (parts.Length != 3)
                throw new ArgumentException("Not a valid font string", "font");

            Font loadedFont = new Font(parts[0], float.Parse(parts[1]), (FontStyle)int.Parse(parts[2]));
            return loadedFont;
        }
        private static byte[] imageToByteArray(Bitmap bmp)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            return (byte[])converter.ConvertTo(bmp, typeof(byte[]));
        }
        private static string FontToString(Font font)
        {
            return font.FontFamily.Name + ":" + font.Size + ":" + (int)font.Style;
        }

        private void cmbPictureTOPPen_Click(object sender, EventArgs e)
        {
            cmbPictureTOPPen.Items.Clear();
            DirectoryInfo d = new DirectoryInfo(@"Pen");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.pen"); //Getting Text files

            foreach (FileInfo file in Files)
                cmbPictureTOPPen.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
            cmbPictureTOPPen.SelectedItem = cmbPictureTOPPen.Items[0];
        }

        private void cmbPictureBottomPen_Click(object sender, EventArgs e)
        {
            cmbPictureBottomPen.Items.Clear();
            DirectoryInfo d = new DirectoryInfo(@"Pen");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.pen"); //Getting Text files

            foreach (FileInfo file in Files)
                cmbPictureBottomPen.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
            cmbPictureBottomPen.SelectedItem = cmbPictureBottomPen.Items[0];
        }

        private void CbEnableTrack1_CheckedChanged(object sender, EventArgs e)
        {
            ActiveChCR();
        }

        private void CbEnableTrack2_CheckedChanged(object sender, EventArgs e)
        {
            ActiveChCR();
        }

        private void CbEnableTrack3_CheckedChanged(object sender, EventArgs e)
        {
            ActiveChCR();
        }
    }
}
