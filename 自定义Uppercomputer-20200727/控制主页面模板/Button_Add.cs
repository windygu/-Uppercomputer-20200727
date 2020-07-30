using CCWin.SkinControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 自定义Uppercomputer_20200727.控件重做;

namespace 自定义Uppercomputer_20200727.控制主页面模板
{
    /// <本类主要产生用户点击添加-移除-拖放-等操作>
    class Button_Add
    {
        string Button_serial= "Button_reform";//默认名称
        public Button_reform Add(System.Windows.Forms.Control.ControlCollection control, Point point)//添加按钮方法
        {
            _=control.Owner.Name;//获取创建的窗口名称
            this.Button_serial += ((from Control pi in control where pi is Button_reform select pi).ToList().Count)+2;            
            Button_reform reform = new Button_reform();//实例化按钮
            reform.Size = new Size(83, 31);//设置大小
            reform.Location = point;//设置按钮位置
            reform.Name = Button_serial;//设置名称
            reform.Text = Button_serial;//设置文本
            reform.BringToFront();//将控件放置所有控件最顶层        
            return reform;//返回数据
        }
    }
}
