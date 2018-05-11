namespace TOEC_Inspection
{
    partial class frm_Config_Station
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Config_Station));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btn_AddStation = new DevExpress.XtraBars.BarButtonItem();
            this.btn_Del = new DevExpress.XtraBars.BarButtonItem();
            this.btn_Save = new DevExpress.XtraBars.BarButtonItem();
            this.btn_AddLine = new DevExpress.XtraBars.BarButtonItem();
            this.rp_Config_Station = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.TreeList_Station = new DevExpress.XtraTreeList.TreeList();
            this.treeListBand1 = new DevExpress.XtraTreeList.Columns.TreeListBand();
            this.tcol_ID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tcol_PID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.col_StationName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.col_TelCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.col_IP = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Custom_ImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.col_PID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TreeList_Station)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Custom_ImageCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.btn_AddStation,
            this.btn_Del,
            this.btn_Save,
            this.btn_AddLine});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 8;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rp_Config_Station});
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.ShowOnMultiplePages;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(858, 98);
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // btn_AddStation
            // 
            this.btn_AddStation.Caption = "添加车站";
            this.btn_AddStation.Id = 1;
            this.btn_AddStation.ImageUri.Uri = "Add";
            this.btn_AddStation.Name = "btn_AddStation";
            this.btn_AddStation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_AddStation_ItemClick);
            // 
            // btn_Del
            // 
            this.btn_Del.Caption = "删除节点";
            this.btn_Del.Id = 2;
            this.btn_Del.ImageUri.Uri = "Cancel";
            this.btn_Del.Name = "btn_Del";
            this.btn_Del.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_Del_ItemClick);
            // 
            // btn_Save
            // 
            this.btn_Save.Caption = "保存配置";
            this.btn_Save.Id = 3;
            this.btn_Save.ImageUri.Uri = "Save";
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_Save_ItemClick);
            // 
            // btn_AddLine
            // 
            this.btn_AddLine.Caption = "添加线路";
            this.btn_AddLine.Glyph = ((System.Drawing.Image)(resources.GetObject("btn_AddLine.Glyph")));
            this.btn_AddLine.Id = 5;
            this.btn_AddLine.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btn_AddLine.LargeGlyph")));
            this.btn_AddLine.Name = "btn_AddLine";
            this.btn_AddLine.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_AddLine_ItemClick);
            // 
            // rp_Config_Station
            // 
            this.rp_Config_Station.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.rp_Config_Station.Name = "rp_Config_Station";
            this.rp_Config_Station.Text = "车站配置";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btn_AddLine);
            this.ribbonPageGroup1.ItemLinks.Add(this.btn_AddStation);
            this.ribbonPageGroup1.ItemLinks.Add(this.btn_Del);
            this.ribbonPageGroup1.ItemLinks.Add(this.btn_Save);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "基础信息配置";
            // 
            // TreeList_Station
            // 
            this.TreeList_Station.Bands.AddRange(new DevExpress.XtraTreeList.Columns.TreeListBand[] {
            this.treeListBand1});
            this.TreeList_Station.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tcol_ID,
            this.tcol_PID,
            this.col_StationName,
            this.col_TelCode,
            this.col_IP});
            this.TreeList_Station.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeList_Station.Location = new System.Drawing.Point(0, 98);
            this.TreeList_Station.Name = "TreeList_Station";
            this.TreeList_Station.OptionsBehavior.PopulateServiceColumns = true;
            this.TreeList_Station.OptionsView.ShowBandsMode = DevExpress.Utils.DefaultBoolean.False;
            this.TreeList_Station.ParentFieldName = "PID";
            this.TreeList_Station.SelectImageList = this.Custom_ImageCollection;
            this.TreeList_Station.Size = new System.Drawing.Size(858, 376);
            this.TreeList_Station.TabIndex = 1;
            // 
            // treeListBand1
            // 
            this.treeListBand1.Caption = "treeListBand1";
            this.treeListBand1.Columns.Add(this.tcol_ID);
            this.treeListBand1.Columns.Add(this.tcol_PID);
            this.treeListBand1.Columns.Add(this.col_StationName);
            this.treeListBand1.Columns.Add(this.col_TelCode);
            this.treeListBand1.Columns.Add(this.col_IP);
            this.treeListBand1.MinWidth = 33;
            this.treeListBand1.Name = "treeListBand1";
            // 
            // tcol_ID
            // 
            this.tcol_ID.Caption = "ID";
            this.tcol_ID.FieldName = "ID";
            this.tcol_ID.MinWidth = 33;
            this.tcol_ID.Name = "tcol_ID";
            this.tcol_ID.Width = 154;
            // 
            // tcol_PID
            // 
            this.tcol_PID.Caption = "PID";
            this.tcol_PID.FieldName = "PID";
            this.tcol_PID.Name = "tcol_PID";
            this.tcol_PID.Width = 62;
            // 
            // col_StationName
            // 
            this.col_StationName.Caption = "站名";
            this.col_StationName.FieldName = "StationName";
            this.col_StationName.MinWidth = 33;
            this.col_StationName.Name = "col_StationName";
            this.col_StationName.Visible = true;
            this.col_StationName.VisibleIndex = 0;
            this.col_StationName.Width = 178;
            // 
            // col_TelCode
            // 
            this.col_TelCode.Caption = "电报码";
            this.col_TelCode.FieldName = "TelCode";
            this.col_TelCode.Name = "col_TelCode";
            this.col_TelCode.Visible = true;
            this.col_TelCode.VisibleIndex = 1;
            this.col_TelCode.Width = 160;
            // 
            // col_IP
            // 
            this.col_IP.Caption = "IP";
            this.col_IP.FieldName = "IP";
            this.col_IP.Name = "col_IP";
            this.col_IP.Visible = true;
            this.col_IP.VisibleIndex = 2;
            this.col_IP.Width = 195;
            // 
            // Custom_ImageCollection
            // 
            this.Custom_ImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("Custom_ImageCollection.ImageStream")));
            this.Custom_ImageCollection.Images.SetKeyName(0, "home.png");
            this.Custom_ImageCollection.Images.SetKeyName(1, "down.png");
            this.Custom_ImageCollection.Images.SetKeyName(2, "right.png");
            this.Custom_ImageCollection.Images.SetKeyName(3, "node.png");
            this.Custom_ImageCollection.Images.SetKeyName(4, "open.png");
            this.Custom_ImageCollection.Images.SetKeyName(5, "close.png");
            this.Custom_ImageCollection.Images.SetKeyName(6, "deny.png");
            this.Custom_ImageCollection.Images.SetKeyName(7, "wrong.png");
            // 
            // col_PID
            // 
            this.col_PID.Caption = "PID";
            this.col_PID.FieldName = "PID";
            this.col_PID.Name = "col_PID";
            this.col_PID.Visible = true;
            this.col_PID.VisibleIndex = 1;
            this.col_PID.Width = 84;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "PID";
            this.treeListColumn1.FieldName = "PID";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 2;
            this.treeListColumn1.Width = 84;
            // 
            // frm_Config_Station
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 474);
            this.Controls.Add(this.TreeList_Station);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "frm_Config_Station";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "车站配置";
            this.Load += new System.EventHandler(this.frm_Config_Station_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TreeList_Station)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Custom_ImageCollection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage rp_Config_Station;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem btn_AddStation;
        private DevExpress.XtraBars.BarButtonItem btn_Del;
        private DevExpress.XtraBars.BarButtonItem btn_Save;
        private DevExpress.XtraTreeList.TreeList TreeList_Station;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcol_ID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_StationName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_TelCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_IP;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_PID;
        private DevExpress.Utils.ImageCollection Custom_ImageCollection;
        private DevExpress.XtraTreeList.Columns.TreeListBand treeListBand1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcol_PID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraBars.BarButtonItem btn_AddLine;
    }
}