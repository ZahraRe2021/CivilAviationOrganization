using SaraPrinterLaser.bl;
using SaraPrinterLaser.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls;

namespace SaraPrinterLaser
{
    public partial class RejectForm : Telerik.WinControls.UI.RadForm
    {

        job CardToRejectBox;
        string Model;

        public RejectForm(string model)
        {
            InitializeComponent();
            Model = model;


        }

        private void radLabel3_Click(object sender, EventArgs e)
        {

        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            lblState.Text = bl.FileWork.stateMashin.ToString();
            for (int i = 0; i < Worker.Error.Length; i++)
            {


                if (!Worker.Error[i].Contains("##"))
                {
                    Label a = new Label();
                    a.Text = Worker.Error[i];
                    a.Height = 20;
                    a.AutoSize = false;
                    a.Width = 500;
                    flpError.Controls.Add(a);
                    Worker.Error[i] = "##" + Worker.Error[i];

                }


            }
            if (CardToRejectBox.done.Length > 0)
            {
                for (int j = 0; j < Worker.myjob.done.Length - flpProcessTik.Controls.Count; j++)
                {
                    PictureBox pic = new PictureBox();
                    pic.ImageLocation = "gif/check.jpg";
                    pic.Width = 20;
                    pic.Height = 20;
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    flpProcessTik.Controls.Add(pic);
                    PbarPrint.Value1++;
                    if (PbarPrint.Maximum > PbarPrint.Value2 && PbarPrint.Maximum != 1)
                    {
                        PbarPrint.Value2 = PbarPrint.Value1 + 1;

                    }

                    PbarPrint.Text = string.Format("{0}/{1}", PbarPrint.Value1, PbarPrint.Maximum);


                }


            }

            if (Worker.myjob.Status == job.StatusList.printed)
            {
                this.DialogResult = DialogResult.OK;
            }
            for (int i = 0; i < Worker.Error.Length; i++)
            {


                if (!Worker.Error[i].Contains("##"))
                {
                    Label a = new Label();
                    a.Text = Worker.Error[i];
                    a.AutoSize = false;
                    a.Width = 500;
                    flpError.Controls.Add(a);
                    Worker.Error[i] = "##" + Worker.Error[i];

                }


            }

        }

        private void RejectForm_Load(object sender, EventArgs e)
        {
            CR.ReturnDeviceStatus InitStatus = new CR.ReturnDeviceStatus();
            string Response = "";
            int initCounter = 0, initJ = 0, initI = 0; ;
            bool Restart = false;
            do
            {
                if (initI > 1) Restart = true;
                if (initCounter == 0)
                {
                    InitStatus = Hardware.CR.Initialize(CR.Inittype.NoMoveCard, ref Response);
                    initJ++;
                }
                Thread.Sleep(5);
                initCounter++;
                if (initCounter > 10) initCounter = 0;
                if (initJ > 5)
                {
                    initI++;
                    initJ = 0;
                    CR.CRT350RClose(CR.Hdle);
                    Config.CloseAllPortExceptLaser();
                    Thread.Sleep(1000);
                    CR.Hdle = CR.CRT350ROpen(Config.CrPortName);
                    Config.OpenAllPortExceptLaser();
                    if (CR.Hdle == 0) Restart = true;
                }
            } while (InitStatus != CR.ReturnDeviceStatus.MB_OK && !Restart);
            initCounter = 0; initJ = 0;
            if (!Restart)
            {
                switch (Model)
                {
                    case "Dispenser":
                        CardToRejectBox = new job(job.jobModel.JamDispenser);

                        break;
                    case "CardHolder":
                        CardToRejectBox = new job(job.jobModel.JamHolder);
                        break;
                    case "CR":
                        CardToRejectBox = new job(job.jobModel.JamCR);
                        break;
                    case "JamMarkingArea":
                        CardToRejectBox = new job(job.jobModel.JamMarkingArea);
                        break;


                    default:
                        break;
                }
                lblState.Text = Model;
                Worker.newjob = true;
                Worker.Error = new string[0] { };
                FileWork.ClearAnswer();
                FileWork.answer = new string[0] { };
                Worker.myjob = CardToRejectBox;
                Worker.Active = true;
                Worker.myjob.done = new string[0] { };
                Worker RejectCard = new Worker();
                PbarPrint.Minimum = 0;
                PbarPrint.Value1 = 0;

                PbarPrint.Maximum = Worker.myjob.WorkList.Length;
                PbarPrint.Text = string.Format("{0}/{1}", PbarPrint.Value1, PbarPrint.Maximum);
                for (int i = 0; i < Worker.myjob.WorkList.Length; i++)
                {
                    Label lbl = new Label();
                    lbl.Height = 20;
                    lbl.Text = Worker.myjob.WorkList[i];
                    flpProcessName.Controls.Add(lbl);
                }
                RejectCard.main.Start();
                timerStatus.Start();
            }
            else
            {
                MessageBox.Show("سخت افزار دچار مشکل شده است لطفا برنامه را بسته ، پرینتر را خاموش و بعد از 10 ثانیه روشن کنید ،برنامه را دوباره اجرا نمایید.");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RejectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerStatus.Stop();
            Worker.Active = false;
        }
    }
}
