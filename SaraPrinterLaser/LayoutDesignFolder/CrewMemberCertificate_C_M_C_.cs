using SaraPrinterLaser.Hardware;
using System;
using System.Drawing;
using static SaraPrinterLaser.LayoutDesignFolder.LayoutDesignTools;

namespace SaraPrinterLaser.LayoutDesignFolder
{
    public class CrewMemberCertificate_C_M_C_
    {

        #region attributes
        private static readonly Font SmallFont = new Font("Bodoni Bd BT", 8, FontStyle.Regular);
        private static readonly Font NormalFont = new Font("Bodoni Bd BT", 9, FontStyle.Regular);
        #endregion
        #region FixedItems_TopOftheCard
        private static readonly Font MRZFont = new Font("OCR-B 10 BT", 12, FontStyle.Regular);
        public static CAOspecificCardLaserPen TitleFixTopItemLaserParameters = defaultLaserParametersName;
        public static Font TitleFixTopItemFont = SmallFont;


        private static readonly string[] FixTopCardItems = { "Surname", "Given Name", "Sex", "Nationality", "Date of Brith", "Employed by", "Occupation", "Doc No.", "Date of expiry", "Signature of holder :" };
        private static readonly double[,] FixTopCardItems_XYPostions = { { -15.7, 7.9 }, { 16.1, 7.9 }, { -15.7, 1.1 }, { -4.3, 1.1 }, { 16.1, 1.1 }, { -15.7, -6.1 }, { 16.1, -6.1 }, { -15.7, -13 }, { 16.1, -13 }, { -15.7, -21.5 } };

        public CAOspecificCardLaserPen FixTopItemLaserParameters;
        public Font FixTopItemFont = NormalFont;
        #endregion
        #region VariableItems_TopOftheCard
        public EntryTextProperty VariableTopItem_Surname = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = -15.7,
            YPos = 5.5,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableTopItem_GivenName = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 16.1,
            YPos = 5.5,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableTopItem_Sex = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = -15.7,
            YPos = -1.3,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableTopItem_Nationality = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = -4.3,
            YPos = -1.3,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableTopItem_DateOfBrith = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 16.1,
            YPos = -1.3,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableTopItem_Employedby = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = -15.7,
            YPos = -9.5,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableTopItem_Occupation = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 16.1,
            YPos = -9.5,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableTopItem_DocNo = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = -15.7,
            YPos = -15.8,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableTopItem_DateOfExpiry = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 16.1,
            YPos = -15.8,
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

        public EntryTextProperty VariableBottomItem_MRZ0 = new EntryTextProperty
        {
            EntryTextFont = MRZFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = -39.7,
            YPos = -11.2,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableBottomItem_MRZ1 = new EntryTextProperty
        {
            EntryTextFont = MRZFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = -39.7,
            YPos = -15.43,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableBottomItem_MRZ2 = new EntryTextProperty
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
        public StatusClass ArrangeItemsOnCard(CrewMemberCertificate_C_M_C_ Carditems, ref string[] MRZData, ref string[] CardTextItemsValue, ref string[] CardImageImagePathItemsValue, ref string[] CardImageBase64ItemsValue)
        {
            #region initialize
            StatusClass ReturnStatus = new StatusClass()
            {
                ResponseReturnStatus = StatusClass.ResponseStatus.Ok,
                ReturnDescription = StatusClass.Message_SucsessOpration
            };
            LayoutDesignTools _tools = new LayoutDesignTools();
            Laser.LmcErrCode Response = Laser.ClearLibAllEntity(); if (Response != Laser.LmcErrCode.LMC1_ERR_SUCCESS) return Laser.GetLaserBoardDescription(Response);
            EntryPictureProperty _EntryTextPicture = new EntryPictureProperty();

            Array.Clear(CardTextItemsValue, 0, CardTextItemsValue.Length);
            Array.Resize<string>(ref CardTextItemsValue, 12);

            Array.Clear(CardImageBase64ItemsValue, 0, CardImageBase64ItemsValue.Length);
            Array.Resize<string>(ref CardImageBase64ItemsValue, 2);

            Array.Clear(CardImageImagePathItemsValue, 0, CardImageImagePathItemsValue.Length);
            Array.Resize<string>(ref CardImageImagePathItemsValue, 2);

            if (String.IsNullOrWhiteSpace(Carditems.VariableTopItem_Surname.EntryText) && String.IsNullOrWhiteSpace(Carditems.VariableTopItem_GivenName.EntryText) &&
            String.IsNullOrWhiteSpace(Carditems.VariableTopItem_Sex.EntryText) && String.IsNullOrWhiteSpace(Carditems.VariableTopItem_Nationality.EntryText) &&
            String.IsNullOrWhiteSpace(Carditems.VariableTopItem_DateOfBrith.EntryText) && String.IsNullOrWhiteSpace(Carditems.VariableTopItem_Employedby.EntryText) &&
            String.IsNullOrWhiteSpace(Carditems.VariableTopItem_Occupation.EntryText) && String.IsNullOrWhiteSpace(Carditems.VariableTopItem_DocNo.EntryText) &&
            String.IsNullOrWhiteSpace(Carditems.VariableTopItem_DateOfExpiry.EntryText) && String.IsNullOrWhiteSpace(Carditems.VariableTopItem_PersonalPicture.EntryPicturePath) &&
            String.IsNullOrWhiteSpace(Carditems.VariableTopItem_SingniturePicture.EntryPicturePath) && String.IsNullOrWhiteSpace(Carditems.VariableBottomItem_IssuingAuthoritySingniturePicture.EntryPicturePath))
            {
                ReturnStatus.ResponseReturnStatus = StatusClass.ResponseStatus.Fail;
                ReturnStatus.ReturnDescription = StatusClass.Error_CardItemsTextIsEmpty;
                return ReturnStatus;
            }
            #endregion
            #region ArrangeFixedItems


            EntryTextProperty _EntryText = new EntryTextProperty
            {
                EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
                EntryTextFont = Carditems.FixTopItemFont,
                TextLaserParameters = Carditems.FixTopItemLaserParameters,
                TextImageResolution = TextToImageResolution
            };
            if (Carditems.FixTopItemLaserParameters == null)
            {
                _EntryText.TextLaserParameters = LayoutDesignTools.defaultLaserParametersName;
            }

            //FixedItems
            for (int i = 0; i < FixTopCardItems.Length; i++)
            {
                _EntryText.EntryText = FixTopCardItems[i];
                _EntryText.XPos = FixTopCardItems_XYPostions[i, 0];
                _EntryText.YPos = FixTopCardItems_XYPostions[i, 1];
                ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, _EntryText); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            }



            #endregion
            #region ArrangeVariableItems

            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_Surname); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_GivenName); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_Sex); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_Nationality); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_DateOfBrith); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_Employedby); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_Occupation); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_DocNo); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_DateOfExpiry); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;

            CardTextItemsValue[0] = Carditems.VariableTopItem_Surname.EntryText;
            CardTextItemsValue[1] = Carditems.VariableTopItem_GivenName.EntryText;
            CardTextItemsValue[2] = Carditems.VariableTopItem_Sex.EntryText;
            CardTextItemsValue[3] = Carditems.VariableTopItem_Nationality.EntryText;
            CardTextItemsValue[4] = Carditems.VariableTopItem_DateOfBrith.EntryText;
            CardTextItemsValue[5] = Carditems.VariableTopItem_Employedby.EntryText;
            CardTextItemsValue[6] = Carditems.VariableTopItem_Occupation.EntryText;
            CardTextItemsValue[7] = Carditems.VariableTopItem_DocNo.EntryText;
            CardTextItemsValue[8] = Carditems.VariableTopItem_DateOfExpiry.EntryText;


            ReturnStatus = _EntryTextPicture.AddPictureOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_PersonalPicture); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryTextPicture.AddPictureOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_SingniturePicture); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;

            string tmp = "";
            ReturnStatus = _tools.SaveEzdFile(CardType.CrewMemberCertificate_C_M_C_, CardSide.Top); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            _tools.SaveLaserBoardInternalSaveImage(CardType.CrewMemberCertificate_C_M_C_, CardSide.Top, ref tmp, ref ReturnStatus);
            if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardImageBase64ItemsValue[0] = _tools.ImageToBase64(tmp);
            CardImageImagePathItemsValue[0] = tmp;

            Response = Laser.ClearLibAllEntity(); if (Response != Laser.LmcErrCode.LMC1_ERR_SUCCESS) return Laser.GetLaserBoardDescription(Response);

            #region MRZGeneration

            string MRZSexuality = "";
            if (Carditems.VariableTopItem_Sex.EntryText == LayoutDesignTools.MaleSexuality) MRZSexuality = "M";
            else if (Carditems.VariableTopItem_Sex.EntryText == LayoutDesignTools.FemaleSexuality) MRZSexuality = "F";
            else
            {
                ReturnStatus.ResponseReturnStatus = StatusClass.ResponseStatus.Fail;
                ReturnStatus.ReturnDescription = StatusClass.Error_MRZGenerationFailed;
                return ReturnStatus;
            }
            string ISO3166CountryCode = "";
            if (!LayoutDesignTools.DicNationality.TryGetValue(Carditems.VariableTopItem_Nationality.EntryText, out ISO3166CountryCode))
            {
                ReturnStatus.ResponseReturnStatus = StatusClass.ResponseStatus.Fail;
                ReturnStatus.ReturnDescription = StatusClass.Error_MRZGenerationFailed;
                return ReturnStatus;
            }
            MRZData = new MRZGeneratorSDK.MRZGeneratorSDK().MRZGenerator("I<", "IRN", Carditems.VariableTopItem_DocNo.EntryText, Carditems.VariableTopItem_DateOfBrith.EntryText, MRZSexuality, Carditems.VariableTopItem_DateOfExpiry.EntryText, ISO3166CountryCode, Carditems.VariableTopItem_GivenName.EntryText, Carditems.VariableTopItem_Surname.EntryText);

            if (MRZData == MRZGeneratorSDK.MRZGeneratorSDK.InputDataIsNotValid)
            {
                ReturnStatus.ResponseReturnStatus = StatusClass.ResponseStatus.Fail;
                ReturnStatus.ReturnDescription = StatusClass.Error_MRZGenerationFailed;
                return ReturnStatus;
            }
            Carditems.VariableBottomItem_MRZ0.EntryText = MRZData[0];
            Carditems.VariableBottomItem_MRZ1.EntryText = MRZData[1];
            Carditems.VariableBottomItem_MRZ2.EntryText = MRZData[2];
            #endregion

            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableBottomItem_MRZ0); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardTextItemsValue[9] = Carditems.VariableBottomItem_MRZ0.EntryText;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableBottomItem_MRZ1); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardTextItemsValue[10] = Carditems.VariableBottomItem_MRZ1.EntryText;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableBottomItem_MRZ2); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardTextItemsValue[11] = Carditems.VariableBottomItem_MRZ2.EntryText;

            //ReturnStatus = _EntryTextPicture.AddPictureOnDevice(CardType.CrewMemberCertificate_C_M_C_, VariableBottomItem_IssuingAuthoritySingniturePicture); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;

            ReturnStatus = _tools.SaveEzdFile(CardType.CrewMemberCertificate_C_M_C_, CardSide.Bottom); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            _tools.SaveLaserBoardInternalSaveImage(CardType.CrewMemberCertificate_C_M_C_, CardSide.Bottom, ref tmp, ref ReturnStatus);
            if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardImageBase64ItemsValue[1] = _tools.ImageToBase64(tmp);
            CardImageImagePathItemsValue[1] = tmp;
            #endregion
            return ReturnStatus;
        }
        #endregion
    }
}
