using CCWin.SkinClass;
using CCWin.SkinControl;
using CCWin.Win32.Const;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using 自定义Uppercomputer_20200727.EF实体模型;
using 自定义Uppercomputer_20200727.PLC选择;
using 自定义Uppercomputer_20200727.PLC选择.MODBUS_TCP监控窗口;

namespace 自定义Uppercomputer_20200727.控件重做
{
    /// <继承系统定时器>
    /// <重写定时器控件--用于窗口控件的刷新-文本数据刷新->
    class Time_reform : System.Windows.Forms.Timer
    {
        public bool read_status { get; set; }//当前窗口是否允许继续遍历控件---指示着用户开启了编辑模式
        public bool Button_read_status { get; set; }//当前窗口是否允许写入按钮控件参数--可以当成正在遍历控件
        public bool TextBox_read_status { get; set; }//当前窗口是否允许写入文本输入控件参数--可以当成正在遍历控件
        public bool Switch_read_status { get; set; }//当前窗口是否允许写入切换开关类控件参数--可以当成正在遍历控件
        public bool LedBulb_read_status { get; set; }//当前窗口是否允许写入指示灯类控件参数--可以当成正在遍历控件

        public bool ImageButton_read_status { get; set; }//当前窗口是否允许写入无图片按钮类控件参数--可以当成正在遍历控件
        public string Form { get { return Form; } set { present_Form = value; } }//传入窗口属性

        public ConcurrentBag<Button_reform> Button_list_1 { get; set; }//传入要遍历读取状态的按钮类
        List<Button_reform> Button_list = new List<Button_reform>();//传入要遍历读取状态的按钮类
        public ConcurrentBag<SkinTextBox_reform> TextBox_list_1 { get; set; }//传入要遍历读取数据的文本输入类ID
        List<SkinTextBox_reform> TextBox_list = new List<SkinTextBox_reform>();//传入要遍历读取数据的文本输入类ID

        public ConcurrentBag<Switch_reform> Switch_list_1 { get; set; }//传入要遍历读取数据的切换开关类ID
        List<Switch_reform> Switch_list = new List<Switch_reform>();//传入要遍历读取数据的切换开关类ID

        public ConcurrentBag<LedBulb_reform> LedBulb_list_1 { get; set; }//传入要遍历读取数据的指示灯类ID
        List<LedBulb_reform> LedBulb_list = new List<LedBulb_reform>();//传入要遍历读取数据的指示灯类ID
        public ConcurrentBag<ImageButton_reform> ImageButton_list_1 { get; set; }//传入要遍历读取状态的无图片按钮类
        List<ImageButton_reform> ImageButton_list = new List<ImageButton_reform>();//传入要遍历读取状态的无图片按钮类

        string present_Form;//指示当前窗口
        List<Button_Class> button_Classes = new List<Button_Class>();//按钮类参数
        List<numerical_Class> numerical_Classes = new List<numerical_Class>();//文本输入类参数
        List<Switch_Class> Switch_Classes = new List<Switch_Class>();//切换开关类参数
        List<LedBulb_Class> LedBulb_Classes = new List<LedBulb_Class>();//指示灯类参数
        List<ImageButton_Class> ImageButton_Classes = new List<ImageButton_Class>();//无图片按钮类参数

        Button_EF Button_EF;//按钮类EF对象
        numerical_EF numerical_EF;//文本输入类EF对象
        Switch_EF Switch_EF;//切换开关EF对象
        LedBulb_EF LedBulb_EF;//指示灯类EF对象
        ImageButton_EF ImageButton_EF;//无图片按钮类EF对象

        bool Button_EF_ok = false;//指示遍历数据库完成--不在遍历数据库
        bool numerical_EF_ok = false;//指示遍历数据库完成--不在遍历数据库
        bool Switch_EF_ok = false;//指示遍历数据库完成--不在遍历数据库
        bool LedBulb_EF_ok = false;//指示遍历数据库完成--不在遍历数据库
        bool ImageButton_EF_ok = false;//指示遍历数据库完成--不在遍历数据库

        Form Form_Tick;//传入要刷新的窗口
        public Time_reform(Form form)//构造函数
        {
            this.Button_EF = new Button_EF();//实例化按钮类EF对象
            this.numerical_EF = new numerical_EF();//实例化文本输入类对象
            this.Switch_EF = new Switch_EF();//实例化切换开关类EF对象
            this.LedBulb_EF = new LedBulb_EF();//实例化指示灯类EF对象
            this.ImageButton_EF = new ImageButton_EF();//实例化无图片按钮类EF对象

            this.Tick += Time_Tick_button;//注册按钮类刷新事件
            this.Tick += Time_Tick_Textbox;//注册文本输入类刷新事件
            this.Tick += Time_Tick_Switch;//注册切换开关类刷新事件
            this.Tick += Time_Tick_LedBulb;//注册指示灯类刷新事件
            this.Tick += Time_Tick_ImageButton;//注册无图片按钮类刷新事件
            this.Form_Tick = form;//获取要刷新的窗口
        }
        /// <重写定时器事件-->
        private void Time_Tick_button(object send,EventArgs e)//定时器事件--刷新按钮类控件
        {
            if (read_status != false) { Button_EF_ok = false; return; }//窗口不允许读取
            if (this.Button_read_status != false || Button_list_1.IsNull()) return;//直接返回方法--指示当前控件正在遍历this.TextBox_read_status != false || 
                                                                                   //try
                                                                                   //{
            ThreadPool.QueueUserWorkItem((data_run) =>//线程池--把当前任务加到序列中
            {
                //先开始遍历数据库按钮的参数
                Button_read_status = true;//指示着本类开始遍历控件
                if (Button_EF_ok != true)
                {
                    button_Classes.Clear();//移除所有选项
                    Button_list.Clear();//移除所有选项   
                    foreach (Button_reform list in Button_list_1)//遍历按钮类--获取数据库中的参数
                    {
                        if (read_status != false) { Button_EF_ok = false; this.Button_read_status = false; return; }//窗口不允许读取
                        button_Classes.Add(Button_EF.Button_Parameter_Query(list.Parent + "-" + list.Name));//遍历获取参数
                        Button_list.Add(list);
                    }
                    Button_EF_ok = true;//遍历完成
                }
                if(button_Classes.Count != Button_list.Count) { Button_EF_ok = false; this.Button_read_status = false; return; }//窗口不允许读取
                //开始遍历PLC-并且写入状态
                for (int i = 0; i < button_Classes.Count; i++)
                {

                    if ((Button_list.Count - button_Classes.Count) < 0) { Button_list.Clear(); button_Classes.Clear(); this.Button_read_status = false; return; } //数据库读取信息与窗口不符合
                    if (read_status != false ) { Button_EF_ok = false; this.Button_read_status = false; return; }//窗口不允许读取
                    if (button_Classes[i].IsNull() || Button_list[i].IsNull()) continue;//跳出循环进入下一次
                    if (button_Classes[i].ID.Trim() != Button_list[i].Parent + "-" + Button_list[i].Name) continue;//如果ID不对直接开启下次遍历
                    plc(button_Classes[i].读写设备.Trim(), button_Classes[i], Button_list[i]);//开始遍历PLC并且写入按钮状态                    
                }
                Button_read_status = false;//指示窗口可以进行遍历
            });         
        }
        private  string plc(string pLC, Button_Class button_Class, Button_reform button_Reform)//根据PLC类型读取--按钮类
        {
            switch (pLC)
            {
                case "Mitsubishi":
                    if (PLCselect_Form.Mitsubishi.Trim() != "在线访问")//判断用户选定模式
                    {
                        IPLC_interface mitsubishi_AxActUtlType = new Mitsubishi_axActUtlType();//实例化接口--实现三菱仿真
                        if (mitsubishi_AxActUtlType.PLC_ready)//PLC是否准备完成
                        {
                            List<bool> data = mitsubishi_AxActUtlType.PLC_read_M_bit(button_Class.读写设备_地址.Trim(), button_Class.读写设备_地址_具体地址.Trim());//读取状态
                            button_state(button_Reform, button_Class, data[0] == true ? PLC选择.Button_state.ON : PLC选择.Button_state.Off);//写入状态
                        }
                    }
                    else
                    {
                        IPLC_interface mitsubishi = new Mitsubishi_realize();//实例化接口--实现三菱在线访问
                        if (mitsubishi.PLC_ready)//PLC是否准备完成
                        {
                            List<bool> data = mitsubishi.PLC_read_M_bit(button_Class.读写设备_地址.Trim(), button_Class.读写设备_地址_具体地址.Trim());//读取状态
                            button_state(button_Reform, button_Class, data[0] == true ? PLC选择.Button_state.ON : PLC选择.Button_state.Off);//写入状态
                        }
                    }
                    break;
                case "Siemens":
                    IPLC_interface Siemens = new Siemens_realize();//实例化接口--实现西门子在线访问
                    if (Siemens.PLC_ready)//PLC是否准备完成
                    {
                        List<bool> data = Siemens.PLC_read_M_bit(button_Class.读写设备_地址.Trim(), button_Class.读写设备_地址_具体地址.Trim());//读取状态
                        button_state(button_Reform, button_Class, data[0] == true ? PLC选择.Button_state.ON : PLC选择.Button_state.Off);//写入状态
                    }
                    break;
                case "Modbus_TCP":
                    MODBUD_TCP MODBUD_TCP = new MODBUD_TCP();//实例化接口--实现MODBUS TCP
                    if (MODBUD_TCP.IPLC_interface_PLC_ready)//PLC是否准备完成
                    {
                        List<bool> data = MODBUD_TCP.IPLC_interface_PLC_read_M_bit(button_Class.读写设备_地址.Trim(), button_Class.读写设备_地址_具体地址.Trim());//读取状态
                        button_state(button_Reform, button_Class, data[0] == true ? PLC选择.Button_state.ON : PLC选择.Button_state.Off);//写入状态
                    }
                    break;
            }
            return "OK";
        }
        private void button_state(Button_reform button_Reform, Button_Class button_Classes, Button_state button_State)//填充按钮类
        {
            if (Form_Tick.IsHandleCreated != true) return;//判断创建是否加载完成            
            Form_Tick.BeginInvoke((MethodInvoker)delegate//委托当前窗口处理控件UI
            {
                switch (button_State)
                {
                    case PLC选择.Button_state.Off:
                        button_Reform.Text = button_Classes.Control_state_0_content.Trim();//设置文本
                        button_Reform.ForeColor = Color.FromName(button_Classes.Control_state_0_colour.Trim());//获取数据库中颜色名称进行设置
                        button_Reform.Font = new Font(button_Classes.Control_state_0_typeface.Trim(), button_Classes.Control_state_0_size.ToInt32(), FontStyle.Bold);//设置字体与大小
                        button_Reform.TextAlign = ContentAlignment_1(button_Classes.Control_state_0_aligning.Trim());//设置对齐方式
                        break;
                    case PLC选择.Button_state.ON:
                        button_Reform.Text = button_Classes.Control_state_1_content1.Trim();//设置文本
                        button_Reform.ForeColor = Color.FromName(button_Classes.Control_state_1_colour.Trim());//获取数据库中颜色名称进行设置
                        button_Reform.Font = new Font(button_Classes.Control_state_1_typeface.Trim(), button_Classes.Control_state_1_size.ToInt32(), FontStyle.Bold);//设置字体与大小
                        button_Reform.TextAlign = ContentAlignment_1(button_Classes.Control_state_1_aligning.Trim());//设置对齐方式
                        break;
                }
            });
        }

        private void Time_Tick_Switch(object send, EventArgs e)//定时器事件--刷新切换开关类控件
        {
            if (read_status != false) { Switch_EF_ok = false; return; }//窗口不允许读取
            if (this.Switch_read_status != false || Switch_list_1.IsNull()) return;//直接返回方法--指示当前控件正在遍历this.TextBox_read_status != false || 
            ThreadPool.QueueUserWorkItem((data_run) =>//线程池--把当前任务加到序列中
            {
                //先开始遍历数据库切换开关的参数
                Switch_read_status = true;//指示着本类开始遍历控件
                if (Switch_EF_ok != true)
                {
                    Switch_Classes.Clear();//移除所有选项
                    Switch_list.Clear();//移除所有选项   
                    foreach (Switch_reform list in Switch_list_1)//遍历按钮类--获取数据库中的参数
                    {
                        if (read_status != false) { Switch_EF_ok = false; this.Switch_read_status = false; return; }//窗口不允许读取
                        Switch_Classes.Add(Switch_EF.Button_Parameter_Query(list.Parent + "-" + list.Name));//遍历获取参数
                        Switch_list.Add(list);
                    }
                    Switch_EF_ok = true;//遍历完成
                }
                if (Switch_Classes.Count != Switch_list.Count) { Switch_EF_ok = false; this.Switch_read_status = false; return; }//窗口不允许读取
                //开始遍历PLC-并且写入状态
                for (int i = 0; i < Switch_Classes.Count; i++)
                {
                    if ((Switch_list.Count - Switch_Classes.Count) < 0) { Switch_list.Clear(); Switch_Classes.Clear(); this.Switch_read_status = false; return; } //数据库读取信息与窗口不符合
                    if (read_status != false ) { Switch_EF_ok = false; this.Switch_read_status = false; return; }//窗口不允许读取
                    if(Switch_Classes[i].IsNull() || Switch_list[i].IsNull()) continue;//跳出循环进入下一次
                    if (Switch_Classes[i].ID.Trim() != Switch_list[i].Parent + "-" + Switch_list[i].Name) continue;//如果ID不对直接开启下次遍历
                    plc(Switch_Classes[i].读写设备.Trim(), Switch_Classes[i], Switch_list[i]);//开始遍历PLC并且写入按钮状态                    
                }
                Switch_read_status = false;//指示窗口可以进行遍历
            });       
        }
        private string plc(string pLC, Switch_Class switch_Class, Switch_reform switch_reform)//根据PLC类型读取--切换开关类
        {
            switch (pLC)
            {
                case "Mitsubishi":
                    if (PLCselect_Form.Mitsubishi.Trim() != "在线访问")//判断用户选定模式
                    {
                        IPLC_interface mitsubishi_AxActUtlType = new Mitsubishi_axActUtlType();//实例化接口--实现三菱仿真
                        if (mitsubishi_AxActUtlType.PLC_ready)//PLC是否准备完成
                        {
                            List<bool> data = mitsubishi_AxActUtlType.PLC_read_M_bit(switch_Class.读写设备_地址.Trim(), switch_Class.读写设备_地址_具体地址.Trim());//读取状态
                            button_state(switch_reform, switch_Class, data[0] == true ? PLC选择.Button_state.ON : PLC选择.Button_state.Off);//写入状态
                        }
                    }
                    else
                    {
                        IPLC_interface mitsubishi = new Mitsubishi_realize();//实例化接口--实现三菱在线访问
                        if (mitsubishi.PLC_ready)//PLC是否准备完成
                        {
                            List<bool> data = mitsubishi.PLC_read_M_bit(switch_Class.读写设备_地址.Trim(), switch_Class.读写设备_地址_具体地址.Trim());//读取状态
                            button_state(switch_reform, switch_Class, data[0] == true ? PLC选择.Button_state.ON : PLC选择.Button_state.Off);//写入状态
                        }
                    }
                    break;
                case "Siemens":
                    IPLC_interface Siemens = new Siemens_realize();//实例化接口--实现西门子在线访问
                    if (Siemens.PLC_ready)//PLC是否准备完成
                    {
                        List<bool> data = Siemens.PLC_read_M_bit(switch_Class.读写设备_地址.Trim(), switch_Class.读写设备_地址_具体地址.Trim());//读取状态
                        button_state(switch_reform, switch_Class, data[0] == true ? PLC选择.Button_state.ON : PLC选择.Button_state.Off);//写入状态
                    }
                    break;
                case "Modbus_TCP":
                    MODBUD_TCP MODBUD_TCP = new MODBUD_TCP();//实例化接口--实现MODBUS TCP
                    if (MODBUD_TCP.IPLC_interface_PLC_ready)//PLC是否准备完成
                    {
                        List<bool> data = MODBUD_TCP.IPLC_interface_PLC_read_M_bit(switch_Class.读写设备_地址.Trim(), switch_Class.读写设备_地址_具体地址.Trim());//读取状态
                        button_state(switch_reform, switch_Class, data[0] == true ? PLC选择.Button_state.ON : PLC选择.Button_state.Off);//写入状态
                    }
                    break;
            }
            return "OK";
        }
        private void button_state(Switch_reform button_Reform, Switch_Class button_Classes, Button_state button_State)//填充切换开关类
        {
            if (Form_Tick.IsHandleCreated != true) return;//判断创建是否加载完成            
            Form_Tick.BeginInvoke((MethodInvoker)delegate//委托当前窗口处理控件UI
            {
                switch (button_State)
                {
                    case PLC选择.Button_state.Off:
                        button_Reform.Text = button_Classes.Control_state_0_content.Trim();//设置文本
                        button_Reform.InActiveColor = Color.FromName(button_Classes.Control_state_0_colour.Trim());//获取数据库中颜色名称进行设置
                        button_Reform.Font = new Font(button_Classes.Control_state_0_typeface.Trim(), button_Classes.Control_state_0_size.ToInt32(), FontStyle.Bold);//设置字体与大小
                        button_Reform.TextAlign = ContentAlignment_1(button_Classes.Control_state_0_aligning.Trim());//设置对齐方式
                        button_Reform.BackColor = Color.FromName("182, 182, 182");//填充背景颜色--默认
                        button_Reform.Active = false;
                        break;
                    case PLC选择.Button_state.ON:
                        button_Reform.Text = button_Classes.Control_state_1_content1.Trim();//设置文本
                        button_Reform.InActiveColor = Color.FromName(button_Classes.Control_state_1_colour.Trim());//获取数据库中颜色名称进行设置
                        button_Reform.Font = new Font(button_Classes.Control_state_1_typeface.Trim(), button_Classes.Control_state_1_size.ToInt32(), FontStyle.Bold);//设置字体与大小
                        button_Reform.TextAlign = ContentAlignment_1(button_Classes.Control_state_1_aligning.Trim());//设置对齐方式
                        button_Reform.BackColor = Color.FromName("182, 182, 182");//填充背景颜色--默认
                        button_Reform.Active=true;
                        break;
                }
            });
        }
        private void Time_Tick_LedBulb(object send, EventArgs e)//定时器事件--刷新指示灯类控件
        {
            if (read_status != false) { LedBulb_EF_ok = false; return; }//窗口不允许读取
            if (this.LedBulb_read_status != false || LedBulb_list_1.IsNull()) return;//直接返回方法--指示当前控件正在遍历this.TextBox_read_status != false || 
            ThreadPool.QueueUserWorkItem((data_run) =>//线程池--把当前任务加到序列中
            {
                //先开始遍历数据库指示灯的参数
                LedBulb_read_status = true;//指示着本类开始遍历控件
                if (LedBulb_EF_ok != true)
                {
                    LedBulb_Classes.Clear();//移除所有选项
                    LedBulb_list.Clear();//移除所有选项   
                    foreach (LedBulb_reform list in LedBulb_list_1)//遍历按钮类--获取数据库中的参数
                    {
                        if (read_status != false) { LedBulb_EF_ok = false; this.LedBulb_read_status = false; return; }//窗口不允许读取
                        LedBulb_Classes.Add(LedBulb_EF.Button_Parameter_Query(list.Parent + "-" + list.Name));//遍历获取参数
                        LedBulb_list.Add(list);
                    }
                    LedBulb_EF_ok = true;//遍历完成
                }
                if (LedBulb_Classes.Count != LedBulb_list.Count) { LedBulb_EF_ok = false; this.LedBulb_read_status = false; return; }//窗口不允许读取
                //开始遍历PLC-并且写入状态
                for (int i = 0; i < LedBulb_Classes.Count; i++)
                {
                    if ((LedBulb_list.Count - LedBulb_Classes.Count) < 0) { LedBulb_list.Clear(); LedBulb_Classes.Clear(); this.LedBulb_read_status = false; return; } //数据库读取信息与窗口不符合
                    if (read_status != false) { LedBulb_EF_ok = false; this.LedBulb_read_status = false; return; }//窗口不允许读取
                    if (LedBulb_Classes[i].IsNull() || LedBulb_list[i].IsNull()) continue;//跳出循环进入下一次
                    if (LedBulb_Classes[i].ID.Trim() != LedBulb_list[i].Parent + "-" + LedBulb_list[i].Name) continue;//如果ID不对直接开启下次遍历
                    plc(LedBulb_Classes[i].读写设备.Trim(), LedBulb_Classes[i], LedBulb_list[i]);//开始遍历PLC并且写入按钮状态                    
                }
                LedBulb_read_status = false;//指示窗口可以进行遍历
            });
        }
        private string plc(string pLC, LedBulb_Class LedBulb_Class, LedBulb_reform LedBulb_reform)//根据PLC类型读取--指示灯类
        {
            switch (pLC)
            {
                case "Mitsubishi":
                    if (PLCselect_Form.Mitsubishi.Trim() != "在线访问")//判断用户选定模式
                    {
                        IPLC_interface mitsubishi_AxActUtlType = new Mitsubishi_axActUtlType();//实例化接口--实现三菱仿真
                        if (mitsubishi_AxActUtlType.PLC_ready)//PLC是否准备完成
                        {
                            List<bool> data = mitsubishi_AxActUtlType.PLC_read_M_bit(LedBulb_Class.读写设备_地址.Trim(), LedBulb_Class.读写设备_地址_具体地址.Trim());//读取状态
                            button_state(LedBulb_reform, LedBulb_Class, data[0] == true ? PLC选择.Button_state.ON : PLC选择.Button_state.Off);//写入状态
                        }
                    }
                    else
                    {
                        IPLC_interface mitsubishi = new Mitsubishi_realize();//实例化接口--实现三菱在线访问
                        if (mitsubishi.PLC_ready)//PLC是否准备完成
                        {
                            List<bool> data = mitsubishi.PLC_read_M_bit(LedBulb_Class.读写设备_地址.Trim(), LedBulb_Class.读写设备_地址_具体地址.Trim());//读取状态
                            button_state(LedBulb_reform, LedBulb_Class, data[0] == true ? PLC选择.Button_state.ON : PLC选择.Button_state.Off);//写入状态
                        }
                    }
                    break;
                case "Siemens":
                    IPLC_interface Siemens = new Siemens_realize();//实例化接口--实现西门子在线访问
                    if (Siemens.PLC_ready)//PLC是否准备完成
                    {
                        List<bool> data = Siemens.PLC_read_M_bit(LedBulb_Class.读写设备_地址.Trim(), LedBulb_Class.读写设备_地址_具体地址.Trim());//读取状态
                        button_state(LedBulb_reform, LedBulb_Class, data[0] == true ? PLC选择.Button_state.ON : PLC选择.Button_state.Off);//写入状态
                    }
                    break;
                case "Modbus_TCP":
                    MODBUD_TCP MODBUD_TCP = new MODBUD_TCP();//实例化接口--实现MODBUS TCP
                    if (MODBUD_TCP.IPLC_interface_PLC_ready)//PLC是否准备完成
                    {
                        List<bool> data = MODBUD_TCP.IPLC_interface_PLC_read_M_bit(LedBulb_Class.读写设备_地址.Trim(), LedBulb_Class.读写设备_地址_具体地址.Trim());//读取状态
                        button_state(LedBulb_reform, LedBulb_Class, data[0] == true ? PLC选择.Button_state.ON : PLC选择.Button_state.Off);//写入状态
                    }
                    break;
            }
            return "OK";
        }
        private void button_state(LedBulb_reform button_Reform, LedBulb_Class button_Classes, Button_state button_State)//填充指示灯类
        {
            if (Form_Tick.IsHandleCreated != true) return;//判断创建是否加载完成            
            Form_Tick.BeginInvoke((MethodInvoker)delegate//委托当前窗口处理控件UI
            {
                switch (button_State)
                {
                    case PLC选择.Button_state.Off:
                        button_Reform.Text = button_Classes.Control_state_0_content.Trim();//设置文本
                        button_Reform.Color = Color.FromName(button_Classes.Control_state_0_colour.Trim());//获取数据库中颜色名称进行设置
                        button_Reform.Font = new Font(button_Classes.Control_state_0_typeface.Trim(), button_Classes.Control_state_0_size.ToInt32(), FontStyle.Bold);//设置字体与大小
                        button_Reform.BackColor = Color.FromName("182, 182, 182");//填充背景颜色--默认
                        break;
                    case PLC选择.Button_state.ON:
                        button_Reform.Text = button_Classes.Control_state_1_content1.Trim();//设置文本
                        button_Reform.Color = Color.FromName(button_Classes.Control_state_1_colour.Trim());//获取数据库中颜色名称进行设置
                        button_Reform.Font = new Font(button_Classes.Control_state_1_typeface.Trim(), button_Classes.Control_state_1_size.ToInt32(), FontStyle.Bold);//设置字体与大小
                        button_Reform.BackColor = Color.FromName("182, 182, 182");//填充背景颜色--默认
                        break;
                }
            });
        }
        private void Time_Tick_ImageButton(object send, EventArgs e)//定时器事件--刷新无图片按钮类控件
        {
            if (read_status != false) { ImageButton_EF_ok = false; return; }//窗口不允许读取
            if (this.ImageButton_read_status != false || ImageButton_list_1.IsNull()) return;//直接返回方法--指示当前控件正在遍历this.TextBox_read_status != false || 
            ThreadPool.QueueUserWorkItem((data_run) =>//线程池--把当前任务加到序列中
            {
                //先开始遍历数据库按钮的参数
                ImageButton_read_status = true;//指示着本类开始遍历控件
                if (ImageButton_EF_ok != true)
                {
                    ImageButton_Classes.Clear();//移除所有选项
                    ImageButton_list.Clear();//移除所有选项   
                    foreach (ImageButton_reform list in ImageButton_list_1)//遍历按钮类--获取数据库中的参数
                    {
                        if (read_status != false) { ImageButton_EF_ok = false; this.ImageButton_read_status = false; return; }//窗口不允许读取
                        ImageButton_Classes.Add(ImageButton_EF.Button_Parameter_Query(list.Parent + "-" + list.Name));//遍历获取参数
                        ImageButton_list.Add(list);
                    }
                    ImageButton_EF_ok = true;//遍历完成
                }
                if (ImageButton_Classes.Count != ImageButton_list.Count) { ImageButton_EF_ok = false; this.ImageButton_read_status = false; return; }//窗口不允许读取
                //开始遍历PLC-并且写入状态
                for (int i = 0; i < ImageButton_Classes.Count; i++)
                {
                    if ((ImageButton_list.Count - ImageButton_Classes.Count) < 0) { ImageButton_list.Clear(); ImageButton_Classes.Clear(); this.ImageButton_read_status = false; return; } //数据库读取信息与窗口不符合
                    if (read_status != false) { ImageButton_EF_ok = false; this.ImageButton_read_status = false; return; }//窗口不允许读取
                    if (ImageButton_Classes[i].IsNull() || ImageButton_list[i].IsNull()) continue;//跳出循环进入下一次
                    if (ImageButton_Classes[i].ID.Trim() != ImageButton_list[i].Parent + "-" + ImageButton_list[i].Name) continue;//如果ID不对直接开启下次遍历
                    plc(ImageButton_Classes[i].读写设备.Trim(), ImageButton_Classes[i], ImageButton_list[i]);//开始遍历PLC并且写入按钮状态                    
                }
                ImageButton_read_status = false;//指示窗口可以进行遍历
            });         
        }
        private string plc(string pLC, ImageButton_Class button_Class, ImageButton_reform button_Reform)//根据PLC类型读取--按钮类
        {
            switch (pLC)
            {
                case "Mitsubishi":
                    if (PLCselect_Form.Mitsubishi.Trim() != "在线访问")//判断用户选定模式
                    {
                        IPLC_interface mitsubishi_AxActUtlType = new Mitsubishi_axActUtlType();//实例化接口--实现三菱仿真
                        if (mitsubishi_AxActUtlType.PLC_ready)//PLC是否准备完成
                        {
                            List<bool> data = mitsubishi_AxActUtlType.PLC_read_M_bit(button_Class.读写设备_地址.Trim(), button_Class.读写设备_地址_具体地址.Trim());//读取状态
                            button_state(button_Reform, button_Class, data[0] == true ? PLC选择.Button_state.ON : PLC选择.Button_state.Off);//写入状态
                        }
                    }
                    else
                    {
                        IPLC_interface mitsubishi = new Mitsubishi_realize();//实例化接口--实现三菱在线访问
                        if (mitsubishi.PLC_ready)//PLC是否准备完成
                        {
                            List<bool> data = mitsubishi.PLC_read_M_bit(button_Class.读写设备_地址.Trim(), button_Class.读写设备_地址_具体地址.Trim());//读取状态
                            button_state(button_Reform, button_Class, data[0] == true ? PLC选择.Button_state.ON : PLC选择.Button_state.Off);//写入状态
                        }
                    }
                    break;
                case "Siemens":
                    IPLC_interface Siemens = new Siemens_realize();//实例化接口--实现西门子在线访问
                    if (Siemens.PLC_ready)//PLC是否准备完成
                    {
                        List<bool> data = Siemens.PLC_read_M_bit(button_Class.读写设备_地址.Trim(), button_Class.读写设备_地址_具体地址.Trim());//读取状态
                        button_state(button_Reform, button_Class, data[0] == true ? PLC选择.Button_state.ON : PLC选择.Button_state.Off);//写入状态
                    }
                    break;
                case "Modbus_TCP":
                    MODBUD_TCP MODBUD_TCP = new MODBUD_TCP();//实例化接口--实现MODBUS TCP
                    if (MODBUD_TCP.IPLC_interface_PLC_ready)//PLC是否准备完成
                    {
                        List<bool> data = MODBUD_TCP.IPLC_interface_PLC_read_M_bit(button_Class.读写设备_地址.Trim(), button_Class.读写设备_地址_具体地址.Trim());//读取状态
                        button_state(button_Reform, button_Class, data[0] == true ? PLC选择.Button_state.ON : PLC选择.Button_state.Off);//写入状态
                    }
                    break;
            }
            return "OK";
        }
        private void button_state(ImageButton_reform button_Reform, ImageButton_Class button_Classes, Button_state button_State)//填充按钮类
        {
            if (Form_Tick.IsHandleCreated != true) return;//判断创建是否加载完成            
            Form_Tick.BeginInvoke((MethodInvoker)delegate//委托当前窗口处理控件UI
            {
                switch (button_State)
                {
                    case PLC选择.Button_state.Off:
                        button_Reform.Text = button_Classes.Control_state_0_content.Trim();//设置文本
                        button_Reform.ForeColor = Color.FromName(button_Classes.Control_state_0_colour.Trim());//获取数据库中颜色名称进行设置
                        button_Reform.Font = new Font(button_Classes.Control_state_0_typeface.Trim(), button_Classes.Control_state_0_size.ToInt32(), FontStyle.Bold);//设置字体与大小
                        button_Reform.TextAlign = ContentAlignment_1(button_Classes.Control_state_0_aligning.Trim());//设置对齐方式
                        break;
                    case PLC选择.Button_state.ON:
                        button_Reform.Text = button_Classes.Control_state_1_content1.Trim();//设置文本
                        button_Reform.ForeColor = Color.FromName(button_Classes.Control_state_1_colour.Trim());//获取数据库中颜色名称进行设置
                        button_Reform.Font = new Font(button_Classes.Control_state_1_typeface.Trim(), button_Classes.Control_state_1_size.ToInt32(), FontStyle.Bold);//设置字体与大小
                        button_Reform.TextAlign = ContentAlignment_1(button_Classes.Control_state_1_aligning.Trim());//设置对齐方式
                        break;
                }
            });
        }
        private void Time_Tick_Textbox(object send, EventArgs e)//定时器事件--刷新文本输入类控件
        {
            if (read_status != false) { numerical_EF_ok = false; return; }//窗口不允许读取
            if (this.TextBox_read_status != false || TextBox_list_1.IsNull()) return;//直接返回方法--指示当前控件正在遍历 this.Button_read_status != false || 
            ThreadPool.QueueUserWorkItem((data_run) =>//线程池--把当前任务加到序列中
            {
                //先开始遍历数据库按钮的参数
                Thread.Sleep(100);
                this.TextBox_read_status = true;//指示着本类开始遍历控件
                if (numerical_EF_ok != true)
                {
                    numerical_Classes.Clear();//移除所有选项 
                    TextBox_list.Clear();
                    foreach (SkinTextBox_reform list in TextBox_list_1)//遍历按钮类--获取数据库中的参数
                    {
                        if (read_status != false) { numerical_EF_ok = false; this.TextBox_read_status = false; return; }//窗口不允许读取
                        numerical_Classes.Add(numerical_EF.numerical_Parameter_Query(list.Parent + "- " + list.Name));//遍历获取参数
                        TextBox_list.Add(list);
                    }
                    numerical_EF_ok = true;//指着遍历数据库完成
                }
                //开始遍历PLC-并且写入状态
                if (TextBox_list.Count != numerical_Classes.Count) { TextBox_list.Clear(); numerical_Classes.Clear(); this.TextBox_read_status = false; numerical_EF_ok = false; return; }//数据库读取信息与窗口不符合
                for (int i = 0; i < numerical_Classes.Count; i++)
                {
                    if (read_status != false ) { numerical_EF_ok = false; ; this.TextBox_read_status = false; return; }//窗口不允许读取
                    if(numerical_Classes[i].IsNull() || TextBox_list[i].IsNull()) continue;//跳出循环进入下一次
                    if (numerical_Classes[i].ID.Trim() != TextBox_list[i].Parent + "- " + TextBox_list[i].Name) continue;//如果ID不对直接开启下次遍历
                    plc(numerical_Classes[i].读写设备.Trim(), numerical_Classes[i], TextBox_list[i]);//开始遍历PLC并且写入文本状态
                }
                this.TextBox_read_status = false;//指示窗口可以进行遍历
            });
        }
        private string plc(string pLC, numerical_Class numerical_Class, SkinTextBox_reform skinTextBox_Reform)//根据PLC类型读取--文本输入类
        {
            switch (pLC)
            {
                case "Mitsubishi":
                    if (PLCselect_Form.Mitsubishi.Trim() != "在线访问")//判断用户选定模式
                    {
                        IPLC_interface mitsubishi_AxActUtlType = new Mitsubishi_axActUtlType();//实例化接口--实现三菱仿真
                        if (mitsubishi_AxActUtlType.PLC_ready)//PLC是否准备完成
                        {
                            string data = mitsubishi_AxActUtlType.PLC_read_D_register(numerical_Class.读写设备_地址.Trim(), numerical_Class.读写设备_地址_具体地址.Trim(), TextBox_format(numerical_Class.资料格式));//读取PLC数值
                            TextBox_state(skinTextBox_Reform, numerical_Class, data);//填充文本数据--自动判断用户设定的小数点位置--多余的异常
                        }
                    }
                    else
                    {
                        IPLC_interface mitsubishi = new Mitsubishi_realize();//实例化接口--实现三菱在线访问
                        if (mitsubishi.PLC_ready)//PLC是否准备完成
                        {
                            string data = mitsubishi.PLC_read_D_register(numerical_Class.读写设备_地址.Trim(), numerical_Class.读写设备_地址_具体地址.Trim(), TextBox_format(numerical_Class.资料格式));//读取PLC数值
                            TextBox_state(skinTextBox_Reform, numerical_Class, data);//填充文本数据--自动判断用户设定的小数点位置--多余的异常
                        }
                    }
                    break;
                case "Siemens":
                    IPLC_interface Siemens = new Siemens_realize();//实例化接口--实现西门子在线访问
                    if (Siemens.PLC_ready)//PLC是否准备完成
                    {
                        string data = Siemens.PLC_read_D_register(numerical_Class.读写设备_地址.Trim(), numerical_Class.读写设备_地址_具体地址.Trim(), TextBox_format(numerical_Class.资料格式));//读取PLC数值
                        TextBox_state(skinTextBox_Reform, numerical_Class, data);//填充文本数据--自动判断用户设定的小数点位置--多余的异常
                    }
                    break;
                case "Modbus_TCP":
                    MODBUD_TCP MODBUD_TCP = new MODBUD_TCP();//实例化接口--实现MODBUS TCP
                    if (MODBUD_TCP.IPLC_interface_PLC_ready)//PLC是否准备完成
                    {
                        //由于modbus_TCP读写状态不同 读输出 写输入模式 
                         string data = MODBUD_TCP.IPLC_interface_PLC_read_D_register(numerical_Class.读写设备_地址.Trim(), numerical_Class.读写设备_地址_具体地址.Trim(), TextBox_format(numerical_Class.资料格式));//读取PLC数值
                        TextBox_state(skinTextBox_Reform, numerical_Class, data);//填充文本数据--自动判断用户设定的小数点位置--多余的异常
                    }
                    break;
            }
            return "OK";
        }
        private void TextBox_state(SkinTextBox_reform skinTextBox_Reform ,numerical_Class numerical_Class,string Data)//填充文本数据
        {
            int Inde = Data.IndexOf('.');//搜索数据是否有小数点
            if (Inde > 0|| Inde>= numerical_Class.小数点以下位数.ToInt32())//判断是否有小数点
            {
                int In = Data.Length -1 -numerical_Class.小数点以下位数.ToInt32()- Inde;//实现原理--先获取数据长度-后减1-小数点-在减去设定数-获取小数点位置
                for (int i = 0; i < In; i++) Data = Data.Remove(Data.Length - 1, 1); //移除掉                
            }
            else
                Data = complement(Data, numerical_Class);//然后位数不够--自动补码
            if (numerical_Class.小数点以下位数.ToInt32()<1) Data = Data.Replace('.', ' ');//如果用户设定没有小数点直接去除小数点
            skinTextBox_Reform.Text = Data;//直接填充数据
        }
        //下面都是-祖传代码---懒得重新写成类----凑数
        private numerical_format TextBox_format(string Name)//索引控件的资料格式
        {
            foreach (numerical_format suit in Enum.GetValues(typeof(numerical_format)))
            {
                if (suit.ToString()==Name.Trim()) return suit;//找到资料格式并返回
            }
            return numerical_format.Signed_32_Bit;//找不到直接返回默认资料格式
        }
        private System.Drawing.ContentAlignment ContentAlignment_1(string Name)//获取字体的对齐方式--按钮-文本类
        {
            System.Drawing.ContentAlignment contentAlignment = System.Drawing.ContentAlignment.MiddleCenter;//定义对齐方式
            switch (Name.Trim())
            {
                case "左对齐":
                    contentAlignment = System.Drawing.ContentAlignment.MiddleLeft;//设置左对齐
                    break;
                case "居中对齐":
                    contentAlignment = System.Drawing.ContentAlignment.MiddleCenter;//设置居中对齐
                    break;
                case "右对齐":
                    contentAlignment = System.Drawing.ContentAlignment.MiddleRight;//设置有右对齐
                    break;
            }
            return contentAlignment;//返回数据
        }
        private string complement(string Name,numerical_Class numerical_Class)//实现浮点小数自动补码
        {
            int Inde = Name.IndexOf('.');//搜索数据是否有小数点
            if (Inde < 0 & numerical_Class.小数点以下位数.ToInt32() != 0) Name += ".";//自动补码小数点
            if (numerical_Class.小数点以下位数.ToInt32() > 0 & Inde < 0)
            {
                for (int i = 0; i < numerical_Class.小数点以下位数.ToInt32(); i++) Name += "0";//填充数据
            }
            if (numerical_Class.小数点以下位数.ToInt32() > 0 & Inde > 0)
            {
                int In = Name.Length - 1 - Inde;
                for (int i = 0; i < numerical_Class.小数点以下位数.ToInt32() - In; i++) Name += "0";//填充数据
            }
            return Name;//返回数据
        }
        private System.Windows.Forms.HorizontalAlignment HorizontalAlignment_1(string Name)//获取字体的对齐方式--文本输入类
        {
            System.Windows.Forms.HorizontalAlignment horizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;//定义对齐方式
            switch (Name.Trim())
            {
                case "左对齐":
                    horizontalAlignment = System.Windows.Forms.HorizontalAlignment.Left;//设置左对齐
                    break;
                case "居中对齐":
                    horizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;//设置居中对齐
                    break;
                case "右对齐":
                    horizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;//设置有右对齐
                    break;
            }
            return horizontalAlignment;//返回数据
        }
        private int[] point_or_Size(string Name)//分割-来自数据库的-位置与大小数据
        {
            string[] segmentation;//定义分割数组
            segmentation = Name.Split('-');
            return new int[] { Convert.ToInt32(segmentation[0] ?? "81"), Convert.ToInt32(segmentation[1] ?? "31") };
        }
        ~Time_reform()//析构函数
        {            
            this.Tick -= Time_Tick_button;//注册按钮类刷新事件
            this.Tick -= Time_Tick_Textbox;//注册文本输入类刷新事件
            this.Tick -= Time_Tick_Switch;//注册切换开关类刷新事件
            this.Tick -= Time_Tick_LedBulb;//注册指示灯类刷新事件
            this.Tick -= Time_Tick_ImageButton;//注册无图片按钮类刷新事件
        }
    }
}
