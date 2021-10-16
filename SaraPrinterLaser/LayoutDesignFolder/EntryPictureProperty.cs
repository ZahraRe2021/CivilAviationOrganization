using SaraPrinterLaser.Hardware;
using System;
using System.Drawing;
using System.IO;
using static SaraPrinterLaser.LayoutDesignFolder.LayoutDesignTools;

namespace SaraPrinterLaser.LayoutDesignFolder
{
    public class EntryPictureProperty
    {
        #region properties
        public string EntryPicturePath { get; set; }
        public double XPos { get; set; }
        public double YPos { get; set; }
        public CAOspecificCardLaserPen PictureLaserParameters { get; set; }
        public string DatabaseRowName { get; set; }
        #endregion
        #region Methods
        internal StatusClass AddPictureOnDevice(CardType PrintedCardType, EntryPictureProperty EntryPicture)
        {
            LaserConfigClass myconfig = LaserConfigClass.load();
            LayoutDesignTools tools = new LayoutDesignTools();
            StatusClass ReturnStatus = new StatusClass()
            {
                ResponseReturnStatus = StatusClass.ResponseStatus.Ok,
                ReturnDescription = StatusClass.Message_SucsessOpration
            };
            if (String.IsNullOrWhiteSpace(EntryPicture.EntryPicturePath))
            {
                ReturnStatus.ResponseReturnStatus = StatusClass.ResponseStatus.Fail;
                ReturnStatus.ReturnDescription = StatusClass.Error_CardItemsImageIsEmpty;
                return ReturnStatus;
            }
            string TextRandomName = bl.Auth.UserName + "_" + tools.DateConverter() + "." + TextToImageSaveImageFormat;
            string ImageSavePath = tools.GetSaveFolderAddress(PrintedCardType, FileType.Image) + "\\" + TextRandomName;

            File.Copy(EntryPicture.EntryPicturePath, ImageSavePath, true);

            Laser.LmcErrCode Response = Laser.SetPenParam(EntryPicture.PictureLaserParameters.nPenNo, EntryPicture.PictureLaserParameters.nMarkLoop, EntryPicture.PictureLaserParameters.dMarkSpeed, EntryPicture.PictureLaserParameters.dPowerRatio, EntryPicture.PictureLaserParameters.dCurrent, EntryPicture.PictureLaserParameters.nFreq, EntryPicture.PictureLaserParameters.dQPulseWidth, EntryPicture.PictureLaserParameters.nStartTC, EntryPicture.PictureLaserParameters.nLaserOffTC, EntryPicture.PictureLaserParameters.nEndTC, EntryPicture.PictureLaserParameters.nPolyTC, EntryPicture.PictureLaserParameters.dJumpSpeed, EntryPicture.PictureLaserParameters.nJumpPosTC, EntryPicture.PictureLaserParameters.nJumpDistTC, EntryPicture.PictureLaserParameters.dEndComp, EntryPicture.PictureLaserParameters.dAccDist, EntryPicture.PictureLaserParameters.dPointTime, EntryPicture.PictureLaserParameters.bPulsePointMode, EntryPicture.PictureLaserParameters.nPulseNum, EntryPicture.PictureLaserParameters.dFlySpeed); if (Response != Laser.LmcErrCode.LMC1_ERR_SUCCESS) return Laser.GetLaserBoardDescription(Response);

            Bitmap bmp = new Bitmap(EntryPicture.EntryPicturePath);
            float DPIX = bmp.HorizontalResolution;
            float DPIY = bmp.VerticalResolution;
            double[] PictureSize = new LayoutDesignTools().WithDPIconvertPxTomm(bmp.Size, DPIX, DPIY);
            double NewCentreLocationX = EntryPicture.XPos + myconfig.Xcenter - 42.948;
            NewCentreLocationX = Math.Round(NewCentreLocationX, 4);
            double NewCentreLocationY = (EntryPicture.YPos * -1) + myconfig.Ycenter + 27.021;
            NewCentreLocationY = Math.Round(NewCentreLocationY, 4);



            Response = Laser.AddFileToLib(ImageSavePath, TextRandomName, NewCentreLocationX, NewCentreLocationY, 0, 6, EntryPicture.PictureLaserParameters.dRatio, EntryPicture.PictureLaserParameters.nPenNo, EntryPicture.PictureLaserParameters.bHatchFile); if (Response != Laser.LmcErrCode.LMC1_ERR_SUCCESS) return Laser.GetLaserBoardDescription(Response);

            Response = Laser.SetBitmapEntParam(TextRandomName, TextRandomName, EntryPicture.PictureLaserParameters.nbmpttrib, EntryPicture.PictureLaserParameters.nbmpScanAttr, EntryPicture.PictureLaserParameters.dBrightness, EntryPicture.PictureLaserParameters.dContrast, EntryPicture.PictureLaserParameters.dSettingPointTime, EntryPicture.PictureLaserParameters.ndpi, EntryPicture.PictureLaserParameters.blDisableMarkLowGray, EntryPicture.PictureLaserParameters.nminLowGrayPt); if (Response != Laser.LmcErrCode.LMC1_ERR_SUCCESS) return Laser.GetLaserBoardDescription(Response);

            return Laser.GetLaserBoardDescription(Laser.LmcErrCode.LMC1_ERR_SUCCESS);

        }
        #endregion
    }
}
