using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraPrinterLaser.Hardware
{
     public class CommandGenrator
    {

        public enum ModuleType { DispenserModule, CardHolderModule };
        public static string DispenserAddress { get; set; }
        public static string CardHolderAddress { get; set; }
        public enum ModulesCommand : uint
        {
            checkdevice = 0x39393939,
            DispenserModule_PickupStacker1 = 0x6E6E6E6E,
            DispenserModule_PickupStacker2 = 0x87878787,
            DispenserModule_SendSensorsStatusRequest = 0x3B3B3B3B,
            DispenserModule_SendSerialNumber = 0xB3B3B3B3,
            DispenserModule_SendLocalSerialNumber = 0xE6E6E6E6,
            DispenserModule_ClearDispenserCardJam = 0x18181818,

            CardHolderModule_ReceiveCardForMarkingValue = 0x6E6E6E6E,
            CardHolderModule_RotateCard = 0x87878787,
            CardHolderModule_MoveCardToCR = 0x88888888,
            CardHolderModule_ClearRejectBox = 0x15151515,
            CardHolderModule_MoveToRejectBox = 0x68686868,
            CardHolderModule_FlipperMotorEnableOff = 0x6F6F6F6F,
            CardHolderModule_SendMachineCounter = 0x86868686,
            CardHolderModule_ResetSendMachineCounter = 0x80808080,
            CardHolderModule_RequestFlipperinstall = 0x60606060,
            CardHolderModule_SendSerialNumber = 0xB3B3B3B3,
            CardHolderModule_SendLocalSerialNumber = 0xE6E6E6E6,
            CardHolderModule_ClearCardJam = 0x05050505,
            CardHolderModule_FanOn = 0x32323232,
            CardHolderModule_FanOff = 0x33333333

        };
        public enum ReturnStatusOfTheModules
        {
            ErrorCode_Timeout,
            ErrorCode_Fail,
            ErrorCode_CardEmpty,
            ErrorCode_NoStackerDetect,
            ErrorCode_CardJam,
            ErrorCode_undefined,
            ErrorCode_DeviceBusy,
            ErrorCode_FailDataReceive,
            ErrorCode_WithoutFlipper,
            ErrorCode_InputDataIncorrect, //This mean The Input Data To Function is Incorrect**This is not For machine

            WarningCode_CardCapacityWarning,

            StatusCode_Ok,
            SerialNumber_Ok,
            StatusCode_TrueReceive,
            StatusCode_DeviceHaveFlipper

        };
        public enum StatusOfTheModules : uint
        {
            ErrorCode_Timeout = 0x76767676,
            ErrorCode_Fail = 0x80808080,
            ErrorCode_CardEmpty = 0xD576D576,
            ErrorCode_NoStackerDetect = 0xD580D580,
            ErrorCode_CardJam = 0x44444444,
            ErrorCode_undefined = 0x876E876E,
            ErrorCode_DeviceBusy = 0x14141414,
            ErrorCode_FailDataReceive = 0x95857565,
            ErrorCode_WithoutFlipper = 0x76885489,

            WarningCode_CardCapacityWarning = 0x44144414,

            StatusCode_Ok = 0xD5D5D5D5,
            StatusCode_TrueReject = 0x12111009,
            StatusCode_TrueReceive = 0x65656565,
            StatusCode_DeviceHaveFlipper = 0x5D5D5D5D

        };
        public  string CommandGeneratorForModule(ModulesCommand Inputbytes, ModuleType whichModule)
        {
            string RecevieData = Inputbytes.ToString("X");
            if (RecevieData.Length != 8) return null;
            byte[] bytes = StringToByteArray(RecevieData);
            if (bytes.Length != 4) return null;
            byte[] Senddata = new byte[9];
            if (bytes == null) return null;
            int CheckSum = 0;
            string FinalReturnVar = null;
            if (whichModule == ModuleType.DispenserModule)
            {
                Senddata[0] = 0xAA;
                Senddata[1] = 0x55;
                Senddata[2] = 0x0A;
                Senddata[3] = bytes[0];
                Senddata[4] = bytes[1];
                Senddata[5] = bytes[2];
                Senddata[6] = bytes[3];
                for (int i = 0; i < 8; i++) CheckSum += Senddata[i];
                Senddata[7] = (byte)(CheckSum / 0x100);
                Senddata[8] = (byte)(CheckSum % 0x100);
                for (int i = 0; i < Senddata.Length; i++)
                {
                    if (Senddata[i] > 0x0F) FinalReturnVar += Senddata[i].ToString("X");
                    else
                    {
                        FinalReturnVar += "0";
                        FinalReturnVar += Senddata[i].ToString("X");
                    }
                }
            }
            else if (whichModule == ModuleType.CardHolderModule)
            {
                Senddata[0] = 0xAA;
                Senddata[1] = 0x55;
                Senddata[2] = 0x05;
                Senddata[3] = bytes[0];
                Senddata[4] = bytes[1];
                Senddata[5] = bytes[2];
                Senddata[6] = bytes[3];
                for (int i = 0; i < 8; i++) CheckSum += Senddata[i];
                Senddata[7] = (byte)(CheckSum / 0x100);
                Senddata[8] = (byte)(CheckSum % 0x100);
                for (int i = 0; i < Senddata.Length; i++)
                {
                    if (Senddata[i] > 0x0F) FinalReturnVar += Senddata[i].ToString("X");
                    else
                    {
                        FinalReturnVar += "0";
                        FinalReturnVar += Senddata[i].ToString("X");
                    }
                }
            }
            else
                return null;
            return FinalReturnVar;
        }
        
        public ReturnStatusOfTheModules StatusGenratorFromModule(string InputData)
        {
            if (InputData.Length != 18) return ReturnStatusOfTheModules.ErrorCode_InputDataIncorrect;
            byte[] bytes = StringToByteArray(InputData);
            if (bytes.Length != 9) return ReturnStatusOfTheModules.ErrorCode_InputDataIncorrect;
            if (bytes == null) return ReturnStatusOfTheModules.ErrorCode_InputDataIncorrect;
            int CheckSum = 0, ReceviveCheckSum = 0;
            string DataCapture = null;
            ReceviveCheckSum = ((bytes[7] * 0x100) + bytes[8]);
            for (int i = 0; i < (bytes.Length - 2); i++) CheckSum += bytes[i];

            if (ReceviveCheckSum == CheckSum)
            {
                if (bytes[0] == 0xAA && bytes[1] == 0x55) // This Mean Header Bytes is True
                {
                    if ((bytes[2] & 0x0F) == 0x05)// || (bytes[2] & 0x0F) == 0x05)// This Mean device Address  is True
                    {
                        if ((bytes[3] & 0xF0) == 0xC0)
                        {
                            switch (bytes[3])
                            {
                                case 0xCA:
                                    CardHolderAddress = "MLF600";
                                    DispenserAddress = "MLF600";
                                    break;
                                case 0xCB:
                                    CardHolderAddress = "MLF580";
                                    DispenserAddress = "MLF580";
                                    break;
                                case 0xCC:
                                    CardHolderAddress = "MLF550";
                                    DispenserAddress = "MLF550";
                                    break;
                                case 0xCD:
                                    CardHolderAddress = "MLF500";
                                    DispenserAddress = "MLF500";
                                    break;
                                default:
                                    return ReturnStatusOfTheModules.ErrorCode_InputDataIncorrect;

                            }
                            if (bytes[4] < 0x0F) CardHolderAddress += "0" + bytes[4].ToString();
                            else CardHolderAddress += bytes[4].ToString();

                            if (bytes[5] != 0) CardHolderAddress += (bytes[5] * 0x100).ToString();
                            else CardHolderAddress += "00";

                            if (bytes[6] < 0x0F) CardHolderAddress += "0" + bytes[6].ToString();
                            else CardHolderAddress += bytes[6].ToString();

                            if (bytes[4] < 0x0F) DispenserAddress += "0" + bytes[4].ToString();
                            else DispenserAddress += bytes[4].ToString();

                            if (bytes[5] != 0) DispenserAddress += (bytes[5] * 0x100).ToString();
                            else DispenserAddress += "00";

                            if (bytes[6] < 0x0F) DispenserAddress += "0" + bytes[6].ToString();
                            else DispenserAddress += bytes[6].ToString();
                            return ReturnStatusOfTheModules.SerialNumber_Ok;


                        }
                        else if ((bytes[3] & 0xF0) == 0xF0)
                        {

                            switch (bytes[3])
                            {
                                case 0xFA:
                                    DispenserAddress = "MLF600";
                                    break;
                                case 0xFB:
                                    DispenserAddress = "MLF580";
                                    break;
                                case 0xFC:
                                    DispenserAddress = "MLF550";
                                    break;
                                case 0xFD:
                                    DispenserAddress = "MLF500";
                                    break;
                                default:
                                    return ReturnStatusOfTheModules.ErrorCode_InputDataIncorrect;

                            }

                            if (bytes[4] < 0x0F) DispenserAddress += "0" + bytes[4].ToString();
                            else DispenserAddress += bytes[4].ToString();

                            if (bytes[5] != 0) DispenserAddress += (bytes[5] * 0x100).ToString();
                            else DispenserAddress += "00";

                            if (bytes[6] < 0x0F) DispenserAddress += "0" + bytes[6].ToString();
                            else DispenserAddress += bytes[6].ToString();
                        }
                        else
                        {

                            for (int i = 3; i < 7; i++)
                            {
                                if (bytes[i] > 0x0F) DataCapture += bytes[i].ToString("X");
                                else
                                {
                                    DataCapture += "0";
                                    DataCapture += bytes[i].ToString("X");
                                }
                            }
                            if (DataCapture == StatusOfTheModules.ErrorCode_Timeout.ToString("X")) return ReturnStatusOfTheModules.ErrorCode_Timeout;
                            else if (DataCapture == StatusOfTheModules.ErrorCode_Fail.ToString("X")) return ReturnStatusOfTheModules.ErrorCode_Fail;
                            else if (DataCapture == StatusOfTheModules.ErrorCode_CardEmpty.ToString("X")) return ReturnStatusOfTheModules.ErrorCode_CardEmpty;
                            else if (DataCapture == StatusOfTheModules.ErrorCode_NoStackerDetect.ToString("X")) return ReturnStatusOfTheModules.ErrorCode_NoStackerDetect;
                            else if (DataCapture == StatusOfTheModules.ErrorCode_CardJam.ToString("X")) return ReturnStatusOfTheModules.ErrorCode_CardJam;
                            else if (DataCapture == StatusOfTheModules.ErrorCode_undefined.ToString("X")) return ReturnStatusOfTheModules.ErrorCode_undefined;
                            else if (DataCapture == StatusOfTheModules.ErrorCode_DeviceBusy.ToString("X")) return ReturnStatusOfTheModules.ErrorCode_DeviceBusy;
                            else if (DataCapture == StatusOfTheModules.ErrorCode_FailDataReceive.ToString("X")) return ReturnStatusOfTheModules.ErrorCode_FailDataReceive;
                            else if (DataCapture == StatusOfTheModules.ErrorCode_WithoutFlipper.ToString("X")) return ReturnStatusOfTheModules.ErrorCode_WithoutFlipper;
                            else if (DataCapture == StatusOfTheModules.WarningCode_CardCapacityWarning.ToString("X")) return ReturnStatusOfTheModules.WarningCode_CardCapacityWarning;
                            else if (DataCapture == StatusOfTheModules.StatusCode_Ok.ToString("X")) return ReturnStatusOfTheModules.StatusCode_Ok;
                            else if (DataCapture == StatusOfTheModules.StatusCode_TrueReceive.ToString("X")) return ReturnStatusOfTheModules.StatusCode_TrueReceive;
                            else if (DataCapture == StatusOfTheModules.StatusCode_DeviceHaveFlipper.ToString("X")) return ReturnStatusOfTheModules.StatusCode_DeviceHaveFlipper;
                            else return ReturnStatusOfTheModules.ErrorCode_InputDataIncorrect;
                        }
                    }
                    else
                        return ReturnStatusOfTheModules.ErrorCode_InputDataIncorrect;
                }
                else
                    return ReturnStatusOfTheModules.ErrorCode_InputDataIncorrect;
            }
            else
                return ReturnStatusOfTheModules.ErrorCode_InputDataIncorrect;

            return ReturnStatusOfTheModules.StatusCode_Ok;

        }
        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            try
            {
                for (int i = 0; i < NumberChars; i += 2)
                    bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            catch
            {
                return null;
            }
            return bytes;
        }
        public static string ByteArrayToString(byte[] InputData)
        {
            string OutputResponse = null;
            for (int i = 0; i < InputData.Length; i++)
            {
                if (InputData[i] > 0x0F) OutputResponse += InputData[i].ToString("X");
                else
                {
                    OutputResponse += "0";
                    OutputResponse += InputData[i].ToString("X");
                }
            }
            return OutputResponse;
        }

        public static ReturnStatusOfTheModules ExchangeDataWithModules(SerialPort whichSerialPort, ModuleType WhichModule, ModulesCommand Inputbytes)
        {
            ReturnStatusOfTheModules DataType = new ReturnStatusOfTheModules();

            return DataType;
        }



    }
}