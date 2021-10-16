using SaraPrinterLaser.bl;
using SaraPrinterLaser.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaraPrinterLaser.LayoutDesignFolder
{
    public partial class frmCivilAviationInspectorCertificate : Form
    {
        private CivilAviationInspectorCertificate CardItems = new CivilAviationInspectorCertificate();
        Settings SettingsData = new Settings();
        public frmCivilAviationInspectorCertificate()
        {
            InitializeComponent();
        }
        private void BtnEnterDatabase_Click(object sender, EventArgs e)
        {
            new EnterData().ShowDialog();
        }
        private void FrmCivilAviationInspectorCertificate_Load(object sender, EventArgs e)
        {
            foreach (string item in LayoutDesignTools.DicNationality.Keys) CBNationality.Items.Add(item); CBNationality.SelectedItem = CBNationality.Items[0];
            cbINSPECTOR.Items.Add(CivilAviationInspectorCertificate.HEALTHINSPECTOR); cbINSPECTOR.Items.Add(CivilAviationInspectorCertificate.SAFETYINSPECTOR);
            cbINSPECTOR.Items.Add(CivilAviationInspectorCertificate.SECURITYINSPECTOR); cbINSPECTOR.SelectedItem = cbINSPECTOR.Items[0];
            cbSex.Items.Add(LayoutDesignTools.MaleSexuality); cbSex.Items.Add(LayoutDesignTools.FemaleSexuality); cbSex.SelectedItem = cbSex.Items[0];
            dl.ParametersSave Setting = new dl.ParametersSave();
            cmbDataSource.Items.Clear();
            DataSet dset = new dl.InfoModel().ListOfName();
            cmbDataSource.DataSource = dset.Tables[0];
            cmbDataSource.ValueMember = "ID";
            cmbDataSource.DisplayMember = "InfoName";
            cmbDataSource.SelectedIndex = -1;
            if (cmbDataSource.Items.Count > 0)
                cmbDataSource.SelectedItem = cmbDataSource.Items[0];

            CardItems.VariableTopItem_PersonalPicture = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Picture.CivilAviationInspectorCertificate_VariableTopItem_PersonalPicture);
            CardItems.VariableTopItem_SingniturePicture = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Picture.CivilAviationInspectorCertificate_VariableTopItem_SingniturePicture);
            CardItems.VariableBottomItem_IssuingAuthoritySingniturePicture = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Picture.CivilAviationInspectorCertificate_VariableBottomItem_IssuingAuthoritySingniturePicture);


            CardItems.LatinVariableTopItem_Name = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableTopItem_Name);
            CardItems.PersianVariableTopItem_Name = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_PersianVariableTopItem_Name);
            CardItems.LatinVariableTopItem_Nationality = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableTopItem_Nationality);
            CardItems.LatinVariableTopItem_DateOfBrith = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableTopItem_DateOfBrith);
            CardItems.LatinVariableTopItem_CardNo = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableTopItem_CardNo);
            CardItems.LatinVariableBottomItem_DateOfExpiry = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableBottomItem_DateOfExpiry);
            CardItems.PersianVariableBottomItem_DateOfExpiry = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_PersianVariableBottomItem_DateOfExpiry);

            CardItems.LatinVariableBottomItem_MRZ0 = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableBottomItem_MRZ0);
            CardItems.LatinVariableBottomItem_MRZ1 = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableBottomItem_MRZ1);
            CardItems.LatinVariableBottomItem_MRZ2 = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableBottomItem_MRZ2);




        }

        #region TextDoubleClickPropertyEvents
        private void LblName_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CivilAviationInspectorCertificate().LatinVariableTopItem_Name, LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableTopItem_Name, LayoutDesignTools.CardType.CivilAviationSafetyInspectorCertificate))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.LatinVariableTopItem_Name = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LblPersianNameSurname_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CivilAviationInspectorCertificate().PersianVariableTopItem_Name, LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_PersianVariableTopItem_Name, LayoutDesignTools.CardType.CivilAviationSafetyInspectorCertificate))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.PersianVariableTopItem_Name = form.TextProperty;
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
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CivilAviationInspectorCertificate().LatinVariableTopItem_Nationality, LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableTopItem_Nationality, LayoutDesignTools.CardType.CivilAviationSafetyInspectorCertificate))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.LatinVariableTopItem_Nationality = form.TextProperty;
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
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CivilAviationInspectorCertificate().LatinVariableTopItem_DateOfBrith, LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableTopItem_DateOfBrith, LayoutDesignTools.CardType.CivilAviationSafetyInspectorCertificate))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.LatinVariableTopItem_DateOfBrith = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LblCardNo_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CivilAviationInspectorCertificate().LatinVariableTopItem_CardNo, LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableTopItem_CardNo, LayoutDesignTools.CardType.CivilAviationSafetyInspectorCertificate))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.LatinVariableTopItem_CardNo = form.TextProperty;
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
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CivilAviationInspectorCertificate().LatinVariableBottomItem_DateOfExpiry, LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableBottomItem_DateOfExpiry, LayoutDesignTools.CardType.CivilAviationSafetyInspectorCertificate))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.LatinVariableBottomItem_DateOfExpiry = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LblPersianDateOfExpiry_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CivilAviationInspectorCertificate().PersianVariableBottomItem_DateOfExpiry, LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_PersianVariableBottomItem_DateOfExpiry, LayoutDesignTools.CardType.CivilAviationSafetyInspectorCertificate))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.PersianVariableBottomItem_DateOfExpiry = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LblMRZ0_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CivilAviationInspectorCertificate().LatinVariableBottomItem_MRZ0, LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableBottomItem_MRZ0, LayoutDesignTools.CardType.CivilAviationSafetyInspectorCertificate))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.LatinVariableBottomItem_MRZ0 = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LblMRZ1_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CivilAviationInspectorCertificate().LatinVariableBottomItem_MRZ1, LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableBottomItem_MRZ1, LayoutDesignTools.CardType.CivilAviationSafetyInspectorCertificate))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.LatinVariableBottomItem_MRZ1 = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LblMRZ2_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CivilAviationInspectorCertificate().LatinVariableBottomItem_MRZ2, LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableBottomItem_MRZ2, LayoutDesignTools.CardType.CivilAviationSafetyInspectorCertificate))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.LatinVariableBottomItem_MRZ2 = form.TextProperty;
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
                    using (var form = new frmEntryPictureProperty(cmbDataSource, CbActiveDatabase.Checked, new CivilAviationInspectorCertificate().VariableTopItem_PersonalPicture, LayoutDesignTools.ItemsOnTheCards_Picture.CivilAviationInspectorCertificate_VariableTopItem_PersonalPicture, LayoutDesignTools.CardType.CivilAviationSafetyInspectorCertificate))
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
                    using (var form = new frmEntryPictureProperty(cmbDataSource, CbActiveDatabase.Checked, new CivilAviationInspectorCertificate().VariableTopItem_SingniturePicture, LayoutDesignTools.ItemsOnTheCards_Picture.CivilAviationInspectorCertificate_VariableTopItem_SingniturePicture, LayoutDesignTools.CardType.CivilAviationSafetyInspectorCertificate))
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

                                    tools.RResizeImage(new Bitmap(Resources.Signiature), PibSignatureOfHolder.Width, PibSignatureOfHolder.Height);
                                }

                            }
                            else
                                tools.RResizeImage(new Bitmap(Resources.Signiature), PibSignatureOfHolder.Width, PibSignatureOfHolder.Height);
                        }
                    }
                }
            }
        }
        private void PibSignatureAuthority_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    LayoutDesignTools tools = new LayoutDesignTools();
                    using (var form = new frmEntryPictureProperty(cmbDataSource, CbActiveDatabase.Checked, new CivilAviationInspectorCertificate().VariableBottomItem_IssuingAuthoritySingniturePicture, LayoutDesignTools.ItemsOnTheCards_Picture.CivilAviationInspectorCertificate_VariableBottomItem_IssuingAuthoritySingniturePicture, LayoutDesignTools.CardType.CivilAviationSafetyInspectorCertificate))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableBottomItem_IssuingAuthoritySingniturePicture = form.PictureProperty;
                            if (!String.IsNullOrWhiteSpace(CardItems.VariableBottomItem_IssuingAuthoritySingniturePicture.EntryPicturePath))
                            {
                                try
                                {
                                    Bitmap bmp = new Bitmap(CardItems.VariableBottomItem_IssuingAuthoritySingniturePicture.EntryPicturePath);
                                    //  float DPIX = bmp.HorizontalResolution;
                                    //  float DPIY = bmp.VerticalResolution;
                                    //  double[] PictureSize = tools.WithDPIconvertPxTomm(bmp.Size, DPIX, DPIY);
                                    //     pbPersonalPic.Size = tools.WithDPIConvermmtoPx(PictureSize[0], PictureSize[1], DPIX, DPIY);
                                    PibSignatureAuthority.Image = tools.RResizeImage(bmp, PibSignatureAuthority.Width, PibSignatureAuthority.Height);

                                }
                                catch (Exception)
                                {

                                    PibSignatureAuthority.Image = tools.RResizeImage(new Bitmap(Resources.Signiature), PibSignatureOfHolder.Width, PibSignatureOfHolder.Height);
                                }

                            }
                            else
                                PibSignatureAuthority.Image = tools.RResizeImage(new Bitmap(Resources.Signiature), PibSignatureOfHolder.Width, PibSignatureOfHolder.Height);
                        }
                    }
                }
            }
        }
        #endregion
        #region MouseMoveEvent
        private void LblName_MouseMove(object sender, MouseEventArgs e)
        {
            lblName.BorderStyle = BorderStyle.FixedSingle;
        }

        private void FrmCivilAviationInspectorCertificate_MouseMove(object sender, MouseEventArgs e)
        {
            lblName.BorderStyle = BorderStyle.None;
            lblPersianNameSurname.BorderStyle = BorderStyle.None;
            lblNationality.BorderStyle = BorderStyle.None;
            lblDateOfBrith.BorderStyle = BorderStyle.None;
            lblCardNo.BorderStyle = BorderStyle.None;
            lblDateOfExpiry.BorderStyle = BorderStyle.None;
            lblPersianDateOfExpiry.BorderStyle = BorderStyle.None;
            lblMRZ0.BorderStyle = BorderStyle.None;
            lblMRZ1.BorderStyle = BorderStyle.None;
            lblMRZ2.BorderStyle = BorderStyle.None;

            pbPersonalPic.BorderStyle = BorderStyle.None;
            PibSignatureOfHolder.BorderStyle = BorderStyle.None;
            PibSignatureAuthority.BorderStyle = BorderStyle.None;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            lblName.BorderStyle = BorderStyle.None;
            lblPersianNameSurname.BorderStyle = BorderStyle.None;
            lblNationality.BorderStyle = BorderStyle.None;
            lblDateOfBrith.BorderStyle = BorderStyle.None;
            lblCardNo.BorderStyle = BorderStyle.None;
            lblDateOfExpiry.BorderStyle = BorderStyle.None;
            lblPersianDateOfExpiry.BorderStyle = BorderStyle.None;
            lblMRZ0.BorderStyle = BorderStyle.None;
            lblMRZ1.BorderStyle = BorderStyle.None;
            lblMRZ2.BorderStyle = BorderStyle.None;

            pbPersonalPic.BorderStyle = BorderStyle.None;
            PibSignatureOfHolder.BorderStyle = BorderStyle.None;
            PibSignatureAuthority.BorderStyle = BorderStyle.None;
        }

        private void LblPersianNameSurname_MouseMove(object sender, MouseEventArgs e)
        {
            lblPersianNameSurname.BorderStyle = BorderStyle.FixedSingle;
        }

        private void LblNationality_MouseMove(object sender, MouseEventArgs e)
        {
            lblNationality.BorderStyle = BorderStyle.FixedSingle;
        }

        private void LblDateOfBrith_MouseMove(object sender, MouseEventArgs e)
        {
            lblDateOfBrith.BorderStyle = BorderStyle.FixedSingle;
        }

        private void LblCardNo_MouseMove(object sender, MouseEventArgs e)
        {
            lblCardNo.BorderStyle = BorderStyle.FixedSingle;
        }

        private void PbPersonalPic_MouseMove(object sender, MouseEventArgs e)
        {
            pbPersonalPic.BorderStyle = BorderStyle.Fixed3D;
        }

        private void PibSignatureOfHolder_MouseMove(object sender, MouseEventArgs e)
        {
            PibSignatureOfHolder.BorderStyle = BorderStyle.Fixed3D;
        }

        private void PibSignatureAuthority_MouseMove(object sender, MouseEventArgs e)
        {
            PibSignatureAuthority.BorderStyle = BorderStyle.Fixed3D;
        }

        private void LblDateOfExpiry_MouseMove(object sender, MouseEventArgs e)
        {
            lblDateOfExpiry.BorderStyle = BorderStyle.FixedSingle;

        }

        private void LblPersianDateOfExpiry_MouseMove(object sender, MouseEventArgs e)
        {
            lblPersianDateOfExpiry.BorderStyle = BorderStyle.FixedSingle;

        }

        private void LblMRZ0_MouseMove(object sender, MouseEventArgs e)
        {
            lblMRZ0.BorderStyle = BorderStyle.FixedSingle;

        }

        private void LblMRZ1_MouseMove(object sender, MouseEventArgs e)
        {
            lblMRZ1.BorderStyle = BorderStyle.FixedSingle;

        }

        private void LblMRZ2_MouseMove(object sender, MouseEventArgs e)
        {
            lblMRZ2.BorderStyle = BorderStyle.FixedSingle;
        }
        private void PictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            lblName.BorderStyle = BorderStyle.None;
            lblPersianNameSurname.BorderStyle = BorderStyle.None;
            lblNationality.BorderStyle = BorderStyle.None;
            lblDateOfBrith.BorderStyle = BorderStyle.None;
            lblCardNo.BorderStyle = BorderStyle.None;
            lblDateOfExpiry.BorderStyle = BorderStyle.None;
            lblPersianDateOfExpiry.BorderStyle = BorderStyle.None;
            lblMRZ0.BorderStyle = BorderStyle.None;
            lblMRZ1.BorderStyle = BorderStyle.None;
            lblMRZ2.BorderStyle = BorderStyle.None;

            pbPersonalPic.BorderStyle = BorderStyle.None;
            PibSignatureOfHolder.BorderStyle = BorderStyle.None;
            PibSignatureAuthority.BorderStyle = BorderStyle.None;
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
                if (String.IsNullOrWhiteSpace(txtName.Text) || String.IsNullOrWhiteSpace(txtlblPersianNameSurname.Text) ||
                    String.IsNullOrWhiteSpace(dtpDateOfBrith.Text) || String.IsNullOrWhiteSpace(txtCardNo.Text) ||
                    String.IsNullOrWhiteSpace(dtpPersianDateOfExpiry.Text) || String.IsNullOrWhiteSpace(dtpDateOfExpiry.Text))
                    MessageBox.Show(StatusClass.Error_CardItemsTextIsEmpty, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

                else
                {
                    CardItems.LatinVariableTopItem_Name.EntryText = txtName.Text;
                    CardItems.PersianVariableTopItem_Name.EntryText = txtlblPersianNameSurname.Text;
                    CardItems.LatinVariableTopItem_Nationality.EntryText = CBNationality.SelectedItem.ToString();
                    CardItems.LatinVariableTopItem_DateOfBrith.EntryText = dtpDateOfBrith.Text;
                    CardItems.LatinVariableTopItem_CardNo.EntryText = txtCardNo.Text;
                    CardItems.PersianVariableBottomItem_DateOfExpiry.EntryText = dtpPersianDateOfExpiry.Text;
                    CardItems.LatinVariableBottomItem_DateOfExpiry.EntryText = dtpDateOfExpiry.Text;


                    CardItems.LatinVariableBottomItem_MRZ0.EntryText = lblMRZ0.Text;
                    CardItems.LatinVariableBottomItem_MRZ1.EntryText = lblMRZ1.Text;
                    CardItems.LatinVariableBottomItem_MRZ2.EntryText = lblMRZ2.Text;

                    CivilAviationInspectorCertificate.INSPECTORCardType cardType = CivilAviationInspectorCertificate.INSPECTORCardType.Safety;
                    if (CivilAviationInspectorCertificate.flgSAFETYINSPECTOR) cardType = CivilAviationInspectorCertificate.INSPECTORCardType.Safety;
                    else if (CivilAviationInspectorCertificate.flgSECURITYINSPECTOR) cardType = CivilAviationInspectorCertificate.INSPECTORCardType.Security;
                    else if (CivilAviationInspectorCertificate.flgHEALTHINSPECTOR) cardType = CivilAviationInspectorCertificate.INSPECTORCardType.Health;

                    ReturnStatus = new CivilAviationInspectorCertificate().ArrangeItemsOnUltraLicenceCard(CardItems, cardType, ref TextItems, ref ImagePath, ref ImageBase64);
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
            }
            else
            {



            }
            LayoutDesignTools.flgDefinedCardPrinted = false;
            btnSinglePrint.Enabled = true;
        }

        private void CbINSPECTOR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbINSPECTOR.SelectedItem.ToString() == CivilAviationInspectorCertificate.HEALTHINSPECTOR)
            {
                CivilAviationInspectorCertificate.flgHEALTHINSPECTOR = true;
                CivilAviationInspectorCertificate.flgSAFETYINSPECTOR = false;
                CivilAviationInspectorCertificate.flgSECURITYINSPECTOR = false;
            }
            else if (cbINSPECTOR.SelectedItem.ToString() == CivilAviationInspectorCertificate.SAFETYINSPECTOR)
            {
                CivilAviationInspectorCertificate.flgHEALTHINSPECTOR = false;
                CivilAviationInspectorCertificate.flgSAFETYINSPECTOR = true;
                CivilAviationInspectorCertificate.flgSECURITYINSPECTOR = false;
            }
            else if (cbINSPECTOR.SelectedItem.ToString() == CivilAviationInspectorCertificate.SECURITYINSPECTOR)
            {
                CivilAviationInspectorCertificate.flgHEALTHINSPECTOR = false;
                CivilAviationInspectorCertificate.flgSAFETYINSPECTOR = false;
                CivilAviationInspectorCertificate.flgSECURITYINSPECTOR = true;
            }
        }
    }
}
