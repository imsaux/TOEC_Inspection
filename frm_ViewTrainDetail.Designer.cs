namespace TOEC_Inspection
{
    partial class frm_ViewTrainDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ViewTrainDetail));
            this.ribbonControl_Main = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barStaticItem_CurInfo = new DevExpress.XtraBars.BarStaticItem();
            this.btn_Pic_L = new DevExpress.XtraBars.BarButtonItem();
            this.btn_Pic_R = new DevExpress.XtraBars.BarButtonItem();
            this.btn_Pic_T = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem_PictureState = new DevExpress.XtraBars.BarStaticItem();
            this.btn_Previous = new DevExpress.XtraBars.BarButtonItem();
            this.btn_Next = new DevExpress.XtraBars.BarButtonItem();
            this.btn_Previous_Problem = new DevExpress.XtraBars.BarButtonItem();
            this.btn_Next_Problem = new DevExpress.XtraBars.BarButtonItem();
            this.btn_ZXPic_L = new DevExpress.XtraBars.BarButtonItem();
            this.btn_ZXPic_R = new DevExpress.XtraBars.BarButtonItem();
            this.lbl_AlarmCount = new DevExpress.XtraBars.BarHeaderItem();
            this.rp_Pic = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.pictureEdit_Main = new DevExpress.XtraEditors.PictureEdit();
            this.navigationFrame_Main = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.np_Pic = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.np_Video = new DevExpress.XtraBars.Navigation.NavigationPage();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl_Main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit_Main.Properties)).BeginInit();
            this.navigationFrame_Main.SuspendLayout();
            this.np_Pic.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonControl_Main
            // 
            this.ribbonControl_Main.ExpandCollapseItem.Id = 0;
            this.ribbonControl_Main.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl_Main.ExpandCollapseItem,
            this.barStaticItem_CurInfo,
            this.btn_Pic_L,
            this.btn_Pic_R,
            this.btn_Pic_T,
            this.barStaticItem_PictureState,
            this.btn_Previous,
            this.btn_Next,
            this.btn_Previous_Problem,
            this.btn_Next_Problem,
            this.btn_ZXPic_L,
            this.btn_ZXPic_R,
            this.lbl_AlarmCount});
            this.ribbonControl_Main.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl_Main.MaxItemId = 10;
            this.ribbonControl_Main.Name = "ribbonControl_Main";
            this.ribbonControl_Main.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rp_Pic});
            this.ribbonControl_Main.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.True;
            this.ribbonControl_Main.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Show;
            this.ribbonControl_Main.ShowToolbarCustomizeItem = false;
            this.ribbonControl_Main.Size = new System.Drawing.Size(1109, 120);
            this.ribbonControl_Main.StatusBar = this.ribbonStatusBar1;
            this.ribbonControl_Main.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl_Main.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // barStaticItem_CurInfo
            // 
            this.barStaticItem_CurInfo.Caption = "车厢信息";
            this.barStaticItem_CurInfo.Glyph = ((System.Drawing.Image)(resources.GetObject("barStaticItem_CurInfo.Glyph")));
            this.barStaticItem_CurInfo.Id = 1;
            this.barStaticItem_CurInfo.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barStaticItem_CurInfo.LargeGlyph")));
            this.barStaticItem_CurInfo.Name = "barStaticItem_CurInfo";
            this.barStaticItem_CurInfo.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // btn_Pic_L
            // 
            this.btn_Pic_L.Caption = "左图";
            this.btn_Pic_L.Glyph = ((System.Drawing.Image)(resources.GetObject("btn_Pic_L.Glyph")));
            this.btn_Pic_L.Id = 2;
            this.btn_Pic_L.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btn_Pic_L.LargeGlyph")));
            this.btn_Pic_L.Name = "btn_Pic_L";
            this.btn_Pic_L.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_Pic_L_ItemClick);
            // 
            // btn_Pic_R
            // 
            this.btn_Pic_R.Caption = "右图";
            this.btn_Pic_R.Glyph = ((System.Drawing.Image)(resources.GetObject("btn_Pic_R.Glyph")));
            this.btn_Pic_R.Id = 3;
            this.btn_Pic_R.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btn_Pic_R.LargeGlyph")));
            this.btn_Pic_R.Name = "btn_Pic_R";
            this.btn_Pic_R.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_Pic_R_ItemClick);
            // 
            // btn_Pic_T
            // 
            this.btn_Pic_T.Caption = "顶图";
            this.btn_Pic_T.Glyph = ((System.Drawing.Image)(resources.GetObject("btn_Pic_T.Glyph")));
            this.btn_Pic_T.Id = 4;
            this.btn_Pic_T.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btn_Pic_T.LargeGlyph")));
            this.btn_Pic_T.Name = "btn_Pic_T";
            // 
            // barStaticItem_PictureState
            // 
            this.barStaticItem_PictureState.Caption = "图像信息";
            this.barStaticItem_PictureState.Glyph = ((System.Drawing.Image)(resources.GetObject("barStaticItem_PictureState.Glyph")));
            this.barStaticItem_PictureState.Id = 7;
            this.barStaticItem_PictureState.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barStaticItem_PictureState.LargeGlyph")));
            this.barStaticItem_PictureState.Name = "barStaticItem_PictureState";
            this.barStaticItem_PictureState.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // btn_Previous
            // 
            this.btn_Previous.Caption = "上一节";
            this.btn_Previous.Glyph = ((System.Drawing.Image)(resources.GetObject("btn_Previous.Glyph")));
            this.btn_Previous.Id = 8;
            this.btn_Previous.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btn_Previous.LargeGlyph")));
            this.btn_Previous.Name = "btn_Previous";
            this.btn_Previous.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_Previous_ItemClick);
            // 
            // btn_Next
            // 
            this.btn_Next.Caption = "下一节";
            this.btn_Next.Glyph = ((System.Drawing.Image)(resources.GetObject("btn_Next.Glyph")));
            this.btn_Next.Id = 9;
            this.btn_Next.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btn_Next.LargeGlyph")));
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_Next_ItemClick);
            // 
            // btn_Previous_Problem
            // 
            this.btn_Previous_Problem.Caption = "上一节问题车";
            this.btn_Previous_Problem.Glyph = ((System.Drawing.Image)(resources.GetObject("btn_Previous_Problem.Glyph")));
            this.btn_Previous_Problem.Id = 1;
            this.btn_Previous_Problem.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btn_Previous_Problem.LargeGlyph")));
            this.btn_Previous_Problem.Name = "btn_Previous_Problem";
            this.btn_Previous_Problem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_Previous_Problem_ItemClick);
            // 
            // btn_Next_Problem
            // 
            this.btn_Next_Problem.Caption = "下一节问题车";
            this.btn_Next_Problem.Glyph = ((System.Drawing.Image)(resources.GetObject("btn_Next_Problem.Glyph")));
            this.btn_Next_Problem.Id = 2;
            this.btn_Next_Problem.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btn_Next_Problem.LargeGlyph")));
            this.btn_Next_Problem.Name = "btn_Next_Problem";
            this.btn_Next_Problem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_Next_Problem_ItemClick);
            // 
            // btn_ZXPic_L
            // 
            this.btn_ZXPic_L.Caption = "走行部左";
            this.btn_ZXPic_L.Glyph = ((System.Drawing.Image)(resources.GetObject("btn_ZXPic_L.Glyph")));
            this.btn_ZXPic_L.Id = 3;
            this.btn_ZXPic_L.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btn_ZXPic_L.LargeGlyph")));
            this.btn_ZXPic_L.Name = "btn_ZXPic_L";
            this.btn_ZXPic_L.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ZXPic_L_ItemClick);
            // 
            // btn_ZXPic_R
            // 
            this.btn_ZXPic_R.Caption = "走行部右";
            this.btn_ZXPic_R.Glyph = ((System.Drawing.Image)(resources.GetObject("btn_ZXPic_R.Glyph")));
            this.btn_ZXPic_R.Id = 4;
            this.btn_ZXPic_R.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btn_ZXPic_R.LargeGlyph")));
            this.btn_ZXPic_R.Name = "btn_ZXPic_R";
            this.btn_ZXPic_R.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ZXPic_R_ItemClick);
            // 
            // lbl_AlarmCount
            // 
            this.lbl_AlarmCount.Caption = "共计报警个数";
            this.lbl_AlarmCount.Id = 8;
            this.lbl_AlarmCount.Name = "lbl_AlarmCount";
            // 
            // rp_Pic
            // 
            this.rp_Pic.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2,
            this.ribbonPageGroup3});
            this.rp_Pic.Name = "rp_Pic";
            this.rp_Pic.Text = "图像信息";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btn_Pic_L);
            this.ribbonPageGroup1.ItemLinks.Add(this.btn_Pic_R);
            this.ribbonPageGroup1.ItemLinks.Add(this.btn_ZXPic_L);
            this.ribbonPageGroup1.ItemLinks.Add(this.btn_ZXPic_R);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "浏览图片";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btn_Previous);
            this.ribbonPageGroup2.ItemLinks.Add(this.btn_Next);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "按顺位操作";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.btn_Previous_Problem);
            this.ribbonPageGroup3.ItemLinks.Add(this.btn_Next_Problem);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "按问题操作";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.barStaticItem_CurInfo);
            this.ribbonStatusBar1.ItemLinks.Add(this.barStaticItem_PictureState);
            this.ribbonStatusBar1.ItemLinks.Add(this.btn_Previous);
            this.ribbonStatusBar1.ItemLinks.Add(this.btn_Next);
            this.ribbonStatusBar1.ItemLinks.Add(this.lbl_AlarmCount);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 484);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl_Main;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1109, 27);
            // 
            // pictureEdit_Main
            // 
            this.pictureEdit_Main.Location = new System.Drawing.Point(332, 73);
            this.pictureEdit_Main.MenuManager = this.ribbonControl_Main;
            this.pictureEdit_Main.Name = "pictureEdit_Main";
            this.pictureEdit_Main.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit_Main.Properties.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureEdit_Main_Properties_MouseDown);
            this.pictureEdit_Main.Properties.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureEdit_Main_Properties_MouseMove);
            this.pictureEdit_Main.Properties.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureEdit_Main_Properties_MouseWheel);
            this.pictureEdit_Main.Size = new System.Drawing.Size(473, 208);
            this.pictureEdit_Main.TabIndex = 2;
            // 
            // navigationFrame_Main
            // 
            this.navigationFrame_Main.Controls.Add(this.np_Pic);
            this.navigationFrame_Main.Controls.Add(this.np_Video);
            this.navigationFrame_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationFrame_Main.Location = new System.Drawing.Point(0, 120);
            this.navigationFrame_Main.Name = "navigationFrame_Main";
            this.navigationFrame_Main.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPage[] {
            this.np_Pic,
            this.np_Video});
            this.navigationFrame_Main.SelectedPage = this.np_Video;
            this.navigationFrame_Main.SelectedPageIndex = 0;
            this.navigationFrame_Main.Size = new System.Drawing.Size(1109, 364);
            this.navigationFrame_Main.TabIndex = 5;
            this.navigationFrame_Main.Text = "navigationFrame1";
            // 
            // np_Pic
            // 
            this.np_Pic.Controls.Add(this.pictureEdit_Main);
            this.np_Pic.Name = "np_Pic";
            this.np_Pic.Size = new System.Drawing.Size(1109, 364);
            // 
            // np_Video
            // 
            this.np_Video.Name = "np_Video";
            this.np_Video.Size = new System.Drawing.Size(1109, 364);
            // 
            // frm_ViewTrainDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 511);
            this.Controls.Add(this.navigationFrame_Main);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl_Main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_ViewTrainDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "车厢信息";
            this.Load += new System.EventHandler(this.frm_Pic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl_Main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit_Main.Properties)).EndInit();
            this.navigationFrame_Main.ResumeLayout(false);
            this.np_Pic.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl_Main;
        private DevExpress.XtraBars.Ribbon.RibbonPage rp_Pic;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem_CurInfo;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.BarButtonItem btn_Pic_L;
        private DevExpress.XtraBars.BarButtonItem btn_Pic_R;
        private DevExpress.XtraBars.BarButtonItem btn_Pic_T;
        private DevExpress.XtraEditors.PictureEdit pictureEdit_Main;
        private DevExpress.XtraBars.BarStaticItem barStaticItem_PictureState;
        private DevExpress.XtraBars.BarButtonItem btn_Previous;
        private DevExpress.XtraBars.BarButtonItem btn_Next;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem btn_Previous_Problem;
        private DevExpress.XtraBars.BarButtonItem btn_Next_Problem;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem btn_ZXPic_L;
        private DevExpress.XtraBars.BarButtonItem btn_ZXPic_R;
        private DevExpress.XtraBars.Navigation.NavigationFrame navigationFrame_Main;
        private DevExpress.XtraBars.Navigation.NavigationPage np_Pic;
        private DevExpress.XtraBars.Navigation.NavigationPage np_Video;
        private DevExpress.XtraBars.BarHeaderItem lbl_AlarmCount;
    }
}