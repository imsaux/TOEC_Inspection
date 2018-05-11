using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace TOEC_Inspection
{
    public partial class frm_Config_Station : DevExpress.XtraEditors.XtraForm
    {
        public frm_Config_Station()
        {
            InitializeComponent();
        }

        private void frm_Config_Station_Load(object sender, EventArgs e)
        {
            InitTreeList();
        }

        #region 加载车站树
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
                    TreeList_Station.KeyFieldName = "ID";
                    TreeList_Station.ParentFieldName = "PID";
                    TreeList_Station.DataSource = dt;
                    TreeList_Station.ExpandAll();
                }
                else
                {
                    MessageBox.Show("未查询到车站信息", "友情提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
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
        #endregion

        /// <summary>
        /// 添加线路
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AddLine_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode NewNode = TreeList_Station.Nodes.Add(GetMaxID() + 1, "0", "", "", "", "802");
            Add_Node(NewNode);
        }
        /// <summary>
        /// 添加车站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AddStation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int PID = int.Parse(TreeList_Station.FocusedNode["ID"].ToString());
            TreeListNode NewNode = TreeList_Station.FocusedNode.Nodes.Add(GetMaxID() + 1, PID, "", "", "", "802");
            Add_Node(NewNode);
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Del_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("是否删除该节点？", "友情提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
               == DialogResult.OK)
            {
                try
                {
                    if (Del_Node(TreeList_Station.FocusedNode))
                    {
                        TreeList_Station.DeleteSelectedNodes();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败:" + ex.Message);
                }
            }
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                TreeList_Station.Focus();//通过切换焦点，确保当前编辑的内容进入空间节点
                Save_Node(TreeList_Station.Nodes);
                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message);
            }
            finally { InitTreeList(); }
        }

        private int GetMaxID()
        {
            int res = 0;
            using (MySqlConnection mycon = new MySqlConnection(CommonSetting.Default.ConStr_Inspection))
            {
                mycon.Open();
                MySqlDataReader mdr = new MySqlCommand("SELECT MAX(id) FROM t_station;", mycon).ExecuteReader();
                if (mdr.HasRows)
                {
                    string tmp = "";
                    while (mdr.Read())
                    {
                        tmp += mdr.GetString(0);
                    }
                    res = int.Parse(tmp);
                }
                else
                {
                    res = 0;
                }
                mycon.Close();
            }
            return res;
        }


        private bool Exist_Node(TreeListNode n)
        {
            bool flag = false;
            using (MySqlConnection mycon = new MySqlConnection(CommonSetting.Default.ConStr_Inspection))
            {
                mycon.Open();
                MySqlDataReader mdr = new MySqlCommand("SELECT 0 FROM t_station WHERE t_station.ID='" + n["ID"] + "';", mycon).ExecuteReader();
                if (mdr.HasRows)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                mycon.Close();
            }
            return flag;
        }

        private void Save_Node(TreeListNodes MainNodes)
        {
            try
            {
                foreach (TreeListNode n in MainNodes)
                {
                    if (string.IsNullOrWhiteSpace(n["ID"].ToString()))
                    {
                        throw (new Exception("ID 不能为空"));
                    }
                    if (Exist_Node(n))
                    {
                        if (!Edit_Node(n)) { continue; }
                    }
                    else
                    {
                        if (!Add_Node(n)) { continue; };
                    }
                    if (n.Nodes.Count > 0)
                    {
                        //递归调用
                        Save_Node(n.Nodes);
                    }
                }
            }
            catch (Exception ex) { throw (ex); }
        }


        private bool Add_Node(TreeListNode n)
        {
            bool flag = false;
            using (MySqlConnection mycon = new MySqlConnection(CommonSetting.Default.ConStr_Inspection))
            {
                mycon.Open();
                MySqlCommand mcmd = new MySqlCommand("INSERT INTO t_station(`ID`,`PID`,`TelCode`,`StationName`,`IP`) VALUES('"
                    + int.Parse(n["ID"].ToString()) + "', '"
                    + n["PID"] + "', '"
                    + n["TelCode"] + "', '"
                    + n["StationName"] + "', '"
                    + n["IP"] + "'); ", mycon);
                if (mcmd.ExecuteNonQuery() > 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                mycon.Close();
            }
            return flag;
        }

        private bool Edit_Node(TreeListNode n)
        {
            bool flag = false;
            using (MySqlConnection mycon = new MySqlConnection(CommonSetting.Default.ConStr_Inspection))
            {
                mycon.Open();
                MySqlCommand mcmd = new MySqlCommand("UPDATE t_station set PID='" + n["PID"]
                    + "',TelCode='" + n["TelCode"]
                    + "',StationName='" + n["StationName"]
                    + "',IP='" + n["IP"]
                    + "' WHERE ID='" + n["ID"] + "';", mycon);
                if (mcmd.ExecuteNonQuery() > 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                mycon.Close();
            }
            return flag;
        }

        private bool Del_Node(TreeListNode n)
        {
            bool flag = false;
            //无ID，可以直接删除
            if (string.IsNullOrWhiteSpace(n["ID"].ToString()))
            {
                return true;
            }
            else
            {
                using (MySqlConnection mycon = new MySqlConnection(CommonSetting.Default.ConStr_Inspection))
                {
                    mycon.Open();
                    MySqlCommand mcmd = new MySqlCommand("DELETE FROM t_station WHERE t_station.ID='" + n["ID"] + "';", mycon);
                    if (mcmd.ExecuteNonQuery() > 0)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    mycon.Close();
                }
                return flag;
            }
        }
    }
}