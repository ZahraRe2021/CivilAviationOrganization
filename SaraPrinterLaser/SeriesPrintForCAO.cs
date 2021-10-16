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
using SaraPrinterLaser.PaintForm;
using SaraPrinterLaser.LayoutDesignFolder;

namespace SaraPrinterLaser
{
    public partial class SeriesPrintForCAO : Form
    {

        #region Attribute


        private static Dictionary<LayoutDesignTools.ItemsOnTheCards_Text, string> SeriesTextSelection = new Dictionary<LayoutDesignTools.ItemsOnTheCards_Text, string>();
        private static Dictionary<LayoutDesignTools.ItemsOnTheCards_Picture, string> SeriesImageSelection = new Dictionary<LayoutDesignTools.ItemsOnTheCards_Picture, string>();
        private readonly Dictionary<string, int> DataModel = new Dictionary<string, int>();
        private readonly LayoutDesignTools.CardType SeriesPrintCardType = new LayoutDesignTools.CardType();

        private static CrewMemberCertificate_C_M_C_ CMCCardItems = new CrewMemberCertificate_C_M_C_();
      //  private static UltraLightLicenceCard ULCardItems = new UltraLightLicenceCard();
       // private static CivilAviationInspectorCertificate InpectorCardItems = new CivilAviationInspectorCertificate();

        private job test;
        private Worker workerprint;
        private static int DataPrintID { get; set; }
        private bool PrintCancel { get; set; }
        private int PrintMax = 0;
        private static int CardholderSelection = 0;

        int DataRowOnPrint = 0;
        private bool ShowOnce;



        #endregion



        public SeriesPrintForCAO(LayoutDesignTools.CardType _InputCardType, CrewMemberCertificate_C_M_C_ _CMCCardItems, Dictionary<LayoutDesignTools.ItemsOnTheCards_Text, string> _SeriesTextSelection, Dictionary<LayoutDesignTools.ItemsOnTheCards_Picture, string> _SeriesImageSelection, int DatabaseId, int _CardholderSelection)
        {
            InitializeComponent();

            SeriesTextSelection = _SeriesTextSelection;
            SeriesImageSelection = _SeriesImageSelection;
            SeriesPrintCardType = _InputCardType;
            DataPrintID = DatabaseId;
            CMCCardItems = _CMCCardItems;
            CardholderSelection = _CardholderSelection;
            string DataRead = new dl.InfoModel().InfoDataByID(DatabaseId);

            int position = 0;
            foreach (var item in DataRead.Split('ß'))
            {
                DataModel.Add(item, position);
                position++;
            }
        }
        //public SeriesPrintForCAO(LayoutDesignTools.CardType _InputCardType, CivilAviationInspectorCertificate _INSCardItems, Dictionary<LayoutDesignTools.ItemsOnTheCards_Text, string> _SeriesTextSelection, Dictionary<LayoutDesignTools.ItemsOnTheCards_Picture, string> _SeriesImageSelection)
        //{
        //    InitializeComponent();
        //}
        //public SeriesPrintForCAO(LayoutDesignTools.CardType _InputCardType, UltraLightLicenceCard _ULCardItems, Dictionary<LayoutDesignTools.ItemsOnTheCards_Text, string> _SeriesTextSelection, Dictionary<LayoutDesignTools.ItemsOnTheCards_Picture, string> _SeriesImageSelection)
        //{
        //    InitializeComponent();
        //}


        private void PrintDataOnTheCard(bool rotateactive, bool cractive, int cardholder)
        {
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
                            if (cractive && Config.RFIDState) test = new job(job.jobModel.PrintCard1);
                            else test = new job(job.jobModel.PrintCard1_c);
                        }
                        else
                        {
                            if (cractive) test = new job(job.jobModel.PrintCard1_r);
                            else test = new job(job.jobModel.PrintCard1_c_r);
                        }
                    }
                    else
                    {
                        if (rotateactive && Config.RotateState)
                        {
                            if (cractive && Config.RFIDState) test = new job(job.jobModel.PrintCard2);
                            else test = new job(job.jobModel.PrintCard2_c);
                        }
                        else
                        {
                            if (cractive) test = new job(job.jobModel.PrintCard2_r);
                            else test = new job(job.jobModel.PrintCard2_c_r);
                        }

                    }
                    FileWork.ClearAnswer();
                    Worker.newjob = true;
                    Worker.myjob = test;
                    Worker.Active = true;
                    Worker.myjob.done = new string[0] { };
                    Worker.Error = new string[0];
                    Worker.myjob.Status = job.StatusList.startPrint;
                    LayoutDesignTools.flgDefinedCardPrinted = true;
                    workerprint = new Worker();
                    PbarPrint.Minimum = 0;
                    PbarPrint.Value1 = 0;
                    PbarPrint.Maximum = Worker.myjob.WorkList.Length;
                    PbarPrint.Text = string.Format("{0}/{1}", PbarPrint.Value1, PbarPrint.Maximum);
                    for (int i = 0; i < Worker.myjob.WorkList.Length; i++)
                    {
                        Label lbl = new Label
                        {
                            Height = 35,
                            Text = Worker.myjob.WorkList[i]
                        };
                        flpProcessName.Controls.Add(lbl);
                    }
                    workerprint.main.Start();
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

        private void InsertDataOntheCard(string[] dataget)
        {
            foreach (var item in SeriesTextSelection.Keys)
            {
                int DataPosition = 0;
                if (SeriesTextSelection.TryGetValue(item, out string Columns))
                {
                    if (DataModel.TryGetValue(Columns, out DataPosition))
                    {
                        switch (item)
                        {
                            //case LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableTopItem_Name:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_PersianVariableTopItem_Name:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableTopItem_Nationality:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableTopItem_DateOfBrith:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableTopItem_CardNo:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableBottomItem_DateOfExpiry:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_PersianVariableBottomItem_DateOfExpiry:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableBottomItem_MRZ0:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableBottomItem_MRZ1:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.CivilAviationInspectorCertificate_LatinVariableBottomItem_MRZ2:
                            //    break;
                            case LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Surname:
                                CMCCardItems.VariableTopItem_Surname.EntryText = dataget[DataPosition];
                                break;
                            case LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_GivenName:
                                CMCCardItems.VariableTopItem_GivenName.EntryText = dataget[DataPosition];
                                break;
                            case LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Sex:
                                CMCCardItems.VariableTopItem_Sex.EntryText = dataget[DataPosition];
                                break;
                            case LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Nationality:
                                CMCCardItems.VariableTopItem_Nationality.EntryText = dataget[DataPosition];
                                break;
                            case LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_DateOfBrith:
                                CMCCardItems.VariableTopItem_DateOfBrith.EntryText = dataget[DataPosition];
                                break;
                            case LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Employedby:
                                CMCCardItems.VariableTopItem_Employedby.EntryText = dataget[DataPosition];
                                break;
                            case LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_Occupation:
                                CMCCardItems.VariableTopItem_Occupation.EntryText = dataget[DataPosition];
                                break;
                            case LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_DocNo:
                                CMCCardItems.VariableTopItem_DocNo.EntryText = dataget[DataPosition];
                                break;
                            case LayoutDesignTools.ItemsOnTheCards_Text.CrewMemberCertificate_C_M_C_VariableTopItem_DateOfExpiry:
                                CMCCardItems.VariableTopItem_DateOfExpiry.EntryText = dataget[DataPosition];
                                break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_Number:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_Name:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_DateOfBrith:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_Nationality:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_DateOfIssue:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_DateOfExpiry:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableTopItem_Authority:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableBottomItem_Remarks:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Text.UltraLightLicenceCard_VariableBottomItem_Ratings:
                            //    break;
                            default:
                                break;
                        }
                    }
                }
            }
            foreach (var item in SeriesImageSelection.Keys)
            {
                int DataPosition = 0;
                if (SeriesImageSelection.TryGetValue(item, out string Columns))
                {
                    if (DataModel.TryGetValue(Columns, out DataPosition))
                    {
                        switch (item)
                        {
                            //case LayoutDesignTools.ItemsOnTheCards_Picture.CivilAviationInspectorCertificate_VariableTopItem_PersonalPicture:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Picture.CivilAviationInspectorCertificate_VariableTopItem_SingniturePicture:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Picture.CivilAviationInspectorCertificate_VariableBottomItem_IssuingAuthoritySingniturePicture:
                            //    break;
                            case LayoutDesignTools.ItemsOnTheCards_Picture.CrewMemberCertificate_C_M_C_VariableTopItem_PersonalPicture:
                                CMCCardItems.VariableTopItem_PersonalPicture.EntryPicturePath = dataget[DataPosition];
                                break;
                            case LayoutDesignTools.ItemsOnTheCards_Picture.CrewMemberCertificate_C_M_C_VariableTopItem_SingniturePicture:
                                CMCCardItems.VariableTopItem_SingniturePicture.EntryPicturePath = dataget[DataPosition];
                                break;
                            //case LayoutDesignTools.ItemsOnTheCards_Picture.CrewMemberCertificate_C_M_C_VariableBottomItem_IssuingAuthoritySingniturePicture:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Picture.UltraLightLicenceCard_VariableTopItem_PersonalPicture:
                            //    break;
                            //case LayoutDesignTools.ItemsOnTheCards_Picture.UltraLightLicenceCard_VariableTopItem_SingniturePicture:
                            //    break;
                            default:
                                break;
                        }
                    }
                }
            }
            switch (SeriesPrintCardType)
            {
                case LayoutDesignTools.CardType.UltraLightLicence:
                    break;
                case LayoutDesignTools.CardType.CrewMemberCertificate_C_M_C_:
                    {
                        string[] TextItems = new string[0];
                        string[] ImagePath = new string[2];
                        string[] ImageBase64 = new string[2];
                        string[] TextOfMRZ = new string[3];
                        StatusClass ReturnStatus = new CrewMemberCertificate_C_M_C_().ArrangeItemsOnCard(CMCCardItems, ref TextOfMRZ, ref TextItems, ref ImagePath, ref ImageBase64);
                        if (ReturnStatus.ResponseReturnStatus == StatusClass.ResponseStatus.Ok) PrintDataOnTheCard(true, false, CardholderSelection);
                        else
                            this.Close();


                    }
                    break;
                case LayoutDesignTools.CardType.CivilAviationSafetyInspectorCertificate:
                    break;
                case LayoutDesignTools.CardType.CivilAviationSecurityInspectorCertificate:
                    break;
                default:
                    break;
            }



        }
        private void PrintingPageSeries_Load(object sender, EventArgs e)
        {
            int datacount = new dl.InfoData().CountDataByInfoModelID(DataPrintID);
            if (PrintMax < datacount) PrintMax = datacount;

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
            DataSet dset = new dl.InfoData().FirstPrint(DataPrintID);
            try
            {
                DataRowOnPrint = Convert.ToInt32(dset.Tables[0].Rows[0]["ID"]);
                InsertDataOntheCard(dset.Tables[0].Rows[0]["data"].ToString().Split('ß'));
                
            }
            catch (Exception)
            {
                MessageBox.Show("اطلاعاتی برای چاپ وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void TimerStatus_Tick(object sender, EventArgs e)
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
                            Label a = new Label
                            {
                                Text = Worker.Error[i],
                                Height = 20,
                                AutoSize = false,
                                Width = 500
                            };
                            flpError.Controls.Add(a);
                            Worker.Error[i] = "##" + Worker.Error[i];

                        }
                        else
                        {
                            Label a = new Label
                            {
                                Text = Worker.Error[i],
                                Height = 20,
                                AutoSize = false,
                                Width = 500
                            };
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
                    PictureBox pic = new PictureBox
                    {
                        ImageLocation = "gif/check.jpg",
                        Width = 20,
                        Height = 28,
                        SizeMode = PictureBoxSizeMode.Zoom
                    };
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
                        MessageBox.Show("لطفا کارت در مخزن قرار دهید", "اعلام", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    new dl.InfoData().FirstPrintSave(DataRowOnPrint);
                    DataSet dset = new dl.InfoData().FirstPrint(DataPrintID);
                    try
                    {
                        DataRowOnPrint = Convert.ToInt32(dset.Tables[0].Rows[0]["ID"]);
                        InsertDataOntheCard(dset.Tables[0].Rows[0]["data"].ToString().Split('ß'));
                    }
                    catch (Exception)
                    {

                    }

                }
            }
            else
            {
                new dl.InfoData().FirstPrintSave(DataRowOnPrint);
                PbarCount.Value1 = PbarCount.Maximum;
                PbarCount.Text = string.Format("{0}/{1}  فرایند چاپ کامل شد", PbarCount.Maximum, PbarCount.Maximum);
                TimerCreatePicture.Stop();
                MessageBox.Show("چاپ با موفقیت به پایان رسید", "اعلام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOk.Enabled = true;
                this.Close();
            }
        }
    }
}
