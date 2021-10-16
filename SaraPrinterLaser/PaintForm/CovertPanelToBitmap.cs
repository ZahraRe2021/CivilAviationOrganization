using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SaraPrinterLaser.PaintForm
{
    public static class CovertPanelToBitmap
    {
        #region PrivateFunctions

        private static Bitmap newImage = new Bitmap(2023, 1276, PxFormat);
        private static PixelFormat PxFormat = PixelFormat.Format8bppIndexed;
        /// <summary>
        /// Convert Font to String
        /// </summary>
        /// <returns></returns>
        private static string FontToString(Font font)
        {
            return font.FontFamily.Name + ":" + font.Size + ":" + (int)font.Style;
        }
        /// <summary>
        /// Convert String to Font
        /// </summary>
        /// <returns></returns>
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
        private static Bitmap GetControlImage(Panel ctl)
        {
            ctl.Show();
            Bitmap bm = new Bitmap(ctl.Width, ctl.Height);

            ctl.DrawToBitmap(bm, new Rectangle(0, 0, ctl.Width, ctl.Height));
            bm.Save("Test.bmp", ImageFormat.Bmp);
            return bm;
        }
        private static Bitmap GrayScale(Bitmap Bmp)
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
        #endregion

        public static Panel loadXMLFILE(string Xmltext)
        {
            string[] GeneralParameters = null;
            string[] BarcodesParameters = null;
            string[] PictureBoxesParameters = null;
            string[] LabelsParameters = null;
            string[] OrientationLabelsParameters = null;
            Panel ReturnPanel = new Panel();
            ReturnPanel.Size = new Size(674, 425);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(Xmltext);
            XmlNode xnList = xml.SelectSingleNode("SaraHardwareCompanyLaserPrinter");

            foreach (XmlNode xn in xnList)
            {
                #region FillVariableFromXml
                string CntrlType = xn["Type"].InnerText;
                string CntrlName = xn["Name"].InnerText;
                string CntrlText = xn["Text"].InnerText;
                string CntrlFonts = xn["Fonts"].InnerText;
                string CntrlLocatioX = xn["LocationX"].InnerText;
                string CntrlLocatioY = xn["LocationY"].InnerText;
                string CntrlSizeWidth = xn["SizeWidth"].InnerText;
                string CntrlSizeHeight = xn["SizeHeight"].InnerText;
                string CntrlpicImage = xn["PictureImage"].InnerText;
                string CntrlBackColor = xn["BackColor"].InnerText;
                string CntrlForeColor = xn["ForeColor"].InnerText;
                string CntrlRightToLeft = xn["RightToLeft"].InnerText;
                string CntrlBackgroundImageLayout = xn["BackgroundImageLayout"].InnerText;
                string CntrlCursor = xn["Cursor"].InnerText;
                string CntrlAllowDrop = xn["AllowDrop"].InnerText;
                string CntrlEnable = xn["Enable"].InnerText;
                string CntrlTabIndex = xn["TabIndex"].InnerText;
                string CntrlVisible = xn["Visible"].InnerText;
                string CntrlCausesValidation = xn["CausesValidation"].InnerText;
                string CntrlAnchor = xn["Anchor"].InnerText;
                string CntrlDock = xn["Dock"].InnerText;
                string CntrlMargin = xn["Margin"].InnerText;
                string CntrlPadding = xn["Padding"].InnerText;
                string CntrlMaximumSizeWidth = xn["MaximumSizeWidth"].InnerText;
                string CntrlMaximumSizeHeight = xn["MaximumSizeHeight"].InnerText;
                string CntrlMinimumSizeWidth = xn["MinimumSizeWidth"].InnerText;
                string CntrlMinimumSizeHeight = xn["MinimumSizeHeight"].InnerText;
                string CntrlUseWaitCursor = xn["UseWaitCursor"].InnerText;

                string CntrlBCBoarderStyle = xn["BCBoarderStyle"].InnerText;
                string CntrlBCHorizontalAlignment = xn["BCHorizontalAlignment"].InnerText;
                string CntrlBCHorizontalTextAlignment = xn["BCHorizontalTextAlignment"].InnerText;
                string CntrlBCLookAndFeel = xn["BCLookAndFeel"].InnerText;
                string CntrlBCVerticalAlignment = xn["BCVerticalAlignment"].InnerText;
                string CntrlBCVerticalTextAlignment = xn["BCVerticalTextAlignment"].InnerText;
                string CntrlBCAutoModule = xn["BCAutoModule"].InnerText;
                string CntrlBCImeMode = xn["BCImeMode"].InnerText;
                string CntrlBCModule = xn["BCModule"].InnerText;
                string CntrlBCOrientation = xn["BCOrientation"].InnerText;
                string CntrlBCShowText = xn["BCShowText"].InnerText;
                string CntrlBCSymbology = xn["BCSymbology"].InnerText;
                string CntrlBCtabstop = xn["BCtabstop"].InnerText;
                string CntrlBCbinaryData = xn["BCbinaryData"].InnerText;
                string CntrlBCAllowHtmlTextInToolTip = xn["BCAllowHtmlTextInToolTip"].InnerText;
                string CntrlBCShowToolTips = xn["BCShowToolTips"].InnerText;
                string CntrlBCToolTip = xn["BCToolTip"].InnerText;
                string CntrlBCToolTipIconType = xn["BCToolTipIconType"].InnerText;
                string CntrlBCToolTipTitle = xn["BCToolTipTitle"].InnerText;

                string CntrlCntrlPBBoarderStyle = xn["PBBoarderStyle"].InnerText;
                string CntrlPBWaitOnLoad = xn["PBWaitOnLoad"].InnerText;
                string CntrlPBSizeMode = xn["PBSizeMode"].InnerText;
                string CntrlPBImageLocation = xn["PBImageLocation"].InnerText;

                string CntrllblLabelBoarderStyle = xn["lblLabelBoarderStyle"].InnerText;
                string CntrllblFlatStyle = xn["lblFlatStyle"].InnerText;
                string CntrllblImageAlign = xn["lblImageAlign"].InnerText;
                string CntrllblImageIndex = xn["lblImageIndex"].InnerText;
                string CntrllblimageKey = xn["lblimageKey"].InnerText;
                string CntrllblTextAlign = xn["lblTextAlign"].InnerText;
                string CntrllblUseMnemonic = xn["lblUseMnemonic"].InnerText;
                string CntrllblAutoEllipsis = xn["lblAutoEllipsis"].InnerText;
                string CntrllblUseCompatibleTextRendering = xn["lblUseCompatibleTextRendering"].InnerText;
                string CntrllblAutoSize = xn["lblAutoSize"].InnerText;

                string CntrlOrLblBorderStyle = xn["OrLblBorderStyle"].InnerText;
                string CntrlOrLblFlatStyle = xn["OrLblFlatStyle"].InnerText;
                string CntrlOrLblImageAlign = xn["OrLblImageAlign"].InnerText;
                string CntrlOrLblImageIndex = xn["OrLblImageIndex"].InnerText;
                string CntrlOrLblImageKey = xn["OrLblImageKey"].InnerText;
                string CntrlOrLblRotationAngle = xn["OrLblRotationAngle"].InnerText;
                string CntrlOrLblTextAlign = xn["OrLblTextAlign"].InnerText;
                string CntrlOrLblTextDirection = xn["OrLblTextDirection"].InnerText;
                string CntrlOrLblTextOrientation = xn["OrLblTextOrientation"].InnerText;
                string CntrlOrLblUseMnemonic = xn["OrLblUseMnemonic"].InnerText;
                string CntrlOrLblAutoEllipsis = xn["OrLblAutoEllipsis"].InnerText;
                string CntrlOrLblUseCompatibleTextRendering = xn["OrLblUseCompatibleTextRendering"].InnerText;
                string CntrlOrLblAutoSize = xn["OrLblAutoSize"].InnerText;

                //For grid
                string gridsrowsBackColor = xn["gridsrowsBackColor"].InnerText;
                string gridsAlternaterowsBackColor = xn["gridsAlternaterowsBackColor"].InnerText;
                string gridsheaderColor = xn["gridsheaderColor"].InnerText;
                // For Grid
                #endregion
                if (CntrlType != "System.Windows.Forms.Panel")
                {
                    GeneralParameters = new string[]
                    {
                    CntrlType ,
                    CntrlName ,
                    CntrlText ,
                    CntrlFonts ,
                    CntrlLocatioX,
                    CntrlLocatioY ,
                    CntrlSizeWidth ,
                    CntrlSizeHeight ,
                    CntrlpicImage ,
                    CntrlBackColor ,
                    CntrlForeColor ,
                    CntrlRightToLeft,
                    CntrlBackgroundImageLayout,
                    CntrlCursor ,
                    CntrlAllowDrop ,
                    CntrlEnable ,
                    CntrlTabIndex,
                    CntrlVisible ,
                    CntrlCausesValidation ,
                    CntrlAnchor ,
                    CntrlDock ,
                    CntrlMargin ,
                    CntrlPadding ,
                    CntrlMaximumSizeWidth ,
                    CntrlMaximumSizeHeight ,
                    CntrlMinimumSizeWidth ,
                    CntrlMinimumSizeHeight ,
                    CntrlUseWaitCursor,
                    gridsrowsBackColor,
                    gridsAlternaterowsBackColor,
                    gridsheaderColor
                };
                    BarcodesParameters = new string[]
                    {
                    CntrlBCBoarderStyle ,
                    CntrlBCHorizontalAlignment ,
                    CntrlBCHorizontalTextAlignment ,
                    CntrlBCLookAndFeel ,
                    CntrlBCVerticalAlignment ,
                    CntrlBCVerticalTextAlignment ,
                    CntrlBCAutoModule ,
                    CntrlBCImeMode ,
                    CntrlBCModule ,
                    CntrlBCOrientation ,
                    CntrlBCShowText ,
                    CntrlBCSymbology ,
                    CntrlBCtabstop ,
                    CntrlBCbinaryData ,
                    CntrlBCAllowHtmlTextInToolTip ,
                    CntrlBCShowToolTips ,
                    CntrlBCToolTip ,
                    CntrlBCToolTipIconType ,
                    CntrlBCToolTipTitle
                };
                    PictureBoxesParameters = new string[]
                    {
                    CntrlCntrlPBBoarderStyle,
                    CntrlPBWaitOnLoad ,
                    CntrlPBSizeMode,
                    CntrlPBImageLocation
                    };
                    LabelsParameters = new string[]
                    {
                    CntrllblLabelBoarderStyle,
                    CntrllblFlatStyle,
                    CntrllblImageAlign,
                    CntrllblImageIndex ,
                    CntrllblimageKey ,
                    CntrllblTextAlign ,
                    CntrllblUseMnemonic,
                    CntrllblAutoEllipsis,
                    CntrllblUseCompatibleTextRendering,
                    CntrllblAutoSize
                };
                    OrientationLabelsParameters = new string[]
                    {
                    CntrlOrLblBorderStyle ,
                    CntrlOrLblFlatStyle ,
                    CntrlOrLblImageAlign ,
                    CntrlOrLblImageIndex ,
                    CntrlOrLblImageKey ,
                    CntrlOrLblRotationAngle,
                    CntrlOrLblTextAlign ,
                    CntrlOrLblTextDirection ,
                    CntrlOrLblTextOrientation,
                    CntrlOrLblUseMnemonic ,
                    CntrlOrLblAutoEllipsis ,
                    CntrlOrLblUseCompatibleTextRendering ,
                    CntrlOrLblAutoSize
                };
                    switch (GeneralParameters[0])
                    {

                        case "System.Windows.Forms.PictureBox":
                            {
                                PictureBox PictureBoxControl = new PictureBox();
                                Color CntrlBackColorPictureBox = new Color();
                                PictureBoxControl.Name = GeneralParameters[1];
                                //PictureBoxControl.Text==>Nothing
                                //PictureBoxControl.Font==>Nothing
                                PictureBoxControl.Location = new Point((int.Parse(GeneralParameters[4])), (int.Parse(GeneralParameters[5])));
                                PictureBoxControl.Size = new Size((int.Parse(GeneralParameters[6])), (int.Parse(GeneralParameters[7])));
                                if (!String.IsNullOrWhiteSpace( GeneralParameters[8])) { Bitmap BitmapImage = new Bitmap(new MemoryStream(Convert.FromBase64String(GeneralParameters[8]))); PictureBoxControl.Image = BitmapImage; }
                                CntrlBackColorPictureBox = ColorTranslator.FromHtml(GeneralParameters[9]);
                                PictureBoxControl.BackColor = CntrlBackColorPictureBox;
                                //PictureBoxControl.ForeColor==>Nothing
                                //PictureBoxControl.RightToleft==>Nothing
                                PictureBoxControl.BackgroundImageLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), GeneralParameters[12]);
                                //PictureBoxControl.Cursor /*I don't Know*/
                                //PictureBoxControl.AllowDrop=>Nothing
                                PictureBoxControl.Enabled = bool.Parse(GeneralParameters[15]);
                                //PictureBoxControl.tabIndex==>Nothing
                                PictureBoxControl.Visible = bool.Parse(GeneralParameters[17]);
                                //PictureBoxControl.CausesValidation==>Nothing
                                PictureBoxControl.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), GeneralParameters[19]);
                                PictureBoxControl.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), GeneralParameters[20]);
                                //PictureBoxControl.Margin = StringTomargin;/*I don't Know*/
                                //PictureBoxControl.padding = StringToPadding;/*I don't Know*/
                                PictureBoxControl.MaximumSize = new Size((int.Parse(GeneralParameters[23])), (int.Parse(GeneralParameters[24])));
                                PictureBoxControl.MinimumSize = new Size((int.Parse(GeneralParameters[25])), (int.Parse(GeneralParameters[26])));
                                PictureBoxControl.UseWaitCursor = bool.Parse(GeneralParameters[27]);

                                PictureBoxControl.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), PictureBoxesParameters[0]);
                                PictureBoxControl.WaitOnLoad = bool.Parse(PictureBoxesParameters[1]);
                                PictureBoxControl.SizeMode = (PictureBoxSizeMode)Enum.Parse(typeof(PictureBoxSizeMode), PictureBoxesParameters[2]);
                                PictureBoxControl.ImageLocation = PictureBoxesParameters[3];

                                ReturnPanel.Controls.Add(PictureBoxControl);

                            }
                            break;

                        case "System.Windows.Forms.Label":
                            {
                                Label LabelControl = new Label();
                                Color CntrlBackColorLabel = new Color();
                                Color CntrlForeColorLabel = new Color();
                                LabelControl.Name = GeneralParameters[1];
                                LabelControl.Text = GeneralParameters[2];
                                Font LabelFont = StringToFont(GeneralParameters[3]);
                                LabelControl.Font = LabelFont;
                                LabelControl.Location = new Point((int.Parse(GeneralParameters[4])), (int.Parse(GeneralParameters[5])));
                                LabelControl.Size = new Size((int.Parse(GeneralParameters[6])), (int.Parse(GeneralParameters[7])));
                                if (!String.IsNullOrWhiteSpace(GeneralParameters[8])) { Bitmap BitmapImage = new Bitmap(new MemoryStream(Convert.FromBase64String(GeneralParameters[8]))); LabelControl.Image = BitmapImage; }
                                CntrlBackColorLabel = ColorTranslator.FromHtml(GeneralParameters[9]);
                                LabelControl.BackColor = CntrlBackColorLabel;
                                CntrlForeColorLabel = ColorTranslator.FromHtml(GeneralParameters[10]);
                                LabelControl.ForeColor = CntrlForeColorLabel;
                                LabelControl.RightToLeft = (RightToLeft)Enum.Parse(typeof(RightToLeft), GeneralParameters[11]);
                                LabelControl.BackgroundImageLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), GeneralParameters[12]);
                                //LabelControl.Cursor /*I don't Know*/
                                LabelControl.AllowDrop = bool.Parse(GeneralParameters[14]);
                                LabelControl.Enabled = bool.Parse(GeneralParameters[15]);
                                LabelControl.TabIndex = int.Parse(GeneralParameters[16]);
                                LabelControl.Visible = bool.Parse(GeneralParameters[17]);
                                LabelControl.CausesValidation = bool.Parse(GeneralParameters[18]);
                                LabelControl.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), GeneralParameters[19]);
                                LabelControl.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), GeneralParameters[20]);
                                //LabelControl.Margin = StringTomargin;/*I don't Know*/
                                //LabelControl.padding = StringToPadding;/*I don't Know*/
                                LabelControl.MaximumSize = new Size((int.Parse(GeneralParameters[23])), (int.Parse(GeneralParameters[24])));
                                LabelControl.MinimumSize = new Size((int.Parse(GeneralParameters[25])), (int.Parse(GeneralParameters[26])));
                                LabelControl.UseWaitCursor = bool.Parse(GeneralParameters[27]);

                                LabelControl.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), LabelsParameters[0]);
                                LabelControl.FlatStyle = (FlatStyle)Enum.Parse(typeof(FlatStyle), LabelsParameters[1]);
                                LabelControl.ImageAlign = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), LabelsParameters[2]);
                                LabelControl.ImageIndex = int.Parse(LabelsParameters[3]);
                                LabelControl.ImageKey = LabelsParameters[4];
                                LabelControl.TextAlign = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), LabelsParameters[5]);
                                LabelControl.UseMnemonic = bool.Parse(LabelsParameters[6]);
                                LabelControl.AutoEllipsis = bool.Parse(LabelsParameters[7]);
                                LabelControl.UseCompatibleTextRendering = bool.Parse(LabelsParameters[8]);
                                LabelControl.AutoSize = bool.Parse(LabelsParameters[9]);

                                ReturnPanel.Controls.Add(LabelControl);
                            }
                            break;
                        case "DevExpress.XtraEditors.BarCodeControl":

                            {

                                DevExpress.XtraEditors.BarCodeControl BarcodeControl = new DevExpress.XtraEditors.BarCodeControl();
                                Color CntrlBackColorBarcodeControl = new Color();
                                Color CntrlForeColorBarcodeControl = new Color();
                                BarcodeControl.Name = GeneralParameters[1];
                                BarcodeControl.Text = GeneralParameters[2];
                                Font BarcodeControlFont = StringToFont(GeneralParameters[3]);
                                BarcodeControl.Font = BarcodeControlFont;
                                BarcodeControl.Location = new Point((int.Parse(GeneralParameters[4])), (int.Parse(GeneralParameters[5])));
                                BarcodeControl.Size = new Size((int.Parse(GeneralParameters[6])), (int.Parse(GeneralParameters[7])));
                                //BarcodeControl.Image ==>Nothing
                                CntrlBackColorBarcodeControl = ColorTranslator.FromHtml(GeneralParameters[9]);
                                BarcodeControl.BackColor = CntrlBackColorBarcodeControl;
                                CntrlForeColorBarcodeControl = ColorTranslator.FromHtml(GeneralParameters[10]);
                                BarcodeControl.ForeColor = CntrlForeColorBarcodeControl;
                                BarcodeControl.RightToLeft = (RightToLeft)Enum.Parse(typeof(RightToLeft), GeneralParameters[11]);
                                BarcodeControl.BackgroundImageLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), GeneralParameters[12]);
                                //BarcodeControl.Cursor /*I don't Know*/
                                BarcodeControl.AllowDrop = bool.Parse(GeneralParameters[14]);
                                BarcodeControl.Enabled = bool.Parse(GeneralParameters[15]);
                                BarcodeControl.TabIndex = int.Parse(GeneralParameters[16]);
                                BarcodeControl.Visible = bool.Parse(GeneralParameters[17]);
                                BarcodeControl.CausesValidation = bool.Parse(GeneralParameters[18]);
                                BarcodeControl.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), GeneralParameters[19]);
                                BarcodeControl.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), GeneralParameters[20]);
                                //BarcodeControl.Margin = StringTomargin;/*I don't Know*/
                                //BarcodeControl.padding = StringToPadding;/*I don't Know*/
                                BarcodeControl.MaximumSize = new Size((int.Parse(GeneralParameters[23])), (int.Parse(GeneralParameters[24])));
                                BarcodeControl.MinimumSize = new Size((int.Parse(GeneralParameters[25])), (int.Parse(GeneralParameters[26])));
                                BarcodeControl.UseWaitCursor = bool.Parse(GeneralParameters[27]);

                                BarcodeControl.BorderStyle = (DevExpress.XtraEditors.Controls.BorderStyles)Enum.Parse(typeof(DevExpress.XtraEditors.Controls.BorderStyles), BarcodesParameters[0]);
                                BarcodeControl.HorizontalAlignment = (DevExpress.Utils.HorzAlignment)Enum.Parse(typeof(DevExpress.Utils.HorzAlignment), BarcodesParameters[1]);
                                BarcodeControl.HorizontalTextAlignment = (DevExpress.Utils.HorzAlignment)Enum.Parse(typeof(DevExpress.Utils.HorzAlignment), BarcodesParameters[2]);
                                BarcodeControl.VerticalAlignment = (DevExpress.Utils.VertAlignment)Enum.Parse(typeof(DevExpress.Utils.VertAlignment), BarcodesParameters[4]);
                                BarcodeControl.VerticalTextAlignment = (DevExpress.Utils.VertAlignment)Enum.Parse(typeof(DevExpress.Utils.VertAlignment), BarcodesParameters[5]);
                                BarcodeControl.AutoModule = bool.Parse(BarcodesParameters[6]);
                                BarcodeControl.ImeMode = (ImeMode)Enum.Parse(typeof(ImeMode), BarcodesParameters[7]);
                                BarcodeControl.Module = double.Parse(BarcodesParameters[8]);
                                BarcodeControl.Orientation = (DevExpress.XtraPrinting.BarCode.BarCodeOrientation)Enum.Parse(typeof(DevExpress.XtraPrinting.BarCode.BarCodeOrientation), BarcodesParameters[9]);
                                BarcodeControl.ShowText = bool.Parse(BarcodesParameters[10]);
                                switch (BarcodesParameters[11])
                                {
                                    case "DevExpress.XtraPrinting.BarCode.CodabarGenerator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.CodabarGenerator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.Industrial2of5Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Industrial2of5Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.Interleaved2of5Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Interleaved2of5Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.Code39Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Code39Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.Code39ExtendedGenerator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Code39ExtendedGenerator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.Code93Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Code93Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.Code93ExtendedGenerator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Code93ExtendedGenerator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.Code128Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Code128Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.Code11Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Code11Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.CodeMSIGenerator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.CodeMSIGenerator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.PostNetGenerator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.PostNetGenerator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.EAN13Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.EAN13Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.UPCAGenerator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.UPCAGenerator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.EAN8Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.EAN8Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.EAN128Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.EAN128Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.UPCSupplemental2Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.UPCSupplemental2Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.UPCSupplemental5Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.UPCSupplemental5Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.UPCE0Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.UPCE0Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.UPCE1Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.UPCE1Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.Matrix2of5Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Matrix2of5Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.PDF417Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.PDF417Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.DataMatrixGenerator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.DataMatrixGenerator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.QRCodeGenerator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.QRCodeGenerator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.IntelligentMailGenerator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.IntelligentMailGenerator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.DataMatrixGS1Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.DataMatrixGS1Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.ITF14Generator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.ITF14Generator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.DataBarGenerator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.DataBarGenerator();
                                        break;
                                    case "DevExpress.XtraPrinting.BarCode.IntelligentMailPackageGenerator":
                                        BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.IntelligentMailPackageGenerator();
                                        break;
                                    default:
                                        break;
                                }
                                BarcodeControl.TabStop = bool.Parse(BarcodesParameters[12]);
                                // BarcodeControl.BinaryData=
                                BarcodeControl.AllowHtmlTextInToolTip = (DevExpress.Utils.DefaultBoolean)Enum.Parse(typeof(DevExpress.Utils.DefaultBoolean), BarcodesParameters[14]);
                                BarcodeControl.ShowToolTips = bool.Parse(BarcodesParameters[15]);
                                BarcodeControl.ToolTip = BarcodesParameters[16];
                                BarcodeControl.ToolTipIconType = (DevExpress.Utils.ToolTipIconType)Enum.Parse(typeof(DevExpress.Utils.ToolTipIconType), BarcodesParameters[17]);
                                BarcodeControl.ToolTipTitle = BarcodesParameters[18];
                                ReturnPanel.Controls.Add(BarcodeControl);
                            }

                            break;

                        case "CustomControl.OrientAbleTextControls.OrientedTextLabel":
                            {

                                CustomControl.OrientAbleTextControls.OrientedTextLabel OrientedTextLabelControl = new CustomControl.OrientAbleTextControls.OrientedTextLabel();
                                Color CntrlBackColorOrientedTextLabel = new Color();
                                Color CntrlForeColorOrientedTextLabel = new Color();
                                OrientedTextLabelControl.Name = GeneralParameters[1];
                                OrientedTextLabelControl.Text = GeneralParameters[2];
                                Font OrientedTextLabelControlFont = StringToFont(GeneralParameters[3]);
                                OrientedTextLabelControl.Font = OrientedTextLabelControlFont;
                                OrientedTextLabelControl.Location = new Point((int.Parse(GeneralParameters[4])), (int.Parse(GeneralParameters[5])));
                                OrientedTextLabelControl.Size = new Size((int.Parse(GeneralParameters[6])), (int.Parse(GeneralParameters[7])));
                                if (!String.IsNullOrWhiteSpace(GeneralParameters[8])) { Bitmap BitmapImage = new Bitmap(new MemoryStream(Convert.FromBase64String(GeneralParameters[8]))); OrientedTextLabelControl.Image = BitmapImage; }
                                CntrlBackColorOrientedTextLabel = ColorTranslator.FromHtml(GeneralParameters[9]);
                                OrientedTextLabelControl.BackColor = CntrlBackColorOrientedTextLabel;
                                CntrlForeColorOrientedTextLabel = ColorTranslator.FromHtml(GeneralParameters[10]);
                                OrientedTextLabelControl.ForeColor = CntrlForeColorOrientedTextLabel;
                                OrientedTextLabelControl.RightToLeft = (RightToLeft)Enum.Parse(typeof(RightToLeft), GeneralParameters[11]);
                                OrientedTextLabelControl.BackgroundImageLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), GeneralParameters[12]);
                                //OrientedTextLabelControl.Cursor /*I don't Know*/
                                OrientedTextLabelControl.AllowDrop = bool.Parse(GeneralParameters[14]);
                                OrientedTextLabelControl.Enabled = bool.Parse(GeneralParameters[15]);
                                OrientedTextLabelControl.TabIndex = int.Parse(GeneralParameters[16]);
                                OrientedTextLabelControl.Visible = bool.Parse(GeneralParameters[17]);
                                OrientedTextLabelControl.CausesValidation = bool.Parse(GeneralParameters[18]);
                                OrientedTextLabelControl.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), GeneralParameters[19]);
                                OrientedTextLabelControl.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), GeneralParameters[20]);
                                //OrientedTextLabelControl.Margin = StringTomargin;/*I don't Know*/
                                //OrientedTextLabelControl.padding = StringToPadding;/*I don't Know*/
                                OrientedTextLabelControl.MaximumSize = new Size((int.Parse(GeneralParameters[23])), (int.Parse(GeneralParameters[24])));
                                OrientedTextLabelControl.MinimumSize = new Size((int.Parse(GeneralParameters[25])), (int.Parse(GeneralParameters[26])));
                                OrientedTextLabelControl.UseWaitCursor = bool.Parse(GeneralParameters[27]);

                                OrientedTextLabelControl.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), OrientationLabelsParameters[0]);
                                OrientedTextLabelControl.FlatStyle = (FlatStyle)Enum.Parse(typeof(FlatStyle), OrientationLabelsParameters[1]);
                                OrientedTextLabelControl.ImageAlign = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), OrientationLabelsParameters[2]);
                                OrientedTextLabelControl.ImageIndex = int.Parse(OrientationLabelsParameters[3]);
                                OrientedTextLabelControl.ImageKey = OrientationLabelsParameters[4];
                                OrientedTextLabelControl.RotationAngle = double.Parse(OrientationLabelsParameters[5]);
                                OrientedTextLabelControl.TextAlign = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), OrientationLabelsParameters[6]);
                                OrientedTextLabelControl.TextDirection = (CustomControl.OrientAbleTextControls.Direction)Enum.Parse(typeof(CustomControl.OrientAbleTextControls.Direction), OrientationLabelsParameters[7]);
                                OrientedTextLabelControl.TextOrientation = (CustomControl.OrientAbleTextControls.Orientation)Enum.Parse(typeof(CustomControl.OrientAbleTextControls.Orientation), OrientationLabelsParameters[8]);
                                OrientedTextLabelControl.UseMnemonic = bool.Parse(OrientationLabelsParameters[9]);
                                OrientedTextLabelControl.AutoEllipsis = bool.Parse(OrientationLabelsParameters[10]);
                                OrientedTextLabelControl.UseCompatibleTextRendering = bool.Parse(OrientationLabelsParameters[11]);
                                OrientedTextLabelControl.AutoSize = bool.Parse(OrientationLabelsParameters[12]);

                                ReturnPanel.Controls.Add(OrientedTextLabelControl);

                            }

                            break;
                        default:
                            break;
                    }
                }
            }
            return ReturnPanel;

        }
        public static Bitmap ExportPicture(Panel ctl)
        {
            newImage = GetControlImage(ctl);
            Bitmap scale = GrayScale(newImage);
            scale = scale.Clone(new Rectangle(0, 0, ctl.Width, ctl.Height), PaintForm.CovertPanelToBitmap.PxFormat);
            scale.SetResolution(200, 200);
            newImage.Dispose();
            return scale;

        }


        public static Panel CopyControl(Control Ctrl)
        {
            Panel CopiedCtrlPanel = new Panel();
            CopiedCtrlPanel.Size = new Size(674, 425);
            try
            {
                string tmp = Ctrl.GetType().ToString();
                switch (tmp)
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
    }
}
