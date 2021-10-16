using SaraPrinterLaser.LayoutDesignFolder;
using SaraPrinterLaser.Properties;
using System;
using System.Data;
using System.Drawing;

namespace SaraPrinterLaser.dl
{
    internal class ParametersSave : DataAccessClass
    {
        #region PictureParameters
        internal void SaveParameters(LayoutDesignTools.CardType _CardType, LayoutDesignTools.ItemsOnTheCards_Picture ItemType, EntryPictureProperty _PictureProperty, bool DataBaseExist)
        {
            string Q = "";
            if (DataBaseExist)
                Q = "INSERT INTO CardPictureItemsProperty(PictureLocation,CardType,   ItemsType,  XPosition, YPosition, LaserParametersName, DatabaseRowName)" +
                                                        "VALUES(@PictureLocation,@CardType, @ItemsType, @XPosition,@YPosition,@LaserParametersName,@DatabaseRowName)";
            else
                Q = "INSERT INTO CardPictureItemsProperty(PictureLocation,CardType,   ItemsType,  XPosition, YPosition, LaserParametersName)" +
                                                     "VALUES(@PictureLocation,@CardType, @ItemsType, @XPosition,@YPosition,@LaserParametersName)";

            base.cmd.Parameters.AddWithValue("@PictureLocation", _PictureProperty.EntryPicturePath);

            base.cmd.Parameters.AddWithValue("@CardType", _CardType.ToString());
            base.cmd.Parameters.AddWithValue("@ItemsType", ItemType.ToString());

            base.cmd.Parameters.AddWithValue("@YPosition", _PictureProperty.XPos.ToString());
            base.cmd.Parameters.AddWithValue("@YPosition", _PictureProperty.YPos.ToString());

            base.cmd.Parameters.AddWithValue("@LaserParametersName", _PictureProperty.PictureLaserParameters.ParametersName);
            if (DataBaseExist)
                base.cmd.Parameters.AddWithValue("@DatabaseRowName", _PictureProperty.DatabaseRowName);


            base.Connect();
            base.NonQuery(Q, CommandType.Text);
            base.DisConnect();
        }
        internal void DeleteParameters(LayoutDesignTools.ItemsOnTheCards_Picture ItemType)
        {
            base.cmd.Parameters.AddWithValue("@ItemsType", ItemType.ToString());
            base.Connect();
            base.NonQuery("DELETE FROM CardPictureItemsProperty WHERE ItemsType=@ItemsType;", CommandType.Text);
            base.DisConnect();
        }
        internal void ParametersUpdate(LayoutDesignTools.CardType _UPCardType, LayoutDesignTools.ItemsOnTheCards_Picture _UPItemType, EntryPictureProperty _UPPictureProperty, bool _UPDataBaseExist)
        {
            DeleteParameters(_UPItemType);
            SaveParameters(_UPCardType, _UPItemType, _UPPictureProperty, _UPDataBaseExist);
        }
        internal bool ParameterExist(LayoutDesignTools.ItemsOnTheCards_Picture ItemType)
        {
            bool PramExist = false;
            base.Connect();
            DataSet Result = base.TableFill("select ItemsType from CardPictureItemsProperty", CommandType.Text);
            base.DisConnect();
            for (int i = 0; i < Result.Tables[0].Rows.Count; i++)
            {
                if (Result.Tables[0].Rows[i]["ItemsType"].ToString() == ItemType.ToString())
                    PramExist = true;
            }
            return PramExist;
        }
        public EntryPictureProperty ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Picture ItemType)
        {
            EntryPictureProperty Parameters = new EntryPictureProperty();
            int Counter = 0;
            bool flgParamExist = false;
            try
            {
                string Q = "select * from CardPictureItemsProperty where ItemsType=@ItemsType", tmp = ItemType.ToString();
                base.cmd.Parameters.AddWithValue("@ItemsType", ItemType.ToString());
                base.Connect();
                DataSet Result = base.TableFill(Q, CommandType.Text);
                base.DisConnect();


                for (int i = 0; i < Result.Tables[0].Rows.Count; i++)
                    if (Result.Tables[0].Rows[i]["ItemsType"].ToString() == tmp && !String.IsNullOrWhiteSpace(Result.Tables[0].Rows[i]["XPosition"].ToString()) &&
                      !String.IsNullOrWhiteSpace(Result.Tables[0].Rows[i]["YPosition"].ToString()) && !String.IsNullOrWhiteSpace(Result.Tables[0].Rows[i]["LaserParametersName"].ToString()))
                    {
                        Counter = i;
                        flgParamExist = true;
                    }

                if (!flgParamExist) return null;

                Parameters.EntryPicturePath = Result.Tables[0].Rows[Counter]["PictureLocation"].ToString();

                Parameters.XPos = double.Parse(Result.Tables[0].Rows[Counter]["XPosition"].ToString());
                Parameters.YPos = double.Parse(Result.Tables[0].Rows[Counter]["YPosition"].ToString());





                string paramLaserName = Result.Tables[0].Rows[Counter]["LaserParametersName"].ToString();
                Parameters.PictureLaserParameters = new dl.ParametersSave().ParametersLoad(paramLaserName);
                Parameters.PictureLaserParameters.ParametersName = Parameters.PictureLaserParameters.ParametersName = paramLaserName;

                Parameters.DatabaseRowName = Result.Tables[0].Rows[Counter]["DatabaseRowName"].ToString();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(StatusClass.Error_DatabaseDataEntryIsIncorrect + '\n' + ex.Message, StatusClass.Error, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return Parameters;
        }
        #endregion
        #region TextParameters
        internal void SaveParameters(LayoutDesignTools.CardType _CardType, LayoutDesignTools.ItemsOnTheCards_Text ItemType, EntryTextProperty _textProperty, bool DataBaseExist)
        {
            string Q = "";
            if (DataBaseExist)
                Q = "INSERT INTO CardTextItemsProperty(CardType, ItemsType, FontName, FontSize,FontStyle,XPosition,YPostion,textDirection,LaserParametersName,DatabaseRowName)" +
                           "VALUES(@CardType, @ItemsType, @FontName, @FontSize,@FontStyle,@XPosition,@YPostion,@textDirection,@LaserParametersName,@DatabaseRowName)";
            else
                Q = "INSERT INTO CardTextItemsProperty(CardType, ItemsType, FontName, FontSize,FontStyle,XPosition,YPostion,textDirection,LaserParametersName)" +
                           "VALUES(@CardType, @ItemsType, @FontName, @FontSize,@FontStyle,@XPosition,@YPostion,@textDirection,@LaserParametersName)";

            base.cmd.Parameters.AddWithValue("@CardType", _CardType.ToString());
            base.cmd.Parameters.AddWithValue("@ItemsType", ItemType.ToString());
            base.cmd.Parameters.AddWithValue("@FontName", _textProperty.EntryTextFont.Name);
            base.cmd.Parameters.AddWithValue("@FontSize", _textProperty.EntryTextFont.Size.ToString());
            base.cmd.Parameters.AddWithValue("@FontStyle", _textProperty.EntryTextFont.Style.ToString());
            base.cmd.Parameters.AddWithValue("@XPosition", _textProperty.XPos.ToString());
            base.cmd.Parameters.AddWithValue("@YPostion", _textProperty.YPos.ToString());
            base.cmd.Parameters.AddWithValue("@textDirection", _textProperty.EntryTextFontLanguage.ToString());
            base.cmd.Parameters.AddWithValue("@LaserParametersName", _textProperty.TextLaserParameters.ParametersName.ToString());
            if (DataBaseExist)
                base.cmd.Parameters.AddWithValue("@DatabaseRowName", _textProperty.DatabaseRowName.ToString());


            base.Connect();
            base.NonQuery(Q, CommandType.Text);
            base.DisConnect();

        }
        internal void DeleteParameters(LayoutDesignTools.ItemsOnTheCards_Text ItemType)
        {
            string Q = "DELETE FROM CardTextItemsProperty WHERE ItemsType=@ItemsType;";
            base.cmd.Parameters.AddWithValue("@ItemsType", ItemType.ToString());
            base.Connect();
            base.NonQuery(Q, CommandType.Text);
            base.DisConnect();
        }
        internal void ParametersUpdate(LayoutDesignTools.CardType _UPCardType, LayoutDesignTools.ItemsOnTheCards_Text _UPItemType, EntryTextProperty _UPtextProperty, bool _UPDataBaseExist)
        {
            DeleteParameters(_UPItemType);
            SaveParameters(_UPCardType, _UPItemType, _UPtextProperty, _UPDataBaseExist);
        }
        internal bool ParameterExist(LayoutDesignTools.ItemsOnTheCards_Text ItemType)
        {
            bool PramExist = false;
            string Q = "SELECT ItemsType FROM CardTextItemsProperty";

            base.Connect();
            DataSet Result = base.TableFill(Q, CommandType.Text);
            base.DisConnect();
            for (int i = 0; i < Result.Tables[0].Rows.Count; i++)
            {
                string tmp = Result.Tables[0].Rows[i]["ItemsType"].ToString(), tmp2 = ItemType.ToString();


                if (tmp == tmp2)
                    PramExist = true;
            }
            return PramExist;
        }
        public EntryTextProperty ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text ItemType)
        {
            EntryTextProperty Parameters = new EntryTextProperty();
            int Counter = 0;
            bool flgParamExist = false;
            try
            {
                string Q = "select * from CardTextItemsProperty where ItemsType=@ItemsType", tmp = ItemType.ToString();
                base.cmd.Parameters.AddWithValue("@ItemsType", tmp);
                base.Connect();
                DataSet Result = base.TableFill(Q, CommandType.Text);
                base.DisConnect();

                for (int i = 0; i < Result.Tables[0].Rows.Count; i++)
                    if (
                        Result.Tables[0].Rows[i]["ItemsType"].ToString() == tmp && !String.IsNullOrWhiteSpace(Result.Tables[0].Rows[i]["FontName"].ToString())
                     && !String.IsNullOrWhiteSpace(Result.Tables[0].Rows[i]["FontSize"].ToString()) && !String.IsNullOrWhiteSpace(Result.Tables[0].Rows[i]["FontStyle"].ToString()) &&
                     !String.IsNullOrWhiteSpace(Result.Tables[0].Rows[i]["XPosition"].ToString()) && !String.IsNullOrWhiteSpace(Result.Tables[0].Rows[i]["YPostion"].ToString()) &&
                        !String.IsNullOrWhiteSpace(Result.Tables[0].Rows[i]["textDirection"].ToString()) && !String.IsNullOrWhiteSpace(Result.Tables[0].Rows[i]["LaserParametersName"].ToString()))
                    {
                        Counter = i;
                        flgParamExist = true;
                    }
                if (!flgParamExist) return null;







                Parameters.EntryTextFont = new Font
                    (Result.Tables[0].Rows[Counter]["FontName"].ToString(),
                    float.Parse(Result.Tables[0].Rows[Counter]["FontSize"].ToString()),
                    (FontStyle)Enum.Parse(typeof(FontStyle), Result.Tables[0].Rows[Counter]["FontStyle"].ToString(), true));

                Parameters.XPos = double.Parse(Result.Tables[0].Rows[Counter]["XPosition"].ToString());
                Parameters.YPos = double.Parse(Result.Tables[0].Rows[Counter]["YPostion"].ToString());

                Parameters.EntryTextFontLanguage = (EntryTextProperty.TextLanguage)Enum.Parse(typeof(EntryTextProperty.TextLanguage), Result.Tables[0].Rows[Counter]["textDirection"].ToString(), true);

                string paramLaserName = Result.Tables[0].Rows[Counter]["LaserParametersName"].ToString();
                Parameters.TextLaserParameters = new dl.ParametersSave().ParametersLoad(paramLaserName);
                Parameters.TextLaserParameters.ParametersName = paramLaserName;
                Parameters.DatabaseRowName = Result.Tables[0].Rows[Counter]["DatabaseRowName"].ToString();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(StatusClass.Error_DatabaseDataEntryIsIncorrect + '\n' + ex.Message, StatusClass.Error, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }








            return Parameters;
        }
        #region Test
        public EntryTextProperty ParametersLoad(LayoutDesignTools.ItemsOnTheCards_Text ItemType, ref DataSet Result)
        {
            EntryTextProperty Parameters = new EntryTextProperty();
            int Counter = 0;
            try
            {
                string Q = "select * from CardTextItemsProperty where ItemsType=@ItemsType", tmp = ItemType.ToString();
                base.cmd.Parameters.AddWithValue("@ItemsType", tmp);
                base.Connect();
                Result = base.TableFill(Q, CommandType.Text);
                base.DisConnect();

                for (int i = 0; i < Result.Tables[0].Rows.Count; i++)
                    if (Result.Tables[0].Rows[i]["ItemsType"].ToString() == tmp)
                        Counter = i;





                Parameters.EntryTextFont = new Font
                    (Result.Tables[0].Rows[Counter]["FontName"].ToString(),
                    float.Parse(Result.Tables[0].Rows[Counter]["FontSize"].ToString()),
                    (FontStyle)Enum.Parse(typeof(FontStyle), Result.Tables[0].Rows[Counter]["FontStyle"].ToString(), true));

                Parameters.XPos = double.Parse(Result.Tables[0].Rows[Counter]["XPosition"].ToString());
                Parameters.YPos = double.Parse(Result.Tables[0].Rows[Counter]["YPostion"].ToString());

                Parameters.EntryTextFontLanguage = (EntryTextProperty.TextLanguage)Enum.Parse(typeof(EntryTextProperty.TextLanguage), Result.Tables[0].Rows[Counter]["textDirection"].ToString(), true);

                string paramLaserName = Result.Tables[0].Rows[Counter]["LaserParametersName"].ToString();
                Parameters.TextLaserParameters = new dl.ParametersSave().ParametersLoad(paramLaserName);
                Parameters.TextLaserParameters.ParametersName = paramLaserName;
                Parameters.DatabaseRowName = Result.Tables[0].Rows[Counter]["DatabaseRowName"].ToString();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(StatusClass.Error_DatabaseDataEntryIsIncorrect + '\n' + ex.Message, StatusClass.Error, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }








            return Parameters;
        }
        #endregion
        #endregion
        #region LaserParameters
        internal void SaveParameters(CAOspecificCardLaserPen Parameters)
        {

            string Q = "insert into " +
                "LaserParameters(dbParametersName,dbnPenNo,dbnMarkLoop,dbdMarkSpeed,dbdPowerRatio,dbdCurrent," +
                "dbnFreq,dbdQPulseWidth,dbnStartTC,dbnLaserOffTC,dbnEndTC,dbnPolyTC," +
                "dbdJumpSpeed,dbnJumpPosTC,dbnJumpDistTC,dbdEndComp,dbdAccDist,dbdPointTime," +
                "dbbPulsePointMode,dbnPulseNum,dbdFlySpeed,dbnAlign,dbndpi,dbnminLowGrayPt," +
                "dbdBrightness,dbdContrast,dbdSettingPointTime,dbdRatio,dbnbmpScanAttr,dbnbmpttrib," +
                "dbbHatchFile,dbblInvert,dbblGray,dbblDither,dbblBidirectional,dbblYscan," +
                "dbblDisableMarkLowGray,dbblPower,dbblOffSetPT,dbblOptimize,dbblDynamic,dbblDPIfixedWidth," +
                "dbblDPIfixedHeight,dbblBrightness,dbblDrill,dbblfixDPI) " +
                "values(@dbParametersName,@dbnPenNo,@dbnMarkLoop,@dbdMarkSpeed,@dbdPowerRatio,@dbdCurrent,@" +
                "dbnFreq,@dbdQPulseWidth,@dbnStartTC,@dbnLaserOffTC,@dbnEndTC,@dbnPolyTC,@" +
                "dbdJumpSpeed,@dbnJumpPosTC,@dbnJumpDistTC,@dbdEndComp,@dbdAccDist,@dbdPointTime,@" +
                "dbbPulsePointMode,@dbnPulseNum,@dbdFlySpeed,@dbnAlign,@dbndpi,@dbnminLowGrayPt,@" +
                "dbdBrightness,@dbdContrast,@dbdSettingPointTime,@dbdRatio,@dbnbmpScanAttr,@dbnbmpttrib,@" +
                "dbbHatchFile,@dbblInvert,@dbblGray,@dbblDither,@dbblBidirectional,@dbblYscan,@" +
                "dbblDisableMarkLowGray,@dbblPower,@dbblOffSetPT,@dbblOptimize,@dbblDynamic,@dbblDPIfixedWidth,@" +
                "dbblDPIfixedHeight,@dbblBrightness,@dbblDrill,@dbblfixDPI)";

            base.cmd.Parameters.AddWithValue("@dbParametersName", Parameters.ParametersName);
            base.cmd.Parameters.AddWithValue("@dbnPenNo", Parameters.nPenNo.ToString());
            base.cmd.Parameters.AddWithValue("@dbnMarkLoop", Parameters.nMarkLoop.ToString());
            base.cmd.Parameters.AddWithValue("@dbdMarkSpeed", Parameters.dMarkSpeed.ToString());
            base.cmd.Parameters.AddWithValue("@dbdPowerRatio", Parameters.dPowerRatio.ToString());
            base.cmd.Parameters.AddWithValue("@dbdCurrent", Parameters.dCurrent.ToString());
            base.cmd.Parameters.AddWithValue("@dbnFreq", Parameters.nFreq.ToString());
            base.cmd.Parameters.AddWithValue("@dbdQPulseWidth", Parameters.dQPulseWidth.ToString());
            base.cmd.Parameters.AddWithValue("@dbnStartTC", Parameters.nStartTC.ToString());
            base.cmd.Parameters.AddWithValue("@dbnLaserOffTC", Parameters.nLaserOffTC.ToString());
            base.cmd.Parameters.AddWithValue("@dbnEndTC", Parameters.nEndTC.ToString());
            base.cmd.Parameters.AddWithValue("@dbnPolyTC", Parameters.nPolyTC.ToString());
            base.cmd.Parameters.AddWithValue("@dbdJumpSpeed", Parameters.dJumpSpeed.ToString());
            base.cmd.Parameters.AddWithValue("@dbnJumpPosTC", Parameters.nJumpPosTC.ToString());
            base.cmd.Parameters.AddWithValue("@dbnJumpDistTC", Parameters.nJumpDistTC.ToString());
            base.cmd.Parameters.AddWithValue("@dbdEndComp", Parameters.dEndComp.ToString());
            base.cmd.Parameters.AddWithValue("@dbdAccDist", Parameters.dAccDist.ToString());
            base.cmd.Parameters.AddWithValue("@dbdPointTime", Parameters.dPointTime.ToString());
            base.cmd.Parameters.AddWithValue("@dbbPulsePointMode", Parameters.bPulsePointMode.ToString());
            base.cmd.Parameters.AddWithValue("@dbnPulseNum", Parameters.nPulseNum.ToString());
            base.cmd.Parameters.AddWithValue("@dbdFlySpeed", Parameters.dFlySpeed.ToString());
            base.cmd.Parameters.AddWithValue("@dbnAlign", Parameters.nAlign.ToString());
            base.cmd.Parameters.AddWithValue("@dbndpi", Parameters.ndpi.ToString());
            base.cmd.Parameters.AddWithValue("@dbnminLowGrayPt", Parameters.nminLowGrayPt.ToString());
            base.cmd.Parameters.AddWithValue("@dbdBrightness", Parameters.dBrightness.ToString());
            base.cmd.Parameters.AddWithValue("@dbdContrast", Parameters.dContrast.ToString());
            base.cmd.Parameters.AddWithValue("@dbdSettingPointTime", Parameters.dSettingPointTime.ToString());
            base.cmd.Parameters.AddWithValue("@dbdRatio", Parameters.dRatio.ToString());
            base.cmd.Parameters.AddWithValue("@dbnbmpScanAttr", Parameters.nbmpScanAttr.ToString());
            base.cmd.Parameters.AddWithValue("@dbnbmpttrib", Parameters.nbmpttrib.ToString());
            base.cmd.Parameters.AddWithValue("@dbbHatchFile", Parameters.bHatchFile.ToString());
            base.cmd.Parameters.AddWithValue("@dbblInvert", Parameters.blInvert.ToString());
            base.cmd.Parameters.AddWithValue("@dbblGray", Parameters.blGray.ToString());
            base.cmd.Parameters.AddWithValue("@dbblDither", Parameters.blDither.ToString());
            base.cmd.Parameters.AddWithValue("@dbblBidirectional", Parameters.blBidirectional.ToString());
            base.cmd.Parameters.AddWithValue("@dbblYscan", Parameters.blYscan.ToString());
            base.cmd.Parameters.AddWithValue("@dbblDisableMarkLowGray", Parameters.blDisableMarkLowGray.ToString());
            base.cmd.Parameters.AddWithValue("@dbblPower", Parameters.blPower.ToString());
            base.cmd.Parameters.AddWithValue("@dbblOffSetPT", Parameters.blOffSetPT.ToString());
            base.cmd.Parameters.AddWithValue("@dbblOptimize", Parameters.blOptimize.ToString());
            base.cmd.Parameters.AddWithValue("@dbblDynamic", Parameters.blDynamic.ToString());
            base.cmd.Parameters.AddWithValue("@dbblDPIfixedWidth", Parameters.blDPIfixedWidth.ToString());
            base.cmd.Parameters.AddWithValue("@dbblDPIfixedHeight", Parameters.blDPIfixedHeight.ToString());
            base.cmd.Parameters.AddWithValue("@dbblBrightness", Parameters.blBrightness.ToString());
            base.cmd.Parameters.AddWithValue("@dbblDrill", Parameters.blDrill.ToString());
            base.cmd.Parameters.AddWithValue("@dbblfixDPI", Parameters.blfixDPI.ToString());

            base.Connect();
            base.NonQuery(Q, CommandType.Text);
            base.DisConnect();
        }
        internal void DeleteParameters(string PenName)
        {
            string Q = "DELETE FROM LaserParameters WHERE dbParametersName=@dbParametersName;";
            base.cmd.Parameters.AddWithValue("@dbParametersName", PenName);
            base.Connect();
            base.NonQuery(Q, CommandType.Text);
            base.DisConnect();
        }
        internal void ParametersUpdate(CAOspecificCardLaserPen LaserParameters, string PenName)
        {
            DeleteParameters(PenName);
            SaveParameters(LaserParameters);
        }
        public CAOspecificCardLaserPen ParametersLoad(string ParametersName)
        {
            CAOspecificCardLaserPen Parameters = new CAOspecificCardLaserPen();
            //try
            //{
            string Q = "select * from LaserParameters where dbParametersName=@dbParametersName";
            base.cmd.Parameters.AddWithValue("@dbParametersName", ParametersName);
            base.Connect();
            DataSet Result = base.TableFill(Q, CommandType.Text);
            base.DisConnect();

            Parameters.ParametersName = Result.Tables[0].Rows[0]["dbParametersName"].ToString();
            Parameters.nPenNo = int.Parse(Result.Tables[0].Rows[0]["dbnPenNo"].ToString());
            Parameters.nMarkLoop = int.Parse(Result.Tables[0].Rows[0]["dbnMarkLoop"].ToString());
            Parameters.dMarkSpeed = double.Parse(Result.Tables[0].Rows[0]["dbdMarkSpeed"].ToString());
            Parameters.dPowerRatio = double.Parse(Result.Tables[0].Rows[0]["dbdPowerRatio"].ToString());
            Parameters.dCurrent = double.Parse(Result.Tables[0].Rows[0]["dbdCurrent"].ToString());
            Parameters.nFreq = int.Parse(Result.Tables[0].Rows[0]["dbnFreq"].ToString());
            Parameters.dQPulseWidth = double.Parse(Result.Tables[0].Rows[0]["dbdQPulseWidth"].ToString());
            Parameters.nStartTC = int.Parse(Result.Tables[0].Rows[0]["dbnStartTC"].ToString());
            Parameters.nLaserOffTC = int.Parse(Result.Tables[0].Rows[0]["dbnLaserOffTC"].ToString());
            Parameters.nEndTC = int.Parse(Result.Tables[0].Rows[0]["dbnEndTC"].ToString());
            Parameters.nPolyTC = int.Parse(Result.Tables[0].Rows[0]["dbnPolyTC"].ToString());
            Parameters.dJumpSpeed = double.Parse(Result.Tables[0].Rows[0]["dbdJumpSpeed"].ToString());
            Parameters.nJumpPosTC = int.Parse(Result.Tables[0].Rows[0]["dbnJumpPosTC"].ToString());
            Parameters.nJumpDistTC = int.Parse(Result.Tables[0].Rows[0]["dbnJumpDistTC"].ToString());
            Parameters.dEndComp = double.Parse(Result.Tables[0].Rows[0]["dbdEndComp"].ToString());
            Parameters.dAccDist = double.Parse(Result.Tables[0].Rows[0]["dbdAccDist"].ToString());
            Parameters.dPointTime = double.Parse(Result.Tables[0].Rows[0]["dbdPointTime"].ToString());
            Parameters.bPulsePointMode = bool.Parse(Result.Tables[0].Rows[0]["dbbPulsePointMode"].ToString());
            Parameters.nPulseNum = int.Parse(Result.Tables[0].Rows[0]["dbnPulseNum"].ToString());
            Parameters.dFlySpeed = double.Parse(Result.Tables[0].Rows[0]["dbdFlySpeed"].ToString());
            Parameters.nAlign = int.Parse(Result.Tables[0].Rows[0]["dbnAlign"].ToString());
            Parameters.ndpi = int.Parse(Result.Tables[0].Rows[0]["dbndpi"].ToString());
            Parameters.nminLowGrayPt = int.Parse(Result.Tables[0].Rows[0]["dbnminLowGrayPt"].ToString());
            Parameters.dBrightness = double.Parse(Result.Tables[0].Rows[0]["dbdBrightness"].ToString());
            Parameters.dContrast = double.Parse(Result.Tables[0].Rows[0]["dbdContrast"].ToString());
            Parameters.dSettingPointTime = double.Parse(Result.Tables[0].Rows[0]["dbdSettingPointTime"].ToString());
            Parameters.dRatio = double.Parse(Result.Tables[0].Rows[0]["dbdRatio"].ToString());
            Parameters.nbmpScanAttr = int.Parse(Result.Tables[0].Rows[0]["dbnbmpScanAttr"].ToString());
            Parameters.nbmpttrib = int.Parse(Result.Tables[0].Rows[0]["dbnbmpttrib"].ToString());
            Parameters.bHatchFile = bool.Parse(Result.Tables[0].Rows[0]["dbbHatchFile"].ToString());
            Parameters.blInvert = bool.Parse(Result.Tables[0].Rows[0]["dbblInvert"].ToString());
            Parameters.blGray = bool.Parse(Result.Tables[0].Rows[0]["dbblGray"].ToString());
            Parameters.blDither = bool.Parse(Result.Tables[0].Rows[0]["dbblDither"].ToString());
            Parameters.blBidirectional = bool.Parse(Result.Tables[0].Rows[0]["dbblBidirectional"].ToString());
            Parameters.blYscan = bool.Parse(Result.Tables[0].Rows[0]["dbblYscan"].ToString());
            Parameters.blDisableMarkLowGray = bool.Parse(Result.Tables[0].Rows[0]["dbblDisableMarkLowGray"].ToString());
            Parameters.blPower = bool.Parse(Result.Tables[0].Rows[0]["dbblPower"].ToString());
            Parameters.blOffSetPT = bool.Parse(Result.Tables[0].Rows[0]["dbblOffSetPT"].ToString());
            Parameters.blOptimize = bool.Parse(Result.Tables[0].Rows[0]["dbblOptimize"].ToString());
            Parameters.blDynamic = bool.Parse(Result.Tables[0].Rows[0]["dbblDynamic"].ToString());
            Parameters.blDPIfixedWidth = bool.Parse(Result.Tables[0].Rows[0]["dbblDPIfixedWidth"].ToString());
            Parameters.blDPIfixedHeight = bool.Parse(Result.Tables[0].Rows[0]["dbblDPIfixedHeight"].ToString());
            Parameters.blBrightness = bool.Parse(Result.Tables[0].Rows[0]["dbblBrightness"].ToString());
            Parameters.blDrill = bool.Parse(Result.Tables[0].Rows[0]["dbblDrill"].ToString());
            Parameters.blfixDPI = bool.Parse(Result.Tables[0].Rows[0]["dbblfixDPI"].ToString());
            //}
            //catch (Exception ex)
            //{

            //    System.Windows.Forms.MessageBox.Show(StatusClass.Error_DatabaseDataEntryIsIncorrect + '\n' + ex.Message, StatusClass.Error, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //}





            return Parameters;
        }
        internal string[] GetPennames()
        {
            string[] GetPenNames = new string[0];
            CAOspecificCardLaserPen Parameters = new CAOspecificCardLaserPen();
            string Q = "select dbParametersName from LaserParameters";
            base.Connect();
            DataSet Result = base.TableFill(Q, CommandType.Text);
            base.DisConnect();

            for (int i = 0; i < Result.Tables[0].Rows.Count; i++)
            {
                Array.Resize<string>(ref GetPenNames, i + 1);
                GetPenNames[i] = Result.Tables[0].Rows[i]["dbParametersName"].ToString();
            }
            return GetPenNames;
        }
        #endregion
    }
}
