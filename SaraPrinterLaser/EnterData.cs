using Bytescout.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaraPrinterLaser
{
    public partial class EnterData : Form
    {
        public string PictureFolderPath { get; set; }
        public string PituresFormat { get; set; }
        public string ExcelFolderPath { get; set; }
        public string ExcelFileName { get; set; }
        double AllCellCount = 0;
        int ProgressbarCounter = 0;
        bool FlgDoneProgress = false;
        public EnterData()
        {
            InitializeComponent();
        }

        private void lblAddress_Click(object sender, EventArgs e)
        {

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog opg = new OpenFileDialog();
            opg.RestoreDirectory = true;
            opg.Filter = "Excel File|*.xlsx";
            opg.Title = "لطفا فایل اکسل پایگاه داده را انتخاب کنید";
            if (opg.ShowDialog() == DialogResult.OK)
            {
                txtAddress.Text = opg.FileName;
                ExcelFolderPath = txtAddress.Text;
                txtName.Text = opg.SafeFileName;
                ExcelFileName = opg.SafeFileName;
                if (!String.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    btnCheck.Visible = true;
                    txtSheetName.Visible = true;
                    lblSheetName.Visible = true;
                    MessageBox.Show("پس از وارد کردن نام شیت دکمه بررسی اطلاعات را فشار دهید.", "پیغام",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("لطفا آدرس فایل اکسل را به درستی وارد نمایید", "خطا", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

            }

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                Spreadsheet document = new Spreadsheet();
                document.LoadFromFile(txtAddress.Text, CacheType.File);
                // Get worksheet by name
                Worksheet worksheet = document.Workbook.Worksheets.ByName(txtSheetName.Text);
                bool checkclm = true;
                int count = 0;
                while (checkclm)
                {

                    AllCellCount = ((worksheet.NotEmptyColumnMax + 1) - worksheet.NotEmptyColumnMin)
                        * ((worksheet.NotEmptyRowMax + 1) - worksheet.NotEmptyRowMin);
                    PbarCount.Maximum = (int)AllCellCount;
                    Cell sanjcell = worksheet.Cell(0, count);
                    count++;
                    if (!String.IsNullOrWhiteSpace(sanjcell.ValueAsString))
                    {
                        nudCount.Value++;
                        lblClm.Text += sanjcell.ValueAsString;
                        lblClm.Text += "ß";

                        TextBox aa = new TextBox();
                        aa.Text = sanjcell.ValueAsString;
                        aa.ReadOnly = true;
                        flpItems.Controls.Add(aa);
                    }
                    else
                    {
                        checkclm = false;
                    }
                }
                if (MessageBox.Show("آیا مایل به ورود پوشه عکس هستید؟", "سوال", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    btnEnterPictureFolder.Visible = true;
                    txtPictureFolderPath.Visible = true;
                    lblPictureFolderPath.Visible = true;
                    MessageBox.Show("لطفا پوشه عکس را وارد نمایید ", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //  btnEnterPictureFolder.Visible = true;
                    txtPictureFolderPath.Visible = true;
                    txtPictureFolderPath.Text = "Nothing";
                    PictureFolderPath = "Nothing";
                    PituresFormat = "Nothing";
                    lblPictureFolderPath.Visible = true;
                    // btnPictureFormat.Visible = true;
                    cmbPictureFormat.Visible = true;
                    lblPictureFormat.Visible = true;
                    cmbPictureFormat.SelectedItem = cmbPictureFormat.Items[0];
                    btnSave.Visible = true;
                    txtName.Visible = true;
                    lblSave.Visible = true;
                    MessageBox.Show("پس از وارد کردن نام پایگاه داده دکمه ذخیره اطلاعات را فشار دهید.", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information);



                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                string aa = ex.ToString();
                MessageBox.Show("قبل از ورود اطلاعات فایل مورد نظر را ذخیره کرده و به طور کامل آن را ببندید . در ضمن از درست بودن اسم شیت اطمینان حاصل نمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((new dl.InfoModel().CheckInfoName(txtName.Text)) == 0)
            {
                SaveData.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("نام  تکراری است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPictureFormat_Click(object sender, EventArgs e)
        {
            bool FailFormatDetect = false;
            PituresFormat = "." + cmbPictureFormat.SelectedItem.ToString();
            if (!String.IsNullOrWhiteSpace(PituresFormat))
            {
                System.IO.DirectoryInfo di2 = new DirectoryInfo(PictureFolderPath);
                foreach (FileInfo file in di2.GetFiles())
                {
                    if (!file.Name.Contains(PituresFormat))
                    {
                        FailFormatDetect = true;
                        break;
                    }
                }
                if (!FailFormatDetect)
                {
                    btnSave.Visible = true;
                    txtName.Visible = true;
                    lblSave.Visible = true;
                    MessageBox.Show("پس از وارد کردن نام پایگاه داده دکمه ذخیره اطلاعات را فشار دهید.", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("در فولدر انتخاب شده حداقل یک عکس موجود است که با فرمت انتخابی مطابقت ندارد", "خطا",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void btnEnterPictureFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.Description = "لطفا پوشه عکس را وارد کنید";

            FBD.ShowNewFolderButton = true;
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                PictureFolderPath = FBD.SelectedPath;
                txtPictureFolderPath.Text = PictureFolderPath;
                if (!String.IsNullOrWhiteSpace(txtPictureFolderPath.Text))
                {
                    btnPictureFormat.Visible = true;
                    cmbPictureFormat.Visible = true;
                    lblPictureFormat.Visible = true;
                    cmbPictureFormat.SelectedItem = cmbPictureFormat.Items[0];
                    MessageBox.Show("لطفا فرمت عکس ها را وارد نمایید", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("لطفا آدرس پوشه عکس را به درستی وارد نمایید", "خطا", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

            }

        }

        private void EnterData_Load(object sender, EventArgs e)
        {

        }

        public void TmrProgressBar_Tick(object sender, EventArgs e)
        {
            PbarCount.Value1 = ProgressbarCounter;
            PbarCount.Text = string.Format("{0}/{1}", PbarCount.Value1, PbarCount.Maximum);
            PbarCount.Refresh();
            PbarCount.Update();
            PbarCount.ResumeUpdate();
            if (FlgDoneProgress)
            {
                btnCheck.Visible = false;
                txtSheetName.Visible = false;
                lblSheetName.Visible = false;
                btnSave.Visible = false;
                txtName.Visible = false;
                lblSave.Visible = false;
                tmrProgressBar.Stop();
                MessageBox.Show("اطلاعات با موفقیت ثبت شد.", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void SaveData_DoWork(object sender, DoWorkEventArgs e)
        {
            tmrProgressBar.Start();
            Spreadsheet document = new Spreadsheet();
            document.LoadFromFile(txtAddress.Text);
            Worksheet worksheet = document.Workbook.Worksheets.ByName(txtSheetName.Text);
            System.IO.StreamReader file = new System.IO.StreamReader(txtAddress.Text);
            string line = file.ReadLine();
            file.Close();

            new dl.InfoModel().Insert(txtName.Text,
                lblClm.Text.Substring(0, lblClm.Text.Length - 2),
                PictureFolderPath,
                PituresFormat);
            int InfoId = new dl.InfoModel().GetInfoID(txtName.Text);
            bool checkfinish = true;

            int rowCount = 1;
            while (checkfinish)
            {

                string rows = "";
                for (int i = 0; i < nudCount.Value; i++)
                {
                    ProgressbarCounter++;


                    tmrProgressBar.Start();

                    rows += worksheet.Cell(rowCount, i).ValueAsString;
                    rows += "ß";
                }
                if (rows.Length > nudCount.Value + 1)
                {
                    rows = rows.Substring(0, rows.Length - 1);
                    new dl.InfoData().Insert(rows, InfoId);
                }
                else
                {
                    checkfinish = false;
                }

                rowCount++;


            }
            FlgDoneProgress = true;

        }
    }
}
