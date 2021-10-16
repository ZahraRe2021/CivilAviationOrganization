using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SaraPrinterLaser.Hardware;
using SaraPrinterLaser.LayoutDesignFolder;

namespace SaraPrinterLaser.bl
{
    public class FileWork
    {
        private static Object thisLock = new Object();
        private static Object thisLock2 = new Object();
        private static Object thisLock3 = new Object();
        public static string[] commmand = new string[0];
        public static string[] answer = new string[0];
        public static FileWork.StateOfmashin stateMashin;
        public static bool Answered;
        internal static string FileerrorFile = "errorFile.vsc";
        internal static string FileLogFile = "Log.vsc";
        internal static string FileJobFile = "onlinejob.vsc";
        internal static string FileAnswerMashin = "Answer.vsc";
        internal static string FileStateMashin = "State.vsc";
        internal static string FileMachineType = "MachineType.vsc";
        public static bool readAnswerMashin()
        {
            lock (thisLock)
            {

                try
                {
                    string[] resault = File.ReadAllLines(FileAnswerMashin);
                    for (int i = 0; i < resault.Length; i++)
                    {
                        resault[i] = resault[i];

                    }
                    answer = resault;
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }


        }
        public enum StateOfmashin
        {
            Ready,
            Printed,
            pickupStacker_Start, pickupStacker_End,
            DispenserJam_Start, DispenserJam_End,
            reciveForMarking_Start, reciveForMarking_End,
            MarkingAreaJam_Start, MarkingAreaJam_End,
            Rotate_Start, Rotate_End,
            MoveCr_Start, MoveCr_End,
            InCR_Start, InCR_End,
            MoveToRejectBox_Start, MoveToRejectBox_End,
            CrJam_Start, CrJam_End,
            MoveToRejectBoxFromCR_Start, MoveToRejectBoxFromCR_End,



        }
        public static string[] ReadAllSecureConfig()
        {
            string[] SecureConfig = new string[4];

            try
            {
                SecureConfig = File.ReadAllLines(FileMachineType);
            }
            catch (Exception)
            {
                SecureConfig = null;

            }


            return SecureConfig;

        }


        public static bool writeAnswerMashin(string NewAnswer)
        {
            lock (thisLock2)
            {
                try
                {
                    lock (thisLock)
                    {


                        File.AppendAllText(FileAnswerMashin, NewAnswer + "\r\n");
                        Array.Resize<string>(ref answer, answer.Length + 1);
                        answer[answer.Length - 1] = NewAnswer;

                    }

                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }


        }
        public static void ClearAnswer()
        {
            try
            {
                lock (thisLock3)
                {
                    FileStream fcreate = File.Open(FileAnswerMashin, FileMode.Create);
                    FileWork.answer = new string[0];
                    fcreate.Close();

                }

            }
            catch (Exception)
            {


            }

        }
        public static void changeState(FileWork.StateOfmashin NewState)
        {
            lock (thisLock3)
            {
                FileWork.stateMashin = NewState;
                File.WriteAllText(FileStateMashin, NewState.ToString());

            }


        }
        public static void readstate()
        {
            if (File.Exists(FileStateMashin))
            {
                string resualt = File.ReadAllText(FileStateMashin);

                switch (resualt)
                {
                    case "Ready":
                        FileWork.stateMashin = FileWork.StateOfmashin.Ready;
                        break;
                    case "pickupStacker_Start":
                        FileWork.stateMashin = FileWork.StateOfmashin.pickupStacker_Start;
                        break;
                    case "pickupStacker_End":
                        FileWork.stateMashin = FileWork.StateOfmashin.pickupStacker_End;
                        break;
                    case "DispenserJam_Start":
                        FileWork.stateMashin = FileWork.StateOfmashin.DispenserJam_Start;
                        break;
                    case "DispenserJam_End":
                        FileWork.stateMashin = FileWork.StateOfmashin.DispenserJam_End;
                        break;
                    case "reciveForMarking_Start":
                        FileWork.stateMashin = FileWork.StateOfmashin.reciveForMarking_Start;
                        break;
                    case "reciveForMarking_End":
                        FileWork.stateMashin = FileWork.StateOfmashin.reciveForMarking_End;
                        break;
                    case "MarkingAreaJam_Start":
                        FileWork.stateMashin = FileWork.StateOfmashin.MarkingAreaJam_Start;
                        break;
                    case "MarkingAreaJam_End":
                        FileWork.stateMashin = FileWork.StateOfmashin.MarkingAreaJam_End;
                        break;
                    case "Rotate_Start":
                        FileWork.stateMashin = FileWork.StateOfmashin.Rotate_Start;
                        break;
                    case "Rotate_End":
                        FileWork.stateMashin = FileWork.StateOfmashin.Rotate_End;
                        break;
                    case "MoveCr_Start":
                        FileWork.stateMashin = FileWork.StateOfmashin.MoveCr_Start;
                        break;
                    case "MoveCr_End":
                        FileWork.stateMashin = FileWork.StateOfmashin.MoveCr_End;
                        break;
                    case "InCR_Start":
                        FileWork.stateMashin = FileWork.StateOfmashin.InCR_Start;
                        break;
                    case "InCR_End":
                        FileWork.stateMashin = FileWork.StateOfmashin.InCR_End;
                        break;
                    case "MoveToRejectBox_Start":
                        FileWork.stateMashin = FileWork.StateOfmashin.MoveToRejectBox_Start;
                        break;
                    case "MoveToRejectBox_End":
                        FileWork.stateMashin = FileWork.StateOfmashin.MoveToRejectBox_End;
                        break;
                    case "CrJam_Start":
                        FileWork.stateMashin = FileWork.StateOfmashin.CrJam_Start;
                        break;
                    case "CrJam_End":
                        FileWork.stateMashin = FileWork.StateOfmashin.CrJam_End;
                        break;
                    case "MoveToRejectBoxFromCR_Start":
                        FileWork.stateMashin = FileWork.StateOfmashin.MoveToRejectBoxFromCR_Start;
                        break;
                    case "MoveToRejectBoxFromCR_End":
                        FileWork.stateMashin = FileWork.StateOfmashin.MoveToRejectBoxFromCR_End;
                        break;
                    default:
                        break;
                }

            }
            else
            {
                FileWork.changeState(StateOfmashin.Ready);
            }
        }
        public static void WritePen(string PenName, LaserPen penParam)
        {
            if (File.Exists("Pen\\" + PenName + ".pen"))
            {
                File.Delete("Pen\\" + PenName + ".pen");

            }

            System.IO.Stream ms = File.OpenWrite("Pen\\" + PenName + ".pen");


            BinaryFormatter formatter = new BinaryFormatter();
            //It serialize the employee object
            formatter.Serialize(ms, penParam);
            ms.Flush();
            ms.Close();
            ms.Dispose();

        }
        public static void WriteCAOspecificCardLaserPen(string PenName, CAOspecificCardLaserPen penParam)
        {
            if (!Directory.Exists("CAO Specific Card Laser Pen")) Directory.CreateDirectory("CAO Specific Card Laser Pen");
            if (File.Exists("CAO Specific Card Laser Pen\\" + PenName + ".CAOPEN"))
            {
                File.Delete("CAO Specific Card Laser Pen\\" + PenName + ".CAOPEN");

            }

            System.IO.Stream ms = File.OpenWrite("CAO Specific Card Laser Pen\\" + PenName + ".CAOPEN");


            BinaryFormatter formatter = new BinaryFormatter();
            //It serialize the employee object
            formatter.Serialize(ms, penParam);
            ms.Flush();
            ms.Close();
            ms.Dispose();

        }
        public static CAOspecificCardLaserPen ReadCAOspecificCardLaserPen(string PenName)
        {
            CAOspecificCardLaserPen resualt = new CAOspecificCardLaserPen();
            BinaryFormatter formatter = new BinaryFormatter();


            FileStream fs = File.Open("CAO Specific Card Laser Pen\\" + PenName + ".CAOPEN", FileMode.Open);

            object obj = formatter.Deserialize(fs);
            resualt = (CAOspecificCardLaserPen)obj;
            fs.Flush();
            fs.Close();
            fs.Dispose();
            return resualt;

        }
        public static LaserPen readPen(string PenName)
        {
            LaserPen resualt = new LaserPen();
            BinaryFormatter formatter = new BinaryFormatter();


            FileStream fs = File.Open("Pen\\" + PenName + ".pen", FileMode.Open);

            object obj = formatter.Deserialize(fs);
            resualt = (LaserPen)obj;
            fs.Flush();
            fs.Close();
            fs.Dispose();
            return resualt;

        }
    }

}
