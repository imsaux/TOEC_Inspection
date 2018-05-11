using System;
using System.Data;
using System.Text;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
namespace TOEC_Inspection
{
    public partial class frm_Config_DB : DevExpress.XtraEditors.XtraForm
    {
        public frm_Config_DB()
        {
            InitializeComponent();
            txt_IP_Inspection.Text = CommonSetting.Default.IP_Inspection;
        }

        /// <summary>
        /// 数据库连接测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DBTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_IP_Inspection.Text)) { XtraMessageBox.Show("IP不能为空"); return; }
            StringBuilder Log = new StringBuilder();
            if (DBConStr_Is_OK("server=" + txt_IP_Inspection.Text + ";database=toec_update;user=sartas;pwd=sartas;port=3306;CharSet=gb2312;"))
            {
                CommonSetting.Default.IP_Inspection = txt_IP_Inspection.Text;
                CommonSetting.Default.ConStr_Inspection = "server=" + txt_IP_Inspection.Text + ";database=toec_update;user=sartas;pwd=sartas;port=3306;CharSet=gb2312;";
                CommonSetting.Default.Save();
                Log.Append("巡检数据库连接成功，已保存！\r\n");
            }
            else
            {
                Log.Append("巡检库连接失败！【原因】1.请检查中间件Mysql-Connector-net；2.连接字符串是否正确；\r\n");
            }
            XtraMessageBox.Show(Log.ToString());
        }

        /// <summary>
        /// 数据库连接测试
        /// </summary>
        /// <returns></returns>
        public static bool DBConStr_Is_OK(string DBCon)
        {
            try
            {
                using (MySqlConnection mycon = new MySqlConnection(DBCon))
                {
                    mycon.Open();
                    if (mycon.State == ConnectionState.Open)
                    {
                        mycon.Close(); return true;
                    }
                    else
                    {
                        mycon.Close(); return false;
                    }
                }
            }
            catch { return false; }
        }
    }
}