using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraPrinterLaser.LayoutDesignFolder
{
   public class EntryTextProperty
    {
        public enum TextLanguage
        {
            Persian,
            Latin
        }
        public string EntryText { get; set; }
        public Font EntryTextFont { get; set; }
        public TextLanguage EntryTextFontLanguage { get; set; }
        public double XPos { get; set; }
        public double YPos { get; set; }

    }
}
