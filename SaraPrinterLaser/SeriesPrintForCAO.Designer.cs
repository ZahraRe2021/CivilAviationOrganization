namespace SaraPrinterLaser
{
    partial class SeriesPrintForCAO
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerStatus = new System.Windows.Forms.Timer(this.components);
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.CancelPrint = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.PbarCount = new Telerik.WinControls.UI.RadProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.PbarPrint = new Telerik.WinControls.UI.RadProgressBar();
            this.flpProcessTik = new System.Windows.Forms.FlowLayoutPanel();
            this.flpProcessName = new System.Windows.Forms.FlowLayoutPanel();
            this.lblState = new Telerik.WinControls.UI.RadLabel();
            this.btnOk = new System.Windows.Forms.Button();
            this.flpError = new System.Windows.Forms.FlowLayoutPanel();
            this.TimerCreatePicture = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbarCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbarPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblState)).BeginInit();
            this.SuspendLayout();
            // 
            // timerStatus
            // 
            this.timerStatus.Interval = 20;
            this.timerStatus.Tick += new System.EventHandler(this.TimerStatus_Tick);
            // 
            // radLabel1
            // 
            this.radLabel1.AutoScroll = true;
            this.radLabel1.AutoSize = false;
            this.radLabel1.Location = new System.Drawing.Point(239, 408);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radLabel1.Size = new System.Drawing.Size(438, 18);
            this.radLabel1.TabIndex = 15;
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // CancelPrint
            // 
            this.CancelPrint.Location = new System.Drawing.Point(363, 428);
            this.CancelPrint.Name = "CancelPrint";
            this.CancelPrint.Size = new System.Drawing.Size(101, 48);
            this.CancelPrint.TabIndex = 22;
            this.CancelPrint.Text = "توقف چاپ";
            this.CancelPrint.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("B Nazanin", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label2.Location = new System.Drawing.Point(546, 46);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(132, 23);
            this.label2.TabIndex = 21;
            this.label2.Text = "تعداد عمل های انجام شده";
            // 
            // PbarCount
            // 
            this.PbarCount.Location = new System.Drawing.Point(30, 15);
            this.PbarCount.Name = "PbarCount";
            this.PbarCount.Size = new System.Drawing.Size(510, 24);
            this.PbarCount.TabIndex = 19;
            this.PbarCount.Text = "pbar";
            ((Telerik.WinControls.UI.RadProgressBarElement)(this.PbarCount.GetChildAt(0))).Text = "pbar";
            ((Telerik.WinControls.UI.ProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.LightGreen;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.LightGreen;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.LightGreen;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.LightGreen;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(0))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.UI.UpperProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(1))).BackColor2 = System.Drawing.Color.Green;
            ((Telerik.WinControls.UI.UpperProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(1))).BackColor3 = System.Drawing.Color.Green;
            ((Telerik.WinControls.UI.UpperProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(1))).BackColor4 = System.Drawing.Color.Green;
            ((Telerik.WinControls.UI.UpperProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.Green;
            ((Telerik.WinControls.UI.UpperProgressIndicatorElement)(this.PbarCount.GetChildAt(0).GetChildAt(1))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("B Nazanin", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label1.Location = new System.Drawing.Point(546, 16);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(127, 23);
            this.label1.TabIndex = 20;
            this.label1.Text = "تعداد چاپ های باقیمانده";
            // 
            // PbarPrint
            // 
            this.PbarPrint.Location = new System.Drawing.Point(30, 45);
            this.PbarPrint.Name = "PbarPrint";
            this.PbarPrint.Size = new System.Drawing.Size(510, 24);
            this.PbarPrint.TabIndex = 18;
            this.PbarPrint.Text = "pbar";
            ((Telerik.WinControls.UI.RadProgressBarElement)(this.PbarPrint.GetChildAt(0))).Text = "pbar";
            ((Telerik.WinControls.UI.ProgressIndicatorElement)(this.PbarPrint.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.LightGreen;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)(this.PbarPrint.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.LightGreen;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)(this.PbarPrint.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.LightGreen;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)(this.PbarPrint.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.LightGreen;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)(this.PbarPrint.GetChildAt(0).GetChildAt(0))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.UI.UpperProgressIndicatorElement)(this.PbarPrint.GetChildAt(0).GetChildAt(1))).BackColor2 = System.Drawing.Color.Green;
            ((Telerik.WinControls.UI.UpperProgressIndicatorElement)(this.PbarPrint.GetChildAt(0).GetChildAt(1))).BackColor3 = System.Drawing.Color.Green;
            ((Telerik.WinControls.UI.UpperProgressIndicatorElement)(this.PbarPrint.GetChildAt(0).GetChildAt(1))).BackColor4 = System.Drawing.Color.Green;
            ((Telerik.WinControls.UI.UpperProgressIndicatorElement)(this.PbarPrint.GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.Green;
            ((Telerik.WinControls.UI.UpperProgressIndicatorElement)(this.PbarPrint.GetChildAt(0).GetChildAt(1))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // flpProcessTik
            // 
            this.flpProcessTik.Location = new System.Drawing.Point(133, 75);
            this.flpProcessTik.Name = "flpProcessTik";
            this.flpProcessTik.Size = new System.Drawing.Size(28, 327);
            this.flpProcessTik.TabIndex = 17;
            // 
            // flpProcessName
            // 
            this.flpProcessName.Location = new System.Drawing.Point(30, 75);
            this.flpProcessName.Name = "flpProcessName";
            this.flpProcessName.Size = new System.Drawing.Size(97, 327);
            this.flpProcessName.TabIndex = 16;
            // 
            // lblState
            // 
            this.lblState.AutoScroll = true;
            this.lblState.AutoSize = false;
            this.lblState.Location = new System.Drawing.Point(34, 408);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(199, 18);
            this.lblState.TabIndex = 14;
            this.lblState.Text = "radLabel10";
            this.lblState.Visible = false;
            // 
            // btnOk
            // 
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(256, 428);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(101, 48);
            this.btnOk.TabIndex = 13;
            this.btnOk.Text = "ثبت";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // flpError
            // 
            this.flpError.Location = new System.Drawing.Point(167, 75);
            this.flpError.Name = "flpError";
            this.flpError.Size = new System.Drawing.Size(510, 327);
            this.flpError.TabIndex = 12;
            // 
            // TimerCreatePicture
            // 
            this.TimerCreatePicture.Tick += new System.EventHandler(this.TimerCreatePicture_Tick);
            // 
            // PrintingPageSeries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 490);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.CancelPrint);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PbarCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PbarPrint);
            this.Controls.Add(this.flpProcessTik);
            this.Controls.Add(this.flpProcessName);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.flpError);
            this.Name = "PrintingPageSeries";
            this.Text = "PrintingPageSeries";
            this.Load += new System.EventHandler(this.PrintingPageSeries_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbarCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbarPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblState)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerStatus;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private System.Windows.Forms.Button CancelPrint;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadProgressBar PbarCount;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadProgressBar PbarPrint;
        private System.Windows.Forms.FlowLayoutPanel flpProcessTik;
        private System.Windows.Forms.FlowLayoutPanel flpProcessName;
        private Telerik.WinControls.UI.RadLabel lblState;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.FlowLayoutPanel flpError;
        private System.Windows.Forms.Timer TimerCreatePicture;
    }
}