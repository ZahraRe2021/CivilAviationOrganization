namespace SaraPrinterLaser
{
    partial class PrintingPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintingPage));
            this.timerStatus = new System.Windows.Forms.Timer(this.components);
            this.flpError = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblState = new Telerik.WinControls.UI.RadLabel();
            this.flpProcessName = new System.Windows.Forms.FlowLayoutPanel();
            this.flpProcessTik = new System.Windows.Forms.FlowLayoutPanel();
            this.PbarPrint = new Telerik.WinControls.UI.RadProgressBar();
            this.PbarCount = new Telerik.WinControls.UI.RadProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CancelPrint = new System.Windows.Forms.Button();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.lblState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbarPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbarCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.SuspendLayout();
            // 
            // timerStatus
            // 
            this.timerStatus.Interval = 20;
            this.timerStatus.Tick += new System.EventHandler(this.timerStatus_Tick);
            // 
            // flpError
            // 
            this.flpError.Location = new System.Drawing.Point(157, 73);
            this.flpError.Name = "flpError";
            this.flpError.Size = new System.Drawing.Size(510, 327);
            this.flpError.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(246, 426);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(101, 48);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "ثبت";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblState
            // 
            this.lblState.AutoScroll = true;
            this.lblState.AutoSize = false;
            this.lblState.Location = new System.Drawing.Point(24, 406);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(199, 18);
            this.lblState.TabIndex = 4;
            this.lblState.Text = "radLabel10";
            this.lblState.Visible = false;
            this.lblState.Click += new System.EventHandler(this.lblState_Click);
            // 
            // flpProcessName
            // 
            this.flpProcessName.Location = new System.Drawing.Point(20, 73);
            this.flpProcessName.Name = "flpProcessName";
            this.flpProcessName.Size = new System.Drawing.Size(97, 327);
            this.flpProcessName.TabIndex = 5;
            // 
            // flpProcessTik
            // 
            this.flpProcessTik.Location = new System.Drawing.Point(123, 73);
            this.flpProcessTik.Name = "flpProcessTik";
            this.flpProcessTik.Size = new System.Drawing.Size(28, 327);
            this.flpProcessTik.TabIndex = 6;
            // 
            // PbarPrint
            // 
            this.PbarPrint.Location = new System.Drawing.Point(20, 43);
            this.PbarPrint.Name = "PbarPrint";
            this.PbarPrint.Size = new System.Drawing.Size(510, 24);
            this.PbarPrint.TabIndex = 7;
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
            // PbarCount
            // 
            this.PbarCount.Location = new System.Drawing.Point(20, 13);
            this.PbarCount.Name = "PbarCount";
            this.PbarCount.Size = new System.Drawing.Size(510, 24);
            this.PbarCount.TabIndex = 8;
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
            this.label1.Location = new System.Drawing.Point(536, 14);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(127, 23);
            this.label1.TabIndex = 9;
            this.label1.Text = "تعداد چاپ های باقیمانده";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("B Nazanin", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label2.Location = new System.Drawing.Point(536, 44);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(132, 23);
            this.label2.TabIndex = 10;
            this.label2.Text = "تعداد عمل های انجام شده";
            // 
            // CancelPrint
            // 
            this.CancelPrint.Location = new System.Drawing.Point(353, 426);
            this.CancelPrint.Name = "CancelPrint";
            this.CancelPrint.Size = new System.Drawing.Size(101, 48);
            this.CancelPrint.TabIndex = 11;
            this.CancelPrint.Text = "توقف چاپ";
            this.CancelPrint.UseVisualStyleBackColor = true;
            this.CancelPrint.Click += new System.EventHandler(this.CancelPrint_Click);
            // 
            // radLabel1
            // 
            this.radLabel1.AutoScroll = true;
            this.radLabel1.AutoSize = false;
            this.radLabel1.Location = new System.Drawing.Point(229, 406);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radLabel1.Size = new System.Drawing.Size(438, 18);
            this.radLabel1.TabIndex = 5;
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // PrintingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 486);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PrintingPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "صفحه چاپ";
            this.Load += new System.EventHandler(this.PrintingPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lblState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbarPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbarCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timerStatus;
        private System.Windows.Forms.FlowLayoutPanel flpError;
        private System.Windows.Forms.Button btnOk;
        private Telerik.WinControls.UI.RadLabel lblState;
        private System.Windows.Forms.FlowLayoutPanel flpProcessName;
        private System.Windows.Forms.FlowLayoutPanel flpProcessTik;
        private Telerik.WinControls.UI.RadProgressBar PbarPrint;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadProgressBar PbarCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CancelPrint;
        private Telerik.WinControls.UI.RadLabel radLabel1;
    }
}