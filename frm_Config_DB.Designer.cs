namespace TOEC_Inspection
{
    partial class frm_Config_DB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Config_DB));
            this.btn_DBTest = new DevExpress.XtraEditors.SimpleButton();
            this.txt_IP_Inspection = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txt_IP_Inspection.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_DBTest
            // 
            this.btn_DBTest.Image = ((System.Drawing.Image)(resources.GetObject("btn_DBTest.Image")));
            this.btn_DBTest.Location = new System.Drawing.Point(126, 47);
            this.btn_DBTest.Name = "btn_DBTest";
            this.btn_DBTest.Size = new System.Drawing.Size(117, 25);
            this.btn_DBTest.TabIndex = 0;
            this.btn_DBTest.Text = "测试连接并保存";
            this.btn_DBTest.Click += new System.EventHandler(this.btn_DBTest_Click);
            // 
            // txt_IP_Inspection
            // 
            this.txt_IP_Inspection.Location = new System.Drawing.Point(81, 12);
            this.txt_IP_Inspection.Name = "txt_IP_Inspection";
            this.txt_IP_Inspection.Size = new System.Drawing.Size(162, 20);
            this.txt_IP_Inspection.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(23, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(47, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "巡检库IP";
            // 
            // frm_Config_DB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 83);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txt_IP_Inspection);
            this.Controls.Add(this.btn_DBTest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Config_DB";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "巡检数据库配置";
            ((System.ComponentModel.ISupportInitialize)(this.txt_IP_Inspection.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_DBTest;
        private DevExpress.XtraEditors.TextEdit txt_IP_Inspection;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}