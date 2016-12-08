using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    static class Program
    {
        private static Mutex m_MutexObject;
        private static string m_MutexName = "FenJian";

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //OperatingSystem os = Environment.OSVersion;
                //if (os.Version.Major == 6)
                //{
                //    if (!IsAdministrator())
                //    {
                //        MessageBox.Show("你是WIN7或以上操作系统，请右键使用管理员权限来运行，或选中Exe右键=>属性=>间容=>钩选以管理员权限运行此程序！");
                //        return;
                //    }
                //}
                try
                {
                    m_MutexObject = new Mutex(false, m_MutexName);
                }
                catch (ApplicationException)
                {

                }
                if (!m_MutexObject.WaitOne(0, false))
                {
                    MessageBox.Show("Cs客户端已存在启动的实例，不能重复启动！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_MutexObject.Close();
                    return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new MDIParent());
                //// 测试注释掉
                Application.Run(new MDIParent());//new MDIParent(this).Show();
                return;
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog("主进程", ex.ToString());
                MessageBox.Show(ex.ToString());
                Process.GetCurrentProcess().Kill();
            }
        }

        //判断是否有管理员权限
        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
