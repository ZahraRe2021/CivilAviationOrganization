using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraPrinterLaser.Hardware
{
    public class job
    {
        public string[] WorkList { get; set; }
        public string[] done { get; set; }
        public string[] LatestJobDone;
        public bool checkRFid { get; set; }
        public string filename { get; set; }
        public int InputDataIncurrectCount { get; set; }
        public job.StatusList Status { get; set; }
        public DateTime CreateDate { get; set; }
        public LaserPen PenRo { get; set; }
        public LaserPen PenZir { get; set; }
        public LaserPen jobTopPicturePen { get; set; }
        public LaserPen jobBottomPicturePen { get; set; }
        public enum StatusList
        {
            startPrint,
            printed,
            Emtpy,
            OnWork,
            ErrorHardWere,
            CardJam,
            JamCleared,
        }
        public enum jobModel
        {
            CheckFilliper,
            NoRFIDCard,
            CheckSerial,
            PrintCard1,
            PrintCard2,
            PrintCard1_r,
            PrintCard2_r,
            PrintCard1_c,
            PrintCard2_c,
            PrintCard1_c_r,
            PrintCard2_c_r,
            JamHolder,
            JamDispenser,
            JamCR,
            JamMarkingArea
        }
        public job(jobModel model)
        {
            switch (model)
            {
                case jobModel.CheckFilliper:
                    WorkList = new string[] { "CheckFiliper" };
                    break;
                case jobModel.CheckSerial:
                    WorkList = new string[] { "GetSerialNumber" };
                    break;
                case jobModel.PrintCard1:
                    WorkList = new string[] { "stacker1", "LaserRo", "Rotate", "laserZir", "Rotate", "InCr", "WriteCr", "ReadCr", "out" };
                    break;
                case jobModel.NoRFIDCard:
                    WorkList = new string[] { "MoveToRejectBox" };
                    break;
                case jobModel.PrintCard2:
                    WorkList = new string[] { "stacker2", "LaserRo", "Rotate", "laserZir", "InCr", "WriteCr", "ReadCr", "out" };
                    break;
                case jobModel.PrintCard1_r:
                    WorkList = new string[] { "stacker1", "LaserRo", "InCr", "WriteCr", "ReadCr", "out" };
                    break;
                case jobModel.PrintCard2_r:
                    WorkList = new string[] { "stacker2", "LaserRo", "InCr", "WriteCr", "ReadCr", "out" };
                    break;
                case jobModel.PrintCard1_c:
                    WorkList = new string[] { "stacker1", "LaserRo", "Rotate", "laserZir", "InCr", "out" };
                    break;
                case jobModel.PrintCard2_c:
                    WorkList = new string[] { "stacker2", "LaserRo", "Rotate", "laserZir", "InCr", "out" };
                    break;
                case jobModel.PrintCard1_c_r:
                    WorkList = new string[] { "stacker1", "LaserRo", "InCr", "out" };
                    break;
                case jobModel.PrintCard2_c_r:
                    WorkList = new string[] { "stacker2", "LaserRo", "InCr", "out" };
                    break;
                //Jam Job
                case jobModel.JamHolder:
                    WorkList = new string[] { "ClearCardJam" };
                    break;
                case jobModel.JamDispenser:
                    WorkList = new string[] { "MoveToRejectBox" };
                    break;
                case jobModel.JamMarkingArea:
                    WorkList = new string[] { "ClearRejectBox" };
                    break;
                case jobModel.JamCR:
                    WorkList = new string[] { "InCrKeepInside", "MoveToRejectBoxFromCR" };
                    break;
                default:
                    break;
            }
        }
    }
}