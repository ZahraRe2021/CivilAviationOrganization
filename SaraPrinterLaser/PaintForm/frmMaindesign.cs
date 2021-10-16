using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.PropertyGridInternal;
using System.Xml;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using SaraPrinterLaser.Properties;
using SaraPrinterLaser.Hardware;
using SaraPrinterLaser.bl;
using System.Security.Principal;
using System.Diagnostics;
using EncryptionClass;
using System.Data.OleDb;
using SaraPrinterLaser.LayoutDesignFolder;

namespace SaraPrinterLaser.PaintForm
{
    public partial class frmMaindesign : Form
    {
        #region SaveVaribales
        public static bool flgSaveFlag { get; set; }
        public static bool flgSelectPicture { get; set; }
        public static string SaveFilePath { get; set; }
        public static string PicDatabase64 = "";
        public static string[] Actions = new string[0];
        public static Control[] CtrlActions = new Control[0];
        public static Control[] tmpCtrlActions = new Control[0];
        public enum DesignAction
        {
            TopOpenFile,
            BottomOpenFile,
            TopOpenPicture,
            BottomOpenPicture,

            TopCut,
            TopPaste,
            TopBringToFront,
            TopBringToBack,
            TopDelete,

            BottomCut,
            BottomPaste,
            BottomBringToFront,
            BottomBringToBack,
            BottomDelete,

            TopPutOLable,
            TopPutLabel,
            TopPutPicture,
            TopPutBarcode,

            BottomPutOLable,
            BottomPutLabel,
            BottomPutPicture,
            BottomPutBarcode,

            TopPropertyActGrid,
            BottomPropertyActGrid,

            TopMoveCTRL,
            BottomMoveCTRL,

            TopPicP,
            BottomPicP,

        }
        public enum ActionType
        {
            Design,
            Control,
            PropertyGrid,
            Event,
            PutPicture,
        }
        public enum MoveDirections
        {
            Right,
            Left,
            Down,
            Up
        }
        public enum ActSituation
        {
            Before,
            After,
        }
        #endregion
        #region Attribute
        //////////////////////////////////
        /// <summary>
        /// Attributes
        /// </summary>
        /// <returns></returns>
        bool issolo = false;
        Settings tanzim = new Settings();
        bool KeysDeActive = false;
        const int DRAG_HANDLE_SIZE = 1;
        int mouseX, mouseY;
        const int NameLantgh = 5;
        Control SelectedControl;
        Control copiedControl;
        Direction direction;
        Point newLocation;
        Size newSize;
        System.Windows.Forms.ContextMenuStrip cmsPanelMenu = new System.Windows.Forms.ContextMenuStrip();
        private static PixelFormat PxFormat = PixelFormat.Format8bppIndexed;
        // int TabIndex = 0;
        int ImageIndex = 0;
        // string[] gParam = null;
        string[] GeneralParameters = null;
        string[] BarcodesParameters = null;
        string[] PictureBoxesParameters = null;
        string[] LabelsParameters = null;
        string[] OrientationLabelsParameters = null;
        string TempPath = Path.GetTempPath();
        Bitmap MemoryImage;
        private static Random random = new Random();
        bool cutCheck = false;
        bool copyCheck = false;
        private ToolTip tt;
        //private Dictionary<string, string> ImageSelection = new Dictionary<string, string>();
        string ImageSelection = "";
        public string FileName { get; set; }
        // Adding a private font (Win2000 and later)
        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern IntPtr AddFontMemResourceEx(byte[] pbFont, int cbFont, IntPtr pdv, out uint pcFonts);

        // Cleanup of a private font (Win2000 and later)
        [DllImport("gdi32.dll", ExactSpelling = true)]
        internal static extern bool RemoveFontMemResourceEx(IntPtr fh);
        // Some private holders of font information we are loading
        static private IntPtr m_fh = IntPtr.Zero;
        static private PrivateFontCollection m_pfc = null;
        string[] gParamPop = null;
        // To set the SQL parameter.
        List<String> ControlNames = new List<String>();
        enum Direction
        {
            NW,
            N,
            NE,
            W,
            E,
            SW,
            S,
            SE,
            None
        }
        Bitmap newImage = new Bitmap(2023, 1276, PixelFormat.Format1bppIndexed);
        public bool Flag { get; set; } = false;
        public string BackgroundPicturePath { get; set; }
        public Panel pnlMain;
        #endregion
        #region ControlEvents
        public string LayoutName = "";
        /// <summary>
        /// Convert Font to String
        /// </summary>
        /// <returns></returns>
        private string FontToString(Font font)
        {
            return font.FontFamily.Name + ":" + font.Size + ":" + (int)font.Style;
        }
        /// <summary>
        /// Convert String to Font
        /// </summary>
        /// <returns></returns>
        private Font StringToFont(string font)
        {
            string[] parts = font.Split(':');
            if (parts.Length != 3)
                throw new ArgumentException("Not a valid font string", "font");

            Font loadedFont = new Font(parts[0], float.Parse(parts[1]), (FontStyle)int.Parse(parts[2]));
            return loadedFont;
        }
        /// <summary>
        /// RUN time Control Events
        /// </summary>
        /// <returns></returns>
        /// 
        /// <summary>
        /// When Runtime Control Clicks this event triger here we can write our code for runtime control click.
        /// Here in this click event i have Called RunTimeCodeGenerate method this mehtod will create class at runtime and run your code.
        /// </summary>
        /// <returns></returns>
        /// 
        private void control_Click(object sender, EventArgs e)
        {
            flgSaveFlag = true;
            //if (rdoMessage.Checked == true)
            //{
            //    RunTimeCodeGenerate(txtCode.Text.Trim());
            //}
            //else if (rdoDataTable.Checked == true)
            //{
            //    RunTimeCodeGenerate_ReturnTypeDataTable(txtCode.Text.Trim());
            //}
            //else if (rdoXML.Checked == true)
            //{
            //    RunTimeCodeGenerate_ReturnTypeDataTable(txtCode.Text.Trim());
            //}
            //else if (rdoDatabase.Checked == true)
            //{
            //    RunTimeCodeGenerate_ReturnTypeDataTable(txtCode.Text.Trim());
            //}
        }
        /// <summary>
        /// RUN time Control Mouse Enter Event used for Control Move
        /// </summary>
        /// <returns></returns>
        /// 
        private void control_MouseEnter(object sender, EventArgs e)
        {
            timer1.Stop();
            Cursor = Cursors.SizeAll;
            Control control = (Control)sender;
            tt = new ToolTip();
            tt.InitialDelay = 0;
            tt.IsBalloon = true;
            tt.Show(control.Name.ToString(), control);
        }
        /// <summary>
        /// RUN time Control Mouse Leave Event used for Control Move
        /// </summary>
        /// <returns></returns>
        /// 
        private void control_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            tt.Dispose();
            timer1.Start();

        }
        /// <summary>
        /// RUN time Control Mouse Down Event used for Control Move
        /// </summary>
        /// <returns></returns>
        /// 
        private void control_MouseDown(object sender, MouseEventArgs e)
        {
            flgSaveFlag = true;
            KeysDeActive = false;
            if (e.Button == MouseButtons.Left)
            {
                if (tabControl1.SelectedTab == TopDesignTab)
                    PanelRo.Invalidate();  //unselect other control
                else if (tabControl1.SelectedTab == BottomDesignTab)
                    PanelPosht.Invalidate();
                SelectedControl = (Control)sender;
                Control control = (Control)sender;
                SelectedControl.Cursor = Cursors.SizeAll;
                mouseX = -e.X;
                mouseY = -e.Y;

                control.Invalidate();
                DrawControlBorder(sender);
                propertyGrid1.SelectedObject = SelectedControl;

                if (tabControl1.SelectedTab == TopDesignTab)
                    DoAction(DesignAction.TopMoveCTRL, ActSituation.After);
                else if (tabControl1.SelectedTab == BottomDesignTab)
                    DoAction(DesignAction.BottomMoveCTRL, ActSituation.After);

            }
        }
        /// <summary>
        /// RUN time Control Mouse Move Event used for Control Move
        /// </summary>
        /// <returns></returns>
        /// 
        private void control_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                flgSaveFlag = true;
                Control control = (Control)sender;
                Point nextPosition = new Point();

                if (tabControl1.SelectedTab == TopDesignTab)
                    nextPosition = PanelRo.PointToClient(MousePosition);
                else if (tabControl1.SelectedTab == BottomDesignTab)
                    nextPosition = PanelPosht.PointToClient(MousePosition);

                nextPosition.Offset(mouseX, mouseY);
                control.Location = nextPosition;
                Invalidate();
            }
        }
        /// <summary>
        /// RUN time Control Mouse Up Event used for Control Move
        /// </summary>
        /// <returns></returns>
        /// 
        private void control_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                flgSaveFlag = true;
                Control control = (Control)sender;
                Cursor.Clip = System.Drawing.Rectangle.Empty;
                SelectedControl.Cursor = Cursors.Arrow;
                control.Invalidate();
                DrawControlBorder(control);

                SelectedControl = (Control)sender;
                propertyGrid1.SelectedObject = (Control)sender;
                propertyGrid1.Refresh();
                propertyGrid1.Update();
            }
        }
        private void control_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedControl.GetType().ToString() == "System.Windows.Forms.PictureBox")
            {
                flgSaveFlag = true;
                OpenFileDialog PictureBoxSelectPicture = new OpenFileDialog();
                PictureBox SelectPicture = new PictureBox();
                PictureBoxSelectPicture.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;*.bmp";
                PictureBoxSelectPicture.RestoreDirectory = true;
                PictureBoxSelectPicture.Title = "لطفا تصویر را انتخاب نمایید";
                if (PictureBoxSelectPicture.ShowDialog() == DialogResult.OK)
                {

                    SelectPicture = (PictureBox)SelectedControl;
                    SelectPicture.SizeMode = PictureBoxSizeMode.Normal;
                    Bitmap bmp = new Bitmap(PictureBoxSelectPicture.FileName);
                    string Base64Bitmap = ImageToBase64(PictureBoxSelectPicture.FileName);
                    File.WriteAllText(TempPath + "Base64Bitmap//" + PictureBoxSelectPicture.SafeFileName + ".txt", EncryptionClass.EncryptionClass.Encrypt(Base64Bitmap));

                    float DPIX = bmp.HorizontalResolution;
                    float DPIY = bmp.VerticalResolution;
                    double[] PictureSize = WithDPIconvertPxTomm(bmp.Size, DPIX, DPIY);
                    SelectPicture.Size = WithDPIConvermmtoPx(PictureSize[0], PictureSize[1], 200, 200);

                    SelectPicture.Image = RResizeImage(bmp, SelectPicture.Width, SelectPicture.Height);
                    ImageSelection = PictureBoxSelectPicture.SafeFileName + ".txt";
                    SelectPicture.Name = PictureBoxSelectPicture.SafeFileName + ".txt";
                    PicDatabase64 = Base64Bitmap;
                    if (tabControl1.SelectedTab == TopDesignTab)
                        DoAction(DesignAction.TopPicP, ActSituation.After);
                    else if (tabControl1.SelectedTab == BottomDesignTab)
                        DoAction(DesignAction.BottomPicP, ActSituation.After);

                    //try
                    //{
                    //    foreach (var item in ImageSelection.Keys)
                    //    {
                    //        if (item == SelectPicture.Name)
                    //            ImageSelection.Remove(item);
                    //    }
                    //}
                    //catch (Exception)
                    //{


                    //}
                    //ImageSelection.Add(SelectPicture.Name, PictureBoxSelectPicture.FileName);
                }
            }
            else
                MessageBox.Show("لطفا تصویر را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        /// <summary>
        /// Draw a border and drag handles around the control, on mouse down and up.
        /// </summary>
        /// <param name="sender"></param>
        private void DrawControlBorder(object sender)
        {
            Control control = (Control)sender;
            //define the border to be drawn, it will be offset by DRAG_HANDLE_SIZE / 2
            //around the control, so when the drag handles are drawn they will be seem
            //connected in the middle.
            Rectangle Border = new Rectangle(
                new Point(control.Location.X - DRAG_HANDLE_SIZE / 2,
                    control.Location.Y - DRAG_HANDLE_SIZE / 2),
                new Size(control.Size.Width + DRAG_HANDLE_SIZE,
                    control.Size.Height + DRAG_HANDLE_SIZE));
            //define the 8 drag handles, that has the size of DRAG_HANDLE_SIZE
            Rectangle NW = new Rectangle(
                new Point(control.Location.X - DRAG_HANDLE_SIZE,
                    control.Location.Y - DRAG_HANDLE_SIZE),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle N = new Rectangle(
                new Point(control.Location.X + control.Width / 2 - DRAG_HANDLE_SIZE / 2,
                    control.Location.Y - DRAG_HANDLE_SIZE),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle NE = new Rectangle(
                new Point(control.Location.X + control.Width,
                    control.Location.Y - DRAG_HANDLE_SIZE),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle W = new Rectangle(
                new Point(control.Location.X - DRAG_HANDLE_SIZE,
                    control.Location.Y + control.Height / 2 - DRAG_HANDLE_SIZE / 2),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle E = new Rectangle(
                new Point(control.Location.X + control.Width,
                    control.Location.Y + control.Height / 2 - DRAG_HANDLE_SIZE / 2),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle SW = new Rectangle(
                new Point(control.Location.X - DRAG_HANDLE_SIZE,
                    control.Location.Y + control.Height),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle S = new Rectangle(
                new Point(control.Location.X + control.Width / 2 - DRAG_HANDLE_SIZE / 2,
                    control.Location.Y + control.Height),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle SE = new Rectangle(
                new Point(control.Location.X + control.Width,
                    control.Location.Y + control.Height),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));

            //get the form graphic
            Graphics g = PanelRo.CreateGraphics();


            if (tabControl1.SelectedTab == BottomDesignTab)
            {
                g.Dispose();
                g = PanelPosht.CreateGraphics();
            }
            //draw the border and drag handles
            ControlPaint.DrawBorder(g, Border, Color.Gray, ButtonBorderStyle.Dotted);
            ControlPaint.DrawGrabHandle(g, NW, true, true);
            ControlPaint.DrawGrabHandle(g, N, true, true);
            ControlPaint.DrawGrabHandle(g, NE, true, true);
            ControlPaint.DrawGrabHandle(g, W, true, true);
            ControlPaint.DrawGrabHandle(g, E, true, true);
            ControlPaint.DrawGrabHandle(g, SW, true, true);
            ControlPaint.DrawGrabHandle(g, S, true, true);
            ControlPaint.DrawGrabHandle(g, SE, true, true);
            g.Dispose();

        }
        /// <summary>
        /// Get The Print area from panel
        /// </summary>
        /// <returns></returns>
        public void GetPrintArea(Panel pnl)
        {
            MemoryImage = new Bitmap(pnl.Width, pnl.Height);
            Rectangle rect = new Rectangle(0, 0, pnl.Width, pnl.Height);
            pnl.DrawToBitmap(MemoryImage, new Rectangle(0, 0, pnl.Width, pnl.Height));
            pnl.Invalidate();
        }
        /// <summary>
        /// To PrintPaint
        /// </summary>
        /// <returns></returns>
        ////protected override void OnPaint(PaintEventArgs e)
        ////{
        ////    //if (MemoryImage != null)
        ////    //{
        ////    //    e.Graphics.DrawImage(MemoryImage, 0, 0);
        ////    //    base.OnPaint(e);
        ////    //}
        ////}
        #endregion
        #region Timer event
        /// <summary>
        /// Get the direction and display correct cursor
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flgSaveFlag)
            {
                btnSaveit.Enabled = true;
            }
            else
            {
                btnSaveit.Enabled = false;
            }
            #region Get the direction and display correct cursor
            if (SelectedControl != null)
            {
                Point pos = new Point();
                if (tabControl1.SelectedTab == TopDesignTab) pos = PanelRo.PointToClient(MousePosition);
                else if (tabControl1.SelectedTab == BottomDesignTab) pos = PanelPosht.PointToClient(MousePosition);

                //check if the mouse cursor is within the drag handle
                if ((pos.X >= SelectedControl.Location.X - DRAG_HANDLE_SIZE &&
                    pos.X <= SelectedControl.Location.X) &&
                    (pos.Y >= SelectedControl.Location.Y - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y))
                {
                    //   direction = Direction.NW;
                    //  Cursor = Cursors.SizeNWSE;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width + DRAG_HANDLE_SIZE &&
                    pos.Y >= SelectedControl.Location.Y + SelectedControl.Height &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height + DRAG_HANDLE_SIZE))
                {
                    //     direction = Direction.SE;
                    //    Cursor = Cursors.SizeNWSE;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width / 2 - DRAG_HANDLE_SIZE / 2) &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width / 2 + DRAG_HANDLE_SIZE / 2 &&
                    pos.Y >= SelectedControl.Location.Y - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y)
                {
                    //   direction = Direction.N;
                    //   Cursor = Cursors.SizeNS;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width / 2 - DRAG_HANDLE_SIZE / 2) &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width / 2 + DRAG_HANDLE_SIZE / 2 &&
                    pos.Y >= SelectedControl.Location.Y + SelectedControl.Height &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height + DRAG_HANDLE_SIZE)
                {
                    //    direction = Direction.S;
                    //    Cursor = Cursors.SizeNS;
                }
                else if ((pos.X >= SelectedControl.Location.X - DRAG_HANDLE_SIZE &&
                    pos.X <= SelectedControl.Location.X &&
                    pos.Y >= SelectedControl.Location.Y + SelectedControl.Height / 2 - DRAG_HANDLE_SIZE / 2 &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height / 2 + DRAG_HANDLE_SIZE / 2))
                {
                    direction = Direction.W;
                    Cursor = Cursors.SizeWE;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width + DRAG_HANDLE_SIZE &&
                    pos.Y >= SelectedControl.Location.Y + SelectedControl.Height / 2 - DRAG_HANDLE_SIZE / 2 &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height / 2 + DRAG_HANDLE_SIZE / 2))
                {
                    //    direction = Direction.E;
                    //    Cursor = Cursors.SizeWE;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width + DRAG_HANDLE_SIZE) &&
                    (pos.Y >= SelectedControl.Location.Y - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y))
                {
                    //    direction = Direction.NE;
                    //    Cursor = Cursors.SizeNESW;
                }
                else if ((pos.X >= SelectedControl.Location.X - DRAG_HANDLE_SIZE &&
                    pos.X <= SelectedControl.Location.X + DRAG_HANDLE_SIZE) &&
                    (pos.Y >= SelectedControl.Location.Y + SelectedControl.Height - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height + DRAG_HANDLE_SIZE))
                {
                    //   direction = Direction.SW;
                    //   Cursor = Cursors.SizeNESW;
                }
                else
                {
                    //   Cursor = Cursors.Default;
                    //   direction = Direction.None;
                }
            }
            else
            {
                direction = Direction.None;
                Cursor = Cursors.Default;
            }
            #endregion
        }
        #endregion
        #region Functions
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
        public System.Drawing.Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
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

        public static Control CopyControl2(Control Ctrl)
        {
            Control CopiedCtrlPanel = new Control();
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
                                return PasteControlPictureBox;
                                // CopiedCtrlPanel.Controls.Add(PasteControlPictureBox);
                            }
                            catch (Exception)
                            {
                            }
                        }
                        break;

                    case "System.Windows.Forms.Label": //Label
                        {
                            try
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
                                return PasteControlLabel;
                            }
                            catch (Exception)
                            {
                            }
                            //  CopiedCtrlPanel.Controls.Add(PasteControlLabel);
                        }
                        break;

                    case "CustomControl.OrientAbleTextControls.OrientedTextLabel":
                        {
                            try
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
                                return PasteControlOrientedTextLabel;
                                // CopiedCtrlPanel.Controls.Add(PasteControlOrientedTextLabel);

                            }
                            catch (Exception)
                            {


                            }
                        }
                        break;
                    case "DevExpress.XtraEditors.BarCodeControl":
                        {
                            try
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
                                return PasteControlBarcode;
                                //CopiedCtrlPanel.Controls.Add(PasteControlBarcode);

                            }
                            catch (Exception)
                            {


                            }
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
            TwoType[0] = scale.Clone(new Rectangle(0, 0, scale.Width, scale.Height), PxFormat);
            TwoType[0].SetResolution(200, 200);
            TmpImage.Dispose();
            scale.Dispose();

            TwoType[1] = GetControlImage(OnlyPicture);
            TwoType[1].SetResolution(200, 200);

            return TwoType;
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
        public static Point ConvermmtoPx(double XmmLocation, double YmmLocation)
        {
            const int DPI = 200;
            Point Location = new Point();
            double XPos = 0, YPos = 0;
            XPos = (XmmLocation * DPI) / 25.4;
            YPos = (YmmLocation * DPI) / 25.4;
            Location.X = (int)Math.Ceiling(XPos);
            Location.Y = (int)Math.Ceiling(YPos);
            return Location;
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
        public bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);

            }
            catch (UnauthorizedAccessException)
            {
                isAdmin = false;
            }
            catch (Exception)
            {
                isAdmin = false;
            }
            return isAdmin;
        }
        public static Bitmap ReadBase64PictureFromFile(string pathFile)
        {
            string Base64Encrypted = File.ReadAllText(pathFile);
            string NonEncrypted = EncryptionClass.EncryptionClass.Decrypt(Base64Encrypted);
            Bitmap BitmapImage = new Bitmap(new MemoryStream(Convert.FromBase64String(NonEncrypted)));
            return BitmapImage;

        }
        // Funtcion to save Form as XML File to reuse
        private XmlDocument MakeXML(Panel PanelControl)
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

                    string PicBitMapImages = "";
                    if (p.GetType().ToString() == "System.Windows.Forms.PictureBox")
                    {
                        if (File.Exists(Path.GetTempPath() + "Base64Bitmap\\" + p.Name))
                        {
                            PicBitMapImages = EncryptionClass.EncryptionClass.Decrypt(File.ReadAllText(Path.GetTempPath() + "Base64Bitmap\\" + p.Name));
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
        private byte[] imageToByteArray(Bitmap bmp)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            return (byte[])converter.ConvertTo(bmp, typeof(byte[]));
        }
        // To Open a Exisitng Form from XML file.
        private void loadXMLFILE(string Xmltext)
        {

            //MBA
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(Xmltext);
            xml.Save(@"D:\Mehrdad.xml");
            XmlNode xnList = xml.SelectSingleNode("SaraHardwareCompanyLaserPrinter");

            foreach (XmlNode xn in xnList)
            {
                #region FillVariableFromXml
                string CntrlType = xn["Type"].InnerText;
                string CntrlName = xn["Name"].InnerText;
                string CntrlText = xn["Text"].InnerText;
                string CntrlFonts = xn["Fonts"].InnerText;
                string CntrlLocatioX = xn["LocationX"].InnerText;
                string CntrlLocatioY = xn["LocationY"].InnerText;
                string CntrlSizeWidth = xn["SizeWidth"].InnerText;
                string CntrlSizeHeight = xn["SizeHeight"].InnerText;
                string CntrlpicImage = xn["PictureImage"].InnerText;
                string CntrlBackColor = xn["BackColor"].InnerText;
                string CntrlForeColor = xn["ForeColor"].InnerText;
                string CntrlRightToLeft = xn["RightToLeft"].InnerText;
                string CntrlBackgroundImageLayout = xn["BackgroundImageLayout"].InnerText;
                string CntrlCursor = xn["Cursor"].InnerText;
                string CntrlAllowDrop = xn["AllowDrop"].InnerText;
                string CntrlEnable = xn["Enable"].InnerText;
                string CntrlTabIndex = xn["TabIndex"].InnerText;
                string CntrlVisible = xn["Visible"].InnerText;
                string CntrlCausesValidation = xn["CausesValidation"].InnerText;
                string CntrlAnchor = xn["Anchor"].InnerText;
                string CntrlDock = xn["Dock"].InnerText;
                string CntrlMargin = xn["Margin"].InnerText;
                string CntrlPadding = xn["Padding"].InnerText;
                string CntrlMaximumSizeWidth = xn["MaximumSizeWidth"].InnerText;
                string CntrlMaximumSizeHeight = xn["MaximumSizeHeight"].InnerText;
                string CntrlMinimumSizeWidth = xn["MinimumSizeWidth"].InnerText;
                string CntrlMinimumSizeHeight = xn["MinimumSizeHeight"].InnerText;
                string CntrlUseWaitCursor = xn["UseWaitCursor"].InnerText;

                string CntrlBCBoarderStyle = xn["BCBoarderStyle"].InnerText;
                string CntrlBCHorizontalAlignment = xn["BCHorizontalAlignment"].InnerText;
                string CntrlBCHorizontalTextAlignment = xn["BCHorizontalTextAlignment"].InnerText;
                string CntrlBCLookAndFeel = xn["BCLookAndFeel"].InnerText;
                string CntrlBCVerticalAlignment = xn["BCVerticalAlignment"].InnerText;
                string CntrlBCVerticalTextAlignment = xn["BCVerticalTextAlignment"].InnerText;
                string CntrlBCAutoModule = xn["BCAutoModule"].InnerText;
                string CntrlBCImeMode = xn["BCImeMode"].InnerText;
                string CntrlBCModule = xn["BCModule"].InnerText;
                string CntrlBCOrientation = xn["BCOrientation"].InnerText;
                string CntrlBCShowText = xn["BCShowText"].InnerText;
                string CntrlBCSymbology = xn["BCSymbology"].InnerText;
                string CntrlBCtabstop = xn["BCtabstop"].InnerText;
                string CntrlBCbinaryData = xn["BCbinaryData"].InnerText;
                string CntrlBCAllowHtmlTextInToolTip = xn["BCAllowHtmlTextInToolTip"].InnerText;
                string CntrlBCShowToolTips = xn["BCShowToolTips"].InnerText;
                string CntrlBCToolTip = xn["BCToolTip"].InnerText;
                string CntrlBCToolTipIconType = xn["BCToolTipIconType"].InnerText;
                string CntrlBCToolTipTitle = xn["BCToolTipTitle"].InnerText;

                string CntrlCntrlPBBoarderStyle = xn["PBBoarderStyle"].InnerText;
                string CntrlPBWaitOnLoad = xn["PBWaitOnLoad"].InnerText;
                string CntrlPBSizeMode = xn["PBSizeMode"].InnerText;
                string CntrlPBImageLocation = xn["PBImageLocation"].InnerText;

                string CntrllblLabelBoarderStyle = xn["lblLabelBoarderStyle"].InnerText;
                string CntrllblFlatStyle = xn["lblFlatStyle"].InnerText;
                string CntrllblImageAlign = xn["lblImageAlign"].InnerText;
                string CntrllblImageIndex = xn["lblImageIndex"].InnerText;
                string CntrllblimageKey = xn["lblimageKey"].InnerText;
                string CntrllblTextAlign = xn["lblTextAlign"].InnerText;
                string CntrllblUseMnemonic = xn["lblUseMnemonic"].InnerText;
                string CntrllblAutoEllipsis = xn["lblAutoEllipsis"].InnerText;
                string CntrllblUseCompatibleTextRendering = xn["lblUseCompatibleTextRendering"].InnerText;
                string CntrllblAutoSize = xn["lblAutoSize"].InnerText;

                string CntrlOrLblBorderStyle = xn["OrLblBorderStyle"].InnerText;
                string CntrlOrLblFlatStyle = xn["OrLblFlatStyle"].InnerText;
                string CntrlOrLblImageAlign = xn["OrLblImageAlign"].InnerText;
                string CntrlOrLblImageIndex = xn["OrLblImageIndex"].InnerText;
                string CntrlOrLblImageKey = xn["OrLblImageKey"].InnerText;
                string CntrlOrLblRotationAngle = xn["OrLblRotationAngle"].InnerText;
                string CntrlOrLblTextAlign = xn["OrLblTextAlign"].InnerText;
                string CntrlOrLblTextDirection = xn["OrLblTextDirection"].InnerText;
                string CntrlOrLblTextOrientation = xn["OrLblTextOrientation"].InnerText;
                string CntrlOrLblUseMnemonic = xn["OrLblUseMnemonic"].InnerText;
                string CntrlOrLblAutoEllipsis = xn["OrLblAutoEllipsis"].InnerText;
                string CntrlOrLblUseCompatibleTextRendering = xn["OrLblUseCompatibleTextRendering"].InnerText;
                string CntrlOrLblAutoSize = xn["OrLblAutoSize"].InnerText;

                //For grid
                string gridsrowsBackColor = xn["gridsrowsBackColor"].InnerText;
                string gridsAlternaterowsBackColor = xn["gridsAlternaterowsBackColor"].InnerText;
                string gridsheaderColor = xn["gridsheaderColor"].InnerText;
                // For Grid
                #endregion
                if (CntrlType != "System.Windows.Forms.Panel")
                {
                    GeneralParameters = new string[]
                    {
                    CntrlType ,
                    CntrlName ,
                    CntrlText ,
                    CntrlFonts ,
                    CntrlLocatioX,
                    CntrlLocatioY ,
                    CntrlSizeWidth ,
                    CntrlSizeHeight ,
                    CntrlpicImage ,
                    CntrlBackColor ,
                    CntrlForeColor ,
                    CntrlRightToLeft,
                    CntrlBackgroundImageLayout,
                    CntrlCursor ,
                    CntrlAllowDrop ,
                    CntrlEnable ,
                    CntrlTabIndex,
                    CntrlVisible ,
                    CntrlCausesValidation ,
                    CntrlAnchor ,
                    CntrlDock ,
                    CntrlMargin ,
                    CntrlPadding ,
                    CntrlMaximumSizeWidth ,
                    CntrlMaximumSizeHeight ,
                    CntrlMinimumSizeWidth ,
                    CntrlMinimumSizeHeight ,
                    CntrlUseWaitCursor,
                    gridsrowsBackColor,
                    gridsAlternaterowsBackColor,
                    gridsheaderColor
                };
                    BarcodesParameters = new string[]
                    {
                    CntrlBCBoarderStyle ,
                    CntrlBCHorizontalAlignment ,
                    CntrlBCHorizontalTextAlignment ,
                    CntrlBCLookAndFeel ,
                    CntrlBCVerticalAlignment ,
                    CntrlBCVerticalTextAlignment ,
                    CntrlBCAutoModule ,
                    CntrlBCImeMode ,
                    CntrlBCModule ,
                    CntrlBCOrientation ,
                    CntrlBCShowText ,
                    CntrlBCSymbology ,
                    CntrlBCtabstop ,
                    CntrlBCbinaryData ,
                    CntrlBCAllowHtmlTextInToolTip ,
                    CntrlBCShowToolTips ,
                    CntrlBCToolTip ,
                    CntrlBCToolTipIconType ,
                    CntrlBCToolTipTitle
                };
                    PictureBoxesParameters = new string[]
                    {
                    CntrlCntrlPBBoarderStyle,
                    CntrlPBWaitOnLoad ,
                    CntrlPBSizeMode,
                    CntrlPBImageLocation,
                    CntrlpicImage
                    };
                    LabelsParameters = new string[]
                    {
                    CntrllblLabelBoarderStyle,
                    CntrllblFlatStyle,
                    CntrllblImageAlign,
                    CntrllblImageIndex ,
                    CntrllblimageKey ,
                    CntrllblTextAlign ,
                    CntrllblUseMnemonic,
                    CntrllblAutoEllipsis,
                    CntrllblUseCompatibleTextRendering,
                    CntrllblAutoSize
                };
                    OrientationLabelsParameters = new string[]
                    {
                    CntrlOrLblBorderStyle ,
                    CntrlOrLblFlatStyle ,
                    CntrlOrLblImageAlign ,
                    CntrlOrLblImageIndex ,
                    CntrlOrLblImageKey ,
                    CntrlOrLblRotationAngle,
                    CntrlOrLblTextAlign ,
                    CntrlOrLblTextDirection ,
                    CntrlOrLblTextOrientation,
                    CntrlOrLblUseMnemonic ,
                    CntrlOrLblAutoEllipsis ,
                    CntrlOrLblUseCompatibleTextRendering ,
                    CntrlOrLblAutoSize
                };
                    loadShanuLabelDesign();
                }
            }

        }
        private void loadShanuLabelDesign()
        {
            switch (GeneralParameters[0])
            {

                case "System.Windows.Forms.PictureBox":
                    {
                        PictureBox PictureBoxControl = new PictureBox();
                        Color CntrlBackColorPictureBox = new Color();
                        PictureBoxControl.Name = GeneralParameters[1];
                        //PictureBoxControl.Text==>Nothing
                        //PictureBoxControl.Font==>Nothing
                        PictureBoxControl.Location = new Point((int.Parse(GeneralParameters[4])), (int.Parse(GeneralParameters[5])));
                        PictureBoxControl.Size = new Size((int.Parse(GeneralParameters[6])), (int.Parse(GeneralParameters[7])));
                        if (!String.IsNullOrWhiteSpace(GeneralParameters[8])) { Bitmap BitmapImage = new Bitmap(new MemoryStream(Convert.FromBase64String(GeneralParameters[8]))); PictureBoxControl.Image = BitmapImage; }
                        CntrlBackColorPictureBox = ColorTranslator.FromHtml(GeneralParameters[9]);
                        PictureBoxControl.BackColor = CntrlBackColorPictureBox;
                        //PictureBoxControl.ForeColor==>Nothing
                        //PictureBoxControl.RightToleft==>Nothing
                        PictureBoxControl.BackgroundImageLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), GeneralParameters[12]);
                        //PictureBoxControl.Cursor /*I don't Know*/
                        //PictureBoxControl.AllowDrop=>Nothing
                        PictureBoxControl.Enabled = bool.Parse(GeneralParameters[15]);
                        //PictureBoxControl.tabIndex==>Nothing
                        PictureBoxControl.Visible = bool.Parse(GeneralParameters[17]);
                        //PictureBoxControl.CausesValidation==>Nothing
                        PictureBoxControl.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), GeneralParameters[19]);
                        PictureBoxControl.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), GeneralParameters[20]);
                        //PictureBoxControl.Margin = StringTomargin;/*I don't Know*/
                        //PictureBoxControl.padding = StringToPadding;/*I don't Know*/
                        PictureBoxControl.MaximumSize = new Size((int.Parse(GeneralParameters[23])), (int.Parse(GeneralParameters[24])));
                        PictureBoxControl.MinimumSize = new Size((int.Parse(GeneralParameters[25])), (int.Parse(GeneralParameters[26])));
                        PictureBoxControl.UseWaitCursor = bool.Parse(GeneralParameters[27]);


                        if (!String.IsNullOrWhiteSpace(PictureBoxesParameters[4]))
                        {
                            try
                            {
                                string TempFolder = Path.GetTempPath();
                                Base64ToImage(PictureBoxesParameters[4]).Save(TempFolder + "LaserPrinterImagetmp\\" + GeneralParameters[1].Replace(".txt", ""), ImageFormat.Bmp);

                                Bitmap bmp = new Bitmap(TempFolder + "LaserPrinterImagetmp\\" + GeneralParameters[1].Replace(".txt", ""));
                                string Base64Bitmap = ImageToBase64(TempFolder + "LaserPrinterImagetmp\\" + GeneralParameters[1].Replace(".txt", ""));
                                File.WriteAllText(TempPath + "Base64Bitmap//" + GeneralParameters[1], EncryptionClass.EncryptionClass.Encrypt(Base64Bitmap));

                                float DPIX = bmp.HorizontalResolution;
                                float DPIY = bmp.VerticalResolution;
                                double[] PictureSize = WithDPIconvertPxTomm(bmp.Size, DPIX, DPIY);
                                PictureBoxControl.Size = WithDPIConvermmtoPx(PictureSize[0], PictureSize[1], 200, 200);

                                PictureBoxControl.Image = RResizeImage(bmp, PictureBoxControl.Width, PictureBoxControl.Height);

                            }
                            catch (Exception)
                            {


                            }
                        }
                        PictureBoxControl.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), PictureBoxesParameters[0]);
                        PictureBoxControl.WaitOnLoad = bool.Parse(PictureBoxesParameters[1]);
                        PictureBoxControl.SizeMode = (PictureBoxSizeMode)Enum.Parse(typeof(PictureBoxSizeMode), PictureBoxesParameters[2]);
                        PictureBoxControl.ImageLocation = PictureBoxesParameters[3];

                        PictureBoxControl.MouseEnter += new EventHandler(control_MouseEnter);
                        PictureBoxControl.MouseLeave += new EventHandler(control_MouseLeave);
                        PictureBoxControl.MouseDown += new MouseEventHandler(control_MouseDown);
                        PictureBoxControl.MouseMove += new MouseEventHandler(control_MouseMove);
                        PictureBoxControl.MouseUp += new MouseEventHandler(control_MouseUp);
                        if (tabControl1.SelectedTab == TopDesignTab)
                            PanelRo.Controls.Add(PictureBoxControl);
                        else if (tabControl1.SelectedTab == BottomDesignTab)
                            PanelPosht.Controls.Add(PictureBoxControl);
                        //  ComboControlNames.Items.Add(gParam[10]);
                    }
                    break;

                case "System.Windows.Forms.Label":
                    {
                        Label LabelControl = new Label();
                        Color CntrlBackColorLabel = new Color();
                        Color CntrlForeColorLabel = new Color();
                        LabelControl.Name = GeneralParameters[1];
                        LabelControl.Text = GeneralParameters[2];
                        Font LabelFont = StringToFont(GeneralParameters[3]);
                        LabelControl.Font = LabelFont;
                        LabelControl.Location = new Point((int.Parse(GeneralParameters[4])), (int.Parse(GeneralParameters[5])));
                        LabelControl.Size = new Size((int.Parse(GeneralParameters[6])), (int.Parse(GeneralParameters[7])));
                        if (!String.IsNullOrWhiteSpace(GeneralParameters[8])) { Bitmap BitmapImage = new Bitmap(new MemoryStream(Convert.FromBase64String(GeneralParameters[8]))); LabelControl.Image = BitmapImage; }
                        CntrlBackColorLabel = ColorTranslator.FromHtml(GeneralParameters[9]);
                        LabelControl.BackColor = CntrlBackColorLabel;
                        CntrlForeColorLabel = ColorTranslator.FromHtml(GeneralParameters[10]);
                        LabelControl.ForeColor = CntrlForeColorLabel;
                        LabelControl.RightToLeft = (RightToLeft)Enum.Parse(typeof(RightToLeft), GeneralParameters[11]);
                        LabelControl.BackgroundImageLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), GeneralParameters[12]);
                        //LabelControl.Cursor /*I don't Know*/
                        LabelControl.AllowDrop = bool.Parse(GeneralParameters[14]);
                        LabelControl.Enabled = bool.Parse(GeneralParameters[15]);
                        LabelControl.TabIndex = int.Parse(GeneralParameters[16]);
                        LabelControl.Visible = bool.Parse(GeneralParameters[17]);
                        LabelControl.CausesValidation = bool.Parse(GeneralParameters[18]);
                        LabelControl.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), GeneralParameters[19]);
                        LabelControl.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), GeneralParameters[20]);
                        //LabelControl.Margin = StringTomargin;/*I don't Know*/
                        //LabelControl.padding = StringToPadding;/*I don't Know*/
                        LabelControl.MaximumSize = new Size((int.Parse(GeneralParameters[23])), (int.Parse(GeneralParameters[24])));
                        LabelControl.MinimumSize = new Size((int.Parse(GeneralParameters[25])), (int.Parse(GeneralParameters[26])));
                        LabelControl.UseWaitCursor = bool.Parse(GeneralParameters[27]);

                        LabelControl.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), LabelsParameters[0]);
                        LabelControl.FlatStyle = (FlatStyle)Enum.Parse(typeof(FlatStyle), LabelsParameters[1]);
                        LabelControl.ImageAlign = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), LabelsParameters[2]);
                        LabelControl.ImageIndex = int.Parse(LabelsParameters[3]);
                        LabelControl.ImageKey = LabelsParameters[4];
                        LabelControl.TextAlign = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), LabelsParameters[5]);
                        LabelControl.UseMnemonic = bool.Parse(LabelsParameters[6]);
                        LabelControl.AutoEllipsis = bool.Parse(LabelsParameters[7]);
                        LabelControl.UseCompatibleTextRendering = bool.Parse(LabelsParameters[8]);
                        LabelControl.AutoSize = bool.Parse(LabelsParameters[9]);

                        LabelControl.MouseEnter += new EventHandler(control_MouseEnter);
                        LabelControl.MouseLeave += new EventHandler(control_MouseLeave);
                        LabelControl.MouseDown += new MouseEventHandler(control_MouseDown);
                        LabelControl.MouseMove += new MouseEventHandler(control_MouseMove);
                        LabelControl.MouseUp += new MouseEventHandler(control_MouseUp);
                        LabelControl.Click += new EventHandler(control_Click);
                        if (tabControl1.SelectedTab == TopDesignTab)
                            PanelRo.Controls.Add(LabelControl);
                        else if (tabControl1.SelectedTab == BottomDesignTab)
                            PanelPosht.Controls.Add(LabelControl);
                    }
                    break;
                case "DevExpress.XtraEditors.BarCodeControl":

                    {

                        DevExpress.XtraEditors.BarCodeControl BarcodeControl = new DevExpress.XtraEditors.BarCodeControl();
                        Color CntrlBackColorBarcodeControl = new Color();
                        Color CntrlForeColorBarcodeControl = new Color();
                        BarcodeControl.Name = GeneralParameters[1];
                        BarcodeControl.Text = GeneralParameters[2];
                        Font BarcodeControlFont = StringToFont(GeneralParameters[3]);
                        BarcodeControl.Font = BarcodeControlFont;
                        BarcodeControl.Location = new Point((int.Parse(GeneralParameters[4])), (int.Parse(GeneralParameters[5])));
                        BarcodeControl.Size = new Size((int.Parse(GeneralParameters[6])), (int.Parse(GeneralParameters[7])));
                        //BarcodeControl.Image ==>Nothing
                        CntrlBackColorBarcodeControl = ColorTranslator.FromHtml(GeneralParameters[9]);
                        BarcodeControl.BackColor = CntrlBackColorBarcodeControl;
                        CntrlForeColorBarcodeControl = ColorTranslator.FromHtml(GeneralParameters[10]);
                        BarcodeControl.ForeColor = CntrlForeColorBarcodeControl;
                        BarcodeControl.RightToLeft = (RightToLeft)Enum.Parse(typeof(RightToLeft), GeneralParameters[11]);
                        BarcodeControl.BackgroundImageLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), GeneralParameters[12]);
                        //BarcodeControl.Cursor /*I don't Know*/
                        BarcodeControl.AllowDrop = bool.Parse(GeneralParameters[14]);
                        BarcodeControl.Enabled = bool.Parse(GeneralParameters[15]);
                        BarcodeControl.TabIndex = int.Parse(GeneralParameters[16]);
                        BarcodeControl.Visible = bool.Parse(GeneralParameters[17]);
                        BarcodeControl.CausesValidation = bool.Parse(GeneralParameters[18]);
                        BarcodeControl.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), GeneralParameters[19]);
                        BarcodeControl.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), GeneralParameters[20]);
                        //BarcodeControl.Margin = StringTomargin;/*I don't Know*/
                        //BarcodeControl.padding = StringToPadding;/*I don't Know*/
                        BarcodeControl.MaximumSize = new Size((int.Parse(GeneralParameters[23])), (int.Parse(GeneralParameters[24])));
                        BarcodeControl.MinimumSize = new Size((int.Parse(GeneralParameters[25])), (int.Parse(GeneralParameters[26])));
                        BarcodeControl.UseWaitCursor = bool.Parse(GeneralParameters[27]);

                        BarcodeControl.BorderStyle = (DevExpress.XtraEditors.Controls.BorderStyles)Enum.Parse(typeof(DevExpress.XtraEditors.Controls.BorderStyles), BarcodesParameters[0]);
                        BarcodeControl.HorizontalAlignment = (DevExpress.Utils.HorzAlignment)Enum.Parse(typeof(DevExpress.Utils.HorzAlignment), BarcodesParameters[1]);
                        BarcodeControl.HorizontalTextAlignment = (DevExpress.Utils.HorzAlignment)Enum.Parse(typeof(DevExpress.Utils.HorzAlignment), BarcodesParameters[2]);
                        BarcodeControl.VerticalAlignment = (DevExpress.Utils.VertAlignment)Enum.Parse(typeof(DevExpress.Utils.VertAlignment), BarcodesParameters[4]);
                        BarcodeControl.VerticalTextAlignment = (DevExpress.Utils.VertAlignment)Enum.Parse(typeof(DevExpress.Utils.VertAlignment), BarcodesParameters[5]);
                        BarcodeControl.AutoModule = bool.Parse(BarcodesParameters[6]);
                        BarcodeControl.ImeMode = (ImeMode)Enum.Parse(typeof(ImeMode), BarcodesParameters[7]);
                        BarcodeControl.Module = double.Parse(BarcodesParameters[8]);
                        BarcodeControl.Orientation = (DevExpress.XtraPrinting.BarCode.BarCodeOrientation)Enum.Parse(typeof(DevExpress.XtraPrinting.BarCode.BarCodeOrientation), BarcodesParameters[9]);
                        BarcodeControl.ShowText = bool.Parse(BarcodesParameters[10]);
                        switch (BarcodesParameters[11])
                        {
                            case "DevExpress.XtraPrinting.BarCode.CodabarGenerator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.CodabarGenerator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.Industrial2of5Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Industrial2of5Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.Interleaved2of5Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Interleaved2of5Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.Code39Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Code39Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.Code39ExtendedGenerator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Code39ExtendedGenerator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.Code93Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Code93Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.Code93ExtendedGenerator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Code93ExtendedGenerator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.Code128Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Code128Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.Code11Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Code11Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.CodeMSIGenerator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.CodeMSIGenerator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.PostNetGenerator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.PostNetGenerator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.EAN13Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.EAN13Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.UPCAGenerator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.UPCAGenerator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.EAN8Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.EAN8Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.EAN128Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.EAN128Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.UPCSupplemental2Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.UPCSupplemental2Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.UPCSupplemental5Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.UPCSupplemental5Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.UPCE0Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.UPCE0Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.UPCE1Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.UPCE1Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.Matrix2of5Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Matrix2of5Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.PDF417Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.PDF417Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.DataMatrixGenerator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.DataMatrixGenerator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.QRCodeGenerator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.QRCodeGenerator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.IntelligentMailGenerator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.IntelligentMailGenerator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.DataMatrixGS1Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.DataMatrixGS1Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.ITF14Generator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.ITF14Generator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.DataBarGenerator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.DataBarGenerator();
                                break;
                            case "DevExpress.XtraPrinting.BarCode.IntelligentMailPackageGenerator":
                                BarcodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.IntelligentMailPackageGenerator();
                                break;
                            default:
                                break;
                        }
                        BarcodeControl.TabStop = bool.Parse(BarcodesParameters[12]);
                        // BarcodeControl.BinaryData=
                        BarcodeControl.AllowHtmlTextInToolTip = (DevExpress.Utils.DefaultBoolean)Enum.Parse(typeof(DevExpress.Utils.DefaultBoolean), BarcodesParameters[14]);
                        BarcodeControl.ShowToolTips = bool.Parse(BarcodesParameters[15]);
                        BarcodeControl.ToolTip = BarcodesParameters[16];
                        BarcodeControl.ToolTipIconType = (DevExpress.Utils.ToolTipIconType)Enum.Parse(typeof(DevExpress.Utils.ToolTipIconType), BarcodesParameters[17]);
                        BarcodeControl.ToolTipTitle = BarcodesParameters[18];

                        BarcodeControl.MouseEnter += new EventHandler(control_MouseEnter);
                        BarcodeControl.MouseLeave += new EventHandler(control_MouseLeave);
                        BarcodeControl.MouseDown += new MouseEventHandler(control_MouseDown);
                        BarcodeControl.MouseMove += new MouseEventHandler(control_MouseMove);
                        BarcodeControl.MouseUp += new MouseEventHandler(control_MouseUp);
                        BarcodeControl.Click += new EventHandler(control_Click);
                        if (tabControl1.SelectedTab == TopDesignTab)
                            PanelRo.Controls.Add(BarcodeControl);
                        else if (tabControl1.SelectedTab == BottomDesignTab)
                            PanelPosht.Controls.Add(BarcodeControl);

                    }

                    break;

                case "CustomControl.OrientAbleTextControls.OrientedTextLabel":
                    {

                        CustomControl.OrientAbleTextControls.OrientedTextLabel OrientedTextLabelControl = new CustomControl.OrientAbleTextControls.OrientedTextLabel();
                        Color CntrlBackColorOrientedTextLabel = new Color();
                        Color CntrlForeColorOrientedTextLabel = new Color();
                        OrientedTextLabelControl.Name = GeneralParameters[1];
                        OrientedTextLabelControl.Text = GeneralParameters[2];
                        Font OrientedTextLabelControlFont = StringToFont(GeneralParameters[3]);
                        OrientedTextLabelControl.Font = OrientedTextLabelControlFont;
                        OrientedTextLabelControl.Location = new Point((int.Parse(GeneralParameters[4])), (int.Parse(GeneralParameters[5])));
                        OrientedTextLabelControl.Size = new Size((int.Parse(GeneralParameters[6])), (int.Parse(GeneralParameters[7])));
                        if (!String.IsNullOrWhiteSpace(GeneralParameters[8])) { Bitmap BitmapImage = new Bitmap(new MemoryStream(Convert.FromBase64String(GeneralParameters[8]))); OrientedTextLabelControl.Image = BitmapImage; }
                        CntrlBackColorOrientedTextLabel = ColorTranslator.FromHtml(GeneralParameters[9]);
                        OrientedTextLabelControl.BackColor = CntrlBackColorOrientedTextLabel;
                        CntrlForeColorOrientedTextLabel = ColorTranslator.FromHtml(GeneralParameters[10]);
                        OrientedTextLabelControl.ForeColor = CntrlForeColorOrientedTextLabel;
                        OrientedTextLabelControl.RightToLeft = (RightToLeft)Enum.Parse(typeof(RightToLeft), GeneralParameters[11]);
                        OrientedTextLabelControl.BackgroundImageLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), GeneralParameters[12]);
                        //OrientedTextLabelControl.Cursor /*I don't Know*/
                        OrientedTextLabelControl.AllowDrop = bool.Parse(GeneralParameters[14]);
                        OrientedTextLabelControl.Enabled = bool.Parse(GeneralParameters[15]);
                        OrientedTextLabelControl.TabIndex = int.Parse(GeneralParameters[16]);
                        OrientedTextLabelControl.Visible = bool.Parse(GeneralParameters[17]);
                        OrientedTextLabelControl.CausesValidation = bool.Parse(GeneralParameters[18]);
                        OrientedTextLabelControl.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), GeneralParameters[19]);
                        OrientedTextLabelControl.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), GeneralParameters[20]);
                        //OrientedTextLabelControl.Margin = StringTomargin;/*I don't Know*/
                        //OrientedTextLabelControl.padding = StringToPadding;/*I don't Know*/
                        OrientedTextLabelControl.MaximumSize = new Size((int.Parse(GeneralParameters[23])), (int.Parse(GeneralParameters[24])));
                        OrientedTextLabelControl.MinimumSize = new Size((int.Parse(GeneralParameters[25])), (int.Parse(GeneralParameters[26])));
                        OrientedTextLabelControl.UseWaitCursor = bool.Parse(GeneralParameters[27]);

                        OrientedTextLabelControl.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), OrientationLabelsParameters[0]);
                        OrientedTextLabelControl.FlatStyle = (FlatStyle)Enum.Parse(typeof(FlatStyle), OrientationLabelsParameters[1]);
                        OrientedTextLabelControl.ImageAlign = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), OrientationLabelsParameters[2]);
                        OrientedTextLabelControl.ImageIndex = int.Parse(OrientationLabelsParameters[3]);
                        OrientedTextLabelControl.ImageKey = OrientationLabelsParameters[4];
                        OrientedTextLabelControl.RotationAngle = double.Parse(OrientationLabelsParameters[5]);
                        OrientedTextLabelControl.TextAlign = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), OrientationLabelsParameters[6]);
                        OrientedTextLabelControl.TextDirection = (CustomControl.OrientAbleTextControls.Direction)Enum.Parse(typeof(CustomControl.OrientAbleTextControls.Direction), OrientationLabelsParameters[7]);
                        OrientedTextLabelControl.TextOrientation = (CustomControl.OrientAbleTextControls.Orientation)Enum.Parse(typeof(CustomControl.OrientAbleTextControls.Orientation), OrientationLabelsParameters[8]);
                        OrientedTextLabelControl.UseMnemonic = bool.Parse(OrientationLabelsParameters[9]);
                        OrientedTextLabelControl.AutoEllipsis = bool.Parse(OrientationLabelsParameters[10]);
                        OrientedTextLabelControl.UseCompatibleTextRendering = bool.Parse(OrientationLabelsParameters[11]);
                        OrientedTextLabelControl.AutoSize = bool.Parse(OrientationLabelsParameters[12]);

                        OrientedTextLabelControl.MouseEnter += new EventHandler(control_MouseEnter);
                        OrientedTextLabelControl.MouseLeave += new EventHandler(control_MouseLeave);
                        OrientedTextLabelControl.MouseDown += new MouseEventHandler(control_MouseDown);
                        OrientedTextLabelControl.MouseMove += new MouseEventHandler(control_MouseMove);
                        OrientedTextLabelControl.MouseUp += new MouseEventHandler(control_MouseUp);
                        OrientedTextLabelControl.Click += new EventHandler(control_Click);
                        if (tabControl1.SelectedTab == TopDesignTab)
                            PanelRo.Controls.Add(OrientedTextLabelControl);
                        else if (tabControl1.SelectedTab == BottomDesignTab)
                            PanelPosht.Controls.Add(OrientedTextLabelControl);
                    }

                    break;

                default:
                    break;
            }

        }
        /// <summary>
        /// TO Paste New Control to Panel
        /// </summary>
        /// <returns></returns>
        private void PasteNewControl()
        {
            if (tabControl1.SelectedTab == TopDesignTab)
                DoAction(DesignAction.TopPaste, ActSituation.Before);
            else if (tabControl1.SelectedTab == BottomDesignTab)
                DoAction(DesignAction.BottomPaste, ActSituation.Before);
            try
            {
                Random XLocationX = new Random();
                Random YLocationY = new Random();

                switch (copiedControl.GetType().ToString())
                {
                    case "System.Windows.Forms.PictureBox":
                        {
                            try
                            {
                                PictureBox PasteControlPictureBox = new PictureBox();
                                PictureBox CopiedControlPictureBox = copiedControl as PictureBox;
                                PasteControlPictureBox.Name = "PB_" + RandomString(NameLantgh);
                                PasteControlPictureBox.Text = CopiedControlPictureBox.Text;
                                PasteControlPictureBox.Font = CopiedControlPictureBox.Font;
                                if (tabControl1.SelectedTab == TopDesignTab)
                                    PasteControlPictureBox.Location = new Point(XLocationX.Next(PanelRo.Location.X, PanelRo.Size.Width - 250), YLocationY.Next(PanelRo.Location.Y, PanelRo.Size.Height - 200));
                                else if (tabControl1.SelectedTab == BottomDesignTab)
                                    PasteControlPictureBox.Location = new Point(XLocationX.Next(PanelPosht.Location.X, PanelPosht.Size.Width - 250), YLocationY.Next(PanelPosht.Location.Y, PanelPosht.Size.Height - 200));
                                PasteControlPictureBox.BackgroundImageLayout = CopiedControlPictureBox.BackgroundImageLayout;
                                PasteControlPictureBox.Size = CopiedControlPictureBox.Size;
                                PasteControlPictureBox.BackColor = CopiedControlPictureBox.BackColor;
                                PasteControlPictureBox.ForeColor = CopiedControlPictureBox.ForeColor;
                                PasteControlPictureBox.RightToLeft = CopiedControlPictureBox.RightToLeft;
                                PasteControlPictureBox.Cursor = CopiedControlPictureBox.Cursor;
                                PasteControlPictureBox.AllowDrop = CopiedControlPictureBox.AllowDrop;
                                PasteControlPictureBox.Enabled = CopiedControlPictureBox.Enabled;
                                PasteControlPictureBox.TabIndex = TabIndex; TabIndex++;
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

                                PasteControlPictureBox.BringToFront();
                                PasteControlPictureBox.MouseEnter += new EventHandler(control_MouseEnter);
                                PasteControlPictureBox.MouseLeave += new EventHandler(control_MouseLeave);
                                PasteControlPictureBox.MouseDown += new MouseEventHandler(control_MouseDown);
                                PasteControlPictureBox.MouseMove += new MouseEventHandler(control_MouseMove);
                                PasteControlPictureBox.MouseUp += new MouseEventHandler(control_MouseUp);
                                PasteControlPictureBox.MouseDoubleClick += new MouseEventHandler(control_DoubleClick);
                                if (tabControl1.SelectedTab == TopDesignTab)
                                {
                                    PanelRo.Controls.Add(PasteControlPictureBox);

                                }
                                else if (tabControl1.SelectedTab == BottomDesignTab)
                                {
                                    PanelPosht.Controls.Add(PasteControlPictureBox);
                                }
                            }
#pragma warning disable CS0168 // Variable is declared but never used
                            catch (Exception ex) { }
#pragma warning restore CS0168 // Variable is declared but never used

                        }
                        break;

                    case "System.Windows.Forms.Label": //Label
                        {

                            Label PasteControlLabel = new Label();
                            Label CopiedLabel = copiedControl as Label;
                            PasteControlLabel.Name = "Lbl_" + RandomString(NameLantgh);
                            PasteControlLabel.Text = CopiedLabel.Text;
                            PasteControlLabel.Font = CopiedLabel.Font;
                            if (tabControl1.SelectedTab == TopDesignTab)
                                PasteControlLabel.Location = new Point(XLocationX.Next(PanelRo.Location.X, PanelRo.Size.Width - 250), YLocationY.Next(PanelRo.Location.Y, PanelRo.Size.Height - 200));
                            else if (tabControl1.SelectedTab == BottomDesignTab)
                                PasteControlLabel.Location = new Point(XLocationX.Next(PanelPosht.Location.X, PanelPosht.Size.Width - 250), YLocationY.Next(PanelPosht.Location.Y, PanelPosht.Size.Height - 200));
                            PasteControlLabel.Size = CopiedLabel.Size;
                            PasteControlLabel.BackColor = CopiedLabel.BackColor;
                            PasteControlLabel.ForeColor = CopiedLabel.ForeColor;
                            PasteControlLabel.RightToLeft = CopiedLabel.RightToLeft;
                            PasteControlLabel.Cursor = CopiedLabel.Cursor;
                            PasteControlLabel.AllowDrop = CopiedLabel.AllowDrop;
                            PasteControlLabel.Enabled = CopiedLabel.Enabled;
                            PasteControlLabel.TabIndex = TabIndex; TabIndex++;
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
                            PasteControlLabel.ImageIndex = ImageIndex; ImageIndex++;
                            PasteControlLabel.ImageKey = CopiedLabel.ImageKey;
                            PasteControlLabel.TextAlign = CopiedLabel.TextAlign;
                            PasteControlLabel.UseMnemonic = CopiedLabel.UseMnemonic;
                            PasteControlLabel.AutoEllipsis = CopiedLabel.AutoEllipsis;
                            PasteControlLabel.UseCompatibleTextRendering = CopiedLabel.UseCompatibleTextRendering;
                            PasteControlLabel.AutoSize = CopiedLabel.AutoSize;

                            PasteControlLabel.MouseEnter += new EventHandler(control_MouseEnter);
                            PasteControlLabel.MouseLeave += new EventHandler(control_MouseLeave);
                            PasteControlLabel.MouseDown += new MouseEventHandler(control_MouseDown);
                            PasteControlLabel.MouseMove += new MouseEventHandler(control_MouseMove);
                            PasteControlLabel.MouseUp += new MouseEventHandler(control_MouseUp);
                            PasteControlLabel.BringToFront();
                            if (tabControl1.SelectedTab == TopDesignTab)
                            {
                                PanelRo.Controls.Add(PasteControlLabel);
                            }
                            else if (tabControl1.SelectedTab == BottomDesignTab)
                            {
                                PanelPosht.Controls.Add(PasteControlLabel);
                            }
                        }
                        break;

                    case "CustomControl.OrientAbleTextControls.OrientedTextLabel":
                        {
                            CustomControl.OrientAbleTextControls.OrientedTextLabel PasteControlOrientedTextLabel = new CustomControl.OrientAbleTextControls.OrientedTextLabel();
                            CustomControl.OrientAbleTextControls.OrientedTextLabel CopiedControlOrientedTextLabel = copiedControl as CustomControl.OrientAbleTextControls.OrientedTextLabel;
                            PasteControlOrientedTextLabel.Name = "Otxt_" + RandomString(NameLantgh);
                            PasteControlOrientedTextLabel.Text = CopiedControlOrientedTextLabel.Text;
                            PasteControlOrientedTextLabel.Font = CopiedControlOrientedTextLabel.Font;
                            if (tabControl1.SelectedTab == TopDesignTab)
                                PasteControlOrientedTextLabel.Location = new Point(XLocationX.Next(PanelRo.Location.X, PanelRo.Size.Width - 250), YLocationY.Next(PanelRo.Location.Y, PanelRo.Size.Height - 200));
                            else if (tabControl1.SelectedTab == BottomDesignTab)
                                PasteControlOrientedTextLabel.Location = new Point(XLocationX.Next(PanelPosht.Location.X, PanelPosht.Size.Width - 250), YLocationY.Next(PanelPosht.Location.Y, PanelPosht.Size.Height - 200));
                            PasteControlOrientedTextLabel.Size = CopiedControlOrientedTextLabel.Size;
                            PasteControlOrientedTextLabel.BackColor = CopiedControlOrientedTextLabel.BackColor;
                            PasteControlOrientedTextLabel.ForeColor = CopiedControlOrientedTextLabel.ForeColor;
                            PasteControlOrientedTextLabel.RightToLeft = CopiedControlOrientedTextLabel.RightToLeft;
                            PasteControlOrientedTextLabel.Cursor = CopiedControlOrientedTextLabel.Cursor;
                            PasteControlOrientedTextLabel.AllowDrop = CopiedControlOrientedTextLabel.AllowDrop;
                            PasteControlOrientedTextLabel.Enabled = CopiedControlOrientedTextLabel.Enabled;
                            PasteControlOrientedTextLabel.TabIndex = TabIndex; TabIndex++;
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
                            PasteControlOrientedTextLabel.ImageIndex = ImageIndex; ImageIndex++;
                            PasteControlOrientedTextLabel.ImageKey = CopiedControlOrientedTextLabel.ImageKey;
                            PasteControlOrientedTextLabel.RotationAngle = CopiedControlOrientedTextLabel.RotationAngle;
                            PasteControlOrientedTextLabel.TextAlign = CopiedControlOrientedTextLabel.TextAlign;
                            PasteControlOrientedTextLabel.TextDirection = CopiedControlOrientedTextLabel.TextDirection;
                            PasteControlOrientedTextLabel.TextOrientation = CopiedControlOrientedTextLabel.TextOrientation;
                            PasteControlOrientedTextLabel.UseMnemonic = CopiedControlOrientedTextLabel.UseMnemonic;
                            PasteControlOrientedTextLabel.AutoEllipsis = CopiedControlOrientedTextLabel.AutoEllipsis;
                            PasteControlOrientedTextLabel.UseCompatibleTextRendering = CopiedControlOrientedTextLabel.UseCompatibleTextRendering;
                            PasteControlOrientedTextLabel.AutoSize = CopiedControlOrientedTextLabel.AutoSize;

                            PasteControlOrientedTextLabel.MouseEnter += new EventHandler(control_MouseEnter);
                            PasteControlOrientedTextLabel.MouseLeave += new EventHandler(control_MouseLeave);
                            PasteControlOrientedTextLabel.MouseDown += new MouseEventHandler(control_MouseDown);
                            PasteControlOrientedTextLabel.MouseMove += new MouseEventHandler(control_MouseMove);
                            PasteControlOrientedTextLabel.MouseUp += new MouseEventHandler(control_MouseUp);
                            //   PasteControlOrientedTextLabel.LocationChanged += new EventHandler(Control_LocationChanged);
                            PasteControlOrientedTextLabel.BringToFront();
                            if (tabControl1.SelectedTab == TopDesignTab)
                                PanelRo.Controls.Add(PasteControlOrientedTextLabel);
                            else if (tabControl1.SelectedTab == BottomDesignTab)
                                PanelPosht.Controls.Add(PasteControlOrientedTextLabel);
                        }
                        break;
                    case "DevExpress.XtraEditors.BarCodeControl":
                        {
                            DevExpress.XtraEditors.BarCodeControl PasteControlBarcode = new DevExpress.XtraEditors.BarCodeControl();
                            DevExpress.XtraEditors.BarCodeControl CopiedControlBarcode = copiedControl as DevExpress.XtraEditors.BarCodeControl;
                            PasteControlBarcode.Name = "BC_" + RandomString(NameLantgh);
                            PasteControlBarcode.Text = CopiedControlBarcode.Text;
                            PasteControlBarcode.Font = CopiedControlBarcode.Font;
                            if (tabControl1.SelectedTab == TopDesignTab)
                                PasteControlBarcode.Location = new Point(XLocationX.Next(PanelRo.Location.X, PanelRo.Size.Width - 250), YLocationY.Next(PanelRo.Location.Y, PanelRo.Size.Height - 200));
                            else if (tabControl1.SelectedTab == BottomDesignTab)
                                PasteControlBarcode.Location = new Point(XLocationX.Next(PanelPosht.Location.X, PanelPosht.Size.Width - 250), YLocationY.Next(PanelPosht.Location.Y, PanelPosht.Size.Height - 200));
                            PasteControlBarcode.Size = CopiedControlBarcode.Size;
                            PasteControlBarcode.BackColor = CopiedControlBarcode.BackColor;
                            PasteControlBarcode.ForeColor = CopiedControlBarcode.ForeColor;
                            PasteControlBarcode.RightToLeft = CopiedControlBarcode.RightToLeft;
                            PasteControlBarcode.BackgroundImageLayout = CopiedControlBarcode.BackgroundImageLayout;
                            PasteControlBarcode.Cursor = CopiedControlBarcode.Cursor;
                            PasteControlBarcode.AllowDrop = CopiedControlBarcode.AllowDrop;
                            PasteControlBarcode.Enabled = CopiedControlBarcode.Enabled;
                            PasteControlBarcode.TabIndex = TabIndex; TabIndex++;
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


                            PasteControlBarcode.MouseEnter += new EventHandler(control_MouseEnter);
                            PasteControlBarcode.MouseLeave += new EventHandler(control_MouseLeave);
                            PasteControlBarcode.MouseDown += new MouseEventHandler(control_MouseDown);
                            PasteControlBarcode.MouseMove += new MouseEventHandler(control_MouseMove);
                            PasteControlBarcode.MouseUp += new MouseEventHandler(control_MouseUp);
                            PasteControlBarcode.BringToFront();
                            if (tabControl1.SelectedTab == TopDesignTab)
                                PanelRo.Controls.Add(PasteControlBarcode);

                            else if (tabControl1.SelectedTab == BottomDesignTab)
                                PanelPosht.Controls.Add(PasteControlBarcode);
                        }
                        break;



                }


            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                MessageBox.Show("شیء مورد نظر به درستی کپی نشده است لطفا دوباره سعی نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (tabControl1.SelectedTab == TopDesignTab)
                DoAction(DesignAction.TopPaste, ActSituation.After);
            else if (tabControl1.SelectedTab == BottomDesignTab)
                DoAction(DesignAction.BottomPaste, ActSituation.After);
        }
        private Bitmap ExportPicture(Panel ctl)
        {


            newImage = GetControlImage(ctl);
            var scale = GrayScale(newImage);
            scale = scale.Clone(new Rectangle(0, 0, scale.Width, scale.Height), PxFormat);
            scale.SetResolution(200, 200);
            newImage.Dispose();
            return scale;

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
        public Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            //destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public void NewPageRotin()
        {
            Array.Resize<string>(ref Actions, 0);
            Array.Resize<Control>(ref CtrlActions, 0);
            if (tabControl1.SelectedTab == TopDesignTab)
            {
                PanelRo.Controls.Clear();
                propertyGrid1.SelectedObject = null;
                PanelRo.Invalidate();
            }
            else if (tabControl1.SelectedTab == BottomDesignTab)
            {
                PanelPosht.Controls.Clear();
                propertyGrid1.SelectedObject = null;
                PanelPosht.Invalidate();
            }
        }
        public void CutRotin()
        {
            if (SelectedControl != null)
            {
                if (tabControl1.SelectedTab == TopDesignTab)
                    DoAction(DesignAction.TopCut, ActSituation.After);
                else if (tabControl1.SelectedTab == BottomDesignTab)
                    DoAction(DesignAction.BottomCut, ActSituation.After);
                flgSaveFlag = true;
                copiedControl = SelectedControl;
                cutCheck = true;
                toolPastes.Enabled = true;
                pasteCtrlVToolStripMenuItem.Enabled = true;
                //MenuPaste.Enabled = true;
            }
            if (SelectedControl != null)
            {
                flgSaveFlag = true;
                if (tabControl1.SelectedTab == TopDesignTab)
                {
                    PanelRo.Controls.Remove(SelectedControl);
                    propertyGrid1.SelectedObject = null;
                    PanelRo.Invalidate();
                }
                else if (tabControl1.SelectedTab == BottomDesignTab)
                {
                    PanelPosht.Controls.Remove(SelectedControl);
                    propertyGrid1.SelectedObject = null;
                    PanelPosht.Invalidate();
                }
            }
        }
        public void CopyRotin()
        {

            if (SelectedControl != null)
            {
                flgSaveFlag = true;
                copiedControl = SelectedControl;
                if (copiedControl != null)
                {
                    cutCheck = false;
                    copyCheck = true;
                    toolPastes.Enabled = true;
                    pasteCtrlVToolStripMenuItem.Enabled = true;
                    //MenuPaste.Enabled = true;
                }
            }
        }
        public void PasteRotin()
        {

            if (copiedControl != null)
            {
                flgSaveFlag = true;
                PasteNewControl();
                //control.Invalidate();
                if (copyCheck == true)
                {
                    toolPastes.Enabled = true;
                    pasteCtrlVToolStripMenuItem.Enabled = true;
                    //MenuPaste.Enabled = true;
                }
                if (cutCheck == true)
                {
                    toolPastes.Enabled = false;
                    pasteCtrlVToolStripMenuItem.Enabled = true;
                    //MenuPaste.Enabled = false;
                    cutCheck = false;
                }

            }
        }
        public void DeleteRotin()
        {
            if (tabControl1.SelectedTab == TopDesignTab)
            {
                if (SelectedControl != null)
                {
                    if (tabControl1.SelectedTab == TopDesignTab)
                        DoAction(DesignAction.TopDelete, ActSituation.After);
                    else if (tabControl1.SelectedTab == BottomDesignTab)
                        DoAction(DesignAction.BottomDelete, ActSituation.After);
                    flgSaveFlag = true;
                    //  ComboControlNames.Items.Remove(SelectedControl.Name);
                    PanelRo.Controls.Remove(SelectedControl);
                    propertyGrid1.SelectedObject = null;
                    PanelRo.Invalidate();

                }
            }
            else if (tabControl1.SelectedTab == BottomDesignTab)
            {
                if (SelectedControl != null)
                {
                    flgSaveFlag = true;
                    //  ComboControlNames.Items.Remove(SelectedControl.Name);
                    PanelPosht.Controls.Remove(SelectedControl);
                    propertyGrid1.SelectedObject = null;
                    PanelPosht.Invalidate();
                }
            }
        }
        public void DeleteAllRotin()
        {
            if (tabControl1.SelectedTab == TopDesignTab)
            {
                flgSaveFlag = true;
                PanelRo.Controls.Clear();
                propertyGrid1.SelectedObject = null;
                PanelRo.Invalidate();
            }
            else if (tabControl1.SelectedTab == BottomDesignTab)
            {
                flgSaveFlag = true;
                PanelPosht.Controls.Clear();
                propertyGrid1.SelectedObject = null;
                PanelPosht.Invalidate();
            }
        }
        public void XMLSaveToHard(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();

            // string date = dl.DateConvertor.DateConverter(false);
            // date = date.Replace("/", "-");
            // date = date.Replace(":", "-");
            // date = date.Replace(" ", "_");
            // path = path.Replace(".SLM", "  " + date + ".SLM");
            if (tabControl1.SelectedTab == TopDesignTab)
            {
                xmlDoc = MakeXML(PanelRo);
                //File.Create(path);
                File.WriteAllText(path, EncryptionClass.EncryptionClass.Encrypt(xmlDoc.OuterXml));

            }
            else if (tabControl1.SelectedTab == BottomDesignTab)
            {
                xmlDoc = MakeXML(PanelPosht);
                //  File.Create(path);
                File.WriteAllText(path, EncryptionClass.EncryptionClass.Encrypt(xmlDoc.OuterXml));
            }
        }
        public void SaveBitmapToHard(string path)
        {
            string date = dl.DateConvertor.DateConverter(false);
            date = date.Replace("/", "-");
            date = date.Replace(":", "-");
            date = date.Replace(" ", "_");
            path = path.Replace(".bmp", "  " + date + ".bmp");
            if (tabControl1.SelectedTab == TopDesignTab)
            {
                Bitmap bmp = ExportPicture(PanelRo);
                bmp.Save(path);
            }
            else if (tabControl1.SelectedTab == BottomDesignTab)
            {
                Bitmap bmp = ExportPicture(PanelPosht);
                bmp.Save(path);
            }
        }
        public void BitmapLoadFile()
        {

            PictureBox ImportPicture = new PictureBox();
            OpenFileDialog FileOpenBackgroundImage = new OpenFileDialog();
            ImportPicture.Location = new Point(0, 0);
            if (tabControl1.SelectedTab == TopDesignTab)
            {
                ImportPicture.Size = new Size(PanelRo.Width, PanelRo.Height);
                PanelRo.Controls.Add(ImportPicture);
                DoAction(DesignAction.TopOpenPicture, ActSituation.After);
            }
            else if (tabControl1.SelectedTab == BottomDesignTab)
            {
                ImportPicture.Size = new Size(PanelPosht.Width, PanelPosht.Height);
                PanelPosht.Controls.Add(ImportPicture);
                DoAction(DesignAction.BottomOpenPicture, ActSituation.After);
            }
            FileOpenBackgroundImage.RestoreDirectory = true;
            FileOpenBackgroundImage.Multiselect = false;
            FileOpenBackgroundImage.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;*.bmp";
            if (FileOpenBackgroundImage.ShowDialog() == DialogResult.OK)
            {
                ImportPicture.ImageLocation = FileOpenBackgroundImage.FileName;
                ImportPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                ImportPicture.Update();
                ImportPicture.Refresh();
            }
        }
        public void BitmapSaveFile()
        {
            bool BackDis = false;
            if (!radCheckBox1.Checked) BackDis = true;
            if (!BackDis)
                DisableBackgroundImage();
            SaveFileDialog SvFile = new SaveFileDialog();
            SvFile.Filter = "Bitmap File|*.bmp";
            SvFile.RestoreDirectory = true;
            if (SvFile.ShowDialog() == DialogResult.OK)
                SaveBitmapToHard(SvFile.FileName);
            if (!BackDis)
                EnableBackgroundImage();

        }
        public void XmlLoadFile()
        {
            Stopwatch Sw = new Stopwatch();
            try
            {
                OpenFileDialog OpenFileDialogObject = new OpenFileDialog();
                OpenFileDialogObject.RestoreDirectory = true;
                OpenFileDialogObject.Filter = "Sara Hardware Laser Printer File |*.SLM";
                OpenFileDialogObject.Title = "لطفاً فایل مورد نظر را انتخاب نمایید.";
                OpenFileDialogObject.FilterIndex = 1;
                OpenFileDialogObject.Multiselect = false;
                if (OpenFileDialogObject.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Sw.Start();
                        SaveFilePath = OpenFileDialogObject.FileName;
                        string aa = File.ReadAllText(OpenFileDialogObject.FileName);
                        loadXMLFILE(EncryptionClass.EncryptionClass.Decrypt(aa));
                        Sw.Stop();
                        MessageBox.Show("فایل با موفقیت بارگذاری گردید", Sw.ElapsedMilliseconds.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("فایل انتخاب شده معتبر نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show(".داده ها درست ذخیره نشده است لطفادوباره سعی نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void XMLSaveFile()
        {
            flgSaveFlag = false;
            bool BackDis = false;
            if (!radCheckBox1.Checked) BackDis = true;
            if (!BackDis)
                DisableBackgroundImage();
            SaveFileDialog SvFile = new SaveFileDialog();
            SvFile.Filter = "Sara Hardware Laser Printer File |*.SLM";
            SvFile.RestoreDirectory = true;
            if (SvFile.ShowDialog() == DialogResult.OK)
            {
                XMLSaveToHard(SvFile.FileName);
                SaveFilePath = SvFile.FileName;
            }
            if (!BackDis)
                EnableBackgroundImage();
        }
        public void Saveit()
        {
            flgSaveFlag = false;
            if (String.IsNullOrWhiteSpace(SaveFilePath))
            {
                bool BackDis = false;
                if (!radCheckBox1.Checked) BackDis = true;
                if (!BackDis)
                    DisableBackgroundImage();
                SaveFileDialog SvFile = new SaveFileDialog();
                SvFile.Filter = "Sara Hardware Laser Printer File |*.SLM";
                SvFile.RestoreDirectory = true;
                if (SvFile.ShowDialog() == DialogResult.OK)
                {
                    XMLSaveToHard(SvFile.FileName);
                    SaveFilePath = SvFile.FileName;
                }
                if (!BackDis)
                    EnableBackgroundImage();
            }
            else
            {
                bool BackDis = false;
                if (!radCheckBox1.Checked) BackDis = true;
                if (!BackDis)
                    DisableBackgroundImage();
                XMLSaveToHard(SaveFilePath);
                if (!BackDis)
                    EnableBackgroundImage();
            }
        }
        public void DisableBackgroundImage()
        {
            if (radCheckBox1.Checked)
            {
                do
                {
                    radCheckBox1.Checked = false;
                    PanelRo.BackgroundImage = null;
                    PanelPosht.BackgroundImage = null;
                } while (radCheckBox1.Checked);
            }
        }
        public void EnableBackgroundImage()
        {
            if (!radCheckBox1.Checked)
            {
                do
                {
                    radCheckBox1.Checked = true;

                } while (!radCheckBox1.Checked);
            }
        }
        public void PlaceTextonPanel()
        {
            if (tabControl1.SelectedTab == TopDesignTab)
                DoAction(DesignAction.TopPutLabel, ActSituation.Before);
            else if (tabControl1.SelectedTab == BottomDesignTab)
                DoAction(DesignAction.BottomPutLabel, ActSituation.Before);
            flgSaveFlag = true;
            Random XLocationX = new Random();
            Random YLocationY = new Random();
            Label ControlLabel = new Label();
            ControlLabel.Name = "Lbl_" + RandomString(NameLantgh);
            ControlLabel.Text = "شرکت سخت افزار سارا";
            ControlLabel.Font = new System.Drawing.Font("B Nazanin", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            if (tabControl1.SelectedTab == TopDesignTab)
                ControlLabel.Location = new Point(XLocationX.Next(PanelRo.Location.X, PanelRo.Size.Width - 250), YLocationY.Next(PanelRo.Location.Y, PanelRo.Size.Height - 200));
            else if (tabControl1.SelectedTab == BottomDesignTab)
                ControlLabel.Location = new Point(XLocationX.Next(PanelPosht.Location.X, PanelPosht.Size.Width - 250), YLocationY.Next(PanelPosht.Location.Y, PanelPosht.Size.Height - 200));
            ControlLabel.Size = new Size(27, 98);
            ControlLabel.BackColor = Color.Transparent;
            ControlLabel.ForeColor = Color.Black;
            ControlLabel.RightToLeft = RightToLeft.Yes;
            ControlLabel.Cursor = Cursors.Arrow;
            ControlLabel.AllowDrop = false;
            ControlLabel.Enabled = true;
            ControlLabel.TabIndex = TabIndex; TabIndex++;
            ControlLabel.Visible = true;
            ControlLabel.CausesValidation = true;
            ControlLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            ControlLabel.Dock = DockStyle.None;
            ControlLabel.Margin = new Padding(0, 0, 0, 0);
            ControlLabel.Padding = new Padding(0, 0, 0, 0);
            ControlLabel.MaximumSize = new Size(0, 0);
            ControlLabel.MinimumSize = new Size(0, 0);
            ControlLabel.UseWaitCursor = false;

            ControlLabel.BorderStyle = BorderStyle.None;
            ControlLabel.FlatStyle = FlatStyle.Standard;
            ControlLabel.Image = null;
            ControlLabel.ImageAlign = ContentAlignment.MiddleCenter;
            ControlLabel.ImageIndex = ImageIndex; ImageIndex++;
            ControlLabel.ImageKey = "";
            ControlLabel.TextAlign = ContentAlignment.TopRight;
            ControlLabel.UseMnemonic = true;
            ControlLabel.AutoEllipsis = false;
            ControlLabel.UseCompatibleTextRendering = false;
            ControlLabel.AutoSize = true;

            ControlLabel.MouseEnter += new EventHandler(control_MouseEnter);
            ControlLabel.MouseLeave += new EventHandler(control_MouseLeave);
            ControlLabel.MouseDown += new MouseEventHandler(control_MouseDown);
            ControlLabel.MouseMove += new MouseEventHandler(control_MouseMove);
            ControlLabel.MouseUp += new MouseEventHandler(control_MouseUp);
            ControlLabel.BringToFront();
            if (tabControl1.SelectedTab == TopDesignTab)
            {
                PanelRo.Controls.Add(ControlLabel);
                DoAction(DesignAction.TopPutLabel, ActSituation.After);
            }
            else if (tabControl1.SelectedTab == BottomDesignTab)
            {
                PanelPosht.Controls.Add(ControlLabel);
                DoAction(DesignAction.BottomPutLabel, ActSituation.After);
            }
        }
        public void PlaceORTextonPanel()
        {
            if (tabControl1.SelectedTab == TopDesignTab)
                DoAction(DesignAction.TopPutOLable, ActSituation.Before);
            else if (tabControl1.SelectedTab == BottomDesignTab)
                DoAction(DesignAction.BottomPutOLable, ActSituation.Before);
            flgSaveFlag = true;
            Random XLocationX = new Random();
            Random YLocationY = new Random();
            CustomControl.OrientAbleTextControls.OrientedTextLabel ControlOrientedTextLabel = new CustomControl.OrientAbleTextControls.OrientedTextLabel();
            ControlOrientedTextLabel.Name = "Otxt_" + RandomString(NameLantgh);
            ControlOrientedTextLabel.Text = "شرکت سخت افزار سارا";
            ControlOrientedTextLabel.Font = new System.Drawing.Font("B Nazanin", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            if (tabControl1.SelectedTab == TopDesignTab)
                ControlOrientedTextLabel.Location = new Point(XLocationX.Next(PanelRo.Location.X, PanelRo.Size.Width - 250), YLocationY.Next(PanelRo.Location.Y, PanelRo.Size.Height - 200));
            else if (tabControl1.SelectedTab == BottomDesignTab)
                ControlOrientedTextLabel.Location = new Point(XLocationX.Next(PanelPosht.Location.X, PanelPosht.Size.Width - 250), YLocationY.Next(PanelPosht.Location.Y, PanelPosht.Size.Height - 200));
            ControlOrientedTextLabel.Size = new Size(27, 140);
            ControlOrientedTextLabel.BackColor = Color.Transparent;
            ControlOrientedTextLabel.ForeColor = Color.Black;
            ControlOrientedTextLabel.RightToLeft = RightToLeft.Yes;
            ControlOrientedTextLabel.Cursor = Cursors.Arrow;
            ControlOrientedTextLabel.AllowDrop = false;
            ControlOrientedTextLabel.Enabled = true;
            ControlOrientedTextLabel.TabIndex = TabIndex; TabIndex++;
            ControlOrientedTextLabel.Visible = true;
            ControlOrientedTextLabel.CausesValidation = true;
            ControlOrientedTextLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            ControlOrientedTextLabel.Dock = DockStyle.None;
            ControlOrientedTextLabel.Margin = new Padding(0, 0, 0, 0);
            ControlOrientedTextLabel.Padding = new Padding(0, 0, 0, 0);
            ControlOrientedTextLabel.MaximumSize = new Size(0, 0);
            ControlOrientedTextLabel.MinimumSize = new Size(0, 0);
            ControlOrientedTextLabel.UseWaitCursor = false;

            ControlOrientedTextLabel.BorderStyle = BorderStyle.None;
            ControlOrientedTextLabel.FlatStyle = FlatStyle.Standard;
            ControlOrientedTextLabel.Image = null;
            ControlOrientedTextLabel.ImageAlign = ContentAlignment.MiddleCenter;
            ControlOrientedTextLabel.ImageIndex = ImageIndex; ImageIndex++;
            ControlOrientedTextLabel.ImageKey = "";
            ControlOrientedTextLabel.RotationAngle = 90;
            ControlOrientedTextLabel.TextAlign = ContentAlignment.MiddleCenter;
            ControlOrientedTextLabel.TextDirection = CustomControl.OrientAbleTextControls.Direction.Clockwise;
            ControlOrientedTextLabel.TextOrientation = CustomControl.OrientAbleTextControls.Orientation.Rotate;
            ControlOrientedTextLabel.UseMnemonic = true;
            ControlOrientedTextLabel.AutoEllipsis = false;
            ControlOrientedTextLabel.UseCompatibleTextRendering = false;
            ControlOrientedTextLabel.AutoSize = false;

            ControlOrientedTextLabel.MouseEnter += new EventHandler(control_MouseEnter);
            ControlOrientedTextLabel.MouseLeave += new EventHandler(control_MouseLeave);
            ControlOrientedTextLabel.MouseDown += new MouseEventHandler(control_MouseDown);
            ControlOrientedTextLabel.MouseMove += new MouseEventHandler(control_MouseMove);
            ControlOrientedTextLabel.MouseUp += new MouseEventHandler(control_MouseUp);
            //   ControlOrientedTextLabel.LocationChanged += new EventHandler(Control_LocationChanged);
            ControlOrientedTextLabel.BringToFront();
            if (tabControl1.SelectedTab == TopDesignTab)
            {
                PanelRo.Controls.Add(ControlOrientedTextLabel);
                DoAction(DesignAction.TopPutOLable, ActSituation.After);
            }
            else if (tabControl1.SelectedTab == BottomDesignTab)
            {
                PanelPosht.Controls.Add(ControlOrientedTextLabel);
                DoAction(DesignAction.BottomPutOLable, ActSituation.After);
            }
        }
        public void PlacePictureBoxonPanel()
        {
            if (tabControl1.SelectedTab == TopDesignTab)
                DoAction(DesignAction.TopPutPicture, ActSituation.Before);
            else if (tabControl1.SelectedTab == BottomDesignTab)
                DoAction(DesignAction.BottomPutPicture, ActSituation.Before);
            flgSaveFlag = true;
            Random XLocationX = new Random();
            Random YLocationY = new Random();
            PictureBox ControlPictureBox = new PictureBox();
            ControlPictureBox.Name = "PB_" + RandomString(NameLantgh);
            ControlPictureBox.Text = "شرکت سخت افزار سارا";
            ControlPictureBox.Font = new System.Drawing.Font("B Nazanin", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            if (tabControl1.SelectedTab == TopDesignTab)
            {
                ControlPictureBox.Location = new Point(XLocationX.Next(PanelRo.Location.X, PanelRo.Size.Width - 250), YLocationY.Next(PanelRo.Location.Y, PanelRo.Size.Height - 200));
            }
            else if (tabControl1.SelectedTab == BottomDesignTab)
            {
                ControlPictureBox.Location = new Point(XLocationX.Next(PanelPosht.Location.X, PanelPosht.Size.Width - 250), YLocationY.Next(PanelPosht.Location.Y, PanelPosht.Size.Height - 200));
            }

            ControlPictureBox.BackgroundImageLayout = ImageLayout.None;
            ControlPictureBox.Size = new Size(100, 100);
            ControlPictureBox.BackColor = Color.Gray;
            ControlPictureBox.ForeColor = Color.Black;
            ControlPictureBox.RightToLeft = RightToLeft.Yes;
            ControlPictureBox.Cursor = Cursors.Arrow;
            ControlPictureBox.AllowDrop = false;
            ControlPictureBox.Enabled = true;
            ControlPictureBox.TabIndex = TabIndex; TabIndex++;
            ControlPictureBox.Visible = true;
            ControlPictureBox.CausesValidation = true;
            ControlPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            ControlPictureBox.Dock = DockStyle.None;
            ControlPictureBox.Margin = new Padding(0, 0, 0, 0);
            ControlPictureBox.Padding = new Padding(0, 0, 0, 0);
            ControlPictureBox.MaximumSize = new Size(0, 0);
            ControlPictureBox.MinimumSize = new Size(0, 0);
            ControlPictureBox.UseWaitCursor = false;

            ControlPictureBox.BorderStyle = BorderStyle.None;
            ControlPictureBox.WaitOnLoad = false;
            ControlPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;

            ControlPictureBox.BringToFront();
            ControlPictureBox.MouseEnter += new EventHandler(control_MouseEnter);
            ControlPictureBox.MouseLeave += new EventHandler(control_MouseLeave);
            ControlPictureBox.MouseDown += new MouseEventHandler(control_MouseDown);
            ControlPictureBox.MouseMove += new MouseEventHandler(control_MouseMove);
            ControlPictureBox.MouseUp += new MouseEventHandler(control_MouseUp);
            ControlPictureBox.MouseDoubleClick += new MouseEventHandler(control_DoubleClick);
            if (tabControl1.SelectedTab == TopDesignTab)
            {
                PanelRo.Controls.Add(ControlPictureBox);
                DoAction(DesignAction.TopPutPicture, ActSituation.After);
            }
            else if (tabControl1.SelectedTab == BottomDesignTab)
            {
                PanelPosht.Controls.Add(ControlPictureBox);
                DoAction(DesignAction.BottomPutPicture, ActSituation.After);
            }

        }
        public void PlaceBarcodeonPanel()
        {
            if (tabControl1.SelectedTab == TopDesignTab)
                DoAction(DesignAction.TopPutBarcode, ActSituation.Before);
            else if (tabControl1.SelectedTab == BottomDesignTab)
                DoAction(DesignAction.BottomPutBarcode, ActSituation.Before);
            flgSaveFlag = true;
            Random XLocationX = new Random();
            Random YLocationY = new Random();
            DevExpress.XtraEditors.BarCodeControl ControlBarcode = new DevExpress.XtraEditors.BarCodeControl();
            ControlBarcode.Name = "BC_" + RandomString(NameLantgh);
            ControlBarcode.Text = "02188894546";
            ControlBarcode.Font = new System.Drawing.Font("B Nazanin", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            if (tabControl1.SelectedTab == TopDesignTab)
            {
                ControlBarcode.Location = new Point(XLocationX.Next(PanelRo.Location.X, PanelRo.Size.Width - 250), YLocationY.Next(PanelRo.Location.Y, PanelRo.Size.Height - 200));
            }
            else if (tabControl1.SelectedTab == BottomDesignTab)
            {
                ControlBarcode.Location = new Point(XLocationX.Next(PanelPosht.Location.X, PanelPosht.Size.Width - 250), YLocationY.Next(PanelPosht.Location.Y, PanelPosht.Size.Height - 200));
            }

            ControlBarcode.Size = new Size(320, 83);
            ControlBarcode.BackColor = Color.White;
            ControlBarcode.ForeColor = Color.Black;
            ControlBarcode.RightToLeft = RightToLeft.No;
            ControlBarcode.BackgroundImageLayout = ImageLayout.None;
            ControlBarcode.Cursor = Cursors.Arrow;
            ControlBarcode.AllowDrop = false;
            ControlBarcode.Enabled = true;
            ControlBarcode.TabIndex = TabIndex; TabIndex++;
            ControlBarcode.Visible = true;
            ControlBarcode.CausesValidation = true;
            ControlBarcode.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            ControlBarcode.Dock = DockStyle.None;
            ControlBarcode.Margin = new Padding(0, 0, 0, 0);
            ControlBarcode.Padding = new Padding(0, 0, 0, 0);
            ControlBarcode.MaximumSize = new Size(0, 0);
            ControlBarcode.MinimumSize = new Size(0, 0);
            ControlBarcode.UseWaitCursor = false;

            ControlBarcode.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            ControlBarcode.HorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            ControlBarcode.HorizontalTextAlignment = DevExpress.Utils.HorzAlignment.Center;
            ControlBarcode.VerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            ControlBarcode.VerticalTextAlignment = DevExpress.Utils.VertAlignment.Center;
            ControlBarcode.AutoModule = true;
            ControlBarcode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            ControlBarcode.Module = 2;
            ControlBarcode.Orientation = DevExpress.XtraPrinting.BarCode.BarCodeOrientation.Normal;
            ControlBarcode.ShowText = true;
            ControlBarcode.Symbology = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            ControlBarcode.TabStop = false;
            ControlBarcode.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            ControlBarcode.ShowToolTips = true;
            ControlBarcode.ToolTip = "";
            ControlBarcode.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            ControlBarcode.ToolTipTitle = "02188894546";


            ControlBarcode.MouseEnter += new EventHandler(control_MouseEnter);
            ControlBarcode.MouseLeave += new EventHandler(control_MouseLeave);
            ControlBarcode.MouseDown += new MouseEventHandler(control_MouseDown);
            ControlBarcode.MouseMove += new MouseEventHandler(control_MouseMove);
            ControlBarcode.MouseUp += new MouseEventHandler(control_MouseUp);
            ControlBarcode.BringToFront();
            if (tabControl1.SelectedTab == TopDesignTab)
            {
                PanelRo.Controls.Add(ControlBarcode);
                DoAction(DesignAction.TopPutBarcode, ActSituation.After);
            }

            else if (tabControl1.SelectedTab == BottomDesignTab)
            {
                PanelPosht.Controls.Add(ControlBarcode);
                DoAction(DesignAction.BottomPutBarcode, ActSituation.After);
            }
        }
        public void BackgroundImageLoad()
        {


            OpenFileDialog FileOpenBackgroundImage = new OpenFileDialog();
            FileOpenBackgroundImage.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;*.bmp";
            FileOpenBackgroundImage.RestoreDirectory = true;
            FileOpenBackgroundImage.Multiselect = false;
            FileOpenBackgroundImage.FilterIndex = 1;
            try
            {
                if (FileOpenBackgroundImage.ShowDialog() == DialogResult.OK)
                {
                    flgSaveFlag = true;
                    if (tabControl1.SelectedTab == TopDesignTab)
                    {
                        if (!Directory.Exists(@"Images"))
                            Directory.CreateDirectory(@"Images");
                        if (!File.Exists(@"Images\BackgroundPicture_Top.txt"))
                            File.Create(@"Images\BackgroundPicture_Top.txt");
                        else
                            File.WriteAllText(@"Images\BackgroundPicture_Top.txt", "");
                        Bitmap bmpFile = new Bitmap(FileOpenBackgroundImage.FileName);
                        byte[] bytes = imageToByteArray(bmpFile);
                        string PicBitMapImages = Convert.ToBase64String(bytes);
                        string EncrytedPicture = EncryptionClass.EncryptionClass.Encrypt(PicBitMapImages);
                        File.WriteAllText(@"Images\BackgroundPicture_Top.txt", EncrytedPicture);


                        PanelRo.BackgroundImageLayout = ImageLayout.Stretch;
                        PanelRo.BackgroundImage = ReadBase64PictureFromFile(@"Images\BackgroundPicture_Top.txt");
                        radCheckBox1.Checked = true;
                        bmpFile.Dispose();
                        PicBitMapImages = "";
                        EncrytedPicture = "";
                        MessageBox.Show("عکس با موفقیت تغییر یافت", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (tabControl1.SelectedTab == BottomDesignTab)
                    {
                        if (!Directory.Exists(@"Images"))
                            Directory.CreateDirectory(@"Images");
                        if (!File.Exists(@"Images\BackgroundPicture_Bottom.txt"))
                            File.Create(@"Images\BackgroundPicture_Bottom.txt");
                        else
                            File.WriteAllText(@"Images\BackgroundPicture_Bottom.txt", "");
                        Bitmap bmpFile = new Bitmap(FileOpenBackgroundImage.FileName);
                        byte[] bytes = imageToByteArray(bmpFile);
                        string PicBitMapImages = Convert.ToBase64String(bytes);
                        string EncrytedPicture = EncryptionClass.EncryptionClass.Encrypt(PicBitMapImages);
                        File.WriteAllText(@"Images\BackgroundPicture_Bottom.txt", EncrytedPicture);
                        PanelPosht.BackgroundImageLayout = ImageLayout.Stretch;
                        PanelPosht.BackgroundImage = ReadBase64PictureFromFile(@"Images\BackgroundPicture_Bottom.txt");
                        radCheckBox1.Checked = true;
                        bmpFile.Dispose();
                        PicBitMapImages = "";
                        EncrytedPicture = "";
                        MessageBox.Show("عکس با موفقیت تغییر یافت", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    PanelPosht.BackgroundImage = null;
                    PanelPosht.Update();
                    PanelPosht.Refresh();
                    radCheckBox1.Checked = false;
                    PanelRo.BackgroundImage = null;
                    PanelRo.Update();
                    PanelRo.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("عکس زمینه بدرستی انتخاب نشده است.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void DoAction(DesignAction Act, ActSituation ACTSIT)
        {
            if (ACTSIT == ActSituation.After)
            {
                if (Act == DesignAction.TopOpenFile || Act == DesignAction.BottomOpenFile || Act == DesignAction.TopOpenPicture || Act == DesignAction.BottomOpenPicture)
                {
                    Array.Resize<string>(ref Actions, Actions.Length + 1);
                    Array.Resize<Control>(ref CtrlActions, CtrlActions.Length + 1);
                    Actions[Actions.Length - 1] = ActionType.Design.ToString() + " " + Act.ToString();
                }
                else if (Act == DesignAction.TopCut ||
                         Act == DesignAction.BottomCut ||
                         Act == DesignAction.TopDelete ||
                         Act == DesignAction.BottomDelete ||
                         Act == DesignAction.TopBringToFront ||
                         Act == DesignAction.BottomBringToFront ||
                         Act == DesignAction.TopBringToBack ||
                         Act == DesignAction.BottomBringToBack)
                {
                    Array.Resize<string>(ref Actions, Actions.Length + 1);
                    Array.Resize<Control>(ref CtrlActions, CtrlActions.Length + 1);
                    Actions[Actions.Length - 1] = ActionType.Control.ToString() + " " + Act.ToString();
                    CtrlActions[CtrlActions.Length - 1] = CopyControl2(SelectedControl);

                }
                else if (Act == DesignAction.TopPaste ||
                         Act == DesignAction.BottomPaste ||
                         Act == DesignAction.TopPutBarcode ||
                         Act == DesignAction.BottomPutBarcode ||
                         Act == DesignAction.TopPutLabel ||
                         Act == DesignAction.BottomPutLabel ||
                         Act == DesignAction.TopPutOLable ||
                         Act == DesignAction.BottomPutOLable ||
                         Act == DesignAction.TopPutPicture ||
                         Act == DesignAction.BottomPutPicture)
                {
                    bool CtrlExist = false;
                    bool flgCorrectCard = false;
                    int Cnt = 0;
                    foreach (Control item in PanelRo.Controls)
                    {
                        if (tmpCtrlActions.Length > 0)
                        {
                            flgCorrectCard = false;
                            Cnt = 0;
                            for (int i = 0; i < tmpCtrlActions.Length; i++)
                            {
                                Cnt++;
                                if (item.Name == tmpCtrlActions[i].Name)
                                {
                                    flgCorrectCard = true;
                                    Cnt = 0;
                                }
                                if (Cnt == tmpCtrlActions.Length && !flgCorrectCard)
                                {
                                    CtrlExist = true;
                                    Array.Resize<string>(ref Actions, Actions.Length + 1);
                                    Array.Resize<Control>(ref CtrlActions, CtrlActions.Length + 1);
                                    Actions[Actions.Length - 1] = ActionType.Control.ToString() + " " + Act.ToString();
                                    CtrlActions[CtrlActions.Length - 1] = CopyControl2(item);
                                }
                            }
                        }
                        else
                        {
                            CtrlExist = true;
                            Array.Resize<string>(ref Actions, Actions.Length + 1);
                            Array.Resize<Control>(ref CtrlActions, CtrlActions.Length + 1);
                            Actions[Actions.Length - 1] = ActionType.Control.ToString() + " " + Act.ToString();
                            CtrlActions[CtrlActions.Length - 1] = CopyControl2(item);
                        }
                    }
                    if (!CtrlExist)
                    {
                        Cnt = 0;
                        foreach (Control item in PanelPosht.Controls)
                        {
                            if (tmpCtrlActions.Length > 0)
                            {
                                flgCorrectCard = false;
                                Cnt = 0;
                                for (int i = 0; i < tmpCtrlActions.Length; i++)
                                {
                                    Cnt++;
                                    if (item.Name == tmpCtrlActions[i].Name)
                                    {
                                        flgCorrectCard = true;
                                        Cnt = 0;
                                    }
                                    if (Cnt == tmpCtrlActions.Length && !flgCorrectCard)
                                    {
                                        CtrlExist = true;
                                        Array.Resize<string>(ref Actions, Actions.Length + 1);
                                        Array.Resize<Control>(ref CtrlActions, CtrlActions.Length + 1);
                                        Actions[Actions.Length - 1] = ActionType.Control.ToString() + " " + Act.ToString();
                                        CtrlActions[CtrlActions.Length - 1] = CopyControl2(item);
                                    }
                                }
                            }
                            else
                            {
                                CtrlExist = true;
                                Array.Resize<string>(ref Actions, Actions.Length + 1);
                                Array.Resize<Control>(ref CtrlActions, CtrlActions.Length + 1);
                                Actions[Actions.Length - 1] = ActionType.Control.ToString() + " " + Act.ToString();
                                CtrlActions[CtrlActions.Length - 1] = CopyControl2(item);
                            }
                        }

                    }
                }
                else if (Act == DesignAction.TopMoveCTRL ||
                         Act == DesignAction.BottomMoveCTRL)
                {
                    Array.Resize<string>(ref Actions, Actions.Length + 1);
                    Array.Resize<Control>(ref CtrlActions, CtrlActions.Length + 1);
                    Actions[Actions.Length - 1] = ActionType.Event.ToString() + " " + Act.ToString();
                    CtrlActions[CtrlActions.Length - 1] = CopyControl2(SelectedControl);
                }
                else if (Act == DesignAction.TopPicP ||
                         Act == DesignAction.BottomPicP)
                {
                    Array.Resize<string>(ref Actions, Actions.Length + 1);
                    Array.Resize<Control>(ref CtrlActions, CtrlActions.Length + 1);
                    Actions[Actions.Length - 1] = ActionType.PutPicture.ToString() + " " + Act.ToString();
                    CtrlActions[CtrlActions.Length - 1] = CopyControl2(SelectedControl);
                }
            }
            else if (ACTSIT == ActSituation.Before)
            {
                if (Act == DesignAction.TopPaste ||
                         Act == DesignAction.BottomPaste ||
                         Act == DesignAction.TopPutBarcode ||
                         Act == DesignAction.BottomPutBarcode ||
                         Act == DesignAction.TopPutLabel ||
                         Act == DesignAction.BottomPutLabel ||
                         Act == DesignAction.TopPutOLable ||
                         Act == DesignAction.BottomPutOLable ||
                         Act == DesignAction.TopPutPicture ||
                         Act == DesignAction.BottomPutPicture)
                {
                    Array.Resize<Control>(ref tmpCtrlActions, 0);
                    foreach (Control item in PanelRo.Controls)
                    {
                        Array.Resize<Control>(ref tmpCtrlActions, tmpCtrlActions.Length + 1);
                        tmpCtrlActions[tmpCtrlActions.Length - 1] = CopyControl2(item);
                    }
                    foreach (Control item in PanelPosht.Controls)
                    {
                        Array.Resize<Control>(ref tmpCtrlActions, tmpCtrlActions.Length + 1);
                        tmpCtrlActions[tmpCtrlActions.Length - 1] = CopyControl2(item);
                    }

                }
            }
        }
        public void UndoAction()
        {
            if (Actions.Length > 0)
            {
                if (Actions[Actions.Length - 1].Contains(ActionType.Design.ToString()))
                {
                    Actions[Actions.Length - 1] = Actions[Actions.Length - 1].Replace(ActionType.Design.ToString(), "");
                    Actions[Actions.Length - 1] = Actions[Actions.Length - 1].Trim();
                    switch (Actions[Actions.Length - 1])
                    {
                        case "TopOpenFile":
                            {
                                PanelRo.Controls.Clear();
                            }
                            break;
                        case "BottomOpenFile":
                            {
                                PanelPosht.Controls.Clear();
                            }
                            break;
                        case "TopOpenPicture":
                            {
                                PanelRo.Controls.Clear();
                            }
                            break;
                        case "BottomOpenPicture":
                            {
                                PanelPosht.Controls.Clear();
                            }
                            break;
                        default:
                            break;
                    }
                    Array.Resize<string>(ref Actions, Actions.Length - 1);
                    Array.Resize<Control>(ref CtrlActions, CtrlActions.Length - 1);
                }
                else if (Actions[Actions.Length - 1].Contains(ActionType.Control.ToString()))
                {
                    Actions[Actions.Length - 1] = Actions[Actions.Length - 1].Replace(ActionType.Control.ToString(), "");
                    Actions[Actions.Length - 1] = Actions[Actions.Length - 1].Trim();
                    switch (Actions[Actions.Length - 1])
                    {
                        case "TopCut":
                            {
                                tabControl1.SelectedTab = TopDesignTab;
                                PanelRo.Controls.Add(CtrlActions[CtrlActions.Length - 1]);
                            }
                            break;
                        case "BottomCut":
                            {
                                tabControl1.SelectedTab = BottomDesignTab;
                                PanelPosht.Controls.Add(CtrlActions[CtrlActions.Length - 1]);
                            }
                            break;
                        case "TopDelete":
                            {
                                tabControl1.SelectedTab = TopDesignTab;
                                PanelRo.Controls.Add(CtrlActions[CtrlActions.Length - 1]);
                            }
                            break;
                        case "BottomDelete":
                            {
                                tabControl1.SelectedTab = BottomDesignTab;
                                PanelPosht.Controls.Add(CtrlActions[CtrlActions.Length - 1]);
                            }
                            break;
                        case "TopBringToFront":
                            {
                                tabControl1.SelectedTab = TopDesignTab;
                                CtrlActions[CtrlActions.Length - 1].SendToBack();

                            }
                            break;
                        case "BottomBringToFront":
                            {
                                tabControl1.SelectedTab = BottomDesignTab;
                                CtrlActions[CtrlActions.Length - 1].SendToBack();
                            }
                            break;
                        case "TopBringToBack":
                            {
                                tabControl1.SelectedTab = TopDesignTab;
                                CtrlActions[CtrlActions.Length - 1].BringToFront();
                            }
                            break;
                        case "BottomBringToBack":
                            {
                                tabControl1.SelectedTab = BottomDesignTab;
                                CtrlActions[CtrlActions.Length - 1].BringToFront();
                            }
                            break;
                        case "TopPaste":
                            {
                                tabControl1.SelectedTab = TopDesignTab;
                                foreach (Control item in PanelRo.Controls)
                                {
                                    if (item.Name == CtrlActions[CtrlActions.Length - 1].Name)
                                        PanelRo.Controls.Remove(item);
                                }
                            }
                            break;
                        case "BottomPaste":
                            {
                                tabControl1.SelectedTab = BottomDesignTab;
                                foreach (Control item in PanelPosht.Controls)
                                {
                                    if (item.Name == CtrlActions[CtrlActions.Length - 1].Name)
                                        PanelPosht.Controls.Remove(item);
                                }
                            }
                            break;
                        case "TopPutBarcode":
                            {
                                tabControl1.SelectedTab = TopDesignTab;
                                foreach (Control item in PanelRo.Controls)
                                {
                                    if (item.Name == CtrlActions[CtrlActions.Length - 1].Name)
                                        PanelRo.Controls.Remove(item);
                                }
                            }
                            break;
                        case "BottomPutBarcode":
                            {
                                tabControl1.SelectedTab = BottomDesignTab;
                                foreach (Control item in PanelPosht.Controls)
                                {
                                    if (item.Name == CtrlActions[CtrlActions.Length - 1].Name)
                                        PanelPosht.Controls.Remove(item);
                                }
                            }
                            break;
                        case "TopPutLabel":
                            {
                                tabControl1.SelectedTab = TopDesignTab;
                                foreach (Control item in PanelRo.Controls)
                                {
                                    if (item.Name == CtrlActions[CtrlActions.Length - 1].Name)
                                        PanelRo.Controls.Remove(item);
                                }

                            }
                            break;
                        case "BottomPutLabel":
                            {
                                tabControl1.SelectedTab = BottomDesignTab;
                                foreach (Control item in PanelPosht.Controls)
                                {
                                    if (item.Name == CtrlActions[CtrlActions.Length - 1].Name)
                                        PanelPosht.Controls.Remove(item);
                                }
                            }
                            break;
                        case "TopPutOLable":
                            {
                                tabControl1.SelectedTab = TopDesignTab;
                                foreach (Control item in PanelRo.Controls)
                                {
                                    if (item.Name == CtrlActions[CtrlActions.Length - 1].Name)
                                        PanelRo.Controls.Remove(item);
                                }
                            }
                            break;
                        case "BottomPutOLable":
                            {
                                tabControl1.SelectedTab = BottomDesignTab;
                                foreach (Control item in PanelPosht.Controls)
                                {
                                    if (item.Name == CtrlActions[CtrlActions.Length - 1].Name)
                                        PanelPosht.Controls.Remove(item);
                                }
                            }
                            break;
                        case "TopPutPicture":
                            {
                                tabControl1.SelectedTab = TopDesignTab;
                                foreach (Control item in PanelRo.Controls)
                                {
                                    if (item.Name == CtrlActions[CtrlActions.Length - 1].Name)
                                        PanelRo.Controls.Remove(item);
                                }
                            }
                            break;
                        case "BottomPutPicture":
                            {
                                tabControl1.SelectedTab = BottomDesignTab;
                                foreach (Control item in PanelPosht.Controls)
                                {
                                    if (item.Name == CtrlActions[CtrlActions.Length - 1].Name)
                                        PanelPosht.Controls.Remove(item);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    Array.Resize<string>(ref Actions, Actions.Length - 1);
                    Array.Resize<Control>(ref CtrlActions, CtrlActions.Length - 1);
                }
                else if (Actions[Actions.Length - 1].Contains(ActionType.PropertyGrid.ToString()))
                {
                    Actions[Actions.Length - 1] = Actions[Actions.Length - 1].Replace(ActionType.PropertyGrid.ToString(), "");
                    Actions[Actions.Length - 1] = Actions[Actions.Length - 1].Trim();
                    switch (Actions[Actions.Length - 1])
                    {
                        case "TopPropertyActGrid":
                            {
                                foreach (Control item in PanelRo.Controls)
                                {
                                    if (item.Name == CtrlActions[CtrlActions.Length - 1].Name)
                                    {
                                        PanelRo.Controls.Remove(item);
                                        PanelRo.Controls.Add(CtrlActions[CtrlActions.Length - 1]);
                                        SelectedControl = CtrlActions[CtrlActions.Length - 1];
                                        propertyGrid1.SelectedObject = CtrlActions[CtrlActions.Length - 1];
                                        propertyGrid1.Refresh();
                                        propertyGrid1.Update();
                                        Array.Resize<string>(ref Actions, Actions.Length - 1);
                                        Array.Resize<Control>(ref CtrlActions, CtrlActions.Length - 1);
                                    }
                                }
                            }
                            break;
                        case "BottomPropertyActGrid":
                            {
                                foreach (Control item in PanelPosht.Controls)
                                {
                                    if (item.Name == CtrlActions[CtrlActions.Length - 1].Name)
                                    {
                                        PanelPosht.Controls.Remove(item);
                                        PanelPosht.Controls.Add(CtrlActions[CtrlActions.Length - 1]);
                                        SelectedControl = CtrlActions[CtrlActions.Length - 1];
                                        propertyGrid1.SelectedObject = CtrlActions[CtrlActions.Length - 1];
                                        propertyGrid1.Refresh();
                                        propertyGrid1.Update();
                                        Array.Resize<string>(ref Actions, Actions.Length - 1);
                                        Array.Resize<Control>(ref CtrlActions, CtrlActions.Length - 1);
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }

                }
                else if (Actions[Actions.Length - 1].Contains(ActionType.Event.ToString()))
                {
                    Actions[Actions.Length - 1] = Actions[Actions.Length - 1].Replace(ActionType.Event.ToString(), "");
                    Actions[Actions.Length - 1] = Actions[Actions.Length - 1].Trim();
                    switch (Actions[Actions.Length - 1])
                    {
                        case "TopMoveCTRL":
                            {
                                foreach (Control item in PanelRo.Controls)
                                {
                                    if (item.Name == CtrlActions[CtrlActions.Length - 1].Name)
                                    {
                                        item.Location = new Point(CtrlActions[CtrlActions.Length - 1].Location.X, CtrlActions[CtrlActions.Length - 1].Location.Y);
                                        Array.Resize<string>(ref Actions, Actions.Length - 1);
                                        Array.Resize<Control>(ref CtrlActions, CtrlActions.Length - 1);
                                    }
                                }
                            }
                            break;
                        case "BottomMoveCTRL":
                            {
                                foreach (Control item in PanelPosht.Controls)
                                {
                                    if (item.Name == CtrlActions[CtrlActions.Length - 1].Name)
                                    {
                                        item.Location = new Point(CtrlActions[CtrlActions.Length - 1].Location.X, CtrlActions[CtrlActions.Length - 1].Location.Y);
                                        Array.Resize<string>(ref Actions, Actions.Length - 1);
                                        Array.Resize<Control>(ref CtrlActions, CtrlActions.Length - 1);
                                    }
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }
                else if (Actions[Actions.Length - 1].Contains(ActionType.PutPicture.ToString()))
                {
                    //Actions[Actions.Length - 1] = Actions[Actions.Length - 1].Replace(ActionType.PutPicture.ToString(), "");
                    //Actions[Actions.Length - 1] = Actions[Actions.Length - 1].Trim();
                    //if (Actions[Actions.Length - 1].Contains(DesignAction.TopPicP.ToString()))
                    //{
                    //    Actions[Actions.Length - 1] = Actions[Actions.Length - 1].Replace(DesignAction.TopPicP.ToString(), "");
                    //    Actions[Actions.Length - 1] = Actions[Actions.Length - 1].Trim();
                    //}
                    //else if (Actions[Actions.Length - 1].Contains(DesignAction.BottomPicP.ToString()))
                    //{
                    //    Actions[Actions.Length - 1] = Actions[Actions.Length - 1].Replace(DesignAction.BottomPicP.ToString(), "");
                    //    Actions[Actions.Length - 1] = Actions[Actions.Length - 1].Trim();
                    //}
                }
            }
        }
        #endregion
        #region Events
        public frmMaindesign()
        {
            InitializeComponent();
        }
        public void LoadingDevice()
        {
            ConnectingDevice checkPage = new ConnectingDevice();
            if (checkPage.ShowDialog() == DialogResult.OK)
            {

                Config.OpenAllPortExceptLaser();
            }
            else
            {
                MessageBox.Show("دستگاه در دسترس نیست . لطفا ارتباط دستگاه با رایانه بررسی کنید و از روشن بودن چاپگر اطمینان حاصل کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                Environment.Exit(0);
            }
            FileWork.readstate();
            if (FileWork.stateMashin != FileWork.StateOfmashin.Ready)
            {
                switch (FileWork.stateMashin)
                {

                    case FileWork.StateOfmashin.pickupStacker_Start:
                    case FileWork.StateOfmashin.pickupStacker_End:
                    case FileWork.StateOfmashin.DispenserJam_Start:
                    case FileWork.StateOfmashin.DispenserJam_End:
                    case FileWork.StateOfmashin.reciveForMarking_Start:
                        RejectForm frm = new RejectForm("Dispenser");
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            MessageBox.Show("Device Ready now :)");
                        }
                        else
                        {
                            MessageBox.Show("check Printer ...");
                        }
                        frm.Dispose();
                        break;

                    case FileWork.StateOfmashin.reciveForMarking_End:
                    case FileWork.StateOfmashin.MarkingAreaJam_Start:
                    case FileWork.StateOfmashin.MarkingAreaJam_End:
                    case FileWork.StateOfmashin.Rotate_Start:
                    case FileWork.StateOfmashin.Rotate_End:
                    case FileWork.StateOfmashin.MoveCr_Start:



                        RejectForm frm2 = new RejectForm("CardHolder");
                        if (frm2.ShowDialog() == DialogResult.OK)
                        {
                            MessageBox.Show("Device Ready now :)");
                        }
                        else
                        {
                            MessageBox.Show("check Printer ...");
                        }
                        frm2.Dispose();
                        break;
                    //case FileWork.StateOfmashin.MoveCr_End:
                    //case FileWork.StateOfmashin.InCR_Start:
                    //case FileWork.StateOfmashin.InCR_End:
                    //    RejectForm frm3 = new RejectForm("CR");
                    //    if (frm3.ShowDialog() == DialogResult.OK)
                    //    {
                    //        MessageBox.Show("Device Ready now :)");
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("check Printer ...");
                    //    }
                    //    frm3.Dispose();
                    //    break;
                    case FileWork.StateOfmashin.MoveToRejectBox_Start:
                        RejectForm frm4 = new RejectForm("JamMarkingArea");
                        if (frm4.ShowDialog() == DialogResult.OK)
                        {
                            MessageBox.Show("Device Ready now :)");
                        }
                        else
                        {
                            MessageBox.Show("check Printer ...");
                        }
                        frm4.Dispose();
                        break;

                    case FileWork.StateOfmashin.CrJam_Start:
                        RejectForm frm5 = new RejectForm("CardHolder");
                        if (frm5.ShowDialog() == DialogResult.OK)
                        {
                            MessageBox.Show("Device Ready now :)");
                        }
                        else
                        {
                            MessageBox.Show("check Printer ...");
                        }
                        frm5.Dispose();
                        break;

                    case FileWork.StateOfmashin.Printed:
                        FileWork.changeState(FileWork.StateOfmashin.Ready);
                        break;


                    default:
                        break;
                }
            }
        }
        public void EnterUserPassword()
        {

            if (tanzim.IsRemmeber)
            {
                DialogResult DialogQuestion = MessageBox.Show("آیا مایل هستید با نام کاربری قیلی وارد شوید؟", "پیغام", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (DialogQuestion == DialogResult.Yes)
                {
                    string errorLogin = "systemError";
                    if (bl.Auth.CheckUser(tanzim.UserName, EncryptionClass.EncryptionClass.Decrypt(tanzim.PassWord), ref errorLogin))
                    {

                    }
                    else
                    {
                        tanzim.IsRemmeber = false;
                        tanzim.UserName = "";
                        tanzim.PassWord = "";
                    }
                }
                else if (DialogQuestion == DialogResult.No)
                {
                    tanzim.IsRemmeber = false;
                    tanzim.UserName = "";
                    tanzim.PassWord = "";
                    tanzim.Save();
                    LoginForm pagelogin = new LoginForm();
                    var resualtLogin = DialogResult.Abort;
                    while (resualtLogin != DialogResult.OK)
                    {
                        resualtLogin = pagelogin.ShowDialog();
                        if (resualtLogin != DialogResult.OK)
                        {
                            this.Close();
                        }
                    }
                }
                else if (DialogQuestion == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
            else
            {

                LoginForm pagelogin = new LoginForm();
                var resualtLogin = DialogResult.Abort;
                while (resualtLogin != DialogResult.OK)
                {
                    resualtLogin = pagelogin.ShowDialog();
                    if (resualtLogin != DialogResult.OK)
                    {
                        this.Dispose();
                        Environment.Exit(0);
                        Application.Exit();
                        this.Close();

                    }
                }
            }
            new dl.UserLog().Login();
            tanzim.UserLogID = new dl.UserLog().GetUserLogID();
            tanzim.Save();


            switch (tanzim.Role)
            {

                case "SuperUser":

                    break;
                case "Admin":
                    settingToolStripMenuItem.Visible = false;
                    btnSinglePrint.Visible = false;
                    DatabasePrint.Visible = false;
                    break;
                case "User":
                    prameterToolStripMenuItem.Visible = false;
                    settingToolStripMenuItem.Visible = false;
                    btnSinglePrint.Visible = false;
                    DatabasePrint.Visible = false;
                    break;
                default:
                    break;
            }





        }
        public enum WhichPanelTabControl
        {
            enumTopPanelTabControl,
            enumBottomPanelTabControl
        }
        public void SelectTabControl(WhichPanelTabControl _whichPanel)
        {
            if (_whichPanel == WhichPanelTabControl.enumTopPanelTabControl)
                tabControl1.SelectTab(TopDesignTab);
            else if (_whichPanel == WhichPanelTabControl.enumBottomPanelTabControl)
                tabControl1.SelectTab(BottomDesignTab);
        }
        private void Test()
        {

            string[] Text = new string[9];
            string[] ImagePath = new string[2];
            string[] ImageBase64 = new string[2];



            Text[0] = "1130";
            Text[1] = "M. Sadegh MEHRABANI A.";
            Text[2] = "05/02/1984";
            Text[3] = "Iranian";
            Text[4] = "10/12/2018";
            Text[5] = "23/09/2020";
            Text[6] = "ALI ABEDZADEH";
            Text[7] = "Remarks";
            Text[8] = "Ratings";


            ImagePath[0] = "C:\\Users\\Mehrdad\\AppData\\Roaming\\SaraLaserCardPrinter\\UltraLightLicence\\LaserBoardInternalSaveImage\\1400_01_13-_-_-0_36_50_186=_=BfH3OxsF9hBE5sDRF3Px\\Top.Bmp";
            ImagePath[1] = "C:\\Users\\Mehrdad\\AppData\\Roaming\\SaraLaserCardPrinter\\UltraLightLicence\\LaserBoardInternalSaveImage\\1400_01_13-_-_-0_36_51_121=_=JeJNsZNEIj4ijcMB4LOi\\Bottom.Bmp";


            string test = ImageToBase64(@"C:\Users\Mehrdad\AppData\Roaming\SaraLaserCardPrinter\UltraLightLicence\LaserBoardInternalSaveImage\1400_01_13-_-_-0_33_18_108=_=aZ8YpCUHXmaPLgPAobaP\Bottom.Bmp");
            ImageBase64[0] = "testbase64_2";
            ImageBase64[1] = "testbase64_2";
            new dl.PrintedCardReport().SetCaoCardPrintedReport(LayoutDesignTools.CardType.UltraLightLicence, Text, ImagePath, ImageBase64, bl.Auth.UserName);

        }


        private void frmMaindesign_Load(object sender, EventArgs e)
        {

            // Make Connection String
            Settings tanzim = new Settings();
            OleDbConnectionStringBuilder ConnectionStringBuilder = new OleDbConnectionStringBuilder();
            ConnectionStringBuilder.DataSource = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + "printer.accdb";
            ConnectionStringBuilder.Add("Provider", "Microsoft.ACE.OLEDB.12.0");
            ConnectionStringBuilder.Add("Jet OLEDB:Database Password", "A988b988");
            dl.DataAccessClass.ConnectionString = string.Format(ConnectionStringBuilder.ConnectionString);
            tanzim.ConnectionString = string.Format(ConnectionStringBuilder.ConnectionString);
            tanzim.Save();


            if (!Directory.Exists(TempPath + "Base64Bitmap"))
                Directory.CreateDirectory(TempPath + "Base64Bitmap");
            if (!Directory.Exists(TempPath + "LaserPrinterImagetmp"))
                Directory.CreateDirectory(TempPath + "LaserPrinterImagetmp");
            rulerControl1.MouseTrackingOn = true;
            rulerControl2.MouseTrackingOn = true;
            this.Cursor = Cursors.WaitCursor;
            //ControlList.objDGVBind.Clear();
            flowLayoutPanel1.Controls.Add(PanelRo);
            flowLayoutPanel2.Controls.Add(PanelPosht);
            IsUserAdministrator();
            EnterUserPassword();
            LoadingDevice();
            // Test();


            string hich = "";
            Hardware.CR.Initialize(Hardware.CR.Inittype.NoMoveCard, ref hich);
            if (CR.WriteMagnetticMode(ref hich, CR.MagneticWriteMode.High_Co) == CR.ReturnDeviceStatus.MB_OK)
                Hardware.CR.Initialize(Hardware.CR.Inittype.MoveTogate, ref hich);
            else
            {
                Hardware.CR.Initialize(Hardware.CR.Inittype.MoveTogate, ref hich);
                //  MessageBox.Show("Write Mode Not Ok");
            }
            PanelRo.Cursor = Cursors.Arrow;
            PanelPosht.Cursor = Cursors.Arrow;
            Config.CloseAllPortExceptLaser();



            System.IO.DirectoryInfo di2 = new DirectoryInfo(Path.GetTempPath() + "Base64Bitmap");
            foreach (FileInfo file in di2.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di2.GetDirectories())
            {
                dir.Delete(true);
            }

            System.IO.DirectoryInfo di = new DirectoryInfo(Path.GetTempPath() + "LaserPrinterImagetmp");
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            this.Cursor = Cursors.Arrow;


        }
        private void toolNewAdd_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("صفحه غیر قابل بازگشت هست.آیا مطمئن هستید که صفحه پاک شود؟", "پیغام", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                NewPageRotin();
            }
        }
        private void toolCuts_Click(object sender, EventArgs e)
        {
            CutRotin();


        }
        private void toolCopys_Click(object sender, EventArgs e)
        {
            CopyRotin();


        }
        private void toolPastes_Click(object sender, EventArgs e)
        {
            PasteRotin();

        }
        private void toolBringtoFront_Click(object sender, EventArgs e)
        {
            if (SelectedControl != null)
            {
                if (tabControl1.SelectedTab == TopDesignTab)
                    DoAction(DesignAction.TopBringToFront, ActSituation.After);
                else if (tabControl1.SelectedTab == BottomDesignTab)
                    DoAction(DesignAction.BottomBringToFront, ActSituation.After);
                flgSaveFlag = true;
                SelectedControl.BringToFront();

            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (SelectedControl != null)
            {
                if (tabControl1.SelectedTab == TopDesignTab)
                    DoAction(DesignAction.TopBringToBack, ActSituation.After);
                else if (tabControl1.SelectedTab == BottomDesignTab)
                    DoAction(DesignAction.BottomBringToBack, ActSituation.After);
                flgSaveFlag = true;
                SelectedControl.SendToBack();
            }
        }
        private void DeleteATool_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("صفحه غیر قابل بازگشت هست.آیا مطمئن هستید که صفحه پاک شود؟", "پیغام", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                DeleteAllRotin();
        }
        private void DeleteSTool_Click(object sender, EventArgs e)
        {

            DeleteRotin();
        }
        private void lblTool1_Click(object sender, EventArgs e)
        {
            PlaceTextonPanel();

        }
        private void PicTool_Click(object sender, EventArgs e)
        {
            PlacePictureBoxonPanel();
        }
        private void pnControls_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedControl != null)
                DrawControlBorder(SelectedControl);
            timer1.Start();
        }
        private void pnControls_MouseMove(object sender, MouseEventArgs e)
        {

            if (SelectedControl != null && e.Button == MouseButtons.Left)
            {
                timer1.Stop();

                Invalidate();

                if (SelectedControl.Height < 20)
                {
                    SelectedControl.Height = 20;
                    direction = Direction.None;
                    Cursor = Cursors.Default;
                    return;
                }
                else if (SelectedControl.Width < 20)
                {
                    SelectedControl.Width = 20;
                    direction = Direction.None;
                    Cursor = Cursors.Default;
                    return;
                }

                //get the current mouse position relative the the app
                Point pos = new Point();
                if (tabControl1.SelectedTab == TopDesignTab)
                    pos = PanelRo.PointToClient(MousePosition);
                else if (tabControl1.SelectedTab == BottomDesignTab)
                    pos = PanelPosht.PointToClient(MousePosition);

                #region resize the control in 8 directions
                if (direction == Direction.NW)
                {
                    //north west, location and width, height change
                    newLocation = new Point(pos.X, pos.Y);
                    newSize = new Size(SelectedControl.Size.Width - (newLocation.X - SelectedControl.Location.X),
                        SelectedControl.Size.Height - (newLocation.Y - SelectedControl.Location.Y));
                    SelectedControl.Location = newLocation;
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.SE)
                {
                    //south east, width and height change
                    newLocation = new Point(pos.X, pos.Y);
                    newSize = new Size(SelectedControl.Size.Width + (newLocation.X - SelectedControl.Size.Width - SelectedControl.Location.X),
                        SelectedControl.Height + (newLocation.Y - SelectedControl.Height - SelectedControl.Location.Y));
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.N)
                {
                    //north, location and height change
                    newLocation = new Point(SelectedControl.Location.X, pos.Y);
                    newSize = new Size(SelectedControl.Width,
                        SelectedControl.Height - (pos.Y - SelectedControl.Location.Y));
                    SelectedControl.Location = newLocation;
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.S)
                {
                    //south, only the height changes
                    newLocation = new Point(pos.X, pos.Y);
                    newSize = new Size(SelectedControl.Width,
                        pos.Y - SelectedControl.Location.Y);
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.W)
                {
                    //west, location and width will change
                    newLocation = new Point(pos.X, SelectedControl.Location.Y);
                    newSize = new Size(SelectedControl.Width - (pos.X - SelectedControl.Location.X),
                        SelectedControl.Height);
                    SelectedControl.Location = newLocation;
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.E)
                {
                    //east, only width changes
                    newLocation = new Point(pos.X, pos.Y);
                    newSize = new Size(pos.X - SelectedControl.Location.X,
                        SelectedControl.Height);
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.SW)
                {
                    //south west, location, width and height change
                    newLocation = new Point(pos.X, SelectedControl.Location.Y);
                    newSize = new Size(SelectedControl.Width - (pos.X - SelectedControl.Location.X),
                        pos.Y - SelectedControl.Location.Y);
                    SelectedControl.Location = newLocation;
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.NE)
                {
                    //north east, location, width and height change
                    newLocation = new Point(SelectedControl.Location.X, pos.Y);
                    newSize = new Size(pos.X - SelectedControl.Location.X,
                        SelectedControl.Height - (pos.Y - SelectedControl.Location.Y));
                    SelectedControl.Location = newLocation;
                    SelectedControl.Size = newSize;
                }
                #endregion
            }
        }
        private void MoveObject(int MoveLocation, MoveDirections Dic)
        {
            if (SelectedControl != null)
            {
                propertyGrid1.SelectedObject = null;
                int XLocation = 0, YLocation = 0;
                XLocation = SelectedControl.Location.X;
                YLocation = SelectedControl.Location.Y;
                switch (Dic)
                {
                    case MoveDirections.Right:
                        {
                            XLocation += MoveLocation;
                        }
                        break;
                    case MoveDirections.Left:
                        {
                            XLocation -= MoveLocation;
                        }
                        break;
                    case MoveDirections.Down:
                        {
                            YLocation += MoveLocation;
                        }
                        break;
                    case MoveDirections.Up:
                        {
                            YLocation -= MoveLocation;
                        }
                        break;
                    default:
                        break;
                }
                SelectedControl.Location = new Point(XLocation, YLocation);
                SelectedControl.Refresh();
                SelectedControl.Update();
                PanelRo.Refresh();
                PanelRo.Update();
                PanelPosht.Refresh();
                PanelPosht.Update();
                propertyGrid1.SelectedObject = SelectedControl;
            }
        }
        private void frmMaindesign_KeyDown(object sender, KeyEventArgs e)
        {
            if (!KeysDeActive)
            {
                if (e.KeyCode == Keys.Left)
                    MoveObject(1, MoveDirections.Left);
                if (e.KeyCode == Keys.Right)
                    MoveObject(1, MoveDirections.Right);
                if (e.KeyCode == Keys.Up)
                    MoveObject(1, MoveDirections.Up);
                if (e.KeyCode == Keys.Down)
                    MoveObject(1, MoveDirections.Down);

                if (e.KeyCode == Keys.Left && e.Shift)
                    MoveObject(10, MoveDirections.Left);
                if (e.KeyCode == Keys.Right && e.Shift)
                    MoveObject(10, MoveDirections.Right);
                if (e.KeyCode == Keys.Up && e.Shift)
                    MoveObject(10, MoveDirections.Up);
                if (e.KeyCode == Keys.Down && e.Shift)
                    MoveObject(10, MoveDirections.Down);

                if (e.KeyCode == Keys.X && e.Control)
                    CutRotin();
                if (e.KeyCode == Keys.C && e.Control)
                    CopyRotin();
                if (e.KeyCode == Keys.V && e.Control)
                    PasteRotin();
                if (e.KeyCode == Keys.Delete)
                    DeleteRotin();
                if (e.KeyCode == Keys.Delete && e.Control)
                    DeleteAllRotin();
                if (e.KeyCode == Keys.S && e.Shift && e.Control)
                    XMLSaveFile();
                if (e.KeyCode == Keys.S && e.Control && flgSaveFlag)
                    Saveit();
                if (e.KeyCode == Keys.N && e.Control)
                    NewPageRotin();
                if (e.KeyCode == Keys.O && e.Control)
                    XmlLoadFile();
                if (e.KeyCode == Keys.E && e.Control)
                    BitmapSaveFile();
                if (e.KeyCode == Keys.I && e.Control)
                    BitmapLoadFile();
                if (e.KeyCode == Keys.B && e.Control)
                    BackgroundImageLoad();
                if (e.KeyCode == Keys.Z && e.Control)
                    UndoAction();
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (SelectedControl != null)
                {
                    string Xpos = txtXPos.Text, YPos = txtYPos.Text;
                    Xpos = Xpos.Replace("mm", ""); Xpos = Xpos.Replace(" ", "");
                    YPos = YPos.Replace("mm", ""); YPos = YPos.Replace(" ", "");
                    try
                    {
                        SelectedControl.Location = ConvermmtoPx(double.Parse(Xpos), double.Parse(YPos));
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("لطفا اعداد را بدرستی وارد نمایید", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("لطفا یک آیتم را انتخاب نمایید", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }
        private void radCheckBox1_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            try
            {
                if (tabControl1.SelectedTab == TopDesignTab)
                {
                    if (System.IO.File.Exists(@"Images\BackgroundPicture_Top.txt"))
                    {
                        string ReadData = File.ReadAllText(@"Images\BackgroundPicture_Top.txt");
                        if (!String.IsNullOrWhiteSpace( ReadData))
                        {
                            if (radCheckBox1.Checked)
                            {
                                PanelRo.BackgroundImageLayout = ImageLayout.Stretch;
                                PanelRo.BackgroundImage = ReadBase64PictureFromFile(@"Images\BackgroundPicture_Top.txt");

                            }
                            else
                            {
                                PanelRo.BackgroundImage = null;
                                PanelRo.Update();
                                PanelRo.Refresh();
                            }
                        }
                        else
                        {
                            if (radCheckBox1.Checked)
                            {
                                radCheckBox1.Checked = false;
                                MessageBox.Show("لطفا عکس پس زمینه را انتخاب نمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }
                    else
                    {
                        if (radCheckBox1.Checked)
                        {
                            radCheckBox1.Checked = false;
                            MessageBox.Show("لطفا عکس پس زمینه را انتخاب نمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if (tabControl1.SelectedTab == BottomDesignTab)
                {
                    if (System.IO.File.Exists(@"Images\BackgroundPicture_Bottom.txt"))
                    {
                        string ReadData = File.ReadAllText(@"Images\BackgroundPicture_Bottom.txt");
                        if (!String.IsNullOrWhiteSpace(ReadData))
                        {
                            if (radCheckBox1.Checked)
                            {
                                PanelPosht.BackgroundImageLayout = ImageLayout.Stretch;
                                PanelPosht.BackgroundImage = ReadBase64PictureFromFile(@"Images\BackgroundPicture_Bottom.txt");
                            }
                            else
                            {
                                PanelPosht.BackgroundImage = null;
                                PanelPosht.Update();
                                PanelPosht.Refresh();
                            }
                        }
                        else
                        {
                            if (radCheckBox1.Checked)
                            {
                                radCheckBox1.Checked = false;
                                MessageBox.Show("لطفا عکس پس زمینه را انتخاب نمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }
                    else
                    {
                        if (radCheckBox1.Checked)
                        {
                            radCheckBox1.Checked = false;
                            MessageBox.Show("لطفا عکس پس زمینه را انتخاب نمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception)
            {

                if (radCheckBox1.Checked)
                    radCheckBox1.Checked = false;
                if (tabControl1.SelectedTab == TopDesignTab)
                {
                    PanelRo.BackgroundImage = null;
                    PanelRo.Update();
                    PanelRo.Refresh();
                }
                else if (tabControl1.SelectedTab == BottomDesignTab)
                {
                    PanelPosht.BackgroundImage = null;
                    PanelPosht.Update();
                    PanelPosht.Refresh();
                }

                MessageBox.Show("لطفا عکس پس زمینه را انتخاب نمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        private void frmMaindesign_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool HaveControl = false;
            if (tabControl1.SelectedTab == TopDesignTab)
                if (PanelRo.Controls != null)
                    HaveControl = true;
            if (tabControl1.SelectedTab == BottomDesignTab)
                if (PanelPosht.Controls != null)
                    HaveControl = true;
            if (HaveControl && flgSaveFlag)
                if (MessageBox.Show("آیا میخواهید پنل مورد نظر ذخیره گردد؟", "پیغام", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    XMLSaveFile();
            try
            {
                new dl.UserLog().LogOut();
                Hardware.Laser.CloseWindow();
                Hardware.CR.CRT350RClose(Hardware.CR.Hdle);
                Config.CloseAllPortExceptLaser();
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void V90Label_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            PlaceBarcodeonPanel();
        }
        private void V90Label_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            PlaceORTextonPanel();

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            BitmapSaveFile();
        }
        private void XMLSaveAs_Click(object sender, EventArgs e)
        {
            XMLSaveFile();
        }
        private void LoadXml_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo di2 = new DirectoryInfo(Path.GetTempPath() + "Base64Bitmap");
            foreach (FileInfo file in di2.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di2.GetDirectories())
            {
                dir.Delete(true);
            }

            System.IO.DirectoryInfo di = new DirectoryInfo(Path.GetTempPath() + "LaserPrinterImagetmp");
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            bool HaveControl = false;
            if (tabControl1.SelectedTab == TopDesignTab)
                if (PanelRo.Controls.Count != 0)
                    HaveControl = true;
            if (tabControl1.SelectedTab == BottomDesignTab)
                if (PanelPosht.Controls.Count != 0)
                    HaveControl = true;
            if (HaveControl && flgSaveFlag)
            {
                if (MessageBox.Show("آیا میخواهید پنل مورد نظر ذخیره گردد؟", "پیغام", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    XMLSaveFile();
                    NewPageRotin();
                    XmlLoadFile();
                    if (tabControl1.SelectedTab == TopDesignTab)
                        DoAction(DesignAction.TopOpenFile, ActSituation.After);
                    else if (tabControl1.SelectedTab == BottomDesignTab)
                        DoAction(DesignAction.BottomOpenFile, ActSituation.After);
                }
                else
                {
                    NewPageRotin();
                    XmlLoadFile();
                    if (tabControl1.SelectedTab == TopDesignTab)
                        DoAction(DesignAction.TopOpenFile, ActSituation.After);
                    else if (tabControl1.SelectedTab == BottomDesignTab)
                        DoAction(DesignAction.BottomOpenFile, ActSituation.After);
                }
            }
            else
            {
                XmlLoadFile();
                if (tabControl1.SelectedTab == TopDesignTab)
                    DoAction(DesignAction.TopOpenFile, ActSituation.After);
                else if (tabControl1.SelectedTab == BottomDesignTab)
                    DoAction(DesignAction.BottomOpenFile, ActSituation.After);
            }

        }
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            BitmapLoadFile();

        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            BackgroundImageLoad();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radCheckBox1.Checked)
            {
                do
                {
                    radCheckBox1.Checked = false;
                } while (radCheckBox1.Checked);
                do
                {
                    radCheckBox1.Checked = true;
                } while (!radCheckBox1.Checked);
            }

        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewPageRotin();
        }
        private void openPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BitmapLoadFile();

        }
        private void openXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool HaveControl = false;
            if (tabControl1.SelectedTab == TopDesignTab)
                if (PanelRo.Controls != null)
                    HaveControl = true;
            if (tabControl1.SelectedTab == BottomDesignTab)
                if (PanelPosht.Controls != null)
                    HaveControl = true;
            if (HaveControl)
            {
                if (MessageBox.Show("آیا میخواهید پنل مورد نظر ذخیره گردد؟", "پیغام", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    XMLSaveFile();
                    NewPageRotin();
                    XmlLoadFile();
                    if (tabControl1.SelectedTab == TopDesignTab)
                        DoAction(DesignAction.TopOpenFile, ActSituation.After);
                    else if (tabControl1.SelectedTab == BottomDesignTab)
                        DoAction(DesignAction.BottomOpenFile, ActSituation.After);
                }
                else
                {
                    NewPageRotin();
                    XmlLoadFile();
                    if (tabControl1.SelectedTab == TopDesignTab)
                        DoAction(DesignAction.TopOpenFile, ActSituation.After);
                    else if (tabControl1.SelectedTab == BottomDesignTab)
                        DoAction(DesignAction.BottomOpenFile, ActSituation.After);
                }
            }
            else
            {
                XmlLoadFile();
                if (tabControl1.SelectedTab == TopDesignTab)
                    DoAction(DesignAction.TopOpenFile, ActSituation.After);
                else if (tabControl1.SelectedTab == BottomDesignTab)
                    DoAction(DesignAction.BottomOpenFile, ActSituation.After);
            }
        }
        private void quitAltF4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutRotin();
        }
        private void copyCtrlCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyRotin();
        }
        private void pasteCtrlVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteRotin();
        }
        private void deleteDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteRotin();
        }
        private void deleteAllCtrlDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteAllRotin();
        }
        private void backgroundImageCtrlBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackgroundImageLoad();
        }
        private void bringFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedControl != null)
            {
                if (tabControl1.SelectedTab == TopDesignTab)
                    DoAction(DesignAction.TopBringToFront, ActSituation.After);
                else if (tabControl1.SelectedTab == BottomDesignTab)
                    DoAction(DesignAction.BottomBringToFront, ActSituation.After);
                SelectedControl.BringToFront();

            }
        }
        private void bringBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedControl != null)
            {
                if (tabControl1.SelectedTab == TopDesignTab)
                    DoAction(DesignAction.TopBringToBack, ActSituation.After);
                else if (tabControl1.SelectedTab == BottomDesignTab)
                    DoAction(DesignAction.BottomBringToBack, ActSituation.After);
                SelectedControl.SendToBack();
            }
        }
        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaceTextonPanel();
        }
        private void rotateTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaceORTextonPanel();
        }
        private void pictureBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlacePictureBoxonPanel();
        }
        private void barcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaceBarcodeonPanel();
        }
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            AboutUs Abt = new AboutUs();
            Abt.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }
        private void PanelRo_Click(object sender, EventArgs e)
        {
            KeysDeActive = false;
        }
        private void PanelPosht_Click(object sender, EventArgs e)
        {
            KeysDeActive = false;
        }
        private void saveXmlCtrlEXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XMLSaveFile();
        }
        private void savePictureCtrlEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BitmapSaveFile();
        }
        private void laserSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Laser_Config LaserCfg = new Laser_Config();
            LaserCfg.ShowDialog();
        }
        private void PanelRo_MouseDown(object sender, MouseEventArgs e)
        {
            KeysDeActive = false;
            if (e.Button == MouseButtons.Right)
            {
                cmsPanelMenu.Items.Clear();
                cmsPanelMenu.Items.Add("Cut Ctrl+X");
                cmsPanelMenu.Items.Add("Copy Ctrl+C");
                if (copiedControl != null)
                    cmsPanelMenu.Items.Add("Paste Ctrl+V");
                PanelRo.ContextMenuStrip = cmsPanelMenu;
                cmsPanelMenu.Show();
                cmsPanelMenu.Items[0].Click += new EventHandler(Cut_Click);
                cmsPanelMenu.Items[1].Click += new EventHandler(Copy_Click);
                if (copiedControl != null)
                    cmsPanelMenu.Items[2].Click += new EventHandler(Paste_Click);
            }

        }

        private void Cut_Click(object sender, EventArgs e)
        {
            CutRotin();

        }
        private void Copy_Click(object sender, EventArgs e)
        {
            CopyRotin();

        }
        private void Paste_Click(object sender, EventArgs e)
        {
            PasteRotin();

        }
        private void logOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            while (new LoginForm().ShowDialog() != DialogResult.OK)
            {
                System.Threading.Thread.Sleep(1000);
            }
            this.Show();
        }
        private void userDefineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserManage UserDefine = new UserManage();
            UserDefine.ShowDialog();
        }
        private void changePasswrodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePasswordPage CPP = new ChangePasswordPage();
            CPP.ShowDialog();
        }
        private void debugingFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debugging DF = new Debugging();
            DF.ShowDialog();
        }
        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private Image RResizeImage(Image img, int iWidth, int iHeight)
        {
            Bitmap bmp = new Bitmap(iWidth, iHeight);
            Graphics graphic = Graphics.FromImage((Image)bmp);
            graphic.DrawImage(img, 0, 0, iWidth, iHeight);

            return (Image)bmp;
        }
        /*
         * Frodgah
        private void btnSinglePrint_Click(object sender, EventArgs e)
        {
            
            int EntityName = 0;
            this.Cursor = Cursors.WaitCursor;
            bool BackDis = false;
            bool flgNoPicExist = false;
            string TempFolder = Path.GetTempPath();
            LaserConfigClass LaserSetting = new LaserConfigClass();



            string PictureTopPath = TempFolder + "LaserPrinterImagetmp\\PictureTop.bmp";
            string WithoutPictureBoxPictureTopPath = TempFolder + "LaserPrinterImagetmp\\WithoutPictureBoxPictureTop.bmp";
            string WithPictureBoxPictureTopPath = TempFolder + "LaserPrinterImagetmp\\WithPictureBoxPictureTop.bmp";

            string PictureBottomPath = TempFolder + "LaserPrinterImagetmp\\PictureBottom.bmp";
            string WithoutPictureBoxPictureBottom = TempFolder + "LaserPrinterImagetmp\\WithoutPictureBoxPictureBottom.bmp";
            string WithPictureBoxPictureBottom = TempFolder + "LaserPrinterImagetmp\\WithPictureBoxPictureBottom.bmp";




            if (!radCheckBox1.Checked) BackDis = true;
            if (!BackDis)
                DisableBackgroundImage();


            System.IO.DirectoryInfo di = new DirectoryInfo(TempFolder + "LaserPrinterImagetmp");
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            var bmpTop = SplitPictureControl(PanelRo);
            ExportPicture(PanelRo).Save(PictureTopPath, ImageFormat.Bmp);
            bmpTop[0].Save(WithoutPictureBoxPictureTopPath, ImageFormat.Bmp);
            bmpTop[1].Save(WithPictureBoxPictureTopPath, ImageFormat.Bmp);
            bmpTop[0].Dispose();
            bmpTop[1].Dispose();

            var bmpBottom = SplitPictureControl(PanelPosht);
            ExportPicture(PanelPosht).Save(PictureBottomPath, ImageFormat.Bmp);
            bmpBottom[0].Save(WithoutPictureBoxPictureBottom, ImageFormat.Bmp);
            bmpBottom[1].Save(WithPictureBoxPictureBottom, ImageFormat.Bmp);
            bmpBottom[0].Dispose();
            bmpBottom[1].Dispose();


            if (Laser.ClearLibAllEntity() == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
            {
                foreach (Control item in PanelRo.Controls)
                {

                    if (item.GetType().ToString() == "System.Windows.Forms.PictureBox")
                    {
                        PictureBox tmp = item as PictureBox;
                        string Result = Path.GetTempPath() + "Base64Bitmap\\";
                        if (!Directory.Exists(Result))
                            Directory.CreateDirectory(Result);
                        if (tmp.Image != null)
                        {

                            if (File.Exists(TempPath + "Base64Bitmap//" + tmp.Name))
                            {
                                string PicValue = EncryptionClass.EncryptionClass.Decrypt(File.ReadAllText(TempPath + "Base64Bitmap//" + tmp.Name));

                                double[] Location = convertPxTomm(tmp.Location);
                                Image bmp = Base64ToImage(PicValue);
                                string BmpName = Path.GetTempPath() + "LaserPrinterImagetmp\\" + "PictureTop_" + tmp.Name.Replace(".txt", "");
                                bmp.Save(BmpName, ImageFormat.Bmp);
                                float DPIX = bmp.HorizontalResolution;
                                float DPIY = bmp.VerticalResolution;
                                double[] PictureSize = WithDPIconvertPxTomm(bmp.Size, DPIX, DPIY);
                                //                                double NewYLocation = Location[1]; //+ (PictureSize[1]) + (PictureSize[1] / 2);
                                LaserSetting = LaserConfigClass.load();
                                double NewCentreLocationX = Location[0] - 42.8 +LaserSetting.Xcenter ;// - 43;
                                
                                double NewCentreLocationY = -Location[1] + LaserSetting.Ycenter + 18.6;// (54 - NewYLocation) - 27;
                                if (Laser.SetPenParam(3, 1, 300, 4, 1, 35000, 10, 20, 100, 300, 0, 2000, 500, 100, 0, 0.01, 0.100, false, 1, 0) == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                                {
                                    if (Laser.AddFileToLib(BmpName, "Picture" + EntityName.ToString(), NewCentreLocationX, NewCentreLocationY, 0, 0, 1, 3, false) == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                                    {
                                        if (Laser.SetBitmapEntParam("Picture" + EntityName.ToString(), BmpName, 0, 0, 0, 0, 0, 600, false, 0) == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
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


                        }
                        else
                        {
                            flgNoPicExist = true;
                            MessageBox.Show("لطفا تصویر مورد نظر را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            {
                Laser.SaveEntLibToFile(Path.GetTempPath() + "LaserPrinterImagetmp\\PictureTop.ezd");

                if (!BackDis)
                    EnableBackgroundImage();
                this.Cursor = Cursors.Arrow;
                SinglePrint Sprint = new SinglePrint();
                Sprint.ShowDialog();
            }
        }

        */

        public static double ConvertToRadians(double angle)
        {
            double degree = (Math.PI / 180) * angle;
            return degree;
        }
        private void btnSinglePrint_Click(object sender, EventArgs e)
        {

            int EntityName = 0;
            this.Cursor = Cursors.WaitCursor;
            bool BackDis = false;
            bool flgNoPicExist = false;
            string TempFolder = Path.GetTempPath();
            LaserConfigClass LaserSetting = new LaserConfigClass();



            string PictureTopPath = TempFolder + "LaserPrinterImagetmp\\PictureTop.bmp";
            string WithoutPictureBoxPictureTopPath = TempFolder + "LaserPrinterImagetmp\\WithoutPictureBoxPictureTop.bmp";
            string WithPictureBoxPictureTopPath = TempFolder + "LaserPrinterImagetmp\\WithPictureBoxPictureTop.bmp";

            string PictureBottomPath = TempFolder + "LaserPrinterImagetmp\\PictureBottom.bmp";
            string WithoutPictureBoxPictureBottom = TempFolder + "LaserPrinterImagetmp\\WithoutPictureBoxPictureBottom.bmp";
            string WithPictureBoxPictureBottom = TempFolder + "LaserPrinterImagetmp\\WithPictureBoxPictureBottom.bmp";




            if (!radCheckBox1.Checked) BackDis = true;
            if (!BackDis)
                DisableBackgroundImage();


            System.IO.DirectoryInfo di = new DirectoryInfo(TempFolder + "LaserPrinterImagetmp");
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }





            //  تولید عکس روی کارت
            if (Laser.ClearLibAllEntity() == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
            {
                foreach (Control item in PanelRo.Controls)
                {

                    if (item.GetType().ToString() == "System.Windows.Forms.PictureBox")
                    {
                        PictureBox tmp = item as PictureBox;
                        string Result = Path.GetTempPath() + "Base64Bitmap\\";
                        if (!Directory.Exists(Result))
                            Directory.CreateDirectory(Result);
                        if (tmp.Image != null)
                        {

                            if (File.Exists(TempPath + "Base64Bitmap//" + tmp.Name))
                            {
                                string PicValue = EncryptionClass.EncryptionClass.Decrypt(File.ReadAllText(TempPath + "Base64Bitmap//" + tmp.Name));

                                double[] Location = convertPxTomm(tmp.Location);
                                Image bmp = Base64ToImage(PicValue);
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
                                //                                double NewYLocation = Location[1]; //+ (PictureSize[1]) + (PictureSize[1] / 2);
                                LaserSetting = LaserConfigClass.load();
                                //     double NewCentreLocationX = Location[0] - 42.8 + LaserSetting.Xcenter;// - 43;
                                double NewCentreLocationX = Location[0] + LaserSetting.Xcenter - 42.948;// - 43;
                                NewCentreLocationX = Math.Round(NewCentreLocationX, 4);
                                //    double NewCentreLocationY = -Location[1] + LaserSetting.Ycenter + 1.6;// (54 - NewYLocation) - 27;
                                double NewCentreLocationY = (Location[1] * -1) + LaserSetting.Ycenter + 27.021;// (54 - NewYLocation) - 27;
                                NewCentreLocationY = Math.Round(NewCentreLocationY, 4);
                                if (Laser.SetPenParam(3, 1, 300, 4, 1, 35000, 10, 20, 100, 300, 0, 2000, 500, 100, 0, 0.01, 0.100, false, 1, 0) == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                                {
                                    Laser.LmcErrCode MM;
                                    MM = Laser.AddFileToLib(BmpName, "Picture" + EntityName.ToString(), NewCentreLocationX, NewCentreLocationY, 0, 6, LaserSetting.Ratio, 3, false);
                                    if (MM == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                                    {
                                        if (Laser.SetBitmapEntParam("Picture" + EntityName.ToString(), BmpName, 0, 0, LaserSetting.Brightness, LaserSetting.Contrast, LaserSetting.PointTime, LaserSetting.dpi, false, 0) == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
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


                        }
                        else
                        {
                            flgNoPicExist = true;
                            MessageBox.Show("لطفا تصویر مورد نظر را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                tabControl1.SelectTab(TopDesignTab);
                var bmpTop = SplitPictureControl(PanelRo);
                ExportPicture(PanelRo).Save(PictureTopPath, ImageFormat.Bmp);
                bmpTop[0].Save(WithoutPictureBoxPictureTopPath, ImageFormat.Bmp);
                bmpTop[1].Save(WithPictureBoxPictureTopPath, ImageFormat.Bmp);
                bmpTop[0].Dispose();
                bmpTop[1].Dispose();
            }
            else
            {
                MessageBox.Show("لطفا از اتصال دستگاه اطمینان حاصل نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flgNoPicExist = true;
            }
            if (!flgNoPicExist)
            {
                StatusClass ReturnStatus = new StatusClass()
                {
                    ResponseReturnStatus = StatusClass.ResponseStatus.Fail,
                    ReturnDescription = ""
                };
                Image NonR = Laser.GetCurPreviewImage(674, 422, ref ReturnStatus);

                NonR.Save(Path.GetTempPath() + "LaserPrinterImagetmp\\PictureTopNon.bmp", ImageFormat.Bmp);

                Laser.SetRotateParam(Hardware.Laser.myconfig.Xcenter, Hardware.Laser.myconfig.Ycenter, ConvertToRadians(Hardware.Laser.myconfig.RotateAngle));

                Image Ro = Laser.GetCurPreviewImage(674, 422, ref ReturnStatus);

                NonR.Save(Path.GetTempPath() + "LaserPrinterImagetmp\\PictureTopRo.bmp", ImageFormat.Bmp);
                Laser.SaveEntLibToFile(Path.GetTempPath() + "LaserPrinterImagetmp\\PictureTop.ezd");


            }

            //  تولید عکس پشت کارت
            if (Laser.ClearLibAllEntity() == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
            {
                foreach (Control item in PanelPosht.Controls)
                {

                    if (item.GetType().ToString() == "System.Windows.Forms.PictureBox")
                    {
                        PictureBox tmp = item as PictureBox;
                        string Result = Path.GetTempPath() + "Base64Bitmap\\";
                        if (!Directory.Exists(Result))
                            Directory.CreateDirectory(Result);
                        if (tmp.Image != null)
                        {

                            if (File.Exists(TempPath + "Base64Bitmap//" + tmp.Name))
                            {
                                string PicValue = EncryptionClass.EncryptionClass.Decrypt(File.ReadAllText(TempPath + "Base64Bitmap//" + tmp.Name));

                                double[] Location = convertPxTomm(tmp.Location);
                                Image bmp = Base64ToImage(PicValue);
                                string BmpName = Path.GetTempPath() + "LaserPrinterImagetmp\\" + "PictureBottom_" + tmp.Name.Replace(".txt", "");



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
                                //                                double NewYLocation = Location[1]; //+ (PictureSize[1]) + (PictureSize[1] / 2);
                                LaserSetting = LaserConfigClass.load();
                                //     double NewCentreLocationX = Location[0] - 42.8 + LaserSetting.Xcenter;// - 43;
                                double NewCentreLocationX = Location[0] + LaserSetting.Xcenter - 42.948;// - 43;
                                NewCentreLocationX = Math.Round(NewCentreLocationX, 4);
                                //    double NewCentreLocationY = -Location[1] + LaserSetting.Ycenter + 1.6;// (54 - NewYLocation) - 27;
                                double NewCentreLocationY = (Location[1] * -1) + LaserSetting.Ycenter + 27.021;// (54 - NewYLocation) - 27;
                                NewCentreLocationY = Math.Round(NewCentreLocationY, 4);
                                if (Laser.SetPenParam(3, 1, 300, 4, 1, 35000, 10, 20, 100, 300, 0, 2000, 500, 100, 0, 0.01, 0.100, false, 1, 0) == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                                {
                                    Laser.LmcErrCode MM;
                                    MM = Laser.AddFileToLib(BmpName, "Picture" + EntityName.ToString(), NewCentreLocationX, NewCentreLocationY, 0, 6, LaserSetting.Ratio, 3, false);
                                    if (MM == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
                                    {
                                        if (Laser.SetBitmapEntParam("Picture" + EntityName.ToString(), BmpName, 0, 0, LaserSetting.Brightness, LaserSetting.Contrast, LaserSetting.PointTime, LaserSetting.dpi, false, 0) == Laser.LmcErrCode.LMC1_ERR_SUCCESS)
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


                        }
                        else
                        {
                            flgNoPicExist = true;
                            MessageBox.Show("لطفا تصویر مورد نظر را انتخاب نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                tabControl1.SelectTab(BottomDesignTab);
                var bmpBottom = SplitPictureControl(PanelPosht);
                ExportPicture(PanelPosht).Save(PictureBottomPath, ImageFormat.Bmp);
                bmpBottom[0].Save(WithoutPictureBoxPictureBottom, ImageFormat.Bmp);
                bmpBottom[1].Save(WithPictureBoxPictureBottom, ImageFormat.Bmp);
                bmpBottom[0].Dispose();
                bmpBottom[1].Dispose();
            }
            else
            {
                MessageBox.Show("لطفا از اتصال دستگاه اطمینان حاصل نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flgNoPicExist = true;
            }
            if (!flgNoPicExist)
            {
                StatusClass ReturnStatus = new StatusClass()
                {
                    ResponseReturnStatus = StatusClass.ResponseStatus.Fail,
                    ReturnDescription = ""
                };
                Image NonR = Laser.GetCurPreviewImage(674, 422, ref ReturnStatus);

                NonR.Save(Path.GetTempPath() + "LaserPrinterImagetmp\\PictureBottomNon.bmp", ImageFormat.Bmp);

                Laser.SetRotateParam(Hardware.Laser.myconfig.Xcenter, Hardware.Laser.myconfig.Ycenter, ConvertToRadians(Hardware.Laser.myconfig.RotateAngle));

                Image Ro = Laser.GetCurPreviewImage(674, 422, ref ReturnStatus);

                NonR.Save(Path.GetTempPath() + "LaserPrinterImagetmp\\PictureBottomRo.bmp", ImageFormat.Bmp);
                Laser.SaveEntLibToFile(Path.GetTempPath() + "LaserPrinterImagetmp\\PictureBottom.ezd");
            }

            if (!BackDis)
                EnableBackgroundImage();
            this.Cursor = Cursors.Arrow;
            SinglePrint Sprint = new SinglePrint();
            Sprint.ShowDialog();



        }




        private void DatabasePrint_Click(object sender, EventArgs e)
        {
            bool BackDis = false;
            if (!radCheckBox1.Checked)
                BackDis = true;
            if (!BackDis)
                DisableBackgroundImage();
            this.Cursor = Cursors.WaitCursor;
            DatabasePrint frm = new DatabasePrint(PanelRo, PanelPosht);
            frm.ShowDialog();
            this.Cursor = Cursors.Arrow;
            if (!BackDis)
                EnableBackgroundImage();
        }
        private void lblPersianDate_Click(object sender, EventArgs e)
        {

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            lblPersianDate.Text = dl.DateConvertor.DateConverter(true);
            if (!KeysDeActive)
            {
                if (SelectedControl != null)
                {
                    string sle = SelectedControl.Text;
                    double[] Position = convertPxTomm(SelectedControl.Location);
                    if (Position[0] > 100) Position[0] = 0;
                    if (Position[1] > 100) Position[1] = 0;
                    txtXPos.Text = Position[0].ToString() + " mm";
                    txtYPos.Text = Position[1].ToString() + " mm";
                }
                else
                {
                    txtXPos.Text = "0 mm";
                    txtYPos.Text = "0 mm";
                }
                if (!txtXPos.Text.Contains("mm"))
                    txtXPos.Text += " mm";
                if (!txtYPos.Text.Contains("mm"))
                    txtYPos.Text += " mm";
            }
            switch (Auth.Role)
            {
                case "SuperUser":
                    prameterToolStripMenuItem.Visible = true;
                    settingToolStripMenuItem.Visible = true;
                    break;
                case "Admin":
                    prameterToolStripMenuItem.Visible = true;
                    settingToolStripMenuItem.Visible = false;
                    break;
                case "User":
                    prameterToolStripMenuItem.Visible = false;
                    settingToolStripMenuItem.Visible = false;
                    break;
                default:
                    break;
            }
        }
        private void txtXPos_Click(object sender, EventArgs e)
        {
            KeysDeActive = true;
        }
        private void txtYPos_Click(object sender, EventArgs e)
        {
            KeysDeActive = true;
        }
        private void PanelPosht_MouseDown(object sender, MouseEventArgs e)
        {
            KeysDeActive = false;
            if (e.Button == MouseButtons.Right)
            {
                cmsPanelMenu.Items.Clear();
                cmsPanelMenu.Items.Add("Cut Ctrl+X");
                cmsPanelMenu.Items.Add("Copy Ctrl+C");
                if (copiedControl != null)
                    cmsPanelMenu.Items.Add("Paste Ctrl+V");
                PanelRo.ContextMenuStrip = cmsPanelMenu;
                cmsPanelMenu.Show();
                cmsPanelMenu.Items[0].Click += new EventHandler(Cut_Click);
                cmsPanelMenu.Items[1].Click += new EventHandler(Copy_Click);
                if (copiedControl != null)
                    cmsPanelMenu.Items[2].Click += new EventHandler(Paste_Click);
            }
        }
        private void btnAcceptLocation_Click(object sender, EventArgs e)
        {
            if (SelectedControl != null)
            {
                flgSaveFlag = true;
                string Xpos = txtXPos.Text, YPos = txtYPos.Text;
                Xpos = Xpos.Replace("mm", ""); Xpos = Xpos.Replace(" ", "");
                YPos = YPos.Replace("mm", ""); YPos = YPos.Replace(" ", "");
                try
                {
                    SelectedControl.Location = ConvermmtoPx(double.Parse(Xpos), double.Parse(YPos));
                }
                catch (Exception)
                {

                    MessageBox.Show("لطفا اعداد را بدرستی وارد نمایید", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("لطفا یک آیتم را انتخاب نمایید", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }
        private void txtXPos_MouseDown(object sender, MouseEventArgs e)
        {
            KeysDeActive = true;
        }
        private void txtYPos_MouseDown(object sender, MouseEventArgs e)
        {
            KeysDeActive = true;
        }
        private void txtXPos_MouseMove(object sender, MouseEventArgs e)
        {
            KeysDeActive = true;
        }
        private void txtYPos_MouseMove(object sender, MouseEventArgs e)
        {
            KeysDeActive = true;
        }
        private void PanelRo_Paint(object sender, PaintEventArgs e)
        {

        }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Saveit();

        }
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Control tmp = CopyControl2(SelectedControl);
            Label lblOldControl = tmp as Label;
            PictureBox PicOldControl = tmp as PictureBox;
            CustomControl.OrientAbleTextControls.OrientedTextLabel OlblOldControl = tmp as CustomControl.OrientAbleTextControls.OrientedTextLabel;
            DevExpress.XtraEditors.BarCodeControl BCOldControl = tmp as DevExpress.XtraEditors.BarCodeControl;
            Array.Resize<string>(ref Actions, Actions.Length + 1);
            Array.Resize<Control>(ref CtrlActions, CtrlActions.Length + 1);
            string FunctionType = e.ChangedItem.ToString();
            if (FunctionType.Contains("System.Windows.Forms.PropertyGridInternal.ImmutablePropertyDescriptorGridEntry"))
            {
                FunctionType = FunctionType.Replace("System.Windows.Forms.PropertyGridInternal.ImmutablePropertyDescriptorGridEntry", "");
                //string aa = e.ChangedItem.PropertyDescriptor.GetChildProperties().ToString();
                //if (e.ChangedItem.PropertyDescriptor..Contains("Margin"))
                //    FunctionType = "Margin" + FunctionType;
                //else if (e.ChangedItem.PropertyDescriptor.ComponentType.Name.Contains("Padding"))
                //    FunctionType = "Padding" + FunctionType;
            }
            else if (FunctionType.Contains("System.Windows.Forms.PropertyGridInternal.PropertyDescriptorGridEntry"))
                FunctionType = FunctionType.Replace("System.Windows.Forms.PropertyGridInternal.PropertyDescriptorGridEntry", "");
            FunctionType = FunctionType.Trim();

            switch (SelectedControl.GetType().ToString())
            {
                case "System.Windows.Forms.PictureBox":
                    {
                        switch (FunctionType)
                        {

                            case "Anchor":
                                {
                                    PicOldControl.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), e.OldValue.ToString());
                                }
                                break;

                            case "BackColor":
                                {
                                    PicOldControl.BackColor = (Color)e.OldValue;
                                }
                                break;
                            case "BackgroundImage":
                                {
                                    PicOldControl.BackgroundImage = (Image)e.OldValue;
                                }
                                break;
                            case "BackgroundImageLayout":
                                {

                                    PicOldControl.BackgroundImageLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), e.OldValue.ToString());
                                }
                                break;
                            case "BorderStyle":
                                {
                                    PicOldControl.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), e.OldValue.ToString());
                                }
                                break;

                            case "ContextMenuStrip":
                                {
                                    PicOldControl.ContextMenuStrip = (ContextMenuStrip)e.OldValue;
                                }
                                break;
                            case "Cursor":
                                {
                                    PicOldControl.Cursor = (Cursor)e.OldValue;
                                }
                                break;
                            case "Dock":
                                {
                                    PicOldControl.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), e.OldValue.ToString());

                                }
                                break;
                            case "Enabled":
                                {
                                    PicOldControl.Enabled = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "ErrorImage":
                                {
                                    PicOldControl.ErrorImage = (Image)e.OldValue;
                                }
                                break;

                            case "Image":
                                {
                                    PicOldControl.Image = (Image)e.OldValue;
                                }
                                break;
                            case "ImageLocation":
                                {
                                    PicOldControl.ImageLocation = e.OldValue.ToString();
                                }
                                break;
                            case "InitialImage":
                                {
                                    PicOldControl.InitialImage = (Image)e.OldValue;
                                }
                                break;
                            case "Location":
                                {
                                    PicOldControl.Location = (Point)e.OldValue;
                                }
                                break;
                            case "Margin":
                                {
                                    PicOldControl.Margin = (Padding)e.OldValue;
                                }
                                break;

                            case "MaximumSize":
                                {
                                    PicOldControl.MaximumSize = (Size)e.OldValue;
                                }
                                break;
                            case "MinimumSize":
                                {
                                    PicOldControl.MinimumSize = (Size)e.OldValue;
                                }
                                break;
                            case "Padding":
                                {
                                    PicOldControl.Padding = (Padding)e.OldValue;
                                }
                                break;

                            case "Size":
                                {
                                    PicOldControl.Size = (Size)e.OldValue;
                                }
                                break;
                            case "SizeMode":
                                {

                                    PicOldControl.SizeMode = (PictureBoxSizeMode)Enum.Parse(typeof(PictureBoxSizeMode), e.OldValue.ToString());
                                }
                                break;
                            case "Tag":
                                {
                                    PicOldControl.Tag = e.OldValue;
                                }
                                break;
                            case "UseWaitCursor":
                                {
                                    PicOldControl.UseWaitCursor = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "Visible":
                                {
                                    PicOldControl.Visible = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "WaitOnLoad":
                                {
                                    PicOldControl.WaitOnLoad = bool.Parse(e.OldValue.ToString());
                                }
                                break;


                            default:
                                break;
                        }
                        CtrlActions[CtrlActions.Length - 1] = CopyControl2(PicOldControl);
                    }
                    break;

                case "System.Windows.Forms.Label":
                    {
                        switch (FunctionType)
                        {

                            case "AllowDrop":
                                {
                                    lblOldControl.AllowDrop = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "Anchor":
                                {
                                    lblOldControl.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), e.OldValue.ToString());
                                }
                                break;
                            case "AutoEllipsis":
                                {
                                    lblOldControl.AutoEllipsis = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "AutoSize":
                                {
                                    lblOldControl.AutoSize = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "BackColor":
                                {
                                    lblOldControl.BackColor = (Color)e.OldValue;
                                }
                                break;
                            case "BorderStyle":
                                {
                                    lblOldControl.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), e.OldValue.ToString());
                                }
                                break;
                            case "CausesValidation":
                                {
                                    lblOldControl.CausesValidation = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "ContextMenuStrip":
                                {
                                    lblOldControl.ContextMenuStrip = (ContextMenuStrip)e.OldValue;
                                }
                                break;
                            case "Cursor":
                                {
                                    lblOldControl.Cursor = (Cursor)e.OldValue;
                                }
                                break;
                            case "Dock":
                                {
                                    lblOldControl.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), e.OldValue.ToString());

                                }
                                break;
                            case "Enabled":
                                {
                                    lblOldControl.Enabled = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "FlatStyle":
                                {
                                    lblOldControl.FlatStyle = (FlatStyle)Enum.Parse(typeof(FlatStyle), e.OldValue.ToString());
                                }
                                break;
                            case "Font":
                                {
                                    lblOldControl.Font = (Font)e.OldValue;
                                }
                                break;
                            case "ForeColor":
                                {
                                    lblOldControl.ForeColor = (Color)e.OldValue;
                                }
                                break;
                            case "Image":
                                {
                                    lblOldControl.Image = (Image)e.OldValue;
                                }
                                break;
                            case "ImageAlign":
                                {
                                    lblOldControl.ImageAlign = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), e.OldValue.ToString());
                                }
                                break;
                            case "ImageIndex":
                                {
                                    lblOldControl.ImageIndex = int.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "ImageKey":
                                {
                                    lblOldControl.ImageKey = e.OldValue.ToString();
                                }
                                break;
                            case "ImageList":
                                {
                                    lblOldControl.ImageList = (ImageList)e.OldValue;
                                }
                                break;
                            case "Location":
                                {
                                    lblOldControl.Location = (Point)e.OldValue;
                                }
                                break;
                            case "Margin":
                                {
                                    lblOldControl.Margin = (Padding)e.OldValue;
                                }
                                break;

                            case "MaximumSize":
                                {
                                    lblOldControl.MaximumSize = (Size)e.OldValue;
                                }
                                break;
                            case "MinimumSize":
                                {
                                    lblOldControl.MinimumSize = (Size)e.OldValue;
                                }
                                break;
                            case "Padding":
                                {
                                    lblOldControl.Padding = (Padding)e.OldValue;
                                }
                                break;
                            case "RightToLeft":
                                {
                                    lblOldControl.RightToLeft = (RightToLeft)Enum.Parse(typeof(RightToLeft), e.OldValue.ToString());
                                }
                                break;
                            case "Size":
                                {
                                    lblOldControl.Size = (Size)e.OldValue;
                                }
                                break;
                            case "TabIndex":
                                {
                                    lblOldControl.TabIndex = int.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "Tag":
                                {
                                    lblOldControl.Tag = e.OldValue;
                                }
                                break;
                            case "Text":
                                {
                                    lblOldControl.Text = e.OldValue.ToString();
                                }
                                break;
                            case "TextAlign":
                                {
                                    lblOldControl.TextAlign = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), e.OldValue.ToString());
                                }
                                break;
                            case "UseCompatibleTextRendering":
                                {
                                    lblOldControl.UseCompatibleTextRendering = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "UseMnemonic":
                                {
                                    lblOldControl.UseMnemonic = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "UseWaitCursor":
                                {
                                    lblOldControl.UseWaitCursor = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "Visible":
                                {
                                    lblOldControl.Visible = bool.Parse(e.OldValue.ToString());
                                }
                                break;


                            default:
                                break;
                        }
                        CtrlActions[CtrlActions.Length - 1] = CopyControl2(lblOldControl);
                    }
                    break;

                case "CustomControl.OrientAbleTextControls.OrientedTextLabel":
                    {
                        switch (FunctionType)
                        {

                            case "AllowDrop":
                                {
                                    OlblOldControl.AllowDrop = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "Anchor":
                                {
                                    OlblOldControl.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), e.OldValue.ToString());
                                }
                                break;
                            case "AutoEllipsis":
                                {
                                    OlblOldControl.AutoEllipsis = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "AutoSize":
                                {
                                    OlblOldControl.AutoSize = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "BackColor":
                                {
                                    OlblOldControl.BackColor = (Color)e.OldValue;
                                }
                                break;
                            case "BorderStyle":
                                {
                                    OlblOldControl.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), e.OldValue.ToString());
                                }
                                break;
                            case "CausesValidation":
                                {
                                    OlblOldControl.CausesValidation = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "ContextMenuStrip":
                                {
                                    OlblOldControl.ContextMenuStrip = (ContextMenuStrip)e.OldValue;
                                }
                                break;
                            case "Cursor":
                                {
                                    OlblOldControl.Cursor = (Cursor)e.OldValue;
                                }
                                break;
                            case "Dock":
                                {
                                    OlblOldControl.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), e.OldValue.ToString());

                                }
                                break;
                            case "Enabled":
                                {
                                    OlblOldControl.Enabled = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "FlatStyle":
                                {
                                    OlblOldControl.FlatStyle = (FlatStyle)Enum.Parse(typeof(FlatStyle), e.OldValue.ToString());
                                }
                                break;
                            case "Font":
                                {
                                    OlblOldControl.Font = (Font)e.OldValue;
                                }
                                break;
                            case "ForeColor":
                                {
                                    OlblOldControl.ForeColor = (Color)e.OldValue;
                                }
                                break;
                            case "Image":
                                {
                                    OlblOldControl.Image = (Image)e.OldValue;
                                }
                                break;
                            case "ImageAlign":
                                {
                                    OlblOldControl.ImageAlign = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), e.OldValue.ToString());
                                }
                                break;
                            case "ImageIndex":
                                {
                                    OlblOldControl.ImageIndex = int.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "ImageKey":
                                {
                                    OlblOldControl.ImageKey = e.OldValue.ToString();
                                }
                                break;
                            case "ImageList":
                                {
                                    OlblOldControl.ImageList = (ImageList)e.OldValue;
                                }
                                break;
                            case "Location":
                                {
                                    OlblOldControl.Location = (Point)e.OldValue;
                                }
                                break;
                            case "Margin":
                                {
                                    OlblOldControl.Margin = (Padding)e.OldValue;
                                }
                                break;

                            case "MaximumSize":
                                {
                                    OlblOldControl.MaximumSize = (Size)e.OldValue;
                                }
                                break;
                            case "MinimumSize":
                                {
                                    OlblOldControl.MinimumSize = (Size)e.OldValue;
                                }
                                break;
                            case "Padding":
                                {
                                    OlblOldControl.Padding = (Padding)e.OldValue;
                                }
                                break;
                            case "RightToLeft":
                                {
                                    OlblOldControl.RightToLeft = (RightToLeft)Enum.Parse(typeof(RightToLeft), e.OldValue.ToString());
                                }
                                break;

                            case "RotationAngle":
                                {
                                    OlblOldControl.RotationAngle = double.Parse(e.OldValue.ToString());
                                }
                                break;

                            case "Size":
                                {
                                    OlblOldControl.Size = (Size)e.OldValue;
                                }
                                break;
                            case "TabIndex":
                                {
                                    OlblOldControl.TabIndex = int.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "Tag":
                                {
                                    OlblOldControl.Tag = e.OldValue;
                                }
                                break;
                            case "Text":
                                {
                                    OlblOldControl.Text = e.OldValue.ToString();
                                }
                                break;
                            case "TextAlign":
                                {
                                    OlblOldControl.TextAlign = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), e.OldValue.ToString());
                                }
                                break;
                            case "TextDirection":
                                {
                                    OlblOldControl.TextDirection = (CustomControl.OrientAbleTextControls.Direction)Enum.Parse(typeof(CustomControl.OrientAbleTextControls.Direction), e.OldValue.ToString());
                                }
                                break;
                            case "TextOrientation":
                                {
                                    OlblOldControl.TextOrientation = (CustomControl.OrientAbleTextControls.Orientation)Enum.Parse(typeof(CustomControl.OrientAbleTextControls.Orientation), e.OldValue.ToString());
                                }
                                break;
                            case "UseCompatibleTextRendering":
                                {
                                    OlblOldControl.UseCompatibleTextRendering = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "UseMnemonic":
                                {
                                    OlblOldControl.UseMnemonic = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "UseWaitCursor":
                                {
                                    OlblOldControl.UseWaitCursor = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "Visible":
                                {
                                    OlblOldControl.Visible = bool.Parse(e.OldValue.ToString());
                                }
                                break;


                            default:
                                break;
                        }
                        CtrlActions[CtrlActions.Length - 1] = CopyControl2(OlblOldControl);
                    }
                    break;

                case "DevExpress.XtraEditors.BarCodeControl":
                    {
                        switch (FunctionType)
                        {

                            case "AllowDrop":
                                {
                                    BCOldControl.AllowDrop = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "AllowHtmlTextInToolTip":
                                {

                                    BCOldControl.AllowHtmlTextInToolTip = (DevExpress.Utils.DefaultBoolean)Enum.Parse(typeof(DevExpress.Utils.DefaultBoolean), e.OldValue.ToString());
                                }
                                break;
                            case "Anchor":
                                {
                                    BCOldControl.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), e.OldValue.ToString());
                                }
                                break;
                            case "AutoModule":
                                {
                                    BCOldControl.AutoModule = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "BackColor":
                                {
                                    BCOldControl.BackColor = (Color)e.OldValue;
                                }
                                break;
                            case "BackgroundImage":
                                {
                                    BCOldControl.BackgroundImage = (Image)e.OldValue;
                                }
                                break;
                            case "BackgroundImageLayout":
                                {
                                    BCOldControl.BackgroundImageLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), e.OldValue.ToString());
                                }
                                break;
                            case "BinaryData":
                                {
                                    BCOldControl.BinaryData = (byte[])e.OldValue;
                                }
                                break;
                            case "BorderStyle":
                                {
                                    BCOldControl.BorderStyle = (DevExpress.XtraEditors.Controls.BorderStyles)Enum.Parse(typeof(DevExpress.XtraEditors.Controls.BorderStyles), e.OldValue.ToString());
                                }
                                break;
                            case "CausesValidation":
                                {
                                    BCOldControl.CausesValidation = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "ContextMenuStrip":
                                {
                                    BCOldControl.ContextMenuStrip = (ContextMenuStrip)e.OldValue;
                                }
                                break;
                            case "Cursor":
                                {
                                    BCOldControl.Cursor = (Cursor)e.OldValue;
                                }
                                break;
                            case "Dock":
                                {
                                    BCOldControl.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), e.OldValue.ToString());
                                }
                                break;
                            case "Enabled":
                                {
                                    BCOldControl.Enabled = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "Font":
                                {
                                    BCOldControl.Font = (Font)e.OldValue;
                                }
                                break;
                            case "ForeColor":
                                {
                                    BCOldControl.ForeColor = (Color)e.OldValue;
                                }
                                break;
                            case "HorizontalAlignment":
                                {
                                    BCOldControl.HorizontalAlignment = (DevExpress.Utils.HorzAlignment)Enum.Parse(typeof(DevExpress.Utils.HorzAlignment), e.OldValue.ToString());
                                }
                                break;
                            case "HorizontalTextAlignment":
                                {
                                    BCOldControl.HorizontalTextAlignment = (DevExpress.Utils.HorzAlignment)Enum.Parse(typeof(DevExpress.Utils.HorzAlignment), e.OldValue.ToString());
                                }
                                break;
                            case "ImeMode":
                                {
                                    BCOldControl.ImeMode = (ImeMode)Enum.Parse(typeof(ImeMode), e.OldValue.ToString());
                                }
                                break;
                            case "Location":
                                {
                                    BCOldControl.Location = (Point)e.OldValue;
                                }
                                break;
                            case "Margin":
                                {
                                    BCOldControl.Margin = (Padding)e.OldValue;
                                }
                                break;

                            case "MaximumSize":
                                {
                                    BCOldControl.MaximumSize = (Size)e.OldValue;
                                }
                                break;
                            case "MinimumSize":
                                {
                                    BCOldControl.MinimumSize = (Size)e.OldValue;
                                }
                                break;
                            case "Module":
                                {
                                    BCOldControl.Module = double.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "Orientation":
                                {
                                    BCOldControl.Orientation = (DevExpress.XtraPrinting.BarCode.BarCodeOrientation)Enum.Parse(typeof(DevExpress.XtraPrinting.BarCode.BarCodeOrientation), e.OldValue.ToString());
                                }
                                break;
                            case "Padding":
                                {
                                    BCOldControl.Padding = (Padding)e.OldValue;
                                }
                                break;
                            case "RightToLeft":
                                {
                                    BCOldControl.RightToLeft = (RightToLeft)Enum.Parse(typeof(RightToLeft), e.OldValue.ToString());
                                }
                                break;
                            case "ShowText":
                                {
                                    BCOldControl.ShowText = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "ShowToolTips":
                                {
                                    BCOldControl.ShowToolTips = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "Size":
                                {
                                    BCOldControl.Size = (Size)e.OldValue;
                                }
                                break;
                            case "StyleController":
                                {
                                    BCOldControl.StyleController = (DevExpress.XtraEditors.IStyleController)e.OldValue;
                                }
                                break;
                            case "SuperTip":
                                {
                                    BCOldControl.SuperTip = (DevExpress.Utils.SuperToolTip)e.OldValue;
                                }
                                break;
                            case "Symbology":
                                {
                                    BCOldControl.Symbology = (DevExpress.XtraPrinting.BarCode.BarCodeGeneratorBase)e.OldValue;
                                }
                                break;
                            case "TabIndex":
                                {
                                    BCOldControl.TabIndex = int.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "TabStop":
                                {
                                    BCOldControl.TabStop = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "Tag":
                                {
                                    BCOldControl.Tag = e.OldValue;
                                }
                                break;
                            case "Text":
                                {
                                    BCOldControl.Text = e.OldValue.ToString();
                                }
                                break;
                            case "ToolTip":
                                {
                                    BCOldControl.ToolTip = e.OldValue.ToString();
                                }
                                break;
                            case "ToolTipController":
                                {
                                    BCOldControl.ToolTipController = (DevExpress.Utils.ToolTipController)e.OldValue;
                                }
                                break;
                            case "ToolTipIconType":
                                {
                                    BCOldControl.ToolTipIconType = (DevExpress.Utils.ToolTipIconType)Enum.Parse(typeof(DevExpress.Utils.ToolTipIconType), e.OldValue.ToString());
                                }
                                break;
                            case "ToolTipTitle":
                                {
                                    BCOldControl.ToolTipTitle = e.OldValue.ToString();
                                }
                                break;
                            case "UseWaitCursor":
                                {
                                    BCOldControl.UseWaitCursor = bool.Parse(e.OldValue.ToString());
                                }
                                break;
                            case "VerticalAlignment":
                                {
                                    BCOldControl.VerticalAlignment = (DevExpress.Utils.VertAlignment)Enum.Parse(typeof(DevExpress.Utils.VertAlignment), e.OldValue.ToString());
                                }
                                break;
                            case "VerticalTextAlignment":
                                {
                                    BCOldControl.VerticalTextAlignment = (DevExpress.Utils.VertAlignment)Enum.Parse(typeof(DevExpress.Utils.VertAlignment), e.OldValue.ToString());
                                }
                                break;
                            case "Visible":
                                {
                                    BCOldControl.Visible = bool.Parse(e.OldValue.ToString());
                                }
                                break;


                            default:
                                break;
                        }
                        CtrlActions[CtrlActions.Length - 1] = CopyControl2(BCOldControl);
                    }
                    break;
                default:
                    break;
            }
            if (tabControl1.SelectedTab == TopDesignTab)
            {
                Actions[Actions.Length - 1] = ActionType.PropertyGrid.ToString() + " " + DesignAction.TopPropertyActGrid;
            }
            else if (tabControl1.SelectedTab == BottomDesignTab)
            {
                Actions[Actions.Length - 1] = ActionType.PropertyGrid.ToString() + " " + DesignAction.BottomPropertyActGrid;
            }

        }

        private void radButton1_Click(object sender, EventArgs e)
        {

            // const string InfoName = "LowQuntity_Ex.xlsx";
            // const string InfoData = "ردیفßنامßنام خانوادگیßكد ملیßشماره پرسنلیßPictureßواحد عملیات";
            // const string ExcelFolderPath = @"C:\Users\Mehrdad_9127048636\Desktop\SarahardwareSample\LowQuntity_Ex.xlsx";
            //// const string ExcelFolderPath ="Hello";
            // const string ExcelFileName = "LowQuntity_Ex.xlsx";
            // const string PictureFolderPath = @"C:\Users\Mehrdad_9127048636\Desktop\SarahardwareSample\AKS";
            // const string PictureFormat = ".jpg";


            // new dl.InfoModel().Insert(InfoName, InfoData,PictureFolderPath, PictureFormat);

        }

        private void CAOSpecificCardLaserPenManagmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmCaoFullPenManagement().ShowDialog();
        }

        private void BtnCMCCard_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmCrewMemberCertificate_C_M_C_().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void BtnInspectionCard_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmCivilAviationInspectorCertificate().ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void BtnUlCard_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            new frmUltraLightLicenceCard().ShowDialog();
            this.Cursor = Cursors.Default;
        }











        //private void Button1_Click(object sender, EventArgs e)
        //{
        //    string hich = "";
        //    if (CR.WriteMagnetticMode(ref hich, CR.MagneticWriteMode.High_Co) == CR.ReturnDeviceStatus.MB_OK)
        //        Hardware.CR.Initialize(Hardware.CR.Inittype.KeepInside, ref hich);
        //    else
        //    {
        //        Hardware.CR.Initialize(Hardware.CR.Inittype.KeepInside, ref hich);
        //        MessageBox.Show("Write Mode Not Ok");
        //    }
        //    string[] Data =
        //    {
        //        "",
        //        "6037993199716903=99051015684000000000",
        //        ""


        //    };
        //    bool[] WhichData =
        //    {
        //        false,
        //        true,
        //        false
        //    };
        //    CR.WriteAllMagneticTrack(ref hich, Data, WhichData);
        //}

        private void penManagementToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            PenManagement Penmangment = new PenManagement();
            Penmangment.ShowDialog();

        }
        #endregion
    }
}
