using SaraPrinterLaser.bl;
using SaraPrinterLaser.Hardware;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FTD2XX_NET;
using static FTD2XX_NET.FTDI;

namespace SaraPrinterLaser.Hardware
{
    public class Config
    {
        public static bool DispenserState;
        public static bool CardHolderState;
        public static bool RotateState;
        public static bool RFIDState;
        public static bool laserState;
        public static bool CrState;
        public static bool DeviceAdressState = false;
        public static bool MachineCounterState = false;
        public static bool flgTimeoutRejectBox { get; set; }
        public static bool getDeviceAddress { get; set; }
        public static bool getMachineCounter { get; set; }
        public static UInt32 DeviceCounter { get; set; }
        public struct StutDispenserSensorVariable
        {
            public static bool flgGetDispenserSensorStatus { get; set; }
            public static bool flgGetDispenserSensorStatusSucsess { get; set; }
            public static UInt32 DispenserSensorTotalValue { get; set; }
            public static bool flgGetDispenserSensorStatusRetry { get; set; }
            public static bool flgDispenserSensor_EndWayCardDetector { get; set; }
            public static bool flgDispenserSensor_IRCardCapacityDetector1 { get; set; }
            public static bool flgDispenserSensor_IRCardCapacityDetector2 { get; set; }
            public static bool flgDispenserSensor_CardStackerDetector2 { get; set; }
            public static bool flgDispenserSensor_CardStackerDetector1 { get; set; }
            public static bool flgDispenserSensor_coilCardCapacityDetector1 { get; set; }
            public static bool flgDispenserSensor_coilCardCapacityDetector2 { get; set; }
            public static bool flgDispenserSensor_Sensor3 { get; set; }
            public static bool flgDispenserSensor_Sensor2_2 { get; set; }
            public static bool flgDispenserSensor_Sensor2_1 { get; set; }
            public static bool flgDispenserSensor_Sensor1_2 { get; set; }
            public static bool flgDispenserSensor_Sensor1_1 { get; set; }
        }


        public static SerialPort Dispenser = new SerialPort();
        public static SerialPort CardHolder = new SerialPort();


        public static string CrPortName;
        public static CommandGenrator ComGen { get; set; }
        public static bool control { get; set; }
        public static bool Start { get; set; }
        public static bool FlgCheckFiliper { get; set; }
        public static bool NoDeviceExist { get; set; }


        public static void LoadHardWare()
        {
            FTDI FTDIComports = new FTDI();
            CommandGenrator ExchangeData = new CommandGenrator();
            FT_STATUS PortStatus = new FT_STATUS();
            FT_DEVICE_INFO_NODE[] PortList = new FT_DEVICE_INFO_NODE[3];
            SerialPort a = new SerialPort();
            Config.Start = true;
            bool flgCR = false;
            //checkRFID
            if ((new Hardware.nfc().ReaderConnect()))
            {
                Config.RFIDState = true;
            }
            string[] MachineConfig = FileWork.ReadAllSecureConfig();

            byte[] command = convertor.StringToByteArray("AA553939393939021C");
            PortStatus = FTDIComports.GetDeviceList(PortList);
            if (PortList[0] == null && PortList[1] == null && PortList[2] == null) NoDeviceExist = true;

            if (!NoDeviceExist && MachineConfig != null &&
               !String.IsNullOrWhiteSpace(MachineConfig[0]) &&
                !String.IsNullOrWhiteSpace(MachineConfig[1]) &&
                !String.IsNullOrWhiteSpace(MachineConfig[0]))
            {

                string[] Comlist = SerialPort.GetPortNames();
                foreach (var item in Comlist)
                {
                    if (item == MachineConfig[0] || item == MachineConfig[1] || item == MachineConfig[2])
                    {
                        if (Config.CardHolderState && Config.DispenserState)
                            break;
                        try
                        {
                            Config.control = true;
                            a = new SerialPort(item, 115200, Parity.None, 8);
                            a.DataReceived += A_DataReceived;
                            a.Open();
                            Int64 i = 0;
                            Int64 j = 0;
                            while (Config.control)
                            {
                                if (i == 0)
                                {
                                    a.DiscardInBuffer();
                                    a.DiscardOutBuffer();
                                    a.Write(command, 0, command.Length);
                                    j++;
                                }
                                if (j > 5)
                                {
                                    Config.control = false;
                                }
                                i++;
                                System.Threading.Thread.Sleep(1);
                                if (i > 50) i = 0;
                            }
                            a.Close();
                        }
                        catch (Exception)
                        {


                        }
                    }
                }
                Config.Start = false;
                foreach (var item in SerialPort.GetPortNames())
                {
                    if (item != Config.Dispenser.PortName && item != Config.CardHolder.PortName && !Config.CrState)
                    {
                        CR.Hdle = CR.CRT350ROpenWithBaut(item, 115200);
                        if (CR.Hdle != 0)
                        {
                            CrPortName = item;
                            Config.CrState = true;
                        }
                    }
                }
                if (!Config.CrState)
                {
                    foreach (var item in SerialPort.GetPortNames())
                    {
                        if (item != Config.Dispenser.PortName && item != Config.CardHolder.PortName && !Config.CrState)
                        {
                            CR.Hdle = CR.CRT350ROpen(item);
                            if (CR.Hdle != 0)
                            {
                                CrPortName = item;
                                Config.CrState = true;
                            }
                        }
                    }
                }

                Config.FlgCheckFiliper = true;
                if (Config.CardHolderState)
                {
                    if (Config.CardHolderState)
                        Config.CardHolder.Open();
                    Config.CardHolder.DiscardInBuffer();
                    Config.CardHolder.DiscardOutBuffer();
                    Config.RequestFlipperinstall();
                    int intRequestFlipperinstallCounter = 0;
                    while (Config.FlgCheckFiliper && intRequestFlipperinstallCounter < 50)
                    {
                        System.Threading.Thread.Sleep(100);
                        intRequestFlipperinstallCounter++;
                        if (intRequestFlipperinstallCounter == 25) Config.RequestFlipperinstall();
                    }
                }

                int counter = 0;
                Config.getDeviceAddress = true;
                do
                {
                    Config.CardHolderModule_SendSerialNumber();

                    counter++;
                    Thread.Sleep(200);
                }
                while (getDeviceAddress && !Config.DeviceAdressState && counter < 100);

                //  check laser
                StatusClass ReturnSatus = new StatusClass();
                ReturnSatus = Laser.DeviceInitialize(System.IO.Path.GetDirectoryName(Application.ExecutablePath));
                if (ReturnSatus.ResponseReturnStatus == StatusClass.ResponseStatus.Ok)
                    laserState = true;






                Config.CloseAllPortExceptLaser();
            }
        }
        public static void A_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (!Config.getMachineCounter && Config.Start && !Config.FlgCheckFiliper && !Config.StutDispenserSensorVariable.flgGetDispenserSensorStatus && !Config.getDeviceAddress)
                {
                    int bytes = ((SerialPort)sender).BytesToRead;
                    byte[] buffer = new byte[bytes];
                    ((SerialPort)sender).Read(buffer, 0, bytes);
                    string answer = convertor.ByteArrayToString(buffer);
                    if (answer == "aa552a0a0a0a0a0151" && !Config.DispenserState)
                    {
                        Config.Dispenser = (SerialPort)sender;
                        Config.DispenserState = true;
                        Config.control = false;
                    }
                    else if (answer == "aa5525050505050138" && !Config.CardHolderState)
                    {
                        Config.CardHolder = (SerialPort)sender;
                        Config.CardHolderState = true;
                        Config.control = false;

                    }
                    else
                    {
                        ((SerialPort)sender).Close();

                    }

                }
                else if (!Config.getMachineCounter && !Config.Start && Config.FlgCheckFiliper && !Config.StutDispenserSensorVariable.flgGetDispenserSensorStatus && !Config.getDeviceAddress)
                {
                    if (((SerialPort)sender).BytesToRead > 17)
                    {
                        int bytes = ((SerialPort)sender).BytesToRead;
                        byte[] buffer = new byte[bytes];
                        ((SerialPort)sender).Read(buffer, 0, bytes);
                        string answer = convertor.ByteArrayToString(buffer);
                        if (answer == "aa55256565656502b8aa558576885489035f")
                        {
                            Config.RotateState = false;
                            Config.FlgCheckFiliper = false;
                        }
                        else if (answer == "aa55256565656502b8aa55255d5d5d5d0298")
                        {
                            Config.RotateState = true;
                            Config.FlgCheckFiliper = false;
                        }
                    }

                }
                else if (!Config.getMachineCounter && !Config.Start && !Config.FlgCheckFiliper && Config.StutDispenserSensorVariable.flgGetDispenserSensorStatus && !Config.getDeviceAddress)
                {

                    if (((SerialPort)sender).BytesToRead > 17)
                    {
                        int bytes = ((SerialPort)sender).BytesToRead;
                        byte[] buffer = new byte[bytes];
                        int checkSum = 0, CheckChsum = 0;
                        byte[] SensorStatus = new byte[9];
                        ((SerialPort)sender).Read(buffer, 0, bytes);
                        string answer = convertor.ByteArrayToString(buffer);
                        if (answer.Contains("aa552a6565656502bd"))
                        {
                            Array.Copy(buffer, 9, SensorStatus, 0, 9);
                            checkSum = (SensorStatus[7] * 0x100) + SensorStatus[8];
                            if (SensorStatus[0] == 0xAA && SensorStatus[1] == 0x55 && SensorStatus[2] == 0x2A)
                            {
                                for (int i = 0; i < 7; i++)
                                    CheckChsum += SensorStatus[i];
                                if (checkSum == CheckChsum)
                                {
                                    Config.StutDispenserSensorVariable.DispenserSensorTotalValue = 0;
                                    Config.StutDispenserSensorVariable.DispenserSensorTotalValue += (UInt32)(SensorStatus[3] * 0x1000000);
                                    Config.StutDispenserSensorVariable.DispenserSensorTotalValue += (UInt32)(SensorStatus[4] * 0x10000);
                                    Config.StutDispenserSensorVariable.DispenserSensorTotalValue += (UInt32)(SensorStatus[5] * 0x100);
                                    Config.StutDispenserSensorVariable.DispenserSensorTotalValue += (UInt32)(SensorStatus[6]);

                                    Config.StutDispenserSensorVariable.flgDispenserSensor_EndWayCardDetector = Convert.ToBoolean(Config.StutDispenserSensorVariable.DispenserSensorTotalValue & 0x80000000);
                                    Config.StutDispenserSensorVariable.flgDispenserSensor_IRCardCapacityDetector1 = Convert.ToBoolean(Config.StutDispenserSensorVariable.DispenserSensorTotalValue & 0x40000000);
                                    Config.StutDispenserSensorVariable.flgDispenserSensor_IRCardCapacityDetector2 = Convert.ToBoolean(Config.StutDispenserSensorVariable.DispenserSensorTotalValue & 0x20000000);
                                    Config.StutDispenserSensorVariable.flgDispenserSensor_CardStackerDetector2 = Convert.ToBoolean(Config.StutDispenserSensorVariable.DispenserSensorTotalValue & 0x10000000);
                                    Config.StutDispenserSensorVariable.flgDispenserSensor_CardStackerDetector1 = Convert.ToBoolean(Config.StutDispenserSensorVariable.DispenserSensorTotalValue & 0x08000000);
                                    Config.StutDispenserSensorVariable.flgDispenserSensor_coilCardCapacityDetector1 = Convert.ToBoolean(Config.StutDispenserSensorVariable.DispenserSensorTotalValue & 0x04000000);
                                    Config.StutDispenserSensorVariable.flgDispenserSensor_coilCardCapacityDetector2 = Convert.ToBoolean(Config.StutDispenserSensorVariable.DispenserSensorTotalValue & 0x02000000);
                                    Config.StutDispenserSensorVariable.flgDispenserSensor_Sensor3 = Convert.ToBoolean(Config.StutDispenserSensorVariable.DispenserSensorTotalValue & 0x01000000);
                                    Config.StutDispenserSensorVariable.flgDispenserSensor_Sensor2_2 = Convert.ToBoolean(Config.StutDispenserSensorVariable.DispenserSensorTotalValue & 0x00800000);
                                    Config.StutDispenserSensorVariable.flgDispenserSensor_Sensor2_1 = Convert.ToBoolean(Config.StutDispenserSensorVariable.DispenserSensorTotalValue & 0x00400000);
                                    Config.StutDispenserSensorVariable.flgDispenserSensor_Sensor1_2 = Convert.ToBoolean(Config.StutDispenserSensorVariable.DispenserSensorTotalValue & 0x00200000);
                                    Config.StutDispenserSensorVariable.flgDispenserSensor_Sensor1_1 = Convert.ToBoolean(Config.StutDispenserSensorVariable.DispenserSensorTotalValue & 0x00100000);
                                    Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusRetry = false;
                                    Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusSucsess = true;


                                }
                            }

                        }
                        else
                        {
                            Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusRetry = true;
                            Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusSucsess = false;
                        }


                    }
                }
                else if (!Config.getMachineCounter && !Config.Start && !Config.FlgCheckFiliper && !Config.StutDispenserSensorVariable.flgGetDispenserSensorStatus && Config.getDeviceAddress)
                {
                    getDeviceAddress = false;
                    if (((SerialPort)sender).BytesToRead > 17)
                    {
                        int bytes = ((SerialPort)sender).BytesToRead;
                        byte[] buffer = new byte[bytes];
                        ((SerialPort)sender).Read(buffer, 0, bytes);
                        string answer = convertor.ByteArrayToString(buffer);
                        if (answer.Contains("aa55256565656502b8"))
                        {
                            answer = answer.Replace("aa55256565656502b8", "");
                            ComGen = new CommandGenrator();
                            if (ComGen.StatusGenratorFromModule(answer) == CommandGenrator.ReturnStatusOfTheModules.SerialNumber_Ok)
                                Config.DeviceAdressState = true;
                            else
                                Config.DeviceAdressState = false;
                        }
                        else
                            Config.DeviceAdressState = false;

                    }
                }
                else if (Config.getMachineCounter && !Config.Start && !Config.FlgCheckFiliper && !Config.StutDispenserSensorVariable.flgGetDispenserSensorStatus && !Config.getDeviceAddress)
                {
                    getMachineCounter = false;
                    Thread.Sleep(500);
                    if (((SerialPort)sender).BytesToRead > 60)
                    {
                        int bytes = ((SerialPort)sender).BytesToRead;
                        byte[] buffer = new byte[bytes];
                        ((SerialPort)sender).Read(buffer, 0, bytes);
                        string answer = convertor.ByteArrayToString(buffer);
                        if (answer.Contains("aa55256565656502b8"))
                        {
                            if (answer.Contains("aa5525d5d5d5d50478"))
                            {
                                Config.DeviceCounter += (UInt32)buffer[39] * 0x1000000;
                                Config.DeviceCounter += (UInt32)buffer[40] * 0x10000;
                                Config.DeviceCounter += (UInt32)buffer[41] * 0x100;
                                Config.DeviceCounter += (UInt32)buffer[42] * 0x1;
                                Config.MachineCounterState = true;
                            }
                        }
                        else
                            Config.DeviceAdressState = false;

                    }
                }
                else
                {
                    if (!Worker.flgCardHolderJam)
                    {
                        FileWork.Answered = true;
                        int counterTime = 0;
                        while (((SerialPort)sender).BytesToRead < 9 && counterTime < 300)
                        {
                            counterTime++;
                            System.Threading.Thread.Sleep(1);
                        }
                        int bytes = ((SerialPort)sender).BytesToRead;
                        byte[] buffer = new byte[bytes];
                        ((SerialPort)sender).Read(buffer, 0, bytes);

                        string remashin = convertor.ByteArrayToString(buffer);
                        if (remashin.Length <= 18)
                        {
                            FileWork.writeAnswerMashin(remashin);
                        }
                        else
                        {
                            if (true)
                            {

                            }
                            for (int i = 0; i < remashin.Length / 18; i++)
                            {
                                FileWork.writeAnswerMashin(remashin.Substring(i * 18, 18));
                            }
                        }
                    }

                }
            }
            catch (Exception)
            {


            }
        }
        public static void CloseAllPortExceptLaser()
        {
            try
            {
                Thread.Sleep(1000);
                if (Config.Dispenser.IsOpen)
                {
                    Config.Dispenser.DiscardInBuffer();
                    Config.Dispenser.DiscardInBuffer();
                    Config.Dispenser.Close();
                }
                Thread.Sleep(1000);
                if (Config.CardHolder.IsOpen)
                {
                    Config.CardHolder.DiscardInBuffer();
                    Config.CardHolder.DiscardInBuffer();
                    Config.CardHolder.Close();
                }
                //if (CR.Hdle != 0)
                //{
                //    CR.CRT350RClose(CR.Hdle);
                //}
            }
            catch (Exception)
            {
                //  MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void OpenAllPortExceptLaser()
        {
            try
            {
                Thread.Sleep(500);
                if (!Config.Dispenser.IsOpen)
                {
                    Config.Dispenser.Open();
                    Config.Dispenser.DiscardInBuffer();
                    Config.Dispenser.DiscardInBuffer();

                }
                Thread.Sleep(500);
                if (!Config.CardHolder.IsOpen)
                {
                    Config.CardHolder.Open();
                    Config.CardHolder.DiscardInBuffer();
                    Config.CardHolder.DiscardInBuffer();

                }
                //if (CR.Hdle == 0)
                //{
                //    CR.CRT350ROpen(Config.CrPortName);
                //}

            }
            catch (Exception ex)
            {
                string aa = ex.Message;
                //MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public static void PickStaker1()
        {
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.DispenserModule_PickupStacker1, CommandGenrator.ModuleType.DispenserModule));
                Config.Dispenser.Write(command, 0, command.Length);
                Config.Dispenser.DiscardOutBuffer();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        public static void PickStaker2()
        {
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.DispenserModule_PickupStacker2, CommandGenrator.ModuleType.DispenserModule));
                Config.Dispenser.Write(command, 0, command.Length);
                Config.Dispenser.DiscardOutBuffer();
            }
            catch (Exception)
            {



                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void ClearDispenserCardJam()
        {
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.DispenserModule_ClearDispenserCardJam, CommandGenrator.ModuleType.DispenserModule));
                Config.Dispenser.Write(command, 0, command.Length);
                Config.Dispenser.DiscardOutBuffer();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void SendDispenserSensorStatus()
        {
            OpenAllPortExceptLaser();
            if (!Config.Start || !Config.FlgCheckFiliper)
            {
                Config.Start = false;
                Config.FlgCheckFiliper = false;
            }
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.DispenserModule_SendSensorsStatusRequest, CommandGenrator.ModuleType.DispenserModule));
                Config.Dispenser.DiscardOutBuffer();
                Config.Dispenser.Write(command, 0, command.Length);
                Config.StutDispenserSensorVariable.flgGetDispenserSensorStatus = true;
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        public static void MoveToRejectBox()
        {
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.CardHolderModule_MoveToRejectBox, CommandGenrator.ModuleType.CardHolderModule));
                Config.CardHolder.Write(command, 0, command.Length);
                Config.CardHolder.DiscardOutBuffer();
                Config.flgTimeoutRejectBox = true;
            }
            catch (Exception)
            {


            }

        }
        public static void MoveToCr()
        {
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.CardHolderModule_MoveCardToCR, CommandGenrator.ModuleType.CardHolderModule));
                Config.CardHolder.Write(command, 0, command.Length);
                Config.CardHolder.DiscardOutBuffer();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void ReceiveCardForMarkingValue()
        {
        LppOOPP:
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.CardHolderModule_ReceiveCardForMarkingValue, CommandGenrator.ModuleType.CardHolderModule));
                Config.CardHolder.Write(command, 0, command.Length);
                Config.CardHolder.DiscardOutBuffer();
            }
            catch (Exception ex)
            {
                string aa = ex.Message;
                if (aa == "The port is closed.")
                {
                    OpenAllPortExceptLaser();
                    goto LppOOPP;
                }
                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        public static void RotateCard()
        {
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.CardHolderModule_RotateCard, CommandGenrator.ModuleType.CardHolderModule));
                Config.CardHolder.Write(command, 0, command.Length);
                Config.CardHolder.DiscardOutBuffer();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void RequestFlipperinstall()
        {
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.CardHolderModule_RequestFlipperinstall, CommandGenrator.ModuleType.CardHolderModule));
                Config.CardHolder.Write(command, 0, command.Length);
                Config.CardHolder.DiscardOutBuffer();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void ResetSendMachineCounter()
        {
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.CardHolderModule_ResetSendMachineCounter, CommandGenrator.ModuleType.CardHolderModule));
                Config.CardHolder.Write(command, 0, command.Length);
                Config.CardHolder.DiscardOutBuffer();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static double SendMachineCounter()
        {
            double ReturnCounter = 0;
            int counterCnt = 0;
            try
            {
                Config.getMachineCounter = true;
                Config.DeviceCounter = 0;
                Config.MachineCounterState = false;
                CommandGenrator test = new CommandGenrator();
                do
                {
                    byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.CardHolderModule_SendMachineCounter, CommandGenrator.ModuleType.CardHolderModule));
                    Config.CardHolder.Write(command, 0, command.Length);
                    Config.CardHolder.DiscardOutBuffer();
                }
                while (!MachineCounterState && counterCnt < 10);
                if (counterCnt > 10)
                    ReturnCounter = -1;
                else
                    ReturnCounter = (double)Config.DeviceCounter;
            }
            catch (Exception)
            {
                ReturnCounter = -1;
                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ReturnCounter;

        }
        public static void CardHolderModule_SendSerialNumber()
        {
        Loop:
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.CardHolderModule_SendSerialNumber, CommandGenrator.ModuleType.CardHolderModule));
                Config.CardHolder.Write(command, 0, command.Length);
                Config.CardHolder.DiscardOutBuffer();
            }
            catch (Exception ex)
            {
                string aa = ex.ToString();
                OpenAllPortExceptLaser();
                goto Loop;
                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void checkdevice()
        {
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.checkdevice, CommandGenrator.ModuleType.CardHolderModule));
                Config.CardHolder.Write(command, 0, command.Length);
                Config.CardHolder.DiscardOutBuffer();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void SendLocalSerialNumber()
        {
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.DispenserModule_SendLocalSerialNumber, CommandGenrator.ModuleType.DispenserModule));
                Config.CardHolder.Write(command, 0, command.Length);
                Config.CardHolder.DiscardOutBuffer();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void SendSensorsStatusRequest()
        {
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.DispenserModule_SendSensorsStatusRequest, CommandGenrator.ModuleType.DispenserModule));
                Config.Dispenser.Write(command, 0, command.Length);
                Config.Dispenser.DiscardOutBuffer();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void DispenserModule_SendSerialNumber()
        {
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.DispenserModule_SendSerialNumber, CommandGenrator.ModuleType.DispenserModule));
                Config.Dispenser.Write(command, 0, command.Length);
                Config.Dispenser.DiscardOutBuffer();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void ClearCardJam()
        {
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.CardHolderModule_ClearCardJam, CommandGenrator.ModuleType.CardHolderModule));
                Config.CardHolder.Write(command, 0, command.Length);
                Config.CardHolder.DiscardOutBuffer();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void ClearRejectBox()
        {
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.CardHolderModule_ClearRejectBox, CommandGenrator.ModuleType.CardHolderModule));
                Config.CardHolder.Write(command, 0, command.Length);
                Config.CardHolder.DiscardOutBuffer();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void Fan_On()
        {
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.CardHolderModule_FanOn, CommandGenrator.ModuleType.CardHolderModule));
                Config.CardHolder.Write(command, 0, command.Length);
                Config.CardHolder.DiscardOutBuffer();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void Fan_Off()
        {
            try
            {
                CommandGenrator test = new CommandGenrator();
                byte[] command = convertor.StringToByteArray(test.CommandGeneratorForModule(CommandGenrator.ModulesCommand.CardHolderModule_FanOff, CommandGenrator.ModuleType.CardHolderModule));
                Config.CardHolder.Write(command, 0, command.Length);
                Config.CardHolder.DiscardOutBuffer();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }




    }
}
