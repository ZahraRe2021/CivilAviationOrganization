using SaraPrinterLaser.LayoutDesignFolder;
using SaraPrinterLaser.Properties;
using System;
using System.Data;

namespace SaraPrinterLaser.dl
{
    internal class PrintedCardReport : DataAccessClass
    {
        internal void SetCaoCardPrintedReport(
            LayoutDesignTools.CardType _CardType, string[] CardTextItemsValue, string[] CardImageImagePathItemsValue, string[] CardImageBase64ItemsValue, string UserName)
        {
            string CardItems = "";
            for (int i = 0; i < CardTextItemsValue.Length; i++)
            {
                CardItems += CardTextItemsValue[i] + "ß";
            }
            string Q = "insert into PrintedCardReport(CardType,CardTextItems,PrintedCardDateAndTime,UsersIDData,UserName,TopLayerBase64ImageFormat," +
                "TopLayerPrintedCardImagePath,BottomLayerBase64ImageFormat,BottomLayerPrintedCardImagePath) values(@CardType,CardTextItems,@PrintedCardDateAndTime,@UsersIDData,@UserName,@TopLayerBase64ImageFormat," +
                "@TopLayerPrintedCardImagePath,@BottomLayerBase64ImageFormat,@BottomLayerPrintedCardImagePath)";
            Settings st = new Settings();

            base.cmd.Parameters.AddWithValue("@CardType", _CardType.ToString());
            base.cmd.Parameters.AddWithValue("@CardTextItems", CardItems);
            base.cmd.Parameters.AddWithValue("@PrintedCardDateAndTime", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));
            base.cmd.Parameters.AddWithValue("@UsersID", st.UsersID);
            base.cmd.Parameters.AddWithValue("@UserName", UserName);
            base.cmd.Parameters.AddWithValue("@TopLayerBase64ImageFormat", CardImageBase64ItemsValue[0]);
            base.cmd.Parameters.AddWithValue("@TopLayerPrintedCardImagePath", CardImageImagePathItemsValue[0]);
            base.cmd.Parameters.AddWithValue("@BottomLayerBase64ImageFormat", CardImageBase64ItemsValue[1]);
            base.cmd.Parameters.AddWithValue("@BottomLayerPrintedCardImagePath", CardImageImagePathItemsValue[1]);

            base.Connect();
            base.NonQuery(Q, CommandType.Text);
            base.DisConnect();

        }
    }
}
