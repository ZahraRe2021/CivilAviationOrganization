
using SaraPrinterLaser.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraPrinterLaser.dl
{
    class GetReport : DataAccessClass
    {
        public int ID { get; set; }
        public int UsersID { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime LogoutDate { get; set; }
        public int PrintCount { get; set; }
        

        public void SetReport(string ExcelPrintedData,string flgWrite,string Base64Format)
        {
            string Q = "insert into ReportOfPrint(ExcelPrintedData,Write,PrintedDate,PrintTime,UsersIDData,UserName,Base64ImageFormat)" +
                " values(@ExcelPrintedData,@Write,@PrintedDate,@PrintTime,@UsersIDData,UserName,@Base64ImageFormat)";
            Settings st = new Settings();
            base.cmd.Parameters.AddWithValue("@ExcelPrintedData", ExcelPrintedData);
            base.cmd.Parameters.AddWithValue("@Write", ExcelPrintedData);
            base.cmd.Parameters.AddWithValue("@UsersID", st.UsersID);
            base.cmd.Parameters.AddWithValue("@LoginDate", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));
            //base.cmd.Parameters.AddWithValue("@LoginDate",dl.DateConvertor.DateConverter(false));// DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));
            base.Connect();
            base.NonQuery(Q, CommandType.Text);
            base.DisConnect();
        }


        public void UpdateCount()
        {
            string Q = "update UserLog set PrintCount=(PrintCount+1) where ID=@ID";
            Settings st = new Settings();
            base.cmd.Parameters.AddWithValue("@ID", st.UserLogID);
            base.Connect();
            base.NonQuery(Q, CommandType.Text);
            base.DisConnect();

        }
        public void LogOut()
        {
            string Q = "update UserLog set [LogoutDate]=@LogoutDate where [ID]=@ID";
            Settings st = new Settings();
            base.cmd.Parameters.AddWithValue("@LogoutDate", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));
            base.cmd.Parameters.AddWithValue("@ID", st.UserLogID);

            base.Connect();
            base.NonQuery(Q, CommandType.Text);
            base.DisConnect();

        }
        public int GetUserLogID()
        {
            string Q = "select top 1 ID from UserLog where USersID=@UsersID  and LogoutDate is null order by LoginDate desc ";
            Settings st = new Settings();
            base.cmd.Parameters.AddWithValue("@UsersID", st.UsersID);

            base.Connect();
            int Result = Convert.ToInt32(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }

    }
}
