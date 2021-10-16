using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SaraPrinterLaser.bl;
using SaraPrinterLaser.Hardware;
using System.Drawing.Imaging;
using System.Threading;
using System.Xml;
using System.IO;

namespace SaraPrinterLaser
{
    public partial class PrintingPageSeries : Form
    {

        #region Attribute
        string PictureFolderPath = "";
        string PictureFormat = "";
        string XmlTop = "", XmlBottom = "";
        bool flgFailFolder = false;
        int DataRowOnPrint = 0;
        int PrintMax = 0;
        job test;
        bool cractive = false;
        bool rotateactive = false;
        bool rfid = false;
        int cardholder = 1;
        string cr1 = "", cr2 = "", cr3 = "";
        string ReadFromDataBaseCr1 = "", ReadFromDataBaseCr2 = "", ReadFromDataBaseCr3 = "";
        bool ATrack1 = false;
        bool ATrack2 = false;
        bool ATrack3 = false;
        bool flgMoveToRejectBoxCorruptedMagneticCard;
        LaserPen penro;
        LaserPen penzir;

        LaserPen TopPicturePen;
        LaserPen BottomPicturePen;
        Worker workerprint;
        PrintList PrintAttrib;
        string layoutro = "";
        string layoutZir = "";
        Dictionary<string, int> dataModel = new Dictionary<string, int>();
        Dictionary<string, string> PictureModel = new Dictionary<string, string>();
        Panel TopPanel = new Panel();
        Panel BottomPanel = new Panel();
        private bool PrintCancel;
        private bool ShowOnce;

        public static string TempFolderPath = Path.GetTempPath();
        public static string PictureTopPath = TempFolderPath + "LaserPrinterImagetmp\\PictureTop.bmp";
        public static string WithoutPictureBoxPictureTopPath = TempFolderPath + "LaserPrinterImagetmp\\WithoutPictureBoxPictureTop.bmp";
        public static string WithPictureBoxPictureTopPath = TempFolderPath + "LaserPrinterImagetmp\\WithPictureBoxPictureTop.bmp";

        public static string PictureBottomPath = TempFolderPath + "LaserPrinterImagetmp\\PictureBottom.bmp";
        public static string WithoutPictureBoxPictureBottom = TempFolderPath + "LaserPrinterImagetmp\\WithoutPictureBoxPictureBottom.bmp";
        public static string WithPictureBoxPictureBottom = TempFolderPath + "LaserPrinterImagetmp\\WithPictureBoxPictureBottom.bmp";


        Bitmap newImage = new Bitmap(2023, 1276, PixelFormat.Format1bppIndexed);
        #endregion


        #region PicturePrint



        #endregion

        public PrintingPageSeries(Panel OrgTopPanel, Panel OrgBottomPanel,
                                  bool CRActive, bool RotateActive,
                                  bool Track1Active, bool Track2Active, bool Track3Active,bool _flgMoveToRejectBoxCorruptedMagneticCard,
                                  bool RFID, int CardHolder,
                                  string CRTrack1, string CRTrack2, string CRTrack3,
                                  string PenRo, string PenZir,
                                  string strTopPicturePen, string strBottomPicturePen,
                                  bl.PrintList ListPrint)

        {
            InitializeComponent();

            TopPanel = OrgTopPanel;
            BottomPanel = OrgBottomPanel;
            PrintAttrib = ListPrint;
            rfid = RFID;
            cractive = CRActive;
            rotateactive = RotateActive;
            cardholder = CardHolder;
            penro = bl.FileWork.readPen(PenRo);
            penzir = bl.FileWork.readPen(PenZir);
            flgMoveToRejectBoxCorruptedMagneticCard = _flgMoveToRejectBoxCorruptedMagneticCard;
            TopPicturePen = bl.FileWork.readPen(strTopPicturePen);
            BottomPicturePen = bl.FileWork.readPen(strBottomPicturePen);

            PictureFolderPath = ListPrint.PictureFolderPath;
            PictureFormat = ListPrint.PictureFormat;

            XmlTop = MakeXML(TopPanel).OuterXml;
            XmlBottom = MakeXML(BottomPanel).OuterXml;
            cr1 = CRTrack1;
            cr2 = CRTrack2;
            cr3 = CRTrack3;

            ATrack1 = Track1Active;
            ATrack2 = Track2Active;
            ATrack3 = Track3Active;


            string DataRead = new dl.InfoModel().InfoDataByID(PrintAttrib.DataPrintID);
            int position = 0;
            foreach (var item in DataRead.Split('ß'))
            {
                dataModel.Add(item, position);
                position++;
            }
        }



        //private void LoadPicture(string PicturePath, string PictureName, PictureBox tmp)
        //{
        //    int EntityName = 0;
        //    bool BackDis = false;
        //    bool flgNoPicExist = false;
        //    string TempFolder = Path.GetTempPath();

        //    LaserConfigClass LaserSetting = new LaserConfigClass();

        //    tmp.SizeMode = PictureBoxSizeMode.Normal;
        //    Bitmap BitmapTemp = new Bitmap(PicturePath);
        //    string Base64Bitmap = ImageToBase64(PicturePath);
        //    File.WriteAllText(TempFolder + "Base64Bitmap//" + PictureName + ".txt", EncryptionClass.EncryptionClass.Encrypt(Base64Bitmap));

        ////    float DPIX = BitmapTemp.HorizontalResolution;
        // //   float DPIY = BitmapTemp.VerticalResolution;
        //  //  double[] PictureSize = WithDPIconvertPxTomm(BitmapTemp.Size, DPIX, DPIY);
        //    tmp.Size = WithDPIConvermmtoPx(PictureSize[0], PictureSize[1], 200, 200);

        //    tmp.Image = RResizeImage(BitmapTemp, tmp.Width, tmp.Height);

        //    tmp.Name = PictureName + ".txt";

        //    if (Laser.ClearLibAllEntity() == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
        //    {

        //        string Result = Path.GetTempPath() + "Base64Bitmap\\";
        //        if (!Directory.Exists(Result))
        //            Directory.CreateDirectory(Result);
        //        if (tmp.Image != null)
        //        {

        //            if (File.Exists(TempFolder + "Base64Bitmap//" + tmp.Name))
        //            {
        //                string PicValue = EncryptionClass.EncryptionClass.Decrypt(File.ReadAllText(TempFolder + "Base64Bitmap//" + tmp.Name));

        //                double[] Location = convertPxTomm(tmp.Location);
        //                Image bmp = Base64ToImage(PicValue);
        //                string BmpName = Path.GetTempPath() + "LaserPrinterImagetmp\\" + "PictureTop_" + tmp.Name.Replace(".txt", "");



        //                if (tmp.Name.Replace(".txt", "").Contains("bmp"))
        //                    bmp.Save(BmpName, ImageFormat.Bmp);
        //                else if (tmp.Name.Replace(".txt", "").Contains("jpg"))
        //                    bmp.Save(BmpName, ImageFormat.Jpeg);
        //                else if (tmp.Name.Replace(".txt", "").Contains("jpeg"))
        //                    bmp.Save(BmpName, ImageFormat.Jpeg);
        //                else if (tmp.Name.Replace(".txt", "").Contains("png"))
        //                    bmp.Save(BmpName, ImageFormat.Png);
        //                else if (tmp.Name.Replace(".txt", "").Contains("gif"))
        //                    bmp.Save(BmpName, ImageFormat.Gif);
        //                else if (tmp.Name.Replace(".txt", "").Contains("tiff"))
        //                    bmp.Save(BmpName, ImageFormat.Tiff);
        //                else if (tmp.Name.Replace(".txt", "").Contains("tif"))
        //                    bmp.Save(BmpName, ImageFormat.Tiff);



        //                float DDPIX = bmp.HorizontalResolution;
        //                float DDPIY = bmp.VerticalResolution;
        //                double[] PPictureSize = WithDPIconvertPxTomm(bmp.Size, DDPIX, DDPIY);
        //                //                                double NewYLocation = Location[1]; //+ (PictureSize[1]) + (PictureSize[1] / 2);
        //                LaserSetting = LaserConfigClass.load();
        //                double NewCentreLocationX = Location[0] - 42.8 + LaserSetting.Xcenter;// - 43;

        //                double NewCentreLocationY = -Location[1] + LaserSetting.Ycenter + 1.6;// (54 - NewYLocation) - 27;
        //                if (Laser.SetPenParam(3, 1, 300, 4, 1, 35000, 10, 20, 100, 300, 0, 2000, 500, 100, 0, 0.01, 0.100, false, 1, 0) == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
        //                {
        //                    Laser.LmcErrCode MM;
        //                    MM = Laser.AddFileToLib(BmpName, "Picture" + EntityName.ToString(), NewCentreLocationX, NewCentreLocationY, 0, 0, 1, 3, false);
        //                    if (MM == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
        //                    {
        //                        if (Laser.SetBitmapEntParam("Picture" + EntityName.ToString(), BmpName, 0, 0, 0, 0, 0, 600, false, 0) == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
        //                            EntityName++;
        //                        else
        //                        {
        //                            MessageBox.Show("لطفا از اتصال دستگاه اطمینان حاصل نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                            flgNoPicExist = true;

        //                        }
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("لطفا از اتصال دستگاه اطمینان حاصل نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                        flgNoPicExist = true;

        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("لطفا از اتصال دستگاه اطمینان حاصل نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    flgNoPicExist = true;

        //                }
        //            }


        //        }
        //        else
        //        {
        //            flgNoPicExist = true;
        //            MessageBox.Show("لطفا تصویر مورد نظر را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }


        //    }
        //    else
        //    {
        //        MessageBox.Show("لطفا از اتصال دستگاه اطمینان حاصل نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        flgNoPicExist = true;

        //    }

        //    if (!flgNoPicExist)
        //    {
        //        Laser.SaveEntLibToFile(Path.GetTempPath() + "LaserPrinterImagetmp\\PictureTop.ezd");

        //        if (!BackDis)
        //            EnableBackgroundImage();
        //        this.Cursor = Cursors.Arrow;
        //        SinglePrint Sprint = new SinglePrint();
        //        Sprint.ShowDialog();
        //    }






        //}
        private void PrintingPageSeries_Load(object sender, EventArgs e)
        {

                //TopPanel = PaintForm.CovertPanelToBitmap.loadXMLFILE(XmlTop);
                //BottomPanel = PaintForm.CovertPanelToBitmap.loadXMLFILE(XmlBottom);
                PrintMax = 0;
                if (PrintAttrib.FixIncrimentActive)
                {
                    if (PrintAttrib.startIncriment >= PrintAttrib.EndIncriment)
                    {
                        MessageBox.Show("اعداد به درستی انتخاب نشده ");
                        this.Close();
                    }
                    else
                        PrintMax = PrintAttrib.EndIncriment - PrintAttrib.startIncriment;
                }
                else
                {
                    if (PrintAttrib.SelectData)
                    {
                        int datacount = new dl.InfoData().CountDataByInfoModelID(PrintAttrib.DataPrintID);
                        if (PrintMax < datacount)
                        {
                            PrintMax = datacount;
                        }
                        if (PrintMax == 0)
                        {
                            MessageBox.Show("اطلاعات برای چاپ وجود ندارد.");
                            this.Close();
                        }
                        else
                        {
                            PbarCount.Maximum = PrintMax;
                            PbarCount.Value2 = 1;
                            PbarCount.Text = string.Format("0/{0}", PrintMax);
                        }
                    }
                }
                #region Prepare To Frist Print  
                string[] dataget = new string[0];
                bool NoPrintAvabile = false;
                if (PrintAttrib.SelectData)
                {
                    DataSet dset = new dl.InfoData().FirstPrint(PrintAttrib.DataPrintID);
                    try
                    {
                        DataRowOnPrint = Convert.ToInt32(dset.Tables[0].Rows[0]["ID"]);
                        dataget = dset.Tables[0].Rows[0]["data"].ToString().Split('ß');
                    }
                    catch (Exception)
                    {
                        NoPrintAvabile = true;
                        MessageBox.Show("اطلاعاتی برای چاپ وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }

                    if (!NoPrintAvabile)
                    {
                        if (cractive)
                        {
                            ReadFromDataBaseCr1 = "";
                            ReadFromDataBaseCr2 = "";
                            ReadFromDataBaseCr3 = "";
                            int CRanswer = 0;
                            if (ATrack1)
                            {
                                bool getNumberCR1 = dataModel.TryGetValue(cr1, out CRanswer);
                                if (getNumberCR1)
                                    ReadFromDataBaseCr1 = dataget[CRanswer];
                            }
                            if (ATrack2)
                            {
                                bool getNumberCR2 = dataModel.TryGetValue(cr2, out CRanswer);
                                if (getNumberCR2)
                                    ReadFromDataBaseCr2 = dataget[CRanswer];
                            }
                            if (ATrack3)
                            {
                                bool getNumberCR3 = dataModel.TryGetValue(cr3, out CRanswer);
                                if (getNumberCR3)
                                    ReadFromDataBaseCr3 = dataget[CRanswer];
                            }
                        }



                        foreach (var item in PrintAttrib.Selection)
                        {

                            if (PrintAttrib.FixIncrimentActive)
                            {
                                string tempShomarande = PrintAttrib.FixIncriment;
                                int ziroNeed = PrintAttrib.EndIncriment.ToString().Length - PrintAttrib.startIncriment.ToString().Length;
                                for (int i = 0; i < ziroNeed; i++)
                                {
                                    tempShomarande += "0";

                                }
                                tempShomarande += PrintAttrib.startIncriment.ToString();

                                if (item.Key.Contains("Lbl_"))
                                {
                                    Label LBL = new Label();
                                    if (item.Key.Contains("روی کارت"))
                                    {
                                        string[] fields = item.Key.Split('=');
                                        string CntrlName = "";
                                        foreach (string ITem in fields)
                                        {
                                            if (ITem.Contains("Lbl_"))
                                                CntrlName = ITem;
                                        }
                                        foreach (Control ITEM in TopPanel.Controls)
                                        {
                                            if (ITEM.Name == CntrlName)
                                                LBL = (Label)ITEM;
                                        }
                                        if (LBL.Name == CntrlName)
                                            LBL.Text = tempShomarande;
                                    }
                                    else if (item.Key.Contains("زیر کارت"))
                                    {
                                        string[] fields = item.Key.Split('=');
                                        string CntrlName = "";
                                        foreach (string ITem in fields)
                                        {
                                            if (ITem.Contains("Lbl_"))
                                                CntrlName = ITem;
                                        }
                                        foreach (Control ITEM in BottomPanel.Controls)
                                        {
                                            if (ITEM.Name == CntrlName)
                                                LBL = (Label)ITEM;
                                        }
                                        if (LBL.Name == CntrlName)
                                            LBL.Text = tempShomarande;
                                    }
                                }
                                else if (item.Key.Contains("Otxt_"))
                                {
                                    CustomControl.OrientAbleTextControls.OrientedTextLabel OText = new CustomControl.OrientAbleTextControls.OrientedTextLabel();
                                    if (item.Key.Contains("روی کارت"))
                                    {
                                        string[] fields = item.Key.Split('=');
                                        string CntrlName = "";
                                        foreach (string ITem in fields)
                                        {
                                            if (ITem.Contains("Otxt_"))
                                                CntrlName = ITem;
                                        }
                                        foreach (Control ITEM in TopPanel.Controls)
                                        {
                                            if (ITEM.Name == CntrlName)
                                                OText = (CustomControl.OrientAbleTextControls.OrientedTextLabel)ITEM;
                                        }
                                        if (OText.Name == CntrlName)
                                            OText.Text = tempShomarande;
                                    }
                                    else if (item.Key.Contains("زیر کارت"))
                                    {
                                        string[] fields = item.Key.Split('=');
                                        string CntrlName = "";
                                        foreach (string ITem in fields)
                                        {
                                            if (ITem.Contains("Otxt_"))
                                                CntrlName = ITem;
                                        }
                                        foreach (Control ITEM in BottomPanel.Controls)
                                        {
                                            if (ITEM.Name == CntrlName)
                                                OText = (CustomControl.OrientAbleTextControls.OrientedTextLabel)ITEM;
                                        }
                                        if (OText.Name == CntrlName)
                                            OText.Text = tempShomarande;
                                    }
                                }
                            }
                            else
                            {
                                int answerposition = 0;
                                bool ishere = dataModel.TryGetValue(item.Value, out answerposition);
                                if (ishere)
                                {
                                    if (item.Key.Contains("عکس"))
                                    {
                                        PictureBox PICBX = new PictureBox();
                                        if (item.Key.Contains("روی کارت"))
                                        {
                                            string[] fields = item.Key.Split('=');
                                            string CntrlName = "";
                                            foreach (string ITem in fields)
                                            {
                                                if (!ITem.Contains("عکس"))
                                                    CntrlName = ITem;
                                            }
                                            foreach (Control ITEM in TopPanel.Controls)
                                            {
                                                if (ITEM.Name == CntrlName)
                                                    PICBX = (PictureBox)ITEM;
                                            }
                                            if (PICBX.Name == CntrlName)
                                            {




                                                try
                                                {
                                                    PICBX.SizeMode = PictureBoxSizeMode.Normal;
                                                    string PathAdddresss = PictureFolderPath + "\\" + dataget[answerposition] + PictureFormat;
                                                    PictureModel.Add(PICBX.Name, PathAdddresss);
                                                    Bitmap bmp = new Bitmap(PathAdddresss);

                                                    float DPIX = bmp.HorizontalResolution;
                                                    float DPIY = bmp.VerticalResolution;
                                                    double[] PictureSize = WithDPIconvertPxTomm(bmp.Size, DPIX, DPIY);
                                                    PICBX.Size = WithDPIConvermmtoPx(PictureSize[0], PictureSize[1], 200, 200);

                                                    PICBX.Image = RResizeImage(bmp, PICBX.Width, PICBX.Height);
                                                }
                                                catch (Exception)
                                                {

                                                    MessageBox.Show("لطفا در انتخاب پوشه عکس دقت فرمایید یا اینکه فایل عکسهای وارد شده صحیح نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    flgFailFolder = true;
                                                    break;

                                                }




                                                //  PICBX.ImageLocation = dataget[answerposition];
                                            }
                                        }
                                        else if (item.Key.Contains("زیر کارت"))
                                        {
                                            string[] fields = item.Key.Split('=');
                                            string CntrlName = "";
                                            foreach (string ITem in fields)
                                            {
                                                if (ITem.Contains("PB_"))
                                                    CntrlName = ITem;
                                            }
                                            foreach (Control ITEM in BottomPanel.Controls)
                                            {
                                                if (ITEM.Name == CntrlName)
                                                    PICBX = (PictureBox)ITEM;
                                            }
                                            if (PICBX.Name == CntrlName)
                                                PICBX.ImageLocation = dataget[answerposition];
                                        }
                                    }
                                    else if (item.Key.Contains("Lbl_"))
                                    {
                                        Label LBL = new Label();
                                        if (item.Key.Contains("روی کارت"))
                                        {
                                            string[] fields = item.Key.Split('=');
                                            string CntrlName = "";
                                            foreach (string ITem in fields)
                                            {
                                                if (ITem.Contains("Lbl_"))
                                                    CntrlName = ITem;
                                            }
                                            foreach (Control ITEM in TopPanel.Controls)
                                            {
                                                if (ITEM.Name == CntrlName)
                                                    LBL = (Label)ITEM;
                                            }
                                            if (LBL.Name == CntrlName)
                                                LBL.Text = dataget[answerposition];
                                        }
                                        else if (item.Key.Contains("زیر کارت"))
                                        {
                                            string[] fields = item.Key.Split('=');
                                            string CntrlName = "";
                                            foreach (string ITem in fields)
                                            {
                                                if (ITem.Contains("Lbl_"))
                                                    CntrlName = ITem;
                                            }
                                            foreach (Control ITEM in BottomPanel.Controls)
                                            {
                                                if (ITEM.Name == CntrlName)
                                                    LBL = (Label)ITEM;
                                            }
                                            if (LBL.Name == CntrlName)
                                                LBL.Text = dataget[answerposition];
                                        }
                                    }
                                    else if (item.Key.Contains("BC_"))
                                    {
                                        DevExpress.XtraEditors.BarCodeControl Barcode = new DevExpress.XtraEditors.BarCodeControl();
                                        if (item.Key.Contains("روی کارت"))
                                        {
                                            string[] fields = item.Key.Split('=');
                                            string CntrlName = "";
                                            foreach (string ITem in fields)
                                            {
                                                if (ITem.Contains("BC_"))
                                                    CntrlName = ITem;
                                            }
                                            foreach (Control ITEM in TopPanel.Controls)
                                            {
                                                if (ITEM.Name == CntrlName)
                                                    Barcode = (DevExpress.XtraEditors.BarCodeControl)ITEM;
                                            }
                                            if (Barcode.Name == CntrlName)
                                                Barcode.Text = dataget[answerposition];
                                        }
                                        else if (item.Key.Contains("زیر کارت"))
                                        {
                                            string[] fields = item.Key.Split('=');
                                            string CntrlName = "";
                                            foreach (string ITem in fields)
                                            {
                                                if (ITem.Contains("BC_"))
                                                    CntrlName = ITem;
                                            }
                                            foreach (Control ITEM in BottomPanel.Controls)
                                            {
                                                if (ITEM.Name == CntrlName)
                                                    Barcode = (DevExpress.XtraEditors.BarCodeControl)ITEM;
                                            }
                                            if (Barcode.Name == CntrlName)
                                                Barcode.Text = dataget[answerposition];
                                        }
                                    }
                                    else if (item.Key.Contains("Otxt_"))
                                    {
                                        CustomControl.OrientAbleTextControls.OrientedTextLabel OText = new CustomControl.OrientAbleTextControls.OrientedTextLabel();
                                        if (item.Key.Contains("روی کارت"))
                                        {
                                            string[] fields = item.Key.Split('=');
                                            string CntrlName = "";
                                            foreach (string ITem in fields)
                                            {
                                                if (ITem.Contains("Otxt_"))
                                                    CntrlName = ITem;
                                            }
                                            foreach (Control ITEM in TopPanel.Controls)
                                            {
                                                if (ITEM.Name == CntrlName)
                                                    OText = (CustomControl.OrientAbleTextControls.OrientedTextLabel)ITEM;
                                            }
                                            if (OText.Name == CntrlName)
                                                OText.Text = dataget[answerposition];
                                        }
                                        else if (item.Key.Contains("زیر کارت"))
                                        {
                                            string[] fields = item.Key.Split('=');
                                            string CntrlName = "";
                                            foreach (string ITem in fields)
                                            {
                                                if (ITem.Contains("Otxt_"))
                                                    CntrlName = ITem;
                                            }
                                            foreach (Control ITEM in BottomPanel.Controls)
                                            {
                                                if (ITEM.Name == CntrlName)
                                                    OText = (CustomControl.OrientAbleTextControls.OrientedTextLabel)ITEM;
                                            }
                                            if (OText.Name == CntrlName)
                                                OText.Text = dataget[answerposition];
                                        }
                                    }
                                }
                            }
                        }
                        if (!flgFailFolder)
                        {
                            if (PictureModel.Count != 0)
                            {
                                putPicturesToEZD(TopPanel, PictureModel);
                                PictureModel.Clear();
                            }

                            var bmpTop = SplitPictureControl(TopPanel);
                            ExportPicture(TopPanel).Save(PictureTopPath, ImageFormat.Bmp);
                            bmpTop[0].Save(WithoutPictureBoxPictureTopPath, ImageFormat.Bmp);
                            bmpTop[1].Save(WithPictureBoxPictureTopPath, ImageFormat.Bmp);
                            bmpTop[0].Dispose();
                            bmpTop[1].Dispose();

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
                                {
                                    if (cardholder == 1)
                                    {
                                        if (rotateactive && Config.RotateState)
                                        {
                                            if (cractive && Config.RFIDState)
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
                                            if (cractive && Config.RFIDState)
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
                                    FileWork.ClearAnswer();
                                    test.checkRFid = rfid;
                                    Worker.newjob = true;
                                    Worker.myjob = test;
                                    Worker.Active = true;
                                    Worker.myjob.done = new string[0] { };
                                    Worker.Error = new string[0];
                                    Worker.myjob.PenRo = penro;
                                    Worker.myjob.PenZir = penzir;
                                    Worker.myjob.jobTopPicturePen = TopPicturePen;
                                    Worker.myjob.jobBottomPicturePen = BottomPicturePen;
                                    Worker.myjob.Status = job.StatusList.startPrint;
                                    Worker.crTrack[0] = ReadFromDataBaseCr1;
                                    Worker.crTrack[1] = ReadFromDataBaseCr2;
                                    Worker.crTrack[2] = ReadFromDataBaseCr3;

                                    Worker.flgWitchTrackWrite[0] = ATrack1;
                                    Worker.flgWitchTrackWrite[1] = ATrack2;
                                    Worker.flgWitchTrackWrite[2] = ATrack3;
                                Worker.FlgMoveToRejectBoxCorruptedMagneticCard = flgMoveToRejectBoxCorruptedMagneticCard;
                                workerprint = new Worker();
                                    PbarPrint.Minimum = 0;
                                    PbarPrint.Value1 = 0;
                                    PbarPrint.Maximum = Worker.myjob.WorkList.Length;
                                    PbarPrint.Text = string.Format("{0}/{1}", PbarPrint.Value1, PbarPrint.Maximum);
                                    for (int i = 0; i < Worker.myjob.WorkList.Length; i++)
                                    {
                                        Label lbl = new Label();
                                        lbl.Height = 35;
                                        lbl.Text = Worker.myjob.WorkList[i];
                                        flpProcessName.Controls.Add(lbl);
                                    }
                                    workerprint.main.Start();
                                    timerStatus.Start();
                                }
                            EndMission:
                                if (Counter == 0)
                                {
                                    MessageBox.Show("به دلیل وجود داشتن کارت در خروجی چاپ انجام نشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    this.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("لظفا از اتصال دستگاه اطمینان حاصل نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                        else
                            this.Close();
                    }
                    else
                        this.Close();
                }
                else
                {
                    MessageBox.Show("اطلاعاتی برای چاپ وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                #endregion
            
        }
        private void timerStatus_Tick(object sender, EventArgs e)
        {
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
                                    bl.FileWork.changeState(bl.FileWork.StateOfmashin.Ready);
                                    bl.FileWork.ClearAnswer();
                            //        new dl.InfoData().FirstPrintSave(DataRowOnPrint);
                                    TimerCreatePicture.Start();
                                   
                                    timerStatus.Stop();

                                }
                            }
                        }
                        else
                        {
                            timerStatus.Stop();
                            TimerCreatePicture.Stop();
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
                        bl.FileWork.ClearAnswer();
                        Worker.Error = new string[0];
                        MessageBox.Show("لطفا کارت در مخزن قرار دهید","اعلام",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
                    break;
                default:
                    break;
            }
        }
        private void TimerCreatePicture_Tick(object sender, EventArgs e)
        {
            PrintMax--;
            if (PrintMax > 0)
            {
                if (PbarCount.Value2 < PbarCount.Maximum)
                {
                    PbarCount.Value1++;
                    PbarCount.Value2++;
                    PbarCount.Text = string.Format("{0}/{1}", PbarCount.Value1, PbarCount.Maximum);
                    PbarPrint.Value1 = 0;
                    PbarPrint.Value2 = 0;

                    if (PrintAttrib.FixIncrimentActive)
                    {
                        PrintAttrib.startIncriment++;

                    }
                    else //(PrintAttrib.SelectData)
                    {
                        new dl.InfoData().FirstPrintSave(DataRowOnPrint);

                    }



                    bool noExsitPrint = false;
                    string[] dataget = new string[0];
                    if (PrintAttrib.SelectData)
                    {
                        DataSet dset = new dl.InfoData().FirstPrint(PrintAttrib.DataPrintID);
                        try
                        {
                            DataRowOnPrint = Convert.ToInt32(dset.Tables[0].Rows[0]["ID"]);
                            dataget = dset.Tables[0].Rows[0]["data"].ToString().Split('ß');
                        }
                        catch (Exception)
                        {
                            noExsitPrint = true;
                            PrintMax = 0;

                        }


                            if (cractive)
                            {
                                ReadFromDataBaseCr1 = "";
                                ReadFromDataBaseCr2 = "";
                                ReadFromDataBaseCr3 = "";
                                int CRanswer = 0;
                                if (ATrack1)
                                {
                                    bool getNumberCR1 = dataModel.TryGetValue(cr1, out CRanswer);
                                    if (getNumberCR1)
                                        ReadFromDataBaseCr1 = dataget[CRanswer];
                                }
                                if (ATrack2)
                                {
                                    bool getNumberCR2 = dataModel.TryGetValue(cr2, out CRanswer);
                                    if (getNumberCR2)
                                        ReadFromDataBaseCr2 = dataget[CRanswer];
                                }
                                if (ATrack3)
                                {
                                    bool getNumberCR3 = dataModel.TryGetValue(cr3, out CRanswer);
                                    if (getNumberCR3)
                                        ReadFromDataBaseCr3 = dataget[CRanswer];
                                }
                            }

                        

                        foreach (var item in PrintAttrib.Selection)
                        {
                            flgFailFolder = false;
                            if (PrintAttrib.FixIncrimentActive)
                            {
                                string tempShomarande = PrintAttrib.FixIncriment;
                                int ziroNeed = PrintAttrib.EndIncriment.ToString().Length - PrintAttrib.startIncriment.ToString().Length;
                                for (int i = 0; i < ziroNeed; i++)
                                {
                                    tempShomarande += "0";

                                }
                                tempShomarande += PrintAttrib.startIncriment.ToString();

                                if (item.Key.Contains("Lbl_"))
                                {
                                    Label LBL = new Label();
                                    if (item.Key.Contains("روی کارت"))
                                    {
                                        string[] fields = item.Key.Split('=');
                                        string CntrlName = "";
                                        foreach (string ITem in fields)
                                        {
                                            if (ITem.Contains("Lbl_"))
                                                CntrlName = ITem;
                                        }
                                        foreach (Control ITEM in TopPanel.Controls)
                                        {
                                            if (ITEM.Name == CntrlName)
                                                LBL = (Label)ITEM;
                                        }
                                        if (LBL.Name == CntrlName)
                                            LBL.Text = tempShomarande;
                                    }
                                    else if (item.Key.Contains("زیر کارت"))
                                    {
                                        string[] fields = item.Key.Split('=');
                                        string CntrlName = "";
                                        foreach (string ITem in fields)
                                        {
                                            if (ITem.Contains("Lbl_"))
                                                CntrlName = ITem;
                                        }
                                        foreach (Control ITEM in BottomPanel.Controls)
                                        {
                                            if (ITEM.Name == CntrlName)
                                                LBL = (Label)ITEM;
                                        }
                                        if (LBL.Name == CntrlName)
                                            LBL.Text = tempShomarande;
                                    }
                                }
                                else if (item.Key.Contains("Otxt_"))
                                {
                                    CustomControl.OrientAbleTextControls.OrientedTextLabel OText = new CustomControl.OrientAbleTextControls.OrientedTextLabel();
                                    if (item.Key.Contains("روی کارت"))
                                    {
                                        string[] fields = item.Key.Split('=');
                                        string CntrlName = "";
                                        foreach (string ITem in fields)
                                        {
                                            if (ITem.Contains("Otxt_"))
                                                CntrlName = ITem;
                                        }
                                        foreach (Control ITEM in TopPanel.Controls)
                                        {
                                            if (ITEM.Name == CntrlName)
                                                OText = (CustomControl.OrientAbleTextControls.OrientedTextLabel)ITEM;
                                        }
                                        if (OText.Name == CntrlName)
                                            OText.Text = tempShomarande;
                                    }
                                    else if (item.Key.Contains("زیر کارت"))
                                    {
                                        string[] fields = item.Key.Split('=');
                                        string CntrlName = "";
                                        foreach (string ITem in fields)
                                        {
                                            if (ITem.Contains("Otxt_"))
                                                CntrlName = ITem;
                                        }
                                        foreach (Control ITEM in BottomPanel.Controls)
                                        {
                                            if (ITEM.Name == CntrlName)
                                                OText = (CustomControl.OrientAbleTextControls.OrientedTextLabel)ITEM;
                                        }
                                        if (OText.Name == CntrlName)
                                            OText.Text = tempShomarande;
                                    }
                                }
                            }
                            else
                            {
                                int answerposition = 0;
                                bool ishere = dataModel.TryGetValue(item.Value, out answerposition);
                                if (ishere)
                                {
                                    if (item.Key.Contains("عکس"))
                                    {
                                        PictureBox PICBX = new PictureBox();
                                        if (item.Key.Contains("روی کارت"))
                                        {
                                            string[] fields = item.Key.Split('=');
                                            string CntrlName = "";
                                            foreach (string ITem in fields)
                                            {
                                                if (!ITem.Contains("عکس"))
                                                    CntrlName = ITem;
                                            }
                                            foreach (Control ITEM in TopPanel.Controls)
                                            {
                                                if (ITEM.Name == CntrlName)
                                                    PICBX = (PictureBox)ITEM;
                                            }
                                            if (PICBX.Name == CntrlName)
                                            {




                                                try
                                                {
                                                    PICBX.SizeMode = PictureBoxSizeMode.Normal;
                                                    string PathAdddresss = PictureFolderPath + "\\" + dataget[answerposition] + PictureFormat;
                                                    PictureModel.Add(PICBX.Name, PathAdddresss);
                                                    Bitmap bmp = new Bitmap(PathAdddresss);

                                                    float DPIX = bmp.HorizontalResolution;
                                                    float DPIY = bmp.VerticalResolution;
                                                    double[] PictureSize = WithDPIconvertPxTomm(bmp.Size, DPIX, DPIY);
                                                    PICBX.Size = WithDPIConvermmtoPx(PictureSize[0], PictureSize[1], 200, 200);

                                                    PICBX.Image = RResizeImage(bmp, PICBX.Width, PICBX.Height);
                                                }
                                                catch (Exception)
                                                {

                                                    MessageBox.Show("لطفا در انتخاب پوشه عکس دقت فرمایید یا اینکه فایل عکسهای وارد شده صحیح نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    flgFailFolder = true;
                                                    break;

                                                }




                                                //PictureModel.Add(PICBX.Name, dataget[answerposition]);




                                                //PICBX.SizeMode = PictureBoxSizeMode.Normal;
                                                //Bitmap bmp = new Bitmap(dataget[answerposition]);


                                                //float DPIX = bmp.HorizontalResolution;
                                                //float DPIY = bmp.VerticalResolution;
                                                //double[] PictureSize = WithDPIconvertPxTomm(bmp.Size, DPIX, DPIY);
                                                //PICBX.Size = WithDPIConvermmtoPx(PictureSize[0], PictureSize[1], 200, 200);

                                                //PICBX.Image = RResizeImage(bmp, PICBX.Width, PICBX.Height);

                                                /*
                                                 * 

                                                 اینجا عکس باید پیاده شود
                                                 MBA_PIC

                                                 */


                                                //  PICBX.ImageLocation = dataget[answerposition];
                                            }
                                        }
                                        else if (item.Key.Contains("زیر کارت"))
                                        {
                                            string[] fields = item.Key.Split('=');
                                            string CntrlName = "";
                                            foreach (string ITem in fields)
                                            {
                                                if (ITem.Contains("PB_"))
                                                    CntrlName = ITem;
                                            }
                                            foreach (Control ITEM in BottomPanel.Controls)
                                            {
                                                if (ITEM.Name == CntrlName)
                                                    PICBX = (PictureBox)ITEM;
                                            }
                                            if (PICBX.Name == CntrlName)
                                                PICBX.ImageLocation = dataget[answerposition];
                                        }
                                    }
                                    else if (item.Key.Contains("Lbl_"))
                                    {
                                        Label LBL = new Label();
                                        if (item.Key.Contains("روی کارت"))
                                        {
                                            string[] fields = item.Key.Split('=');
                                            string CntrlName = "";
                                            foreach (string ITem in fields)
                                            {
                                                if (ITem.Contains("Lbl_"))
                                                    CntrlName = ITem;
                                            }
                                            foreach (Control ITEM in TopPanel.Controls)
                                            {
                                                if (ITEM.Name == CntrlName)
                                                    LBL = (Label)ITEM;
                                            }
                                            if (LBL.Name == CntrlName)
                                            {
                                                try
                                                {
                                                    LBL.Text = dataget[answerposition];
                                                }
                                                catch (Exception)
                                                {
                                                }
                                            }
                                        }
                                        else if (item.Key.Contains("زیر کارت"))
                                        {
                                            string[] fields = item.Key.Split('=');
                                            string CntrlName = "";
                                            foreach (string ITem in fields)
                                            {
                                                if (ITem.Contains("Lbl_"))
                                                    CntrlName = ITem;
                                            }
                                            foreach (Control ITEM in BottomPanel.Controls)
                                            {
                                                if (ITEM.Name == CntrlName)
                                                    LBL = (Label)ITEM;
                                            }
                                            if (LBL.Name == CntrlName)
                                                LBL.Text = dataget[answerposition];
                                        }
                                    }
                                    else if (item.Key.Contains("BC_"))
                                    {
                                        DevExpress.XtraEditors.BarCodeControl Barcode = new DevExpress.XtraEditors.BarCodeControl();
                                        if (item.Key.Contains("روی کارت"))
                                        {
                                            string[] fields = item.Key.Split('=');
                                            string CntrlName = "";
                                            foreach (string ITem in fields)
                                            {
                                                if (ITem.Contains("BC_"))
                                                    CntrlName = ITem;
                                            }
                                            foreach (Control ITEM in TopPanel.Controls)
                                            {
                                                if (ITEM.Name == CntrlName)
                                                    Barcode = (DevExpress.XtraEditors.BarCodeControl)ITEM;
                                            }
                                            if (Barcode.Name == CntrlName)
                                                Barcode.Text = dataget[answerposition];
                                        }
                                        else if (item.Key.Contains("زیر کارت"))
                                        {
                                            string[] fields = item.Key.Split('=');
                                            string CntrlName = "";
                                            foreach (string ITem in fields)
                                            {
                                                if (ITem.Contains("BC_"))
                                                    CntrlName = ITem;
                                            }
                                            foreach (Control ITEM in BottomPanel.Controls)
                                            {
                                                if (ITEM.Name == CntrlName)
                                                    Barcode = (DevExpress.XtraEditors.BarCodeControl)ITEM;
                                            }
                                            if (Barcode.Name == CntrlName)
                                                Barcode.Text = dataget[answerposition];
                                        }
                                    }
                                    else if (item.Key.Contains("Otxt_"))
                                    {
                                        CustomControl.OrientAbleTextControls.OrientedTextLabel OText = new CustomControl.OrientAbleTextControls.OrientedTextLabel();
                                        if (item.Key.Contains("روی کارت"))
                                        {
                                            string[] fields = item.Key.Split('=');
                                            string CntrlName = "";
                                            foreach (string ITem in fields)
                                            {
                                                if (ITem.Contains("Otxt_"))
                                                    CntrlName = ITem;
                                            }
                                            foreach (Control ITEM in TopPanel.Controls)
                                            {
                                                if (ITEM.Name == CntrlName)
                                                    OText = (CustomControl.OrientAbleTextControls.OrientedTextLabel)ITEM;
                                            }
                                            if (OText.Name == CntrlName)
                                                OText.Text = dataget[answerposition];
                                        }
                                        else if (item.Key.Contains("زیر کارت"))
                                        {
                                            string[] fields = item.Key.Split('=');
                                            string CntrlName = "";
                                            foreach (string ITem in fields)
                                            {
                                                if (ITem.Contains("Otxt_"))
                                                    CntrlName = ITem;
                                            }
                                            foreach (Control ITEM in BottomPanel.Controls)
                                            {
                                                if (ITEM.Name == CntrlName)
                                                    OText = (CustomControl.OrientAbleTextControls.OrientedTextLabel)ITEM;
                                            }
                                            if (OText.Name == CntrlName)
                                                OText.Text = dataget[answerposition];
                                        }
                                    }
                                }
                            }
                        }



                        if (!flgFailFolder)
                        {
                            if (PictureModel.Count != 0)
                            {
                                putPicturesToEZD(TopPanel, PictureModel);
                                PictureModel.Clear();
                            }
                            var bmpTop = SplitPictureControl(TopPanel);
                            ExportPicture(TopPanel).Save(PictureTopPath, ImageFormat.Bmp);
                            bmpTop[0].Save(WithoutPictureBoxPictureTopPath, ImageFormat.Bmp);
                            bmpTop[1].Save(WithPictureBoxPictureTopPath, ImageFormat.Bmp);
                            bmpTop[0].Dispose();
                            bmpTop[1].Dispose();

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
                                {
                                    if (cardholder == 1)
                                    {
                                        if (rotateactive && Config.RotateState)
                                        {
                                            if (cractive && Config.RFIDState)
                                                test = new job(job.jobModel.PrintCard1);
                                            else
                                                test = new job(job.jobModel.PrintCard1_c);
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
                                            if (cractive && Config.RFIDState)
                                                test = new job(job.jobModel.PrintCard2);
                                            else
                                                test = new job(job.jobModel.PrintCard2_c);
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
                                    FileWork.ClearAnswer();
                                    test.checkRFid = rfid;
                                    Worker.newjob = true;
                                    Worker.myjob = test;
                                    Worker.Active = true;
                                    Worker.myjob.done = new string[0] { };
                                    Worker.Error = new string[0];
                                    Worker.myjob.PenRo = penro;
                                    Worker.myjob.PenZir = penzir;
                                    Worker.myjob.jobTopPicturePen = TopPicturePen;
                                    Worker.myjob.jobBottomPicturePen = BottomPicturePen;
                                    Worker.myjob.Status = job.StatusList.startPrint;

                                    Worker.crTrack[0] = ReadFromDataBaseCr1;
                                    Worker.crTrack[1] = ReadFromDataBaseCr2;
                                    Worker.crTrack[2] = ReadFromDataBaseCr3;
                                    Worker.FlgMoveToRejectBoxCorruptedMagneticCard = flgMoveToRejectBoxCorruptedMagneticCard;

                                    Worker.flgWitchTrackWrite[0] = ATrack1;
                                    Worker.flgWitchTrackWrite[1] = ATrack2;
                                    Worker.flgWitchTrackWrite[2] = ATrack3;

                                    workerprint = new Worker();
                                    PbarPrint.Minimum = 0;
                                    PbarPrint.Value1 = 0;
                                    PbarPrint.Maximum = Worker.myjob.WorkList.Length;
                                    PbarPrint.Text = string.Format("{0}/{1}", PbarPrint.Value1, PbarPrint.Maximum);
                                    for (int i = 0; i < Worker.myjob.WorkList.Length; i++)
                                    {
                                        Label lbl = new Label();
                                        lbl.Height = 35;
                                        lbl.Text = Worker.myjob.WorkList[i];
                                        flpProcessName.Controls.Add(lbl);
                                    }
                                    workerprint.main.Start();
                                    flpProcessTik.Controls.Clear();
                                    TimerCreatePicture.Stop();
                                    timerStatus.Start();
                                }
                            EndMission:
                                if (Counter == 0)
                                {
                                    MessageBox.Show("به دلیل وجود داشتن کارت در خروجی چاپ انجام نشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    this.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("لظفا از اتصال دستگاه اطمینان حاصل نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }


                    }



                }

                TimerCreatePicture.Stop();
            }
            else
            {
            EndPrint:
                if (PrintAttrib.SelectData)
                {

                    new dl.InfoData().FirstPrintSave(DataRowOnPrint);

                }
                PbarCount.Value1 = PbarCount.Maximum;
                PbarCount.Text = string.Format("{0}/{1}  فرایند چاپ کامل شد", PbarCount.Maximum, PbarCount.Maximum);
                TimerCreatePicture.Stop();
                MessageBox.Show("چاپ با موفقیت به پایان رسید", "اعلام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOk.Enabled = true;
                this.Close();

            }
        }
        public static XmlDocument MakeXML(Panel PanelControl)
        {
            //Control Variable Definition
            DevExpress.XtraEditors.BarCodeControl BarcodeControl = new DevExpress.XtraEditors.BarCodeControl();
            CustomControl.OrientAbleTextControls.OrientedTextLabel OrientedTextLabel = new CustomControl.OrientAbleTextControls.OrientedTextLabel();
            PictureBox PictureBoxControl = new PictureBox();
            Label LabelControl = new Label();
            bool flgSomethingWorng = false;
            // Write down the XML declaration
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);

            // Create the root element
            XmlElement rootNode = xmlDoc.CreateElement("SaraHardwareCompanyLaserPrinter");
            xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);
            xmlDoc.AppendChild(rootNode);

            //----------------------PropertyVariable-------------------------------------------
            try
            {
                #region PropertyVariable
                foreach (Control p in PanelControl.Controls)
                {
                    string Names = p.Name;//OK
                    string types = p.GetType().ToString();//OK
                    string LocationX = p.Location.X.ToString();//OK
                    string LocationY = p.Location.Y.ToString();//OK
                    string sizeWidth = p.Width.ToString();//OK
                    string sizeHegiht = p.Height.ToString();//OK
                    string Texts = p.Text.ToString();//OK
                    string Fonts = FontToString(p.Font);//OK
                    string RightToLeft = p.RightToLeft.ToString();//OK
                    string BackColors = p.BackColor.Name.ToString();//OK
                    string Forecolors = p.ForeColor.Name.ToString();//OK
                    string BackgroundImageLayout = p.BackgroundImageLayout.ToString();
                    string Cursor = p.Cursor.ToString();
                    string AllowDrop = p.AllowDrop.ToString();
                    string Enable = p.Enabled.ToString();
                    string TabIndex = p.TabIndex.ToString();
                    string Visible = p.Visible.ToString();
                    string CausesValidation = p.CausesValidation.ToString();
                    string Anchor = p.Anchor.ToString();
                    string Dock = p.Dock.ToString();
                    string Margin = p.Margin.ToString();
                    string Padding = p.Padding.ToString();
                    string MaximumSizeWidth = p.MaximumSize.Width.ToString();
                    string MaximumSizeHeight = p.MaximumSize.Height.ToString();
                    string MinimumSizeWidth = p.MinimumSize.Width.ToString();
                    string MinimumSizeHeight = p.MinimumSize.Height.ToString();
                    string UseWaitCursor = p.UseWaitCursor.ToString();

                    //barcode Variables
                    string BCBoarderStyle = "";
                    string BCHorizontalAlignment = "";
                    string BCHorizontalTextAlignment = "";
                    string BCLookAndFeel = "";
                    string BCVerticalAlignment = "";
                    string BCVerticalTextAlignment = "";
                    string BCAutoModule = "";
                    string BCImeMode = "";
                    string BCModule = "";
                    string BCOrientation = "";
                    string BCShowText = "";
                    string BCSymbology = "";
                    string BCtabstop = "";
                    string BCbinaryData = "";
                    string BCAllowHtmlTextInToolTip = "";
                    string BCShowToolTips = "";
                    string BCToolTip = "";
                    string BCToolTipIconType = "";
                    string BCToolTipTitle = "";

                    //PictureBox Variable
                    string PBBoarderStyle = "";
                    string PBWaitOnLoad = "";
                    string PBSizeMode = "";
                    string PBImageLocation = "";

                    //Label Variable
                    string lblLabelBoarderStyle = "";
                    string lblFlatStyle = "";
                    string lblImageAlign = "";
                    string lblImageIndex = "";
                    string lblimageKey = "";
                    string lblTextAlign = "";
                    string lblUseMnemonic = "";
                    string lblAutoEllipsis = "";
                    string lblUseCompatibleTextRendering = "";
                    string lblAutoSize = "";
                    //OrientedTextLabel
                    string OrLblBorderStyle = "";
                    string OrLblFlatStyle = "";
                    string OrLblImageAlign = "";
                    string OrLblImageIndex = "";
                    string OrLblImageKey = "";
                    string OrLblRotationAngle = "";
                    string OrLblTextAlign = "";
                    string OrLblTextDirection = "";
                    string OrLblTextOrientation = "";
                    string OrLblUseMnemonic = "";
                    string OrLblAutoEllipsis = "";
                    string OrLblUseCompatibleTextRendering = "";
                    string OrLblAutoSize = "";

                    #endregion
                    #region Fill PictureBox and Barcode and Label Propert

                    switch (p.GetType().ToString())
                    {
                        case "DevExpress.XtraEditors.BarCodeControl":
                            BarcodeControl = (DevExpress.XtraEditors.BarCodeControl)p;
                            BCBoarderStyle = BarcodeControl.BorderStyle.ToString();
                            BCHorizontalAlignment = BarcodeControl.HorizontalAlignment.ToString();
                            BCHorizontalTextAlignment = BarcodeControl.HorizontalTextAlignment.ToString();
                            BCLookAndFeel = BarcodeControl.LookAndFeel.ToString();
                            BCVerticalAlignment = BarcodeControl.VerticalAlignment.ToString();
                            BCVerticalTextAlignment = BarcodeControl.VerticalTextAlignment.ToString();
                            BCAutoModule = BarcodeControl.AutoModule.ToString();
                            BCImeMode = BarcodeControl.ImeMode.ToString();
                            BCModule = BarcodeControl.Module.ToString();
                            BCOrientation = BarcodeControl.Orientation.ToString();
                            BCShowText = BarcodeControl.ShowText.ToString();
                            BCSymbology = BarcodeControl.Symbology.ToString();
                            BCtabstop = BarcodeControl.TabStop.ToString();
                            BCbinaryData = BarcodeControl.BinaryData.ToString();
                            BCAllowHtmlTextInToolTip = BarcodeControl.AllowHtmlTextInToolTip.ToString();
                            BCShowToolTips = BarcodeControl.ShowToolTips.ToString();
                            BCToolTip = BarcodeControl.ToolTip.ToString();
                            BCToolTipIconType = BarcodeControl.ToolTipIconType.ToString();
                            BCToolTipTitle = BarcodeControl.ToolTipTitle.ToString();

                            break;

                        case "System.Windows.Forms.PictureBox":
                            PictureBoxControl = (PictureBox)p;
                            PBBoarderStyle = PictureBoxControl.BorderStyle.ToString();
                            PBWaitOnLoad = PictureBoxControl.WaitOnLoad.ToString();
                            PBSizeMode = PictureBoxControl.SizeMode.ToString();
                            break;


                        case "System.Windows.Forms.Label":
                            LabelControl = (Label)p;
                            lblLabelBoarderStyle = LabelControl.BorderStyle.ToString();
                            lblFlatStyle = LabelControl.FlatStyle.ToString();
                            lblImageAlign = LabelControl.ImageAlign.ToString();
                            lblImageIndex = LabelControl.ImageIndex.ToString();
                            lblimageKey = LabelControl.ImageKey.ToString();
                            lblTextAlign = LabelControl.TextAlign.ToString();
                            lblUseMnemonic = LabelControl.UseMnemonic.ToString();
                            lblAutoEllipsis = LabelControl.AutoEllipsis.ToString();
                            lblUseCompatibleTextRendering = LabelControl.UseCompatibleTextRendering.ToString();
                            lblAutoSize = LabelControl.AutoSize.ToString();
                            break;
                        case "CustomControl.OrientAbleTextControls.OrientedTextLabel":
                            OrientedTextLabel = (CustomControl.OrientAbleTextControls.OrientedTextLabel)p;
                            OrLblBorderStyle = OrientedTextLabel.BorderStyle.ToString();
                            OrLblFlatStyle = OrientedTextLabel.FlatStyle.ToString();
                            OrLblImageAlign = OrientedTextLabel.ImageAlign.ToString();
                            OrLblImageIndex = OrientedTextLabel.ImageIndex.ToString();
                            OrLblImageKey = OrientedTextLabel.ImageKey.ToString();
                            OrLblRotationAngle = OrientedTextLabel.RotationAngle.ToString();
                            OrLblTextAlign = OrientedTextLabel.TextAlign.ToString();
                            OrLblTextDirection = OrientedTextLabel.TextDirection.ToString();
                            OrLblTextOrientation = OrientedTextLabel.TextOrientation.ToString();
                            OrLblUseMnemonic = OrientedTextLabel.UseMnemonic.ToString();
                            OrLblAutoEllipsis = OrientedTextLabel.AutoEllipsis.ToString();
                            OrLblUseCompatibleTextRendering = OrientedTextLabel.UseCompatibleTextRendering.ToString();
                            OrLblAutoSize = OrientedTextLabel.AutoSize.ToString();
                            break;
                        default:
                            break;
                    }


                    #endregion
                    #region Convert Picture To Base64
                    PictureBox pic = p as PictureBox; //cast control into PictureBox
                    byte[] bytes = null;
                    string PicBitMapImages = "";
                    if (pic != null) //if it is pictureBox, then it will not be null.
                    {
                        if (pic.Image != null)
                        {
                            Bitmap img = new Bitmap(pic.Image);
                            bytes = imageToByteArray(img);
                            PicBitMapImages = Convert.ToBase64String(bytes);
                        }
                    }
                    #endregion
                    #region CreateXmlElement
                    // Create a new <Category> element and add it to the root node
                    XmlElement parentNode = xmlDoc.CreateElement("AllObjectParent");
                    // Set attribute name and value!
                    parentNode.SetAttribute("ID", p.GetType().ToString());
                    xmlDoc.DocumentElement.PrependChild(parentNode);

                    // Create the required nodes
                    XmlElement CntrlType = xmlDoc.CreateElement("Type");//OK
                    XmlElement CntrlName = xmlDoc.CreateElement("Name");//OK
                    XmlElement CntrlText = xmlDoc.CreateElement("Text");//OK
                    XmlElement CntrlFonts = xmlDoc.CreateElement("Fonts");//OK
                    XmlElement CntrlLocationX = xmlDoc.CreateElement("LocationX");//OK
                    XmlElement CntrlLocationY = xmlDoc.CreateElement("LocationY");//OK
                    XmlElement CntrlSizeWidth = xmlDoc.CreateElement("SizeWidth");//OK
                    XmlElement CntrlSizeHegith = xmlDoc.CreateElement("SizeHeight");//OK
                    XmlElement CntrlPictureImage = xmlDoc.CreateElement("PictureImage");
                    XmlElement CntrlBackColor = xmlDoc.CreateElement("BackColor");//OK
                    XmlElement CntrlForeColor = xmlDoc.CreateElement("ForeColor");//OK
                    XmlElement CntrlRightToLeft = xmlDoc.CreateElement("RightToLeft");//OK
                    XmlElement CntrlBackgroundImageLayout = xmlDoc.CreateElement("BackgroundImageLayout");
                    XmlElement CntrlCursor = xmlDoc.CreateElement("Cursor");
                    XmlElement CntrlAllowDrop = xmlDoc.CreateElement("AllowDrop");
                    XmlElement CntrlEnable = xmlDoc.CreateElement("Enable");
                    XmlElement CntrlTabIndex = xmlDoc.CreateElement("TabIndex");
                    XmlElement CntrlVisible = xmlDoc.CreateElement("Visible");
                    XmlElement CntrlCausesValidation = xmlDoc.CreateElement("CausesValidation");
                    XmlElement CntrlAnchor = xmlDoc.CreateElement("Anchor");
                    XmlElement CntrlDock = xmlDoc.CreateElement("Dock");
                    XmlElement CntrlMargin = xmlDoc.CreateElement("Margin");
                    XmlElement CntrlPadding = xmlDoc.CreateElement("Padding");
                    XmlElement CntrlMaximumSizeWidth = xmlDoc.CreateElement("MaximumSizeWidth");
                    XmlElement CntrlMaximumSizeHeight = xmlDoc.CreateElement("MaximumSizeHeight");
                    XmlElement CntrlMinimumSizeWidth = xmlDoc.CreateElement("MinimumSizeWidth");
                    XmlElement CntrlMinimumSizeHeight = xmlDoc.CreateElement("MinimumSizeHeight");
                    XmlElement CntrlUseWaitCursor = xmlDoc.CreateElement("UseWaitCursor");

                    XmlElement CntrlBCBoarderStyle = xmlDoc.CreateElement("BCBoarderStyle");
                    XmlElement CntrlBCHorizontalAlignment = xmlDoc.CreateElement("BCHorizontalAlignment");
                    XmlElement CntrlBCHorizontalTextAlignment = xmlDoc.CreateElement("BCHorizontalTextAlignment");
                    XmlElement CntrlBCLookAndFeel = xmlDoc.CreateElement("BCLookAndFeel");
                    XmlElement CntrlBCVerticalAlignment = xmlDoc.CreateElement("BCVerticalAlignment");
                    XmlElement CntrlBCVerticalTextAlignment = xmlDoc.CreateElement("BCVerticalTextAlignment");
                    XmlElement CntrlBCAutoModule = xmlDoc.CreateElement("BCAutoModule");
                    XmlElement CntrlBCImeMode = xmlDoc.CreateElement("BCImeMode");
                    XmlElement CntrlBCModule = xmlDoc.CreateElement("BCModule");
                    XmlElement CntrlBCOrientation = xmlDoc.CreateElement("BCOrientation");
                    XmlElement CntrlBCShowText = xmlDoc.CreateElement("BCShowText");
                    XmlElement CntrlBCSymbology = xmlDoc.CreateElement("BCSymbology");
                    XmlElement CntrlBCtabstop = xmlDoc.CreateElement("BCtabstop");
                    XmlElement CntrlBCbinaryData = xmlDoc.CreateElement("BCbinaryData");
                    XmlElement CntrlBCAllowHtmlTextInToolTip = xmlDoc.CreateElement("BCAllowHtmlTextInToolTip");
                    XmlElement CntrlBCShowToolTips = xmlDoc.CreateElement("BCShowToolTips");
                    XmlElement CntrlBCToolTip = xmlDoc.CreateElement("BCToolTip");
                    XmlElement CntrlBCToolTipIconType = xmlDoc.CreateElement("BCToolTipIconType");
                    XmlElement CntrlBCToolTipTitle = xmlDoc.CreateElement("BCToolTipTitle");

                    XmlElement CntrlPBBoarderStyle = xmlDoc.CreateElement("PBBoarderStyle");
                    XmlElement CntrlPBWaitOnLoad = xmlDoc.CreateElement("PBWaitOnLoad");
                    XmlElement CntrlPBSizeMode = xmlDoc.CreateElement("PBSizeMode");
                    XmlElement CntrlPBImageLocation = xmlDoc.CreateElement("PBImageLocation");

                    XmlElement CntrllblLabelBoarderStyle = xmlDoc.CreateElement("lblLabelBoarderStyle");
                    XmlElement CntrllblFlatStyle = xmlDoc.CreateElement("lblFlatStyle");
                    XmlElement CntrllblImageAlign = xmlDoc.CreateElement("lblImageAlign");
                    XmlElement CntrllblImageIndex = xmlDoc.CreateElement("lblImageIndex");
                    XmlElement CntrllblimageKey = xmlDoc.CreateElement("lblimageKey");
                    XmlElement CntrllblTextAlign = xmlDoc.CreateElement("lblTextAlign");
                    XmlElement CntrllblUseMnemonic = xmlDoc.CreateElement("lblUseMnemonic");
                    XmlElement CntrllblAutoEllipsis = xmlDoc.CreateElement("lblAutoEllipsis");
                    XmlElement CntrllblUseCompatibleTextRendering = xmlDoc.CreateElement("lblUseCompatibleTextRendering");
                    XmlElement CntrllblAutoSize = xmlDoc.CreateElement("lblAutoSize");

                    XmlElement CntrlOrLblBorderStyle = xmlDoc.CreateElement("OrLblBorderStyle");
                    XmlElement CntrlOrLblFlatStyle = xmlDoc.CreateElement("OrLblFlatStyle");
                    XmlElement CntrlOrLblImageAlign = xmlDoc.CreateElement("OrLblImageAlign");
                    XmlElement CntrlOrLblImageIndex = xmlDoc.CreateElement("OrLblImageIndex");
                    XmlElement CntrlOrLblImageKey = xmlDoc.CreateElement("OrLblImageKey");
                    XmlElement CntrlOrLblRotationAngle = xmlDoc.CreateElement("OrLblRotationAngle");
                    XmlElement CntrlOrLblTextAlign = xmlDoc.CreateElement("OrLblTextAlign");
                    XmlElement CntrlOrLblTextDirection = xmlDoc.CreateElement("OrLblTextDirection");
                    XmlElement CntrlOrLblTextOrientation = xmlDoc.CreateElement("OrLblTextOrientation");
                    XmlElement CntrlOrLblUseMnemonic = xmlDoc.CreateElement("OrLblUseMnemonic");
                    XmlElement CntrlOrLblAutoEllipsis = xmlDoc.CreateElement("OrLblAutoEllipsis");
                    XmlElement CntrlOrLblUseCompatibleTextRendering = xmlDoc.CreateElement("OrLblUseCompatibleTextRendering");
                    XmlElement CntrlOrLblAutoSize = xmlDoc.CreateElement("OrLblAutoSize");

                    //For Grid
                    XmlElement gridrowsBackColor = xmlDoc.CreateElement("gridsrowsBackColor");
                    XmlElement gridAlternaterowsBackColor = xmlDoc.CreateElement("gridsAlternaterowsBackColor");
                    XmlElement gridheaderColor = xmlDoc.CreateElement("gridsheaderColor");
                    // For Grid
                    //XmlElement nodePanelWidth = xmlDoc.CreateElement("panelWidth");
                    //XmlElement nodePanelHeight = xmlDoc.CreateElement("panelHeight");
                    // retrieve the text 

                    DataGridView dgvs = p as DataGridView; //cast control into PictureBox

                    if (dgvs != null) //if it is pictureBox, then it will not be null.
                    {
                        BackColors = dgvs.BackgroundColor.Name;
                        Forecolors = dgvs.ForeColor.Name;
                    }
                    #endregion
                    #region Fill XmlTest

                    XmlText XmlTextCntrlType = xmlDoc.CreateTextNode(types);
                    XmlText XmlTextCntrlNames = xmlDoc.CreateTextNode(Names);
                    XmlText XmlTextCntrlText = xmlDoc.CreateTextNode(Texts);
                    XmlText XmlTextCntrlFont = xmlDoc.CreateTextNode(Fonts);
                    XmlText XmlTextCntrlLocationX = xmlDoc.CreateTextNode(LocationX);
                    XmlText XmlTextCntrlLocationY = xmlDoc.CreateTextNode(LocationY);
                    XmlText XmlTextCntrlSizeWidth = xmlDoc.CreateTextNode(sizeWidth);
                    XmlText XmlTextCntrlSizeHeight = xmlDoc.CreateTextNode(sizeHegiht);
                    XmlText XmlTextCntrlPictureImage = xmlDoc.CreateTextNode(PicBitMapImages);
                    XmlText XmlTextCntrlBackColor = xmlDoc.CreateTextNode(BackColors);
                    XmlText XmlTextCntrlForeColor = xmlDoc.CreateTextNode(Forecolors);
                    XmlText XmlTextCntrlRightToLeft = xmlDoc.CreateTextNode(RightToLeft);
                    XmlText XmlTextCntrlBackgroundImageLayout = xmlDoc.CreateTextNode(BackgroundImageLayout);
                    XmlText XmlTextCntrlCursor = xmlDoc.CreateTextNode(Cursor);
                    XmlText XmlTextCntrlAllowDrop = xmlDoc.CreateTextNode(AllowDrop);
                    XmlText XmlTextCntrlEnable = xmlDoc.CreateTextNode(Enable);
                    XmlText XmlTextCntrlTabIndex = xmlDoc.CreateTextNode(TabIndex);
                    XmlText XmlTextCntrlVisible = xmlDoc.CreateTextNode(Visible);
                    XmlText XmlTextCntrlCausesValidation = xmlDoc.CreateTextNode(CausesValidation);
                    XmlText XmlTextCntrlAnchor = xmlDoc.CreateTextNode(Anchor);
                    XmlText XmlTextCntrlDock = xmlDoc.CreateTextNode(Dock);
                    XmlText XmlTextCntrlMargin = xmlDoc.CreateTextNode(Margin);
                    XmlText XmlTextCntrlPadding = xmlDoc.CreateTextNode(Padding);
                    XmlText XmlTextCntrlMaximumSizeWidth = xmlDoc.CreateTextNode(MaximumSizeWidth);
                    XmlText XmlTextCntrlMaximumSizeHeight = xmlDoc.CreateTextNode(MaximumSizeHeight);
                    XmlText XmlTextCntrlMinimumSizeWidth = xmlDoc.CreateTextNode(MinimumSizeWidth);
                    XmlText XmlTextCntrlMinimumSizeHeight = xmlDoc.CreateTextNode(MinimumSizeHeight);
                    XmlText XmlTextCntrlUseWaitCursor = xmlDoc.CreateTextNode(UseWaitCursor);


                    XmlText XmlTextCntrlBCBoarderStyle = xmlDoc.CreateTextNode(BCBoarderStyle);
                    XmlText XmlTextCntrlBCHorizontalAlignment = xmlDoc.CreateTextNode(BCHorizontalAlignment);
                    XmlText XmlTextCntrlBCHorizontalTextAlignment = xmlDoc.CreateTextNode(BCHorizontalTextAlignment);
                    XmlText XmlTextCntrlBCLookAndFeel = xmlDoc.CreateTextNode(BCLookAndFeel);
                    XmlText XmlTextCntrlBCVerticalAlignment = xmlDoc.CreateTextNode(BCVerticalAlignment);
                    XmlText XmlTextCntrlBCVerticalTextAlignment = xmlDoc.CreateTextNode(BCVerticalTextAlignment);
                    XmlText XmlTextCntrlBCAutoModule = xmlDoc.CreateTextNode(BCAutoModule);
                    XmlText XmlTextCntrlBCImeMode = xmlDoc.CreateTextNode(BCImeMode);
                    XmlText XmlTextCntrlBCModule = xmlDoc.CreateTextNode(BCModule);
                    XmlText XmlTextCntrlBCOrientation = xmlDoc.CreateTextNode(BCOrientation);
                    XmlText XmlTextCntrlBCShowText = xmlDoc.CreateTextNode(BCShowText);
                    XmlText XmlTextCntrlBCSymbology = xmlDoc.CreateTextNode(BCSymbology);
                    XmlText XmlTextCntrlBCtabstop = xmlDoc.CreateTextNode(BCtabstop);
                    XmlText XmlTextCntrlBCbinaryData = xmlDoc.CreateTextNode(BCbinaryData);
                    XmlText XmlTextCntrlBCAllowHtmlTextInToolTip = xmlDoc.CreateTextNode(BCAllowHtmlTextInToolTip);
                    XmlText XmlTextCntrlBCShowToolTips = xmlDoc.CreateTextNode(BCShowToolTips);
                    XmlText XmlTextCntrlBCToolTip = xmlDoc.CreateTextNode(BCToolTip);
                    XmlText XmlTextCntrlBCToolTipIconType = xmlDoc.CreateTextNode(BCToolTipIconType);
                    XmlText XmlTextCntrlBCToolTipTitle = xmlDoc.CreateTextNode(BCToolTipTitle);

                    XmlText XmlTextCntrlPBBoarderStyle = xmlDoc.CreateTextNode(PBBoarderStyle);
                    XmlText XmlTextCntrlPBWaitOnLoad = xmlDoc.CreateTextNode(PBWaitOnLoad);
                    XmlText XmlTextCntrlPBSizeMode = xmlDoc.CreateTextNode(PBSizeMode);
                    XmlText XmlTextCntrlPBImageLocation = xmlDoc.CreateTextNode(PBImageLocation);

                    XmlText XmlTextCntrllblLabelBoarderStyle = xmlDoc.CreateTextNode(lblLabelBoarderStyle);
                    XmlText XmlTextCntrllblFlatStyle = xmlDoc.CreateTextNode(lblFlatStyle);
                    XmlText XmlTextCntrllblImageAlign = xmlDoc.CreateTextNode(lblImageAlign);
                    XmlText XmlTextCntrllblImageIndex = xmlDoc.CreateTextNode(lblImageIndex);
                    XmlText XmlTextCntrllblimageKey = xmlDoc.CreateTextNode(lblimageKey);
                    XmlText XmlTextCntrllblTextAlign = xmlDoc.CreateTextNode(lblTextAlign);
                    XmlText XmlTextCntrllblUseMnemonic = xmlDoc.CreateTextNode(lblUseMnemonic);
                    XmlText XmlTextCntrllblAutoEllipsis = xmlDoc.CreateTextNode(lblAutoEllipsis);
                    XmlText XmlTextCntrllblUseCompatibleTextRendering = xmlDoc.CreateTextNode(lblUseCompatibleTextRendering);
                    XmlText XmlTextCntrllblAutoSize = xmlDoc.CreateTextNode(lblAutoSize);



                    XmlText XmlTextCntrlOrLblBorderStyle = xmlDoc.CreateTextNode(OrLblBorderStyle);
                    XmlText XmlTextCntrlOrLblFlatStyle = xmlDoc.CreateTextNode(OrLblFlatStyle);
                    XmlText XmlTextCntrlOrLblImageAlign = xmlDoc.CreateTextNode(OrLblImageAlign);
                    XmlText XmlTextCntrlOrLblImageIndex = xmlDoc.CreateTextNode(OrLblImageIndex);
                    XmlText XmlTextCntrlOrLblImageKey = xmlDoc.CreateTextNode(OrLblImageKey);
                    XmlText XmlTextCntrlOrLblRotationAngle = xmlDoc.CreateTextNode(OrLblRotationAngle);
                    XmlText XmlTextCntrlOrLblTextAlign = xmlDoc.CreateTextNode(OrLblTextAlign);
                    XmlText XmlTextCntrlOrLblTextDirection = xmlDoc.CreateTextNode(OrLblTextDirection);
                    XmlText XmlTextCntrlOrLblTextOrientation = xmlDoc.CreateTextNode(OrLblTextOrientation);
                    XmlText XmlTextCntrlOrLblUseMnemonic = xmlDoc.CreateTextNode(OrLblUseMnemonic);
                    XmlText XmlTextCntrlOrLblAutoEllipsis = xmlDoc.CreateTextNode(OrLblAutoEllipsis);
                    XmlText XmlTextCntrlOrLblUseCompatibleTextRendering = xmlDoc.CreateTextNode(OrLblUseCompatibleTextRendering);
                    XmlText XmlTextCntrlOrLblAutoSize = xmlDoc.CreateTextNode(OrLblAutoSize);


                    //Grid
                    XmlText ctlGridrowsBackColor = xmlDoc.CreateTextNode("");
                    XmlText ctlGridAlternaterowsBackColor = xmlDoc.CreateTextNode("");
                    XmlText ctlGridheaderColor = xmlDoc.CreateTextNode("");

                    if (dgvs != null) //if it is pictureBox, then it will not be null.
                    {
                        ctlGridrowsBackColor = xmlDoc.CreateTextNode(dgvs.BackgroundColor.Name);
                        ctlGridAlternaterowsBackColor = xmlDoc.CreateTextNode(dgvs.AlternatingRowsDefaultCellStyle.BackColor.Name);
                        ctlGridheaderColor = xmlDoc.CreateTextNode(dgvs.ColumnHeadersDefaultCellStyle.BackColor.Name);
                    }

                    XmlText txtPanelWidth = xmlDoc.CreateTextNode(PanelControl.Width.ToString());
                    XmlText txtPanelHeight = xmlDoc.CreateTextNode(PanelControl.Height.ToString());
                    #endregion
                    #region PutChildToParents


                    //GRid
                    PanelControl.Controls.GetChildIndex(p);
                    // append the nodes to the parentNode without the value
                    parentNode.AppendChild(CntrlType);
                    parentNode.AppendChild(CntrlName);
                    parentNode.AppendChild(CntrlText);
                    parentNode.AppendChild(CntrlFonts);
                    parentNode.AppendChild(CntrlLocationX);
                    parentNode.AppendChild(CntrlLocationY);
                    parentNode.AppendChild(CntrlSizeWidth);
                    parentNode.AppendChild(CntrlSizeHegith);
                    parentNode.AppendChild(CntrlPictureImage);
                    parentNode.AppendChild(CntrlBackColor);
                    parentNode.AppendChild(CntrlForeColor);
                    parentNode.AppendChild(CntrlRightToLeft);
                    parentNode.AppendChild(CntrlBackgroundImageLayout);
                    parentNode.AppendChild(CntrlCursor);
                    parentNode.AppendChild(CntrlAllowDrop);
                    parentNode.AppendChild(CntrlEnable);
                    parentNode.AppendChild(CntrlTabIndex);
                    parentNode.AppendChild(CntrlVisible);
                    parentNode.AppendChild(CntrlCausesValidation);
                    parentNode.AppendChild(CntrlAnchor);
                    parentNode.AppendChild(CntrlDock);
                    parentNode.AppendChild(CntrlMargin);
                    parentNode.AppendChild(CntrlPadding);
                    parentNode.AppendChild(CntrlMaximumSizeWidth);
                    parentNode.AppendChild(CntrlMaximumSizeHeight);
                    parentNode.AppendChild(CntrlMinimumSizeWidth);
                    parentNode.AppendChild(CntrlMinimumSizeHeight);
                    parentNode.AppendChild(CntrlUseWaitCursor);

                    parentNode.AppendChild(CntrlBCBoarderStyle);
                    parentNode.AppendChild(CntrlBCHorizontalAlignment);
                    parentNode.AppendChild(CntrlBCHorizontalTextAlignment);
                    parentNode.AppendChild(CntrlBCLookAndFeel);
                    parentNode.AppendChild(CntrlBCVerticalAlignment);
                    parentNode.AppendChild(CntrlBCVerticalTextAlignment);
                    parentNode.AppendChild(CntrlBCAutoModule);
                    parentNode.AppendChild(CntrlBCImeMode);
                    parentNode.AppendChild(CntrlBCModule);
                    parentNode.AppendChild(CntrlBCOrientation);
                    parentNode.AppendChild(CntrlBCShowText);
                    parentNode.AppendChild(CntrlBCSymbology);
                    parentNode.AppendChild(CntrlBCtabstop);
                    parentNode.AppendChild(CntrlBCbinaryData);
                    parentNode.AppendChild(CntrlBCAllowHtmlTextInToolTip);
                    parentNode.AppendChild(CntrlBCShowToolTips);
                    parentNode.AppendChild(CntrlBCToolTip);
                    parentNode.AppendChild(CntrlBCToolTipIconType);
                    parentNode.AppendChild(CntrlBCToolTipTitle);

                    parentNode.AppendChild(CntrlPBBoarderStyle);
                    parentNode.AppendChild(CntrlPBWaitOnLoad);
                    parentNode.AppendChild(CntrlPBSizeMode);
                    parentNode.AppendChild(CntrlPBImageLocation);

                    parentNode.AppendChild(CntrllblLabelBoarderStyle);
                    parentNode.AppendChild(CntrllblFlatStyle);
                    parentNode.AppendChild(CntrllblImageAlign);
                    parentNode.AppendChild(CntrllblImageIndex);
                    parentNode.AppendChild(CntrllblimageKey);
                    parentNode.AppendChild(CntrllblTextAlign);
                    parentNode.AppendChild(CntrllblUseMnemonic);
                    parentNode.AppendChild(CntrllblAutoEllipsis);
                    parentNode.AppendChild(CntrllblUseCompatibleTextRendering);
                    parentNode.AppendChild(CntrllblAutoSize);

                    parentNode.AppendChild(CntrlOrLblBorderStyle);
                    parentNode.AppendChild(CntrlOrLblFlatStyle);
                    parentNode.AppendChild(CntrlOrLblImageAlign);
                    parentNode.AppendChild(CntrlOrLblImageIndex);
                    parentNode.AppendChild(CntrlOrLblImageKey);
                    parentNode.AppendChild(CntrlOrLblRotationAngle);
                    parentNode.AppendChild(CntrlOrLblTextAlign);
                    parentNode.AppendChild(CntrlOrLblTextDirection);
                    parentNode.AppendChild(CntrlOrLblTextOrientation);
                    parentNode.AppendChild(CntrlOrLblUseMnemonic);
                    parentNode.AppendChild(CntrlOrLblAutoEllipsis);
                    parentNode.AppendChild(CntrlOrLblUseCompatibleTextRendering);
                    parentNode.AppendChild(CntrlOrLblAutoSize);

                    //for Grid
                    parentNode.AppendChild(gridrowsBackColor);
                    parentNode.AppendChild(gridAlternaterowsBackColor);
                    parentNode.AppendChild(gridheaderColor);
                    //grid
                    // save the value of the fields into the nodes


                    CntrlType.AppendChild(XmlTextCntrlType);
                    CntrlName.AppendChild(XmlTextCntrlNames);
                    CntrlText.AppendChild(XmlTextCntrlText);
                    CntrlFonts.AppendChild(XmlTextCntrlFont);
                    CntrlLocationX.AppendChild(XmlTextCntrlLocationX);
                    CntrlLocationY.AppendChild(XmlTextCntrlLocationY);
                    CntrlSizeWidth.AppendChild(XmlTextCntrlSizeWidth);
                    CntrlSizeHegith.AppendChild(XmlTextCntrlSizeHeight);
                    CntrlPictureImage.AppendChild(XmlTextCntrlPictureImage);
                    CntrlBackColor.AppendChild(XmlTextCntrlBackColor);
                    CntrlForeColor.AppendChild(XmlTextCntrlForeColor);
                    CntrlRightToLeft.AppendChild(XmlTextCntrlRightToLeft);
                    CntrlBackgroundImageLayout.AppendChild(XmlTextCntrlBackgroundImageLayout);
                    CntrlCursor.AppendChild(XmlTextCntrlCursor);
                    CntrlAllowDrop.AppendChild(XmlTextCntrlAllowDrop);
                    CntrlEnable.AppendChild(XmlTextCntrlEnable);
                    CntrlTabIndex.AppendChild(XmlTextCntrlTabIndex);
                    CntrlVisible.AppendChild(XmlTextCntrlVisible);
                    CntrlCausesValidation.AppendChild(XmlTextCntrlCausesValidation);
                    CntrlAnchor.AppendChild(XmlTextCntrlAnchor);
                    CntrlDock.AppendChild(XmlTextCntrlDock);
                    CntrlMargin.AppendChild(XmlTextCntrlMargin);
                    CntrlPadding.AppendChild(XmlTextCntrlPadding);
                    CntrlMaximumSizeWidth.AppendChild(XmlTextCntrlMaximumSizeWidth);
                    CntrlMaximumSizeHeight.AppendChild(XmlTextCntrlMaximumSizeHeight);
                    CntrlMinimumSizeWidth.AppendChild(XmlTextCntrlMinimumSizeWidth);
                    CntrlMinimumSizeHeight.AppendChild(XmlTextCntrlMinimumSizeHeight);
                    CntrlUseWaitCursor.AppendChild(XmlTextCntrlUseWaitCursor);

                    CntrlBCBoarderStyle.AppendChild(XmlTextCntrlBCBoarderStyle);
                    CntrlBCHorizontalAlignment.AppendChild(XmlTextCntrlBCHorizontalAlignment);
                    CntrlBCHorizontalTextAlignment.AppendChild(XmlTextCntrlBCHorizontalTextAlignment);
                    CntrlBCLookAndFeel.AppendChild(XmlTextCntrlBCLookAndFeel);
                    CntrlBCVerticalAlignment.AppendChild(XmlTextCntrlBCVerticalAlignment);
                    CntrlBCVerticalTextAlignment.AppendChild(XmlTextCntrlBCVerticalTextAlignment);
                    CntrlBCAutoModule.AppendChild(XmlTextCntrlBCAutoModule);
                    CntrlBCImeMode.AppendChild(XmlTextCntrlBCImeMode);
                    CntrlBCModule.AppendChild(XmlTextCntrlBCModule);
                    CntrlBCOrientation.AppendChild(XmlTextCntrlBCOrientation);
                    CntrlBCShowText.AppendChild(XmlTextCntrlBCShowText);
                    CntrlBCSymbology.AppendChild(XmlTextCntrlBCSymbology);
                    CntrlBCtabstop.AppendChild(XmlTextCntrlBCtabstop);
                    CntrlBCbinaryData.AppendChild(XmlTextCntrlBCbinaryData);
                    CntrlBCAllowHtmlTextInToolTip.AppendChild(XmlTextCntrlBCAllowHtmlTextInToolTip);
                    CntrlBCShowToolTips.AppendChild(XmlTextCntrlBCShowToolTips);
                    CntrlBCToolTip.AppendChild(XmlTextCntrlBCToolTip);
                    CntrlBCToolTipIconType.AppendChild(XmlTextCntrlBCToolTipIconType);
                    CntrlBCToolTipTitle.AppendChild(XmlTextCntrlBCToolTipTitle);

                    CntrlPBBoarderStyle.AppendChild(XmlTextCntrlPBBoarderStyle);
                    CntrlPBWaitOnLoad.AppendChild(XmlTextCntrlPBWaitOnLoad);
                    CntrlPBSizeMode.AppendChild(XmlTextCntrlPBSizeMode);
                    CntrlPBImageLocation.AppendChild(XmlTextCntrlPBImageLocation);

                    CntrllblLabelBoarderStyle.AppendChild(XmlTextCntrllblLabelBoarderStyle);
                    CntrllblFlatStyle.AppendChild(XmlTextCntrllblFlatStyle);
                    CntrllblImageAlign.AppendChild(XmlTextCntrllblImageAlign);
                    CntrllblImageIndex.AppendChild(XmlTextCntrllblImageIndex);
                    CntrllblimageKey.AppendChild(XmlTextCntrllblimageKey);
                    CntrllblTextAlign.AppendChild(XmlTextCntrllblTextAlign);
                    CntrllblUseMnemonic.AppendChild(XmlTextCntrllblUseMnemonic);
                    CntrllblAutoEllipsis.AppendChild(XmlTextCntrllblAutoEllipsis);
                    CntrllblUseCompatibleTextRendering.AppendChild(XmlTextCntrllblUseCompatibleTextRendering);
                    CntrllblAutoSize.AppendChild(XmlTextCntrllblAutoSize);


                    CntrlOrLblBorderStyle.AppendChild(XmlTextCntrlOrLblBorderStyle);
                    CntrlOrLblFlatStyle.AppendChild(XmlTextCntrlOrLblFlatStyle);
                    CntrlOrLblImageAlign.AppendChild(XmlTextCntrlOrLblImageAlign);
                    CntrlOrLblImageIndex.AppendChild(XmlTextCntrlOrLblImageIndex);
                    CntrlOrLblImageKey.AppendChild(XmlTextCntrlOrLblImageKey);
                    CntrlOrLblRotationAngle.AppendChild(XmlTextCntrlOrLblRotationAngle);
                    CntrlOrLblTextAlign.AppendChild(XmlTextCntrlOrLblTextAlign);
                    CntrlOrLblTextDirection.AppendChild(XmlTextCntrlOrLblTextDirection);
                    CntrlOrLblTextOrientation.AppendChild(XmlTextCntrlOrLblTextOrientation);
                    CntrlOrLblUseMnemonic.AppendChild(XmlTextCntrlOrLblUseMnemonic);
                    CntrlOrLblAutoEllipsis.AppendChild(XmlTextCntrlOrLblAutoEllipsis);
                    CntrlOrLblUseCompatibleTextRendering.AppendChild(XmlTextCntrlOrLblUseCompatibleTextRendering);
                    CntrlOrLblAutoSize.AppendChild(XmlTextCntrlOrLblAutoSize);

                    //for Grid
                    gridrowsBackColor.AppendChild(ctlGridrowsBackColor);
                    gridAlternaterowsBackColor.AppendChild(ctlGridAlternaterowsBackColor);
                    gridheaderColor.AppendChild(ctlGridheaderColor);
                    //grid
                    //nodePanelHeight.AppendChild(txtPanelHeight);
                }

                #endregion

            }
            catch (Exception)
            {
                flgSomethingWorng = true;
                MessageBox.Show("داده ها درست وارد نشده است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (flgSomethingWorng)
                xmlDoc.RemoveAll();
            return xmlDoc;
        }
        public enum PicutreToEzdStatus
        {
            Ok,
            Fail,
            Timeout
        }
        private static PicutreToEzdStatus putPicturesToEZD(Panel WhichPanel, Dictionary<string, string> Pictures)
        {
            PicutreToEzdStatus ReturnStatus = PicutreToEzdStatus.Fail;
            int EntityName = 0;
            LaserConfigClass LaserSetting = new LaserConfigClass();
            Hardware.Laser.LmcErrCode TmpStatus = Laser.LmcErrCode.LMC1_ERR_UNKNOW;
            bool flgNoPicExist = false;
            string TempFolder = Path.GetTempPath();

            TmpStatus = Laser.ClearLibAllEntity();
            if (TmpStatus == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
            {
                foreach (Control item in WhichPanel.Controls)
                {

                    if (item.GetType().ToString() == "System.Windows.Forms.PictureBox")
                    {
                        string answerposition = "";
                        PictureBox tmp = item as PictureBox;
                        if (Pictures.TryGetValue(tmp.Name, out answerposition))
                        {
                            LaserSetting = LaserConfigClass.load();
                            double[] Location = convertPxTomm(tmp.Location);
                            Bitmap bmp = new Bitmap(answerposition);
                            string BmpName = Path.GetTempPath() + "LaserPrinterImagetmp\\" + "PictureTop_" + tmp.Name.Replace(".txt", "");



                            if (tmp.Name.Replace(".txt", "").Contains("bmp"))
                                bmp.Save(BmpName, ImageFormat.Bmp);
                            else if (tmp.Name.Replace(".txt", "").Contains("jpg"))
                                bmp.Save(BmpName, ImageFormat.Jpeg);
                            else if (tmp.Name.Replace(".txt", "").Contains("jpeg"))
                                bmp.Save(BmpName, ImageFormat.Jpeg);
                            else if (tmp.Name.Replace(".txt", "").Contains("png"))
                                bmp.Save(BmpName, ImageFormat.Png);
                            else if (tmp.Name.Replace(".txt", "").Contains("gif"))
                                bmp.Save(BmpName, ImageFormat.Gif);
                            else if (tmp.Name.Replace(".txt", "").Contains("tiff"))
                                bmp.Save(BmpName, ImageFormat.Tiff);
                            else if (tmp.Name.Replace(".txt", "").Contains("tif"))
                                bmp.Save(BmpName, ImageFormat.Tiff);



                            float DPIX = bmp.HorizontalResolution;
                            float DPIY = bmp.VerticalResolution;
                            double[] PictureSize = WithDPIconvertPxTomm(bmp.Size, DPIX, DPIY);


                            double NewCentreLocationX = Location[0] - 42.8 + LaserSetting.Xcenter;// - 43;
                            double NewCentreLocationY = -Location[1] + LaserSetting.Ycenter + 1.6;// (54 - NewYLocation) - 27;

                            TmpStatus = Laser.SetPenParam(3, 1, 300, 5, 1, 35000, 10, 10, 100, 300, 0, 4000, 500, 100, 0, 0.01, 0.100, false, 1, 0);
                            if (TmpStatus == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                            {
                                TmpStatus = Laser.AddFileToLib(BmpName, "Picture" + EntityName.ToString(), NewCentreLocationX, NewCentreLocationY, 0, 0, 1, 3, false);
                                if (TmpStatus == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                                {
                                    TmpStatus = Laser.SetBitmapEntParam("Picture" + EntityName.ToString(), BmpName, 0, 0, 0, 0, 0, 600, false, 0);
                                    if (TmpStatus == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                                        EntityName++;
                                    else
                                    {
                                        MessageBox.Show("لطفا از اتصال دستگاه اطمینان حاصل نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        flgNoPicExist = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("لطفا از اتصال دستگاه اطمینان حاصل نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    flgNoPicExist = true;
                                    break;
                                }
                            }
                            else
                            {
                                MessageBox.Show("لطفا از اتصال دستگاه اطمینان حاصل نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                flgNoPicExist = true;
                                break;
                            }

                        }
                        else
                        {
                            MessageBox.Show("اطلاعات پایگاه داده صحیح نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            flgNoPicExist = true;
                            break;
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("لطفا از اتصال دستگاه اطمینان حاصل نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flgNoPicExist = true;

            }

            if (!flgNoPicExist)
                Laser.SaveEntLibToFile(Path.GetTempPath() + "LaserPrinterImagetmp\\PictureTop.ezd");

            return ReturnStatus;


        }
        private static Font StringToFont(string font)
        {
            string[] parts = font.Split(':');
            if (parts.Length != 3)
                throw new ArgumentException("Not a valid font string", "font");

            Font loadedFont = new Font(parts[0], float.Parse(parts[1]), (FontStyle)int.Parse(parts[2]));
            return loadedFont;
        }
        private static byte[] imageToByteArray(Bitmap bmp)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            return (byte[])converter.ConvertTo(bmp, typeof(byte[]));
        }
        private static string FontToString(Font font)
        {
            return font.FontFamily.Name + ":" + font.Size + ":" + (int)font.Style;
        }
        public static void ReadyObjectForLaser(Panel WhichPanel, bool CheckPrinter)
        {
            string TempFolder = Path.GetTempPath();
            System.IO.DirectoryInfo di = new DirectoryInfo(TempFolder + "LaserPrinterImagetmp");

            string PictureTopPath = TempFolder + "LaserPrinterImagetmp\\PictureTop.bmp";
            string WithoutPictureBoxPictureTopPath = TempFolder + "LaserPrinterImagetmp\\WithoutPictureBoxPictureTop.bmp";
            string WithPictureBoxPictureTopPath = TempFolder + "LaserPrinterImagetmp\\WithPictureBoxPictureTop.bmp";

            string PictureBottomPath = TempFolder + "LaserPrinterImagetmp\\PictureBottom.bmp";
            string WithoutPictureBoxPictureBottom = TempFolder + "LaserPrinterImagetmp\\WithoutPictureBoxPictureBottom.bmp";
            string WithPictureBoxPictureBottom = TempFolder + "LaserPrinterImagetmp\\WithPictureBoxPictureBottom.bmp";


            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }
        public static double[] convertPxTomm(Point Pixellocation)
        {
            const int DPI = 200;
            double[] Location = new double[2];
            double XPos = 0, YPos = 0;
            XPos = (Pixellocation.X * 25.4) / DPI;
            YPos = (Pixellocation.Y * 25.4) / DPI;
            Location[0] = XPos;
            Location[1] = YPos;
            return Location;
        }
        public static System.Drawing.Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }
        public static double[] WithDPIconvertPxTomm(Size Pixellocation, float DPIX, float DPIY)
        {

            double[] Location = new double[2];
            double XPos = 0, YPos = 0;
            XPos = (Pixellocation.Width * 25.4) / DPIX;
            YPos = (Pixellocation.Height * 25.4) / DPIY;
            Location[0] = XPos;
            Location[1] = YPos;
            return Location;
        }
        public static Size WithDPIConvermmtoPx(double XmmLocation, double YmmLocation, float DPIX, float DPIY)
        {

            Size Location = new Size();
            double XPos = 0, YPos = 0;
            XPos = (XmmLocation * DPIX) / 25.4;
            YPos = (YmmLocation * DPIY) / 25.4;
            Location.Width = (int)Math.Ceiling(XPos);
            Location.Height = (int)Math.Ceiling(YPos);
            return Location;
        }
        public static string ImageToBase64(string Path)
        {

            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
        public static Panel CopyControl(Control Ctrl)
        {
            Panel CopiedCtrlPanel = new Panel();
            try
            {
                switch (Ctrl.GetType().ToString())
                {
                    case "System.Windows.Forms.PictureBox":
                        {
                            try
                            {
                                PictureBox PasteControlPictureBox = new PictureBox();
                                PictureBox CopiedControlPictureBox = Ctrl as PictureBox;
                                PasteControlPictureBox.Name = CopiedControlPictureBox.Name;
                                PasteControlPictureBox.Text = CopiedControlPictureBox.Text;
                                PasteControlPictureBox.Font = CopiedControlPictureBox.Font;
                                PasteControlPictureBox.Location = CopiedControlPictureBox.Location;
                                PasteControlPictureBox.BackgroundImageLayout = CopiedControlPictureBox.BackgroundImageLayout;
                                PasteControlPictureBox.Size = CopiedControlPictureBox.Size;
                                PasteControlPictureBox.BackColor = CopiedControlPictureBox.BackColor;
                                PasteControlPictureBox.ForeColor = CopiedControlPictureBox.ForeColor;
                                PasteControlPictureBox.RightToLeft = CopiedControlPictureBox.RightToLeft;
                                PasteControlPictureBox.Cursor = CopiedControlPictureBox.Cursor;
                                PasteControlPictureBox.AllowDrop = CopiedControlPictureBox.AllowDrop;
                                PasteControlPictureBox.Enabled = CopiedControlPictureBox.Enabled;
                                PasteControlPictureBox.TabIndex = CopiedControlPictureBox.TabIndex;
                                PasteControlPictureBox.Visible = CopiedControlPictureBox.Visible;
                                PasteControlPictureBox.CausesValidation = CopiedControlPictureBox.CausesValidation;
                                PasteControlPictureBox.Anchor = CopiedControlPictureBox.Anchor;
                                PasteControlPictureBox.Dock = CopiedControlPictureBox.Dock;
                                PasteControlPictureBox.Margin = CopiedControlPictureBox.Margin;
                                PasteControlPictureBox.Padding = CopiedControlPictureBox.Padding;
                                PasteControlPictureBox.MaximumSize = CopiedControlPictureBox.MaximumSize;
                                PasteControlPictureBox.MinimumSize = CopiedControlPictureBox.MinimumSize;
                                PasteControlPictureBox.UseWaitCursor = CopiedControlPictureBox.UseWaitCursor;
                                PasteControlPictureBox.BorderStyle = CopiedControlPictureBox.BorderStyle;
                                PasteControlPictureBox.WaitOnLoad = CopiedControlPictureBox.WaitOnLoad;
                                PasteControlPictureBox.SizeMode = CopiedControlPictureBox.SizeMode;
                                PasteControlPictureBox.ImageLocation = CopiedControlPictureBox.ImageLocation;
                                PasteControlPictureBox.Image = CopiedControlPictureBox.Image;
                                CopiedCtrlPanel.Controls.Add(PasteControlPictureBox);
                            }
                            catch (Exception)
                            {
                            }
                        }
                        break;

                    case "System.Windows.Forms.Label": //Label
                        {

                            Label PasteControlLabel = new Label();
                            Label CopiedLabel = Ctrl as Label;
                            PasteControlLabel.Name = CopiedLabel.Name;
                            PasteControlLabel.Text = CopiedLabel.Text;
                            PasteControlLabel.Font = CopiedLabel.Font;

                            PasteControlLabel.Location = CopiedLabel.Location;
                            PasteControlLabel.Size = CopiedLabel.Size;
                            PasteControlLabel.BackColor = CopiedLabel.BackColor;
                            PasteControlLabel.ForeColor = CopiedLabel.ForeColor;
                            PasteControlLabel.RightToLeft = CopiedLabel.RightToLeft;
                            PasteControlLabel.Cursor = CopiedLabel.Cursor;
                            PasteControlLabel.AllowDrop = CopiedLabel.AllowDrop;
                            PasteControlLabel.Enabled = CopiedLabel.Enabled;
                            PasteControlLabel.TabIndex = CopiedLabel.TabIndex;
                            PasteControlLabel.Visible = CopiedLabel.Visible;
                            PasteControlLabel.CausesValidation = CopiedLabel.CausesValidation;
                            PasteControlLabel.Anchor = CopiedLabel.Anchor;
                            PasteControlLabel.Dock = CopiedLabel.Dock;
                            PasteControlLabel.Margin = CopiedLabel.Margin;
                            PasteControlLabel.Padding = CopiedLabel.Padding;
                            PasteControlLabel.MaximumSize = CopiedLabel.MaximumSize;
                            PasteControlLabel.MinimumSize = CopiedLabel.MinimumSize;
                            PasteControlLabel.UseWaitCursor = CopiedLabel.UseWaitCursor;

                            PasteControlLabel.BorderStyle = CopiedLabel.BorderStyle;
                            PasteControlLabel.FlatStyle = CopiedLabel.FlatStyle;
                            PasteControlLabel.Image = CopiedLabel.Image;
                            PasteControlLabel.ImageAlign = CopiedLabel.ImageAlign;
                            PasteControlLabel.ImageIndex = CopiedLabel.ImageIndex;
                            PasteControlLabel.ImageKey = CopiedLabel.ImageKey;
                            PasteControlLabel.TextAlign = CopiedLabel.TextAlign;
                            PasteControlLabel.UseMnemonic = CopiedLabel.UseMnemonic;
                            PasteControlLabel.AutoEllipsis = CopiedLabel.AutoEllipsis;
                            PasteControlLabel.UseCompatibleTextRendering = CopiedLabel.UseCompatibleTextRendering;
                            PasteControlLabel.AutoSize = CopiedLabel.AutoSize;

                            CopiedCtrlPanel.Controls.Add(PasteControlLabel);
                        }
                        break;

                    case "CustomControl.OrientAbleTextControls.OrientedTextLabel":
                        {
                            CustomControl.OrientAbleTextControls.OrientedTextLabel PasteControlOrientedTextLabel = new CustomControl.OrientAbleTextControls.OrientedTextLabel();
                            CustomControl.OrientAbleTextControls.OrientedTextLabel CopiedControlOrientedTextLabel = Ctrl as CustomControl.OrientAbleTextControls.OrientedTextLabel;
                            PasteControlOrientedTextLabel.Name = CopiedControlOrientedTextLabel.Name;
                            PasteControlOrientedTextLabel.Text = CopiedControlOrientedTextLabel.Text;
                            PasteControlOrientedTextLabel.Font = CopiedControlOrientedTextLabel.Font;
                            PasteControlOrientedTextLabel.Location = CopiedControlOrientedTextLabel.Location;
                            PasteControlOrientedTextLabel.Size = CopiedControlOrientedTextLabel.Size;
                            PasteControlOrientedTextLabel.BackColor = CopiedControlOrientedTextLabel.BackColor;
                            PasteControlOrientedTextLabel.ForeColor = CopiedControlOrientedTextLabel.ForeColor;
                            PasteControlOrientedTextLabel.RightToLeft = CopiedControlOrientedTextLabel.RightToLeft;
                            PasteControlOrientedTextLabel.Cursor = CopiedControlOrientedTextLabel.Cursor;
                            PasteControlOrientedTextLabel.AllowDrop = CopiedControlOrientedTextLabel.AllowDrop;
                            PasteControlOrientedTextLabel.Enabled = CopiedControlOrientedTextLabel.Enabled;
                            PasteControlOrientedTextLabel.TabIndex = CopiedControlOrientedTextLabel.TabIndex;
                            PasteControlOrientedTextLabel.Visible = CopiedControlOrientedTextLabel.Visible;
                            PasteControlOrientedTextLabel.CausesValidation = CopiedControlOrientedTextLabel.CausesValidation;
                            PasteControlOrientedTextLabel.Anchor = CopiedControlOrientedTextLabel.Anchor;
                            PasteControlOrientedTextLabel.Dock = CopiedControlOrientedTextLabel.Dock;
                            PasteControlOrientedTextLabel.Margin = CopiedControlOrientedTextLabel.Margin;
                            PasteControlOrientedTextLabel.Padding = CopiedControlOrientedTextLabel.Padding;
                            PasteControlOrientedTextLabel.MaximumSize = CopiedControlOrientedTextLabel.MaximumSize;
                            PasteControlOrientedTextLabel.MinimumSize = CopiedControlOrientedTextLabel.MinimumSize;
                            PasteControlOrientedTextLabel.UseWaitCursor = CopiedControlOrientedTextLabel.UseWaitCursor;

                            PasteControlOrientedTextLabel.BorderStyle = CopiedControlOrientedTextLabel.BorderStyle;
                            PasteControlOrientedTextLabel.FlatStyle = CopiedControlOrientedTextLabel.FlatStyle;
                            PasteControlOrientedTextLabel.Image = CopiedControlOrientedTextLabel.Image;
                            PasteControlOrientedTextLabel.ImageAlign = CopiedControlOrientedTextLabel.ImageAlign;
                            PasteControlOrientedTextLabel.ImageIndex = CopiedControlOrientedTextLabel.ImageIndex;
                            PasteControlOrientedTextLabel.ImageKey = CopiedControlOrientedTextLabel.ImageKey;
                            PasteControlOrientedTextLabel.RotationAngle = CopiedControlOrientedTextLabel.RotationAngle;
                            PasteControlOrientedTextLabel.TextAlign = CopiedControlOrientedTextLabel.TextAlign;
                            PasteControlOrientedTextLabel.TextDirection = CopiedControlOrientedTextLabel.TextDirection;
                            PasteControlOrientedTextLabel.TextOrientation = CopiedControlOrientedTextLabel.TextOrientation;
                            PasteControlOrientedTextLabel.UseMnemonic = CopiedControlOrientedTextLabel.UseMnemonic;
                            PasteControlOrientedTextLabel.AutoEllipsis = CopiedControlOrientedTextLabel.AutoEllipsis;
                            PasteControlOrientedTextLabel.UseCompatibleTextRendering = CopiedControlOrientedTextLabel.UseCompatibleTextRendering;
                            PasteControlOrientedTextLabel.AutoSize = CopiedControlOrientedTextLabel.AutoSize;
                            CopiedCtrlPanel.Controls.Add(PasteControlOrientedTextLabel);
                        }
                        break;
                    case "DevExpress.XtraEditors.BarCodeControl":
                        {
                            DevExpress.XtraEditors.BarCodeControl PasteControlBarcode = new DevExpress.XtraEditors.BarCodeControl();
                            DevExpress.XtraEditors.BarCodeControl CopiedControlBarcode = Ctrl as DevExpress.XtraEditors.BarCodeControl;
                            PasteControlBarcode.Name = CopiedControlBarcode.Name;
                            PasteControlBarcode.Text = CopiedControlBarcode.Text;
                            PasteControlBarcode.Font = CopiedControlBarcode.Font;
                            PasteControlBarcode.Location = CopiedControlBarcode.Location;
                            PasteControlBarcode.Size = CopiedControlBarcode.Size;
                            PasteControlBarcode.BackColor = CopiedControlBarcode.BackColor;
                            PasteControlBarcode.ForeColor = CopiedControlBarcode.ForeColor;
                            PasteControlBarcode.RightToLeft = CopiedControlBarcode.RightToLeft;
                            PasteControlBarcode.BackgroundImageLayout = CopiedControlBarcode.BackgroundImageLayout;
                            PasteControlBarcode.Cursor = CopiedControlBarcode.Cursor;
                            PasteControlBarcode.AllowDrop = CopiedControlBarcode.AllowDrop;
                            PasteControlBarcode.Enabled = CopiedControlBarcode.Enabled;
                            PasteControlBarcode.TabIndex = CopiedControlBarcode.TabIndex;
                            PasteControlBarcode.Visible = CopiedControlBarcode.Visible;
                            PasteControlBarcode.CausesValidation = CopiedControlBarcode.CausesValidation;
                            PasteControlBarcode.Anchor = CopiedControlBarcode.Anchor;
                            PasteControlBarcode.Dock = CopiedControlBarcode.Dock;
                            PasteControlBarcode.Margin = CopiedControlBarcode.Margin;
                            PasteControlBarcode.Padding = CopiedControlBarcode.Padding;
                            PasteControlBarcode.MaximumSize = CopiedControlBarcode.MaximumSize;
                            PasteControlBarcode.MinimumSize = CopiedControlBarcode.MinimumSize;
                            PasteControlBarcode.UseWaitCursor = CopiedControlBarcode.UseWaitCursor;

                            PasteControlBarcode.BorderStyle = CopiedControlBarcode.BorderStyle;
                            PasteControlBarcode.HorizontalAlignment = CopiedControlBarcode.HorizontalAlignment;
                            PasteControlBarcode.HorizontalTextAlignment = CopiedControlBarcode.HorizontalTextAlignment;
                            PasteControlBarcode.VerticalAlignment = CopiedControlBarcode.VerticalAlignment;
                            PasteControlBarcode.VerticalTextAlignment = CopiedControlBarcode.VerticalTextAlignment;
                            PasteControlBarcode.AutoModule = CopiedControlBarcode.AutoModule;
                            PasteControlBarcode.ImeMode = CopiedControlBarcode.ImeMode;
                            PasteControlBarcode.Module = CopiedControlBarcode.Module;
                            PasteControlBarcode.Orientation = CopiedControlBarcode.Orientation;
                            PasteControlBarcode.ShowText = CopiedControlBarcode.ShowText;
                            PasteControlBarcode.Symbology = CopiedControlBarcode.Symbology;
                            PasteControlBarcode.TabStop = CopiedControlBarcode.TabStop;
                            PasteControlBarcode.AllowHtmlTextInToolTip = CopiedControlBarcode.AllowHtmlTextInToolTip;
                            PasteControlBarcode.ShowToolTips = CopiedControlBarcode.ShowToolTips;
                            PasteControlBarcode.ToolTip = CopiedControlBarcode.ToolTip;
                            PasteControlBarcode.ToolTipIconType = CopiedControlBarcode.ToolTipIconType;
                            PasteControlBarcode.ToolTipTitle = CopiedControlBarcode.ToolTipTitle;
                            CopiedCtrlPanel.Controls.Add(PasteControlBarcode);
                        }
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("شیء مورد نظر به درستی کپی نشده است لطفا دوباره سعی نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return CopiedCtrlPanel;
        }
        public Bitmap GrayScale(Bitmap Bmp)
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
        private Bitmap GetControlImage(Control ctl)
        {
            Bitmap bm = new Bitmap(ctl.Width, ctl.Height);
            ctl.DrawToBitmap(bm, new Rectangle(0, 0, ctl.Width, ctl.Height));

            return bm;
        }
        Bitmap[] SplitPictureControl(Control Ctrl)
        {
            Bitmap[] TwoType = new Bitmap[2];

            int j = 0;
            Panel ExceptPicture = new Panel();
            Panel OnlyPicture = new Panel();
            ExceptPicture.Size = new Size(674, 425);
            OnlyPicture.Size = new Size(674, 425);
            ExceptPicture.BackColor = Color.White;
            OnlyPicture.BackColor = Color.White;

            foreach (Control item in Ctrl.Controls)
            {
                if (item.GetType() == typeof(System.Windows.Forms.PictureBox))
                {
                    Panel tmp = new Panel();
                    tmp.Size = new Size(674, 425);
                    tmp = CopyControl(item);
                    j = 0;
                    for (int i = 0; i < tmp.Controls.Count; i++)
                    {
                        if (tmp.Controls[i].Name == item.Name)
                            j = i;
                    }
                    if (tmp.Controls.Count > 0)
                        OnlyPicture.Controls.Add(tmp.Controls[j]);
                    tmp.Dispose();
                }
                else
                {
                    Panel tmp = new Panel();
                    tmp.Size = new Size(674, 425);
                    tmp = CopyControl(item);
                    j = 0;
                    for (int i = 0; i < tmp.Controls.Count; i++)
                    {
                        if (tmp.Controls[i].Name == item.Name)
                            j = i;
                    }
                    if (tmp.Controls.Count > 0)
                        ExceptPicture.Controls.Add(tmp.Controls[j]);
                    tmp.Dispose();
                }
            }

            var TmpImage = GetControlImage(ExceptPicture);

            var scale = GrayScale(TmpImage);
            TwoType[0] = scale.Clone(new Rectangle(0, 0, scale.Width, scale.Height), PixelFormat.Format8bppIndexed);
            TwoType[0].SetResolution(200, 200);
            TmpImage.Dispose();
            scale.Dispose();

            TwoType[1] = GetControlImage(OnlyPicture);
            TwoType[1].SetResolution(200, 200);

            return TwoType;
        }
        private Image RResizeImage(Image img, int iWidth, int iHeight)
        {
            Bitmap bmp = new Bitmap(iWidth, iHeight);
            Graphics graphic = Graphics.FromImage((Image)bmp);
            graphic.DrawImage(img, 0, 0, iWidth, iHeight);

            return (Image)bmp;
        }
        private Bitmap ExportPicture(Panel ctl)
        {


            newImage = GetControlImage(ctl);
            var scale = GrayScale(newImage);
            scale = scale.Clone(new Rectangle(0, 0, scale.Width, scale.Height), PixelFormat.Format8bppIndexed);
            scale.SetResolution(200, 200);
            newImage.Dispose();
            return scale;

        }
    }
}
