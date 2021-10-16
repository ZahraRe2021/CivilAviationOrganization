using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SaraPrinterLaser.bl;
namespace SaraPrinterLaser.Hardware
{
    public class Worker
    {
        private static Object thisLock = new Object();
        public static bool Active = true;
        public static job myjob;
        public static bool newjob;
        public static bool flgTakeCardDone = false;
        public static string[] Error = new string[0];
        public static string[] crTrack = new string[3];
        public static bool NoCardExist = false;
        public static bool NoCardAndMagazineExist = false;
        public static byte TryToExchange = 20;
        public static bool flgCardjam { get; set; }
        public static bool flgDispenserJam { get; set; }
        public static bool flgCardHolderJam { get; set; }
        public static bool flgInCR { get; set; }
        public Thread main = new Thread(new ThreadStart(Start));
        public static void Start()
        {
            #region connect
            Config.OpenAllPortExceptLaser();

            #endregion connect
            while (Active)
            {
                if (FileWork.Answered)
                {
                    #region answer
                    //      FileWork.Answered = false;
                    for (int i = FileWork.answer.Length - 1; i >= 0; i--)
                    {
                        try
                        {
                            if (!FileWork.answer[i].Contains("jobDone"))
                            {
                                CommandGenrator CMDG = new CommandGenrator();
                                string temp = CMDG.StatusGenratorFromModule(FileWork.answer[i]).ToString();
                                FileWork.answer[i] = "jobDone" + FileWork.answer[i];
                                switch (temp)
                                {
                                    case "StatusCode_DeviceHaveFlipper"://OK
                                        {
                                            //Do nothing
                                        }
                                        break;
                                    case "SerialNumber_Ok"://OK
                                        {
                                            Worker.Active = false;
                                        }
                                        break;
                                    case " StatusCode_TrueReceive"://OK

                                        break;
                                    case "StatusCode_Ok"://OK
                                        Status_OK();
                                        break;

                                    case "WarningCode_CardCapacityWarning"://OK
                                        {
                                            NoCardExist = false;
                                            flgDispenserJam = false;
                                            flgCardHolderJam = false;
                                            NoCardAndMagazineExist = false;
                                            Worker.myjob.InputDataIncurrectCount = 0;
                                            if (Worker.myjob.WorkList.Length > Worker.myjob.done.Length)
                                            {
                                                string[] temp2 = Worker.myjob.done;
                                                Array.Resize(ref temp2, Worker.myjob.done.Length + 1);
                                                temp2[temp2.Length - 1] = "done";
                                                Worker.myjob.done = temp2;
                                                switch (FileWork.stateMashin)
                                                {
                                                    case FileWork.StateOfmashin.pickupStacker_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.pickupStacker_End);
                                                        break;
                                                    case FileWork.StateOfmashin.pickupStacker_End:
                                                        FileWork.changeState(FileWork.StateOfmashin.reciveForMarking_Start);
                                                        break;
                                                    case FileWork.StateOfmashin.Rotate_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.Rotate_End);
                                                        break;
                                                    case FileWork.StateOfmashin.MoveToRejectBox_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.MoveToRejectBox_End);
                                                        break;
                                                    case FileWork.StateOfmashin.MoveToRejectBoxFromCR_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.MoveToRejectBoxFromCR_End);
                                                        break;
                                                    case FileWork.StateOfmashin.MoveCr_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.MoveCr_End);
                                                        break;
                                                    case FileWork.StateOfmashin.MarkingAreaJam_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.MarkingAreaJam_End);
                                                        break;
                                                    case FileWork.StateOfmashin.InCR_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.InCR_End);
                                                        break;
                                                    case FileWork.StateOfmashin.DispenserJam_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.DispenserJam_End);
                                                        break;
                                                    case FileWork.StateOfmashin.CrJam_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.CrJam_End);
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] = "JobWarning_" + myjob.LatestJobDone[myjob.LatestJobDone.Length - 1];
                                                newjob = true;

                                            }
                                            else
                                            {
                                                FileWork.changeState(FileWork.StateOfmashin.Printed);
                                                Worker.myjob.Status = job.StatusList.printed;
                                                Worker.Active = false;
                                            }
                                            Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                            Worker.Error[Worker.Error.Length - 1] = "مخزن در حال خالی شدن است";
                                        }
                                        break;
                                    case "ErrorCode_InputDataIncorrect"://OK
                                        {
                                            if (!NoCardAndMagazineExist)
                                            {
                                                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                                Worker.Error[Worker.Error.Length - 1] = "اطلاعات در یافتی اشتباه است";
                                                if (Worker.myjob.InputDataIncurrectCount < TryToExchange)
                                                {
                                                    switch (myjob.LatestJobDone[myjob.LatestJobDone.Length - 1])
                                                    {
                                                        case "Job_stacker1":
                                                            Rotin_stacker1();
                                                            break;
                                                        case "Job_stacker2":
                                                            Rotin_stacker2();
                                                            break;
                                                        case "Job_TakeCard":
                                                            Rotin_TakeCard();
                                                            break;
                                                        case "Job_MoveToRejectBoxFromCR":
                                                            Rotin_MoveToRejectBoxFromCR();
                                                            break;
                                                        case "Job_ClearRejectBox":
                                                            Rotin_ClearRejectBox();
                                                            break;
                                                        case "Job_ClearDispenserCardJam":
                                                            Rotin_ClearDispenserCardJam();
                                                            break;
                                                        case "Job_Rotate":
                                                            Rotin_Rotate();
                                                            break;
                                                        case "Job_InCr":
                                                            Rotin_InCr();
                                                            break;
                                                        case "Job_MoveToRejectBox":
                                                            Rotin_MoveToRejectBox();
                                                            break;

                                                        default:

                                                            break;
                                                    }
                                                    Worker.myjob.InputDataIncurrectCount++;
                                                    newjob = true;
                                                }
                                                else
                                                {
                                                    Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                                    Worker.Error[Worker.Error.Length - 1] = "دستگاه از دسترس خارج شده است";
                                                    Worker.myjob.Status = job.StatusList.ErrorHardWere;
                                                    Worker.Active = false;
                                                }
                                            }
                                        }
                                        break;
                                    case "ErrorCode_WithoutFlipper"://OK
                                        {
                                            Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                            Worker.Error[Worker.Error.Length - 1] = "دستگاه به سیستم دو رو زن مجهز نیست .";
                                            Worker.Active = false;
                                        }
                                        break;
                                    case "ErrorCode_FailDataReceive"://OK
                                        {
                                            Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                            Worker.Error[Worker.Error.Length - 1] = "اطلاعات دریافتی اشتباه است";
                                            if (Worker.myjob.InputDataIncurrectCount < TryToExchange)
                                            {
                                                switch (myjob.LatestJobDone[myjob.LatestJobDone.Length - 1])
                                                {
                                                    case "Job_stacker1":
                                                        Rotin_stacker1();
                                                        break;
                                                    case "Job_stacker2":
                                                        Rotin_stacker2();
                                                        break;
                                                    case "Job_TakeCard":
                                                        Rotin_TakeCard();
                                                        break;
                                                    case "Job_MoveToRejectBoxFromCR":
                                                        Rotin_MoveToRejectBoxFromCR();
                                                        break;
                                                    case "Job_ClearRejectBox":
                                                        Rotin_ClearRejectBox();
                                                        break;
                                                    case "Job_ClearDispenserCardJam":
                                                        Rotin_ClearDispenserCardJam();
                                                        break;
                                                    case "Job_Rotate":
                                                        Rotin_Rotate();
                                                        break;
                                                    case "Job_InCr":
                                                        Rotin_InCr();
                                                        break;
                                                    case "Job_MoveToRejectBox":
                                                        Rotin_MoveToRejectBox();
                                                        break;

                                                    default:

                                                        break;
                                                }
                                                Worker.myjob.InputDataIncurrectCount++;
                                                newjob = true;

                                            }
                                            else
                                            {
                                                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                                Worker.Error[Worker.Error.Length - 1] = "دستگاه از دسترس خارج شده است";
                                                Worker.myjob.Status = job.StatusList.ErrorHardWere;
                                                Worker.Active = false;


                                            }
                                        }
                                        break;
                                    case "ErrorCode_DeviceBusy"://OK
                                        {
                                            Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                            Worker.Error[Worker.Error.Length - 1] = "دستگاه در حال انجام ";
                                            if (Worker.myjob.InputDataIncurrectCount < TryToExchange)
                                            {
                                                switch (myjob.LatestJobDone[myjob.LatestJobDone.Length - 1])
                                                {
                                                    case "Job_stacker1":
                                                        Rotin_stacker1();
                                                        break;
                                                    case "Job_stacker2":
                                                        Rotin_stacker2();
                                                        break;
                                                    case "Job_TakeCard":
                                                        Rotin_TakeCard();
                                                        break;
                                                    case "Job_MoveToRejectBoxFromCR":
                                                        Rotin_MoveToRejectBoxFromCR();
                                                        break;
                                                    case "Job_ClearRejectBox":
                                                        Rotin_ClearRejectBox();
                                                        break;
                                                    case "Job_ClearDispenserCardJam":
                                                        Rotin_ClearDispenserCardJam();
                                                        break;
                                                    case "Job_Rotate":
                                                        Rotin_Rotate();
                                                        break;
                                                    case "Job_InCr":
                                                        Rotin_InCr();
                                                        break;
                                                    case "Job_MoveToRejectBox":
                                                        Rotin_MoveToRejectBox();
                                                        break;

                                                    default:

                                                        break;
                                                }
                                                Worker.myjob.InputDataIncurrectCount++;
                                                newjob = true;

                                            }
                                            else
                                            {
                                                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                                Worker.Error[Worker.Error.Length - 1] = " دستگاه از دسترس خارج شده است";
                                                Worker.myjob.Status = job.StatusList.ErrorHardWere;
                                                Worker.Active = false;
                                            }
                                        }
                                        break;
                                    case "ErrorCode_undefined"://OK
                                        {
                                            Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                            Worker.Error[Worker.Error.Length - 1] = "ErrorCode_undefined";
                                            if (Worker.myjob.InputDataIncurrectCount < TryToExchange)
                                            {
                                                switch (myjob.LatestJobDone[myjob.LatestJobDone.Length - 1])
                                                {
                                                    case "Job_stacker1":
                                                        Rotin_stacker1();
                                                        break;
                                                    case "Job_stacker2":
                                                        Rotin_stacker2();
                                                        break;
                                                    case "Job_TakeCard":
                                                        Rotin_TakeCard();
                                                        break;
                                                    case "Job_MoveToRejectBoxFromCR":
                                                        Rotin_MoveToRejectBoxFromCR();
                                                        break;
                                                    case "Job_ClearRejectBox":
                                                        Rotin_ClearRejectBox();
                                                        break;
                                                    case "Job_ClearDispenserCardJam":
                                                        Rotin_ClearDispenserCardJam();
                                                        break;
                                                    case "Job_Rotate":
                                                        Rotin_Rotate();
                                                        break;
                                                    case "Job_InCr":
                                                        Rotin_InCr();
                                                        break;
                                                    case "Job_MoveToRejectBox":
                                                        Rotin_MoveToRejectBox();
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                Worker.myjob.InputDataIncurrectCount++;
                                                newjob = true;

                                            }
                                            else
                                            {
                                                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                                Worker.Error[Worker.Error.Length - 1] = "دستگاه از دسترس خارج شده است";
                                                Worker.myjob.Status = job.StatusList.ErrorHardWere;
                                                Worker.Active = false;


                                            }
                                        }
                                        break;
                                    case "ErrorCode_CardJam":
                                        {
                                            Worker.flgCardjam = true;
                                            string DeviceAddress = FileWork.answer[i].Substring(12, 1);
                                            if (DeviceAddress == "a")
                                            {
                                                flgDispenserJam = true;
                                                if (Worker.myjob.InputDataIncurrectCount < TryToExchange)
                                                {
                                                    Rotin_TakeCard();
                                                    newjob = false;
                                                }
                                                else
                                                {
                                                    Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                                    Worker.Error[Worker.Error.Length - 1] = "لطفا خط شخصی سازی دستگاه را بررسی نمایید";
                                                    Worker.myjob.Status = job.StatusList.ErrorHardWere;
                                                    Worker.Active = false;
                                                    newjob = false;
                                                }
                                                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                                Worker.Error[Worker.Error.Length - 1] = "ErrorCode_CardJam_Dispenser";
                                            }
                                            else if (DeviceAddress == "5")
                                            {
                                                flgCardHolderJam = true;
                                                FileWork.Answered = false;
                                                if (flgDispenserJam)
                                                {
                                                    Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                                    Worker.Error[Worker.Error.Length - 1] = "لطفا خط شخصی سازی دستگاه را بررسی نمایید";
                                                    Worker.myjob.Status = job.StatusList.ErrorHardWere;
                                                    Worker.Active = false;
                                                    newjob = false;
                                                    flgCardHolderJam = false;
                                                }
                                                else
                                                {
                                                    if (!flgInCR)
                                                    {
                                                        if (Config.RotateState)
                                                        {
                                                            bool CheckCard = false;
                                                            string ResponseCR = "";
                                                            int GetSensorCounter = 0;
                                                            CR.ReturnDeviceStatus GetSensorStatus = new CR.ReturnDeviceStatus();
                                                            do
                                                            {
                                                                Config.ClearCardJam();
                                                                GetSensorStatus = CR.getBackSensorStatus(ref ResponseCR, ref CheckCard);
                                                                if (GetSensorStatus != CR.ReturnDeviceStatus.MB_OK) break;
                                                                Thread.Sleep(10);
                                                            } while (!CheckCard && GetSensorCounter < 100);
                                                            if (GetSensorCounter <= 100)
                                                            {
                                                                if (CheckCard)
                                                                {
                                                                    GetSensorCounter = 0;
                                                                    GetSensorStatus = CR.Initialize(CR.Inittype.MoveTogate, ref ResponseCR);

                                                                    CheckCard = false;
                                                                    GetSensorCounter = 0;
                                                                    do
                                                                    {
                                                                        GetSensorStatus = CR.getGateSensorStatus(ref ResponseCR, ref CheckCard);
                                                                        if (GetSensorStatus != CR.ReturnDeviceStatus.MB_OK) break;
                                                                        Thread.Sleep(10);
                                                                    } while (!CheckCard && GetSensorCounter < 100);

                                                                    if (GetSensorCounter <= 100 && GetSensorStatus == CR.ReturnDeviceStatus.MB_OK)
                                                                    {
                                                                        if (CheckCard)
                                                                        {
                                                                            FileWork.changeState(FileWork.StateOfmashin.Printed);
                                                                            Worker.myjob.Status = job.StatusList.printed;
                                                                            Worker.Active = false;
                                                                            newjob = false;
                                                                            flgCardHolderJam = false;
                                                                        }
                                                                        else
                                                                        {
                                                                            Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                                                            Worker.Error[Worker.Error.Length - 1] = "لطفا خط شخصی سازی دستگاه را بررسی نمایید";
                                                                            Worker.myjob.Status = job.StatusList.ErrorHardWere;
                                                                            Worker.Active = false;
                                                                            newjob = false;
                                                                            flgCardHolderJam = false;
                                                                        }
                                                                    }



                                                                }
                                                            }
                                                            else
                                                            {
                                                                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                                                Worker.Error[Worker.Error.Length - 1] = "لطفا خط شخصی سازی دستگاه را بررسی نمایید";
                                                                Worker.myjob.Status = job.StatusList.ErrorHardWere;
                                                                Worker.Active = false;
                                                                newjob = false;
                                                                flgCardHolderJam = false;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            string Response = "";
                                                            bool CheckInternalSensor = false;
                                                            int Counter = 0;

                                                            Retry:
                                                            try
                                                            {
                                                                Array.Resize(ref myjob.LatestJobDone, myjob.LatestJobDone.Length + 1);
                                                                myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] = "Job_InCr";
                                                            }
                                                            catch (Exception)
                                                            {
                                                                myjob.LatestJobDone = new string[0] { };
                                                                goto Retry;
                                                            }
                                                            bl.FileWork.changeState(FileWork.StateOfmashin.MoveCr_Start);
                                                            Config.ClearCardJam();
                                                            CR.ReturnDeviceStatus PermitBehind = CR.PermitBehind(ref Response);
                                                            RetryGetSensor:
                                                            if (CR.getInternalSensorStatus(ref Response, ref CheckInternalSensor) == CR.ReturnDeviceStatus.MB_OK)
                                                            {
                                                                if (CheckInternalSensor)
                                                                {
                                                                    newjob = false;
                                                                }
                                                                else
                                                                    if (Counter < 100)
                                                                    goto RetryGetSensor;
                                                            }
                                                            else
                                                            if (Counter < 100)
                                                                goto RetryGetSensor;
                                                            if (Counter >= 100)
                                                            {
                                                                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                                                Worker.Error[Worker.Error.Length - 1] = Response + "لطفا خط شخصی سازی را بررسی نمایید.";
                                                            }
                                                        }
                                                        Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                                        Worker.Error[Worker.Error.Length - 1] = "ErrorCode_CardJam_CardHolder";
                                                    }

                                                }



                                                //FileWork.changeState(FileWork.StateOfmashin.MarkingAreaJam_Start);
                                                //Worker.myjob.Status = job.StatusList.CardJam;
                                                flgCardHolderJam = false;
                                                FileWork.Answered = true;
                                            }
                                            else
                                            {
                                                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                                Worker.Error[Worker.Error.Length - 1] = "ErrorCode_CardJam_Unknow";
                                                FileWork.changeState(FileWork.StateOfmashin.InCR_Start);
                                                Worker.myjob.Status = job.StatusList.CardJam;

                                            }

                                        }
                                        break;
                                    case "StatusCode_TrueReject"://OK
                                        {
                                            NoCardExist = false;
                                            flgDispenserJam = false;
                                            flgCardHolderJam = false;
                                            NoCardAndMagazineExist = false;
                                            Worker.myjob.InputDataIncurrectCount = 0;
                                            if (Worker.myjob.WorkList.Length != Worker.myjob.done.Length + 1)
                                            {
                                                string[] temp2 = Worker.myjob.done;
                                                Array.Resize(ref temp2, Worker.myjob.done.Length + 1);
                                                temp2[temp2.Length - 1] = "done";
                                                Worker.myjob.done = temp2;
                                                switch (FileWork.stateMashin)
                                                {
                                                    case FileWork.StateOfmashin.pickupStacker_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.pickupStacker_End);
                                                        break;
                                                    case FileWork.StateOfmashin.pickupStacker_End:
                                                        FileWork.changeState(FileWork.StateOfmashin.reciveForMarking_Start);
                                                        break;
                                                    case FileWork.StateOfmashin.Rotate_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.Rotate_End);
                                                        break;
                                                    case FileWork.StateOfmashin.MoveToRejectBox_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.MoveToRejectBox_End);
                                                        break;
                                                    case FileWork.StateOfmashin.MoveToRejectBoxFromCR_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.MoveToRejectBoxFromCR_End);
                                                        break;
                                                    case FileWork.StateOfmashin.MoveCr_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.MoveCr_End);
                                                        break;
                                                    case FileWork.StateOfmashin.MarkingAreaJam_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.MarkingAreaJam_End);
                                                        break;
                                                    case FileWork.StateOfmashin.InCR_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.InCR_End);
                                                        break;
                                                    case FileWork.StateOfmashin.DispenserJam_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.DispenserJam_End);
                                                        break;
                                                    case FileWork.StateOfmashin.CrJam_Start:
                                                        FileWork.changeState(FileWork.StateOfmashin.CrJam_End);
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                Worker.myjob.Status = job.StatusList.JamCleared;
                                                FileWork.changeState(FileWork.StateOfmashin.Printed);
                                                Worker.Active = false;
                                            }
                                            else
                                            {

                                                FileWork.changeState(FileWork.StateOfmashin.Printed);
                                                Worker.myjob.Status = job.StatusList.printed;
                                                Worker.Active = false;
                                            }
                                            //Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                            //Worker.Error[Worker.Error.Length - 1] = "StatusCode_TrueReject";
                                        }
                                        break;
                                    case "ErrorCode_NoStackerDetect"://OK
                                        {
                                            NoCardAndMagazineExist = true;
                                            if (!NoCardExist)
                                            {
                                                NoCardExist = true;
                                                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                                Worker.Error[Worker.Error.Length - 1] = "مخزن کارت وجود ندارد.";
                                                Worker.myjob.Status = job.StatusList.Emtpy;
                                            }
                                            newjob = true;
                                            //Worker.Active = false;
                                        }
                                        break;
                                    case "ErrorCode_CardEmpty"://OK
                                        {
                                            NoCardAndMagazineExist = true;
                                            if (!NoCardExist)
                                            {
                                                NoCardExist = true;
                                                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                                Worker.Error[Worker.Error.Length - 1] = "مخزن مورد نظر کارت ندارد.";
                                                Worker.myjob.Status = job.StatusList.Emtpy;
                                            }
                                            newjob = true;
                                        }
                                        break;
                                    case "ErrorCode_Fail"://OK
                                        {
                                            Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                            Worker.Error[Worker.Error.Length - 1] = "ErrorCode_Fail";
                                            if (Worker.myjob.InputDataIncurrectCount < TryToExchange)
                                            {
                                                switch (myjob.LatestJobDone[myjob.LatestJobDone.Length - 1])
                                                {
                                                    case "Job_stacker1":
                                                        Rotin_stacker1();
                                                        break;
                                                    case "Job_stacker2":
                                                        Rotin_stacker2();
                                                        break;
                                                    case "Job_TakeCard":
                                                        Rotin_TakeCard();
                                                        break;
                                                    case "Job_MoveToRejectBoxFromCR":
                                                        Rotin_MoveToRejectBoxFromCR();
                                                        break;
                                                    case "Job_ClearRejectBox":
                                                        Rotin_ClearRejectBox();
                                                        break;
                                                    case "Job_ClearDispenserCardJam":
                                                        Rotin_ClearDispenserCardJam();
                                                        break;
                                                    case "Job_Rotate":
                                                        Rotin_Rotate();
                                                        break;
                                                    case "Job_InCr":
                                                        Rotin_InCr();
                                                        break;
                                                    case "Job_MoveToRejectBox":
                                                        Rotin_MoveToRejectBox();
                                                        break;

                                                    default:

                                                        break;
                                                }
                                                Worker.myjob.InputDataIncurrectCount++;
                                                newjob = true;

                                            }
                                            else
                                            {
                                                Worker.myjob.Status = job.StatusList.ErrorHardWere;
                                                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                                Worker.Error[Worker.Error.Length - 1] = "دستگاه از دسترس خارج شده است";
                                                Worker.Active = false;


                                            }
                                        }
                                        break;
                                    case "ErrorCode_Timeout":
                                        {

                                            if (FileWork.stateMashin == FileWork.StateOfmashin.pickupStacker_Start ||
                                               FileWork.stateMashin == FileWork.StateOfmashin.pickupStacker_End ||
                                               FileWork.stateMashin == FileWork.StateOfmashin.DispenserJam_Start ||
                                               FileWork.stateMashin == FileWork.StateOfmashin.DispenserJam_End ||
                                               FileWork.stateMashin == FileWork.StateOfmashin.reciveForMarking_Start ||
                                               FileWork.stateMashin == FileWork.StateOfmashin.MoveToRejectBox_Start)
                                            {
                                                switch (myjob.LatestJobDone[myjob.LatestJobDone.Length - 1])
                                                {
                                                    case "Job_stacker1":
                                                        Status_OK();
                                                        break;
                                                    case "Job_stacker2":
                                                        Status_OK();
                                                        break;
                                                    case "Job_TakeCard":
                                                        Status_OK();
                                                        break;
                                                    case "Job_MoveToRejectBoxFromCR":
                                                        Status_OK();
                                                        break;
                                                    case "Job_ClearRejectBox":
                                                        Status_OK();
                                                        break;
                                                    case "Job_ClearDispenserCardJam":
                                                        Status_OK();
                                                        break;
                                                    case "Job_Rotate":
                                                        Status_OK();
                                                        break;
                                                    case "Job_InCr":
                                                        Status_OK();
                                                        break;
                                                    case "Job_MoveToRejectBox":
                                                        Status_OK();
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }
                                            //    if (!UseTimeout)
                                            //    {
                                            //        bool RFIDCheck = false;
                                            //        foreach (string item in myjob.LatestJobDone)
                                            //            if (item == "Job_CheckRFID") RFIDCheck = true;
                                            //        if (RFIDCheck)
                                            //        {
                                            //            Thread.Sleep(100);
                                            //            bl.FileWork.changeState(FileWork.StateOfmashin.Ready);
                                            //            Worker.myjob.Status = job.StatusList.printed;
                                            //            newjob = false;
                                            //            Worker.Active = false;
                                            //        }
                                            //        else
                                            //        {

                                            //            int GetSStatus = 0;
                                            //            Config.SendDispenserSensorStatus();
                                            //            while (!Config.StutDispenserSensorVariable.flgGetDispenserSensorStatus && GetSStatus < 100) { if (++GetSStatus > 100) break; Thread.Sleep(10); }
                                            //            if (Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusSucsess)
                                            //            {
                                            //                if (!Config.StutDispenserSensorVariable.flgDispenserSensor_EndWayCardDetector && !Worker.flgCardjam)
                                            //                {
                                            //                    Worker.myjob.InputDataIncurrectCount = 0;
                                            //                    if (Worker.myjob.WorkList.Length > Worker.myjob.done.Length)
                                            //                    {
                                            //                        string[] temp2 = Worker.myjob.done;
                                            //                        Array.Resize(ref temp2, Worker.myjob.done.Length + 1);
                                            //                        temp2[temp2.Length - 1] = "done";
                                            //                        Worker.myjob.done = temp2;
                                            //                        switch (FileWork.stateMashin)
                                            //                        {
                                            //                            case FileWork.StateOfmashin.pickupStacker_Start:
                                            //                                FileWork.changeState(FileWork.StateOfmashin.pickupStacker_End);
                                            //                                break;
                                            //                            case FileWork.StateOfmashin.pickupStacker_End:
                                            //                                FileWork.changeState(FileWork.StateOfmashin.reciveForMarking_Start);
                                            //                                break;
                                            //                            case FileWork.StateOfmashin.Rotate_Start:
                                            //                                FileWork.changeState(FileWork.StateOfmashin.Rotate_End);
                                            //                                break;
                                            //                            default:
                                            //                                break;
                                            //                        }
                                            //                        newjob = true;
                                            //                    }
                                            //                }
                                            //                else if (Config.StutDispenserSensorVariable.flgDispenserSensor_EndWayCardDetector)
                                            //                {
                                            //                    Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                            //                    Worker.Error[Worker.Error.Length - 1] = "ErrorCode_Timeout";
                                            //                    if (Worker.myjob.InputDataIncurrectCount < TryToExchange)
                                            //                    {
                                            //                        Worker.myjob.InputDataIncurrectCount++;
                                            //                        Worker.myjob.Status = job.StatusList.ErrorHardWere;
                                            //                        newjob = true;
                                            //                    }
                                            //                    else
                                            //                    {
                                            //                        Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                                            //                        Worker.Error[Worker.Error.Length - 1] = "دستگاه از دسترس خارج شده است";
                                            //                        Worker.Active = false;
                                            //                    }
                                            //                }
                                            //            }
                                            //        }
                                            //    }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }

                        }
                        catch (Exception)
                        {
                            //         System.Windows.Forms.MessageBox.Show("ErrorCode5000", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                    }
                    #endregion answer
                }
                #region job
                if (newjob)
                {
                    // CR.Initialize(CR.Inittype.KeepInside);
                    if (Worker.myjob.WorkList.Length > Worker.myjob.done.Length)
                    {
                        switch (Worker.myjob.WorkList[Worker.myjob.done.Length])
                        {
                            //Dispenser Job
                            case "stacker1":
                                Rotin_stacker1();
                                break;
                            case "stacker2":
                                Rotin_stacker2();
                                break;
                            case "ClearDispenserCardJam":
                                Rotin_ClearDispenserCardJam();
                                break;

                            //CardHolder Job
                            case "Fan_On":
                                Rotin_Fan_On();
                                break;
                            case "Fan_Off":
                                Rotin_Fan_Off();
                                break;
                            case "TakeCard":
                                Rotin_TakeCard();
                                break;
                            case "MoveToRejectBoxFromCR":
                                Rotin_MoveToRejectBoxFromCR();
                                break;
                            case "ClearRejectBox":
                                Rotin_ClearRejectBox();
                                break;
                            case "Rotate":
                                Rotin_Rotate();
                                break;
                            case "MoveToRejectBox":
                                Rotin_MoveToRejectBox();
                                break;
                            case "ClearCardJam":
                                Rotin_ClearCardJam();
                                break;

                            //RFID
                            case "RFIDtest":
                                Rotin_RFIDtest();
                                break;

                            //Laser
                            case "LaserRo":
                                Rotin_LaserRo();
                                break;
                            case "laserZir":
                                Rotin_laserZir();
                                break;

                            //CRModule
                            case "InCr":
                                Rotin_InCr();
                                break;
                            case "InCrKeepInside":
                                Rotin_InCrKeepInside();
                                break;
                            case "crIntMoveToGate":
                                Rotin_crIntMoveToGate();
                                break;
                            case "CR_Init_Nomove":
                                Rotin_CR_Init_Nomove();
                                break;
                            case "CR_Init_MoveToGate":
                                Rotin_CR_Init_MoveToGate();
                                break;
                            case "WriteCr":
                                Rotin_WriteCr();
                                break;
                            case "ReadCr":
                                Rotin_ReadCr();
                                break;
                            case "out":
                                Rotin_out();
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Worker.Active = false;
                    }
                }
                #endregion job
            }
            #region disconnect
            Config.CloseAllPortExceptLaser();
            #endregion disconnect
        }
        #region Functions
        public static void finishwork()
        {
            string[] temp = Worker.myjob.done;
            Array.Resize(ref temp, Worker.myjob.done.Length + 1);
            temp[temp.Length - 1] = "done";
            Worker.myjob.done = temp;
        }
        private static void Rotin_Fan_On()//OK
        {
            Retry:
            try
            {
                Array.Resize(ref myjob.LatestJobDone, myjob.LatestJobDone.Length + 1);
                myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] = "Job_Fan_On";
            }
            catch (Exception)
            {
                myjob.LatestJobDone = new string[0] { };
                goto Retry;
            }

            Config.Fan_On();
            newjob = false;
        }
        private static void Rotin_Fan_Off()//OK
        {
            Retry:
            try
            {
                Array.Resize(ref myjob.LatestJobDone, myjob.LatestJobDone.Length + 1);
                myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] = "Job_Fan_Off";
            }
            catch (Exception)
            {
                myjob.LatestJobDone = new string[0] { };
                goto Retry;
            }

            Config.Fan_Off();
            newjob = false;
        }
        private static void Rotin_stacker1()//OK
        {
            Retry:
            try
            {
                Array.Resize(ref myjob.LatestJobDone, myjob.LatestJobDone.Length + 1);
                myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] = "Job_stacker1";
            }
            catch (Exception)
            {
                myjob.LatestJobDone = new string[0] { };
                goto Retry;
            }

            bl.FileWork.changeState(FileWork.StateOfmashin.pickupStacker_Start);
            Config.PickStaker1();

            newjob = false;
        }
        private static void Rotin_stacker2()//OK
        {
            Retry:
            try
            {
                Array.Resize(ref myjob.LatestJobDone, myjob.LatestJobDone.Length + 1);
                myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] = "Job_stacker2";
            }
            catch (Exception)
            {
                myjob.LatestJobDone = new string[0] { };
                goto Retry;
            }

            bl.FileWork.changeState(FileWork.StateOfmashin.pickupStacker_Start);
            Config.PickStaker2();

            newjob = false;
        }
        private static void Rotin_TakeCard()//OK
        {
            Retry:
            try
            {
                Array.Resize(ref myjob.LatestJobDone, myjob.LatestJobDone.Length + 1);
                myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] = "Job_TakeCard";
            }
            catch (Exception)
            {
                myjob.LatestJobDone = new string[0] { };
                goto Retry;
            }
            bl.FileWork.changeState(FileWork.StateOfmashin.reciveForMarking_Start);
            Config.ReceiveCardForMarkingValue();
            newjob = false;
        }
        private static void Rotin_RFIDtest()
        {
            if (myjob.checkRFid && Config.RFIDState)
            {

                nfc TesterRFID = new nfc();
                if (TesterRFID.RequestATR() == null)
                {
                    Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                    Worker.Error[Worker.Error.Length - 1] = "RFID Faild";

                    //MBA_NewJob

                    FileWork.Answered = false;
                    Worker.newjob = true;
                    Worker.myjob = new job(job.jobModel.NoRFIDCard);
                    Worker.Active = true;
                    Worker.myjob.LatestJobDone = new string[0] { };
                    Worker.myjob.done = new string[0] { };
                    Worker.Error = new string[0];
                    Worker.myjob.PenRo = null;
                    Worker.myjob.PenZir = null;
                    Worker.myjob.Status = job.StatusList.startPrint;
                    Worker Work = new Worker();
                    Work.main.Start();
                    Retry:
                    try
                    {
                        Array.Resize(ref myjob.LatestJobDone, myjob.LatestJobDone.Length + 1);
                        myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] = "Job_CheckRFID";
                    }
                    catch (Exception)
                    {
                        myjob.LatestJobDone = new string[0] { };
                        goto Retry;
                    }


                    //    Thread.Sleep(100);
                    //    bl.FileWork.changeState(FileWork.StateOfmashin.Ready);
                    //    Config.MoveToRejectBox();
                    //    Thread.Sleep(2000);
                    //    Worker.myjob.Status = job.StatusList.printed;
                    //    newjob = false;
                    //    Worker.Active = false;
                }
                else
                    finishwork();
            }
            else
                finishwork();
        }
        /// <summary>
        /// Move Fast to to reject box
        /// این تابع با انجام دو کار پشت سر هم با عکس می شود که کارت به ریجکت باکس منتقل شود.
        /// </summary>
        private static void Rotin_MoveToRejectBoxFromCR()
        {
            CR.ReturnDeviceStatus CardTojekectBox = new CR.ReturnDeviceStatus();
            int MoveToRejectBoxCounter = 0;
            string strDeviceResponse = "";

            Retry:
            try
            {
                Array.Resize(ref myjob.LatestJobDone, myjob.LatestJobDone.Length + 1);
                myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] = "Job_MoveToRejectBoxFromCR";
            }
            catch (Exception)
            {
                myjob.LatestJobDone = new string[0] { };
                goto Retry;
            }

            Config.MoveToRejectBox();
            System.Threading.Thread.Sleep(2000);

            do
            {
                CardTojekectBox = CR.CardToRejectBox(ref strDeviceResponse);
                MoveToRejectBoxCounter++;
                Thread.Sleep(10);
            } while (CardTojekectBox != CR.ReturnDeviceStatus.MB_OK && MoveToRejectBoxCounter < 100);

            if (CardTojekectBox != CR.ReturnDeviceStatus.MB_OK)
            {
                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                Worker.Error[Worker.Error.Length - 1] = strDeviceResponse + "کارت به زباله دان انتقال پیدا نکرد";
            }
            newjob = false;
            bl.FileWork.changeState(FileWork.StateOfmashin.Ready);
        }
        private static void Rotin_ClearRejectBox()
        {
            CR.ReturnDeviceStatus ClearRejectBox = new CR.ReturnDeviceStatus();
            string strDeviceResponse = "";
            int ClearRejectBoxCounter = 0;

            Retry:
            try
            {
                Array.Resize(ref myjob.LatestJobDone, myjob.LatestJobDone.Length + 1);
                myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] = "Job_ClearRejectBox";
            }
            catch (Exception)
            {
                myjob.LatestJobDone = new string[0] { };
                goto Retry;
            }

            Config.ClearRejectBox();
            Thread.Sleep(2000);

            do
            {
                ClearRejectBox = CR.CardToRejectBox(ref strDeviceResponse);
            } while (ClearRejectBox != CR.ReturnDeviceStatus.MB_OK && ClearRejectBoxCounter < 100);


            if (ClearRejectBox != CR.ReturnDeviceStatus.MB_OK)
            {
                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                Worker.Error[Worker.Error.Length - 1] = strDeviceResponse;
            }
            newjob = false;
        }
        private static void Rotin_LaserRo()
        {
            Laser.ClearLibAllEntity();
            if (flgTakeCardDone)
            {
                Laser.SetPenParam(Worker.myjob.PenRo.nPenNo, Worker.myjob.PenRo.nMarkLoop, Worker.myjob.PenRo.dMarkSpeed, Worker.myjob.PenRo.dPowerRatio, Worker.myjob.PenRo.dCurrent, Worker.myjob.PenRo.nFreq, Worker.myjob.PenRo.dQPulseWidth, Worker.myjob.PenRo.nStartTC, Worker.myjob.PenRo.nLaserOffTC, Worker.myjob.PenRo.nEndTC, Worker.myjob.PenRo.nPolyTC, Worker.myjob.PenRo.dJumpSpeed, Worker.myjob.PenRo.nJumpPosTC, Worker.myjob.PenRo.nJumpDistTC, Worker.myjob.PenRo.dEndComp, Worker.myjob.PenRo.dAccDist, Worker.myjob.PenRo.dPointTime, Worker.myjob.PenRo.bPulsePointMode, Worker.myjob.PenRo.nPulseNum, Worker.myjob.PenRo.dFlySpeed);
                //Laser.LmcErrCode ErrorStatus = Laser.AddFileToLib("Image/PictureUp.bmp", "LaserBitmapro", -42.460, -27.381, 0, 0, 1, 0, true);
                Laser.LmcErrCode ErrorStatus = Laser.AddFileToLib(@"Image\PictureTop.bmp", "LaserBitmapro", Hardware.Laser.myconfig.Xcenter, Hardware.Laser.myconfig.Ycenter, 0, 8, 1, myjob.PenRo.nPenNo, false);
                if (ErrorStatus == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                {

                    Laser.SetBitmapEntParam("LaserBitmapro", @"Image\PictureTop.bmp", Hardware.Laser.myconfig.bmpttrib, Hardware.Laser.myconfig.bmpScanAttr, Hardware.Laser.myconfig.Brightness, Hardware.Laser.myconfig.Contrast, Hardware.Laser.myconfig.PointTime, Hardware.Laser.myconfig.dpi, Hardware.Laser.myconfig.blDisableMarkLowGray, Hardware.Laser.myconfig.minLowGrayPt);
                    //Laser.SetBitmapEntParam("LaserBitmapro", "Image/PictureUp.bmp", 0, 0, 0, 0, 100, 500, false, 90);
                    int resualt = Laser.Mark(false);
                    System.Threading.Thread.Sleep(500);
                }
            }
            finishwork();
        }
        private static void Rotin_ClearDispenserCardJam()
        {
            Retry:
            try
            {
                Array.Resize(ref myjob.LatestJobDone, myjob.LatestJobDone.Length + 1);
                myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] = "Job_ClearDispenserCardJam";
            }
            catch (Exception)
            {
                myjob.LatestJobDone = new string[0] { };
                goto Retry;
            }

            bl.FileWork.changeState(FileWork.StateOfmashin.DispenserJam_Start);
            Config.ClearDispenserCardJam();

            newjob = false;
        }
        private static void Rotin_Rotate()
        {
            Retry:
            try
            {
                Array.Resize(ref myjob.LatestJobDone, myjob.LatestJobDone.Length + 1);
                myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] = "Job_Rotate";
            }
            catch (Exception)
            {
                myjob.LatestJobDone = new string[0] { };
                goto Retry;
            }

            bl.FileWork.changeState(FileWork.StateOfmashin.Rotate_Start);
            Config.RotateCard();

            newjob = false;
        }
        private static void Rotin_laserZir()
        {
            
            Laser.ClearLibAllEntity();
            Laser.SetPenParam(Worker.myjob.PenZir.nPenNo, Worker.myjob.PenZir.nMarkLoop, Worker.myjob.PenZir.dMarkSpeed, Worker.myjob.PenZir.dPowerRatio, Worker.myjob.PenZir.dCurrent, Worker.myjob.PenZir.nFreq, Worker.myjob.PenZir.dQPulseWidth, Worker.myjob.PenZir.nStartTC, Worker.myjob.PenZir.nLaserOffTC, Worker.myjob.PenZir.nEndTC, Worker.myjob.PenZir.nPolyTC, Worker.myjob.PenZir.dJumpSpeed, Worker.myjob.PenZir.nJumpPosTC, Worker.myjob.PenZir.nJumpDistTC, Worker.myjob.PenZir.dEndComp, Worker.myjob.PenZir.dAccDist, Worker.myjob.PenZir.dPointTime, Worker.myjob.PenZir.bPulsePointMode, Worker.myjob.PenZir.nPulseNum, Worker.myjob.PenZir.dFlySpeed);
            Laser.LmcErrCode ErrorStatus2 = Laser.AddFileToLib(@"Image\PictureBottom.bmp", "LaserBitmapzir", Hardware.Laser.myconfig.Xcenter, Hardware.Laser.myconfig.Ycenter, 0, 8, 1, myjob.PenZir.nPenNo, false);
            if (ErrorStatus2 == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
            {

                Laser.SetBitmapEntParam("LaserBitmapzir", @"Image\PictureBottom.bmp", Hardware.Laser.myconfig.bmpttrib, Hardware.Laser.myconfig.bmpScanAttr, Hardware.Laser.myconfig.Brightness, Hardware.Laser.myconfig.Contrast, Hardware.Laser.myconfig.PointTime, Hardware.Laser.myconfig.dpi, Hardware.Laser.myconfig.blDisableMarkLowGray, Hardware.Laser.myconfig.minLowGrayPt);
                int resualt = Laser.Mark(false);
                System.Threading.Thread.Sleep(1000);
            }
            //Laser.LmcErrCode  ErrorStatus = Laser.AddFileToLib("test.bmp", "LaserBitmapFiles", -42.460, -27.381, 0, 0, 1, 0, true);
            //if (ErrorStatus == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
            //{

            //    Laser.SetBitmapEntParam("LaserBitmapFiles", "test.bmp", 0, 0, 0, 0, 100, 500, true, 90);

            //    int resualt = Laser.Mark(false);
            //    //bmp2 = System.Drawing.Bitmap.FromHbitmap(LaserDllImports.GetCurPrevBitmap((int)CardSize.CardXSize, (int)CardSize.CardYSize));


            //}
            finishwork();
        }
        private static void Rotin_InCr()
        {
            flgInCR = true;
            System.Diagnostics.Stopwatch Stopw = new System.Diagnostics.Stopwatch();
            if (Config.RotateState)
            {
                string strDeviceResponse = "";
                Config.MoveToCr();
                bl.FileWork.changeState(FileWork.StateOfmashin.MoveCr_Start);
                CR.ReturnDeviceStatus aa = CR.PermitBehind(ref strDeviceResponse);
                if (aa != CR.ReturnDeviceStatus.MB_OK)
                {
                    Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                    Worker.Error[Worker.Error.Length - 1] = strDeviceResponse;
                }
                newjob = false;
            }
            else
            {
                string Response = "";
                bool CheckInternalSensor = false;
                int Counter = 0;

                Retry:
                try
                {
                    Array.Resize(ref myjob.LatestJobDone, myjob.LatestJobDone.Length + 1);
                    myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] = "Job_InCr";
                }
                catch (Exception)
                {
                    myjob.LatestJobDone = new string[0] { };
                    goto Retry;
                }
                bl.FileWork.changeState(FileWork.StateOfmashin.MoveCr_Start);
                Config.MoveToCr();

                Stopw.Start();
                CR.ReturnDeviceStatus PermitBehind = CR.PermitBehind(ref Response);
                Stopw.Stop();

                if (Stopw.ElapsedMilliseconds > 1500 && PermitBehind != CR.ReturnDeviceStatus.MB_OK)
                {
                    Config.ClearCardJam();
                    PermitBehind = CR.PermitBehind(ref Response);
                    RetryGetSensor2:
                    if (CR.getInternalSensorStatus(ref Response, ref CheckInternalSensor) == CR.ReturnDeviceStatus.MB_OK)
                    {
                        if (CheckInternalSensor)
                        {
                            newjob = false;
                        }
                        else
                            if (Counter < 100)
                            goto RetryGetSensor2;
                    }
                    else
                    if (Counter < 100)
                        goto RetryGetSensor2;
                    if (Counter >= 100)
                    {
                        Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                        Worker.Error[Worker.Error.Length - 1] = Response + "لطفا خط شخصی سازی را بررسی نمایید.";
                    }

                }
                else
                {
                    RetryGetSensor:
                    if (CR.getInternalSensorStatus(ref Response, ref CheckInternalSensor) == CR.ReturnDeviceStatus.MB_OK)
                    {
                        if (CheckInternalSensor)
                        {
                            newjob = false;
                        }
                        else
                            if (Counter < 100)
                            goto RetryGetSensor;
                    }
                    else
                    if (Counter < 100)
                        goto RetryGetSensor;
                    if (Counter >= 100)
                    {
                        Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                        Worker.Error[Worker.Error.Length - 1] = Response + "لطفا خط شخصی سازی را بررسی نمایید.";
                    }
                }
            }

        }
        private static void Rotin_InCrKeepInside()
        {
            CR.ReturnDeviceStatus InCrKeepInside = new CR.ReturnDeviceStatus();
            int InCrKeepInsideCounter = 0;
            string strDeviceResponse = "";
            do
            {
                InCrKeepInsideCounter++;
                InCrKeepInside = CR.Initialize(CR.Inittype.KeepInside, ref strDeviceResponse);

            } while (InCrKeepInside != CR.ReturnDeviceStatus.MB_OK && InCrKeepInsideCounter < 100);

            if (InCrKeepInside != CR.ReturnDeviceStatus.MB_OK)
            {
                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                Worker.Error[Worker.Error.Length - 1] = strDeviceResponse + "راه اندازی ماژول انجام نشد";
            }
            finishwork();
        }
        private static void Rotin_crIntMoveToGate()
        {
            CR.ReturnDeviceStatus InCrKeepInside = new CR.ReturnDeviceStatus();
            int InCrKeepInsideCounter = 0;
            string strDeviceResponse = "";
            do
            {
                InCrKeepInsideCounter++;
                InCrKeepInside = CR.Initialize(CR.Inittype.MoveTogate, ref strDeviceResponse);

            } while (InCrKeepInside != CR.ReturnDeviceStatus.MB_OK && InCrKeepInsideCounter < 100);

            if (InCrKeepInside != CR.ReturnDeviceStatus.MB_OK)
            {
                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                Worker.Error[Worker.Error.Length - 1] = strDeviceResponse + "راه اندازی ماژول انجام نشد";
            }
            finishwork();
        }
        private static void Rotin_MoveToRejectBox()
        {
            Retry:
            try
            {
                Array.Resize(ref myjob.LatestJobDone, myjob.LatestJobDone.Length + 1);
                myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] = "Job_MoveToRejectBox";
            }
            catch (Exception)
            {
                myjob.LatestJobDone = new string[0] { };
                goto Retry;
            }

            bl.FileWork.changeState(FileWork.StateOfmashin.MoveToRejectBox_Start);
            Config.MoveToRejectBox();

            newjob = false;
        }
        private static void Rotin_ClearCardJam()
        {
            string strDeviceResponse = "";
            string ResponseStatus = "";
            bool Status = false;
            Config.ClearCardJam();

            Retry:
            try
            {
                Array.Resize(ref myjob.LatestJobDone, myjob.LatestJobDone.Length + 1);
                myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] = "Job_ClearCardJam";
            }
            catch (Exception)
            {
                myjob.LatestJobDone = new string[0] { };
                goto Retry;
            }

            System.Threading.Thread.Sleep(2000);
            CR.ReturnDeviceStatus getBackSensorStatus = CR.getBackSensorStatus(ref ResponseStatus, ref Status);
            if (getBackSensorStatus == CR.ReturnDeviceStatus.MB_OK)
            {
                
                if (Status)
                {
                    CR.ReturnDeviceStatus Initstatus = CR.Initialize(CR.Inittype.MoveTogate, ref strDeviceResponse);
                    if (Initstatus != CR.ReturnDeviceStatus.MB_OK)
                    {
                        Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);

                        Worker.Error[Worker.Error.Length - 1] = strDeviceResponse;

                    }
                    else
                    {
                        CR.ReturnDeviceStatus getGateSensorStatus = CR.getGateSensorStatus(ref ResponseStatus, ref Status);
                        if (Status)
                        {
                            Config.MoveToRejectBox();
                            System.Threading.Thread.Sleep(4000);
                            CR.CardToRejectBox(ref ResponseStatus);
                            bl.FileWork.changeState(FileWork.StateOfmashin.Ready);
                            Worker.myjob.Status = job.StatusList.printed;
                            Worker.Active = false;
                            newjob = false;


                        }
                    }

                }
                else
                {
                    SensorRetry:
                    Config.StutDispenserSensorVariable.flgGetDispenserSensorStatus = false;
                    Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusSucsess = false;
                    Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusRetry = false;
                    int GetSensorStatusCounter = 0;
                    Config.SendDispenserSensorStatus();
                    CR.ReturnDeviceStatus PermitStatus = CR.PermitBehind(ref strDeviceResponse);
                    if (PermitStatus == CR.ReturnDeviceStatus.MB_OK)
                    {
                        Config.MoveToRejectBox();
                        System.Threading.Thread.Sleep(4000);
                        CR.CardToRejectBox(ref ResponseStatus);
                        bl.FileWork.changeState(FileWork.StateOfmashin.Ready);
                        Worker.myjob.Status = job.StatusList.printed;
                        Worker.Active = false;
                        newjob = false;
                    }
                    else
                    {
                        while (!Config.StutDispenserSensorVariable.flgGetDispenserSensorStatus && GetSensorStatusCounter < 100)
                        {
                            GetSensorStatusCounter++;
                            System.Threading.Thread.Sleep(10);
                        }
                        if (Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusSucsess)
                        {
                            Config.StutDispenserSensorVariable.flgGetDispenserSensorStatus = false;
                            if (Config.StutDispenserSensorVariable.flgDispenserSensor_EndWayCardDetector)
                                System.Windows.Forms.MessageBox.Show("لطفا مسیر حرکت کارت را بررسی نمایید", "خطا", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            else
                            {
                                bl.FileWork.changeState(FileWork.StateOfmashin.Ready);
                                Worker.myjob.Status = job.StatusList.printed;
                                Worker.Active = false;
                                newjob = false;
                            }
                        }
                        else if (Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusRetry)
                            goto SensorRetry;
                        Config.StutDispenserSensorVariable.flgGetDispenserSensorStatus = false;
                        Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusSucsess = false;
                        Config.StutDispenserSensorVariable.flgGetDispenserSensorStatusRetry = false;
                    }
                }
            }
            else
            {
                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                Worker.Error[Worker.Error.Length - 1] = strDeviceResponse + "کارت به مخزن خروجی انتقال پیدا نکرد";
            }
        }
        private static void Rotin_CR_Init_Nomove()
        {
            CR.ReturnDeviceStatus InCrKeepInside = new CR.ReturnDeviceStatus();
            int InCrKeepInsideCounter = 0;
            string strDeviceResponse = "";
            do
            {
                InCrKeepInsideCounter++;
                InCrKeepInside = CR.Initialize(CR.Inittype.NoMoveCard, ref strDeviceResponse);

            } while (InCrKeepInside != CR.ReturnDeviceStatus.MB_OK && InCrKeepInsideCounter < 100);

            if (InCrKeepInside != CR.ReturnDeviceStatus.MB_OK)
            {
                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                Worker.Error[Worker.Error.Length - 1] = strDeviceResponse + "راه اندازی ماژول انجام نشد";
            }
            finishwork();
        }
        private static void Rotin_CR_Init_MoveToGate()
        {
            CR.ReturnDeviceStatus InCrKeepInside = new CR.ReturnDeviceStatus();
            int InCrKeepInsideCounter = 0;
            string strDeviceResponse = "";
            do
            {
                InCrKeepInsideCounter++;
                InCrKeepInside = CR.Initialize(CR.Inittype.KeepInside, ref strDeviceResponse);

            } while (InCrKeepInside != CR.ReturnDeviceStatus.MB_OK && InCrKeepInsideCounter < 100);

            if (InCrKeepInside != CR.ReturnDeviceStatus.MB_OK)
            {
                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                Worker.Error[Worker.Error.Length - 1] = strDeviceResponse + "راه اندازی ماژول انجام نشد";
            }
            finishwork();
        }
        private static void Rotin_WriteCr()
        {
            CR.ReturnDeviceStatus WriteCr = new CR.ReturnDeviceStatus();


            WriteCr = CR.WriteAllMagneticTrack(Worker.crTrack);

            finishwork();
        }
        private static void Rotin_ReadCr()
        {
            finishwork();
        }
        private static void Rotin_out()
        {
            flgInCR = false;
            string strDeviceResponse = "";
            CR.ReturnDeviceStatus cc = CR.CardToEject(ref strDeviceResponse);
            if (cc != CR.ReturnDeviceStatus.MB_OK)
            {
                Array.Resize<string>(ref Worker.Error, Worker.Error.Length + 1);
                Worker.Error[Worker.Error.Length - 1] = strDeviceResponse;
                bl.FileWork.changeState(FileWork.StateOfmashin.CrJam_End);

            }
            bl.FileWork.changeState(FileWork.StateOfmashin.Printed);
            Worker.myjob.Status = job.StatusList.printed;
            finishwork();

            Worker.Active = false;
        }
        private static void Status_OK()
        {
            NoCardExist = false;
            flgCardHolderJam = false;
            flgDispenserJam = false;
            NoCardAndMagazineExist = false;
            Worker.myjob.InputDataIncurrectCount = 0;
            if (Worker.myjob.WorkList.Length > Worker.myjob.done.Length)
            {
                string[] temp2 = Worker.myjob.done;
                Array.Resize(ref temp2, Worker.myjob.done.Length + 1);
                temp2[temp2.Length - 1] = "done";
                Worker.myjob.done = temp2;
                switch (FileWork.stateMashin)
                {
                    case FileWork.StateOfmashin.pickupStacker_Start:
                        FileWork.changeState(FileWork.StateOfmashin.pickupStacker_End);
                        break;
                    case FileWork.StateOfmashin.pickupStacker_End:
                        FileWork.changeState(FileWork.StateOfmashin.reciveForMarking_Start);
                        break;
                    case FileWork.StateOfmashin.Rotate_Start:
                        FileWork.changeState(FileWork.StateOfmashin.Rotate_End);
                        break;
                    case FileWork.StateOfmashin.MoveToRejectBox_Start:
                        FileWork.changeState(FileWork.StateOfmashin.MoveToRejectBox_End);
                        break;
                    case FileWork.StateOfmashin.MoveToRejectBoxFromCR_Start:
                        FileWork.changeState(FileWork.StateOfmashin.MoveToRejectBoxFromCR_End);
                        break;
                    case FileWork.StateOfmashin.MoveCr_Start:
                        FileWork.changeState(FileWork.StateOfmashin.MoveCr_End);
                        break;
                    case FileWork.StateOfmashin.MarkingAreaJam_Start:
                        FileWork.changeState(FileWork.StateOfmashin.MarkingAreaJam_End);
                        break;
                    case FileWork.StateOfmashin.InCR_Start:
                        FileWork.changeState(FileWork.StateOfmashin.InCR_End);
                        break;
                    case FileWork.StateOfmashin.DispenserJam_Start:
                        FileWork.changeState(FileWork.StateOfmashin.DispenserJam_End);
                        break;
                    case FileWork.StateOfmashin.CrJam_Start:
                        FileWork.changeState(FileWork.StateOfmashin.CrJam_End);
                        break;
                    default:
                        break;
                }
                if (myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] == "Job_TakeCard") flgTakeCardDone = true;
                else flgTakeCardDone = false;
                myjob.LatestJobDone[myjob.LatestJobDone.Length - 1] = "JobOk_" + myjob.LatestJobDone[myjob.LatestJobDone.Length - 1];
                if (myjob.LatestJobDone.Length == 2 && FileWork.stateMashin == FileWork.StateOfmashin.MoveToRejectBox_End)
                {
                    if (myjob.LatestJobDone[0] == "JobOk_Job_TakeCard" && myjob.LatestJobDone[1] == "JobOk_Job_MoveToRejectBox")
                    {
                        FileWork.changeState(FileWork.StateOfmashin.Printed);
                        Worker.myjob.Status = job.StatusList.printed;
                        Worker.Active = false;
                        newjob = false;

                    }
                }
                else
                {
                    newjob = true;
                }

            }
            else
            {

                FileWork.changeState(FileWork.StateOfmashin.Printed);
                Worker.myjob.Status = job.StatusList.printed;
                Worker.Active = false;
            }
        }
        #endregion

    }
}
