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
    class GroupBox_Add
    {
        string GroupBox_serial = "GroupBox_reform";//默认名称
        public GroupBox_reform Add(System.Windows.Forms.Control.ControlCollection control, Point point)//添加按钮方法
        {
            this.GroupBox_serial += (from Control pi in control where pi is GroupBox_reform select pi).ToList().Count + 2;
            GroupBox_reform reform = new GroupBox_reform();//实例化按钮
            reform.Size = new Size(200, 200);//设置大小
            reform.Location = point;//设置按钮位置
            reform.Name = GroupBox_serial.Trim();//设置名称
            reform.Text = GroupBox_serial.Trim();//设置文本
            reform.SendToBack();//所有控件的最下层   
            return reform;//返回数据
        }
    }
}
