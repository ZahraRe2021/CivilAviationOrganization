using SaraPrinterLaser.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaraPrinterLaser
{
    public partial class ConnectingDevice : Form
    {
        int timercount = 0;

        public ConnectingDevice()
        {
            InitializeComponent();
            pbxLaser.ImageLocation = "Gif\\comp-2.gif";
            lblResult.Text = "نرم افزار در حال اتصال به دستگاه می باشد";
            lblResult.BackColor = Color.White;
            lblResult.ForeColor = Color.FromArgb(36, 190, 255);
        }

        async private void ConnectingDevice_Load(object sender, EventArgs e)
        {
            timercount = 0;
            timerCheckDevice.Start();
            btnEnter.Enabled = false;
            await Task.Run(() =>
            {
                Config.LoadHardWare();
            });
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void timerCheckDevice_Tick(object sender, EventArgs e)
        {
            if (timercount < 1000)
            {
                if (!Config.NoDeviceExist)
                {
                    timercount++;
                    if (Config.CrState && Config.DispenserState && Config.laserState && Config.CardHolderState)
                    {
                        pbxLaser.ImageLocation = "Gif\\check-mark-animated-gif-4.gif";
                        lblResult.Text = "دستگاه با موفقیت متصل شد         ";
                        lblResult.BackColor = Color.White;
                        lblResult.ForeColor = Color.FromArgb(50, 188, 67);
                        timerCheckDevice.Stop();
                        Thread.Sleep(100);
                        this.DialogResult = DialogResult.OK;
                    }

                }
                else
                    timercount = 60000;
            }
            else
            {
                btnEnter.Enabled = true;
                btnEnter.Visible = true;
                pbxLaser.ImageLocation = "Gif\\gif-icons-menu-transition-animations-animated-3-dotted.gif";
                lblResult.Text = "لطفا از اتصال دستگاه اطمینان حاصل نمایید";
                lblResult.BackColor = Color.White;
                lblResult.ForeColor = Color.FromArgb(222, 22, 57);
                timercount = 0;
                timerCheckDevice.Stop();



            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://www.sarahardware.com/");
            Process.Start(sInfo);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://www.sarahardware.com/");
            Process.Start(sInfo);
        }
    }
}
