using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using TOEC_Common;

namespace TOEC_Inspection
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                if (System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
                {
                    MessageBox.Show("TOEC Inspection 已经打开了。", "消息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Application.Exit();
                    ///test
                }
                else
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    DevExpress.Skins.SkinManager.EnableFormSkins();
                    DevExpress.UserSkins.BonusSkins.Register();
                    UserLookAndFeel.Default.SetSkinStyle(CommonSetting.Default.SkinName);
                    Application.Run(new frm_Master());
                }
            }
            catch (Exception ex)
            {
                Log.logsys.Error("程序异常", ex);
                MessageBox.Show(ex.Message);
            }
        }
    }
}