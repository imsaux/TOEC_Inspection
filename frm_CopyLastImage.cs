using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TOEC_SASTAD_Data.BLL;
using System.IO;
using TOEC_Common;

namespace TOEC_Inspection
{
    public partial class frm_CopyLastImage : XtraForm
    {
        public DateTime st { get; set; }
        public DateTime ed { get; set; }

        public frm_CopyLastImage()
        {
            InitializeComponent();
            combo_PicType.SelectedIndex = 0;
            combo_Side.SelectedIndex = 0;
        }

        /// <summary>
        /// 拷图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btn_Copy_Click(object sender, EventArgs e)
        {
            try
            {
                string PicsType = combo_PicType.EditValue.ToString();
                string side = PicsType.Contains("ZX") ? "Z" + combo_Side.EditValue.ToString() : combo_Side.EditValue.ToString();
                string[] TrainType = combo_TrainType.EditValue.ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() != DialogResult.OK) { return; }

                BLL_Train tbll = new BLL_Train();
                DataTable dt = tbll.Get_TrainDataTable_ByVehicleType(st, ed, TrainType);
                int count_backwards = int.Parse(txt_LastCount.Text);//拷贝倒数N张
                if (dt != null && dt.Rows.Count > 0)
                {
                    Log.logsys.Info("【拷贝倒数" + count_backwards + "张图片】数据库查询列车数量：" + dt.Rows.Count);
                    Log.logsys.Info("【拷贝倒数" + count_backwards + "张图片】理论拷贝图片数量：" + dt.Rows.Count * count_backwards);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            string line = dt.Rows[i]["LineName"].ToString();
                            string passingtime = dt.Rows[i]["Train_ComeDate"].ToString();
                            string path = "E:\\" + PicsType + "\\" + line + "\\" + passingtime;
                            //原图文件夹
                            DirectoryInfo dir_train = new DirectoryInfo(path);
                            if (!Directory.Exists(dir_train.FullName)) { continue; }
                            int MaxNum;//最大顺位号
                            string vehicletype = Get_vehicletype(dir_train, out MaxNum);
                            if (string.IsNullOrWhiteSpace(vehicletype)) { continue; }

                            FileInfo[] pics = dir_train.GetFiles("*.jpg");
                            if (MaxNum >= count_backwards)
                            {
                                for (int orderNum = MaxNum; orderNum > MaxNum - count_backwards; orderNum--)
                                {
                                    //原图路径
                                    FileInfo pic_original = new FileInfo(path + "\\" + side + orderNum.ToString().PadLeft(3, '0') + "_" + orderNum + ".jpg");
                                    if (!File.Exists(pic_original.FullName))
                                    {
                                        Log.logsys.Error("图片不存在：" + pic_original.FullName);
                                        continue;
                                    }
                                    //新文件夹
                                    DirectoryInfo dir_new = new DirectoryInfo(fbd.SelectedPath + "\\" + dt.Rows[i]["LineName"].ToString() + "\\");
                                    if (!Directory.Exists(dir_new.FullName)) { Directory.CreateDirectory(dir_new.FullName); }
                                    //新图路径
                                    FileInfo pic_new = new FileInfo(dir_new.FullName + "\\" + vehicletype + "_" + line + "_" + passingtime + "_" + side + orderNum.ToString().PadLeft(3, '0') + "_" + orderNum + ".jpg");
                                    File.Copy(pic_original.FullName, pic_new.FullName, true);
                                }
                            }
                            else
                            {
                                Log.logsys.Error(path + "车辆数过少");
                            }

                        }
                        catch (Exception ex) { Log.logsys.Error("拷图异常", ex); continue; }
                    }
                    XtraMessageBox.Show("拷贝完成");
                }
                else { XtraMessageBox.Show("查询无结果"); return; }
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// 读取Index最后一行的车次和顺位号
        /// </summary>
        /// <param name="dir_train"></param>
        /// <param name="MaxNum">最大顺位号</param>
        /// <returns></returns>
        public string Get_vehicletype(DirectoryInfo dir_train, out int MaxNum)
        {
            string res = null;
            MaxNum = 0;
            FileInfo index = new FileInfo(dir_train.FullName + "\\index.txt");
            if (!File.Exists(index.FullName)) { return res; }

            string line = File.ReadAllLines(index.FullName).Last();
            string[] part = line.Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (part.Length > 3)
            {
                MaxNum = Convert.ToInt16(part[0].Trim());
                res = part[3].Substring(0, 7).Trim();
            }
            return res;
        }
    }
}
