using SaraPrinterLaser.Hardware;
using System;
using System.Drawing;
using static SaraPrinterLaser.LayoutDesignFolder.LayoutDesignTools;

namespace SaraPrinterLaser.LayoutDesignFolder
{
    public class UltraLightLicenceCard
    {
        #region attributes
        private static readonly Font NormalFont = new Font("Arial", 10, FontStyle.Regular);
        private static readonly Font SmallFont = new Font("Arial", 8, FontStyle.Regular);

        #endregion
        #region FixedItems_TopOftheCard
        private static readonly string[] FixTopCardItems = { "Number:", "Name:", "Date of Brith:", "Nationality:", "Date of Issue:", "Date of expiry:", "Authority:" };
        private static readonly double[,] FixTopCardItems_XYPostions = { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } };

        public CAOspecificCardLaserPen FixTopItemLaserParameters = defaultLaserParametersName;
        public Font FixTopItemFont = NormalFont;
        #endregion
        #region VariableItems_TopOftheCard
        public EntryTextProperty VariableTopItem_Number = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableTopItem_Name = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableTopItem_DateOfBrith = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableTopItem_Nationality = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableTopItem_DateOfIssue = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableTopItem_DateOfExpiry = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableTopItem_Authority = new EntryTextProperty
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

        public EntryTextProperty VariableBottomItem_Remarks = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = defaultLaserParametersName,
        };
        public EntryTextProperty VariableBottomItem_Ratings = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
            TextLaserParameters = defaultLaserParametersName,
        };
        #endregion
        #region Methods
        public StatusClass ArrangeItemsOnUltraLicenceCard(UltraLightLicenceCard Carditems, ref string[] CardTextItemsValue, ref string[] CardImageImagePathItemsValue, ref string[] CardImageBase64ItemsValue)
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
            Array.Resize<string>(ref CardTextItemsValue, 9);

            Array.Clear(CardImageBase64ItemsValue, 0, CardImageBase64ItemsValue.Length);
            Array.Resize<string>(ref CardImageBase64ItemsValue, 2);

            Array.Clear(CardImageImagePathItemsValue, 0, CardImageImagePathItemsValue.Length);
            Array.Resize<string>(ref CardImageImagePathItemsValue, 2);

            if (String.IsNullOrWhiteSpace(Carditems.VariableTopItem_Number.EntryText) && String.IsNullOrWhiteSpace(Carditems.VariableTopItem_Name.EntryText) &&
               String.IsNullOrWhiteSpace(Carditems.VariableTopItem_DateOfBrith.EntryText) && String.IsNullOrWhiteSpace(Carditems.VariableTopItem_Nationality.EntryText) &&
               String.IsNullOrWhiteSpace(Carditems.VariableTopItem_DateOfIssue.EntryText) && String.IsNullOrWhiteSpace(Carditems.VariableTopItem_DateOfExpiry.EntryText) &&
               String.IsNullOrWhiteSpace(Carditems.VariableTopItem_Authority.EntryText) && String.IsNullOrWhiteSpace(Carditems.VariableTopItem_PersonalPicture.EntryPicturePath) &&
               String.IsNullOrWhiteSpace(Carditems.VariableTopItem_SingniturePicture.EntryPicturePath) && String.IsNullOrWhiteSpace(Carditems.VariableBottomItem_Remarks.EntryText) &&
               String.IsNullOrWhiteSpace(Carditems.VariableBottomItem_Ratings.EntryText))
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
            //FixedItems
            for (int i = 0; i < FixTopCardItems.Length; i++)
            {
                _EntryText.EntryText = FixTopCardItems[i];
                _EntryText.XPos = FixTopCardItems_XYPostions[i, 0];
                _EntryText.YPos = FixTopCardItems_XYPostions[i, 1];
                ReturnStatus = _EntryText.AddTextOnDevice(CardType.UltraLightLicence, _EntryText); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            }
            #endregion
            #region ArrangeVariableItems

            ReturnStatus = _EntryText.AddTextOnDevice(CardType.UltraLightLicence, Carditems.VariableTopItem_Number); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.UltraLightLicence, Carditems.VariableTopItem_Name); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.UltraLightLicence, Carditems.VariableTopItem_DateOfBrith); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.UltraLightLicence, Carditems.VariableTopItem_Nationality); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.UltraLightLicence, Carditems.VariableTopItem_DateOfIssue); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.UltraLightLicence, Carditems.VariableTopItem_DateOfExpiry); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.UltraLightLicence, Carditems.VariableTopItem_Authority); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardTextItemsValue[0] = Carditems.VariableTopItem_Number.EntryText;
            CardTextItemsValue[1] = Carditems.VariableTopItem_Name.EntryText;
            CardTextItemsValue[2] = Carditems.VariableTopItem_DateOfBrith.EntryText;
            CardTextItemsValue[3] = Carditems.VariableTopItem_Nationality.EntryText;
            CardTextItemsValue[4] = Carditems.VariableTopItem_DateOfIssue.EntryText;
            CardTextItemsValue[5] = Carditems.VariableTopItem_DateOfExpiry.EntryText;
            CardTextItemsValue[6] = Carditems.VariableTopItem_Authority.EntryText;




            ReturnStatus = _EntryTextPicture.AddPictureOnDevice(CardType.UltraLightLicence, Carditems.VariableTopItem_PersonalPicture); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryTextPicture.AddPictureOnDevice(CardType.UltraLightLicence, Carditems.VariableTopItem_SingniturePicture); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;

            ReturnStatus = _tools.SaveEzdFile(CardType.UltraLightLicence, CardSide.Top); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            string tmp = "";
            _tools.SaveLaserBoardInternalSaveImage(CardType.UltraLightLicence, CardSide.Top, ref tmp, ref ReturnStatus);
            if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardImageBase64ItemsValue[0] = _tools.ImageToBase64(tmp);
            CardImageImagePathItemsValue[0] = tmp;

            Response = Laser.ClearLibAllEntity(); if (Response != Laser.LmcErrCode.LMC1_ERR_SUCCESS) return Laser.GetLaserBoardDescription(Response);

            ReturnStatus = _EntryText.AddTextOnDevice(CardType.UltraLightLicence, Carditems.VariableBottomItem_Remarks); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.UltraLightLicence, Carditems.VariableBottomItem_Ratings); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardTextItemsValue[7] = Carditems.VariableBottomItem_Remarks.EntryText;
            CardTextItemsValue[8] = Carditems.VariableBottomItem_Ratings.EntryText;

            ReturnStatus = _tools.SaveEzdFile(CardType.UltraLightLicence, CardSide.Bottom); if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            _tools.SaveLaserBoardInternalSaveImage(CardType.UltraLightLicence, CardSide.Bottom, ref tmp, ref ReturnStatus);
            if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardImageBase64ItemsValue[1] = _tools.ImageToBase64(tmp);
            CardImageImagePathItemsValue[1] = tmp;
            #endregion
            return ReturnStatus;
        }


        #endregion
    }
}
