using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 自定义Uppercomputer_20200727.控件重做;

namespace 自定义Uppercomputer_20200727.控制主页面模板
{
    /// <summary>
    /// <指示灯类控件添加>
    /// </summary>
    class LedBulb_Add
    {
        string LedBulb_serial = "LedBulb_reform";//默认名称
        public LedBulb_reform Add(System.Windows.Forms.Control.ControlCollection control, Point point)//添加按钮方法
        {
            _ = control.Owner.Name;//获取创建的窗口名称
            this.LedBulb_serial += ((from Control pi in control where pi is LedBulb_reform select pi).ToList().Count) + 2;
            LedBulb_reform reform = new LedBulb_reform();//实例化按钮
            reform.Size = new Size(36, 32);//设置大小
            reform.Location = point;//设置按钮位置
            reform.Name = LedBulb_serial;//设置名称
            reform.Text = LedBulb_serial;//设置文本
            reform.BringToFront();//将控件放置所有控件最顶层        
            return reform;//返回数据
        }
    }
}
