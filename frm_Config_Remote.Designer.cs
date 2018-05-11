namespace TOEC_Inspection
{
    partial class frm_Config_Remote
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Config_Remote));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btn_Save = new DevExpress.XtraBars.BarButtonItem();
            this.btn_Refresh = new DevExpress.XtraBars.BarButtonItem();
            this.rp1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.gc_Setting = new DevExpress.XtraGrid.GridControl();
            this.gv_Setting = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcol_Key = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcol_Meaning = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcol_Value = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lbl_ConnectStatus = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Setting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Setting)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.btn_Save,
            this.btn_Refresh,
            this.lbl_ConnectStatus});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 4;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rp1});
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.ShowOnMultiplePages;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(875, 98);
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // btn_Save
            // 
            this.btn_Save.Caption = "保存";
            this.btn_Save.Glyph = ((System.Drawing.Image)(resources.GetObject("btn_Save.Glyph")));
            this.btn_Save.Id = 1;
            this.btn_Save.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btn_Save.LargeGlyph")));
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_Save_ItemClick);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Caption = "刷新";
            this.btn_Refresh.Glyph = ((System.Drawing.Image)(resources.GetObject("btn_Refresh.Glyph")));
            this.btn_Refresh.Id = 1;
            this.btn_Refresh.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btn_Refresh.LargeGlyph")));
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_Refresh_ItemClick);
            // 
            // rp1
            // 
            this.rp1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.rp1.Name = "rp1";
            this.rp1.Text = "远程配置管理";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btn_Save);
            this.ribbonPageGroup1.ItemLinks.Add(this.btn_Refresh);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "基本操作";
            // 
            // gc_Setting
            // 
            this.gc_Setting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_Setting.Location = new System.Drawing.Point(0, 98);
            this.gc_Setting.MainView = this.gv_Setting;
            this.gc_Setting.MenuManager = this.ribbonControl1;
            this.gc_Setting.Name = "gc_Setting";
            this.gc_Setting.Size = new System.Drawing.Size(875, 344);
            this.gc_Setting.TabIndex = 1;
            this.gc_Setting.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_Setting});
            // 
            // gv_Setting
            // 
            this.gv_Setting.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcol_Key,
            this.gcol_Meaning,
            this.gcol_Value});
            this.gv_Setting.GridControl = this.gc_Setting;
            this.gv_Setting.Name = "gv_Setting";
            this.gv_Setting.OptionsView.ShowGroupPanel = false;
            // 
            // gcol_Key
            // 
            this.gcol_Key.Caption = "代码";
            this.gcol_Key.FieldName = "Key";
            this.gcol_Key.Name = "gcol_Key";
            this.gcol_Key.OptionsColumn.AllowEdit = false;
            this.gcol_Key.Width = 126;
            // 
            // gcol_Meaning
            // 
            this.gcol_Meaning.Caption = "含义";
            this.gcol_Meaning.FieldName = "Meaning";
            this.gcol_Meaning.Name = "gcol_Meaning";
            this.gcol_Meaning.OptionsColumn.AllowEdit = false;
            this.gcol_Meaning.Visible = true;
            this.gcol_Meaning.VisibleIndex = 0;
            this.gcol_Meaning.Width = 137;
            // 
            // gcol_Value
            // 
            this.gcol_Value.Caption = "值";
            this.gcol_Value.FieldName = "Value";
            this.gcol_Value.Name = "gcol_Value";
            this.gcol_Value.Visible = true;
            this.gcol_Value.VisibleIndex = 1;
            this.gcol_Value.Width = 594;
            // 
            // lbl_ConnectStatus
            // 
            this.lbl_ConnectStatus.Caption = "连接状态";
            this.lbl_ConnectStatus.Glyph = ((System.Drawing.Image)(resources.GetObject("lbl_ConnectStatus.Glyph")));
            this.lbl_ConnectStatus.Id = 2;
            this.lbl_ConnectStatus.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("lbl_ConnectStatus.LargeGlyph")));
            this.lbl_ConnectStatus.Name = "lbl_ConnectStatus";
            this.lbl_ConnectStatus.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.lbl_ConnectStatus);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "连接状态";
            // 
            // frm_Config_Remote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 442);
            this.Controls.Add(this.gc_Setting);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "frm_Config_Remote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "远程配置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_Config_Remote_FormClosed);
            this.Load += new System.EventHandler(this.frm_Config_Remote_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Setting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Setting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage rp1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraGrid.GridControl gc_Setting;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_Setting;
        private DevExpress.XtraGrid.Columns.GridColumn gcol_Key;
        private DevExpress.XtraGrid.Columns.GridColumn gcol_Meaning;
        private DevExpress.XtraGrid.Columns.GridColumn gcol_Value;
        private DevExpress.XtraBars.BarButtonItem btn_Save;
        private DevExpress.XtraBars.BarButtonItem btn_Refresh;
        private DevExpress.XtraBars.BarStaticItem lbl_ConnectStatus;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
    }
}