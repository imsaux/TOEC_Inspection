namespace TOEC_Inspection
{
    partial class frm_CopyProblemImge
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
            this.btn_ChosePath = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.toggle_Chose = new DevExpress.XtraEditors.ToggleSwitch();
            ((System.ComponentModel.ISupportInitialize)(this.toggle_Chose.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ChosePath
            // 
            this.btn_ChosePath.Location = new System.Drawing.Point(9, 7);
            this.btn_ChosePath.Name = "btn_ChosePath";
            this.btn_ChosePath.Size = new System.Drawing.Size(101, 23);
            this.btn_ChosePath.TabIndex = 0;
            this.btn_ChosePath.Text = "选择保存路径";
            this.btn_ChosePath.Click += new System.EventHandler(this.btn_ChosePath_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(116, 7);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(101, 23);
            this.btn_Save.TabIndex = 0;
            this.btn_Save.Text = "开始保存";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // toggle_Chose
            // 
            this.toggle_Chose.Location = new System.Drawing.Point(223, 6);
            this.toggle_Chose.Name = "toggle_Chose";
            this.toggle_Chose.Properties.OffText = "不选";
            this.toggle_Chose.Properties.OnText = "全选";
            this.toggle_Chose.Size = new System.Drawing.Size(100, 25);
            this.toggle_Chose.TabIndex = 1;
            this.toggle_Chose.Toggled += new System.EventHandler(this.toggle_Chose_Toggled);
            // 
            // frm_CopyImge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 37);
            this.Controls.Add(this.toggle_Chose);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_ChosePath);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_CopyImge";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "拷贝问题图片";
            this.Load += new System.EventHandler(this.frm_CopyImge_Load);
            ((System.ComponentModel.ISupportInitialize)(this.toggle_Chose.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_ChosePath;
        private DevExpress.XtraEditors.SimpleButton btn_Save;
        private DevExpress.XtraEditors.ToggleSwitch toggle_Chose;
    }
}