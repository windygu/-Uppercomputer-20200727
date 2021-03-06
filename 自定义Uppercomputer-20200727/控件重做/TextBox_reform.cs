﻿using CCWin.SkinClass;
using CCWin.SkinControl;
using CSEngineTest;
using DragResizeControlWindowsDrawDemo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;   
using System.Windows.Forms;
using 自定义Uppercomputer_20200727.EF实体模型;
using 自定义Uppercomputer_20200727.PLC选择;
using 自定义Uppercomputer_20200727.PLC选择.MODBUS_TCP监控窗口;
using 自定义Uppercomputer_20200727.控件重做.按钮类与宏指令通用类;
using 自定义Uppercomputer_20200727.文本输入键盘;

namespace 自定义Uppercomputer_20200727.控件重做
{
    /// <summary>
    /// 本类主要重写系统数值输入控件
    /// 继承系统数值输入控件
    /// </summary>
    class SkinTextBox_reform : TextBox
    {
        string SkinTextBox_ID { get; set; }//文本属性ID
        SkinContextMenuStrip_reform menuStrip_Reform;//绑定右键菜单类
        numerical_Class numerical_Classes;//实例化当前控件参数
        bool write_ok;//太久用户不输入文本自动允许PLC写入数据
        public bool write_ok_plc { get => write_ok; }//控件允许输入状态--只读状态
        public SkinTextBox_reform()//构造函数
        {
            this.menuStrip_Reform = new SkinContextMenuStrip_reform();//实例化右键菜单
            this.ContextMenuStrip = this.menuStrip_Reform;//绑定右键菜单
            this.MouseDown += MouseDown_reform;//注册事件
            this.MouseUp += MouseUp_reform;//注册事件
            this.MouseMove += MouseMove__reform;//注册事件
            this.MouseEnter += MouseEnter_reform;//注册事件--获取控件信息
            this.KeyPress += numerical_KeyPress;//注册事件    
            this.MouseDown += numerical_MouseDown;//注册事件
            this.DoubleClick += numerical_DoubleClick;//注册事件
            DragResizeControl.RegisterControl(this);//实现控件改变大小与拖拽位置
            write_ok = true;//默认允许写入控件文本
            this.ReadOnly = true;//指示当前控件只读
        }
        /// <方法重写当鼠标移到控件时获取——ID>
        private void MouseEnter_reform(object send, EventArgs e)
        {
            this.Cursor = Cursors.Hand;//改变鼠标状态
            SkinTextBox_reform button = (SkinTextBox_reform)send;//获取控件信息
            this.SkinTextBox_ID = button.Parent.ToString();//写入信息
            this.menuStrip_Reform.SkinContextMenuStrip_Button_ID = button.Parent.ToString();//写入信息
            this.menuStrip_Reform.all_purpose = send;//获取事件触发的控件
            this.menuStrip_Reform.SkinContextMenuStrip_Button_type = this.GetType().Name;//获取类型名称
             //如果用户不开启编辑模式--右键菜单选项为锁定状态
            this.menuStrip_Reform.Enabled = Form2.edit_mode;//启用状态

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
        {
            //标志位复位-并且写入数据库
            if (startMove)
            {
                Button_EF button_EF = new Button_EF();//实例化EF
                button_EF.Button_Parameter_modification(this.Parent + "- " + this.Name
                    , new control_location
                    {
                        location = (numerical_public.Size_X(this.Left)).ToString() + "-" + (numerical_public.Size_Y(this.Top)).ToString(),
                        size = (numerical_public.Size_X(this.Size.Width) + "-" + numerical_public.Size_Y(this.Size.Height))
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
                //this.Location = new Point(x, y);
            }
        }
        /// <方法重写实现不允许用输入--使用键盘 —>
        private void numerical_KeyPress(object sender, KeyPressEventArgs e)//键盘事件
        {            
            e.Handled = true; //不允许用输入--使用键盘        
        }
        /// <方法重写实现当鼠标在控件上方按下时触发--获取数据库中相应控件的参数—实现参数写入与约束>
        private void numerical_MouseDown(object sender, MouseEventArgs e)//当前控件当鼠标在控件上方按下时触发--获取数据库中相应控件的参数
        {
            numerical_EF numerical_EF = new numerical_EF();//实例化EF对象
            numerical_Classes=numerical_EF.numerical_Parameter_Query(((SkinTextBox_reform)sender).Parent + "- " + ((SkinTextBox_reform)sender).Name);//查询控件参数信息            
        }
        /// <方法重写实现用户双击控件---进入键盘—实现参数写入与约束>
        private void numerical_DoubleClick(object sender, EventArgs e)//用户双击控件---进入键盘
        {
            /// <方法定时擦除用户是否在输入>
            write_ok = false;//不允许修改控件
            if (numerical_Classes.资料格式.Trim() == "Hex_16_Bit" || numerical_Classes.资料格式.Trim() == "Hex_32_Bit"||numerical_Classes.读写设备.Trim()== "HMI")
            {
                keyboard_Hex keyboard_Hex = new keyboard_Hex(this.Text, numerical_Classes);//实例化Hex键盘
                keyboard_Hex.ShowDialog();//显示窗口
                this.Text = keyboard_Hex.O_Text;//获取用户输入的文本
            }
            else
            {
                keyboard keyboard = new keyboard(this.Text, numerical_Classes);//调出键盘
                keyboard.ShowDialog();//显示窗口
                this.Text = keyboard.O_Text;//获取用户输入的文本
            }
            write_ok = true;//允许修改控件
            //把控件文本写到PLC
            if (numerical_Classes.读写不同地址_ON_OFF == 0)
                plc(numerical_Classes.读写设备.Trim());//选择相应PLC 进行写入
            else
                plc(numerical_Classes.写设备_复选.Trim());//选择相应PLC 进行写入
            //plc(numerical_Classes.读写设备.Trim());//选择PLC类型---
        }
        private string plc(string pLC)//根据PLC类型写入
        {
            switch(pLC)
            {
                case "Mitsubishi":
                    if (PLCselect_Form.Mitsubishi.Trim() != "在线访问")//判断用户选定模式
                    {
                        IPLC_interface mitsubishi_AxActUtlType = new Mitsubishi_axActUtlType();//实例化接口--实现三菱仿真
                        if (mitsubishi_AxActUtlType.PLC_ready)
                        {
                            if (numerical_Classes.读写不同地址_ON_OFF == 0)
                                mitsubishi_AxActUtlType.PLC_write_D_register(numerical_Classes.读写设备_地址.Trim(), numerical_Classes.读写设备_地址_具体地址.Trim(), this.Text, Index(numerical_Classes.资料格式));
                            else
                                mitsubishi_AxActUtlType.PLC_write_D_register(numerical_Classes.写设备_地址_复选.Trim(), numerical_Classes.写设备_地址_具体地址_复选.Trim(), this.Text, Index(numerical_Classes.资料格式));
                        }
                        else MessageBox.Show("未连接设备：" + pLC.Trim(), "Err");//推出异常提示用户
                    }
                    else
                    {
                        IPLC_interface mitsubishi = new Mitsubishi_realize();//实例化接口--实现三菱在线访问
                        if (mitsubishi.PLC_ready)
                        {
                            if (numerical_Classes.读写不同地址_ON_OFF == 0)
                                mitsubishi.PLC_write_D_register(numerical_Classes.读写设备_地址.Trim(), numerical_Classes.读写设备_地址_具体地址.Trim(), this.Text, Index(numerical_Classes.资料格式));
                            else
                                mitsubishi.PLC_write_D_register(numerical_Classes.写设备_地址_复选.Trim(), numerical_Classes.写设备_地址_具体地址_复选.Trim(), this.Text, Index(numerical_Classes.资料格式));
                        }
                        else MessageBox.Show("未连接设备：" + pLC.Trim(), "Err");//推出异常提示
                    }
                    break;
                case "Siemens":
                    IPLC_interface Siemens = new Siemens_realize();//实例化接口--实现西门子在线访问
                    if (Siemens.PLC_ready)
                    {
                        if (numerical_Classes.读写不同地址_ON_OFF == 0)
                            Siemens.PLC_write_D_register(numerical_Classes.读写设备_地址.Trim(), numerical_Classes.读写设备_地址_具体地址.Trim(), this.Text, Index(numerical_Classes.资料格式));
                        else
                            Siemens.PLC_write_D_register(numerical_Classes.写设备_地址_复选.Trim(), numerical_Classes.写设备_地址_具体地址_复选.Trim(), this.Text, Index(numerical_Classes.资料格式));
                    }
                    else MessageBox.Show("未连接设备：" + pLC.Trim(), "Err");//推出异常提示
                    break;
                case "Modbus_TCP":
                    MODBUD_TCP MODBUD_TCP = new MODBUD_TCP();//实例化接口--实现MODBUS TCP
                    if (MODBUD_TCP.IPLC_interface_PLC_ready)
                    {
                        if (numerical_Classes.读写不同地址_ON_OFF == 0)
                            MODBUD_TCP.IPLC_interface_PLC_write_D_register(numerical_Classes.读写设备_地址.Trim(), numerical_Classes.读写设备_地址_具体地址.Trim(), this.Text, Index(numerical_Classes.资料格式));
                        else
                            MODBUD_TCP.IPLC_interface_PLC_write_D_register(numerical_Classes.写设备_地址_复选.Trim(), numerical_Classes.写设备_地址_具体地址_复选.Trim(), this.Text, Index(numerical_Classes.资料格式));
                    }
                    else MessageBox.Show("未连接设备：" + pLC.Trim(), "Err");//推出异常提示用户
                    break;
                //写入到 宏指令 静态区D_Data
                case "HMI":
                    if (numerical_Classes.读写不同地址_ON_OFF == 0)
                        macroinstruction_data<int>.D_Data[numerical_Classes.读写设备_地址_具体地址.Trim().ToInt32()] = this.Text;
                    else
                        macroinstruction_data<int>.D_Data[numerical_Classes.写设备_地址_具体地址_复选.Trim().ToInt32()] = this.Text;
                    break;
            }
            return "OK_RUN";
        }
        private numerical_format Index(string Name)//查询索引
        {
            foreach (numerical_format suit in Enum.GetValues(typeof(numerical_format)))
            {
                if(suit.ToString()==Name.Trim()) return suit;//遍历枚举查询索引
            }
            return numerical_format.Unsigned_32_Bit;//如果不匹配则返回默认无符号类型
        }
        protected override void Dispose(bool disposing)
        {
            this.MouseDown -= MouseDown_reform;//注册事件
            this.MouseUp -= MouseUp_reform;//注册事件
            this.MouseMove -= MouseMove__reform;//注册事件
            this.MouseEnter -= MouseEnter_reform;//注册事件--获取控件信息
            this.KeyPress -= numerical_KeyPress;//注册事件    
            this.MouseDown -= numerical_MouseDown;//注册事件 
            this.DoubleClick -= numerical_DoubleClick;//注册事件
            this.menuStrip_Reform.Dispose();
            DragResizeControl.UnRegisterControl(this);
            base.Dispose(disposing);
        }
    }
}
