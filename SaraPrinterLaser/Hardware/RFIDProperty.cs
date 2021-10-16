using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraPrinterLaser.Hardware
{
    public class RFIDProperty
    {
        public string RFIDType { get; set; }
        #region ISO/IEC 14443 Type A
        public string strUIDNumber { get; set; }
        public byte[] bytUIDNumber;

        public string strATQA { get; set; }
        public byte[] bytATQA = new byte[2];

        public string strSAK { get; set; }
        public byte bytSAK { get; set; }

        public string strATS { get; set; }
        public byte[] bytATS;

        public string strPPSS { get; set; }
        public byte bytPPSS { get; set; }
        #endregion
    }
}
