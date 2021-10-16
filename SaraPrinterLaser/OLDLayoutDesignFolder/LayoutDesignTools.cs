using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CivilAviationOrganization.LayoutDesignFolder
{
    public class LayoutDesignTools
    {
        #region attributes


        internal const string TextToImageSaveFolderName = "SaraLaserCardPrinter";
        internal const string TextToImageSaveImageFormat = "Bmp";
        internal Random random = new Random();
        internal const byte NameLantgh = 5;
        internal const float TextToImageResolution = 96;

        internal enum CardType
        {
            UltraLightLicence,
            CrewMemberCertificate_C_M_C_,
            CivilAviationSafetyInspectorCertificate,
            CivilAviationSecurityInspectorCertificate,

        }
        internal enum CardSide
        {
            Top,
            Bottom
        }
        internal enum FileType
        {
            EzdCadFiles,
            Text,
            Image,
            LaserBoardInternalSaveImage
        }
        #endregion
        #region Methods
        internal string ImageToBase64(string Path)
        {
            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
        internal Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }
        internal void ResizeArray<T>(ref T[,] original, int newCoNum, int newRoNum)
        {
            var newArray = new T[newCoNum, newRoNum];
            int columnCount = original.GetLength(1);
            int columnCount2 = newRoNum;
            int columns = original.GetUpperBound(0);
            for (int co = 0; co <= columns; co++)
                Array.Copy(original, co * columnCount, newArray, co * columnCount2, columnCount);
            original = newArray;
        }
        internal string GetSaveFolderAddress(CardType _CardType, FileType _FileType)
        {
            string tmp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + TextToImageSaveFolderName + "\\" + _CardType.ToString() + "\\" + _FileType.ToString();
            if (_FileType == FileType.LaserBoardInternalSaveImage || _FileType == FileType.EzdCadFiles)
                tmp += "\\" + DateConverter() + "=_=" + RandomString(NameLantgh + 15);
            if (!Directory.Exists(tmp)) Directory.CreateDirectory(tmp);
            return tmp;
        }
        internal Bitmap SaveLaserBoardInternalSaveImage(CardType PrintedCardType, CardSide _Side, ref string ImageSavePath,ref StatusClass ReturnStatus)
        {
            Bitmap bmptmp = (Bitmap)Laser.GetCurPreviewImage(2022, 1276,ref ReturnStatus);
            if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return null;
            try
            {
                switch (_Side)
                {
                    case CardSide.Top:
                        ImageSavePath = GetSaveFolderAddress(PrintedCardType, FileType.LaserBoardInternalSaveImage) + "\\" + "Top" + "." + TextToImageSaveImageFormat;
                        break;
                    case CardSide.Bottom:
                        ImageSavePath = GetSaveFolderAddress(PrintedCardType, FileType.LaserBoardInternalSaveImage) + "\\" + "Bottom" + "." + TextToImageSaveImageFormat;
                        break;
                    default:
                        break;
                }
                bmptmp.SetResolution(600, 600);
                bmptmp.Save(ImageSavePath, ImageFormat.Bmp);
                return bmptmp;
            }
            catch (Exception ex)
            {
                ReturnStatus.ResponseReturnStatus = StatusClass.ResponseStatus.Fail;
                ReturnStatus.ReturnDescription = StatusClass.Error_ImageGenerationFail + '\n' + ex.Message;
                return null;

            }

        }
        internal StatusClass SaveEzdFile(CardType PrintedCardType, CardSide _Side)
        {
            string ImageSavePath = "", ImageSaveDirectory = GetSaveFolderAddress(PrintedCardType, FileType.EzdCadFiles);
            switch (_Side)
            {
                case CardSide.Top:
                    ImageSavePath = ImageSaveDirectory + "\\" + "Top" + "." + "EZD";
                    break;
                case CardSide.Bottom:
                    ImageSavePath = ImageSaveDirectory + "\\" + "Bottom" + "." + "EZD";
                    break;
                default:
                    break;
            }
            Laser.LmcErrCode Response = Laser.SaveEntLibToFile(ImageSavePath);

            return Laser.GetLaserBoardDescription(Response);
        }
        internal string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        internal string DateConverter()
        {

            DateTime dateTimePicker = DateTime.Now;
            PersianCalendar PerCal = new PersianCalendar();

            string Year = PerCal.GetYear(dateTimePicker).ToString();
            string Month = PerCal.GetMonth(dateTimePicker).ToString();
            string Day = PerCal.GetDayOfMonth(dateTimePicker).ToString();
            string Hour = PerCal.GetHour(dateTimePicker).ToString();
            string Minute = PerCal.GetMinute(dateTimePicker).ToString();
            string Secound = PerCal.GetSecond(dateTimePicker).ToString();
            string MiliSecound = PerCal.GetMilliseconds(dateTimePicker).ToString();
            if (Day.Length == 1)
            {
                Day = PerCal.GetDayOfMonth(dateTimePicker).ToString().Insert(0, "0");
            }
            if (Month.Length == 1)
            {
                Month = PerCal.GetMonth(dateTimePicker).ToString().Insert(0, "0");
            }
            string strDate = Year + "_" + Month + "_" + Day + "-_-_-" + Hour + "_" + Minute + "_" + Secound + "_" + MiliSecound;
            return strDate;
        }
        #endregion
    }
}
//internal ReturnStatus ArrangeItemsOnTheCard(CardType PrintedCardType, EntryTextProperty[] EntryTexts, EntryPictureProperty[] EntryPictures, FullLaserPen[] ParametersOfTheItems)
//{
//    ReturnStatus _ReturnStatus = ReturnStatus.Fail;
//    int NumberOfItems = 0;
//    switch (PrintedCardType)
//    {
//        case CardType.UltraLightLicence: NumberOfItems = 23; break;
//        case CardType.CrewMemberCertificate_C_M_C_: NumberOfItems = 23; break;
//        case CardType.CivilAviationSafetyInspectorCertificate: NumberOfItems = 23; break;
//        case CardType.CivilAviationSecurityInspectorCertificate: NumberOfItems = 23; break;
//        default:
//            break;
//    }


//    ////Entry Text 
//    //for (int i = 0; i < NumberOfItems; i++)
//    //{

//    //    AddTextOnDevice





//    //                }



//    return _ReturnStatus;
//}