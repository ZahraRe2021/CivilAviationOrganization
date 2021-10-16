namespace SaraPrinterLaser
{
    partial class RejectForm
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
            this.flpError = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.timerStatus = new System.Windows.Forms.Timer(this.components);
            this.lblState = new Telerik.WinControls.UI.RadLabel();
            this.PbarPrint = new Telerik.WinControls.UI.RadProgressBar();
            this.flpProcessTik = new System.Windows.Forms.FlowLayoutPanel();
            this.flpProcessName = new System.Windows.Forms.FlowLayoutPanel();
            this.lblModel = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.lblState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbarPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblModel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // flpError
            // 
            this.flpError.Location = new System.Drawing.Point(150, 41);
            this.flpError.Name = "flpError";
            this.flpError.Size = new System.Drawing.Size(510, 341);
            this.flpError.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(318, 422);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // timerStatus
            // 
            this.timerStatus.Interval = 200;
            this.timerStatus.Tick += new System.EventHandler(this.timerStatus_Tick);
            // 
            // lblState
            // 
            this.lblState.Location = new System.Drawing.Point(55, 450);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(32, 18);
            this.lblState.TabIndex = 4;
            this.lblState.Text = "State";
            // 
            // PbarPrint
            // 
            this.PbarPrint.Location = new System.Drawing.Point(150, 10);
            this.PbarPrint.Name = "PbarPrint";
            this.PbarPrint.Size = new System.Drawing.Size(510, 24);
            this.PbarPrint.TabIndex = 10;
            this.PbarPrint.Text = "pbar";
            // 
            // flpProcessTik
            // 
            this.flpProcessTik.Location = new System.Drawing.Point(116, 41);
            this.flpProcessTik.Name = "flpProcessTik";
            this.flpProcessTik.Size = new System.Drawing.Size(28, 341);
            this.flpProcessTik.TabIndex = 9;
            // 
            // flpProcessName
            // 
            this.flpProcessName.Location = new System.Drawing.Point(12, 41);
            this.flpProcessName.Name = "flpProcessName";
            this.flpProcessName.Size = new System.Drawing.Size(97, 341);
            this.flpProcessName.TabIndex = 8;
            // 
            // lblModel
            // 
            this.lblModel.Location = new System.Drawing.Point(55, 422);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(38, 18);
            this.lblModel.TabIndex = 11;
            this.lblModel.Text = "Model";
            // 
            // RejectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 480);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.PbarPrint);
            this.Controls.Add(this.flpProcessTik);
            this.Controls.Add(this.flpProcessName);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.flpError);
            this.Name = "RejectForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "پاکسازی";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RejectForm_FormClosing);
            this.Load += new System.EventHandler(this.RejectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lblState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbarPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblModel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flpError;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Timer timerStatus;
        private Telerik.WinControls.UI.RadLabel lblState;
        private Telerik.WinControls.UI.RadProgressBar PbarPrint;
        private System.Windows.Forms.FlowLayoutPanel flpProcessTik;
        private System.Windows.Forms.FlowLayoutPanel flpProcessName;
        private Telerik.WinControls.UI.RadLabel lblModel;
    }
}
