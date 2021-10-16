using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraPrinterLaser.bl
{
    public class convertor
    {
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        public string ByteArrayWithSpaceToString(byte[] InputData)
        {

            string OutputResponse = null;
            for (int i = 0; i < InputData.Length; i++)
            {
                if (InputData[i] > 0x0F)
                {
                    OutputResponse += " ";
                    OutputResponse += InputData[i].ToString("X");
                }
                else
                {
                    OutputResponse += " ";
                    OutputResponse += "0";
                    OutputResponse += InputData[i].ToString("X");
                }
            }
            return OutputResponse;
        }
    }
}
