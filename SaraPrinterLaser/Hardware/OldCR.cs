using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SaraPrinterLaser.Hardware
{
    class CR
    {
        #region All Imported Dlls
        public static uint Hdle { get; set; }
        [DllImport("CRT_350V3.dll")]
        public static extern UInt32 CRT350ROpen(string port);
        [DllImport("CRT_350V3.dll")]
        public static extern UInt32 CRT350ROpenWithBaut(string port, UInt32 Baudrate);
        [DllImport("CRT_350V3.dll")]
        public static extern int CRT350RClose(UInt32 ComHandle);
        [DllImport("CRT_350V3.dll")]
        public static extern int RS232_ExeCommand(UInt32 ComHandle, byte TxCmCode, byte TxPmCode, UInt16 TxDataLen, byte[] TxData, ref byte RxReplyType, ref byte RxStCode0, ref byte RxStCode1, ref UInt16 RxDataLen, byte[] RxData);
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
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (DeviceResponse == null || DeviceResponse == "") DeviceResponse = "Command execution is impossible";
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
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (DeviceResponse == null || DeviceResponse == "") DeviceResponse = "Command execution is impossible";
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
                        ReturnStatus = ReturnDeviceStatus.MB_OK;
                    else
                        ReturnStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    ReturnStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;
            if (DeviceResponse == null || DeviceResponse == "") DeviceResponse = "Command execution is impossible";
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
            if (DeviceResponse == null || DeviceResponse == "") DeviceResponse = "Command execution is impossible";
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
            if (DeviceResponse == null || DeviceResponse == "") DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }

        public static ReturnDeviceStatus getBackSensorStatus(ref string DeviceResponse, ref bool GateSensorValue)
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
            if (DeviceResponse == null || DeviceResponse == "") DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }

        public static ReturnDeviceStatus getInternalSensorStatus(ref string DeviceResponse, ref bool GateSensorValue)
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
            if (DeviceResponse == null || DeviceResponse == "") DeviceResponse = "Command execution is impossible";
            return ReturnStatus;
        }
        #endregion all function can be move card
        #region All Functions About Chip Read and Write
        public static ReturnDeviceStatus WriteAllMagneticTrack(string[] strWriteTrackData)
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            if (Hdle != 0)
            {
                if (strWriteTrackData.Length > 3) return ReturnDeviceStatus.MB_Error;


                if (strWriteTrackData[0] == null) strWriteTrackData[0] = "";
                if (strWriteTrackData[1] == null) strWriteTrackData[1] = "";
                if (strWriteTrackData[2] == null) strWriteTrackData[2] = "";

                byte[] tmp1 = Encoding.ASCII.GetBytes(strWriteTrackData[0]);
                byte[] tmp2 = Encoding.ASCII.GetBytes(strWriteTrackData[1]);
                byte[] tmp3 = Encoding.ASCII.GetBytes(strWriteTrackData[2]);

                byte Cm = 0x37, Pm = 0x36;
                UInt16 RxDataLen = 0;
                byte[] TxData = tmp1;
                UInt16 txDataLen = (UInt16)TxData.Length;
                byte[] RxData = new byte[1024];
                byte ReType = 0;
                byte St0 = 0, St1 = 0;

                int i = RS232_ExeCommand(Hdle, Cm, Pm, txDataLen, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                if (i == 0)
                {
                    Cm = 0x37; Pm = 0x37;
                    TxData = tmp2;
                    txDataLen = (UInt16)TxData.Length;
                    RxDataLen = 0;
                    ReType = 0;
                    St0 = 0; St1 = 0;
                    int j = RS232_ExeCommand(Hdle, Cm, Pm, txDataLen, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                    if (j == 0)
                    {
                        Cm = 0x37; Pm = 0x38;
                        TxData = tmp3;
                        txDataLen = (UInt16)TxData.Length;
                        RxDataLen = 0;
                        ReType = 0;
                        St0 = 0; St1 = 0;
                        int K = RS232_ExeCommand(Hdle, Cm, Pm, txDataLen, TxData, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                        if (K == 0)
                        {
                            Cm = 0x37; Pm = 0x39;
                            byte[] TxData1 = { };
                            txDataLen = (UInt16)TxData1.Length;
                            RxDataLen = 0;
                            ReType = 0;
                            St0 = 0; St1 = 0;
                            int Z = RS232_ExeCommand(Hdle, Cm, Pm, txDataLen, TxData1, ref ReType, ref St0, ref St1, ref RxDataLen, RxData);
                            if (Z == 0)
                                ReturnStatus = ReturnDeviceStatus.MB_OK;
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



                //if (strWriteTrackData[0] == "") SendData1[3] = 0x50;
                //if (strWriteTrackData[1] == "") SendData2[3] = 0x20;
                //if (strWriteTrackData[2] == "") SendData3[3] = 0x20;




            }
            else
                ReturnStatus = ReturnDeviceStatus.MB_PortNotOpen;

            return ReturnStatus;
        }
        private static ReturnDeviceStatus CarryTheCardToICContactPosition()
        {
            ReturnDeviceStatus ReturnStatus = ReturnDeviceStatus.MB_Error;
            byte Cm = 0x40, Pm = 0x30;
            UInt16 RxDataLen = 0;
            byte[] TxData = { };
            UInt16 txDataLen = (UInt16)TxData.Length;
            byte[] RxData = new byte[1024];
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
            byte[] RxData = new byte[1024];
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


        #endregion All Functions About Chip Read and Write
    }
}
