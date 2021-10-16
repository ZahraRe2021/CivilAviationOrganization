using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SaraPrinterLaser.dl
{
    [Serializable]
    public class DataAccessClass
    {
        public OleDbConnection con;
        public OleDbDataAdapter da;
        public OleDbCommand cmd;
        public DataSet dSet;
        public static string ConnectionString;
        public DataAccessClass()
        {
            cmd = new OleDbCommand();
            con = new OleDbConnection();
            da = new OleDbDataAdapter(cmd);
            dSet = new DataSet();
        }
        public void Connect()
        {
            try
            {
                OleDbConnectionStringBuilder ConnectionStringBuilder = new OleDbConnectionStringBuilder();
                ConnectionStringBuilder.DataSource = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + "printer.accdb";
                ConnectionStringBuilder.Add("Provider", "Microsoft.ACE.OLEDB.12.0");
                ConnectionStringBuilder.Add("Jet OLEDB:Database Password", "A988b988");
                ConnectionString = string.Format(ConnectionStringBuilder.ConnectionString);
                con.ConnectionString = string.Format(ConnectionStringBuilder.ConnectionString);
                con.Open();
            }
            catch (Exception)
            {

                System.Windows.Forms.MessageBox.Show("لطفا پایگاه داده را در نرم افزار وارد نمایید", "خطا", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

        }
        public void DisConnect()
        {
            cmd.Parameters.Clear();
            con.Close();
        }

        public void NonQuery(string Query, CommandType Type)
        {
            try
            {
                cmd.CommandText = Query;
                cmd.CommandType = Type;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string aa = ex.ToString();
                System.Windows.Forms.MessageBox.Show("پایگاه داده غیر فعال می باشد.", "خطا", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

        }
        public object Scalar(string Query, CommandType Type)
        {
            try
            {
                cmd.CommandText = Query;
                cmd.CommandType = Type;
                cmd.Connection = con;

            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("پایگاه داده غیر فعال می باشد.", "خطا", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
            return cmd.ExecuteScalar();
        }
        public DataSet TableFill(string Query, CommandType Type)
        {
            try
            {
                cmd.CommandText = Query;
                cmd.CommandType = Type;
                cmd.Connection = con;
                da.Fill(dSet);

            }
            catch (Exception ex)
            {
                string aa = ex.Message;
                System.Windows.Forms.MessageBox.Show("پایگاه داده غیر فعال می باشد.", "خطا", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return dSet;
        }
    }


}
