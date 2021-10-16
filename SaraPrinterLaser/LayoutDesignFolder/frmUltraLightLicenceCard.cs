using SaraPrinterLaser.bl;
using SaraPrinterLaser.Hardware;
using SaraPrinterLaser.Properties;
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

namespace SaraPrinterLaser.LayoutDesignFolder
{
    public partial class frmUltraLightLicenceCard : Form
    {
        private UltraLightLicenceCard CardItems = new UltraLightLicenceCard();
        Settings SettingsData = new Settings();
        public frmUltraLightLicenceCard()
        {
            InitializeComponent();
        }

        private void FrmUltraLightLicenceCard_Load(object sender, EventArgs e)
        {

            //   StatusClass ReturnSatus = new StatusClass();
            //    ReturnSatus = Laser.DeviceInitialize(System.IO.Path.GetDirectoryName(Application.ExecutablePath));


            dl.ParametersSave Setting = new dl.ParametersSave();
            cmbDataSource.Items.Clear();
            DataSet dset = new dl.InfoModel().ListOfName();
            cmbDataSource.DataSource = dset.Tables[0];
            cmbDataSource.ValueMember = "ID";
            cmbDataSource.DisplayMember = "InfoName";
            cmbDataSource.SelectedIndex = -1;
            if (cmbDataSource.Items.Count > 0)
                cmbDataSource.SelectedItem = cmbDataSource.Items[0];

            CardItems.VariableTopItem_PersonalPicture = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Picture.UltraLightLicenceCard_VariableTopItem_PersonalPicture);
            CardItems.VariableTopItem_SingniturePicture = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Picture.UltraLightLicenceCard_VariableTopItem_SingniturePicture);


            CardItems.VariableTopItem_Number = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_Number);
            CardItems.VariableTopItem_Name = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_Name);
            CardItems.VariableTopItem_DateOfBrith = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_DateOfBrith);
            CardItems.VariableTopItem_Nationality = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_Nationality);
            CardItems.VariableTopItem_DateOfIssue = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_DateOfIssue);
            CardItems.VariableTopItem_DateOfExpiry = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_DateOfExpiry);
            CardItems.VariableTopItem_Authority = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_Authority);
            CardItems.VariableBottomItem_Remarks = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableBottomItem_Remarks);
            CardItems.VariableBottomItem_Ratings = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableBottomItem_Ratings);
        }

        #region MouseMoveEvent
        private void LblNumber_MouseMove(object sender, MouseEventArgs e)
        {
            lblNumber.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LblName_MouseMove(object sender, MouseEventArgs e)
        {
            lblName.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LblDateOfBrith_MouseMove(object sender, MouseEventArgs e)
        {
            lblDateOfBrith.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LblNationality_MouseMove(object sender, MouseEventArgs e)
        {
            lblNationality.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LblDateOfIssue_MouseMove(object sender, MouseEventArgs e)
        {
            lblDateOfIssue.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LblDateOfExpiry_MouseMove(object sender, MouseEventArgs e)
        {
            lblDateOfExpiry.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LblAuthority_MouseMove(object sender, MouseEventArgs e)
        {
            lblAuthority.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LblRemrks_MouseMove(object sender, MouseEventArgs e)
        {
            lblRemrks.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LblRatings_MouseMove(object sender, MouseEventArgs e)
        {
            lblRatings.BorderStyle = BorderStyle.FixedSingle;
        }
        private void PbPersonalPic_MouseMove(object sender, MouseEventArgs e)
        {
            pbPersonalPic.BorderStyle = BorderStyle.Fixed3D;
        }
        private void PibSignatureOfHolder_MouseMove(object sender, MouseEventArgs e)
        {
            PibSignatureOfHolder.BorderStyle = BorderStyle.Fixed3D;
        }
        private void PbCardTopLayout_MouseMove(object sender, MouseEventArgs e)
        {
            lblNumber.BorderStyle = BorderStyle.None;
            lblName.BorderStyle = BorderStyle.None;
            lblDateOfBrith.BorderStyle = BorderStyle.None;
            lblNationality.BorderStyle = BorderStyle.None;
            lblDateOfIssue.BorderStyle = BorderStyle.None;
            lblDateOfExpiry.BorderStyle = BorderStyle.None;
            lblAuthority.BorderStyle = BorderStyle.None;
            lblRemrks.BorderStyle = BorderStyle.None;
            lblRatings.BorderStyle = BorderStyle.None;
            pbPersonalPic.BorderStyle = BorderStyle.None;
            PibSignatureOfHolder.BorderStyle = BorderStyle.None;
        }
        private void PbCardBottomLayout_MouseMove(object sender, MouseEventArgs e)
        {
            lblNumber.BorderStyle = BorderStyle.None;
            lblName.BorderStyle = BorderStyle.None;
            lblDateOfBrith.BorderStyle = BorderStyle.None;
            lblNationality.BorderStyle = BorderStyle.None;
            lblDateOfIssue.BorderStyle = BorderStyle.None;
            lblDateOfExpiry.BorderStyle = BorderStyle.None;
            lblAuthority.BorderStyle = BorderStyle.None;
            lblRemrks.BorderStyle = BorderStyle.None;
            lblRatings.BorderStyle = BorderStyle.None;
            pbPersonalPic.BorderStyle = BorderStyle.None;
            PibSignatureOfHolder.BorderStyle = BorderStyle.None;
        }
        private void FrmUltraLightLicenceCard_MouseMove(object sender, MouseEventArgs e)
        {
            lblNumber.BorderStyle = BorderStyle.None;
            lblName.BorderStyle = BorderStyle.None;
            lblDateOfBrith.BorderStyle = BorderStyle.None;
            lblNationality.BorderStyle = BorderStyle.None;
            lblDateOfIssue.BorderStyle = BorderStyle.None;
            lblDateOfExpiry.BorderStyle = BorderStyle.None;
            lblAuthority.BorderStyle = BorderStyle.None;
            lblRemrks.BorderStyle = BorderStyle.None;
            lblRatings.BorderStyle = BorderStyle.None;
            pbPersonalPic.BorderStyle = BorderStyle.None;
            PibSignatureOfHolder.BorderStyle = BorderStyle.None;
        }

        #endregion
        #region TextDoubleClickPropertyEvents
        private void LblNumber_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new UltraLightLicenceCard().VariableTopItem_Number, LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_Number, LayoutDesignTools.CardType.UltraLightLicence))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableTopItem_Number = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LblName_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new UltraLightLicenceCard().VariableTopItem_Name, LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_Name, LayoutDesignTools.CardType.UltraLightLicence))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableTopItem_Name = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LblDateOfBrith_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new UltraLightLicenceCard().VariableTopItem_DateOfBrith, LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_DateOfBrith, LayoutDesignTools.CardType.UltraLightLicence))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableTopItem_DateOfBrith = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LblNationality_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new UltraLightLicenceCard().VariableTopItem_Nationality, LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_Nationality, LayoutDesignTools.CardType.UltraLightLicence))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableTopItem_Nationality = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LblDateOfIssue_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new UltraLightLicenceCard().VariableTopItem_DateOfIssue, LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_DateOfIssue, LayoutDesignTools.CardType.UltraLightLicence))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableTopItem_DateOfIssue = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LblDateOfExpiry_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new UltraLightLicenceCard().VariableTopItem_DateOfExpiry, LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_DateOfExpiry, LayoutDesignTools.CardType.UltraLightLicence))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableTopItem_DateOfExpiry = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LblAuthority_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new UltraLightLicenceCard().VariableTopItem_Authority, LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_Authority, LayoutDesignTools.CardType.UltraLightLicence))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableTopItem_Authority = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LblRemrks_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new UltraLightLicenceCard().VariableBottomItem_Remarks, LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableBottomItem_Remarks, LayoutDesignTools.CardType.UltraLightLicence))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableBottomItem_Remarks = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LblRatings_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new UltraLightLicenceCard().VariableBottomItem_Remarks, LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableBottomItem_Remarks, LayoutDesignTools.CardType.UltraLightLicence))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableBottomItem_Remarks = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
        #region PictureDoubleClickPropertyEvents
        private void PbPersonalPic_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    LayoutDesignTools tools = new LayoutDesignTools();
                    using (var form = new frmEntryPictureProperty(cmbDataSource, CbActiveDatabase.Checked, new UltraLightLicenceCard().VariableTopItem_PersonalPicture, LayoutDesignTools.ItemsOnTheCards_Picture.UltraLightLicenceCard_VariableTopItem_PersonalPicture, LayoutDesignTools.CardType.UltraLightLicence))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableTopItem_PersonalPicture = form.PictureProperty;
                            if (!String.IsNullOrWhiteSpace(CardItems.VariableTopItem_PersonalPicture.EntryPicturePath))
                            {
                                try
                                {
                                    Bitmap bmp = new Bitmap(CardItems.VariableTopItem_PersonalPicture.EntryPicturePath);
                                    //  float DPIX = bmp.HorizontalResolution;
                                    //  float DPIY = bmp.VerticalResolution;
                                    //  double[] PictureSize = tools.WithDPIconvertPxTomm(bmp.Size, DPIX, DPIY);
                                    //     pbPersonalPic.Size = tools.WithDPIConvermmtoPx(PictureSize[0], PictureSize[1], DPIX, DPIY);
                                    pbPersonalPic.Image = tools.RResizeImage(bmp, pbPersonalPic.Width, pbPersonalPic.Height);



                                }
                                catch (Exception)
                                {

                                    pbPersonalPic.Image = new Bitmap(Resources.PersonalPic);
                                }

                            }
                            else
                                pbPersonalPic.Image = new Bitmap(Resources.PersonalPic);
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void PibSignatureOfHolder_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    LayoutDesignTools tools = new LayoutDesignTools();
                    using (var form = new frmEntryPictureProperty(cmbDataSource, CbActiveDatabase.Checked, new UltraLightLicenceCard().VariableTopItem_SingniturePicture, LayoutDesignTools.ItemsOnTheCards_Picture.UltraLightLicenceCard_VariableTopItem_SingniturePicture, LayoutDesignTools.CardType.UltraLightLicence))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableTopItem_SingniturePicture = form.PictureProperty;
                            if (!String.IsNullOrWhiteSpace(CardItems.VariableTopItem_SingniturePicture.EntryPicturePath))
                            {
                                try
                                {
                                    Bitmap bmp = new Bitmap(CardItems.VariableTopItem_SingniturePicture.EntryPicturePath);
                                    //  float DPIX = bmp.HorizontalResolution;
                                    //  float DPIY = bmp.VerticalResolution;
                                    //  double[] PictureSize = tools.WithDPIconvertPxTomm(bmp.Size, DPIX, DPIY);
                                    //     pbPersonalPic.Size = tools.WithDPIConvermmtoPx(PictureSize[0], PictureSize[1], DPIX, DPIY);
                                    PibSignatureOfHolder.Image = tools.RResizeImage(bmp, PibSignatureOfHolder.Width, PibSignatureOfHolder.Height);



                                }
                                catch (Exception)
                                {

                                    PibSignatureOfHolder.Image = tools.RResizeImage(new Bitmap(Resources.Signiature), PibSignatureOfHolder.Width, PibSignatureOfHolder.Height);
                                }

                            }
                            else
                                PibSignatureOfHolder.Image = tools.RResizeImage(new Bitmap(Resources.Signiature), PibSignatureOfHolder.Width, PibSignatureOfHolder.Height);
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        private void BtnSinglePrint_Click(object sender, EventArgs e)
        {
            string[] TextItems = new string[0];
            string[] ImagePath = new string[2];
            string[] ImageBase64 = new string[2];
            StatusClass ReturnStatus = new StatusClass()
            {
                ResponseReturnStatus = StatusClass.ResponseStatus.Fail,
                ReturnDescription = ""
            };
            btnSinglePrint.Enabled = false;
            LayoutDesignTools.flgDefinedCardPrinted = true;
            if (!CbActiveDatabase.Checked)
            {
                ReturnStatus = new UltraLightLicenceCard().ArrangeItemsOnUltraLicenceCard(CardItems, ref TextItems, ref ImagePath, ref ImageBase64);
                if (ReturnStatus.ResponseReturnStatus == StatusClass.ResponseStatus.Ok)
                {
                    FileWork.readstate();
                    if (FileWork.stateMashin == FileWork.StateOfmashin.Ready || FileWork.stateMashin == FileWork.StateOfmashin.Printed)
                    {
                        int holder = 1;
                        string[] MachineConfig = FileWork.ReadAllSecureConfig();
                        if (MachineConfig[3] == "1")
                        {
                            holder = 1;
                        }
                        else if (MachineConfig[3] == "2")
                        {
                            holder = 2;
                        }
                        try
                        {
                            if (int.Parse(txtPrintNumber.Text) > 0)
                            {
                                string PenTempName = Path.GetFileNameWithoutExtension(new DirectoryInfo(@"Pen").GetFiles("*.pen")[0].Name);
                                PrintingPage prt = new PrintingPage(false, false, false, false, false, true, false, holder, "", "", "", PenTempName, PenTempName, PenTempName, PenTempName, int.Parse(txtPrintNumber.Text));
                                prt.ShowDialog();
                                for (int i = 0; i < ImageBase64.Length; i++) ImageBase64[i] = "";
                                new dl.PrintedCardReport().SetCaoCardPrintedReport(LayoutDesignTools.CardType.UltraLightLicence, TextItems, ImagePath, ImageBase64, bl.Auth.UserName);
                            }
                            else
                                MessageBox.Show("لطفا تعداد چاپ را تعیین نمایید", StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            string ErrorException = ex.ToString();
                            MessageBox.Show("خطای زیر اتفاق افتاده است " + '\n' + ErrorException, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        MessageBox.Show("پرینتر در حال پرینت می باشد.لطفاصبر کنید و یا از دکمه پاکسازی استفاده کنید.", StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {



            }
            LayoutDesignTools.flgDefinedCardPrinted = false;
            btnSinglePrint.Enabled = true;
        }
        private void CbActiveDatabase_CheckedChanged(object sender, EventArgs e)
        {
            if (CbActiveDatabase.Checked)
            {
                lblPrintQuntity.Visible = false;
                txtPrintNumber.Visible = false;
            }
            else
            {
                lblPrintQuntity.Visible = true;
                txtPrintNumber.Visible = true;
            }
        }
    }
}
