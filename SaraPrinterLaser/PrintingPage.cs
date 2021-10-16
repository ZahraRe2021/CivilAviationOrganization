using SaraPrinterLaser.bl;
using SaraPrinterLaser.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaraPrinterLaser
{
    public partial class PrintingPage : Form
    {
        job test;
        bool cractive = false;
        bool rotateactive = false;
        bool rfid = false;
        int cardholder = 1;
        string cr1 = "";
        string cr2 = "";
        string cr3 = "";

        bool ATrack1 = false;
        bool ATrack2 = false;
        bool ATrack3 = false;
        Hardware.LaserPen penro;
        Hardware.LaserPen penzir;
        LaserPen TopPicturePen;
        LaserPen BottomPicturePen;
        Worker workerprint;
        int PrintNumber = 0;
        int prtCount = 0;
        bool ShowOnce = false;
        bool PrintCancel = false;
        bool Onetime = false;
        bool flgMoveToRejectBoxCorruptedMagneticCard = false;

        public PrintingPage(bool CRActive, bool Track1Active, bool Track2Active, bool Track3Active, bool _flgMoveToRejectBoxCorruptedMagneticCard, bool RotateActive, bool RFID, int CardHolder, string CR1, string Cr2, string Cr3, string PenRo, string PicturePenTop, string PicturePenBottom, string PenZir, int PrintCount)
        {
            rfid = RFID;
            cractive = CRActive;
            rotateactive = RotateActive;
            cardholder = CardHolder;
            cr1 = CR1;
            cr2 = Cr2;
            cr3 = Cr3;

            ATrack1 = Track1Active;
            ATrack2 = Track2Active;
            ATrack3 = Track3Active;

            flgMoveToRejectBoxCorruptedMagneticCard = _flgMoveToRejectBoxCorruptedMagneticCard;

            penro = bl.FileWork.readPen(PenRo);
            penzir = bl.FileWork.readPen(PenZir);

            TopPicturePen = bl.FileWork.readPen(PicturePenTop);
            BottomPicturePen = bl.FileWork.readPen(PicturePenBottom);

            prtCount = PrintCount;
            InitializeComponent();


        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            bool flgFailMagnetiCWrite = false;
            lblState.Text = bl.FileWork.stateMashin.ToString();
            for (int i = 0; i < Worker.Error.Length; i++)
            {
                try
                {
                    if (!Worker.Error[i].Contains("##"))
                    {
                        if (Worker.Error[i].Contains("اطلاعات مغناطیس نوشته نشد"))
                        {
                            Label a = new Label();
                            a.Text = Worker.Error[i];
                            a.Height = 20;
                            a.AutoSize = false;
                            a.Width = 500;
                            flpError.Controls.Add(a);
                            Worker.Error[i] = "##" + Worker.Error[i];
                           
                        }
                        else
                        {
                            Label a = new Label();
                            a.Text = Worker.Error[i];
                            a.Height = 20;
                            a.AutoSize = false;
                            a.Width = 500;
                            flpError.Controls.Add(a);
                            Worker.Error[i] = "##" + Worker.Error[i];
                        }
                        // if (!Worker.Error[i].Contains("اطلاعات در یافتی اشتباه است"))
                        //{

                        //}
                    }
                }
                catch (Exception)
                {
                    Array.Resize<string>(ref Worker.Error, 0);
                }

            }
            if (test.done.Length > 0)
            {
                for (int j = 0; j < Worker.myjob.done.Length - flpProcessTik.Controls.Count; j++)
                {
                    PictureBox pic = new PictureBox();
                    pic.ImageLocation = "gif/check.jpg";
                    pic.Width = 20;
                    pic.Height = 28;
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    flpProcessTik.Controls.Add(pic);
                    PbarPrint.Value1++;
                    if (PbarPrint.Maximum > PbarPrint.Value2)
                    {
                        PbarPrint.Value2 = PbarPrint.Value1 + 1;
                    }

                    PbarPrint.Text = string.Format("{0}/{1}", PbarPrint.Value1, PbarPrint.Maximum);


                }


            }
            switch (Worker.myjob.Status)
            {
                case job.StatusList.printed:
                    {
                        bool checkCard = true;
                        if (!PrintCancel)
                        {
                            string strDeviceResponse = "";
                            Thread.Sleep(1000);
                            CR.ReturnDeviceStatus cc = CR.getGateSensorStatus(ref strDeviceResponse, ref checkCard);
                            if (cc == CR.ReturnDeviceStatus.MB_OK)
                            {
                                if (checkCard)
                                {
                                    if (!ShowOnce)
                                    {
                                        ShowOnce = true;
                                        MessageBox.Show("لطفا کارت چاپ شده را بردارید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    }

                                }
                                else
                                {
                                    ShowOnce = false;
                                    SendKeys.Send("{ESC}");
                                    PrintNumber++;
                                    if (PrintNumber < prtCount)
                                    {
                                        PbarCount.Value1++;
                                        if (PbarCount.Maximum > PbarCount.Value2)
                                        {
                                            PbarCount.Value2 = PbarCount.Value1 + 1;
                                            PbarCount.Refresh();
                                            PbarCount.Update();


                                        }

                                        PbarCount.Text = string.Format("{0}/{1}", PbarCount.Value1, PbarCount.Maximum);
                                        PrintStart();
                                    }
                                    else
                                    {
                                        bl.FileWork.changeState(bl.FileWork.StateOfmashin.Ready);
                                        bl.FileWork.ClearAnswer();
                                        btnOk.Enabled = true;
                                        timerStatus.Stop();
                                        MessageBox.Show("Print ok ..");
                                        this.Close();
                                    }
                                }
                            }
                        }
                        else
                        {
                            timerStatus.Stop();
                            MessageBox.Show("Print ok ..");
                            this.Close();
                        }
                    }
                    break;

                case job.StatusList.Emtpy:
                    {
                        timerStatus.Stop();
                        bl.FileWork.changeState(bl.FileWork.StateOfmashin.Ready);
                        bl.FileWork.ClearAnswer();

                        ClearWorker();

                       // MessageBox.Show("لطفا کارت در مخزن قرار دهید");
                        workerprint.main.Abort();
                        this.Close();
                    }
                    break;
                case job.StatusList.OnWork:
                    break;
                case job.StatusList.ErrorHardWere:
                    {
                        timerStatus.Stop();
                        bl.FileWork.ClearAnswer();
                        btnOk.Enabled = true;
                        MessageBox.Show("سخت افزار دچار مشکل شده است لطفا برنامه را بسته ، پرینتر را خاموش و بعد از 10 ثانیه روشن کنید ،برنامه را دوباره اجرا نمایید.");
                        this.Close();
                    }
                    break;
                case job.StatusList.JamCleared:
                    {
                        timerStatus.Stop();
                        bl.FileWork.changeState(bl.FileWork.StateOfmashin.Ready);
                        bl.FileWork.ClearAnswer();
                        btnOk.Enabled = true;
                        MessageBox.Show("کارت با موفقیت به جایگاه مرجوع منتقل شد . دستگاه آماده به کار است.");
                    }
                    break;
                case job.StatusList.CardJam:
                    {
                        timerStatus.Stop();
                        switch (bl.FileWork.stateMashin)
                        {


                            case bl.FileWork.StateOfmashin.DispenserJam_Start:
                                RejectForm frm = new RejectForm("Dispenser");
                                if (frm.ShowDialog() == DialogResult.OK)
                                {
                                    MessageBox.Show("1");
                                }
                                else
                                {
                                    MessageBox.Show("2");

                                }

                                break;


                            case bl.FileWork.StateOfmashin.MarkingAreaJam_Start:

                                RejectForm frm2 = new RejectForm("Dispenser");
                                if (frm2.ShowDialog() == DialogResult.OK)
                                {
                                    MessageBox.Show("1");
                                }
                                else
                                {
                                    MessageBox.Show("2");

                                }
                                break;


                            case bl.FileWork.StateOfmashin.CrJam_Start:
                                RejectForm frm3 = new RejectForm("Dispenser");
                                if (frm3.ShowDialog() == DialogResult.OK)
                                {
                                    MessageBox.Show("1");
                                }
                                else
                                {
                                    MessageBox.Show("2");

                                }

                                break;

                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void PrintingPage_Load(object sender, EventArgs e)
        {

            Onetime = false;
            PbarCount.Value1 = 0;
            PbarCount.Maximum = prtCount;
            PbarCount.Text = string.Format("{0}/{1}", PbarCount.Value1, PbarCount.Maximum);
            bool checkCard = true;

            int Counter = 10;
            string strDeviceResponse = "";
        Retry:
            CR.ReturnDeviceStatus cc = CR.getGateSensorStatus(ref strDeviceResponse, ref checkCard);
            if (cc == CR.ReturnDeviceStatus.MB_OK)
            {

                if (checkCard)
                {
                    MessageBox.Show("لطفا کارت چاپ شده را بردارید دکمه تایید را بزنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (Counter > 0)
                    {
                        Counter--;
                        goto Retry;
                    }
                    else
                        goto EndMission;
                }
                else
                    PrintStart();

                EndMission:
                if (Counter == 0)
                {
                    MessageBox.Show("به دلیل وجود داشتن کارت در خروجی چاپ انجام نشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

            }
            else
            {

                CR.CRT350RClose(CR.Hdle);
                Thread.Sleep(1000);
                CR.Hdle = CR.CRT350ROpen(Config.CrPortName);
                if (CR.Hdle == 0)
                {
                    MessageBox.Show("برنامه بسته و دوباره باز نمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                MessageBox.Show("پس از خاموش و روشن کردن دستگاه ، نرم افزار را بسته و دوباره باز کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bl.FileWork.ClearAnswer();
            //this.Close();
        }

        private void lblState_Click(object sender, EventArgs e)
        {

        }
        public static void ClearWorker()
        {
            bl.FileWork.ClearAnswer();

            Worker.Error = new string[0];

        }
        private void PrintStart()
        {

            Config.OpenAllPortExceptLaser();

            if (cardholder == 1)
            {
                if (rotateactive && Config.RotateState)
                {
                    if (cractive)
                    {
                        test = new job(job.jobModel.PrintCard1);
                    }
                    else
                    {
                        test = new job(job.jobModel.PrintCard1_c);

                    }
                }
                else
                {


                    if (cractive)
                    {
                        test = new job(job.jobModel.PrintCard1_r);

                    }
                    else
                    {
                        test = new job(job.jobModel.PrintCard1_c_r);

                    }



                }

            }
            else
            {
                if (rotateactive && Config.RotateState)
                {
                    if (cractive)
                    {
                        test = new job(job.jobModel.PrintCard2);
                    }
                    else
                    {
                        test = new job(job.jobModel.PrintCard2_c);

                    }

                }
                else
                {


                    if (cractive)
                    {
                        test = new job(job.jobModel.PrintCard2_r);

                    }
                    else
                    {
                        test = new job(job.jobModel.PrintCard2_c_r);

                    }
                }

            }

            test.checkRFid = rfid;
            Worker.newjob = true;
            Worker.myjob = test;
            Worker.Active = true;
            Worker.myjob.LatestJobDone = new string[0] { };
            Worker.myjob.done = new string[0] { };
            Worker.Error = new string[0];
            Worker.myjob.PenRo = penro;
            Worker.myjob.PenZir = penzir;
            Worker.myjob.jobTopPicturePen = TopPicturePen;
            Worker.myjob.jobBottomPicturePen = BottomPicturePen;
            Worker.myjob.Status = job.StatusList.startPrint;

            Worker.crTrack[0] = cr1;
            Worker.crTrack[1] = cr2;
            Worker.crTrack[2] = cr3;

            Worker.flgWitchTrackWrite[0] = ATrack1;
            Worker.flgWitchTrackWrite[1] = ATrack2;
            Worker.flgWitchTrackWrite[2] = ATrack3;
            Worker.FlgMoveToRejectBoxCorruptedMagneticCard = flgMoveToRejectBoxCorruptedMagneticCard;
            FileWork.answer = new string[0] { };
            FileWork.ClearAnswer();
            workerprint = new Worker();
            PbarCount.Minimum = 0;
            flpProcessTik.Controls.Clear();
            PbarPrint.Value1 = 0;
            PbarPrint.Value2 = 0;
            if (!Onetime)
            {
                Onetime = true;
                PbarPrint.Minimum = 0;

                PbarPrint.Maximum = Worker.myjob.WorkList.Length;
                PbarPrint.Text = string.Format("{0}/{1}", PbarPrint.Value1, PbarPrint.Maximum);
                for (int i = 0; i < Worker.myjob.WorkList.Length; i++)
                {
                    Label lbl = new Label();
                    lbl.Height = 35;
                    lbl.Text = Worker.myjob.WorkList[i];
                    flpProcessName.Controls.Add(lbl);
                }
            }
            workerprint.main.Start();
            timerStatus.Start();
        }

        private void CancelPrint_Click(object sender, EventArgs e)
        {
            PrintCancel = true;
            radLabel1.Text = "پس از پایان چاپ در حال انجام چاپ دیگری انجام نخواهد شد.";
        }
    }
}
