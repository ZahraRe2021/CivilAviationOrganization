using SaraPrinterLaser.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraPrinterLaser.dl
{
    class InfoData:DataAccessClass
    {
        public int ID { get; set; }
        public string Data { get; set; }
        public int InfoModelID { get; set; }
        public int Write { get; set; }
        public DateTime WriteDate { get; set; }
        public int UsersIDData { get; set; }
        public int UsersIDWrite { get; set; }
        public void Insert(string Data, int InfoModelName)
        {
            string Q = "insert into InfoData(Data,InfoModelID, UsersIDData) values(@Data,@InfoModelID,@UserID)";
            base.cmd.Parameters.AddWithValue("@Data", Data);
            base.cmd.Parameters.AddWithValue("@InfoModelID",InfoModelName);
            Settings st = new Settings();
            base.cmd.Parameters.AddWithValue("@UserID", st.UsersID);
        
            base.Connect();
            base.NonQuery(Q, CommandType.Text);
            base.DisConnect();
        }
        public void WriteReport(int ID)
        {
            string Q = "update InfoData set [Write]=Write+1,WriteDate=@WriteDate,USersIDWrite=@UserID where [ID]=@ID";
            Settings st = new Settings();
            base.cmd.Parameters.AddWithValue("@ID", ID);
            base.cmd.Parameters.AddWithValue("@WriteDate", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));
            Settings st2 = new Settings();
            base.cmd.Parameters.AddWithValue("@UserID", st2.UsersID);
            base.Connect();
            base.NonQuery(Q, CommandType.Text);
            base.DisConnect();

        }
        public int CountDataByInfoModelID(int InfoModelID)
        {
            string Q = "select Count(*) from InfoData where InfoModelID=@InfoModelID  ";
            base.cmd.Parameters.AddWithValue("@InfoModelID", InfoModelID);
            base.Connect();
            int Result = Convert.ToInt32(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }
        public DataSet FirstPrint(int InfoModelID)
        {
            string Q = "select top 1 ID,data from InfoData where [Write]=0 and InfoModelID=@InfoModelID";
            base.cmd.Parameters.AddWithValue("@InfoModelID", InfoModelID);
            base.Connect();
            DataSet Result = base.TableFill(Q, CommandType.Text);
            base.DisConnect();
            return Result;
        }
        public void FirstPrintSave(int ID)
        {
            string Q = "update InfoData set [Write]=1  where   ID=@ID";
            base.cmd.Parameters.AddWithValue("@ID", ID);
            base.Connect();
           base.NonQuery(Q, CommandType.Text);
            base.DisConnect();
           
        }
    }
}
