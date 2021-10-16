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

namespace SaraPrinterLaser.LayoutDesignFolder
{
    public partial class frmEntryTextProperty : Form
    {
        private Font DefinedFont;
        private ComboBox cbxDataSource;
        private bool flgDatabaseExist;
        private LayoutDesignTools.CardType enmCardType;
        public EntryTextProperty TextProperty;
        private EntryTextProperty DefualtValue;
        private LayoutDesignTools.ItemsOnTheCards_Text ItemType;

        public static LaserConfigClass LaserSetting { get; private set; }

        internal frmEntryTextProperty(ComboBox _cbxDataSource, bool _flgDatabaseExist, EntryTextProperty _DefualtValue, LayoutDesignTools.ItemsOnTheCards_Text _ItemType, LayoutDesignTools.CardType _CardType)
        {
            InitializeComponent();
            cbxDataSource = _cbxDataSource;
            flgDatabaseExist = _flgDatabaseExist;
            DefualtValue = _DefualtValue;
            ItemType = _ItemType;
            enmCardType = _CardType;
        }

        private void BtnChangeFont_Click(object sender, EventArgs e)
        {
            FontDialog fntdlg = new FontDialog()
            {
                AllowScriptChange = false,
                AllowSimulations = false,
                AllowVerticalFonts = false,
                AllowVectorFonts = false,
                MaxSize = 20,
                MinSize = 8,
                ShowApply = false,
            };
            if (fntdlg.ShowDialog() == DialogResult.OK)
            {
                DefinedFont = fntdlg.Font;
                lblTextChangeFont.Text = DefinedFont.Name + " , " + DefinedFont.Size.ToString() + " , " + Font.Style.ToString();
            }


        }

        private void FrmEntryTextProperty_Load(object sender, EventArgs e)
        {

            dl.ParametersSave SaveSettings = new dl.ParametersSave();

            if (SaveSettings.ParameterExist(ItemType))
                TextProperty = SaveSettings.ParametersLoad(ItemType);
            else
            {
                SaveSettings.SaveParameters(enmCardType, ItemType, DefualtValue, false);
                TextProperty = DefualtValue;
            }

            ArrangeDataOntheForm(TextProperty);
        }
        private void ArrangeDataOntheForm(EntryTextProperty _entryText)
        {
            bool flgItemExist = false;
            string lclItemName = "";
            DefinedFont = _entryText.EntryTextFont;
            lblTextChangeFont.Text = DefinedFont.Name + " , " + DefinedFont.Size.ToString() + " , " + Font.Style.ToString();
            txtXpos.Text = _entryText.XPos.ToString();
            txtYPos.Text = _entryText.YPos.ToString();
            switch (_entryText.EntryTextFontLanguage)
            {
                case EntryTextProperty.TextLanguage.Persian:
                    cmbTextDirecion.SelectedItem = cmbTextDirecion.Items[0];
                    break;
                case EntryTextProperty.TextLanguage.Latin:
                    cmbTextDirecion.SelectedItem = cmbTextDirecion.Items[1];
                    break;
                default:
                    break;
            }

            cmbLaserPen.Items.Clear();
            foreach (var item in new dl.ParametersSave().GetPennames())
            {
                if (item == _entryText.TextLaserParameters.ParametersName)
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
                        if (item == _entryText.DatabaseRowName)
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




        private void BtnChangeParameter_Click(object sender, EventArgs e)
        {
            new frmCaoFullPenManagement().ShowDialog();
            cmbLaserPen.Items.Clear();
            foreach (var item in new dl.ParametersSave().GetPennames())
                cmbLaserPen.Items.Add(item);
            cmbLaserPen.SelectedItem = cmbLaserPen.Items[0];
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnDefualt_Click(object sender, EventArgs e)
        {
            DefinedFont = DefualtValue.EntryTextFont;
            lblTextChangeFont.Text = DefinedFont.Name + " , " + DefinedFont.Size.ToString() + " , " + DefinedFont.Style.ToString();
            txtXpos.Text = DefualtValue.XPos.ToString();
            txtYPos.Text = DefualtValue.YPos.ToString();
            switch (DefualtValue.EntryTextFontLanguage)
            {
                case EntryTextProperty.TextLanguage.Persian:
                    cmbTextDirecion.SelectedItem = cmbTextDirecion.Items[0];
                    break;
                case EntryTextProperty.TextLanguage.Latin:
                    cmbTextDirecion.SelectedItem = cmbTextDirecion.Items[1];
                    break;
                default:
                    break;
            }

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

        private void BtnSabt_Click(object sender, EventArgs e)
        {
            dl.ParametersSave SaveSettings = new dl.ParametersSave();
            try
            {
                TextProperty.EntryTextFont = DefinedFont;
                if (cmbTextDirecion.SelectedItem == cmbTextDirecion.Items[0]) TextProperty.EntryTextFontLanguage = EntryTextProperty.TextLanguage.Persian;
                else if (cmbTextDirecion.SelectedItem == cmbTextDirecion.Items[1]) TextProperty.EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin;
                TextProperty.XPos =  double.Parse(txtXpos.Text);
                TextProperty.YPos =  double.Parse(txtYPos.Text);
                TextProperty.TextLaserParameters.ParametersName = cmbLaserPen.SelectedItem.ToString();
                TextProperty.TextLaserParameters = SaveSettings.ParametersLoad((string)cmbLaserPen.SelectedItem);
                if (flgDatabaseExist)
                {
                    TextProperty.DatabaseRowName = cmbDatabaseRow.SelectedItem.ToString();
                }
                if (SaveSettings.ParameterExist(ItemType))
                    SaveSettings.ParametersUpdate(enmCardType, ItemType, TextProperty, flgDatabaseExist);
                else
                    SaveSettings.SaveParameters(enmCardType, ItemType, TextProperty, flgDatabaseExist);
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
