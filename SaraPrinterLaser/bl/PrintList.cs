using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraPrinterLaser.bl
{
    public class PrintList
    {
        public string PictureFolderPath { get; set; }
        public string PictureFormat { get; set; }

        public int InfoModelRoID { get; set; }
        public int InfoModelZirID { get; set; }
        public Dictionary<string, string> Selection { get; set; }
        public int DataPrintID { get; set; }
        public bool SelectData { get; set; }
        public bool FixIncrimentActive { get; set; }
        public string FixIncriment { get; set; }
        public int startIncriment { get; set; }
        public int EndIncriment { get; set; }
        


    }
}
