using SaraPrinterLaser.Hardware;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SaraPrinterLaser.LayoutDesignFolder
{
    public class LayoutDesignTools
    {
        #region attributes
        public static readonly Dictionary<string, string> DicNationality = new Dictionary<string, string>()
       {
            {"Iranian","IRN"},
            { "Afghan","AFG"},
            {"Albanian","ALB"},
            {"Algerian","DZA"},
            {"Argentinian","ARG"},
            {"Australian","AUS"},
            {"Austrian","AUT"},
            {"Bangladeshi","BGD"},
            {"Belgian","BEL"},
            {"Bolivian","BOL"},
            {"Botswanan","BWA"},
            {"Brazilian","BRA"},
            {"Bulgarian","BGR"},
            {"Cambodian","KHM"},
            {"Cameroonian","CMR"},
            {"Canadian","CAN"},
            {"Chilean","CHL"},
            {"Chinese","CHN"},
            {"Colombian","COL"},
            {"CostaRican","CRI"},
            {"Croat","HRV"},
            {"Cuban","CUB"},
            {"Czech","CZE"},
            {"Dane","DNK"},
            {"Dominican","DOM"},
            {"Ecuadorian","ECU"},
            {"Egyptian","EGY"},
            {"Salvadoran","SLV"},
            {"Estonian","EST"},
            {"Ethiopian","ETH"},
            {"Fijian","FJI"},
            {"Finn","FIN"},
            {"French","FRA"},
            {"German","DEU"},
            {"Ghanaian","GHA"},
            {"Greek","GRC"},
            {"Guatemalan","GTM"},
            {"Haitian","HTI"},
            {"Honduran","HND"},
            {"Hungarian","HUN"},
            {"Icelander","ISL"},
            {"Indian","IND"},
            {"Indonesian","IDN"},
            {"Iraqi","IRQ"},
            {"Irish","IRL"},
            {"Israeli","ISR"},
            {"Italian","ITA"},
            {"Jamaican","JAM"},
            {"Japanese","JPN"},
            {"Jordanian","JOR"},
            {"Kenyan","KEN"},
            {"Korean","KOR"},
            {"Kuwaiti","KWT"},
            {"Laotain","LAO"},
            {"Latvian","LVA"},
            {"Lebanese","LBN"},
            {"Libyan","LBY"},
            {"Lithuanian","LIE"},
            {"Malagasy","MWI"},
            {"Malaysian","MYS"},
            {"Malian","MDV"},
            {"Maltese","MLT"},
            {"Mexican","MEX"},
            {"Mongolian","MNG"},
            {"Moroccan","MAR"},
            {"Mozambican","MOZ"},
            {"Nambian","NAM"},
            {"Nepalese","NPL"},
            {"Dutchman","NLD"},
            {"NewZealander","NZL"},
            {"Nicaraguan","NIC"},
            {"Nigerian","NER"},
            {"Norwegian","NGA"},
            {"Pakistani","PAK"},
            {"Panamanian","PAN"},
            {"Paraguayan","PRY"},
            {"Peruvian","PER"},
            {"Filipino","PHL"},
            {"Pole","POL"},
            {"Portuguese","PRT"},
            {"Romanian","ROU"},
            {"Russian","RUS"},
            {"Saudi","SAU"},
            {"Senegalese","SEN"},
            {"Serbian","SRB"},
            {"Singaporean","SGP"},
            {"Slovak","SXM"},
            {"SouthAfrican","ZAF"},
            {"Spaniard","ESP"},
            {"SriLankan","LKA"},
            {"Sudanese","SDN"},
            {"Swede","SWE"},
            {"Swiss","CHE"},
            {"Syrian","SYR"},
            {"Taiwanese","TWN"},
            {"Tajikistani","TJK"},
            {"Thai","THA"},
            {"Tongan","TKL"},
            {"Tunisian","TUN"},
            {"Turk","TUR"},
            {"Ukranian","UKR"},
            {"Emirati","ARE"},
            {"British","GBR"},
            {"American","USA"},
            {"Uruguayan","URY"},
            {"Venezuelan","VEN"},
            {"Vietnamese","VNM"},
            {"Yemenian","YEM"},
            {"Zambian","ZMB"},
            {"Zimbabwean","ZWE"},
        };
        public static bool flgDefinedCardPrinted { get; set; }
        public static string FolderOfCaoEzdFile = Path.GetTempPath() + "\\" + "FolderOfCaoEzdFile";
        public static string PrintTopEzdFile = FolderOfCaoEzdFile + "\\" + "TopCaoCards.ezd";
        public static string PrintBottomEzdFile = FolderOfCaoEzdFile + "\\" + "BottomCaoCards.ezd";
        internal const string TextToImageSaveFolderName = "SaraLaserCardPrinter";
        internal const string TextToImageSaveImageFormat = "Bmp";
        internal Random random = new Random();
        internal const byte NameLantgh = 5;
        internal const float TextToImageResolution = 96;
        internal const string defaultParametersName = "defaultParameters";
        internal static CAOspecificCardLaserPen defaultLaserParametersName = new dl.ParametersSave().ParametersLoad(defaultParametersName);
        public static readonly string MaleSexuality = "Male";
        public static readonly string FemaleSexuality = "Female";
        public enum ItemsOnTheCards_Text
        {
            CivilAviationInspectorCertificate_LatinVariableTopItem_Name,
            CivilAviationInspectorCertificate_PersianVariableTopItem_Name,
            CivilAviationInspectorCertificate_LatinVariableTopItem_Nationality,
            CivilAviationInspectorCertificate_LatinVariableTopItem_DateOfBrith,
            CivilAviationInspectorCertificate_LatinVariableTopItem_CardNo,
            CivilAviationInspectorCertificate_LatinVariableBottomItem_DateOfExpiry,
            CivilAviationInspectorCertificate_PersianVariableBottomItem_DateOfExpiry,
            CivilAviationInspectorCertificate_LatinVariableBottomItem_MRZ0,
            CivilAviationInspectorCertificate_LatinVariableBottomItem_MRZ1,
            CivilAviationInspectorCertificate_LatinVariableBottomItem_MRZ2,

            CrewMemberCertificate_C_M_C_VariableTopItem_Surname,
            CrewMemberCertificate_C_M_C_VariableTopItem_GivenName,
            CrewMemberCertificate_C_M_C_VariableTopItem_Sex,
            CrewMemberCertificate_C_M_C_VariableTopItem_Nationality,
            CrewMemberCertificate_C_M_C_VariableTopItem_DateOfBrith,
            CrewMemberCertificate_C_M_C_VariableTopItem_Employedby,
            CrewMemberCertificate_C_M_C_VariableTopItem_Occupation,
            CrewMemberCertificate_C_M_C_VariableTopItem_DocNo,
            CrewMemberCertificate_C_M_C_VariableTopItem_DateOfExpiry,
            CrewMemberCertificate_C_M_C_VariableBottomItem_MRZ0,
            CrewMemberCertificate_C_M_C_VariableBottomItem_MRZ1,
            CrewMemberCertificate_C_M_C_VariableBottomItem_MRZ2,

            UltraLightLicenceCard_VariableTopItem_Number,
            UltraLightLicenceCard_VariableTopItem_Name,
            UltraLightLicenceCard_VariableTopItem_DateOfBrith,
            UltraLightLicenceCard_VariableTopItem_Nationality,
            UltraLightLicenceCard_VariableTopItem_DateOfIssue,
            UltraLightLicenceCard_VariableTopItem_DateOfExpiry,
            UltraLightLicenceCard_VariableTopItem_Authority,
            UltraLightLicenceCard_VariableBottomItem_Remarks,
            UltraLightLicenceCard_VariableBottomItem_Ratings

        }
        public enum ItemsOnTheCards_Picture
        {
            CivilAviationInspectorCertificate_VariableTopItem_PersonalPicture,
            CivilAviationInspectorCertificate_VariableTopItem_SingniturePicture,
            CivilAviationInspectorCertificate_VariableBottomItem_IssuingAuthoritySingniturePicture,

            CrewMemberCertificate_C_M_C_VariableTopItem_PersonalPicture,
            CrewMemberCertificate_C_M_C_VariableTopItem_SingniturePicture,
            CrewMemberCertificate_C_M_C_VariableBottomItem_IssuingAuthoritySingniturePicture,

            UltraLightLicenceCard_VariableTopItem_PersonalPicture,
            UltraLightLicenceCard_VariableTopItem_SingniturePicture,
        }
        public enum CardType
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
        /// <summary>
        /// Convert Image To Base64 Format for Save on the Database
        /// </summary>
        /// <param name="Path"> Image Address</param>
        /// <returns> String Base64</returns>
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
                tmp += "\\" + bl.Auth.UserName + "_" + DateConverter();
            if (!Directory.Exists(tmp)) Directory.CreateDirectory(tmp);
            return tmp;
        }
        internal Bitmap SaveLaserBoardInternalSaveImage(CardType PrintedCardType, CardSide _Side, ref string ImageSavePath, ref StatusClass ReturnStatus)
        {
            Bitmap bmptmp = (Bitmap)Laser.GetCurPreviewImage(2022, 1276, ref ReturnStatus);
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

            Laser.LmcErrCode Response = Laser.LmcErrCode.LMC1_ERR_UNKNOW;
            string ImageSavePath = "";
            if (!Directory.Exists(FolderOfCaoEzdFile)) Directory.CreateDirectory(FolderOfCaoEzdFile);
            switch (_Side)
            {
                case CardSide.Top:
                    ImageSavePath = GetSaveFolderAddress(PrintedCardType, FileType.EzdCadFiles) + "\\" + "Top" + "." + "EZD";
                    Response = Laser.SaveEntLibToFile(ImageSavePath); if (Response != Laser.LmcErrCode.LMC1_ERR_SUCCESS) return Laser.GetLaserBoardDescription(Response);
                    Response = Laser.SaveEntLibToFile(PrintTopEzdFile);
                    break;
                case CardSide.Bottom:
                    ImageSavePath = GetSaveFolderAddress(PrintedCardType, FileType.EzdCadFiles) + "\\" + "Bottom" + "." + "EZD";
                    Response = Laser.SaveEntLibToFile(ImageSavePath); if (Response != Laser.LmcErrCode.LMC1_ERR_SUCCESS) return Laser.GetLaserBoardDescription(Response);
                    Response = Laser.SaveEntLibToFile(PrintBottomEzdFile);
                    break;
                default:
                    break;
            }


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
            string strDate = Year + "_" + Month + "_" + Day + "_" + Hour + "_" + Minute + "_" + Secound + "_" + MiliSecound;
            return strDate;
        }
        public double[] WithDPIconvertPxTomm(Size Pixellocation, float DPIX, float DPIY)
        {

            double[] Location = new double[2];
            double XPos = 0, YPos = 0;
            XPos = (Pixellocation.Width * 25.4) / DPIX;
            YPos = (Pixellocation.Height * 25.4) / DPIY;
            Location[0] = XPos;
            Location[1] = YPos;
            return Location;
        }
        public Size WithDPIConvermmtoPx(double XmmLocation, double YmmLocation, float DPIX, float DPIY)
        {

            Size Location = new Size();
            double XPos = 0, YPos = 0;
            XPos = (XmmLocation * DPIX) / 25.4;
            YPos = (YmmLocation * DPIY) / 25.4;
            Location.Width = (int)Math.Ceiling(XPos);
            Location.Height = (int)Math.Ceiling(YPos);
            return Location;
        }
        public Image RResizeImage(Image img, int iWidth, int iHeight)
        {
            Bitmap bmp = new Bitmap(iWidth, iHeight);
            Graphics graphic = Graphics.FromImage((Image)bmp);
            graphic.DrawImage(img, 0, 0, iWidth, iHeight);

            return (Image)bmp;
        }
        internal double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
        #endregion

        



    }
}
