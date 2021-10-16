using SaraPrinterLaser.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraPrinterLaser.dl
{
    class Layout:DataAccessClass
    {
        public int ID { get; set; }
        public string LayoutName { get; set; }
        public string XMLModel { get; set; }
        public string XMLQuery { get; set; }
        public DateTime CreateDate { get; set; }

        public void Insert(string LayoutName, string xmlModel,String xmlQuery)
        {
            string Q = "insert into Layout(LayoutName,XMLModel,XMLQuery,CreateDate) values(@LayoutName,@XMLModel,@XMLQuery,@CreateDate)";
            base.cmd.Parameters.AddWithValue("@LayoutName", LayoutName);
            base.cmd.Parameters.AddWithValue("@XMLModel", xmlModel);
            base.cmd.Parameters.AddWithValue("@XMLQuery", xmlQuery);
            base.cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));
            base.Connect();
            base.NonQuery(Q, CommandType.Text);
            base.DisConnect();
        }
        public string XMLModelByName(string LayoutName)
        {
            string Q = "select XMLModel from Layout where LayoutName=@LayoutName  ";
            base.cmd.Parameters.AddWithValue("@LayoutName", LayoutName);
            base.Connect();
            string Result =Convert.ToString(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }
        public string XMLQueryByName(string LayoutName)
        {
            string Q = "select XMLQuery from Layout where LayoutName=@LayoutName  ";
            base.cmd.Parameters.AddWithValue("@LayoutName", LayoutName);
            base.Connect();
            string Result =Convert.ToString(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }

        public int CheckLayoutNamee(string LayoutName)
        {
            string Q = "select Count(*) from Layout where LayoutName=@LayoutName  ";
            base.cmd.Parameters.AddWithValue("@LayoutName", LayoutName);
            base.Connect();
            int Result = Convert.ToInt32(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }
        public DataSet ListOfName()
        {
            string Q = "select ID,LayoutName from Layout  ";

            base.Connect();
            DataSet Result = base.TableFill(Q, CommandType.Text);
            base.DisConnect();
            return Result;
        }
        public string XMLModelByID(int ID)
        {
            string Q = "select XMLModel from Layout where ID=@ID  ";
            base.cmd.Parameters.AddWithValue("@ID", ID);
            base.Connect();
            string Result = Convert.ToString(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }
        public string NameByID(int ID)
        {
            string Q = "select LayoutName from Layout where ID=@ID  ";
            base.cmd.Parameters.AddWithValue("@ID", ID);
            base.Connect();
            string Result = Convert.ToString(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }
    }
}
