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
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            new dl.ParametersSave().ParametersLoad(LayoutDesignFolder.LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableTopItem_Name, ref ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Refresh();
            dataGridView1.Update();
        }
    }
}
