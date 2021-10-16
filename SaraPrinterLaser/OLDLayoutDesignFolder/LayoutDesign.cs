using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SaraPrinterLaser.Hardware;
using SaraPrinterLaser.LayoutDesignFolder;

namespace SaraPrinterLaser
{
    public static class LayoutDesign
    {
        private const string TextToImageSaveFolderName = "DetailOnTheCardSave";
        private const string TextToImageSaveImageFormat = "Bmp";
        private static Random random = new Random();
        private const byte NameLantgh = 20;

        public enum ReturnStatus
        {
            ok,
            Fail,
            
        }
        public enum CardType
        {
            UltraLightLicence,
            CrewMemberCertificate_C_M_C_,
            CivilAviationSafetyInspectorCertificate,
            CivilAviationSafetyInspectorCertificate2,
            CivilAviationSecurityInspectorCertificate,

        }
        private static ReturnStatus AddPictureOnDevice(CardType PrintedCardType, int Counter, EntryPictureProperty EntryPicture, FullLaserPen ParametersOfTheItems)
        {
            ReturnStatus _ReturnStatus = ReturnStatus.ok;
            const string ItemType = "Image";
            string ImageSavePath = "", ImageSaveDirectory = "", strCardType = "", TextRandomName = "";
            

            

            if (Laser.ClearLibAllEntity() != Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                return ReturnStatus.Fail;

            strCardType = PrintedCardType.ToString();

            if (!Directory.Exists(Path.GetTempPath() + "\\" + TextToImageSaveFolderName))
                Directory.CreateDirectory(Path.GetTempPath() + "\\" + TextToImageSaveFolderName);

            if (!Directory.Exists(Path.GetTempPath() + "\\" + TextToImageSaveFolderName + "\\" + strCardType))
                Directory.CreateDirectory(Path.GetTempPath() + "\\" + TextToImageSaveFolderName + "\\" + strCardType);

            if (!Directory.Exists(Path.GetTempPath() + "\\" + TextToImageSaveFolderName + "\\" + strCardType + "\\" + ItemType))
                Directory.CreateDirectory(Path.GetTempPath() + "\\" + TextToImageSaveFolderName + "\\" + strCardType + "\\" + ItemType);

            ImageSaveDirectory = Path.GetTempPath() + "\\" + TextToImageSaveFolderName + "\\" + strCardType + "\\" + ItemType;
            do { TextRandomName = RandomString(NameLantgh); }
            while (File.Exists(ImageSaveDirectory + "\\" + TextRandomName + "." + TextToImageSaveImageFormat));

            ImageSavePath = ImageSaveDirectory + "\\" + TextRandomName + "." + TextToImageSaveImageFormat;

            File.Copy(EntryPicture.EntryPicturePath, ImageSavePath, true);
            

            if (Laser.SetPenParam(Counter, ParametersOfTheItems.nMarkLoop, ParametersOfTheItems.dMarkSpeed, ParametersOfTheItems.dPowerRatio, ParametersOfTheItems.dCurrent, ParametersOfTheItems.nFreq, ParametersOfTheItems.dQPulseWidth, ParametersOfTheItems.nStartTC, ParametersOfTheItems.nLaserOffTC, ParametersOfTheItems.nEndTC, ParametersOfTheItems.nPolyTC, ParametersOfTheItems.dJumpSpeed, ParametersOfTheItems.nJumpPosTC, ParametersOfTheItems.nJumpDistTC, ParametersOfTheItems.dEndComp, ParametersOfTheItems.dAccDist, ParametersOfTheItems.dPointTime, ParametersOfTheItems.bPulsePointMode, ParametersOfTheItems.nPulseNum, ParametersOfTheItems.dFlySpeed) != Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                return ReturnStatus.Fail;

            if (Laser.AddFileToLib(ImageSavePath, TextRandomName, EntryPicture.XPos, EntryPicture.YPos, 0, ParametersOfTheItems.nAlign, ParametersOfTheItems.dRatio, Counter, ParametersOfTheItems.bHatchFile) != Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                return ReturnStatus.Fail;

            if (Laser.SetBitmapEntParam(TextRandomName, TextRandomName, ParametersOfTheItems.nbmpttrib, ParametersOfTheItems.nbmpScanAttr, ParametersOfTheItems.dBrightness, ParametersOfTheItems.dContrast, ParametersOfTheItems.dSettingPointTime, ParametersOfTheItems.ndpi, ParametersOfTheItems.blDisableMarkLowGray, ParametersOfTheItems.nminLowGrayPt) != Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                return ReturnStatus.Fail;

            return _ReturnStatus;


        }

        private static ReturnStatus AddTextOnDevice(CardType PrintedCardType, int Counter, EntryTextProperty EntryTexts, FullLaserPen ParametersOfTheItems)
        {
            ReturnStatus _ReturnStatus = ReturnStatus.ok;
            const string ItemType = "Texts";
            string ImageSavePath = "", ImageSaveDirectory = "", strCardType = "", TextRandomName = "";

            if (Laser.ClearLibAllEntity() != Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                return ReturnStatus.Fail;

            strCardType = PrintedCardType.ToString();

            if (!Directory.Exists(Path.GetTempPath() + "\\" + TextToImageSaveFolderName))
                Directory.CreateDirectory(Path.GetTempPath() + "\\" + TextToImageSaveFolderName);
            if (!Directory.Exists(Path.GetTempPath() + "\\" + TextToImageSaveFolderName + "\\" + strCardType))
                Directory.CreateDirectory(Path.GetTempPath() + "\\" + TextToImageSaveFolderName + "\\" + strCardType);
            if (!Directory.Exists(Path.GetTempPath() + "\\" + TextToImageSaveFolderName + "\\" + strCardType + "\\" + ItemType))
                Directory.CreateDirectory(Path.GetTempPath() + "\\" + TextToImageSaveFolderName + "\\" + strCardType + "\\" + ItemType);

            ImageSaveDirectory = Path.GetTempPath() + "\\" + TextToImageSaveFolderName + "\\" + strCardType + "\\" + ItemType;
            do { TextRandomName = RandomString(NameLantgh); }
            while (File.Exists(ImageSaveDirectory + "\\" + TextRandomName + "." + TextToImageSaveImageFormat));

            ImageSavePath = ImageSaveDirectory + "\\" + TextRandomName + "." + TextToImageSaveImageFormat;

            TextToImage(EntryTexts.EntryText, EntryTexts.EntryTextFont, EntryTexts.EntryTextFontLanguage).Save(ImageSavePath, ImageFormat.Bmp);

            if (Laser.SetPenParam(Counter, ParametersOfTheItems.nMarkLoop, ParametersOfTheItems.dMarkSpeed, ParametersOfTheItems.dPowerRatio, ParametersOfTheItems.dCurrent, ParametersOfTheItems.nFreq, ParametersOfTheItems.dQPulseWidth, ParametersOfTheItems.nStartTC, ParametersOfTheItems.nLaserOffTC, ParametersOfTheItems.nEndTC, ParametersOfTheItems.nPolyTC, ParametersOfTheItems.dJumpSpeed, ParametersOfTheItems.nJumpPosTC, ParametersOfTheItems.nJumpDistTC, ParametersOfTheItems.dEndComp, ParametersOfTheItems.dAccDist, ParametersOfTheItems.dPointTime, ParametersOfTheItems.bPulsePointMode, ParametersOfTheItems.nPulseNum, ParametersOfTheItems.dFlySpeed) != Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                return ReturnStatus.Fail;

            if (Laser.AddFileToLib(ImageSavePath, TextRandomName, EntryTexts.XPos, EntryTexts.YPos, 0, ParametersOfTheItems.nAlign, ParametersOfTheItems.dRatio, Counter, ParametersOfTheItems.bHatchFile) != Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                return ReturnStatus.Fail;

            if (Laser.SetBitmapEntParam(TextRandomName, TextRandomName, ParametersOfTheItems.nbmpttrib, ParametersOfTheItems.nbmpScanAttr, ParametersOfTheItems.dBrightness, ParametersOfTheItems.dContrast, ParametersOfTheItems.dSettingPointTime, ParametersOfTheItems.ndpi, ParametersOfTheItems.blDisableMarkLowGray, ParametersOfTheItems.nminLowGrayPt) != Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                return ReturnStatus.Fail;

            return _ReturnStatus;

        }

        public static ReturnStatus ArrangeItemsOnTheCard(CardType PrintedCardType, EntryTextProperty[] EntryTexts, EntryPictureProperty[] EntryPictures, FullLaserPen[] ParametersOfTheItems)
        {
            ReturnStatus _ReturnStatus = ReturnStatus.Fail;
            int NumberOfItems = 0;
            switch (PrintedCardType)
            {
                case CardType.UltraLightLicence: NumberOfItems = 23; break;
                case CardType.CrewMemberCertificate_C_M_C_: NumberOfItems = 23; break;
                case CardType.CivilAviationSafetyInspectorCertificate: NumberOfItems = 23; break;
                case CardType.CivilAviationSafetyInspectorCertificate2: NumberOfItems = 23; break;
                case CardType.CivilAviationSecurityInspectorCertificate: NumberOfItems = 23; break;
                default:
                    break;
            }


            ////Entry Text 
            //for (int i = 0; i < NumberOfItems; i++)
            //{

            //    AddTextOnDevice





            //                }



            return _ReturnStatus;
        }



        /// <summary>
        /// Convert text string To Bitmam
        /// </summary>
        /// <param name="EntryText"></param>
        /// <param name="TextFont"></param>
        /// <param name="TextDirection"> Persian or Latin Text </param>
        /// <returns> Image </returns>
        private static Bitmap TextToImage(string EntryText, Font TextFont, EntryTextProperty.TextLanguage _TextLanguage)
        {
            const double FixedcoefficientWidth = 1.01, Fixedcoefficientheight = 0.9;
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
            Label lbltmp = new Label();
            lbltmp.Font = TextFont;
            lbltmp.Text = EntryText;
            lbltmp.BackColor = Color.Transparent;
            lbltmp.ForeColor = Color.Black;
            lbltmp.RightToLeft = _TextDirection;
            lbltmp.TextAlign = ContentAlignment.MiddleCenter;
            lbltmp.UseCompatibleTextRendering = true;
            lbltmp.Size = new Size(TextWidth, TextHeight);
            lbltmp.DrawToBitmap(bmptmp, new Rectangle(0, 0, TextWidth, TextHeight));
            return bmptmp;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }



    }
}
