using CCWin.SkinControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 自定义Uppercomputer_20200727.EF实体模型;

namespace 自定义Uppercomputer_20200727.控件重做
{
    class SkinPictureBox_reform: SkinPictureBox
    {
        public string SkinPictureBox_ID { get; set; }//该按钮ID
        SkinContextMenuStrip_reform menuStrip_Reform;//绑定右键菜单类
        public SkinPictureBox_reform()//构造函数
        {
            this.menuStrip_Reform = new SkinContextMenuStrip_reform();//实例化右键菜单
            this.ContextMenuStrip = this.menuStrip_Reform;//绑定右键菜单
            this.MouseEnter += MouseEnter_reform;//注册事件
            this.Click += Click_reform;//注册事件
            this.MouseDown += MouseDown_reform;//注册事件
            this.MouseUp += MouseUp_reform;//注册事件
            this.MouseMove += MouseMove__reform;//注册事件
            this.DoubleClick += DoubleClick_reform;//注册事件
        }
        /// <方法重写当鼠标移到控件时获取——ID>
        private void MouseEnter_reform(object send, EventArgs e)
        {
            SkinPictureBox button = (SkinPictureBox)send;//获取控件信息
            this.SkinPictureBox_ID = button.Parent.ToString();//写入信息
            this.menuStrip_Reform.SkinContextMenuStrip_Button_ID = button.Parent.ToString();//写入信息
            this.menuStrip_Reform.all_purpose = send;//获取事件触发的控件
            this.menuStrip_Reform.SkinContextMenuStrip_Button_type = this.GetType().Name;//获取类型名称
             //如果用户不开启编辑模式--右键菜单选项为锁定状态
            this.menuStrip_Reform.Enabled = Form2.edit_mode;//启用状态
        }
        /// <方法重写当按钮按下触发—>
        private void Click_reform(object send, EventArgs e)
        {

        }
        /// <方法重写当触发双击>
        private void DoubleClick_reform(object send, EventArgs e)
        {

        }
        /// <方法重写实现拖放功能—>
        bool startMove = false;
        int clickX = 0;  //记录上次点击的鼠标位置
        int clickY = 0;//记录上次点击的鼠标位置
        private void MouseDown_reform(object sender, MouseEventArgs e)//鼠标按下事件
        {  //初始化按钮
            if (Form2.edit_mode != true) return;//返回方法
            clickX = e.X;
            clickY = e.Y;
            startMove = true;
        }
        private void MouseUp_reform(object sender, MouseEventArgs e)//鼠标松开事件
        {  //标志位复位-并且写入数据库
            if (startMove)
            {
                Button_EF button_EF = new Button_EF();//实例化EF
                button_EF.Button_Parameter_modification(((SkinPictureBox_reform)sender).Parent + "-" + ((SkinPictureBox_reform)sender).Name
                    , new control_location
                    {
                        location = (this.Location.X + e.X - clickX).ToString() + "-" + (this.Location.Y +e.Y- clickY).ToString(),
                        size = ((SkinPictureBox_reform)sender).Size.Width + "-" + ((SkinPictureBox_reform)sender).Size.Height
                    });
                startMove = false;
            }
        }
        private void MouseMove__reform(object sender, MouseEventArgs e)//鼠标拖放位置
        {
            if (Form2.edit_mode != true) return;//返回方法
            if (startMove)
            {
                // e.X 是正负数,表示移动的方向
                int x = this.Location.X + e.X - clickX;   //还要减去上次鼠标点击的位置
                int y = e.Y + this.Location.Y - clickY;
                this.Location = new Point(x, y);
            }
        }

        ~SkinPictureBox_reform()//析构函数
        {

        }
    }
}
