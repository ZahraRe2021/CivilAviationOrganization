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

namespace SaraPrinterLaser.LayoutDesignFolder
{
    public partial class frmEntryPictureProperty : Form
    {
        private ComboBox cbxDataSource;
        private bool flgDatabaseExist;
        private LayoutDesignTools.CardType enmCardType;
        public EntryPictureProperty PictureProperty;
        private EntryPictureProperty DefualtValue;
        private LayoutDesignTools.ItemsOnTheCards_Picture ItemType;



        internal frmEntryPictureProperty(ComboBox _cbxDataSource, bool _flgDatabaseExist, EntryPictureProperty _DefualtValue, LayoutDesignTools.ItemsOnTheCards_Picture _ItemType, LayoutDesignTools.CardType _CardType)
        {
            InitializeComponent();
            cbxDataSource = _cbxDataSource;
            flgDatabaseExist = _flgDatabaseExist;
            DefualtValue = _DefualtValue;
            ItemType = _ItemType;
            enmCardType = _CardType;
        }

        private void FrmEntryPictureProperty_Load(object sender, EventArgs e)
        {
            dl.ParametersSave SaveSettings = new dl.ParametersSave();
            if (SaveSettings.ParameterExist(ItemType))
                PictureProperty = SaveSettings.ParametersLoad(ItemType);
            else
            {
                SaveSettings.SaveParameters(enmCardType, ItemType, DefualtValue, false);
                PictureProperty = DefualtValue;
            }
            ArrangeDataOntheForm(PictureProperty);

        }
        private void ArrangeDataOntheForm(EntryPictureProperty _EntryPicture)
        {
            bool flgItemExist = false;
            string lclItemName = "";

            txtImageAddressPath.Text = _EntryPicture.EntryPicturePath;

            txtXpos.Text = _EntryPicture.XPos.ToString();
            txtYPos.Text = _EntryPicture.YPos.ToString();

            cmbLaserPen.Items.Clear();
            foreach (var item in new dl.ParametersSave().GetPennames())
            {
                if (item == _EntryPicture.PictureLaserParameters.ParametersName)
                {
                    flgItemExist = true;
                    lclItemName = item;
                }
                cmbLaserPen.Items.Add(item);
            }
            if (!flgItemExist)
                cmbLaserPen.SelectedItem = cmbLaserPen.Items[0];
            else
                cmbLaserPen.SelectedIndex = cmbLaserPen.FindStringExact(lclItemName);

            flgItemExist = false;

            cmbDatabaseRow.Items.Clear();
            if (flgDatabaseExist)
            {
                int _Result;
                if (cbxDataSource.SelectedIndex != -1 && int.TryParse(cbxDataSource.SelectedValue.ToString(), out _Result))
                {



                    string[] fields = new dl.InfoModel().InfoDataByID(Convert.ToInt32(cbxDataSource.SelectedValue)).Split('ß');
                    foreach (var item in fields)
                    {
                        if (item == _EntryPicture.DatabaseRowName)
                        {
                            flgItemExist = true;
                            lclItemName = item;
                        }
                        cmbDatabaseRow.Items.Add(item);
                    }
                }
                if (cmbDatabaseRow.Items.Count > 0 && cmbDatabaseRow.Enabled == true)
                {
                    if (!flgItemExist)
                        cmbDatabaseRow.SelectedItem = cmbDatabaseRow.Items[0];
                    else
                        cmbDatabaseRow.SelectedIndex = cmbDatabaseRow.FindStringExact(lclItemName);
                }
            }
            else
            {
                lblDatabaseRow.Visible = false;
                cmbDatabaseRow.Visible = false;
            }





        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnDefualt_Click(object sender, EventArgs e)
        {
            txtImageAddressPath.Text = DefualtValue.EntryPicturePath;

            txtXpos.Text = DefualtValue.XPos.ToString();
            txtYPos.Text = DefualtValue.YPos.ToString();


            cmbLaserPen.Items.Clear();
            foreach (var item in new dl.ParametersSave().GetPennames())
                cmbLaserPen.Items.Add(item);
            cmbLaserPen.SelectedItem = cmbLaserPen.Items[0];

            cmbDatabaseRow.Items.Clear();
            if (flgDatabaseExist)
            {
                int _Result;
                if (cbxDataSource.SelectedIndex != -1 && int.TryParse(cbxDataSource.SelectedValue.ToString(), out _Result))
                {
                    string[] fields = new dl.InfoModel().InfoDataByID(Convert.ToInt32(cbxDataSource.SelectedValue)).Split('ß');
                    foreach (var item in fields)
                        cmbDatabaseRow.Items.Add(item);

                }
                if (cmbDatabaseRow.Items.Count > 0 && cmbDatabaseRow.Enabled == true)
                    cmbDatabaseRow.SelectedItem = cmbDatabaseRow.Items[0];
            }
            else
            {
                lblDatabaseRow.Visible = false;
                cmbDatabaseRow.Visible = false;
            }
        }

        private void BtnChangeParameter_Click(object sender, EventArgs e)
        {
            new frmCaoFullPenManagement().ShowDialog();
            cmbLaserPen.Items.Clear();
            foreach (var item in new dl.ParametersSave().GetPennames())
                cmbLaserPen.Items.Add(item);
            cmbLaserPen.SelectedItem = cmbLaserPen.Items[0];
        }

        private void BtnEntryPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog Opg = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.bmp, *.gif) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.bmp; *.gif",
                ReadOnlyChecked = true,
                Multiselect = false,
                AutoUpgradeEnabled = true,
                CheckFileExists = true,
                CheckPathExists = true,
                Title = StatusClass.Message_PleaseSelectTheImage,
                RestoreDirectory = true,
            };
            if (Opg.ShowDialog() == DialogResult.OK)
            {
                txtImageAddressPath.Text = Opg.FileName;
                PictureProperty.EntryPicturePath = Opg.FileName;
            }
        }

        private void BtnSabt_Click(object sender, EventArgs e)
        {
            dl.ParametersSave SaveSettings = new dl.ParametersSave();
            try
            {

                PictureProperty.EntryPicturePath = txtImageAddressPath.Text;
                PictureProperty.XPos =  double.Parse(txtXpos.Text);
                PictureProperty.YPos =  double.Parse(txtYPos.Text);
                PictureProperty.PictureLaserParameters.ParametersName = cmbLaserPen.SelectedItem.ToString();
                PictureProperty.PictureLaserParameters = SaveSettings.ParametersLoad((string)cmbLaserPen.SelectedItem);
                if (flgDatabaseExist)
                {
                    PictureProperty.DatabaseRowName = cmbDatabaseRow.SelectedItem.ToString();
                }
                if (SaveSettings.ParameterExist(ItemType))
                    SaveSettings.ParametersUpdate(enmCardType, ItemType, PictureProperty, flgDatabaseExist);
                else
                    SaveSettings.SaveParameters(enmCardType, ItemType, PictureProperty, flgDatabaseExist);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception)
            {
                this.DialogResult = DialogResult.Cancel;

            }
        }

        private void TxtXpos_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtYPos_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
