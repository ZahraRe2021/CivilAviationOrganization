using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaraPrinterLaser
{
    public partial class GetImageFromPanel : Form
    {
        string withoutpicturepath = "";
        private static PixelFormat PxFormat = PixelFormat.Format8bppIndexed;
        private static Bitmap newImage = new Bitmap(2023, 1276, PxFormat);
        public GetImageFromPanel(Control[] tmpPanel, string Savelocation)
        {

            InitializeComponent();
            withoutpicturepath = Savelocation;
            for (int i = 0; i < tmpPanel.Length; i++)
            {
                if (tmpPanel[i].GetType().ToString() == "System.Windows.Forms.Label")
                    PanelRo.Controls.Add((Label)tmpPanel[i]);

               else if (tmpPanel[i].GetType().ToString() == "CustomControl.OrientAbleTextControls.OrientedTextLabel")
                    PanelRo.Controls.Add((CustomControl.OrientAbleTextControls.OrientedTextLabel)tmpPanel[i]);

               else if (tmpPanel[i].GetType().ToString() == "DevExpress.XtraEditors.BarCodeControl")
                    PanelRo.Controls.Add((DevExpress.XtraEditors.BarCodeControl)tmpPanel[i]);
            }
            //PanelRo = tmpPanel;

        }

        private void GetImageFromPanel_Load(object sender, EventArgs e)
        {
            newImage = GetControlImage(PanelRo);
            Bitmap scale = GrayScale(newImage);
            scale = scale.Clone(new Rectangle(0, 0, PanelRo.Width, PanelRo.Height), PxFormat);
            scale.SetResolution(200, 200);
            scale.Save(withoutpicturepath, ImageFormat.Bmp);
            newImage.Dispose();
        }
        private static Bitmap GetControlImage(Control ctl)
        {
            Bitmap bm = new Bitmap(ctl.Width, ctl.Height);
            ctl.DrawToBitmap(bm, new Rectangle(0, 0, ctl.Width, ctl.Height));

            return bm;
        }
        public static Bitmap GrayScale(Bitmap Bmp)
        {
            int rgb;
            Color c;

            for (int y = 0; y < Bmp.Height; y++)
                for (int x = 0; x < Bmp.Width; x++)
                {
                    c = Bmp.GetPixel(x, y);
                    rgb = (int)((c.R + c.G + c.B) / 3);
                    Bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            return Bmp;
        }
    }
}
