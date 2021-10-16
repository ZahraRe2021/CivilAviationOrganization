
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EncryptionClass;

namespace SaraPrinterLaser.dl
{
   public class Users:DataAccessClass
    {
        public int ID { get; set; }
        public static string UserName { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public int Isactive { get; set; }
        public string Discription { get; set; }
        public DateTime CreateDate { get; set; }

        public void InsertUser(string Username,string Password,string Discription, int RoleID,int Isactive)
        {
            string Q = "insert into Users(UserName,[Password],Discription,RoleID,Isactive,CreateDate) values(@UserName,@Password,@Discription,@RoleID,@Isactive,@CreateDate)";
            base.cmd.Parameters.AddWithValue("@UserName",Username);
            base.cmd.Parameters.AddWithValue("@Password", EncryptionClass.EncryptionClass.Encrypt(Password));
            base.cmd.Parameters.AddWithValue("@Discirption", Discription);
            base.cmd.Parameters.AddWithValue("@RoleID", RoleID);
            base.cmd.Parameters.AddWithValue("@Isactive", Isactive);
            base.cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now.Date);
            base.Connect();
            base.NonQuery(Q, CommandType.Text);
            base.DisConnect();
           
        }
      
        public int CHeckLogin(string Username, string Password)
        {
            string Q = "select RoleID from Users where UserName=@UserName  and [Password]=@Password and Isactive =1 ";
            base.cmd.Parameters.AddWithValue("@UserName", Username);
            base.cmd.Parameters.AddWithValue("@Password", EncryptionClass.EncryptionClass.Encrypt(Password));            
            base.Connect();
            int Result = Convert.ToInt32(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }
        public int GetUserID(string Username, string Password)
        {
            string Q = "select ID from Users where userName=@UserName  and [Password]=@Password and Isactive =1 ";
            base.cmd.Parameters.AddWithValue("@UserName", Username);
            base.cmd.Parameters.AddWithValue("@Password", Password);
            base.Connect();
            int Result = Convert.ToInt32(base.Scalar(Q, CommandType.Text));
            base.DisConnect();
            return Result;
        }
        public void UpdatePass(string Username, string Password)
        {
            string Q = "update  Users set [Password]=@Password where [UserName]=@UserName   ";           
            base.cmd.Parameters.AddWithValue("@Password", EncryptionClass.EncryptionClass.Encrypt(Password));
            base.cmd.Parameters.AddWithValue("@UserName", Username);
            base.Connect();
            base.NonQuery(Q, CommandType.Text);
            base.DisConnect();
          
        }
    }
}
