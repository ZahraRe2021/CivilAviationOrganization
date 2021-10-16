using SaraPrinterLaser.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

//5047-0610-4249-0595
//                    علی طبیعیا پور


namespace SaraPrinterLaser.Hardware
{
    class CR
    {

        #region All Imported Dlls
        internal static uint Hdle { get; set; }
        [DllImport("CRT_350V3.dll")]
        internal static extern UInt32 CRT350ROpen(string port);
        [DllImport("CRT_350V3.dll")]
        internal static extern UInt32 CRT350ROpenWithBaut(string port, UInt32 Baudrate);
        [DllImport("CRT_350V3.dll")]
        internal static extern int CRT350RClose(UInt32 ComHandle);
        [DllImport("CRT_350V3.dll")]
        internal static extern int RS232_ExeCommand(UInt32 ComHandle, byte TxCmCode, byte TxPmCode, UInt16 TxDataLen, byte[] TxData, ref byte RxReplyType, ref byte RxStCode0, ref byte RxStCode1, ref UInt16 RxDataLen, byte[] RxData);
        #endregion All Imported Dlls
        #region This Code is not need now , So please don't be attention, but in Faze two it will be use <Mehrdad Bahrami> 
        public static string RFType()//it dosen't Matter Now
        {
            string resualt = "no Error";
            if (Hdle != 0)
            {

                byte Cm, Pm;
                UInt16 TxDataLen, RxDataLen;
                byte[] TxData = new byte[1024];
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x90;
                Pm = 0x32;
                St0 = St1 = 0;
                TxDataLen = 0;
                RxDataLen = 0;


                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, TxDataLen, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    if (ReType == 0x50)
                    {
                        switch (RxData[0])
                        {
                            case 48:
                                switch (RxData[1])
                                {
                                    case 48:
                                        resualt = "Unknow Type";
                                        break;
                                }
                                break;
                            case 49:
                                switch (RxData[1])
                                {
                                    case 48:
                                        resualt = "S50)";
                                        break;
                                    case 49:
                                        resualt = "S70";
                                        break;
                                    case 50:
                                        resualt = "UL CARD";
                                        break;
                                }
                                break;
                            case 50:
                                switch (RxData[1])
                                {
                                    case 48:
                                        resualt = "Type A CPU";
                                        break;
                                }
                                break;
                            case 51:
                                switch (RxData[1])
                                {
                                    case 48:
                                        resualt = "Type B CPU";
                                        break;
                                }
                                break;
                        }
                    }
                    else
                    {
                        resualt = "RF Card Type ERROR" + "\r\n" + "Error Code:  " + (char)St0 + (char)St1;
                    }

                }
                else
                {
                    resualt = "Communication Error";
                }
            }
            else
            {
                resualt = "Comm. port is not Opened";
            }

            return resualt;
        }

        public static string CardStatus()//it dosen't Matter Now
        {
            string resualt = "No Error";
            if (Hdle != 0)
            {

                byte Cm, Pm;
                UInt16 TxDataLen, RxDataLen;
                byte[] TxData = new byte[1024];
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x31;
                Pm = 0x30;
                St0 = St1 = 0;
                TxDataLen = 0;
                RxDataLen = 0;


                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, TxDataLen, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    if (ReType == 0x50)
                    {
                        resualt = "Card Status OK" + "\r\n" + "Status Code : " + (char)St0 + (char)St1;
                    }
                    else
                    {
                        resualt = "Card Status ERROR" + "\r\n" + "Error Code:  " + (char)St0 + (char)St1;
                    }

                }
                else
                {
                    resualt = "Communication Error";
                }
            }
            else
            {
                resualt = "Comm. port is not Opened";
            }
            return resualt;

        }
        public static string DetailsStatus()
        {
            string resualt = "No Error";
            if (Hdle != 0)
            {

                byte Cm, Pm;
                UInt16 TxDataLen, RxDataLen;
                byte[] TxData = new byte[1024];
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x31;
                Pm = 0x30;
                St0 = St1 = 0;
                TxDataLen = 0;
                RxDataLen = 0;


                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, TxDataLen, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    if (ReType == 0x50)
                    {
                        resualt = "Card Status OK" + "\r\n" + "Status Code : " + (char)St0 + (char)St1;
                    }
                    else
                    {
                        resualt = "Card Status ERROR" + "\r\n" + "Error Code:  " + (char)St0 + (char)St1;
                    }

                }
                else
                {
                    resualt = "Communication Error";
                }
            }
            else
            {
                resualt = "Comm. port is not Opened";
            }

            return resualt;
        }
        public static string ProhabitationCard()//This is "Report presence of card and its position" Not ProhabitationCard So, it dosen't matter at all <Mehrdad Bahrami>
        {
            string resualt = "No Error";
            if (Hdle != 0)
            {

                byte Cm, Pm;
                UInt16 TxDataLen, RxDataLen;
                byte[] TxData = new byte[1024];
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x31;
                Pm = 0x30;
                St0 = St1 = 0;
                TxDataLen = 0;
                RxDataLen = 0;


                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, TxDataLen, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    if (ReType == 0x50)
                    {
                        resualt = "Card Status OK" + "\r\n" + "Status Code : " + (char)St0 + (char)St1;
                    }
                    else
                    {
                        resualt = "Card Status ERROR" + "\r\n" + "Error Code:  " + (char)St0 + (char)St1;
                    }

                }
                else
                {
                    resualt = "Communication Error";
                }
            }
            else
            {
                resualt = "Comm. port is not Opened";
            }
            return resualt;
        }
        #endregion
        #region This variabels are for CR Module Response

        private static Dictionary<int, string> Personalization_Line_Response_StatusCode = new Dictionary<int, string>()
    {
        { 0x3030, "No card detected within ICRW" },
        { 0x3031, "Card locates at card Gate" },
        { 0x3032, "Card locates inside ICRW" }
    };

        private static Dictionary<int, string> Personalization_Line_Response_ErrorCode = new Dictionary<int, string>
        {
            { 0x3030, "A given command code is unidentified" },
            { 0x3031, "Parameter is not correct" },
            { 0x3032, "Command execution is impossible" },
            { 0x3033, "Hardware is not present" },
            { 0x3034, "Command data error" },
            { 0x3035, "Tried to card feed commands before the IC contact release command" },
            { 0x3036, "ICRW does not have keys that decipher the data" },
            { 0x3037, "Reserved For Future" },
            { 0x3038, "Reserved For Future" },
            { 0x3039, "Intake with draw timeout" },

            { 0x3130, "Card jam" },
            { 0x3131, "Shutter failure" },
            { 0x3132, "Sensor failure of PD1, PD2, PD3, PDI / Card remains inside." },
            { 0x3133, "Irregular card length (LONG)" },
            { 0x3134, "Irregular card length (SHORT)" },
            { 0x3135, "FRAM error" },
            //{ 0x3135, "FLASH Memory Parameter Area CRC error" },
            { 0x3136, "The card was moved forcibly." },
            //{ 0x3136, "Card position Move(and pull out error)" },
            { 0x3137, "Jam error at retrieve" },
            { 0x3138, "SW1 or SW2 error" },
            //{ 0x3138, "Two card error" },
            { 0x3139, "Card was not inserted from the rear" },

            { 0x3230, "Read mag-card error(verifying faulty(VRC error))" },
            //{ 0x3230, "Read Error (Parity error)" },
            { 0x3231, "Read mag-card error(Start character error,end character error or LRC error" },
            { 0x3232, "Read Error" },
            //{ 0x3233, "Read Error ((Only STX-ETX-LRC)" },
            { 0x3233, "Read mag-card error(no data, start character, end character and LRC only" },
            { 0x3234, "Read mag-card error(no mag-stripe or code)" },
            { 0x3235, "Read Error ((Quality error) )" },
            { 0x3236, "Read Error （ No SS）" },
            { 0x3237, "Read Error （ No ES）" },
            { 0x3238, "Read Error （ LRC error）" },
            { 0x3239, "Write Verify Erro (Data discordance)" },

            { 0x3330, "Power Down" },
            { 0x3331, "DSR signal was turned to OFF" },
            { 0x3332, "Reserved For Future" },
            { 0x3333, "Reserved For Future" },
            { 0x3334, "Reserved For Future" },
            { 0x3335, "Reserved For Future" },
            { 0x3336, "Reserved For Future" },
            { 0x3337, "Reserved For Future" },
            { 0x3338, "Reserved For Future" },
            { 0x3339, "Reserved For Future" },

            { 0x3430, "Card was pulled out during capture" },
            { 0x3431, "Failure at IC contact solenoid or sensor ICD" },
            { 0x3432, "Reserved For Future" },
            { 0x3433, "Card could not be set to IC contact position" },
            { 0x3434, "Reserved For Future" },
            { 0x3435, "ICRW ejected the card forcibly." },
            { 0x3436, "The ejected card has not been withdrawn until the specified time." },
            { 0x3437, "Reserved For Future" },
            { 0x3438, "Reserved For Future" },
            { 0x3439, "Reserved For Future" },

            { 0x3530, "Capture Counter Overflow Error" },
            { 0x3531, "Motor error" },
            { 0x3532, "Reserved For Future" },
            { 0x3533, "Digital decode read error" },
            { 0x3534, "Reserved For Future" },
            { 0x3535, "Reserved For Future" },
            { 0x3536, "Reserved For Future" },
            { 0x3537, "Reserved For Future" },
            { 0x3538, "Reserved For Future" },
            { 0x3539, "Reserved For Future" },

            { 0x3630, "Abnormal Vcc condition error of IC card or SAM" },
            { 0x3631, "ATR communication error of IC card or SAM" },
            { 0x3632, "Invalid ATR error to the selected activation for IC card or SAM" },
            { 0x3633, "No response error on communication from IC card or SAM" },
            { 0x3634, "Communication error to IC card or SAM(except for no response)" },
            { 0x3635, "HOST sends command for IC card communication before receiving ATR." },
            { 0x3636, "Not supported IC card or SAM error by ICRM(only for EMV activation)" },
            { 0x3637, "Reserved For Future" },
            { 0x3638, "Reserved For Future" },
            { 0x3639, "Not supported IC card or SAM(error by EMV2000 only for EMV activation)" },

            { 0x3730, "Reserved For Future" },
            { 0x3731, "Reserved For Future" },
            { 0x3732, "Reserved For Future" },
            { 0x3733, "EEPROM error" },
            { 0x3734, "Reserved For Future" },
            { 0x3735, "Reserved For Future" },
            { 0x3736, "Reserved For Future" },
            { 0x3737, "Reserved For Future" },
            { 0x3738, "Reserved For Future" },
            { 0x3739, "Reserved For Future" },

            { 0x3830, "Reserved For Future" },
            { 0x3831, "Reserved For Future" },
            { 0x3832, "Reserved For Future" },
            { 0x3833, "Reserved For Future" },
            { 0x3834, "Reserved For Future" },
            { 0x3835, "Reserved For Future" },
            { 0x3836, "Reserved For Future" },
            { 0x3837, "Reserved For Future" },
            { 0x3838, "Reserved For Future" },
            { 0x3839, "Reserved For Future" },

            { 0x3930, "Reserved For Future" },
            { 0x3931, "Reserved For Future" },
            { 0x3932, "Reserved For Future" },
            { 0x3933, "Reserved For Future" },
            { 0x3934, "Reserved For Future" },
            { 0x3935, "Reserved For Future" },
            { 0x3936, "Reserved For Future" },
            { 0x3937, "Reserved For Future" },
            { 0x3938, "Reserved For Future" },
            { 0x3939, "Reserved For Future" },

            { 0x4030, "Reserved For Future" },
            { 0x4031, "Reserved For Future" },
            { 0x4032, "Reserved For Future" },
            { 0x4033, "Reserved For Future" },
            { 0x4034, "Reserved For Future" },
            { 0x4035, "Reserved For Future" },
            { 0x4036, "Reserved For Future" },
            { 0x4037, "Reserved For Future" },
            { 0x4038, "Reserved For Future" },
            { 0x4039, "Reserved For Future" },

            { 0x4130, "No card in cassette box" },
            { 0x4131, "-" },
            { 0x4132, "ICRW module communication time out" },
            { 0x4133, "Exceed of ReCheck limited in ICRW module communication" },
            { 0x4134, "-" },
            { 0x4135, "Dispensing blocked" },
            { 0x4136, "Ferry moving blocked" },
            { 0x4137, "Hook mechanism move failure" },
            { 0x4138, "--" },
            { 0x4139, "Hook position is error(take out the box and re-Power again or send initial command)" },

            { 0x4142, "Full of error card bin" },
            { 0x4143, "Sensors is abnormal in dispensing module" },
            { 0x4144, "Box has been removed, not placed on dispenser" },

            { 0x4230, "Not received Initialize command" },
            { 0x4231, "Reserved For Future" },
            { 0x4232, "Reserved For Future" },
            { 0x4233, "Reserved For Future" },
            { 0x4234, "Reserved For Future" },
            { 0x4235, "Reserved For Future" },
            { 0x4236, "Reserved For Future" },
            { 0x4237, "Reserved For Future" },
            { 0x4238, "Reserved For Future" },
            { 0x4239, "Reserved For Future" },
        };
        #endregion This variabels are for CR Module Response
        #region all function can be move card
        public enum Inittype
        {
            MoveTogate = 0x30,
            RejectBox = 0x31,
            KeepInside = 0x32,
            NoMoveCard = 0x33
        };

        public enum ReturnDeviceStatus
        {
            MB_OK,
            MB_Error,
            MB_PortNotOpen
        };
        public enum MagneticWriteMode
        {
            Low_Co = 0x30,
            High_Co = 0x31,
            Automatic = 0x32,
        }
        public enum RFIDType
        {
            TypeA = 0x41,
            TypeB = 0x42,
            Mifare = 0x4D,
            NoType = 0x30

        }
        public enum RFIDActiveStatus
        {
            Active,
            Deactive
        }
        /// <summary>
        /// This Function Do initilize the CR module
        /// the CR mosule Must be Initilize before any action
        /// Check the Return Acknowlage and return Status of the Command
        /// این تابع دستگاه سی آر را راه اندازی میکند.
        /// </summary>
        /// <Device Device Status of ReturnDeviceStatus enum ></returns>
        public static ReturnDeviceStatus Initialize(Inittype CardState, ref string DeviceResponse)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;
            if (Hdle != 0)
            {

                byte Cm, Pm;
                UInt16 RxDataLen;
                byte[] TxData =
                {
                        0x33,
                        0x32,
                        0x34,
                        0x30,
                        0x30,
                        0x33,
                        0x30,
                        0x31,
                        0x30,
                        0x30,
                        0x31

                };
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x30;
                Pm = (byte)CardState;

                St0 = St1 = 0;
                RxDataLen = 0;

                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, (UInt16)TxData.Length, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);

                    if (ReType == 0x50)
                    {
                        Array.Resize<byte>(ref RxData, RxDataLen);
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                    }
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (String.IsNullOrWhiteSpace(DeviceResponse)) DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }
        /// <summary>
        /// this function take card from behind of the CRModule
        /// </summary>
        /// <returns>Device Status of ReturnDeviceStatus enum</returns>
        public static ReturnDeviceStatus PermitBehind(ref string DeviceResponse)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;
            if (Hdle != 0)
            {
                byte Cm, Pm;
                UInt16 RxDataLen;
                byte[] TxData = { };
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x32;
                Pm = 0x32;

                St0 = St1 = 0;
                RxDataLen = 0;

                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, 0, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    if (ReType == 0x50)
                    {
                        Array.Resize<byte>(ref RxData, RxDataLen);
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                    }
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (String.IsNullOrWhiteSpace(DeviceResponse)) DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }
        /// <summary>
        /// this Function Push Card To Backward of the CrModule
        /// </summary>
        /// <returns>Device Status of ReturnDeviceStatus enum</returns>
        public static ReturnDeviceStatus CardToRejectBox(ref string DeviceResponse)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;
            if (Hdle != 0)
            {
                byte Cm, Pm;
                UInt16 RxDataLen;
                byte[] TxData = { };
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x33;
                Pm = 0x31;

                St0 = St1 = 0;
                RxDataLen = 0;

                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, 0, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    if (ReType == 0x50)
                    {
                        Array.Resize<byte>(ref RxData, RxDataLen);
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                    }
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (String.IsNullOrWhiteSpace(DeviceResponse)) DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }
        /// <summary>
        /// this Function Push Card to gate
        /// </summary>
        /// <returns>Device Status of ReturnDeviceStatus enum</returns>
        public static ReturnDeviceStatus CardToEject(ref string DeviceResponse)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;
            if (Hdle != 0)
            {
                byte Cm, Pm;
                UInt16 RxDataLen;
                byte[] TxData = { };
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x33;
                Pm = 0x30;

                St0 = St1 = 0;
                RxDataLen = 0;

                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, 0, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    if (ReType == 0x50)
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (String.IsNullOrWhiteSpace(DeviceResponse)) DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }
        /// <summary>
        ///  this Fucntion Return status of device in String
        /// </summary>
        /// <param name="Retype"></param>
        /// <param name="St0"></param>
        /// <param name="St1"></param>
        /// <param name="DeviceResponse"></param>
        /// <returns></returns>
        private static ReturnDeviceStatus ShowCRError(int Retype, int St0, int St1, ref string DeviceResponse)
        {
            ReturnDeviceStatus RetrunStatus = ReturnDeviceStatus.MB_Error;

            int intDeviceResponse = (St0 * 0x100) + St1;
            if (Retype == 0x50)
            {
                bool Status = Personalization_Line_Response_StatusCode.TryGetValue(intDeviceResponse, out DeviceResponse);
                if (Status) RetrunStatus = ReturnDeviceStatus.MB_OK;
                else RetrunStatus = ReturnDeviceStatus.MB_Error;
            }

            else if (Retype == 0x4e)
            {
                bool Status = Personalization_Line_Response_ErrorCode.TryGetValue(intDeviceResponse, out DeviceResponse);
                if (Status) RetrunStatus = ReturnDeviceStatus.MB_OK;
                else RetrunStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                RetrunStatus = ReturnDeviceStatus.MB_Error;

            return RetrunStatus;
        }
        public static ReturnDeviceStatus getGateSensorStatus(ref string DeviceResponse, ref bool GateSensorValue)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;

            bool SW2 = false;
            bool SW1 = false;
            if (Hdle != 0)
            {
                byte Cm, Pm;
                UInt16 RxDataLen;
                byte[] TxData = { };
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x31;
                Pm = 0x31;

                St0 = St1 = 0;
                RxDataLen = 0;

                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, 0, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    if (ReType == 0x50)
                    {
                        if (Convert.ToBoolean(RxData[0] & 0x40) == true)
                        {
                            SW2 = false; SW1 = false;
                            if (Convert.ToBoolean(RxData[0] & 0x08) == true) SW2 = true;
                            if (Convert.ToBoolean(RxData[0] & 0x10) == true) SW1 = true;
                            if (!SW1 && !SW2)
                            {
                                GateSensorValue = false;
                                ReturnStatus = ReturnDeviceStatus.MB_OK;
                            }
                            else if (SW1 && SW2)
                            {
                                GateSensorValue = true;
                                ReturnStatus = ReturnDeviceStatus.MB_OK;
                            }
                            else
                                ReturnStatus = ReturnDeviceStatus.MB_Error;
                        }
                        else
                            ReturnStatus = ReturnDeviceStatus.MB_Error;
                    }
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (String.IsNullOrWhiteSpace(DeviceResponse)) DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }
        public static ReturnDeviceStatus PrimitAllCardIn(ref string DeviceResponse)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;

            if (Hdle != 0)
            {
                byte Cm, Pm;
                UInt16 RxDataLen;
                byte[] TxData = { };
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x3A;
                Pm = 0x30;

                St0 = St1 = 0;
                RxDataLen = 0;

                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, 0, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    if (ReType == 0x50)
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (String.IsNullOrWhiteSpace(DeviceResponse)) DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }

        public static ReturnDeviceStatus getBackSensorStatus(ref string DeviceResponse, ref bool GateSensorValue)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;


            if (Hdle != 0)
            {
                byte Cm, Pm;
                UInt16 RxDataLen;
                byte[] TxData = { };
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x31;
                Pm = 0x40;

                St0 = St1 = 0;
                RxDataLen = 0;

                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, 0, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    if (ReType == 0x50)
                    {
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                        if (Convert.ToBoolean(RxData[0] & 0x20) == true)
                        {
                            GateSensorValue = true;

                        }

                    }
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (String.IsNullOrWhiteSpace(DeviceResponse)) DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }


        public static ReturnDeviceStatus getInternalSensorStatus(ref string DeviceResponse, ref bool GateSensorValue)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;


            if (Hdle != 0)
            {
                byte Cm, Pm;
                UInt16 RxDataLen;
                byte[] TxData = { };
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x31;
                Pm = 0x40;

                St0 = St1 = 0;
                RxDataLen = 0;

                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, 0, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    if (ReType == 0x50)
                    {
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                        if (Convert.ToBoolean(RxData[0] & 0x02) == true)
                        {
                            GateSensorValue = true;

                        }

                    }
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (String.IsNullOrWhiteSpace(DeviceResponse)) DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }
        public static ReturnDeviceStatus GetRFSensorStatus(ref string DeviceResponse, ref bool GateSensorValue)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;

            if (Hdle != 0)
            {
                byte Cm, Pm;
                UInt16 RxDataLen;
                byte[] TxData = { };
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x31;
                Pm = 0x40;

                St0 = St1 = 0;
                RxDataLen = 0;

                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, 0, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    if (ReType == 0x50)
                    {
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                        if (Convert.ToBoolean(RxData[0] & 0x04) == true)
                        {
                            GateSensorValue = true;

                        }

                    }
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (String.IsNullOrWhiteSpace(DeviceResponse)) DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }
        public static ReturnDeviceStatus GetAllSensorStatus(ref string DeviceResponse, ref bool GateSensorValue)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;

            if (Hdle != 0)
            {
                byte Cm, Pm;
                UInt16 RxDataLen;
                byte[] TxData = { };
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x31;
                Pm = 0x40;

                St0 = St1 = 0;
                RxDataLen = 0;

                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, 0, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    if (ReType == 0x50)
                    {
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                        if (RxData[0] > 0)
                            GateSensorValue = true;
                    }
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (String.IsNullOrWhiteSpace(DeviceResponse)) DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }
        #endregion all function can be move card
        #region All Functions About Chip Read and Write MagneticCard
        public static ReturnDeviceStatus ReadAllMagneticTrack(ref string DeviceResponse, ref string[] strReadMagneticTrack)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_OK;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;
            bl.convertor convertor = new bl.convertor();
            byte Cm = 0x56, Pm = 0x37;
            UInt16 RxDataLen = 0;
            byte[] TxData = { };
            byte[] RxData = new byte[512];
            byte[] ReadedData1 = new byte[110];
            byte[] ReadedData2 = new byte[110];
            byte[] ReadedData3 = new byte[110];
            byte ReType = 0;
            byte St0 = 0, St1 = 0;
            byte NextArray = 0, DataTrackCount = 0;
            byte[] DataByteCount = new byte[3];

            int i = RS232_ExeCommand(Hdle, Cm, Pm, 0, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
            if (i == 0)
            {
                ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                if (ReType == 0x50 || ReType == 0x4E)
                {
                    for (int SeparatorCounter = 0; SeparatorCounter < RxDataLen; SeparatorCounter++)
                    {
                        if (RxData[SeparatorCounter] == 0x50)
                        {

                        }
                        else if (RxData[SeparatorCounter] == 0X4E)
                        {
                            //ReturnStatus = ReturnDeviceStatus.MB_Error;
                            //DeviceResponse = "Read Track " + NextArray.ToString() + "Fail";
                            //break;
                        }
                        else
                        {
                            if (RxData[SeparatorCounter] == 0x7E && (RxData[SeparatorCounter + 1] == 0x50|| RxData[SeparatorCounter + 1] == 0x4E))
                            {
                                DataByteCount[NextArray] = DataTrackCount;
                                NextArray++;
                                DataTrackCount = 0;
                            }
                            else
                            {
                                if (NextArray == 0)
                                {
                                    ReadedData1[DataTrackCount] = RxData[SeparatorCounter];
                                    DataTrackCount++;
                                }
                                else if (NextArray == 1)
                                {
                                    ReadedData2[DataTrackCount] = RxData[SeparatorCounter];
                                    DataTrackCount++;
                                }
                                else if (NextArray == 2)
                                {
                                    ReadedData3[DataTrackCount] = RxData[SeparatorCounter];
                                    DataTrackCount++;
                                }
                            }
                        }
                    }
                    DataByteCount[NextArray] = DataTrackCount;

                    Array.Resize<byte>(ref ReadedData1, DataByteCount[0]);
                    Array.Resize<byte>(ref ReadedData2, DataByteCount[1]);
                    Array.Resize<byte>(ref ReadedData3, DataByteCount[2]);
                    strReadMagneticTrack[0] = Encoding.ASCII.GetString(ReadedData1);
                    strReadMagneticTrack[1] = Encoding.ASCII.GetString(ReadedData2);
                    strReadMagneticTrack[2] = Encoding.ASCII.GetString(ReadedData3);

                    if (strReadMagneticTrack[0]=="24"|| strReadMagneticTrack[0].Length<5) { strReadMagneticTrack[0] = ""; }
                    if (strReadMagneticTrack[1] == "24" || strReadMagneticTrack[1].Length < 5) { strReadMagneticTrack[1] = ""; }
                    if (strReadMagneticTrack[2] == "24" || strReadMagneticTrack[2].Length < 5) { strReadMagneticTrack[2] = ""; }

                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_Error;
            return ReturnStatus;
        }
        public static ReturnDeviceStatus WriteAllMagneticTrack(ref string DeviceResponse, string[] strWriteTrackData, bool[] WhichTrack)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;
            if (Hdle != 0)
            {
                byte[] tmp1 = new byte[0];
                byte[] tmp2 = new byte[0];
                byte[] tmp3 = new byte[0];
                byte[] TxData = new byte[0];
                byte[] RxData = new byte[1024];
                bool flgFailSendData = false;
                byte Cm = 0x37, Pm = 0x36;
                UInt16 RxDataLen = 0;
                UInt16 txDataLen = 0;
                byte ReType = 0;
                byte St0 = 0, St1 = 0;
                int ReturnSdkStatus = 0;
                if (WhichTrack[0])
                {
                    Cm = 0x37; Pm = 0x36;
                    tmp1 = Encoding.ASCII.GetBytes(strWriteTrackData[0]);
                    Array.Resize<byte>(ref TxData, tmp1.Length);
                    Array.Copy(tmp1, 0, TxData, 0, tmp1.Length);
                    txDataLen = (UInt16)TxData.Length;
                    ReturnSdkStatus = RS232_ExeCommand(Hdle, Cm, Pm, txDataLen, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                    if (ReturnSdkStatus == 0 && ReType == 0x50)

                        ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    else
                    {
                        flgFailSendData = true;
                        return ReturnDeviceStatus.MB_Error;
                    }
                }
                if (WhichTrack[1])
                {
                    if (flgFailSendData)
                        return ReturnDeviceStatus.MB_Error;
                    else
                    {
                        Cm = 0x37; Pm = 0x37;
                        tmp2 = Encoding.ASCII.GetBytes(strWriteTrackData[1]);
                        Array.Resize<byte>(ref TxData, tmp2.Length);
                        Array.Copy(tmp2, 0, TxData, 0, tmp2.Length);
                        txDataLen = (UInt16)TxData.Length;
                        RxDataLen = 0;
                        ReType = 0;
                        St0 = 0; St1 = 0;
                        ReturnSdkStatus = RS232_ExeCommand(Hdle, Cm, Pm, txDataLen, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                        if (ReturnSdkStatus == 0 && ReType == 0x50)
                            ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                        else
                        {
                            flgFailSendData = true;
                            return ReturnDeviceStatus.MB_Error;
                        }
                    }
                }
                if (WhichTrack[2])
                {
                    if (flgFailSendData)
                        return ReturnDeviceStatus.MB_Error;
                    else
                    {
                        Cm = 0x37; Pm = 0x38;
                        tmp2 = Encoding.ASCII.GetBytes(strWriteTrackData[2]);
                        Array.Resize<byte>(ref TxData, tmp3.Length);
                        Array.Copy(tmp3, 0, TxData, 0, tmp3.Length);
                        txDataLen = (UInt16)TxData.Length;
                        RxDataLen = 0;
                        ReType = 0;
                        St0 = 0; St1 = 0;
                        ReturnSdkStatus = RS232_ExeCommand(Hdle, Cm, Pm, txDataLen, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                        if (ReturnSdkStatus == 0 && ReType == 0x50)
                            ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                        else
                        {
                            flgFailSendData = true;
                            return ReturnDeviceStatus.MB_Error;
                        }
                    }
                }

                if (flgFailSendData)
                    return ReturnDeviceStatus.MB_Error;
                else
                {
                    Cm = 0x37; Pm = 0x39;
                    byte[] tmp4 = { };
                    txDataLen = (UInt16)tmp4.Length;
                    RxDataLen = 0;
                    ReType = 0;
                    St0 = 0; St1 = 0;
                    ReturnSdkStatus = RS232_ExeCommand(Hdle, Cm, Pm, txDataLen, tmp4, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                    if (ReturnSdkStatus == 0 && ReType == 0x50)
                    {
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                        ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    }
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;

            return ReturnStatus;
        }
        public static ReturnDeviceStatus WriteMagnetticMode(ref string DeviceResponse, MagneticWriteMode WriteMode)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;

            if (Hdle != 0)
            {
                byte Cm = 0, Pm = 0;
                UInt16 RxDataLen;
                byte[] TxData = { (byte)WriteMode, 0x30 };
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x37;
                Pm = 0x30;

                St0 = St1 = 0;
                RxDataLen = 0;

                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, (ushort)TxData.Length, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    if (ReType == 0x50)
                    {
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                    }
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (String.IsNullOrWhiteSpace(DeviceResponse)) DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }
        public static ReturnDeviceStatus WriteICCard(byte[] inputData, ref byte[] RxData)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            if (Hdle != 0)
            {
                CarryTheCardToICContactPosition();

                byte Cm, Pm;
                UInt16 TxDataLen, RxDataLen;

                byte ReType = 0;
                byte St0, St1;

                Cm = 0x49;
                Pm = 0x39;
                St0 = St1 = 0;
                TxDataLen = (UInt16)inputData.Length;
                RxDataLen = 0;

                int i = RS232_ExeCommand(Hdle, Cm, Pm, TxDataLen, inputData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    if (ReType == 0x50)
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
                ReleaseTheICContact();
            }
            else
            {
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            }
            return ReturnStatus;
        }
        #endregion
        #region All Functions About Chip Read and Write RFID
        public static ReturnDeviceStatus MoveCardToRFLocation(ref string DeviceResponse)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;

            if (Hdle != 0)
            {
                byte Cm = 0, Pm = 0;
                UInt16 RxDataLen;
                byte[] TxData = { };
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x40;
                Pm = 0x38;

                St0 = St1 = 0;
                RxDataLen = 0;

                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, (ushort)TxData.Length, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    if (ReType == 0x50)
                    {
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                    }
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (String.IsNullOrWhiteSpace(DeviceResponse)) DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }
        public static ReturnDeviceStatus RFIDActiveCard(ref string DeviceResponse, ref RFIDProperty RFProperty)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;
            bl.convertor convertor = new bl.convertor();

            if (Hdle != 0)
            {
                byte Cm = 0, Pm = 0;
                UInt16 RxDataLen;
                byte[] TxData = { 0x41, 0x42, 0x4D };
                byte[] RxData = new byte[280];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x5A;
                Pm = 0x30;

                St0 = St1 = 0;
                RxDataLen = 0;

                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, (ushort)TxData.Length, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    if (ReType == 0x50)
                    {
                        RFProperty.bytATQA[0] = RxData[2];
                        RFProperty.bytATQA[1] = RxData[1];
                        RFProperty.strATQA = convertor.ByteArrayWithSpaceToString(RFProperty.bytATQA);

                        switch (RxData[0])
                        {
                            case 65:
                                {
                                    RFProperty.RFIDType = "ISO / IEC 14443 Type A";                              //ISO/IEC 14443 Type A

                                    int UidLength = 0;

                                    if (Convert.ToBoolean(RxData[1] & 0x80))
                                    {
                                        UidLength = 10;
                                        RFProperty.bytUIDNumber = new byte[UidLength];
                                    }
                                    else if (Convert.ToBoolean(RxData[1] & 0x40))
                                    {
                                        UidLength = 7;
                                        RFProperty.bytUIDNumber = new byte[UidLength];
                                    }
                                    else
                                    {
                                        UidLength = 4;
                                        RFProperty.bytUIDNumber = new byte[UidLength];
                                    }
                                    for (int UIDFinder = 0; UIDFinder < (UidLength); UIDFinder++)
                                    {
                                        RFProperty.bytUIDNumber[UIDFinder] = RxData[UIDFinder + 3];
                                    }
                                    RFProperty.strUIDNumber = convertor.ByteArrayWithSpaceToString(RFProperty.bytUIDNumber);
                                    RFProperty.strUIDNumber = RFProperty.strUIDNumber.ToUpper();
                                    RFProperty.bytSAK = RxData[UidLength + 4];
                                    RFProperty.strSAK = RxData[UidLength + 4].ToString();
                                    RFProperty.strPPSS = RxData[RxDataLen].ToString();
                                    RFProperty.bytPPSS = RxData[RxDataLen];
                                    RFProperty.bytATS = new byte[RxDataLen - 8 - UidLength];
                                    for (int ATSFinder = 0; ATSFinder < (RxDataLen - 8 - UidLength); ATSFinder++)
                                    {
                                        RFProperty.bytATS[ATSFinder] = RxData[ATSFinder + 5 + UidLength];
                                    }
                                    RFProperty.strATS = convertor.ByteArrayWithSpaceToString(RFProperty.bytATS);

                                }
                                break;
                            case 66:
                                RFProperty.RFIDType = "ISO/IEC 14443 Type B";
                                break;
                            case 77:
                                RFProperty.bytUIDNumber = new byte[4];
                                for (int UIDFinder = 0; UIDFinder <= (RxDataLen - 5); UIDFinder++)
                                {
                                    RFProperty.bytUIDNumber[UIDFinder] = RxData[UIDFinder + 3];
                                }
                                RFProperty.strUIDNumber = convertor.ByteArrayWithSpaceToString(RFProperty.bytUIDNumber);
                                switch (RxData[RxDataLen - 1])
                                {
                                    case 0:
                                        RFProperty.RFIDType = "Mifare one UL";
                                        break;
                                    case 8:
                                        RFProperty.RFIDType = "Mifare one S50";
                                        break;
                                    case 24:
                                        RFProperty.RFIDType = "Mifare one S70";
                                        break;
                                }
                                break;
                            case 48:
                                RFProperty.RFIDType = "NoType";
                                break;
                            default:
                                RFProperty.RFIDType = "NoType";
                                break;
                        }
                        ReturnStatus = ReturnDeviceStatus.MB_OK;


                        //switch (RxData[0])
                        //{
                        //    case 65:

                        //        break;
                        //    case 66:
                        //        // SMaG&ISO/IEC 14443 Type B O�
                        //        for (i = 0; (i
                        //                    <= (BackDataBufLen - 10)); i++)
                        //        {
                        //            if ((BackDataBuf((i + 6)) < 16))
                        //            {
                        //                SATRTypeABText.Text = (SATRTypeABText.Text + "0");
                        //            }

                        //            SATRTypeABText.Text = (SATRTypeABText.Text + Hex(BackDataBuf((i + 6))));
                        //        }

                        //        SSTab1.Tab = 2;
                        //        SSTab5.Tab = 1;
                        //        Myexit = MsgBox(("activate at" + ('\r' + ('\n' + "ISO/IEC 14443 Type B protocol"))), (vbOKOnly + System.Windows.Forms.MessageBoxIcon.Information), "Activate");
                        //        break;
                        //    case 77:
                        //        // SMaG& Philps Mifare one card O�
                        //        string MessageStr;
                        //        switch (BackDataBuf((BackDataBufLen - 1)))
                        //        {
                        //            case 0:
                        //                MessageStr = "Mifare one UL";
                        //                break;
                        //            case 8:
                        //                MessageStr = "Mifare one S50";
                        //                break;
                        //            case 24:
                        //                MessageStr = "Mifare one S70";
                        //                break;
                        //        }
                        //        break;
                        //}













                    }
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (String.IsNullOrWhiteSpace(DeviceResponse)) DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }
        public static ReturnDeviceStatus RFIDDeActiveCard(ref string DeviceResponse)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;

            if (Hdle != 0)
            {
                byte Cm = 0, Pm = 0;
                UInt16 RxDataLen;
                byte[] TxData = { };
                byte[] RxData = new byte[5];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x5A;
                Pm = 0x31;

                St0 = St1 = 0;
                RxDataLen = 0;

                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, (ushort)TxData.Length, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    if (ReType == 0x50)
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (String.IsNullOrWhiteSpace(DeviceResponse)) DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }
        public static ReturnDeviceStatus RFIDActivitionCardInquireStatus(ref string DeviceResponse, ref RFIDActiveStatus Astatus)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;

            if (Hdle != 0)
            {
                byte Cm = 0, Pm = 0;
                UInt16 RxDataLen;
                byte[] TxData = { };
                byte[] RxData = new byte[1];
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x5A;
                Pm = 0x32;

                St0 = St1 = 0;
                RxDataLen = 0;

                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, (ushort)TxData.Length, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    if (ReType == 0x50)
                    {
                        if (RxData[0] == 0x40)
                            Astatus = RFIDActiveStatus.Deactive;
                        else if (RxData[0] == 0x43)
                            Astatus = RFIDActiveStatus.Active;
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                    }
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (String.IsNullOrWhiteSpace(DeviceResponse)) DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }
        public static ReturnDeviceStatus RFIDSendApdu(ref string DeviceResponse, byte[] ApduData, ref byte[] Receviedata)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            ReturnDeviceStatus ReturnResponse = ReturnDeviceStatus.MB_Error;

            if (Hdle != 0)
            {
                byte Cm = 0, Pm = 0;
                UInt16 RxDataLen;
                Array.Resize<byte>(ref Receviedata, 258);
                if (ApduData.Length > 261)
                    return ReturnDeviceStatus.MB_Error;
                byte ReType = 0;
                byte St0, St1;

                Cm = 0x5A;
                Pm = 0x34;

                St0 = St1 = 0;
                RxDataLen = 0;

                int i = CR.RS232_ExeCommand(Hdle, Cm, Pm, (ushort)ApduData.Length, ApduData, ref ReType, ref St0, ref St1, ref RxDataLen, Receviedata);
                if (i == 0)
                {
                    ReturnResponse = ShowCRError(ReType, St0, St1, ref DeviceResponse);
                    if (ReType == 0x50)
                    {
                        Array.Resize<byte>(ref Receviedata, RxDataLen);
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                    }
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (String.IsNullOrWhiteSpace(DeviceResponse)) DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }

        #endregion
        #region MyRegion
        private static ReturnDeviceStatus CarryTheCardToICContactPosition()
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            byte Cm = 0x40, Pm = 0x30;
            UInt16 RxDataLen = 0;
            byte[] TxData = { };
            UInt16 txDataLen = (UInt16)TxData.Length;
            byte[] RxData = new byte[5];
            byte ReType = 0;
            byte St0 = 0, St1 = 0;
            int i = RS232_ExeCommand(Hdle, Cm, Pm, txDataLen, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
            if (i == 0)
            {
                if (ReType == 0x50)
                    ReturnStatus = ReturnDeviceStatus.MB_OK;
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_Error;

            return ReturnStatus;
        }
        private static ReturnDeviceStatus ReleaseTheICContact()
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            byte Cm = 0x40, Pm = 0x32;
            UInt16 RxDataLen = 0;
            byte[] TxData = { };
            UInt16 txDataLen = (UInt16)TxData.Length;
            byte[] RxData = new byte[5];
            byte ReType = 0;
            byte St0 = 0, St1 = 0;
            int i = RS232_ExeCommand(Hdle, Cm, Pm, txDataLen, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
            if (i == 0)
            {
                if (ReType == 0x50)
                    ReturnStatus = ReturnDeviceStatus.MB_OK;
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_Error;

            return ReturnStatus;
        }
        #endregion
    }
}
