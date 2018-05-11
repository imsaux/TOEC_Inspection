using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;
using Heng.TransferFile;
using System.Net;

namespace TOEC_Inspection
{
    public partial class frm_Config_Remote : DevExpress.XtraEditors.XtraForm
    {
        private Socket setting_s = null;
        public int CurrentPort { get; set; }
        public string CurrentStationName { get; set; }
        public string CurrentIP { get; set; }
        public frm_Config_Remote()
        {
            InitializeComponent();
        }
        private void frm_Config_Remote_Load(object sender, EventArgs e)
        {
            this.Text = CurrentStationName + "远程配置";
            ThreadPool.QueueUserWorkItem(new WaitCallback(RemoteSetting), "GetConfig");
        }
        #region 按钮事件
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gv_Setting.Focus();
            ThreadPool.QueueUserWorkItem(new WaitCallback(RemoteSetting), "SetConfig");
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(RemoteSetting), "GetConfig");
        }
        #endregion

        #region Socket 配置相关指令
        /// <summary>
        /// Socket发送指标测试指令
        /// </summary>
        /// <param name="strGetOrSet">字符串</param>
        private void RemoteSetting(object strGetOrSet)//GetConfig 或者 SetConfig
        {
            //初始化建立连接
            try
            {
                setting_s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //缓冲区设置到2M
                setting_s.SendBufferSize = 1024 * 1024 * 2;
                setting_s.ReceiveBufferSize = 1024 * 1024 * 2;
                setting_s.NoDelay = true;
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(CurrentIP), CurrentPort);
                setting_s.Connect(ipep); //配置服务器IP与端口  
                this.Invoke(new ConnectStatus_Delegate(ConnectStatus), "连接成功");
            }
            catch
            {
                this.Invoke(new ConnectStatus_Delegate(ConnectStatus), "连接【" + CurrentStationName + "】失败");
                return;
            }
            try
            {
                TransferFiles.SendVarData(setting_s, Encoding.UTF8.GetBytes(strGetOrSet.ToString()));
                switch (strGetOrSet.ToString())
                {
                    case "GetConfig":
                        try
                        {
                            //接受配置信息
                            byte[] res = TransferFiles.ReceiveVarData(setting_s);
                            string jsonStr = Encoding.UTF8.GetString(res);
                            if (jsonStr.Contains("Error"))
                            {
                                throw new Exception(jsonStr);
                            }
                            Dictionary<string, string> GetConfig = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
                            this.Invoke(new LoadGridView_Delegate(LoadGridView), GetConfig);
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                        break;
                    case "SetConfig":
                        try
                        {
                            Dictionary<string, string> SetConfig = new Dictionary<string, string>();
                            for (int i = 0; i < gv_Setting.DataRowCount; i++)
                            {
                                DataRow tmp = gv_Setting.GetDataRow(i);
                                SetConfig.Add(tmp["Key"].ToString(), tmp["Value"].ToString());
                            }
                            //发送配置信息
                            TransferFiles.SendVarData(setting_s, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(SetConfig)));
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 委托控件调用（含配置中文解释）
        /// <summary>
        /// 线程委托：修改界面GridControl
        /// </summary>
        /// <param name="ID">模板ID</param>
        /// <param name="State">模板状态</param>
        private delegate void LoadGridView_Delegate(Dictionary<string, string> config);
        private void LoadGridView(Dictionary<string, string> config)
        {
            lock (this)
            {
                if (config != null && config.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Key", typeof(String)));
                    dt.Columns.Add(new DataColumn("Value", typeof(String)));
                    dt.Columns.Add(new DataColumn("Meaning", typeof(String)));

                    foreach (var item in config)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Key"] = item.Key;
                        dr["Value"] = item.Value;
                        switch (item.Key)
                        {
                            case "AutoStart":
                                dr["Meaning"] = "开机自启";
                                break;
                            case "ConStr":
                                dr["Meaning"] = "数据连接";
                                break;
                            case "Path_DataImportExport":
                                dr["Meaning"] = "主服务目录";
                                break;
                            case "Path_SucceedXMLFolder":
                                dr["Meaning"] = "成功调用XML的目录";
                                break;
                            case "Path_Pic":
                                dr["Meaning"] = "图像文件目录";
                                break;
                            case "Path_Video":
                                dr["Meaning"] = "视频文件目录";
                                break;
                            case "Path_Voice":
                                dr["Meaning"] = "音频文件目录";
                                break;
                            case "Path_Hotwheel":
                                dr["Meaning"] = "热轮文件目录";
                                break;
                            case "Path_UpdaterB":
                                dr["Meaning"] = "程序B的安装目录";
                                break;
                        }
                        dt.Rows.Add(dr);
                    }
                    gc_Setting.DataSource = dt;
                }
            }
        }
        /// <summary>
        /// 线程委托：修改连接状态显示
        /// </summary>
        /// <param name="ID">模板ID</param>
        /// <param name="State">模板状态</param>
        private delegate void ConnectStatus_Delegate(string text);
        private void ConnectStatus(string text)
        {
            lock (this)
            {
                lbl_ConnectStatus.Caption = text;
            }
        }
        #endregion


        /// <summary>
        /// 窗台关闭：关闭Socket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Config_Remote_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (setting_s.Connected)
            {
                setting_s.Shutdown(SocketShutdown.Both);
            }
            //关闭套接字
            setting_s.Close();
            setting_s = null;
        }
    }
}