using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using System.Xml;
using DevExpress.XtraTreeList;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;
using Heng.TransferFile;
using Heng;
using DevExpress.XtraBars;
using TOEC_SASTAD_Data.Model;
using TOEC_SASTAD_Data.BLL;
using TOEC_Inspection.Model;
using TOEC_SASTAD_Data;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSplashScreen;
using System.Drawing;
using System.Diagnostics;
using TOEC_Common;

namespace TOEC_Inspection
{
    public partial class frm_Master : XtraForm
    {
        #region  初始化
        private static string CurrentStationName = "";
        private static string CurrentTelCode = "";
        private static string CurrentIP = "";
        //指标测试结果集合
        private BindingSource bs_TestIndex = new BindingSource();
        private List<Display_IndexTest> list_dit = new List<Display_IndexTest>();
        public frm_Master()
        {
            InitializeComponent();
            InitCustomComponent();
        }

        /// <summary>
        /// 初始化自定义控件
        /// </summary>
        private void InitCustomComponent()
        {
            try
            {
                this.Text += " V" + Application.ProductVersion.ToString();
                navigationFrame_Main.SelectedPageIndex = 0;
                splitContainer_Main.SplitterPosition = 150;
                //加载接发车数据查询条件
                repositoryItemDateEdit_st.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
                repositoryItemDateEdit_ed.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
                st_SearchTrain.EditValue = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 08:00:00");
                ed_SearchTrain.EditValue = DateTime.Now.ToString("yyyy-MM-dd 08:00:00");
                repositoryItemComboBox_Line.Items.Add("");//线路
                List<line> ls = new BLL_Line().GetAllLines();
                if (ls != null && ls.Count > 0)
                {
                    foreach (line l in ls)
                    {
                        KeyValuePair<int, string> kv = new KeyValuePair<int, string>(int.Parse(l.LineID), l.LineName);
                        repositoryItemComboBox_Line.Items.Add(kv);
                    }
                }
                repositoryItemComboBox_Problem.Items.Add("");//是否包含问题
                repositoryItemComboBox_Problem.Items.Add("有问题");
                repositoryItemComboBox_Problem.Items.Add("无问题");
                //指标测试控件支持显示时分秒
                st_DateTime.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
                ed_DateTime.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
                st_DateTime.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00");
                ed_DateTime.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                //指标测试结果集绑定
                bs_TestIndex.DataSource = list_dit;
                gc_IndexTest.DataSource = bs_TestIndex;
                //加载左侧车站树
                if (frm_Config_DB.DBConStr_Is_OK(CommonSetting.Default.ConStr_Inspection)) { InitTreeList(); InitPopupProlemPanel(); }
                else { XtraMessageBox.Show("无法连接本地数据库，请先配置！", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
            }
            catch (Exception e) { Log.logsys.Error(e); }
        }
        #endregion

        #region ===============================================  我是功能分割线  ================================================================
        #endregion

        #region TAB: 本地配置
        /// <summary>
        /// 本地数据库配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DBSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm_Config_DB fdbt = new frm_Config_DB();
            fdbt.ShowDialog();
            InitTreeList();
        }
        /// <summary>
        /// 车站管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_EditStation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frm_Config_DB.DBConStr_Is_OK(CommonSetting.Default.ConStr_Inspection))
            {
                frm_Config_Station FORMCS = new frm_Config_Station();
                FORMCS.ShowDialog();
                InitTreeList();
            }
            else
            {
                XtraMessageBox.Show("无法连接本地数据库，请先配置！", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        /// <summary>
        /// 皮肤管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkinBarItem_GalleryItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            CommonSetting.Default.SkinName = e.Item.Caption;
            CommonSetting.Default.Save();
        }
        #endregion

        #region 控件初始化【面板、树、表格、窗体、TAB】

        #region 问题面板
        /// <summary>
        /// 问题面板初始化
        /// </summary>
        private void InitPopupProlemPanel()
        {
            try
            {
                popupContainerControl_Problem.Width = 300;
                BLL_SysCode scbll = new BLL_SysCode();
                List<sys_alg> listAlarmType = scbll.Get_AlarmTypeList();
                popupContainerControl_Problem.SuspendLayout();//挂起逻辑布局
                const int FirstTop = 35;//初始高度
                int offset_top = FirstTop;
                int offset_left = 10;
                for (int i = 0, index = 0; i < listAlarmType.Count; i++, index++)
                {
                    if (index > 9)//一列显示10个
                    {
                        index = 0;
                        offset_left += 160;
                        offset_top = FirstTop;
                        popupContainerControl_Problem.Width += 160;
                    }

                    CheckEdit cb = new CheckEdit();
                    cb.Properties.AutoWidth = true;
                    cb.Enabled = false;
                    cb.Name = "chk_" + listAlarmType[i].Code.ToString();
                    cb.AutoSize = true;
                    cb.Location = new Point(offset_left, offset_top);
                    cb.Size = new Size(95, 16);
                    if (listAlarmType[i].Remark == "unchosen")
                    {
                        cb.Checked = false;
                    }
                    else
                    {
                        cb.Checked = true;
                    }
                    cb.Text = listAlarmType[i].Name;
                    cb.Tag = listAlarmType[i];
                    cb.CheckedChanged += Cb_CheckedChanged;
                    popupContainerControl_Problem.Controls.Add(cb);
                    offset_top += 30;
                    //面板随着内容自动增长
                    if (offset_top >= popupContainerControl_Problem.Height)
                        popupContainerControl_Problem.Height = offset_top + FirstTop;
                }
                offset_top += 70;
                popupContainerControl_Problem.ResumeLayout();//恢复逻辑布局
            }
            catch (Exception ex) { XtraMessageBox.Show("加载问题面板失败，请核对数据库IP：" + ex.Message); }
        }
        private void Cb_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit thisone = (CheckEdit)sender;
            sys_alg thiscode = (sys_alg)thisone.Tag;
            BLL_SysCode scbll = new BLL_SysCode();
            if (thisone.Checked)
            {
                thiscode.Remark = "";
            }
            else
            {
                thiscode.Remark = "unchosen";
            }
            scbll.Set_SysCodeRemark(thiscode);
        }

        private void ChoseRadio_Checked(object sender, EventArgs e)
        {
            if (radio_ChoseAll.Checked)
            {
                foreach (Control c in repositoryItemPopupContainerEdit_Problem.PopupControl.Controls)
                {
                    CheckEdit tmp = (CheckEdit)c;
                    if (tmp.Properties.CheckStyle != CheckStyles.Radio)
                        tmp.Checked = true;
                }
            }
            else if (radio_ChoseNone.Checked)
            {
                foreach (Control c in repositoryItemPopupContainerEdit_Problem.PopupControl.Controls)
                {
                    CheckEdit tmp = (CheckEdit)c;
                    if (tmp.Properties.CheckStyle != CheckStyles.Radio)
                        tmp.Checked = false;
                }
            }
        }

        /// <summary>
        /// 问题Radio判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProblemRadio_Checked(object sender, EventArgs e)
        {
            if (radio_HasProblem.Checked)
            {
                popup_Problem.EditValue = "有问题";
                foreach (Control c in repositoryItemPopupContainerEdit_Problem.PopupControl.Controls)
                {
                    CheckEdit tmp = (CheckEdit)c;
                    if (tmp.Properties.CheckStyle != CheckStyles.Radio)
                        tmp.Enabled = true;
                }
            }
            else if (radio_NoProblem.Checked)
            {
                popup_Problem.EditValue = "无问题";
                foreach (Control c in repositoryItemPopupContainerEdit_Problem.PopupControl.Controls)
                {
                    CheckEdit tmp = (CheckEdit)c;
                    if (tmp.Properties.CheckStyle != CheckStyles.Radio)
                        tmp.Enabled = false;
                }
            }
            else if (radio_All.Checked)
            {
                popup_Problem.EditValue = "";
                foreach (Control c in repositoryItemPopupContainerEdit_Problem.PopupControl.Controls)
                {
                    CheckEdit tmp = (CheckEdit)c;
                    if (tmp.Properties.CheckStyle != CheckStyles.Radio)
                        tmp.Enabled = false;
                }
            }
        }
        #endregion

        #region 车站树
        /// <summary>
        /// 加载车站树
        /// </summary>
        private void InitTreeList()
        {
            using (MySqlConnection mycon = new MySqlConnection(CommonSetting.Default.ConStr_Inspection))
            {
                DataTable dt = new DataTable();
                mycon.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter("SELECT * FROM t_station;", mycon);
                mda.Fill(dt);
                mycon.Close();

                if (dt.Rows.Count > 0)
                {
                    //设置字段 
                    treeList_Station.KeyFieldName = "ID";
                    treeList_Station.ParentFieldName = "PID";
                    treeList_Station.DataSource = dt;
                    treeList_Station.ExpandAll();
                }
                else
                {
                    XtraMessageBox.Show("未查询到车站信息", "友情提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
            }
        }
        /// <summary>
        /// 树形控件添加图标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_Station_CustomDrawNodeImages(object sender, CustomDrawNodeImagesEventArgs e)
        {
            //0 叶子 1 打开文件夹 2 文件夹
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.Expanded)
                {
                    e.SelectImageIndex = 1;
                    return;
                }
                e.SelectImageIndex = 2;
            }
            else
            {
                e.SelectImageIndex = 0;
            }
        }
        /// <summary>
        /// 选择节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_Station_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            CurrentTelCode = e.Node.GetValue("TelCode").ToString();
            if (!string.IsNullOrWhiteSpace(CurrentTelCode))
            {
                CurrentStationName = e.Node.GetValue("StationName").ToString();
                CurrentIP = e.Node.GetValue("IP").ToString();
                Config.DB_IP = CurrentIP;//同时切换了接发车数据的连接IP；
                barStaticItem_StationInfo.Caption = CurrentIP + " " + CurrentTelCode;
                lbl_FileCount.Caption = "文件数状态";
                lbl_PacketCount.Caption = "数据包状态";
            }
            else
            {
                CurrentStationName = "";
                CurrentIP = "";
                barStaticItem_StationInfo.Caption = "";
            }
            InitGridView(CurrentTelCode);
        }
        #endregion

        #region 模板表格
        /// <summary>
        /// 加载模板表格
        /// </summary>
        private void InitGridView(string TelCode)
        {
            try
            {
                gv_Template.ClearSorting();
                using (MySqlConnection mycon = new MySqlConnection(CommonSetting.Default.ConStr_Inspection))
                {
                    DataTable dt = new DataTable();
                    mycon.Open();
                    MySqlDataAdapter mda = new MySqlDataAdapter("SELECT * FROM t_template WHERE t_template.TelCode='" + TelCode + "';", mycon);
                    mda.Fill(dt);
                    mycon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        gc_Template.DataSource = dt;
                    }
                    else
                    {
                        gc_Template.DataSource = null;
                    }
                }
                //默认按照导入时间排序并将第一行设置为焦点
                if (gv_Template.RowCount > 0)
                {
                    gv_Template.SortInfo.Add(gcol_InsertDate, DevExpress.Data.ColumnSortOrder.Descending);
                    gv_Template.FocusedRowHandle = 0;
                }
                lbl_RowCount.Caption = "当前共有" + gv_Template.RowCount + "个模板";

            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// 行颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_Template_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            ///GetRowCellValue 方法应该触发比GetDataRow方法 晚
            if (gv_Template.GetRowCellValue(e.RowHandle, "Status") == null) return;
            switch (gv_Template.GetRowCellValue(e.RowHandle, "Status").ToString())
            {
                case "连接失败":
                    e.Appearance.BackColor = CommonColor.LightDanger;
                    e.Appearance.ForeColor = CommonColor.Dark;
                    break;
                case "传输中...":
                    e.Appearance.BackColor = CommonColor.LightInfo;
                    e.Appearance.ForeColor = CommonColor.Dark;
                    break;
                case "发送失败":
                    e.Appearance.BackColor = CommonColor.LightDanger;
                    e.Appearance.ForeColor = CommonColor.Dark;
                    break;
                case "升级中...":
                    e.Appearance.BackColor = CommonColor.LightInfo;
                    e.Appearance.ForeColor = CommonColor.Dark;
                    break;
                case "升级完毕":
                    e.Appearance.BackColor = CommonColor.LightSuccess;
                    e.Appearance.ForeColor = CommonColor.Dark;
                    break;
                case "升级失败":
                    e.Appearance.BackColor = CommonColor.LightDanger;
                    e.Appearance.ForeColor = CommonColor.Dark;
                    break;
                default: return;

            }
            //if (e.RowHandle == gv_Template.FocusedRowHandle)
            //{
            //    e.Appearance.ForeColor = Color.White;
            //    e.Appearance.BackColor = Heng.CommonColor.Primary;
            //}
        }
        #endregion

        #region 指标测试表格 行颜色
        /// <summary>
        /// 行颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_IndexTest_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gv_IndexTest.GetRowCellValue(e.RowHandle, "TestName") == null) return;
            else
            {
                string FirstField = gv_IndexTest.GetRowCellValue(e.RowHandle, "TestName").ToString();
                if (FirstField.Contains("➤ 查询参数"))
                {
                    e.Appearance.BackColor = CommonColor.LightTea;
                    e.Appearance.ForeColor = CommonColor.Dark;
                }
                else
                if (FirstField.Contains("➤"))
                {
                    e.Appearance.BackColor = CommonColor.LightInfo;
                    e.Appearance.ForeColor = CommonColor.Dark;
                }
            }
        }
        #endregion

        #region 接发车数据 行颜色
        /// <summary>
        /// 列车表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_Train_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var Cell_AlarmLevel = gv_Train.GetRowCellValue(e.RowHandle, "AlarmLevel");
            if (Cell_AlarmLevel == null || Cell_AlarmLevel.ToString() == "") return;
            else
            {
                if (Cell_AlarmLevel.ToString() == AlarmLevel.Level_0.ToNum())
                {
                    gv_Train.SetRowCellValue(e.RowHandle, "AlarmLevel", "");
                }
                else if (Cell_AlarmLevel.ToString() == AlarmLevel.Level_4.ToNum())
                {
                    e.Appearance.BackColor = CommonColor.LightDanger;
                }
                else
                {
                    e.Appearance.BackColor = CommonColor.Danger;
                    e.Appearance.ForeColor = CommonColor.White;
                }
            }
        }
        /// <summary>
        /// 车厢表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_TrainDetail_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var Cell_AlarmLevel = gv_TrainDetail.GetRowCellValue(e.RowHandle, "AlarmLevel");
            if (Cell_AlarmLevel == null || Cell_AlarmLevel.ToString() == "") return;
            else
            {
                if (Cell_AlarmLevel.ToString() == AlarmLevel.Level_0.ToNum())
                {
                    gv_TrainDetail.SetRowCellValue(e.RowHandle, "AlarmLevel", "");
                }
                else if (Cell_AlarmLevel.ToString() == AlarmLevel.Level_4.ToNum())
                {
                    e.Appearance.BackColor = CommonColor.LightDanger;
                }
                else
                {
                    e.Appearance.BackColor = CommonColor.Danger;
                    e.Appearance.ForeColor = CommonColor.White;
                }
            }
        }
        #endregion

        #region 窗体关闭询问
        /// <summary>
        /// 窗体关闭询问
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Master_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = XtraMessageBox.Show("是否关闭程序？",
                "友情提示",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.OK)
            {

                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region 导航切换
        /// <summary>
        /// 导航切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonControl_Main_SelectedPageChanged(object sender, EventArgs e)
        {
            if (ribbonControl_Main.SelectedPage == rp_Template)
            {
                navigationFrame_Main.SelectedPage = np_Template;
            }
            else if (ribbonControl_Main.SelectedPage == rp_Count)
            {
                navigationFrame_Main.SelectedPage = np_Count;
                st_DateTime.Focus();
            }
            else if (ribbonControl_Main.SelectedPage == rp_SASTAD_Data)
            {
                navigationFrame_Main.SelectedPage = np_SASTAD_Data;
                //splitContainer_Main.SplitterPosition = 0;
            }
        }
        #endregion

        #endregion

        #region ===============================================  我是功能分割线  ================================================================
        #endregion

        #region TAB：模板管理
        /// <summary>
        /// 导入模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Upload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CurrentTelCode))
            {
                XtraMessageBox.Show("请选择为哪个车站上传模板！", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            ofd_Upload.CheckFileExists = true;
            ofd_Upload.CheckPathExists = true;
            ofd_Upload.FileName = "TemplateSetting.xml";
            ofd_Upload.Multiselect = false;
            ofd_Upload.Title = "为车站：【" + CurrentStationName + "】上传模板";

            if (ofd_Upload.ShowDialog() == DialogResult.OK)
            {
                if (!ofd_Upload.FileName.EndsWith(".xml")) { XtraMessageBox.Show("您选择的不是XML文件", "", MessageBoxButtons.OK, MessageBoxIcon.Stop); return; }
                string NewID = Guid.NewGuid().ToString();
                try
                {
                    Helper_XML.ModifyAttr_ByID(ofd_Upload.FileName, "Template_ID", "value", NewID, "UTF-8");
                    XmlDocument doc = new XmlDocument();
                    doc.Load(ofd_Upload.FileName);

                    using (MySqlConnection mycon = new MySqlConnection(CommonSetting.Default.ConStr_Inspection))
                    {
                        mycon.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = mycon;
                        cmd.CommandText = "INSERT t_template (ID,TelCode,Content,InsertDate,Status) VALUES('"
                            + NewID + "', '"
                            + CurrentTelCode + "', '"
                            + doc.OuterXml.Replace(@"\", @"$").Replace(@"'", "\"") + "', '"//注意：入库时将反斜杠"\"转移为"$"符号
                            + DateTime.Now + "', '"
                            + UpdaterStatus.Unapplied.ToMeaning() + "');";
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            XtraMessageBox.Show(" 新模板已经添加！\r\r\n【注：模板中所有“\\”会自动转移为“$”进行存储】", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            InitGridView(CurrentTelCode);
                        }
                    }
                }
                catch (Exception ex) { XtraMessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
            }
        }
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Del_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv_Template.GetFocusedDataRow() == null)
            {
                XtraMessageBox.Show("您没有选中任何模板", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((gv_Template.GetFocusedDataRow()["Status"].ToString() == UpdaterStatus.Transfering.ToMeaning() ||
                gv_Template.GetFocusedDataRow()["Status"].ToString() == UpdaterStatus.Updating.ToMeaning())
                && lbl_FileCount.Caption != "文件数状态"
                && lbl_PacketCount.Caption != "数据包状态")
            {
                XtraMessageBox.Show("该模板正在应用中，无法删除", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (XtraMessageBox.Show("是否删除该模板？", "友情提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
               == DialogResult.OK)
            {
                //数据库删除
                using (MySqlConnection mycon = new MySqlConnection(CommonSetting.Default.ConStr_Inspection))
                {
                    mycon.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM t_template where t_template.ID='" + gv_Template.GetFocusedDataRow()["ID"].ToString() + "';", mycon);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        XtraMessageBox.Show("删除成功");
                    }
                }
                //界面删除
                gv_Template.DeleteSelectedRows();
            }
        }
        /// <summary>
        /// 浏览模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_View_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm_ViewTemplate FRM_VT = new frm_ViewTemplate();
            if (gv_Template.GetFocusedDataRow() == null)
            {
                XtraMessageBox.Show("您没有选中任何模板", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FRM_VT.TemplateID = gv_Template.GetFocusedDataRow()["ID"].ToString();
            FRM_VT.TemplateContent = gv_Template.GetFocusedDataRow()["Content"].ToString();
            FRM_VT.InsertDate = gv_Template.GetFocusedDataRow()["InsertDate"].ToString();
            FRM_VT.ApplyDate = gv_Template.GetFocusedDataRow()["ApplyDate"].ToString();
            FRM_VT.Status = gv_Template.GetFocusedDataRow()["Status"].ToString();
            FRM_VT.ShowDialog();
            InitGridView(CurrentTelCode);
        }
        /// <summary>
        /// 应用模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_apply_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv_Template.GetFocusedDataRow() == null)
            {
                XtraMessageBox.Show("您没有选中任何模板", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((gv_Template.GetFocusedDataRow()["Status"].ToString() == UpdaterStatus.Transfering.ToMeaning() ||
                gv_Template.GetFocusedDataRow()["Status"].ToString() == UpdaterStatus.Updating.ToMeaning())
                && lbl_FileCount.Caption != "文件数状态"
                && lbl_PacketCount.Caption != "数据包状态"
                )
            {
                XtraMessageBox.Show("该模板正在应用中，请等待其结束", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (XtraMessageBox.Show("确认根据当前模板升级？", "友情提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
              == DialogResult.OK)
            {
                fbd_Upload.Description = "请选择要发送的升级包文件夹";
                if (fbd_Upload.ShowDialog() == DialogResult.Cancel) { return; }
                if (string.IsNullOrWhiteSpace(fbd_Upload.SelectedPath)) { return; }
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(gv_Template.GetFocusedDataRow()["Content"].ToString().Replace("$", @"\"));
                string TemplateID = Helper_XML.GetAttrValue_FromXML(gv_Template.GetFocusedDataRow()["Content"].ToString(), "//*[@id='Template_ID']", "value");
                if (string.IsNullOrWhiteSpace(TemplateID))
                {
                    XtraMessageBox.Show("所选模板无ID,请重新添加该模板", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else
                {
                    doc.Save(fbd_Upload.SelectedPath + "\\TemplateSetting.xml");
                }
                XmlNodeList xnls = doc.SelectNodes("/root/software[@update='true']");
                if (xnls == null || xnls.Count == 0)
                {
                    XtraMessageBox.Show("该模板不包含任何需要升级的内容", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                //创建要传输的对象
                TemplateAndFiles taf = new TemplateAndFiles();
                taf.TemplateID = TemplateID;
                taf.TelCode = treeList_Station.FocusedNode["TelCode"].ToString();
                taf.FilePaths = new List<string>();
                //获去发送的文件地址
                taf.FilePaths.Add(fbd_Upload.SelectedPath + "\\TemplateSetting.xml");
                foreach (XmlNode n in doc.SelectNodes("/root/software[@update='true']"))
                {
                    if (n.Attributes["zipname"].Value != null)
                    {
                        if (File.Exists(fbd_Upload.SelectedPath + "\\" + n.Attributes["zipname"].Value.ToString()))
                            taf.FilePaths.Add(fbd_Upload.SelectedPath + "\\" + n.Attributes["zipname"].Value.ToString());
                    }
                    else { XtraMessageBox.Show("模板中缺少属性：zipname"); }
                }

                ///此处调用Socket DLL 传输SendPaths
                ThreadPool.QueueUserWorkItem(new WaitCallback(SendTemplate), taf);
            }
        }
        #endregion

        #region Socket 发送模板功能
        /// <summary>
        /// Socket 发送模板功能
        /// </summary>
        /// <param name="TAF"></param>
        private void SendTemplate(object TAF)
        {
            Thread.Sleep(1000);    //等待以防止线程假死
            TemplateAndFiles taf = (TemplateAndFiles)TAF;
            //创建套接字
            Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //缓冲区设置到2M
            ClientSocket.SendBufferSize = 1024 * 1024 * 2;
            ClientSocket.ReceiveBufferSize = 1024 * 1024 * 2;
            //小包不等待
            ClientSocket.NoDelay = true;
            //指向远程服务端节点
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(CurrentIP), 8002);
            try
            {
                ClientSocket.Connect(ipep); //配置服务器IP与端口  
            }
            catch
            {
                this.Invoke(new UpdateView_Delegate(UpdateView), taf.TemplateID, UpdaterStatus.ConnectFail.ToMeaning(), null);
                return;
            }
            try
            {
                this.Invoke(new UpdateView_Delegate(UpdateView), taf.TemplateID, UpdaterStatus.Transfering.ToMeaning(), null);
                //在发送模板ID 和 总文件个数
                ClientSocket.Send(Encoding.ASCII.GetBytes(taf.TemplateID + "#" + taf.FilePaths.Count.ToString().PadLeft(3, '0')), 40, SocketFlags.None);
                //循环发送文件
                for (int i = 0; i < taf.FilePaths.Count; i++)
                {
                    if (File.Exists(taf.FilePaths[i]))
                    {
                        //创建一个文件对象
                        FileInfo fi = new FileInfo(taf.FilePaths[i]);
                        //打开文件流
                        FileStream fs = fi.OpenRead();
                        //包的大小
                        //int PacketSize = 10000;
                        int PacketSize = 1024 * 1024;
                        //包的数量
                        int PacketCount = (int)(fs.Length / ((long)PacketSize));
                        //最后一个包的大小
                        int LastDataPacket = (int)(fs.Length - ((long)(PacketSize * PacketCount)));
                        //获得客户端节点对象
                        IPEndPoint clientep = (IPEndPoint)ClientSocket.RemoteEndPoint;
                        //发送[文件名]到客户端
                        TransferFiles.SendVarData(ClientSocket, Encoding.Unicode.GetBytes(fi.Name));
                        //发送[包的大小]到客户端
                        TransferFiles.SendVarData(ClientSocket, Encoding.Unicode.GetBytes(PacketSize.ToString()));
                        //发送[包的总数量]到客户端
                        TransferFiles.SendVarData(ClientSocket, Encoding.Unicode.GetBytes(PacketCount.ToString()));
                        //发送[最后一个包的大小]到客户端
                        TransferFiles.SendVarData(ClientSocket, Encoding.Unicode.GetBytes(LastDataPacket.ToString()));
                        //数据包
                        byte[] data = new byte[PacketSize];
                        //开始循环发送数据包
                        for (int j = 0; j < PacketCount; j++)
                        {
                            //从文件流读取数据并填充数据包
                            fs.Read(data, 0, data.Length);
                            //发送数据包
                            if (TransferFiles.SendVarData(ClientSocket, data) == -1)
                            {
                                //发送中断
                                this.Invoke(new UpdateView_Delegate(UpdateView), taf.TemplateID, UpdaterStatus.TransferFail.ToMeaning(), null);
                                return;
                            }
                            Thread.Sleep(0);
                            //实时展示传输进度[数据包]
                            this.Invoke(new Updatelbl_Delegate(Updatelbl), taf.TelCode, -1, 0, j, PacketCount);
                        }
                        //如果还有多余的数据包，则应该发送完毕！
                        if (LastDataPacket != 0)
                        {
                            data = new byte[LastDataPacket];
                            fs.Read(data, 0, data.Length);
                            TransferFiles.SendVarData(ClientSocket, data);
                        }
                        //关闭文件流
                        fs.Close();
                        Thread.Sleep(0);
                        //实时展示传输进度[数据包]
                        this.Invoke(new Updatelbl_Delegate(Updatelbl), taf.TelCode, -1, 0, 1, 1);
                        //实时展示传输进度[文件]
                        this.Invoke(new Updatelbl_Delegate(Updatelbl), taf.TelCode, i + 1, taf.FilePaths.Count, -1, 0);
                    }
                    else continue;
                }
                //接受反馈信息
                ClientSocket.ReceiveTimeout = 1000 * 60 * 60;
                byte[] res = TransferFiles.ReceiveVarData(ClientSocket);//接受程序A
                if (Encoding.UTF8.GetString(res).Contains("未找到 TOEC Updater B.exe"))
                {
                    this.Invoke(new UpdateView_Delegate(UpdateView), taf.TemplateID, UpdaterStatus.UpdateFail.ToMeaning(), res);
                }
                else
                {
                    this.Invoke(new UpdateView_Delegate(UpdateView), taf.TemplateID, UpdaterStatus.Updating.ToMeaning(), res);
                    res = TransferFiles.ReceiveVarData(ClientSocket);//接受程序B
                    if (res != null)
                    {
                        this.Invoke(new UpdateView_Delegate(UpdateView), taf.TemplateID, UpdaterStatus.UpdateFinished.ToMeaning(), res);
                    }
                    else
                    {
                        this.Invoke(new UpdateView_Delegate(UpdateView), taf.TemplateID, UpdaterStatus.UpdateFail.ToMeaning(), null);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                this.Invoke(new UpdateView_Delegate(UpdateView), taf.TemplateID, UpdaterStatus.TransferFail.ToMeaning(), null);
            }
            finally
            {
                if (ClientSocket.Connected)
                {
                    ClientSocket.Shutdown(SocketShutdown.Both);
                }
                //关闭套接字
                ClientSocket.Close();
                ClientSocket = null;
                this.Invoke(new Updatelbl_Delegate(Updatelbl), taf.TelCode, 0, 0, 0, 0);
            }
        }
        #endregion

        #region 委托控件调用（模板相关功能）
        /// <summary>
        /// 线程委托：修改界面GridControl
        /// </summary>
        /// <param name="ID">模板ID</param>
        /// <param name="State">模板状态</param>
        private delegate void UpdateView_Delegate(string ID, string State, byte[] res);
        private void UpdateView(string ID, string State, byte[] res)
        {
            string ResContent = "";
            string TemplateID = ID;
            if (res != null)
            {
                ResContent = Encoding.UTF8.GetString(res).Replace(@"\", "$").Replace("'", "`");
                TemplateID = ResContent.Substring(0, 36);
            }

            //改变模板状态
            using (MySqlConnection mycon = new MySqlConnection(CommonSetting.Default.ConStr_Inspection))
            {
                mycon.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE t_template SET");
                sb.Append(" t_template.`Status`='" + State + "', ");
                if (res != null)
                {
                    sb.Append(" t_template.`Result`='" + ResContent + "', ");
                }
                sb.Append(" t_template.`ApplyDate`='" + DateTime.Now + "'");
                sb.Append(" WHERE t_template.ID='" + TemplateID + "';");
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), mycon);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    InitGridView(CurrentTelCode);
                }
            }
        }
        /// <summary>
        /// 线程委托：修改界面lbl_TransferStatus
        /// </summary>
        /// <param name="TelCode">车站电报码</param>
        /// <param name="CurFile">已发送文件数</param>
        /// <param name="TotalFile">总文件数数</param>
        /// <param name="CurData">已发送数据包数</param>
        /// <param name="TotalData">总数据包数</param>
        private delegate void Updatelbl_Delegate(string TelCode, int CurFile, int TotalFile, int CurData, int TotalData);
        /// <summary>
        /// 更新界面进度标签
        /// </summary>
        /// <param name="TelCode">车站电报码</param>
        /// <param name="CurFile">已发送文件数</param>
        /// <param name="TotalFile">总文件数数</param>
        /// <param name="CurData">已发送数据包数</param>
        /// <param name="TotalData">总数据包数</param>
        private void Updatelbl(string TelCode, int CurFile, int TotalFile, int CurData, int TotalData)
        {
            //均为0表示重置
            if (CurFile == 0 && TotalFile == 0 && CurData == 0 && TotalData == 0)
            {
                lbl_FileCount.Caption = "文件数状态";
                lbl_PacketCount.Caption = "数据包状态";
            }
            else if (treeList_Station.FocusedNode["TelCode"].ToString() == TelCode)
            {
                //文件数（已发送文件数为-1表示不修改）
                if (lbl_FileCount.Caption != "已发送文件" + "（" + CurFile + "/" + TotalFile + "）" && CurFile != -1)
                {
                    lbl_FileCount.Caption = "已发送文件" + "（" + CurFile + "/" + TotalFile + "）";
                }
                //数据包（已发送数据包数为-1表示不修改）
                decimal tmpRate = Math.Round(Convert.ToDecimal(CurData) / Convert.ToDecimal(TotalData == 0 ? 1 : TotalData) * 100, 0);
                if (lbl_PacketCount.Caption != "当前数据包 " + tmpRate + "%" && CurData != -1)
                {
                    lbl_PacketCount.Caption = "当前数据包 " + tmpRate + "%";
                }
            }
        }
        #endregion

        #region ===============================================  我是功能分割线  ================================================================
        #endregion

        #region TAB：指标测试
        /// <summary>
        /// 指标测试按钮通用事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Common_IndexTest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CurrentTelCode))
            {
                XtraMessageBox.Show("您未选择哪个车站  o(*￣▽￣*)ブ", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (string.IsNullOrWhiteSpace(st_DateTime.Text) || string.IsNullOrWhiteSpace(ed_DateTime.Text))
            {
                XtraMessageBox.Show("您未填写时间参数  o(*￣▽￣*)ブ", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (DateTime.Parse(st_DateTime.Text) > DateTime.Parse(ed_DateTime.Text))
            {
                XtraMessageBox.Show("您可能没注意，您的查询时间都写反了  o(*￣▽￣*)ブ", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            BarButtonItem bItem = e.Item as BarButtonItem;//dexexpress特殊写法
            ThreadPool.QueueUserWorkItem(new WaitCallback(SendIndexTest), bItem.Name.TrimStart("btn_".ToCharArray()) + "#" + st_DateTime.Text + "#" + ed_DateTime.Text);
        }
        /// <summary>
        /// 清空日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClearMemo_Click(object sender, EventArgs e)
        {
            list_dit.Clear();
            gv_IndexTest.RefreshData();
        }
        /// <summary>
        /// 导出日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                if (list_dit == null || list_dit.Count == 0)
                {
                    XtraMessageBox.Show("当前没有任何结果", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                //Clipboard.Clear();
                //Clipboard.SetText(memoEdit_IndexTest.Text);
                //XtraMessageBox.Show("已经复制到剪贴板中！");
                DialogResult dr = XtraMessageBox.Show("是否导出结果？", "友情提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    if (SaveBrowserDialog.ShowDialog() != DialogResult.Cancel)
                    {
                        string FinnalPath = SaveBrowserDialog.SelectedPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + @"_IndexTest.xlsx";
                        gv_IndexTest.ExportToXlsx(FinnalPath);
                        Process.Start(FinnalPath);
                    }
                }
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }
        #endregion

        #region Socket 发送指标测试
        private object locker = new object();
        /// <summary>
        /// Socket发送指标测试指令
        /// </summary>
        /// <param name="Str">字符串：指令#开始时间#结束时间</param>
        private void SendIndexTest(object Str)
        {
            lock (locker)//保证测试结果不会穿插显示
            {
                //创建套接字
                Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //缓冲区设置到2M
                ClientSocket.SendBufferSize = 1024 * 1024 * 2;
                ClientSocket.ReceiveBufferSize = 1024 * 1024 * 2;
                ClientSocket.NoDelay = true;
                //指向远程服务端节点
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(CurrentIP), 8004);
                try
                {
                    ClientSocket.Connect(ipep); //配置服务器IP与端口  
                }
                catch
                {
                    this.Invoke(new UpdateMemo_Delegate(UpdateMemo), "连接" + CurrentStationName + "失败:" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + "\r\n");
                    return;
                }
                try
                {
                    this.Invoke(new UpdateMemo_Delegate(UpdateMemo), "指令已发送，请耐心等待...");
                    TransferFiles.SendVarData(ClientSocket, Encoding.UTF8.GetBytes(CurrentStationName + "#" + Str.ToString()));
                    //接受反馈信息
                    byte[] res = TransferFiles.ReceiveVarData(ClientSocket);
                    this.Invoke(new UpdateMemo_Delegate(UpdateMemo), Encoding.UTF8.GetString(res));
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
                finally
                {
                    if (ClientSocket.Connected)
                    {
                        ClientSocket.Shutdown(SocketShutdown.Both);
                    }
                    //关闭套接字
                    ClientSocket.Close();
                    ClientSocket = null;
                }
            }
        }
        #endregion

        #region 委托控件调用（指标测试相关功能）
        /// <summary>
        /// 线程委托：修改界面GridControl
        /// </summary>
        /// <param name="ID">模板ID</param>
        /// <param name="State">模板状态</param>
        private delegate void UpdateMemo_Delegate(string text);

        private void UpdateMemo(string text)
        {
            //memoEdit_IndexTest.Text += text + "\r\n";
            string[] res = text.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in res)
            {
                Display_IndexTest dit = new Display_IndexTest();
                string[] r = item.Split("#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                dit.TestName = r[0];
                dit.TestValue = r.Length > 1 ? r[1] : "";
                if ((r[0] == "公式" || r[0] == "数据来源" || r[0] == "*注") && !chk_ShowFormula.Checked)
                { continue; }
                list_dit.Add(dit);
            }
            gv_IndexTest.RefreshData();
        }
        #endregion

        #region ===============================================  我是功能分割线  ================================================================
        #endregion

        #region TAB：接发车数据
        /// <summary>
        /// 列车信息查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SearchTrain_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CurrentIP))
            {
                XtraMessageBox.Show("请选择查询的车站！", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
            }
            try
            {
                //弹出查询提示画面  
                SplashScreenManager.ShowForm(typeof(frm_Wait));
                gv_Train.ClearSorting();
                gc_TrainDetail.DataSource = null;
                //查询参数
                DateTime st = Convert.ToDateTime(st_SearchTrain.EditValue), ed = Convert.ToDateTime(ed_SearchTrain.EditValue);
                string lineid = "";
                KeyValuePair<int, string> kv = new KeyValuePair<int, string>();
                if (!string.IsNullOrWhiteSpace(combo_Line.EditValue == null ? "" : combo_Line.EditValue.ToString()))
                {
                    kv = (KeyValuePair<int, string>)combo_Line.EditValue;
                    lineid = kv.Key.ToString();
                }
                string trainNo = txt_TrainNo.EditValue != null ? txt_TrainNo.EditValue.ToString() : null,
                traindetailNo = txt_TrainDetailNo.EditValue != null ? txt_TrainDetailNo.EditValue.ToString() : null,
                hasProblem = popup_Problem.EditValue != null ? popup_Problem.EditValue.ToString() : null;
                List<string> ProblemTypes = null;
                if (popup_Problem.EditValue != null && popup_Problem.EditValue.ToString() == "有问题")
                {
                    ProblemTypes = new List<string>();
                    foreach (Control c in popupContainerControl_Problem.Controls)
                    {
                        CheckEdit tmp = (CheckEdit)c;
                        if (tmp.Properties.CheckStyle == CheckStyles.Standard && tmp.Checked)
                        {
                            ProblemTypes.Add(tmp.Text);
                        }
                    }
                }
                TimeSpan ts = ed - st;
                if (ts.Days < 0)
                {
                    XtraMessageBox.Show("日期填反了", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                BLL_Train trainbll = new BLL_Train();
                BLL_AlarmDetail adbll = new BLL_AlarmDetail();
                int TrainCount = 0;//结果数量
                DataTable dt = trainbll.GetTrainDataTable(st, ed, lineid, trainNo, traindetailNo, hasProblem, ProblemTypes, out TrainCount);
                gc_Train.DataSource = dt;
                lbl_TrainCount.Caption = "共" + TrainCount + "趟列车";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Table was marked as crashed and should be repaired"))
                {
                    XtraMessageBox.Show("MySQL出现了表崩溃。正在修复中，请稍等...");
                    BLL_Car.RepairTable();
                    XtraMessageBox.Show("修复完毕，请重试（若仍然重新，需要手动）");
                }
                else
                {
                    XtraMessageBox.Show(ex.Message);
                }
            }
            finally
            {
                //关闭登录提示画面
                SplashScreenManager.CloseForm();
            }
        }

        /// <summary>
        /// 【左键】车厢信息查询 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_Train_ShownEditor(object sender, EventArgs e)
        {
            try
            {
                string TrainID = gv_Train.GetFocusedRowCellValue("Train_ID").ToString();//列车ID
                DateTime TrainComeDate = Convert.ToDateTime(gv_Train.GetFocusedRowCellValue("Train_ComeDate"));//过车时间
                gv_TrainDetail.ClearSorting();
                BLL_Car tdbll = new BLL_Car();
                int TrainDetailCount = 0;
                int TrainAlarmCount = 0;
                gc_TrainDetail.DataSource = tdbll.GetCars(TrainID, TrainComeDate, out TrainDetailCount, out TrainAlarmCount);
                lbl_CurTrainInfo.Caption = "当前列车共" + TrainDetailCount + "节，有" + TrainAlarmCount + "个报警";
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// 【右键】打开问题车厢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_Train_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                string TrainID = gv_Train.GetRowCellValue(e.RowHandle, "Train_ID").ToString();//列车ID
                DateTime TrainComeDate = Convert.ToDateTime(gv_Train.GetRowCellValue(e.RowHandle, "Train_ComeDate"));//过车时间
                if (e.Button == MouseButtons.Right)
                {
                    BLL_Car tdbll = new BLL_Car();
                    List<View_TrainDetail> alarmtd = tdbll.GetAllProblemCar(TrainID, TrainComeDate);
                    if (alarmtd != null && alarmtd.Count > 0)
                    {
                        frm_ViewTrainDetail FRM_PICS = new frm_ViewTrainDetail();
                        FRM_PICS.ProblemTrainDetails = alarmtd;
                        FRM_PICS.CurTrainDetail = null;
                        FRM_PICS.ShowDialog();
                    }
                    else { XtraMessageBox.Show("本列车无报警"); }
                }
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }


        /// <summary>
        /// 【右键】查看车厢图片事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_TrainDetail_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    View_TrainDetail cur = new View_TrainDetail();
                    cur.Train_ID = new Guid(gv_Train.GetFocusedRowCellValue("Train_ID").ToString());//列车ID
                    cur.TrainDetail_ID = new Guid(gv_TrainDetail.GetRowCellValue(e.RowHandle, "TrainDetail_ID").ToString());
                    cur.LineID = int.Parse(gv_Train.GetFocusedRowCellValue("Line_ID").ToString());
                    cur.TrainComeDate = Convert.ToDateTime(gv_Train.GetFocusedRowCellValue("Train_ComeDate"));
                    cur.TrainNo = gv_Train.GetFocusedRowCellValue("Train_No").ToString();
                    cur.TrainDetail_No = gv_TrainDetail.GetRowCellValue(e.RowHandle, "TrainDetail_No").ToString();
                    cur.TrainDetail_OrderNo = Convert.ToInt16(gv_TrainDetail.GetRowCellValue(e.RowHandle, "TrainDetail_OrderNo"));
                    cur.vehicletype = gv_TrainDetail.GetRowCellValue(e.RowHandle, "vehicletype").ToString();
                    cur.ProblemType = gv_TrainDetail.GetRowCellValue(e.RowHandle, "ProblemType").ToString();
                    frm_ViewTrainDetail FRM_PICS = new frm_ViewTrainDetail();
                    FRM_PICS.CurTrainDetail = cur;
                    FRM_PICS.ShowDialog();
                }
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }
        #endregion

        #region 列车文件夹
        /// <summary>
        /// 列车文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OpenTrainFolder_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (gv_Train.FocusedRowHandle < 0) { XtraMessageBox.Show("请选择一趟列车"); return; }
                int lid = int.Parse(gv_Train.GetFocusedRowCellValue("Line_ID").ToString());
                string FolderName = new BLL_Line().GetLineFolderName(lid);
                string TrainComeDate = Convert.ToDateTime(gv_Train.GetFocusedRowCellValue("Train_ComeDate")).ToString("yyyyMMddHHmmss");
                string dirpath = @"\\" + Config.DB_IP + @"\GQPics\" + FolderName + @"\" + TrainComeDate;
                if (Directory.Exists(dirpath))
                    Process.Start(dirpath);
                else
                    XtraMessageBox.Show("文件夹不存在");
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }
        #endregion

        #region 拷贝图片
        /// <summary>
        /// 拷贝问题图片[非EF]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CopyPics_ItemClick(object sender, ItemClickEventArgs e)
        {
            frm_CopyProblemImge frm = new frm_CopyProblemImge();
            frm.st = Convert.ToDateTime(st_SearchTrain.EditValue);
            frm.ed = Convert.ToDateTime(ed_SearchTrain.EditValue);
            frm.ShowDialog();
        }

        /// <summary>
        /// 拷贝最后一张[非EF]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Copy4Last_ItemClick(object sender, ItemClickEventArgs e)
        {
            frm_CopyLastImage frm = new frm_CopyLastImage();
            frm.st = Convert.ToDateTime(st_SearchTrain.EditValue);
            frm.ed = Convert.ToDateTime(ed_SearchTrain.EditValue);
            frm.ShowDialog();
        }
        #endregion

        #region 打开SQL脚本工具
        /// <summary>
        /// 打开SQL脚本工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ExcuteSQL_ItemClick(object sender, ItemClickEventArgs e)
        {
            string[] arg = new string[1];
            arg[0] = "ExcuteSQL";
            StartProcess(Application.StartupPath + "/TOEC Unit Assistant.exe", arg);
        }


        public bool StartProcess(string filename, string[] args)
        {
            try
            {
                string s = "";
                foreach (string arg in args)
                {
                    s = s + arg + " ";
                }
                s = s.Trim();
                Process myprocess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(filename, s);
                myprocess.StartInfo = startInfo;
                myprocess.StartInfo.UseShellExecute = false;
                myprocess.Start();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("启动应用程序时出错！原因：" + ex.Message);
            }
            return false;
        }

        #endregion
    }
}