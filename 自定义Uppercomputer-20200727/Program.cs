using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using 自定义Uppercomputer_20200727.PLC选择.MODBUS_TCP监控窗口;

namespace 自定义Uppercomputer_20200727
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Home());
            //Application.Run(new modbus_Form());
        }
    }
}
