using SaraPrinterLaser.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaraPrinterLaser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            foreach (var item in args)
            {
                if (item == "-ConfigConnection")
                {
                    Settings tanzim = new Settings();
                    tanzim.UserLogID = 6;
                    tanzim.UsersID = 6;
                    Application.Run(new DbConfig());
                }
                if (item == "-MachineConfig")
                {
                    Application.Run(new MachineConfig());
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!System.IO.File.Exists("Config\\Laser.Config"))
            {
                Hardware.Laser.myconfig = new Hardware.LaserConfigClass();

            }
            else
            {
                Hardware.Laser.myconfig = Hardware.LaserConfigClass.load();

            }
            //string a = "", b = "";
            //Application.Run(new DatabasePrint(a,b));

            //Application.Run(new LayoutDesignFolder.frmCaoFullPenManagement());
           // Application.Run(new LayoutDesignFolder.frmCivilAviationInspectorCertificate());
         //   Application.Run(new LayoutDesignFolder.frmCrewMemberCertificate_C_M_C_());
           // Application.Run(new LayoutDesignFolder.frmUltraLightLicenceCard());
            //Application.Run(new test());
            Application.Run(new PaintForm.frmMaindesign());
          //  Application.Run(new DbConfig());
           // Application.Run(new EnterData());

        }
    }
}
