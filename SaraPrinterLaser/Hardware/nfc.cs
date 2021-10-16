using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS.Apdu;
using GS.PCSC;
using GS.SCard;
using GS.SCard.Const;
using GS.Util.Hex;

namespace SaraPrinterLaser.Hardware
{
    class nfc
    {
        public WinSCard scard = new WinSCard();
        public static string SelectedReaderName = null;
        public bool ReaderConnect()
        {
            bool ReaderExist = false;
            try
            {



                scard.ExtablishContext();

                string[] readerName = scard.ListReaders();
                for (int i = 0; i < readerName.Length; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        if (readerName[i] == ("SCM Microsystems Inc. SCL010 Contactless Reader "+j.ToString()))
                        {
                            SelectedReaderName = readerName[i];
                            ReaderExist = true;
                        }

                    }

                }

              
            }

            catch

            { }
            return ReaderExist;
        }

        public string RequestATR()
        {
            bool CardExist = false;
            string ATRValue = null;
            scard.ExtablishContext();
            try
            {

                CardExist = scard.IsCardPresent(SelectedReaderName);
                if (CardExist)
                {
                    scard.Connect(SelectedReaderName);
                    ATRValue = "ATR: " + scard.AtrString;
                    scard.Disconnect();
                    scard.ReleaseContext();
                }
                else
                {
                    ATRValue = null;
                    scard.Disconnect();
                    scard.ReleaseContext();
                }


            }

            catch

            {
                ATRValue = null;
                scard.Disconnect();
                scard.ReleaseContext();
            }

            return ATRValue;
        }
    }
}
