using System;
using System.Drawing;
using static CivilAviationOrganization.LayoutDesignFolder.LayoutDesignTools;

namespace CivilAviationOrganization.LayoutDesignFolder
{
    public class CrewMemberCertificate_C_M_C_
    {
        #region attributes
        private static readonly Font NormalFont = new Font("Arial", 10, FontStyle.Regular);
        private static readonly Font SmallFont = new Font("Arial", 8, FontStyle.Regular);
        #endregion
        #region FixedItems_TopOftheCard
        private static readonly string[] FixTopCardItems = { "Surname", "Given Name", "Sex", "Nationality", "Date of Brith", "Employed by", "Occupation", "Doc No.", "Date of expiry", "Signature of holder :" };
        private static readonly double[,] FixTopCardItems_XYPostions = { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } };

        public FullLaserPen FixTopItemLaserParameters;
        public Font FixTopItemFont = NormalFont;
        #endregion
        #region VariableItems_TopOftheCard
        public EntryTextProperty VariableTopItem_Surname = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0
        };
        public EntryTextProperty VariableTopItem_GivenName = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0
        };
        public EntryTextProperty VariableTopItem_Sex = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0
        };
        public EntryTextProperty VariableTopItem_Nationality = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0
        };
        public EntryTextProperty VariableTopItem_DateOfBrith = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0
        };
        public EntryTextProperty VariableTopItem_Employedby = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0
        };
        public EntryTextProperty VariableTopItem_Occupation = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0
        };
        public EntryTextProperty VariableTopItem_DocNo = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0
        };
        public EntryTextProperty VariableTopItem_DateOfExpiry = new EntryTextProperty
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

        public EntryTextProperty VariableBottomItem_MRZ = new EntryTextProperty
        {
            EntryTextFont = NormalFont,
            EntryTextFontLanguage = EntryTextProperty.TextLanguage.Latin,
            TextImageResolution = TextToImageResolution,
            XPos = 0,
            YPos = 0,
        };
        public EntryPictureProperty VariableBottomItem_IssuingAuthoritySingniturePicture = new EntryPictureProperty
        {
            XPos = 0,
            YPos = 0
        };
        #endregion
        #region Methods
        public StatusClass ArrangeItemsOnUltraLicenceCard(CrewMemberCertificate_C_M_C_ Carditems, ref string[] CardTextItemsValue, ref string[,] CardImageImagePathItemsValue, ref string[,] CardImageBase64ItemsValue)
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
            Array.Resize<string>(ref CardTextItemsValue, 10);

            Array.Clear(CardImageBase64ItemsValue, 0, CardImageBase64ItemsValue.Length);
            _tools.ResizeArray<string>(ref CardImageBase64ItemsValue, 1, 2);

            Array.Clear(CardImageImagePathItemsValue, 0, CardImageImagePathItemsValue.Length);
            _tools.ResizeArray<string>(ref CardImageImagePathItemsValue, 1, 2);

            if (Carditems.VariableTopItem_Surname.EntryText == "" && Carditems.VariableTopItem_Surname.EntryText == null && Carditems.VariableTopItem_Surname.EntryText.Length == 0 &&
            Carditems.VariableTopItem_GivenName.EntryText == "" && Carditems.VariableTopItem_GivenName.EntryText == null && Carditems.VariableTopItem_GivenName.EntryText.Length == 0 &&
            Carditems.VariableTopItem_Sex.EntryText == "" && Carditems.VariableTopItem_Sex.EntryText == null && Carditems.VariableTopItem_Sex.EntryText.Length == 0 &&
            Carditems.VariableTopItem_Nationality.EntryText == "" && Carditems.VariableTopItem_Nationality.EntryText == null && Carditems.VariableTopItem_Nationality.EntryText.Length == 0 &&
            Carditems.VariableTopItem_DateOfBrith.EntryText == "" && Carditems.VariableTopItem_DateOfBrith.EntryText == null && Carditems.VariableTopItem_DateOfBrith.EntryText.Length == 0 &&
            Carditems.VariableTopItem_Employedby.EntryText == "" && Carditems.VariableTopItem_Employedby.EntryText == null && Carditems.VariableTopItem_Employedby.EntryText.Length == 0 &&
            Carditems.VariableTopItem_Occupation.EntryText == "" && Carditems.VariableTopItem_Occupation.EntryText == null && Carditems.VariableTopItem_Occupation.EntryText.Length == 0 &&
            Carditems.VariableTopItem_DocNo.EntryText == "" && Carditems.VariableTopItem_DocNo.EntryText == null && Carditems.VariableTopItem_DocNo.EntryText.Length == 0 &&
            Carditems.VariableTopItem_DateOfExpiry.EntryText == "" && Carditems.VariableTopItem_DateOfExpiry.EntryText == null && Carditems.VariableTopItem_DateOfExpiry.EntryText.Length == 0 &&
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

            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_Surname) ; if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_GivenName) ; if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_Sex) ; if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_Nationality) ; if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_DateOfBrith) ; if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_Employedby) ; if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_Occupation) ; if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_DocNo) ; if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableTopItem_DateOfExpiry) ; if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;

            CardTextItemsValue[0] = Carditems.VariableTopItem_Surname.EntryText;
            CardTextItemsValue[1] = Carditems.VariableTopItem_GivenName.EntryText;
            CardTextItemsValue[2] = Carditems.VariableTopItem_Sex.EntryText;
            CardTextItemsValue[3] = Carditems.VariableTopItem_Nationality.EntryText;
            CardTextItemsValue[4] = Carditems.VariableTopItem_DateOfBrith.EntryText;
            CardTextItemsValue[5] = Carditems.VariableTopItem_Employedby.EntryText;
            CardTextItemsValue[6] = Carditems.VariableTopItem_Occupation.EntryText;
            CardTextItemsValue[7] = Carditems.VariableTopItem_DocNo.EntryText;
            CardTextItemsValue[8] = Carditems.VariableTopItem_DateOfExpiry.EntryText;


            ReturnStatus = _EntryTextPicture.AddPictureOnDevice(CardType.CrewMemberCertificate_C_M_C_, VariableTopItem_PersonalPicture) ; if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            ReturnStatus = _EntryTextPicture.AddPictureOnDevice(CardType.CrewMemberCertificate_C_M_C_, VariableTopItem_SingniturePicture) ; if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;

            string tmp = "";
            ReturnStatus = _tools.SaveEzdFile(CardType.CrewMemberCertificate_C_M_C_, CardSide.Top) ; if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            _tools.SaveLaserBoardInternalSaveImage(CardType.CrewMemberCertificate_C_M_C_, CardSide.Top, ref tmp, ref ReturnStatus);
            if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardImageBase64ItemsValue[0, 0] = _tools.ImageToBase64(tmp);
            CardImageImagePathItemsValue[0, 0] = tmp;

            Response = Laser.ClearLibAllEntity(); if (Response != Laser.LmcErrCode.LMC1_ERR_SUCCESS) return Laser.GetLaserBoardDescription(Response);



            ReturnStatus = _EntryText.AddTextOnDevice(CardType.CrewMemberCertificate_C_M_C_, Carditems.VariableBottomItem_MRZ) ; if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardTextItemsValue[7] = Carditems.VariableBottomItem_MRZ.EntryText;

            ReturnStatus = _EntryTextPicture.AddPictureOnDevice(CardType.CrewMemberCertificate_C_M_C_, VariableBottomItem_IssuingAuthoritySingniturePicture) ; if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;

            ReturnStatus = _tools.SaveEzdFile(CardType.CrewMemberCertificate_C_M_C_, CardSide.Bottom) ; if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            _tools.SaveLaserBoardInternalSaveImage(CardType.CrewMemberCertificate_C_M_C_, CardSide.Bottom, ref tmp, ref ReturnStatus);
            if (ReturnStatus.ResponseReturnStatus != StatusClass.ResponseStatus.Ok) return ReturnStatus;
            CardImageBase64ItemsValue[0, 1] = _tools.ImageToBase64(tmp);
            CardImageImagePathItemsValue[0, 1] = tmp;
            #endregion
            return ReturnStatus;
        }
        #endregion
    }
}
