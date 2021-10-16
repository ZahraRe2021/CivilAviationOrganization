using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraPrinterLaser.dl
{
    class Pic:DataAccessClass
    {
        public int ID { get; set; }
        public string PicData { get; set; }
        public void Insert(string Data)
        {
            string Q = "insert into Pic(PicData) values(@PicData)";
            base.cmd.Parameters.AddWithValue("@Data", Data);
          
            base.Connect();
            base.NonQuery(Q, CommandType.Text);
            base.DisConnect();
        }
        public int GetIdfromPic(string  Data)
        {
            string Q = "select top 1 ID from Pic where PicData=@PicData  ";
            base.cmd.Parameters.AddWithValue("@PicData", Data);
            base.Connect();
            int Result = Convert.ToInt32(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }
        public string GetPicDataByID(string  ID)
        {
            string Q = "select top 1  PicData from Pic where ID=@ID  ";
            base.cmd.Parameters.AddWithValue("@ID",Convert.ToInt32(ID));
            base.Connect();
            string Result = Convert.ToString(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }
    }
}
