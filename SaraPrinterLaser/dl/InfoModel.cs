using SaraPrinterLaser.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraPrinterLaser.dl
{
    class InfoModel : DataAccessClass
    {
        public int ID { get; set; }
        public string InfoName { get; set; }
        public string InfoData { get; set; }

        public DateTime InfoDate { get; set; }


        public int UserID { get; set; }
        public void Insert(string InfoName,
                           string InfoData,
                          string PictureFolderPath,//PictureFolderPath
                          string PictureFormat)//PictureFormat
        {

            string Q = "insert into InfoModel(InfoName,InfoData,PictureFolderPath,PictureFormat)" +
                " values(@InfoName,@InfoData,@PictureFolderPath,@PictureFormat)";
            base.cmd.Parameters.AddWithValue("@InfoName", InfoName);
            base.cmd.Parameters.AddWithValue("@InfoData", InfoData);
            base.cmd.Parameters.AddWithValue("@PictureFolderPath", PictureFolderPath);
            base.cmd.Parameters.AddWithValue("@PictureFormat", PictureFormat);



            base.Connect();
            base.NonQuery(Q, CommandType.Text);
            base.DisConnect();

        }
        public int GetInfoID(string InfoName)
        {
            string Q = "select top 1 ID from InfoModel where InfoName=@InfoName  ";
            base.cmd.Parameters.AddWithValue("@InfoName", InfoName);
            base.Connect();
            int Result = Convert.ToInt32(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }
        public int CheckInfoName(string InfoName)
        {
            string Q = "select Count(*) from InfoModel where InfoName=@InfoName  ";
            base.cmd.Parameters.AddWithValue("@InfoName", InfoName);
            base.Connect();
            int Result = Convert.ToInt32(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }
        public string InfoDataByID(int ID)
        {
            string Q = "select InfoData from InfoModel where ID=@ID  ";
            base.cmd.Parameters.AddWithValue("@ID", ID);
            base.Connect();
            string Result = Convert.ToString(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }
        public string InfoPictureFormatByID(int ID)
        {
            string Q = "select PictureFormat from InfoModel where ID=@ID  ";
            base.cmd.Parameters.AddWithValue("@ID", ID);
            base.Connect();
            string Result = Convert.ToString(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }

        public string InfoPictureFolderPathByID(int ID)
        {
            string Q = "select PictureFolderPath from InfoModel where ID=@ID  ";
            base.cmd.Parameters.AddWithValue("@ID", ID);
            base.Connect();
            string Result = Convert.ToString(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }

        public DataSet ListOfName()
        {
            string Q = "select ID,InfoName from InfoModel  ";

            base.Connect();
            DataSet Result = base.TableFill(Q, CommandType.Text);
            base.DisConnect();
            return Result;
        }

    }
}
