namespace TOEC_Inspection
{
    partial class frm_CopyLastImage
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
            this.btn_Copy = new System.Windows.Forms.Button();
            this.combo_TrainType = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.combo_Side = new DevExpress.XtraEditors.ComboBoxEdit();
            this.combo_PicType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txt_LastCount = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.combo_TrainType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.combo_Side.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.combo_PicType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_LastCount.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Copy
            // 
            this.btn_Copy.Location = new System.Drawing.Point(14, 154);
            this.btn_Copy.Name = "btn_Copy";
            this.btn_Copy.Size = new System.Drawing.Size(230, 23);
            this.btn_Copy.TabIndex = 5;
            this.btn_Copy.Text = "走你";
            this.btn_Copy.UseVisualStyleBackColor = true;
            this.btn_Copy.Click += new System.EventHandler(this.btn_Copy_Click);
            // 
            // combo_TrainType
            // 
            this.combo_TrainType.EditValue = "货";
            this.combo_TrainType.Location = new System.Drawing.Point(14, 86);
            this.combo_TrainType.Name = "combo_TrainType";
            this.combo_TrainType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combo_TrainType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("货", "货车", System.Windows.Forms.CheckState.Checked),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("客", "客车"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("动", "动车")});
            this.combo_TrainType.Size = new System.Drawing.Size(230, 20);
            this.combo_TrainType.TabIndex = 9;
            // 
            // combo_Side
            // 
            this.combo_Side.Location = new System.Drawing.Point(14, 50);
            this.combo_Side.Name = "combo_Side";
            this.combo_Side.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combo_Side.Properties.Items.AddRange(new object[] {
            "L",
            "R"});
            this.combo_Side.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.combo_Side.Size = new System.Drawing.Size(230, 20);
            this.combo_Side.TabIndex = 10;
            // 
            // combo_PicType
            // 
            this.combo_PicType.Location = new System.Drawing.Point(14, 12);
            this.combo_PicType.Name = "combo_PicType";
            this.combo_PicType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combo_PicType.Properties.Items.AddRange(new object[] {
            "GQPics",
            "ZXGQPics"});
            this.combo_PicType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.combo_PicType.Size = new System.Drawing.Size(230, 20);
            this.combo_PicType.TabIndex = 10;
            // 
            // txt_LastCount
            // 
            this.txt_LastCount.EditValue = "2";
            this.txt_LastCount.Location = new System.Drawing.Point(93, 118);
            this.txt_LastCount.Name = "txt_LastCount";
            this.txt_LastCount.Size = new System.Drawing.Size(151, 20);
            this.txt_LastCount.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 14);
            this.label1.TabIndex = 12;
            this.label1.Text = "倒数几张图:";
            // 
            // frm_CopyLastImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 188);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_LastCount);
            this.Controls.Add(this.combo_PicType);
            this.Controls.Add(this.combo_Side);
            this.Controls.Add(this.combo_TrainType);
            this.Controls.Add(this.btn_Copy);
            this.Name = "frm_CopyLastImage";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "拷贝最后一张图";
            ((System.ComponentModel.ISupportInitialize)(this.combo_TrainType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.combo_Side.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.combo_PicType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_LastCount.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Copy;
        private DevExpress.XtraEditors.CheckedComboBoxEdit combo_TrainType;
        private DevExpress.XtraEditors.ComboBoxEdit combo_Side;
        private DevExpress.XtraEditors.ComboBoxEdit combo_PicType;
        private DevExpress.XtraEditors.TextEdit txt_LastCount;
        private System.Windows.Forms.Label label1;
    }
}