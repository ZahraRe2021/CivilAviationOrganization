
namespace SaraPrinterLaser
{
    partial class GetImageFromPanel
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
            this.PanelRo = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // PanelRo
            // 
            this.PanelRo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PanelRo.BackColor = System.Drawing.Color.White;
            this.PanelRo.Location = new System.Drawing.Point(12, 12);
            this.PanelRo.Name = "PanelRo";
            this.PanelRo.Size = new System.Drawing.Size(674, 425);
            this.PanelRo.TabIndex = 1;
            // 
            // GetImageFromPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 450);
            this.Controls.Add(this.PanelRo);
            this.Name = "GetImageFromPanel";
            this.Text = "GetImageFromPanel";
            this.Load += new System.EventHandler(this.GetImageFromPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelRo;
    }
}