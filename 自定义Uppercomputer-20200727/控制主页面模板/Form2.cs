using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin.SkinControl;
using HslCommunicationDemo;
using 自定义Uppercomputer_20200727.PLC选择;
using 自定义Uppercomputer_20200727.修改参数界面;
using 自定义Uppercomputer_20200727.修改参数界面.doughnut_Chart图形控件参数;
using 自定义Uppercomputer_20200727.修改参数界面.GroupBox四边形方框;
using 自定义Uppercomputer_20200727.修改参数界面.ImageButton按钮参数;
using 自定义Uppercomputer_20200727.修改参数界面.LedBulb_指示灯参数;
using 自定义Uppercomputer_20200727.修改参数界面.报警条参数;
using 自定义Uppercomputer_20200727.参数设置画面;
using 自定义Uppercomputer_20200727.异常界面;
using 自定义Uppercomputer_20200727.手动控制页面;
using 自定义Uppercomputer_20200727.控件重做;
using 自定义Uppercomputer_20200727.控制主页面;
using 自定义Uppercomputer_20200727.控制主页面模板;
using 自定义Uppercomputer_20200727.生产设置画面;
using 自定义Uppercomputer_20200727.监视画面;
using 自定义Uppercomputer_20200727.运转控制画面;

namespace 自定义Uppercomputer_20200727
{
    public partial class Form2 : CCWin.Skin_Mac
    {
        /// <该页面是模板通用页面->
        Time_reform time_Reform;//读取PLC与修改控件--定时器
        public Form2()
        {
            InitializeComponent();
            time_Reform = new Time_reform(this);//实例化读取定时器--开启读取PLC
        }

        private void skinButton1_Click(object sender, EventArgs e)//公用页面处理
        {
            SkinButton skinButton = (SkinButton)sender;
            _ = new Windowclass(new Form3(), new SkinButton[] { this.skinButton1, this.skinButton2, this.skinButton3,
                this.skinButton4, this.skinButton5, this.skinButton6,this.skinButton7}, new Form[] {new Form3(), new Form4(),new Form5()
                , new Form6(),new Form7(), new 生产设置画面.Form8(), new 参数设置画面.Form9()}, this.skinLabel1, skinButton);
        }

        private void skinContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        int X = 0, Y = 0;//窗口与鼠标的相对位置

        private void buttton按钮ToolStripMenuItem_Click(object sender, EventArgs e)//添加按钮
        {
            Button_Add button = new Button_Add();
            Button_reform skinButton = button.Add(this.Controls, new Point(X, Y));
            this.Controls.Add(skinButton);//添加控件
            Modification_Button modification_Button = new Modification_Button(skinButton.Parent.ToString(), skinButton);
            modification_Button.ShowDialog();
            if (modification_Button.Add_to_allow == false) this.Controls.Remove(skinButton);//不允许添加异常对象
        }

        private void label文本ToolStripMenuItem_Click(object sender, EventArgs e)//添加系统文本
        {
            Skinlabel_Add skinlabel = new Skinlabel_Add();
            SkinLabel skinLabel = skinlabel.Add(this.Controls, new Point(X, Y));
            this.Controls.Add(skinLabel);
            Modification_label modification_Label = new Modification_label(skinLabel.Parent.ToString(), skinLabel);
            modification_Label.ShowDialog();
            if (modification_Label.Add_to_allow == false) this.Controls.Remove(skinLabel);//不允许添加异常对象
        }

        private void texebox数值ToolStripMenuItem_Click(object sender, EventArgs e)//添加系统输入文本
        {
            SkinTextBox_Add skinTextBox = new SkinTextBox_Add();
            TextBox skinTextBox1 = skinTextBox.Add(this.Controls, new Point(X, Y));
            this.Controls.Add(skinTextBox1);
            Modification_numerical modification_Numerical = new Modification_numerical(skinTextBox1.Parent.ToString(), skinTextBox1);
            modification_Numerical.ShowDialog();
            skinTextBox1.Text = "00";
            if (modification_Numerical.Add_to_allow == false) this.Controls.Remove(skinTextBox1);//不允许添加异常对象
        }

        private void lmage图片ToolStripMenuItem_Click(object sender, EventArgs e)//添加系统图片
        {
            SkinPictureBox_Add skinPicture = new SkinPictureBox_Add();
            SkinPictureBox skinPictureBox = skinPicture.Add(this.Controls, new Point(X, Y), this.imageList1.Images[0]);
            this.Controls.Add(skinPictureBox);
            Modification_picture modification_Picture = new Modification_picture(skinPictureBox.Parent.ToString(), skinPictureBox);
            modification_Picture.ShowDialog();
            skinPictureBox.Image = modification_Picture.Image;
            if (modification_Picture.Add_to_allow == false) this.Controls.Remove(skinPictureBox);//不允许添加异常对象
        }
        private void toolStripMenuItem7_Click(object sender, EventArgs e)//添加切换开关
        {
            Switch_Add button = new Switch_Add();
            Switch_reform skinButton = button.Add(this.Controls, new Point(X, Y));
            this.Controls.Add(skinButton);//添加控件
            Modification_Switch modification_Button = new Modification_Switch(skinButton.Parent.ToString(), skinButton);
            modification_Button.ShowDialog();
            if (modification_Button.Add_to_allow == false) this.Controls.Remove(skinButton);//不允许添加异常对象
        }
        private void toolStripMenuItem13_Click(object sender, EventArgs e)//添加指示灯
        {
            LedBulb_Add button = new LedBulb_Add();
            LedBulb_reform skinButton = button.Add(this.Controls, new Point(X, Y));
            this.Controls.Add(skinButton);//添加控件
            Modification_Ledbulb modification_Button = new Modification_Ledbulb(skinButton.Parent.ToString(), skinButton);
            modification_Button.ShowDialog();
            if (modification_Button.Add_to_allow == false) this.Controls.Remove(skinButton);//不允许添加异常对象
        }
        private void toolStripMenuItem9_Click(object sender, EventArgs e)//添加四边形方框
        {
            GroupBox_Add GroupBox = new GroupBox_Add();
            GroupBox_reform GroupBoxl = GroupBox.Add(this.Controls, new Point(X, Y));
            this.Controls.Add(GroupBoxl);
            Modification_GroupBox modification_GroupBox = new Modification_GroupBox(GroupBoxl.Parent.ToString(), GroupBoxl);
            modification_GroupBox.ShowDialog();
            if (modification_GroupBox.Add_to_allow == false) this.Controls.Remove(GroupBoxl);//不允许添加异常对象
        }
        private void toolStripMenuItem12_Click(object sender, EventArgs e)//添加无图片按钮类三
        {
            ImageButton_Add button = new ImageButton_Add();
            ImageButton_reform skinButton = button.Add(this.Controls, new Point(X, Y));
            this.Controls.Add(skinButton);//添加控件
            Modification_ImageButton modification_ImageButton = new Modification_ImageButton(skinButton.Parent.ToString(), skinButton);
            modification_ImageButton.ShowDialog();
            if (modification_ImageButton.Add_to_allow == false) this.Controls.Remove(skinButton);//不允许添加异常对象
        }
        private void toolStripMenuItem8_Click(object sender, EventArgs e)//添加报警条
        {
            ScrollingText_Add button = new ScrollingText_Add(this);
            ScrollingText_reform skinButton = button.Add(this.Controls, new Point(X, Y));
            this.Controls.Add(skinButton);//添加控件
            Modification_ScrollingText modification_ImageButton = new Modification_ScrollingText(skinButton.Parent.ToString(), skinButton);
            modification_ImageButton.ShowDialog();
            if (modification_ImageButton.Add_to_allow == false) this.Controls.Remove(skinButton);//不允许添加异常对象
        }
        private void toolStripMenuItem11_Click(object sender, EventArgs e)//添加圆形图
        {
            doughnut_Chart_Add button = new doughnut_Chart_Add();
            doughnut_Chart_reform skinButton = button.Add(this.Controls, new Point(X, Y));
            this.Controls.Add(skinButton);//添加控件
            Modification_doughnut_Chart modification_ImageButton = new Modification_doughnut_Chart(skinButton.Parent.ToString(), skinButton);
            modification_ImageButton.ShowDialog();
            if (modification_ImageButton.Add_to_allow == false) this.Controls.Remove(skinButton);//不允许添加异常对象
        }
        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            X=e.X;Y = e.Y;//获取屏幕位置
        }
        #region 窗体关闭效果
        /// <param name="hwnd">指定产生动画的窗口的句柄</param>
        /// <param name="dwTime">指定动画持续的时间</param>
        /// <param name="dwFlags">指定动画类型，可以是一个或多个标志的组合。</param>
        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        private const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志该标志
        private const int AW_CENTER = 0x0010;//若使用了AW_HIDE标志，则使窗口向内重叠；否则向外扩展
        private const int AW_HIDE = 0x10000;//隐藏窗口
        private const int AW_ACTIVE = 0x20000;//激活窗口，在使用了AW_HIDE标志后不要使用这个标志
        private const int AW_SLIDE = 0x40000;//使用滑动类型动画效果，默认为滚动动画类型，当使用AW_CENTER标志时，这个标志就被忽略
        private const int AW_BLEND = 0x80000;//使用淡入淡出效果
        #endregion
        private void Form2_Load(object sender, EventArgs e)//加载窗口
        {
            AnimateWindow(this.Handle, 200, AW_BLEND | AW_ACTIVE | AW_VER_NEGATIVE);
        }

        private void Form2_Shown(object sender, EventArgs e)//添加控件
        {
            this.timer2.Start();//运行定时器监控
            if (edit_mode) this.toolStripMenuItem5.Text = "退出编辑模式"; else this.toolStripMenuItem5.Text = "开启编辑模式";//改变显示文本
            From_Load_Add load_Add = new From_Load_Add(this.Name, this.Controls, new List<ImageList>() { this.imageList1, this.imageList2, this.imageList3 }, this);//添加报警条
            From_Load_Add add = new From_Load_Add(this.Name, this.Controls, new List<ImageList>() { this.imageList1, this.imageList2, this.imageList3 });//添加普通文本
            time_Reform.Form = this.Name;//获取当前窗口名称
            time_Reform.Interval = 200;//遍历控件时间
            time_Reform.Start();//运行定时器
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)//调用测试工具
        {
            FormLoad formLoad = new FormLoad();
            if (MessageBox.Show("该测试软件调用来源于：Git", "错误：ERR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                formLoad.ShowDialog();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)//开始链接设备--PLC
        {
            PLCselect_Form pLCselect_Form = new PLCselect_Form();
            pLCselect_Form.ShowDialog();
        }
        public static bool edit_mode = false;//指示用户是否进入编辑模式
        private void toolStripMenuItem5_Click(object sender, EventArgs e)//用户进入编辑模式
        {
            if (edit_mode) edit_mode = false; else edit_mode = true;//改变状态
            if (edit_mode) this.toolStripMenuItem5.Text = "退出编辑模式"; else this.toolStripMenuItem5.Text = "开启编辑模式";//改变显示文本
            this.toolStripMenuItem1.Enabled = edit_mode;
            this.toolStripMenuItem6.Enabled= edit_mode;
        }

        private void timer2_Tick(object sender, EventArgs e)//实时刷新用户是否进入 与退出编辑模式
        {
            if (edit_mode) this.toolStripMenuItem5.Text = "退出编辑模式"; else this.toolStripMenuItem5.Text = "开启编辑模式";//改变显示文本
            this.toolStripMenuItem1.Enabled = edit_mode;
            this.toolStripMenuItem6.Enabled = edit_mode;
            if (PLC_read_Tick& edit_mode!=true) time_Reform.read_status =false; else time_Reform.read_status = true; //指示定时器可以开始遍历
            if (edit_mode) { PLC_read_ok = false;PLC_read_Tick = false; };//指示用户开始了编辑模式
        }
        bool PLC_read_Tick=false;//指示是否遍历窗口完成
        bool PLC_read_ok = false;//指示是否遍历控件是否完成--
        private void PLC_circulation_read_Tick(object sender, EventArgs e)//PLC循环读取定时器
        {
            if (time_Reform.TextBox_read_status != false || time_Reform.Button_read_status != false|| time_Reform.Switch_read_status != false
                || time_Reform.LedBulb_read_status != false || edit_mode) return;//直接返回方法--指示当前控件正在遍历
            if (PLC_read_ok != true)
            {
                PLC_read_Tick = false;//指示定时器不可以开始遍历
                ConcurrentBag<Button_reform> button_Reforms = new ConcurrentBag<Button_reform>();//按钮类集合
                ConcurrentBag<SkinTextBox_reform> skinTextBox_Reforms = new ConcurrentBag<SkinTextBox_reform>();//文本输入类集合
                ConcurrentBag<Switch_reform> Switch_reforms = new ConcurrentBag<Switch_reform>();//切换开关类集合
                ConcurrentBag<LedBulb_reform> LedBulb_reforms = new ConcurrentBag<LedBulb_reform>();//指示灯类集合
                ConcurrentBag<ImageButton_reform> ImageButton_reforms = new ConcurrentBag<ImageButton_reform>();//指示灯类集合
                foreach (var In in this.Controls)//遍历窗口控件
                {
                    if (In is Button_reform) button_Reforms.Add((Button_reform)In);//添加按钮对象
                    if (In is SkinTextBox_reform) skinTextBox_Reforms.Add((SkinTextBox_reform)In);//添加文本输入对象
                    if (In is Switch_reform) Switch_reforms.Add((Switch_reform)In);//切换开关对象
                    if (In is LedBulb_reform) LedBulb_reforms.Add((LedBulb_reform)In);//添加对象
                    if (In is ImageButton_reform) ImageButton_reforms.Add((ImageButton_reform)In);//添加对象
                }
                time_Reform.Button_list_1 = button_Reforms;//获取集合
                time_Reform.TextBox_list_1 = skinTextBox_Reforms;//获取集合
                time_Reform.Switch_list_1 = Switch_reforms;//获取集合
                time_Reform.LedBulb_list_1 = LedBulb_reforms;//获取集合
                time_Reform.ImageButton_list_1 = ImageButton_reforms;//获取集合
                PLC_read_ok = true;
                PLC_read_Tick = true;//指示定时器可以开始遍历
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)//用户点击了报警注册
        {
            Event_registration event_Registration = new Event_registration();//实例化报警注册窗口
            event_Registration.ShowDialog();//弹出窗口
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnimateWindow(this.Handle, 200, AW_SLIDE | AW_ACTIVE | AW_VER_NEGATIVE);
        }
    }
}
