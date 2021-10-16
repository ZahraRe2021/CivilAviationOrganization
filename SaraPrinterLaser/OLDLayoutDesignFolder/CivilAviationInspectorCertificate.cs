using System;
using System.Drawing;
using static CivilAviationOrganization.LayoutDesignFolder.LayoutDesignTools;

namespace CivilAviationOrganization.LayoutDesignFolder
{
    public class CivilAviationInspectorCertificate
    {
        #region attributes
        public enum INSPECTORCardType
        {
            Safety,
            Security
        }

        private static readonly Font NormalFont = new Font("Arial", 10, FontStyle.Regular);
        private static readonly Font SmallFont = new Font("Arial", 8, FontStyle.Regular);
        #endregion
        #region FixedItems_TopOftheCard
        public static FullLaserPen TitleFixTopItemLaserParameters;
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


        public static FullLaserPen LatinFixTopItemLaserParameters;
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


        public static FullLaserPen PersianFixTopItemLaserParameters;
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
            YPos = 0
        };
        public EntryTextProperty PersianVariableTopItem_Name = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Persian,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0
        };
        public EntryTextProperty LatinVariableTopItem_Nationality = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0
        };
        public EntryTextProperty LatinVariableTopItem_DateOfBrith = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0
        };
        public EntryTextProperty LatinVariableTopItem_CardNo = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0
        };

        public EntryPictureProperty VariableTopItem_PersonalPicture = new EntryPictureProperty
        {
            XPos = 0,
            YPos = 0
        };
        public EntryPictureProperty VariableTopItem_SingniturePicture = new EntryPictureProperty
        {
            XPos = 0,
            YPos = 0
        };
        #endregion
        #region VariableItems_BottomOftheCard
        public EntryTextProperty LatinVariableBottomItem_DateOfExpiry = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0
        };
        public EntryTextProperty PersianVariableBottomItem_DateOfExpiry = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Persian,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0
        };
        public EntryTextProperty LatinVariableBottomItem_MRZ = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0
        };
        public EntryPictureProperty VariableBottomItem_IssuingAuthoritySingniturePicture = new EntryPictureProperty
        {
            XPos = 0,
            YPos = 0
        };
        #endregion
        #region Methods
        public StatusClass ArrangeItemsOnUltraLicenceCard(CivilAviationInspectorCertificate Carditems, INSPECTORCardType _INSPECTORCardType, ref string[] CardTextItemsValue, ref string[,] CardImageImagePathItemsValue, ref string[,] CardImageBase64ItemsValue)
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
            Array.Resize<string>(ref CardTextItemsValue, 8);

            Array.Clear(CardImageBase64ItemsValue, 0, CardImageBase64ItemsValue.Length);
            _tools.ResizeArray<string>(ref CardImageBase64ItemsValue, 1, 2);

            Array.Clear(CardImageImagePathItemsValue, 0, CardImageImagePathItemsValue.Length);
            _tools.ResizeArray<string>(ref CardImageImagePathItemsValue, 1, 2);

            if (Carditems.LatinVariableTopItem_Name.EntryText == "" && Carditems.LatinVariableTopItem_Name.EntryText == null && Carditems.LatinVariableTopItem_Name.EntryText.Length == 0 &&
            Carditems.PersianVariableTopItem_Name.EntryText == "" && Carditems.PersianVariableTopItem_Name.EntryText == null && Carditems.PersianVariableTopItem_Name.EntryText.Length == 0 &&
            Carditems.LatinVariableTopItem_Nationality.EntryText == "" && Carditems.LatinVariableTopItem_Nationality.EntryText == null && Carditems.LatinVariableTopItem_Nationality.EntryText.Length == 0 &&
            Carditems.LatinVariableTopItem_DateOfBrith.EntryText == "" && Carditems.LatinVariableTopItem_DateOfBrith.EntryText == null && Carditems.LatinVariableTopItem_DateOfBrith.EntryText.Length == 0 &&
            Carditems.LatinVariableTopItem_CardNo.EntryText == "" && Carditems.LatinVariableTopItem_CardNo.EntryText == null && Carditems.LatinVariableTopItem_CardNo.EntryText.Length == 0 &&
            Carditems.LatinVariableBottomItem_DateOfExpiry.EntryText == "" && Carditems.LatinVariableBottomItem_DateOfExpiry.EntryText == null && Carditems.LatinVariableBottomItem_DateOfExpiry.EntryText.Length == 0 &&
            Carditems.PersianVariableBottomItem_DateOfExpiry.EntryText == "" && Carditems.PersianVariableBottomItem_DateOfExpiry.EntryText == null && Carditems.PersianVariableBottomItem_DateOfExpiry.EntryText.Length == 0 &&
            Carditems.VariableTopItem_PersonalPicture.EntryPicturePath == "" && Carditems.VariableTopItem_PersonalPicture.EntryPicturePath == null && Carditems.VariableTopItem_PersonalPicture.EntryPicturePath.Length == 0 &&
            Carditems.VariableTopItem_SingniturePicture.EntryPicturePath == "" && Carditems.VariableTopItem_SingniturePicture.EntryPicturePath == null && Carditems.VariableTopItem_SingniturePicture.EntryPicturePath.Length == 0 &&
            Carditems.VariableBottomItem_IssuingAuthoritySingniturePicture.EntryPicturePath == "" && Carditems.VariableBottomItem_IssuingAuthoritySingniturePicture.EntryPicturePath == null && Carditems.VariableBottomItem_IssuingAuthoritySingniturePicture.EntryPicturePath.Length == 0)
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

            _tools.SaveLaserBoardInternalSaveImage(CardType.CivilAviationSafetyInspectorCertificate, CardSide.Top, ref tmp,ref ReturnStatus);
            if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardImageBase64ItemsValue[0, 0] = _tools.ImageToBase64(tmp);
            CardImageImagePathItemsValue[0, 0] = tmp;

            Response = Laser.ClearLibAllEntity(); if (Response != Laser.LmcErrCode.LMC1_ERR_SUCCESS) return Laser.GetLaserBoardDescription(Response);


            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, Carditems.LatinVariableBottomItem_DateOfExpiry); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, Carditems.PersianVariableBottomItem_DateOfExpiry); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CivilAviationSafetyInspectorCertificate, Carditems.LatinVariableBottomItem_MRZ); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardTextItemsValue[5] = Carditems.LatinVariableBottomItem_DateOfExpiry.EntryText;
            CardTextItemsValue[6] = Carditems.PersianVariableBottomItem_DateOfExpiry.EntryText;
            CardTextItemsValue[7] = Carditems.LatinVariableBottomItem_MRZ.EntryText;

            ReturnStatus = _EntryTextPicture.AddPictureOnDevice(CardType.CivilAviationSafetyInspectorCertificate, VariableBottomItem_IssuingAuthoritySingniturePicture); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;

            ReturnStatus = _tools.SaveEzdFile(CardType.CivilAviationSafetyInspectorCertificate, CardSide.Bottom); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            _tools.SaveLaserBoardInternalSaveImage(CardType.CivilAviationSafetyInspectorCertificate, CardSide.Bottom, ref tmp,ref ReturnStatus);
            if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardImageBase64ItemsValue[0, 1] = _tools.ImageToBase64(tmp);
            CardImageImagePathItemsValue[0, 1] = tmp;
            #endregion
            return ReturnStatus;
        }
        #endregion
    }
}

