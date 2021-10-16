using SaraPrinterLaser.Hardware;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static SaraPrinterLaser.LayoutDesignFolder.LayoutDesignTools;

namespace SaraPrinterLaser.LayoutDesignFolder
{
    public class EntryTextProperty
    {
        #region properties
        public enum TextLanguage
        {
            Persian,
            Latin
        }
        public string EntryText { get; set; }
        public Font EntryTextFont { get; set; }
        public TextLanguage EntryTextFontLanguage { get; set; }
        public float TextImageResolution { get; set; }
        public double XPos { get; set; }
        public double YPos { get; set; }
        public CAOspecificCardLaserPen TextLaserParameters { get; set; }
        public string DatabaseRowName { get; set; }
        #endregion
        #region Methods
        internal StatusClass AddTextOnDevice(CardType PrintedCardType, EntryTextProperty EntryTexts)
        {
            LaserConfigClass myconfig = LaserConfigClass.load();
            LayoutDesignTools tools = new LayoutDesignTools();
            string TextRandomName = bl.Auth.UserName + "_" + tools.DateConverter() + "." + TextToImageSaveImageFormat;
            string ImageSavePath = tools.GetSaveFolderAddress(PrintedCardType, FileType.Text) + "\\" + TextRandomName;

            StatusClass ReturnStatus = new StatusClass();
            TextToImage(EntryTexts.EntryText, EntryTexts.EntryTextFont, EntryTexts.EntryTextFontLanguage, EntryTexts.TextImageResolution, ref ReturnStatus).Save(ImageSavePath, ImageFormat.Bmp);
            if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;

            Laser.LmcErrCode Response = Laser.SetPenParam(EntryTexts.TextLaserParameters.nPenNo, EntryTexts.TextLaserParameters.nMarkLoop, EntryTexts.TextLaserParameters.dMarkSpeed, EntryTexts.TextLaserParameters.dPowerRatio, EntryTexts.TextLaserParameters.dCurrent, EntryTexts.TextLaserParameters.nFreq, EntryTexts.TextLaserParameters.dQPulseWidth, EntryTexts.TextLaserParameters.nStartTC, EntryTexts.TextLaserParameters.nLaserOffTC, EntryTexts.TextLaserParameters.nEndTC, EntryTexts.TextLaserParameters.nPolyTC, EntryTexts.TextLaserParameters.dJumpSpeed, EntryTexts.TextLaserParameters.nJumpPosTC, EntryTexts.TextLaserParameters.nJumpDistTC, EntryTexts.TextLaserParameters.dEndComp, EntryTexts.TextLaserParameters.dAccDist, EntryTexts.TextLaserParameters.dPointTime, EntryTexts.TextLaserParameters.bPulsePointMode, EntryTexts.TextLaserParameters.nPulseNum, EntryTexts.TextLaserParameters.dFlySpeed); if (Response != Laser.LmcErrCode.LMC1_ERR_SUCCESS) return Laser.GetLaserBoardDescription(Response);


            Response = Laser.AddFileToLib(ImageSavePath, TextRandomName, myconfig.Xcenter + EntryTexts.XPos, myconfig.Ycenter + EntryTexts.YPos, 0, EntryTexts.TextLaserParameters.nAlign, EntryTexts.TextLaserParameters.dRatio, EntryTexts.TextLaserParameters.nPenNo, EntryTexts.TextLaserParameters.bHatchFile); if (Response != Laser.LmcErrCode.LMC1_ERR_SUCCESS) return Laser.GetLaserBoardDescription(Response);

            Response = Laser.SetBitmapEntParam(TextRandomName, TextRandomName, EntryTexts.TextLaserParameters.nbmpttrib, EntryTexts.TextLaserParameters.nbmpScanAttr, EntryTexts.TextLaserParameters.dBrightness, EntryTexts.TextLaserParameters.dContrast, EntryTexts.TextLaserParameters.dSettingPointTime, EntryTexts.TextLaserParameters.ndpi, EntryTexts.TextLaserParameters.blDisableMarkLowGray, EntryTexts.TextLaserParameters.nminLowGrayPt); if (Response != Laser.LmcErrCode.LMC1_ERR_SUCCESS) return Laser.GetLaserBoardDescription(Response);

            return Laser.GetLaserBoardDescription(Laser.LmcErrCode.LMC1_ERR_SUCCESS);
        }
        
        /// <summary>
        /// Convert text string To Bitmap
        /// </summary>
        /// <param name="EntryText"></param>
        /// <param name="TextFont"></param>
        /// <param name="TextDirection"> Persian or Latin Text </param>
        /// <returns> Image </returns>
        internal Bitmap TextToImage(string EntryText, Font TextFont, float _TextImageResolution, ref StatusClass ReturnStatus)
        {
            ReturnStatus.ResponseReturnStatus = StatusClass.ResponseStatus.Ok;
            ReturnStatus.ReturnDescription = StatusClass.Message_SucsessOpration;
            if (_TextImageResolution == 0) _TextImageResolution = 96;
            try
            {
                float penSize = 1f;
                using (var path = new GraphicsPath())
                {
                    path.AddString(EntryText, TextFont.FontFamily, (int)TextFont.Style, TextFont.Size, Point.Empty, StringFormat.GenericTypographic);
                    RectangleF textBounds = path.GetBounds();
                    using (var bitmap = new Bitmap((int)textBounds.Width, (int)textBounds.Height, PixelFormat.Format24bppRgb))
                    using (var g = Graphics.FromImage(bitmap))
                    using (var brush = new SolidBrush(Color.Black))
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        // When rendering without a GraphicsPath object
                        //g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                        g.Clear(Color.White);
                        g.TranslateTransform(-(textBounds.X + penSize), -(textBounds.Y + penSize));
                        g.FillPath(brush, path);
                        bitmap.SetResolution(_TextImageResolution, _TextImageResolution);
                        //  bitmap.Save(@"F:/Mehrdad.bmp", ImageFormat.Bmp);
                        return (Bitmap)bitmap.Clone();
                        // Or: return bitmap; declaring the bitmap without a using statement
                    }
                }
            }
            catch (Exception ex)
            {
                ReturnStatus.ResponseReturnStatus = StatusClass.ResponseStatus.Fail;
                ReturnStatus.ReturnDescription = StatusClass.Error_TextToImageFunctionFailOccure + '\n' + ex.Message;
                return null;
            }
        }



        private Bitmap TextToImage(string EntryText, Font TextFont, EntryTextProperty.TextLanguage _TextLanguage, float _TextImageResolution, ref StatusClass ReturnStatus)
        {
            //1.01
            const double FixedcoefficientWidth = 1.03, Fixedcoefficientheight = 0.95;
            ReturnStatus.ResponseReturnStatus = StatusClass.ResponseStatus.Ok;
            ReturnStatus.ReturnDescription = StatusClass.Message_SucsessOpration;
            if (_TextImageResolution == 0) _TextImageResolution = 96;
            try
            {
                RightToLeft _TextDirection = RightToLeft.Yes;
                switch (_TextLanguage)
                {
                    case EntryTextProperty.TextLanguage.Persian:
                        _TextDirection = RightToLeft.Yes;
                        break;
                    case EntryTextProperty.TextLanguage.Latin:
                        _TextDirection = RightToLeft.No;
                        break;
                    default:
                        break;
                }
                var SizeFtmp = TextRenderer.MeasureText(EntryText, TextFont);
                int TextWidth = Convert.ToInt32(SizeFtmp.Width * FixedcoefficientWidth);
                int TextHeight = Convert.ToInt32(SizeFtmp.Height * Fixedcoefficientheight);
                Bitmap bmptmp = new Bitmap(TextWidth, TextHeight);
                Label lbltmp = new Label
                {
                    Font = TextFont,
                    Text = EntryText,
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    RightToLeft = _TextDirection,
                    TextAlign = ContentAlignment.MiddleCenter,
                    UseCompatibleTextRendering = true,
                    Size = new Size(TextWidth, TextHeight),
                };
                lbltmp.DrawToBitmap(bmptmp, new Rectangle(0, 0, TextWidth, TextHeight));
                bmptmp.SetResolution(_TextImageResolution, _TextImageResolution);

                lbltmp.Dispose();
                return MakeGrayscale3(bmptmp);
            }
            catch (Exception ex)
            {
                ReturnStatus.ResponseReturnStatus = StatusClass.ResponseStatus.Fail;
                ReturnStatus.ReturnDescription = StatusClass.Error_TextToImageFunctionFailOccure + '\n' + ex.Message;
                return null;
            }
        }
        private Bitmap MakeGrayscale3(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            using (Graphics g = Graphics.FromImage(newBitmap))
            {

                //create the grayscale ColorMatrix
                ColorMatrix colorMatrix = new ColorMatrix(
                   new float[][]
                   {
             new float[] {.3f, .3f, .3f, 0, 0},
             new float[] {.59f, .59f, .59f, 0, 0},
             new float[] {.11f, .11f, .11f, 0, 0},
             new float[] {0, 0, 0, 1, 0},
             new float[] {0, 0, 0, 0, 1}
                   });

                //create some image attributes
                using (ImageAttributes attributes = new ImageAttributes())
                {

                    //set the color matrix attribute
                    attributes.SetColorMatrix(colorMatrix);

                    //draw the original image on the new image
                    //using the grayscale color matrix
                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                                0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return newBitmap;
        }
        private double GetGreyLevel(byte r, byte g, byte b)
        {
            return (r * 0.299 + g * 0.587 + b * 0.114) / 255;
        }
        private Bitmap ConvertTo1Bit(Bitmap input)
        {
            var masks = new byte[] { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };
            var output = new Bitmap(input.Width, input.Height, PixelFormat.Format1bppIndexed);
            var data = new sbyte[input.Width, input.Height];
            var inputData = input.LockBits(new Rectangle(0, 0, input.Width, input.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            try
            {
                var scanLine = inputData.Scan0;
                var line = new byte[inputData.Stride];
                for (var y = 0; y < inputData.Height; y++, scanLine += inputData.Stride)
                {
                    Marshal.Copy(scanLine, line, 0, line.Length);
                    for (var x = 0; x < input.Width; x++)
                    {
                        data[x, y] = (sbyte)(64 * (GetGreyLevel(line[x * 3 + 2], line[x * 3 + 1], line[x * 3 + 0]) - 0.5));
                    }
                }
            }
            finally
            {
                input.UnlockBits(inputData);
            }
            var outputData = output.LockBits(new Rectangle(0, 0, output.Width, output.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);
            try
            {
                var scanLine = outputData.Scan0;
                for (var y = 0; y < outputData.Height; y++, scanLine += outputData.Stride)
                {
                    var line = new byte[outputData.Stride];
                    for (var x = 0; x < input.Width; x++)
                    {
                        var j = data[x, y] > 0;
                        if (j) line[x / 8] |= masks[x % 8];
                        var error = (sbyte)(data[x, y] - (j ? 32 : -32));
                        if (x < input.Width - 1) data[x + 1, y] += (sbyte)(7 * error / 16);
                        if (y < input.Height - 1)
                        {
                            if (x > 0) data[x - 1, y + 1] += (sbyte)(3 * error / 16);
                            data[x, y + 1] += (sbyte)(5 * error / 16);
                            if (x < input.Width - 1) data[x + 1, y + 1] += (sbyte)(1 * error / 16);
                        }
                    }
                    Marshal.Copy(line, 0, scanLine, outputData.Stride);
                }
            }
            finally
            {
                output.UnlockBits(outputData);
            }
            return output;
        }
        #endregion

    }
}
