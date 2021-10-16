using SaraPrinterLaser.bl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaraPrinterLaser
{
    public partial class MachineConfig : Form
    {
        public MachineConfig()
        {
            InitializeComponent();
        }

        private void btnConfrim_Click(object sender, EventArgs e)
        {

            string StackerCount = "";
            if (rdbOneStacker.Checked) StackerCount = "1";
            else if (rdbTwoStacker.Checked) StackerCount = "2";
            string[] MachineConfig =
            {
                cmbFristComport.SelectedItem.ToString(),
                cmbSecoundComport.SelectedItem.ToString(),
                cmbThirdComport.SelectedItem.ToString(),
                StackerCount
            };
            try
            {
                bool Status = WriteAllSecureConfig(MachineConfig);
                if (Status)
                {
                    MessageBox.Show("Information Save Successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

                else
                {
                    MessageBox.Show("Write Unsuccessful", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Write Unsuccessful", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private bool WriteAllSecureConfig(string[] MachineConfig)
        {
            bool Status = false;
            try
            {
                if (!File.Exists("MachineType.vsc"))
                    File.Create("MachineType.vsc");
                if (MachineConfig.Length != 4) return false;

                if (MachineConfig != null &&
                    !String.IsNullOrWhiteSpace(MachineConfig[0]) && !String.IsNullOrWhiteSpace(MachineConfig[1]) &&
                    !String.IsNullOrWhiteSpace(MachineConfig[2]) && !String.IsNullOrWhiteSpace(MachineConfig[3]))
                {
                    File.WriteAllLines("MachineType.vsc", MachineConfig);
                    Status = true;
                }
                else
                    Status = false;
            }
            catch (Exception)
            {

                Status = false;
            }

            return Status;
        }
    }
}
