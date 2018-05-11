using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.CodeParser;
using DevExpress.LookAndFeel;
using DevExpress.Office.Utils;
using System.Xml;
using MySql.Data.MySqlClient;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using TOEC_Inspection.Model;
using DevExpress.XtraBars.Navigation;
using Heng;
using System.IO;

namespace TOEC_Inspection
{
    public partial class frm_ViewTemplate : XtraForm
    {
        #region 初始化
        public frm_ViewTemplate()
        {
            InitializeComponent();
        }
        private string _TemplateContent;
        public string TemplateContent
        {
            get
            {
                return _TemplateContent.Replace("$", @"\");
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _TemplateContent = value;
            }
        }
        public string TemplateID { get; set; }
        public string InsertDate { get; set; }
        public string ApplyDate { get; set; }
        public string Status { get; set; }

        private static XmlDocument doc = new XmlDocument();
        private void frm_ViewTemplate_Load(object sender, EventArgs e)
        {
            try
            {
                string[] typeArray = { "Database", "Application", "Sever", "Web" };
                foreach (string item in typeArray)
                {
                    repCOB_type.Items.Add(item);
                }
                string[] encodeArray = { "UTF-8", "GB2312", "ASCII" };
                foreach (string item in encodeArray)
                {
                    repCOB_encode.Items.Add(item);
                }

                lbl_importtime.Caption = "上传时间：" + InsertDate;
                lbl_applytime.Caption = "应用时间：" + ApplyDate;
                lbl_state.Caption = "当前状态：" + Status;
                lbl_TemplateID.Caption = "模板编号：" + TemplateID;

                LoadTab_RelevantInfo();
                LoadTab_EditTemplate();
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }
        #endregion

        #region 选项卡加载：相关信息
        private void LoadTab_RelevantInfo()
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                //忽略注释
                settings.IgnoreComments = true;
                MemoryStream s = new MemoryStream(Encoding.UTF8.GetBytes(TemplateContent));
                XmlReader reader = XmlReader.Create(s, settings);
                doc.Load(reader);
                //doc.LoadXml(TemplateContent);

                XmlNodeList xnls_update = doc.SelectNodes("/root/software[@update='true']");
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("本模板共有" + xnls_update.Count + "升级项");
                sb.AppendLine("【模板ID】" + TemplateID);
                for (int i = 1; i <= xnls_update.Count; i++)
                {
                    if (xnls_update[i - 1].Attributes["comment"] != null)
                    { sb.AppendLine(i + "." + xnls_update[i - 1].Attributes["comment"].Value.ToString()); }
                    else { break; }
                }
                //相关信息
                memoEdit_RelevantInfo.Text = sb.ToString();
                //反馈信息
                GetTemplateResult(TemplateID);
            }
            catch (Exception ex) { throw ex; }
        }

        #region 查询模板升级结果
        /// <summary>
        /// 查询模板升级结果
        /// </summary>
        /// <param name="TemplateID"></param>
        private void GetTemplateResult(string TemplateID)
        {
            using (MySqlConnection mycon = new MySqlConnection(CommonSetting.Default.ConStr_Inspection))
            {
                DataTable dt = new DataTable();
                mycon.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter("SELECT Result FROM t_template WHERE t_template.ID='" + TemplateID + "';", mycon);
                mda.Fill(dt);
                mycon.Close();
                if (dt.Rows.Count > 0)
                {
                    memoEdit_Result.Text = dt.Rows[0]["Result"].ToString().Replace("$", @"\"); ;
                }
                else
                {
                    XtraMessageBox.Show("未查询到车站信息", "友情提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
            }
        }
        #endregion

        #endregion

        #region 选项卡加载：编辑模板
        private static List<Template_Node_config> List_config = null;
        private static BindingSource BS_config = new BindingSource();
        private static List<Template_Node_software> List_software = null;
        private static BindingSource BS_software = new BindingSource();
        private static List<Template_Node_del> List_del = null;
        private static BindingSource BS_del = new BindingSource();

        private void LoadTab_EditTemplate()
        {
            try
            {
                np_Main.SelectedPage = np_Setting_ConfigNodes;
                List_config = new List<Template_Node_config>();
                List_software = new List<Template_Node_software>();
                List_del = new List<Template_Node_del>();

                #region XML预览
                memoEdit_XML.Text = Helper_XML.FormatXml(TemplateContent, "max").Replace("$", @"\");
                #endregion

                #region 模板节点内容加载
                //基础节点加载
                XmlNodeList configlist = doc.GetElementsByTagName("config");
                foreach (XmlNode n in configlist)
                {
                    Template_Node_config cn = new Template_Node_config();
                    cn.id = n.Attributes["id"].Value;
                    cn.value = n.Attributes["value"].Value;
                    cn.name = n.Attributes["name"].Value;
                    if (cn.id == "Template_ID") continue;
                    List_config.Add(cn);
                }
                BS_config.DataSource = List_config;
                gc_configNodes.DataSource = BS_config;

                //软件节点加载
                XmlNodeList softwarelist = doc.GetElementsByTagName("software");
                foreach (XmlNode n in softwarelist)
                {
                    Template_Node_software sn = new Template_Node_software();
                    sn.type = n.Attributes["type"] != null ? n.Attributes["type"].Value : "";
                    sn.comment = n.Attributes["comment"] != null ? n.Attributes["comment"].Value : "";
                    sn.srvname = n.Attributes["srvname"] != null ? n.Attributes["srvname"].Value : "";
                    sn.zipname = n.Attributes["zipname"] != null ? n.Attributes["zipname"].Value : "";
                    sn.exename = n.Attributes["exename"] != null ? n.Attributes["exename"].Value : "";
                    sn.start = n.Attributes["start"] != null ? n.Attributes["start"].Value : "";
                    sn.update = n.Attributes["update"] != null ? Convert.ToBoolean(n.Attributes["update"].Value) : false;
                    if (n.ChildNodes.Count > 0)
                    {
                        sn.List_configXML = new List<Template_SubNode_configXML>();
                        foreach (XmlNode sub in n.ChildNodes)
                        {
                            Template_SubNode_configXML subxn = new Template_SubNode_configXML();
                            subxn.id = sub.Attributes["id"] != null ? sub.Attributes["id"].Value : "";
                            subxn.attr = sub.Attributes["attr"] != null ? sub.Attributes["attr"].Value : "";
                            subxn.encode = sub.Attributes["encode"] != null ? sub.Attributes["encode"].Value : "";
                            subxn.filepath = sub.Attributes["filepath"] != null ? sub.Attributes["filepath"].Value : "";
                            subxn.value = sub.Attributes["value"] != null ? sub.Attributes["value"].Value : "";
                            subxn.comment = sub.Attributes["comment"] != null ? sub.Attributes["comment"].Value : "";
                            subxn.innertext = !string.IsNullOrWhiteSpace(sub.InnerText) ? sub.InnerText : "";
                            if (!string.IsNullOrWhiteSpace(subxn.id) &&
                                !string.IsNullOrWhiteSpace(subxn.filepath) &&
                                (!string.IsNullOrWhiteSpace(subxn.attr) || !string.IsNullOrWhiteSpace(subxn.innertext)))
                            { sn.List_configXML.Add(subxn); }
                            else { continue; }
                        }
                    }
                    List_software.Add(sn);
                }
                //*
                //*层级关系的关键是要就LevelName 与 对象列表名称一致
                //*其他的GridView只是作为模板，并不加载数据

                BS_software.DataSource = List_software;
                gc_softwareNodes.DataSource = BS_software;

                //删除文件节点加载
                XmlNodeList dellist = doc.GetElementsByTagName("del");
                foreach (XmlNode n in dellist)
                {
                    Template_Node_del dn = new Template_Node_del();
                    dn.path = n.Attributes["path"] != null ? n.Attributes["path"].Value : "";
                    List_del.Add(dn);
                }
                BS_del.DataSource = List_del;
                gc_delNodes.DataSource = BS_del;
                #endregion
            }
            catch (Exception ex) { XtraMessageBox.Show("模板缺少关键节点或属性：" + ex.Message, "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        #endregion

        #region 控件事件：模板节点增、删、改、存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ribbonControl1.Focus();
                XmlDocument newdoc = new XmlDocument();
                XmlDeclaration dec = newdoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                newdoc.AppendChild(dec);
                XmlElement root = newdoc.CreateElement("root");
                newdoc.AppendChild(root);
                //单独添加模板ID（模板ID是不允许修改，所以单独添加）
                XmlElement TemplateIDconfig = newdoc.CreateElement("config");
                TemplateIDconfig.SetAttribute("id", "Template_ID");
                TemplateIDconfig.SetAttribute("name", "模板ID");
                TemplateIDconfig.SetAttribute("value", TemplateID);
                root.AppendChild(TemplateIDconfig);
                foreach (Template_Node_config cn in List_config)
                {
                    XmlElement config = newdoc.CreateElement("config");
                    config.SetAttribute("id", cn.id);
                    config.SetAttribute("name", cn.name);
                    config.SetAttribute("value", cn.value);
                    root.AppendChild(config);
                }
                foreach (Template_Node_software sn in List_software)
                {
                    XmlElement software = newdoc.CreateElement("software");
                    software.SetAttribute("type", sn.type);
                    software.SetAttribute("comment", sn.comment);
                    software.SetAttribute("zipname", sn.zipname);
                    software.SetAttribute("exename", sn.exename);
                    software.SetAttribute("srvname", sn.srvname);
                    software.SetAttribute("start", sn.start);
                    software.SetAttribute("update", sn.update.ToString().ToLower());
                    root.AppendChild(software);
                    if (sn.List_configXML != null && sn.List_configXML.Count > 0)
                    {
                        foreach (Template_SubNode_configXML subxn in sn.List_configXML)
                        {
                            XmlElement configXML = newdoc.CreateElement("configXML");
                            configXML.SetAttribute("filepath", subxn.filepath);
                            configXML.SetAttribute("id", subxn.id);
                            configXML.SetAttribute("attr", subxn.attr);
                            configXML.SetAttribute("value", subxn.value);
                            configXML.SetAttribute("encode", subxn.encode);
                            configXML.SetAttribute("comment", subxn.comment);
                            if (!string.IsNullOrWhiteSpace(subxn.innertext))
                                configXML.InnerText = subxn.innertext;
                            software.AppendChild(configXML);
                        }
                    }
                }
                foreach (Template_Node_del dn in List_del)
                {
                    XmlElement del = newdoc.CreateElement("del");
                    del.SetAttribute("path", dn.path);
                    root.AppendChild(del);
                }
                using (MySqlConnection mycon = new MySqlConnection(CommonSetting.Default.ConStr_Inspection))
                {
                    mycon.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = mycon;
                    cmd.CommandText = "UPDATE t_template SET Content='" + newdoc.OuterXml.Replace("\\", "$") + "' WHERE ID='" + TemplateID + "'";
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        XtraMessageBox.Show(" 保存成功！\r\r\n【注：模板中所有“\\”会自动转移为“$”进行存储】", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    cmd.CommandText = "SELECT * FROM t_template WHERE id='" + TemplateID + "';";
                    MySqlDataReader mdr = cmd.ExecuteReader();
                    if (mdr.HasRows)
                    {
                        mdr.Read();
                        memoEdit_XML.Text = Helper_XML.FormatXml(mdr["Content"].ToString().Replace("$", "\\"), "max");
                        TemplateContent = memoEdit_XML.Text;
                        LoadTab_RelevantInfo();
                    }
                }

            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }

        }


        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AddNode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ribbonControl1.Focus();
            NavigationPage curPage = (NavigationPage)np_Main.SelectedPage;
            switch (curPage.Name)
            {
                case "np_Setting_ConfigNodes":
                    XtraMessageBox.Show("禁止添加基础配置节点", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    break;
                case "np_Setting_SoftwareNodes":
                    gv_softwareNodes.ClearSorting();
                    gv_softwareNodes.AddNewRow();
                    break;
                case "np_Setting_DelNodes":
                    gv_delNodes.ClearSorting();
                    gv_delNodes.AddNewRow();
                    break;
                default: break;
            }
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AddSubNode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NavigationPage curPage = (NavigationPage)np_Main.SelectedPage;
            switch (curPage.Name)
            {
                case "np_Setting_SoftwareNodes":
                    try
                    {
                        foreach (Template_Node_software item in List_software)
                        {
                            if (gv_softwareNodes.GetFocusedRowCellValue("zipname") == null)
                            {
                                XtraMessageBox.Show("未填写软件升级包名称[zip]，无法添加子节点", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                            if (item.zipname == gv_softwareNodes.GetFocusedRowCellValue("zipname").ToString())
                            {
                                if (item.List_configXML == null) { item.List_configXML = new List<Template_SubNode_configXML>(); }
                                item.List_configXML.Add(new Template_SubNode_configXML());
                                gv_softwareNodes.RefreshData();
                                break;
                            }
                        }
                    }
                    catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
                    break;
                default: XtraMessageBox.Show("当前配置页面不允许添加子节点", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
            }
            gv_softwareNodes.ExpandMasterRow(gv_softwareNodes.FocusedRowHandle, "List_configXML");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Del_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show("确定删除该行么？", "友情提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    NavigationPage curPage = (NavigationPage)np_Main.SelectedPage;
                    switch (curPage.Name)
                    {
                        case "np_Setting_ConfigNodes":
                            XtraMessageBox.Show("禁止删除基础配置节点", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            break;
                        case "np_Setting_SoftwareNodes":
                            if (gv_softwareNodes.IsFocusedView)
                            {
                                gv_softwareNodes.DeleteRow(gv_softwareNodes.FocusedRowHandle);
                            }
                            else if (gv_softwareNodes.GetVisibleDetailView(gv_softwareNodes.FocusedRowHandle) != null && gv_softwareNodes.GetVisibleDetailView(gv_softwareNodes.FocusedRowHandle).IsFocusedView)
                            {
                                //此处为核心代码
                                GridView curView = (GridView)gv_softwareNodes.GetVisibleDetailView(gv_softwareNodes.FocusedRowHandle);
                                curView.DeleteRow(curView.FocusedRowHandle);
                            }
                            else { XtraMessageBox.Show("请手动点击需要删除的节点", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                            break;
                        case "np_Setting_DelNodes":
                            gv_delNodes.DeleteRow(gv_delNodes.FocusedRowHandle);
                            break;
                        default: break;
                    }
                }
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// 修改行颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Common_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView tmpView = (GridView)sender;
            if (e.RowHandle == tmpView.FocusedRowHandle)
            {
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Heng.CommonColor.Primary;
            }
        }
        /// <summary>
        /// 换行，就折叠其他明细（同时设置：不允许多个明细同时存在）【以便获取用户当前点击的明细】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_softwareNodes_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gv_softwareNodes.GetVisibleDetailView(e.FocusedRowHandle) == null)
            {
                gv_softwareNodes.CollapseAllDetails();
            }
        }
        #endregion
    }
}
