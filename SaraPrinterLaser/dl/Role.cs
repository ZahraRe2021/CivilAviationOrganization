using SaraPrinterLaser.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraPrinterLaser.dl
{
    class Role:DataAccessClass
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public int Isactive { get; set; }
        public DateTime CreateDate { get; set; }
        public DataSet RoleIdName()
        {
            string Q = "Select ID , Name From Role where  Isactive=1";

            base.Connect();
            DataSet Result = base.TableFill(Q, CommandType.Text);
            base.DisConnect();
            return Result;
        }
        public string RoleByUserID()
        {
            string Q = "select Name from Role where ID=(select RoleID from Users where ID=@UserID)  ";
            Settings st = new Settings();
            base.cmd.Parameters.AddWithValue("@UserID", st.UsersID );
            base.Connect();
            string Result = Convert.ToString(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }
    }
}
