using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TOEC_Common;
using TOEC_SASTAD_Data;
using TOEC_SASTAD_Data.BLL;
using TOEC_SASTAD_Data.Model;

namespace TOEC_Inspection
{
    public partial class frm_CopyProblemImge : XtraForm
    {
        public DateTime st { get; set; }
        public DateTime ed { get; set; }

        private string SavePath { get; set; }

        public frm_CopyProblemImge()
        {
            InitializeComponent();
        }

        private void frm_CopyImge_Load(object sender, EventArgs e)
        {
            btn_Save.Enabled = false;
            this.Width = 200;
            BLL_SysCode scbll = new BLL_SysCode();
            List<sys_code> listAlarmType = scbll.Get_AlarmTypeList();
            this.SuspendLayout();//挂起逻辑布局
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
                    this.Width += 160;
                }

                CheckEdit cb = new CheckEdit();
                cb.Properties.AutoWidth = true;
                cb.Name = "chk_" + listAlarmType[i].Code_ID.ToString();
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
                cb.Text = listAlarmType[i].Meaning;
                cb.Tag = listAlarmType[i];
                cb.CheckedChanged += Cb_CheckedChanged;
                this.Controls.Add(cb);
                offset_top += 30;
                //面板随着内容自动增长
                if (offset_top > this.Height)
                    this.Height = offset_top + FirstTop;
            }
            this.ResumeLayout();//恢复逻辑布局
        }

        private void Cb_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit thisone = (CheckEdit)sender;
            sys_code thiscode = (sys_code)thisone.Tag;
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

        private void btn_ChosePath_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "选择导出目录\r\n【注】根据问题类型自动分类保存";
                fbd.ShowNewFolderButton = true;
                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    SavePath = fbd.SelectedPath;
                    if (!Directory.Exists(SavePath)) { Directory.CreateDirectory(SavePath); }
                    btn_Save.Enabled = true;
                }
                else { btn_Save.Enabled = false; }
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            btn_Save.Enabled = false;
            Thread t = new Thread(CopyImage);
            t.Start();
            this.Close();
        }

        private void CopyImage()
        {
            try
            {
                List<string> alarmlist = new List<string>();
                CheckEdit tmpctl = null;
                foreach (Control ctl in this.Controls)
                {
                    if (ctl is CheckEdit)
                    {
                        tmpctl = (CheckEdit)ctl;
                        if (tmpctl.Checked)
                        {
                            alarmlist.Add(tmpctl.Text);
                        }
                    }
                }
                BLL_AlarmDetail adbll = new BLL_AlarmDetail();
                DataTable tmp = adbll.GetAlarmPicPath(st, ed, alarmlist);
                int CopyCount = 0;
                if (tmp != null && tmp.Rows.Count > 0)
                {
                    for (int i = 0; i < tmp.Rows.Count; i++)
                    {
                        string Root = "";
                        string ServerIP = Config.DB_IP;
                        switch (tmp.Rows[i][3].ToString())
                        {
                            case "pic":
                                Root = @"\\" + ServerIP + @"\GQPics\";
                                break;
                            case "zpic":
                                Root = @"\\" + ServerIP + @"\ZXGQPics\";
                                break;
                            default: continue;
                        }
                        //原图不存在直接跳过
                        if (!File.Exists(Root + tmp.Rows[i][0].ToString()))
                        {
                            Log.logsys.Info("原图不存在：" + Root + tmp.Rows[i][0].ToString());
                            continue;
                        }
                        else
                        {
                            //分类路径
                            if (!Directory.Exists(SavePath + "\\" + tmp.Rows[i][2].ToString())) { Directory.CreateDirectory(SavePath + "\\" + tmp.Rows[i][2].ToString()); }
                            string AimPath = SavePath + "\\" + tmp.Rows[i][2].ToString() + "\\" + tmp.Rows[i][1].ToString().Replace("*", "#");
                            if (!File.Exists(AimPath))
                            {
                                CopyCount += 1;
                                File.Copy(Root + tmp.Rows[i][0].ToString(), AimPath, true);
                            }
                        }
                    }
                    XtraMessageBox.Show("共拷贝 " + CopyCount + " 张报警图片", "友情提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("此时间段无结果", "窃喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }

        private void toggle_Chose_Toggled(object sender, EventArgs e)
        {
            CheckEdit tmpctl = null;
            if (toggle_Chose.IsOn)
            {
                foreach (Control ctl in this.Controls)
                {
                    if (ctl is CheckEdit)
                    {
                        tmpctl = (CheckEdit)ctl;
                        tmpctl.Checked = true;
                    }
                }
            }
            else
            {
                foreach (Control ctl in this.Controls)
                {
                    if (ctl is CheckEdit)
                    {
                        tmpctl = (CheckEdit)ctl;
                        tmpctl.Checked = false;
                    }
                }
            }
        }
    }
}
