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
    public partial class frmCrewMemberCertificate_C_M_C_ : Form
    {
        public frmCrewMemberCertificate_C_M_C_()
        {
            InitializeComponent();
        }
        private CrewMemberCertificate_C_M_C_ CardItems = new CrewMemberCertificate_C_M_C_();
        Settings SettingsData = new Settings();



        #region MouseMoveEvent
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
        private void LblSurname_MouseMove(object sender, MouseEventArgs e)
        {
            lblSurname.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LblGivenName_MouseMove(object sender, MouseEventArgs e)
        {
            lblGivenName.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LblSex_MouseMove(object sender, MouseEventArgs e)
        {
            lblSex.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LblNationality_MouseMove(object sender, MouseEventArgs e)
        {
            lblNationality.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LblDateOfBrith_MouseMove(object sender, MouseEventArgs e)
        {
            lblDateOfBrith.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LblEmployedby_MouseMove(object sender, MouseEventArgs e)
        {
            lblEmployedby.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LblOccupation_MouseMove(object sender, MouseEventArgs e)
        {
            lblOccupation.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LblDocNo_MouseMove(object sender, MouseEventArgs e)
        {
            lblDocNo.BorderStyle = BorderStyle.FixedSingle;
        }
        private void LblDateOfExpiry_MouseMove(object sender, MouseEventArgs e)
        {
            lblDateOfExpiry.BorderStyle = BorderStyle.FixedSingle;
        }
        private void PbPersonalPic_MouseMove(object sender, MouseEventArgs e)
        {
            pbPersonalPic.BorderStyle = BorderStyle.Fixed3D;
        }
        private void PibSignatureOfHolder_MouseMove(object sender, MouseEventArgs e)
        {
            PibSignatureOfHolder.BorderStyle = BorderStyle.Fixed3D;
        }
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            lblMRZ0.BorderStyle = BorderStyle.None;
            lblMRZ1.BorderStyle = BorderStyle.None;
            lblMRZ2.BorderStyle = BorderStyle.None;
            lblSurname.BorderStyle = BorderStyle.None;
            lblGivenName.BorderStyle = BorderStyle.None;
            lblSex.BorderStyle = BorderStyle.None;
            lblNationality.BorderStyle = BorderStyle.None;
            lblDateOfBrith.BorderStyle = BorderStyle.None;
            lblEmployedby.BorderStyle = BorderStyle.None;
            lblOccupation.BorderStyle = BorderStyle.None;
            lblDocNo.BorderStyle = BorderStyle.None;
            lblDateOfExpiry.BorderStyle = BorderStyle.None;
            pbPersonalPic.BorderStyle = BorderStyle.None;
            PibSignatureOfHolder.BorderStyle = BorderStyle.None;
        }
        private void PictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            lblMRZ0.BorderStyle = BorderStyle.None;
            lblMRZ1.BorderStyle = BorderStyle.None;
            lblMRZ2.BorderStyle = BorderStyle.None;
            lblSurname.BorderStyle = BorderStyle.None;
            lblGivenName.BorderStyle = BorderStyle.None;
            lblSex.BorderStyle = BorderStyle.None;
            lblNationality.BorderStyle = BorderStyle.None;
            lblDateOfBrith.BorderStyle = BorderStyle.None;
            lblEmployedby.BorderStyle = BorderStyle.None;
            lblOccupation.BorderStyle = BorderStyle.None;
            lblDocNo.BorderStyle = BorderStyle.None;
            lblDateOfExpiry.BorderStyle = BorderStyle.None;
            pbPersonalPic.BorderStyle = BorderStyle.None;
            PibSignatureOfHolder.BorderStyle = BorderStyle.None;
        }
        private void FrmCrewMemberCertificate_C_M_C__MouseMove(object sender, MouseEventArgs e)
        {
            lblMRZ0.BorderStyle = BorderStyle.None;
            lblMRZ1.BorderStyle = BorderStyle.None;
            lblMRZ2.BorderStyle = BorderStyle.None;
            lblSurname.BorderStyle = BorderStyle.None;
            lblGivenName.BorderStyle = BorderStyle.None;
            lblSex.BorderStyle = BorderStyle.None;
            lblNationality.BorderStyle = BorderStyle.None;
            lblDateOfBrith.BorderStyle = BorderStyle.None;
            lblEmployedby.BorderStyle = BorderStyle.None;
            lblOccupation.BorderStyle = BorderStyle.None;
            lblDocNo.BorderStyle = BorderStyle.None;
            lblDateOfExpiry.BorderStyle = BorderStyle.None;
            pbPersonalPic.BorderStyle = BorderStyle.None;
            PibSignatureOfHolder.BorderStyle = BorderStyle.None;
        }
        #endregion
        #region TextDoubleClickPropertyEvents
        private void LblSurname_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CrewMemberCertificate_C_M_C_().VariableTopItem_Surname, LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Surname, LayoutDesignTools.CardType.CrewMemberCertificate_C_M_C_))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableTopItem_Surname = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LblGivenName_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CrewMemberCertificate_C_M_C_().VariableTopItem_GivenName, LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_GivenName, LayoutDesignTools.CardType.CrewMemberCertificate_C_M_C_))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableTopItem_GivenName = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LblSex_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CrewMemberCertificate_C_M_C_().VariableTopItem_Sex, LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Sex, LayoutDesignTools.CardType.CrewMemberCertificate_C_M_C_))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableTopItem_Sex = form.TextProperty;
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
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CrewMemberCertificate_C_M_C_().VariableTopItem_Nationality, LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Nationality, LayoutDesignTools.CardType.CrewMemberCertificate_C_M_C_))
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

        private void LblDateOfBrith_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CrewMemberCertificate_C_M_C_().VariableTopItem_DateOfBrith, LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_DateOfBrith, LayoutDesignTools.CardType.CrewMemberCertificate_C_M_C_))
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

        private void LblEmployedby_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CrewMemberCertificate_C_M_C_().VariableTopItem_Employedby, LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Employedby, LayoutDesignTools.CardType.CrewMemberCertificate_C_M_C_))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableTopItem_Employedby = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LblOccupation_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CrewMemberCertificate_C_M_C_().VariableTopItem_Occupation, LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Occupation, LayoutDesignTools.CardType.CrewMemberCertificate_C_M_C_))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableTopItem_Occupation = form.TextProperty;
                        }
                    }
                }
            }
            else
                MessageBox.Show(StatusClass.Error_UserAccsessFail, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LblDocNo_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CrewMemberCertificate_C_M_C_().VariableTopItem_DocNo, LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_DocNo, LayoutDesignTools.CardType.CrewMemberCertificate_C_M_C_))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableTopItem_DocNo = form.TextProperty;
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
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CrewMemberCertificate_C_M_C_().VariableTopItem_DateOfExpiry, LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_DateOfExpiry, LayoutDesignTools.CardType.CrewMemberCertificate_C_M_C_))
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

        private void LblMRZ0_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CrewMemberCertificate_C_M_C_().VariableBottomItem_MRZ0, LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableBottomItem_MRZ0, LayoutDesignTools.CardType.CrewMemberCertificate_C_M_C_))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableBottomItem_MRZ0 = form.TextProperty;
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
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CrewMemberCertificate_C_M_C_().VariableBottomItem_MRZ1, LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableBottomItem_MRZ1, LayoutDesignTools.CardType.CrewMemberCertificate_C_M_C_))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableBottomItem_MRZ1 = form.TextProperty;
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
                    using (var form = new frmEntryTextProperty(cmbDataSource, CbActiveDatabase.Checked, new CrewMemberCertificate_C_M_C_().VariableBottomItem_MRZ2, LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableBottomItem_MRZ2, LayoutDesignTools.CardType.CrewMemberCertificate_C_M_C_))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CardItems.VariableBottomItem_MRZ2 = form.TextProperty;
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
                    using (var form = new frmEntryPictureProperty(cmbDataSource, CbActiveDatabase.Checked, new CrewMemberCertificate_C_M_C_().VariableTopItem_PersonalPicture, LayoutDesignTools.ItemsOnTheCards_Picture.CrewMemberCertificate_C_M_C_VariableTopItem_PersonalPicture, LayoutDesignTools.CardType.CrewMemberCertificate_C_M_C_))
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
        private void PibSignature_DoubleClick(object sender, EventArgs e)
        {
            if (SettingsData.Role != "User")
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    LayoutDesignTools tools = new LayoutDesignTools();
                    using (var form = new frmEntryPictureProperty(cmbDataSource, CbActiveDatabase.Checked, new CrewMemberCertificate_C_M_C_().VariableTopItem_SingniturePicture, LayoutDesignTools.ItemsOnTheCards_Picture.CrewMemberCertificate_C_M_C_VariableTopItem_SingniturePicture, LayoutDesignTools.CardType.CrewMemberCertificate_C_M_C_))
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

        private void FrmCrewMemberCertificate_C_M_C__Load(object sender, EventArgs e)
        {
            FileWork.readstate();
            if (FileWork.stateMashin == FileWork.StateOfmashin.Ready || FileWork.stateMashin == FileWork.StateOfmashin.Printed) btnCleaning.Visible = false;
            else btnCleaning.Visible = true;

            cbSex.Items.Add(LayoutDesignTools.MaleSexuality); cbSex.Items.Add(LayoutDesignTools.FemaleSexuality); cbSex.SelectedItem = cbSex.Items[0];
            foreach (string item in LayoutDesignTools.DicNationality.Keys) CBNationality.Items.Add(item); CBNationality.SelectedItem = CBNationality.Items[0];
            dl.ParametersSave Setting = new dl.ParametersSave();
            cmbDataSource.Items.Clear();
            DataSet dset = new dl.InfoModel().ListOfName();
            cmbDataSource.DataSource = dset.Tables[0];
            cmbDataSource.ValueMember = "ID";
            cmbDataSource.DisplayMember = "InfoName";
            cmbDataSource.SelectedIndex = -1;
            if (cmbDataSource.Items.Count > 0)
                cmbDataSource.SelectedItem = cmbDataSource.Items[0];


            CardItems.VariableTopItem_PersonalPicture = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Picture.CrewMemberCertificate_C_M_C_VariableTopItem_PersonalPicture);
            CardItems.VariableTopItem_PersonalPicture.EntryPicturePath = "";
            CardItems.VariableTopItem_SingniturePicture = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Picture.CrewMemberCertificate_C_M_C_VariableTopItem_SingniturePicture);
            CardItems.VariableTopItem_SingniturePicture.EntryPicturePath = "";
            CardItems.VariableBottomItem_IssuingAuthoritySingniturePicture = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Picture.CrewMemberCertificate_C_M_C_VariableBottomItem_IssuingAuthoritySingniturePicture);


            CardItems.VariableTopItem_Surname = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Surname);
            CardItems.VariableTopItem_GivenName = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_GivenName);
            CardItems.VariableTopItem_Sex = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Sex);
            CardItems.VariableTopItem_Nationality = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Nationality);
            CardItems.VariableTopItem_DateOfBrith = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_DateOfBrith);
            CardItems.VariableTopItem_Employedby = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Employedby);
            CardItems.VariableTopItem_Occupation = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Occupation);
            CardItems.VariableTopItem_DocNo = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_DocNo);
            CardItems.VariableTopItem_DateOfExpiry = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_DateOfExpiry);

            CardItems.VariableBottomItem_MRZ0 = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableBottomItem_MRZ0);
            CardItems.VariableBottomItem_MRZ1 = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableBottomItem_MRZ1);
            CardItems.VariableBottomItem_MRZ2 = Setting.ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableBottomItem_MRZ2);
        }

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
                if (String.IsNullOrWhiteSpace(txtEmployedby.Text) || String.IsNullOrWhiteSpace(txtGivenName.Text) || String.IsNullOrWhiteSpace(txtlblDocNo.Text) ||
                    String.IsNullOrWhiteSpace(txtSurname.Text) || String.IsNullOrWhiteSpace(txtEmployedby.Text) || String.IsNullOrWhiteSpace(txtOccupation.Text))
                    MessageBox.Show(StatusClass.Error_CardItemsTextIsEmpty, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

                else
                {
                    CardItems.VariableTopItem_Surname.EntryText = txtSurname.Text;
                    CardItems.VariableTopItem_GivenName.EntryText = txtGivenName.Text;
                    CardItems.VariableTopItem_Sex.EntryText = cbSex.SelectedItem.ToString();
                    CardItems.VariableTopItem_Nationality.EntryText = CBNationality.SelectedItem.ToString();
                    CardItems.VariableTopItem_DateOfBrith.EntryText = dtpDateOfBrith.Value.ToString("MM/dd/yyyy");
                    CardItems.VariableTopItem_Employedby.EntryText = txtEmployedby.Text;
                    CardItems.VariableTopItem_Occupation.EntryText = txtOccupation.Text;
                    CardItems.VariableTopItem_DocNo.EntryText = txtlblDocNo.Text;
                    CardItems.VariableTopItem_DateOfExpiry.EntryText = dtpDateOfExpiry.Value.ToString("MM/dd/yy");

                    string[] TextOfMRZ = new string[3];
                    ReturnStatus = new CrewMemberCertificate_C_M_C_().ArrangeItemsOnCard(CardItems, ref TextOfMRZ, ref TextItems, ref ImagePath, ref ImageBase64);
                    if (ReturnStatus.ResponseReturnStatus == StatusClass.ResponseStatus.Ok)
                    {
                        lblMRZ0.Text = TextOfMRZ[0];
                        lblMRZ0.Update();
                        lblMRZ0.Refresh();

                        lblMRZ1.Text = TextOfMRZ[1];
                        lblMRZ1.Update();
                        lblMRZ1.Refresh();

                        lblMRZ2.Text = TextOfMRZ[2];
                        lblMRZ2.Update();
                        lblMRZ2.Refresh();

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
                    else
                        MessageBox.Show(ReturnStatus.ReturnDescription, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                btnSinglePrint.Enabled = false;
                FileWork.readstate();
                if (FileWork.stateMashin == FileWork.StateOfmashin.Ready || FileWork.stateMashin == FileWork.StateOfmashin.Printed)
                {
                    if (String.IsNullOrWhiteSpace(CardItems.VariableTopItem_Surname.DatabaseRowName) || String.IsNullOrWhiteSpace(CardItems.VariableTopItem_GivenName.DatabaseRowName) ||
                       String.IsNullOrWhiteSpace(CardItems.VariableTopItem_Sex.DatabaseRowName) || String.IsNullOrWhiteSpace(CardItems.VariableTopItem_Nationality.DatabaseRowName) ||
                      String.IsNullOrWhiteSpace(CardItems.VariableTopItem_DateOfBrith.DatabaseRowName) || String.IsNullOrWhiteSpace(CardItems.VariableTopItem_Employedby.DatabaseRowName) ||
                      String.IsNullOrWhiteSpace(CardItems.VariableTopItem_Occupation.DatabaseRowName) || String.IsNullOrWhiteSpace(CardItems.VariableTopItem_DocNo.DatabaseRowName) ||
                       String.IsNullOrWhiteSpace(CardItems.VariableTopItem_DateOfExpiry.DatabaseRowName))
                        MessageBox.Show(StatusClass.Error_DatabaseDataEntryIsIncorrect, StatusClass.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {

                        Dictionary<LayoutDesignTools.ItemsOnTheCards_Text, string> TextSelection = new Dictionary<LayoutDesignTools.ItemsOnTheCards_Text, string>();
                        Dictionary<LayoutDesignTools.ItemsOnTheCards_Picture, string> ImageSelection = new Dictionary<LayoutDesignTools.ItemsOnTheCards_Picture, string>();


                        TextSelection.Add(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Surname, CardItems.VariableTopItem_Surname.DatabaseRowName);
                        TextSelection.Add(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_GivenName, CardItems.VariableTopItem_GivenName.DatabaseRowName);
                        TextSelection.Add(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Sex, CardItems.VariableTopItem_Sex.DatabaseRowName);
                        TextSelection.Add(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Nationality, CardItems.VariableTopItem_Nationality.DatabaseRowName);
                        TextSelection.Add(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_DateOfBrith, CardItems.VariableTopItem_DateOfBrith.DatabaseRowName);
                        TextSelection.Add(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Employedby, CardItems.VariableTopItem_Employedby.DatabaseRowName);
                        TextSelection.Add(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Occupation, CardItems.VariableTopItem_Occupation.DatabaseRowName);
                        TextSelection.Add(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_DocNo, CardItems.VariableTopItem_DocNo.DatabaseRowName);
                        TextSelection.Add(LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_DateOfExpiry, CardItems.VariableTopItem_DateOfExpiry.DatabaseRowName);

                        ImageSelection.Add(LayoutDesignTools.ItemsOnTheCards_Picture.CrewMemberCertificate_C_M_C_VariableTopItem_PersonalPicture, CardItems.VariableTopItem_PersonalPicture.DatabaseRowName);
                        ImageSelection.Add(LayoutDesignTools.ItemsOnTheCards_Picture.CrewMemberCertificate_C_M_C_VariableTopItem_SingniturePicture, CardItems.VariableTopItem_SingniturePicture.DatabaseRowName);

                        new SeriesPrintForCAO(LayoutDesignTools.CardType.CrewMemberCertificate_C_M_C_, CardItems, TextSelection, ImageSelection, Convert.ToInt32(cmbDataSource.SelectedValue), 2).ShowDialog();
                    }
                }
                else
                    MessageBox.Show("پرینتر در حال پرینت می باشد.لطفا کنید.", "اعلام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSinglePrint.Enabled = true;
            }
            LayoutDesignTools.flgDefinedCardPrinted = false;
            btnSinglePrint.Enabled = true;
        }

        private void TxtPrintNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
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

        private void BtnCleaning_Click(object sender, EventArgs e)
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

        private void BtnEnterDatabase_Click(object sender, EventArgs e)
        {
            new EnterData().ShowDialog();
        }
    }
}
