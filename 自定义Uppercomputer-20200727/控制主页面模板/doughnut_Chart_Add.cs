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
    class doughnut_Chart_Add
    {
        string Button_serial = "doughnut_Chart_reform";//默认名称
        public doughnut_Chart_reform Add(System.Windows.Forms.Control.ControlCollection control, Point point)//添加按钮方法
        {
            _ = control.Owner.Name;//获取创建的窗口名称
            this.Button_serial += ((from Control pi in control where pi is doughnut_Chart_reform select pi).ToList().Count) + 2;
            doughnut_Chart_reform reform = new doughnut_Chart_reform();//实例化按钮
            reform.Size = new Size(200, 200);//设置大小
            reform.Location = point;//设置按钮位置
            reform.Name = Button_serial;//设置名称
            reform.Text = Button_serial;//设置文本
            reform.BringToFront();//将控件放置所有控件最顶层        
            return reform;//返回数据
        }
    }
}
