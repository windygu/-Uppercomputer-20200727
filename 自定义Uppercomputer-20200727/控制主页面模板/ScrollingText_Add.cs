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
    /// <添加报警条>
    /// </summary>
    class ScrollingText_Add
    {
        Form Form;
        public ScrollingText_Add(Form form)
        {
            this.Form = form;
        }
        string Skinlabel_serial = "ScrollingText_reform";//默认名称
        public ScrollingText_reform Add(System.Windows.Forms.Control.ControlCollection control, Point point)//添加按钮方法
        {
            this.Skinlabel_serial += (from Control pi in control where pi is ScrollingText_reform select pi).ToList().Count + 2;
            ScrollingText_reform reform = new ScrollingText_reform(this.Form);//实例化按钮
            reform.Size = new Size(200, 60);//设置大小
            reform.Location = point;//设置按钮位置
            reform.Name = Skinlabel_serial.Trim();//设置名称
            reform.Text = Skinlabel_serial.Trim();//设置文本
            reform.Interval = 100;//文本刷新时间
            reform.BringToFront();//将控件放置所有控件最顶层        
            return reform;//返回数据
        }
    }
}
