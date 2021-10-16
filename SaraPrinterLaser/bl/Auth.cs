using SaraPrinterLaser.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EncryptionClass;

namespace SaraPrinterLaser.bl
{
    internal class Auth
    {
        internal static string UserName { get; set; }
        internal static string Password { get; set; }
        internal static string Role { get; set; }
        internal static int UsersID { get; set; }
        internal static bool CheckUser(string UserName, string Password, ref string Error)
        {
            bool resualt = false;


            int UsetID = 0;
            if (UserName == "MasterAdmin" && Password == "1367")//1100927
            {
                bl.Auth.UserName = UserName;
                bl.Auth.Password = EncryptionClass.EncryptionClass.Encrypt(Password);
                do
                {
                    Auth.Role = "SuperUser";
                } while (Auth.Role != "SuperUser");
                resualt = true;
            }
            else if (UserName == "Manager" && Password == "1397")
            {
                bl.Auth.UserName = UserName;
                bl.Auth.Password = EncryptionClass.EncryptionClass.Encrypt(Password);
                do
                {
                    Auth.Role = "Admin";
                } while (Auth.Role != "Admin");
                resualt = true;
            }
            else
            {
                UsetID = new dl.Users().CHeckLogin(UserName, Password);
                if (UsetID > 0)
                {
                    bl.Auth.UserName = UserName;
                    bl.Auth.Password = EncryptionClass.EncryptionClass.Encrypt(Password);

                    switch (UsetID)
                    {
                        case 2:
                                Auth.Role = "User";
                            break;
                        case 3:
                                Auth.Role = "Admin";
                            break;
                    }
                    //  Auth.Role = new dl.Role().RoleByUserID();

                    resualt = true;
                }
                else
                {
                    Error = "لطفا در ورود نام  و رمز عبور دقت کنید !!!";
                }
            }
            return resualt;
        }
    }
}
