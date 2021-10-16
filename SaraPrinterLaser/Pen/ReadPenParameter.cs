using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaraPrinterLaser.Pen
{
    public static class ReadPenParameter
    {
        public static string ReadPenSaveToHard()
        {
            string Return = "";

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            dlg.Filter = "Pen Format |*.pen";
            dlg.Multiselect = false;
            if (dlg.ShowDialog()==DialogResult.OK)
            {
                File.ReadAllText(dlg.FileName);
            }
            string S = GenerateStreamFromString

            return Return;
        }




        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
