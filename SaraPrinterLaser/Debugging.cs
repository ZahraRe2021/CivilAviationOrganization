using SaraPrinterLaser.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaraPrinterLaser
{
    public enum ReturnDeviceStatus { MB_OK, MB_Error, MB_DataFormatFail, MB_ComportClose, MB_CardExist, MB_InsertCardInCartridge, NotInRange };
    public partial class Debugging : Form
    {
        public Debugging()
        {
            InitializeComponent();
        }
        bool flgCancelTurn = false;
        int ReseveFormarkingCounter = 0;
        int MovetoRejectBoxCounter = 0;

        private void btnReceiveCardForMarking_Click(object sender, EventArgs e)
        {
            //  timer1.Start();
            Config.ReceiveCardForMarkingValue();
        }

        private void btnPickStaker1_Click(object sender, EventArgs e)
        {
            try
            {
                Config.PickStaker1();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPickStaker2_Click(object sender, EventArgs e)
        {
            try
            {
                Config.PickStaker2();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearDispenserCardJam_Click(object sender, EventArgs e)
        {
            try
            {
                Config.ClearDispenserCardJam();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMoveToRejectBox_Click(object sender, EventArgs e)
        {
            //timer2.Stop();
            Config.MoveToRejectBox();
        }

        private void btnMoveToCr_Click(object sender, EventArgs e)
        {
            try
            {
                Config.MoveToCr();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRequestFlipperinstall_Click(object sender, EventArgs e)
        {
            try
            {
                Config.RequestFlipperinstall();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearCardJam_Click(object sender, EventArgs e)
        {
            try
            {
                Config.ClearCardJam();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPermitBehind_Click(object sender, EventArgs e)
        {
            try
            {
                string strDeviceResponse = "";
                CR.PermitBehind(ref strDeviceResponse);
                lblResult.Text = strDeviceResponse;
                lblResult.Refresh();
                lblResult.Update();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnintKeepInside_Click(object sender, EventArgs e)
        {
            try
            {
                string strDeviceResponse = "";
                CR.Initialize(CR.Inittype.KeepInside, ref strDeviceResponse);
                lblResult.Text = strDeviceResponse;
                lblResult.Refresh();
                lblResult.Update();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnintMoveToGate_Click(object sender, EventArgs e)
        {
            try
            {
                string strDeviceResponse = "";
                CR.Initialize(CR.Inittype.MoveTogate, ref strDeviceResponse);
                lblResult.Text = strDeviceResponse;
                lblResult.Refresh();
                lblResult.Update();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnintNoMove_Click(object sender, EventArgs e)
        {
            try
            {
                string strDeviceResponse = "";
                CR.Initialize(CR.Inittype.NoMoveCard, ref strDeviceResponse);
                lblResult.Text = strDeviceResponse;
                lblResult.Refresh();
                lblResult.Update();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCardToEject_Click(object sender, EventArgs e)
        {
            try
            {
                string strDeviceResponse = "";
                CR.CardToEject(ref strDeviceResponse);
                lblResult.Text = strDeviceResponse;
                lblResult.Refresh();
                lblResult.Update();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLaserOnPoint_Click(object sender, EventArgs e)
        {
            try
            {
                SetParameter(double.Parse(txtSpeedValue.Text), double.Parse(txtPowerValue.Text), 35000, 100, 300, 300, 0);
                Hardware.Laser.MarkPoint(double.Parse(txtXPos.Text), double.Parse(txtYPos.Text), double.Parse(txtTimeInMs.Text), 100);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); ;
                MessageBox.Show("لطفا اعداد را درست وارد نمایید","خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnRotateCard_Click(object sender, EventArgs e)
        {
            try
            {
                Config.RotateCard();
            }
            catch (Exception)
            {

                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public ReturnDeviceStatus SetParameter(double Speed, double Power, int Frequency, int StartTC, int LaserOffTC, int EndTc, int PolyTc)
        {
            ReturnDeviceStatus RetrunStatus = ReturnDeviceStatus.MB_Error;
            Hardware.Laser.LmcErrCode Laserstatus = Hardware.Laser.LmcErrCode.LMC1_ERR_UNKNOW;
            int ParamnMarkLoop = 0;
            double ParamdMarkSpeed = 0;
            double ParamdPowerRatio = 0;
            double ParamdCurrent = 0;
            int ParamnFreq = 0;
            double ParamdQPulseWidth = 0;
            int ParamnStartTC = 0;
            int ParamnLaserOffTC = 0;
            int ParamnEndTC = 0;
            int ParamnPolyTC = 0;
            double ParamdJumpSpeed = 0;
            int ParamnJumpPosTC = 0;
            int ParamnJumpDistTC = 0;
            double ParamdEndComp = 0;
            double ParamdAccDist = 0;
            double ParamdPointTime = 0;
            bool ParambPulsePointMode = false;
            int ParamnPulseNum = 0;
            double ParamdFlySpeed = 0;

            if ((Power > 0 && Power < 100) && (Speed > 0 && Speed < 30000))
            {
                Laserstatus = Hardware.Laser.GetPenParam(220, ref ParamnMarkLoop, ref ParamdMarkSpeed, ref ParamdPowerRatio, ref ParamdCurrent, ref ParamnFreq, ref ParamdQPulseWidth, ref ParamnStartTC, ref ParamnLaserOffTC, ref ParamnEndTC, ref ParamnPolyTC, ref ParamdJumpSpeed, ref ParamnJumpPosTC, ref ParamnJumpDistTC, ref ParamdEndComp, ref ParamdAccDist, ref ParamdPointTime, ref ParambPulsePointMode, ref ParamnPulseNum, ref ParamdFlySpeed);

                if (Laserstatus == Hardware.Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                {
                    Laserstatus = Hardware.Laser.LmcErrCode.LMC1_ERR_UNKNOW;
                    Laserstatus = Hardware.Laser.SetPenParam(0, ParamnMarkLoop, Speed, Power, ParamdCurrent, Frequency, ParamdQPulseWidth, StartTC, LaserOffTC, EndTc, PolyTc, ParamdJumpSpeed, ParamnJumpPosTC, ParamnJumpDistTC, ParamdEndComp, ParamdAccDist, ParamdPointTime, ParambPulsePointMode, ParamnPulseNum, ParamdFlySpeed);
                    if (Laserstatus == Hardware.Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                    {
                        RetrunStatus = ReturnDeviceStatus.MB_OK;
                    }
                    else
                        RetrunStatus = ReturnDeviceStatus.MB_Error;
                }
                else
                    RetrunStatus = ReturnDeviceStatus.MB_Error;
            }
            else
                RetrunStatus = ReturnDeviceStatus.NotInRange;
            return RetrunStatus;
        }
        private void Debugging_Load(object sender, EventArgs e)
        {
            Config.OpenAllPortExceptLaser();
        }

        private void Debugging_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.CloseAllPortExceptLaser();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            flgCancelTurn = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            try
            {
                if (ReseveFormarkingCounter < int.Parse(radTextBox1.Text))
                {
                    if (flgCancelTurn) timer1.Stop();
                    
                    Config.ReceiveCardForMarkingValue();
                    if (Config.CardHolder.BytesToRead >= 18)
                    {
                        Config.CardHolder.DiscardInBuffer();
                        Config.CardHolder.DiscardOutBuffer();
                        ReseveFormarkingCounter++;
                    }
                }
                else
                {
                    ReseveFormarkingCounter = 0;
                    timer1.Stop();
                }


            }
            catch (Exception)
            {
                flgCancelTurn = false;
                radTextBox1.Text = "1";
                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (MovetoRejectBoxCounter < int.Parse(radTextBox1.Text))
                {
                    if (flgCancelTurn) timer1.Stop();

                    Config.MoveToRejectBox();
                    if (Config.CardHolder.BytesToRead >= 18)
                    {
                        Config.CardHolder.DiscardInBuffer();
                        Config.CardHolder.DiscardOutBuffer();
                        MovetoRejectBoxCounter++;
                    }
                }
                else
                {
                    MovetoRejectBoxCounter = 0;
                    timer2.Stop();
                }

            }
            catch (Exception)
            {
                flgCancelTurn = false;
                radTextBox1.Text = "1";
                MessageBox.Show("دستگاه متصل نیست.لطفا ابتدا از اتصال دستگاه اطمینان حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            timer2.Stop();
        }

        private void GBCardHolder_Enter(object sender, EventArgs e)
        {

        }
    }
}
