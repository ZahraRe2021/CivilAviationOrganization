using SaraPrinterLaser.Hardware;
using System;
using System.Drawing;
using static SaraPrinterLaser.LayoutDesignFolder.LayoutDesignTools;

namespace SaraPrinterLaser.LayoutDesignFolder
{
    public class CivilAviationInspectorCertificate
    {

        #region attributes


        public static readonly string SAFETYINSPECTOR = "SAFETY INSPECTOR بازرس ایمنی هوانوردی";
        public static readonly string SECURITYINSPECTOR = "SECURITY INSPECTOR بازرس امنیت هوانوردی";
        public static readonly string HEALTHINSPECTOR = "HEALTH INSPECTOR بازرس بهداشت هوانوردی";
        public static bool flgSAFETYINSPECTOR { get; set; }
        public static bool flgSECURITYINSPECTOR { get; set; }
        public static bool flgHEALTHINSPECTOR { get; set; }
        public enum INSPECTORCardType
        {
            Safety,
            Security,
            Health
        }

        private static readonly Font MRZFont = new Font("OCR-B 10 BT", 20, FontStyle.Regular);

        private static readonly Font NormalFont = new Font("Arial", 10, FontStyle.Regular);
        private static readonly Font SmallFont = new Font("Arial", 8, FontStyle.Regular);
        #endregion
        #region FixedItems_TopOftheCard
        public static CAOspecificCardLaserPen TitleFixTopItemLaserParameters = defaultLaserParametersName;
        public static Font TitleFixTopItemFont = NormalFont;
        private static readonly EntryTextProperty TitleFixedItemTopItem_SAFETYINSPECTOR = new EntryTextProperty
        {
            EntryText = "SAFETY INSPECTOR  بازرس ایمنی هوانوردی      ",
            EntryTextFont = TitleFixTopItemFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = TitleFixTopItemLaserParameters
        };
        private static readonly EntryTextProperty TitleFixedItemTopItem_SecurityINSPECTOR = new EntryTextProperty
        {
            EntryText = "SECURITY INSPECTOR  بازرس امنیت هوانوردی      ",
            EntryTextFont = TitleFixTopItemFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = TitleFixTopItemLaserParameters
        };


        public static CAOspecificCardLaserPen LatinFixTopItemLaserParameters = defaultLaserParametersName;
        public static Font LatinFixTopItemFont = NormalFont;
        private static readonly EntryTextProperty LatinFixedItemTopItem_Name = new EntryTextProperty
        {
            EntryText = "Name:",
            EntryTextFont = LatinFixTopItemFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = LatinFixTopItemLaserParameters
        };
        private static readonly EntryTextProperty LatinFixedItemTopItem_Nationality = new EntryTextProperty
        {
            EntryText = "Nationality:",
            EntryTextFont = LatinFixTopItemFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = LatinFixTopItemLaserParameters
        };
        private static readonly EntryTextProperty LatinFixedItemTopItem_DateOfBrith = new EntryTextProperty
        {
            EntryText = "Date of brith:",
            EntryTextFont = LatinFixTopItemFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = LatinFixTopItemLaserParameters
        };
        private static readonly EntryTextProperty LatinFixedItemTopItem_SignatureOfHolder = new EntryTextProperty
        {
            EntryText = "Signature of holder:",
            EntryTextFont = LatinFixTopItemFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = LatinFixTopItemLaserParameters
        };
        private static readonly EntryTextProperty LatinFixedItemTopItem_CardNo = new EntryTextProperty
        {
            EntryText = "Card No:",
            EntryTextFont = LatinFixTopItemFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = LatinFixTopItemLaserParameters
        };


        public static CAOspecificCardLaserPen PersianFixTopItemLaserParameters = defaultLaserParametersName;
        public static Font PerisanFixTopItemFont = NormalFont;
        private static readonly EntryTextProperty PerisanFixedItemTopItem_Name = new EntryTextProperty
        {
            EntryText = ":نام و نام خانوادگی",
            EntryTextFont = PerisanFixTopItemFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Persian,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = PersianFixTopItemLaserParameters
        };
        private static readonly EntryTextProperty PersianFixedItemTopItem_SignatureOfHolder = new EntryTextProperty
        {
            EntryText = ":امضاء دارنده کارت",
            EntryTextFont = PerisanFixTopItemFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Persian,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = PersianFixTopItemLaserParameters
        };


        #endregion
        #region VariableItems_TopOftheCard
        public EntryTextProperty LatinVariableTopItem_Name = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty PersianVariableTopItem_Name = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Persian,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty LatinVariableTopItem_Nationality = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty LatinVariableTopItem_DateOfBrith = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty LatinVariableTopItem_CardNo = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = defaultLaserParametersName,
        };

        public EntryPictureProperty VariableTopItem_PersonalPicture = new EntryPictureProperty
        {
            EntryPicturePath = "",
            XPos = 0,
            YPos = 0,
            PictureLaserParameters = defaultLaserParametersName,
            DatabaseRowName = ""
        };
        public EntryPictureProperty VariableTopItem_SingniturePicture = new EntryPictureProperty
        {
            EntryPicturePath = "",
            XPos = 0,
            YPos = 0,
            PictureLaserParameters = defaultLaserParametersName,
            DatabaseRowName = ""
        };
        #endregion
        #region VariableItems_BottomOftheCard
        public EntryTextProperty LatinVariableBottomItem_DateOfExpiry = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty PersianVariableBottomItem_DateOfExpiry = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Persian,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty LatinVariableBottomItem_MRZ0 = new EntryTextProperty
        {
            EntryTextFont = MRZFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = -39.7,
            YPos = -11.2,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty LatinVariableBottomItem_MRZ1 = new EntryTextProperty
        {
            EntryTextFont = MRZFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = -39.7,
            YPos = -15.43,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty LatinVariableBottomItem_MRZ2 = new EntryTextProperty
        {
            EntryTextFont = MRZFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = -39.7,
            YPos = -19.66,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryPictureProperty VariableBottomItem_IssuingAuthoritySingniturePicture = new EntryPictureProperty
        {
            EntryPicturePath = "",
            XPos = 0,
            YPos = 0,
            PictureLaserParameters = defaultLaserParametersName,
            DatabaseRowName = ""
        };

        #endregion
        #region Methods
        public StatusClass ArrangeItemsOnUltraLicenceCard(CivilAviationInspectorCertificate Carditems, INSPECTORCardType _INSPECTORCardType, ref string[] CardTextItemsValue, ref string[] CardImageImagePathItemsValue, ref string[] CardImageBase64ItemsValue)
        {
            #region initialize
            StatusClass ReturnStatus = new StatusClass()
            {
                ResponseReturnStatus = StatusClass.ResponseStatus.Ok,
                ReturnDescription = StatusClass.Message_SucsessOpration
            };
            LayoutDesignTools _tools = new LayoutDesignTools();
            Laser.LmcErrCode Response = Laser.ClearLibAllEntity(); if (Response != Laser.LmcErrCode.LMC1_ERR_SUCCESS) return Laser.GetLaserBoardDescription(Response);

            EntryTextProperty _EntryText = new EntryTextProperty();
            EntryPictureProperty _EntryTextPicture = new EntryPictureProperty();

            Array.Clear(CardTextItemsValue, 0, CardTextItemsValue.Length);
            Array.Resize<string>(ref CardTextItemsValue, 10);

            Array.Clear(CardImageBase64ItemsValue, 0, CardImageBase64ItemsValue.Length);
            Array.Resize<string>(ref CardImageBase64ItemsValue, 2);

            Array.Clear(CardImageImagePathItemsValue, 0, CardImageImagePathItemsValue.Length);
            Array.Resize<string>(ref CardImageImagePathItemsValue, 2);

            if (String.IsNullOrWhiteSpace(Carditems.LatinVariableTopItem_Name.EntryText) && String.IsNullOrWhiteSpace(Carditems.PersianVariableTopItem_Name.EntryText) &&
            String.IsNullOrWhiteSpace(Carditems.LatinVariableTopItem_Nationality.EntryText) && String.IsNullOrWhiteSpace(Carditems.LatinVariableTopItem_DateOfBrith.EntryText) &&
            String.IsNullOrWhiteSpace(Carditems.LatinVariableTopItem_CardNo.EntryText) && String.IsNullOrWhiteSpace(Carditems.LatinVariableBottomItem_DateOfExpiry.EntryText) &&
            String.IsNullOrWhiteSpace(Carditems.PersianVariableBottomItem_DateOfExpiry.EntryText) && String.IsNullOrWhiteSpace(Carditems.VariableTopItem_PersonalPicture.EntryPicturePath) &&
            String.IsNullOrWhiteSpace(Carditems.VariableTopItem_SingniturePicture.EntryPicturePath) && String.IsNullOrWhiteSpace(Carditems.VariableBottomItem_IssuingAuthoritySingniturePicture.EntryPicturePath))
            {
                ReturnStatus.ResponseReturnStatus = StatusClass.ResponseStatus.Fail;
                ReturnStatus.ReturnDescription = StatusClass.Error_CardItemsTextIsEmpty;
                return ReturnStatus;
            }
            #endregion
            #region ArrangeFixedItems
            switch (_INSPECTORCardType)
            {
                case INSPECTORCardType.Safety:
                    ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, TitleFixedItemTopItem_SAFETYINSPECTOR); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
                    break;
                case INSPECTORCardType.Security:
                    ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, TitleFixedItemTopItem_SecurityINSPECTOR); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
                    break;
                default:
                    {
                        ReturnStatus.ResponseReturnStatus = StatusClass.ResponseStatus.Fail;
                        ReturnStatus.ReturnDescription = StatusClass.Error_CardDetectiontypeIsIncorrect;
                        return ReturnStatus;
                    }

            }

            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, LatinFixedItemTopItem_Name); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, LatinFixedItemTopItem_Nationality); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, LatinFixedItemTopItem_DateOfBrith); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, LatinFixedItemTopItem_SignatureOfHolder); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, LatinFixedItemTopItem_CardNo); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;

            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, PerisanFixedItemTopItem_Name); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, PersianFixedItemTopItem_SignatureOfHolder); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            #endregion
            #region ArrangeVariableItems
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, Carditems.LatinVariableTopItem_Name); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, Carditems.PersianVariableTopItem_Name); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, Carditems.LatinVariableTopItem_Nationality); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, Carditems.LatinVariableTopItem_DateOfBrith); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, Carditems.LatinVariableTopItem_CardNo); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardTextItemsValue[0] = Carditems.LatinVariableTopItem_Name.EntryText;
            CardTextItemsValue[1] = Carditems.PersianVariableTopItem_Name.EntryText;
            CardTextItemsValue[2] = Carditems.LatinVariableTopItem_Nationality.EntryText;
            CardTextItemsValue[3] = Carditems.LatinVariableTopItem_DateOfBrith.EntryText;
            CardTextItemsValue[4] = Carditems.LatinVariableTopItem_CardNo.EntryText;


            ReturnStatus = _EntryTextPicture.AddPictureOnDevice(CardType.CivilAviationSafetyInspectorCertificate, VariableTopItem_PersonalPicture); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryTextPicture.AddPictureOnDevice(CardType.CivilAviationSafetyInspectorCertificate, VariableTopItem_SingniturePicture); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;

            string tmp = "";
            ReturnStatus = _tools.SaveEzdFile(CardType.CivilAviationSafetyInspectorCertificate, CardSide.Top); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;

            _tools.SaveLaserBoardInternalSaveImage(CardType.CivilAviationSafetyInspectorCertificate, CardSide.Top, ref tmp, ref ReturnStatus);
            if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardImageBase64ItemsValue[0] = _tools.ImageToBase64(tmp);
            CardImageImagePathItemsValue[0] = tmp;

            Response = Laser.ClearLibAllEntity(); if (Response != Laser.LmcErrCode.LMC1_ERR_SUCCESS) return Laser.GetLaserBoardDescription(Response);


            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, Carditems.LatinVariableBottomItem_DateOfExpiry); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, Carditems.PersianVariableBottomItem_DateOfExpiry); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, Carditems.LatinVariableBottomItem_MRZ0); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, Carditems.LatinVariableBottomItem_MRZ1); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, Carditems.LatinVariableBottomItem_MRZ2); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardTextItemsValue[5] = Carditems.LatinVariableBottomItem_DateOfExpiry.EntryText;
            CardTextItemsValue[6] = Carditems.PersianVariableBottomItem_DateOfExpiry.EntryText;
            CardTextItemsValue[7] = Carditems.LatinVariableBottomItem_MRZ0.EntryText;
            CardTextItemsValue[8] = Carditems.LatinVariableBottomItem_MRZ1.EntryText;
            CardTextItemsValue[9] = Carditems.LatinVariableBottomItem_MRZ2.EntryText;

            ReturnStatus = _EntryTextPicture.AddPictureOnDevice(CardType.CivilAviationSafetyInspectorCertificate, VariableBottomItem_IssuingAuthoritySingniturePicture); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;

            ReturnStatus = _tools.SaveEzdFile(CardType.CivilAviationSafetyInspectorCertificate, CardSide.Bottom); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            _tools.SaveLaserBoardInternalSaveImage(CardType.CivilAviationSafetyInspectorCertificate, CardSide.Bottom, ref tmp, ref ReturnStatus);
            if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardImageBase64ItemsValue[1] = _tools.ImageToBase64(tmp);
            CardImageImagePathItemsValue[1] = tmp;
            #endregion
            return ReturnStatus;
        }
        #endregion
    }
}

