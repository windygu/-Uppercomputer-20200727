using CCWin.SkinClass;
using CCWin.SkinControl;
using CCWin.Win32.Const;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using 自定义Uppercomputer_20200727.EF实体模型;
using 自定义Uppercomputer_20200727.图库;
using 自定义Uppercomputer_20200727.控件重做;
using static System.Windows.Forms.Control;

namespace 自定义Uppercomputer_20200727.控制主页面模板
{
    /// <本类主要用于窗口加载过程中同步加载数据库的控件>
    class From_Load_Add
    {
        List<Button_Class> skinButtons = new List<Button_Class>();//按钮类泛型表
        List<picture_Class> skinpicture = new List<picture_Class>();//图片类泛型表
        List<label_Class> skinlabel = new List<label_Class>();//标签类泛型表
        List<numerical_Class> skinnumerical = new List<numerical_Class>();//标签类泛型表
        List<Switch_Class> Switch = new List<Switch_Class>();//选择开关类泛型表
        List<LedBulb_Class> LedBulb = new List<LedBulb_Class>();//指示灯类泛型表
        List<GroupBox_Class> GroupBox = new List<GroupBox_Class>();//标签类泛型表
        List<ImageButton_Class> ImageButton = new List<ImageButton_Class>();//无图片按钮类类泛型表
        List<ScrollingText_Class> ScrollingText = new List<ScrollingText_Class>();//报警条类泛型表
        List<doughnut_Chart_Class> doughnut = new List<doughnut_Chart_Class>();//圆形图类泛型表
        ControlCollection control;//当前窗口控件集合
        List<ImageList> imageLists_1 { get; set; } //图库类集合--不可修改
        //报警条需要参数
        Form Form_event;//当前窗口
        public From_Load_Add(string From_Name, ControlCollection control, List<ImageList> imageLists_1)
        {
            Parameter_Query_Add parameter_Query_Add = new Parameter_Query_Add();//创建EF查询对象
            this.skinButtons = parameter_Query_Add.all_Parameter_Query_Button(From_Name);//查询按钮类
            this.skinpicture = parameter_Query_Add.all_Parameter_Query_picture(From_Name);//查询图片类
            this.skinlabel = parameter_Query_Add.all_Parameter_Query_label(From_Name);//查询标签类
            this.skinnumerical = parameter_Query_Add.all_Parameter_Query_numerical(From_Name);//查询数值类
            this.Switch = parameter_Query_Add.all_Parameter_Query_Switch(From_Name);//查询切换开关类
            this.LedBulb = parameter_Query_Add.all_Parameter_Query_LedBulb(From_Name);//查询指示灯类
            this.GroupBox = parameter_Query_Add.all_Parameter_Query_GroupBox(From_Name);//查询四边框类
            this.ImageButton = parameter_Query_Add.all_Parameter_Query_ImageButton(From_Name);//查询无图片按钮类
            this.doughnut = parameter_Query_Add.all_Parameter_Query_doughnut_Chart(From_Name);//查询圆形图类按钮类
            this.imageLists_1 = imageLists_1;//获取图库
            this.control = control;
            //添加控件
            Load_Add(this.skinButtons);
            Load_Add(this.skinpicture);
            Load_Add(this.skinlabel);
            Load_Add(this.skinnumerical);
            Load_Add(this.Switch);
            Load_Add(this.LedBulb);
            Load_Add(this.ImageButton);
            Load_Add(this.doughnut);
            Load_Add(this.GroupBox);
            //改变控件上下层
            Control_layer_EF layer_EF = new Control_layer_EF();//实例化最下层EF查询对象
            List<Control_layer> control_Layers = layer_EF.all_Parameter_Query_Control_layer(From_Name.Trim());//把查询到数据的保存
            foreach(Control i in control) 
            { 
              for(int index=0;index< control_Layers.Count; index++)
                {
                    if(i.Name==control_Layers[index].type.Trim())
                       if(control_Layers[index].Upper_layer>0)
                            i.BringToFront();//将控件放置所有控件最顶层  
                    else
                            i.SendToBack();//将控件放置所有控件最底层   
                }
            }
        }
        public From_Load_Add(string From_Name, ControlCollection control, List<ImageList> imageLists_1,Form form)
        {
            this.Form_event = form;
            this.imageLists_1 = imageLists_1;//获取图库
            this.control = control;
            Parameter_Query_Add parameter_Query_Add = new Parameter_Query_Add();//创建EF查询对象
            this.ScrollingText = parameter_Query_Add.all_Parameter_Query_ScrollingText(From_Name);//查询报警条类
            Load_Add(ScrollingText);
        }
        private void Load_Add(List<Button_Class> button_Classes)//填充按钮类
        {
            //遍历数组
            foreach (Button_Class add in button_Classes)
            {
                Button_reform reform = new Button_reform();//实例化按钮
                reform.Size = new Size(point_or_Size(add.size)[0], point_or_Size(add.size)[1]);//设置大小
                reform.Location = new Point(point_or_Size(add.location)[0], point_or_Size(add.location)[1]);//设置按钮位置
                reform.Name = add.Control_type.Trim();//设置名称
                reform.Text = add.Control_state_0_content.Trim();//设置文本
                reform.ForeColor = Color.FromName(add.Control_state_0_colour.Trim());//获取数据库中颜色名称进行设置
                reform.Font = new Font(add.Control_state_0_typeface.Trim(),add.Control_state_0_size.ToInt32(), FontStyle.Bold);//设置字体与大小
                reform.TextAlign = ContentAlignment_1(add.Control_state_0_aligning.Trim());//设置对齐方式
                control.Add((Button_reform)reform);
            }
        }
        private void Load_Add(List<picture_Class> button_Classes)//填充图片类
        {
            //遍历数组
            foreach (picture_Class add in button_Classes)
            {
                SkinPictureBox_reform reform = new SkinPictureBox_reform();//实例化按钮
                reform.Size = new Size(point_or_Size(add.size)[0], point_or_Size(add.size)[1]);//设置大小
                reform.Location = new Point(point_or_Size(add.location)[0], point_or_Size(add.location)[1]);//设置按钮位置
                reform.SizeMode= PictureBoxSizeMode.StretchImage;//显示图片方式
                reform.Image = imageLists_1[add.Control_state_0_list].Images[add.Control_state_0_picture];
                reform.Name = add.Control_type.Trim();//设置名称
                control.Add(reform);
            }
        }
        private void Load_Add(List<label_Class> button_Classes)//填充标签类
        {
            //遍历数组
            foreach (label_Class add in button_Classes)
            {
                SkinLabel_reform reform = new SkinLabel_reform();//实例化按钮
                reform.Size = new Size(point_or_Size(add.size)[0], point_or_Size(add.size)[1]);//设置大小
                reform.Location = new Point(point_or_Size(add.location)[0], point_or_Size(add.location)[1]);//设置按钮位置
                reform.Name = add.Control_type.Trim();//设置名称
                reform.Text = add.Control_state_0_content.Trim();//填充文本
                reform.ForeColor = Color.FromName(add.Control_state_0_colour.Trim());//获取数据库中颜色名称进行设置
                reform.Font = new Font(add.Control_state_0_typeface.Trim(), add.Control_state_0_size.ToInt32(), FontStyle.Bold);//设置字体与大小
                reform.TextAlign = ContentAlignment_1(add.Control_state_0_aligning.Trim());//设置对齐方式
                control.Add(reform);
            }
        }
        private void Load_Add(List<numerical_Class> button_Classes)//填充文本输入类
        {
            //遍历数组
            foreach (numerical_Class add in button_Classes)
            {
                SkinTextBox_reform reform = new SkinTextBox_reform();//实例化按钮
                reform.Size = new Size(point_or_Size(add.size)[0], point_or_Size(add.size)[1]);//设置大小
                reform.Location = new Point(point_or_Size(add.location)[0], point_or_Size(add.location)[1]);//设置按钮位置
                reform.Name = add.Control_type.Trim();//设置名称
                reform.Text = "0";//设置文本
                reform.ForeColor = Color.FromName(add.Control_state_0_colour.Trim());//获取数据库中颜色名称进行设置
                reform.Font = new Font(add.Control_state_0_typeface.Trim(), add.Control_state_0_size.ToInt32(), FontStyle.Bold);//设置字体与大小
                reform.TextAlign = HorizontalAlignment_1(add.Control_state_0_aligning.Trim());//设置对齐方式
                control.Add(reform);
            }
        }
        private void Load_Add(List<Switch_Class> button_Classes)//填充切换开关类
        {
            //遍历数组
            foreach (Switch_Class add in button_Classes)
            {
                Switch_reform reform = new Switch_reform();//实例化按钮
                reform.Size = new Size(point_or_Size(add.size)[0], point_or_Size(add.size)[1]);//设置大小
                reform.Location = new Point(point_or_Size(add.location)[0], point_or_Size(add.location)[1]);//设置按钮位置
                reform.Name = add.Control_type.Trim();//设置名称
                reform.Text = add.Control_state_0_content.Trim();//设置文本
                reform.InActiveColor = Color.FromName(add.Control_state_0_colour.Trim());//获取数据库中颜色名称进行设置
                reform.Font = new Font(add.Control_state_0_typeface.Trim(), add.Control_state_0_size.ToInt32(), FontStyle.Bold);//设置字体与大小
                reform.TextAlign = ContentAlignment_1(add.Control_state_0_aligning.Trim());//设置对齐方式
                reform.BackColor = Color.FromName("Transparent");//填充背景颜色--默认
                control.Add((Switch_reform)reform);
            }
        }
        private void Load_Add(List<GroupBox_Class> button_Classes)//四边框类
        {
            //遍历数组
            foreach (GroupBox_Class add in button_Classes)
            {
                GroupBox_reform reform = new GroupBox_reform();//实例化按钮
                reform.Size = new Size(point_or_Size(add.size)[0], point_or_Size(add.size)[1]);//设置大小
                reform.Location = new Point(point_or_Size(add.location)[0], point_or_Size(add.location)[1]);//设置按钮位置
                reform.Name = add.Control_type.Trim();//设置名称
                reform.Text = add.Control_state_0_content.Trim();//填充文本
                reform.ForeColor = Color.FromName(add.Control_state_0_colour.Trim());//获取数据库中颜色名称进行设置
                reform.Font = new Font(add.Control_state_0_typeface.Trim(), add.Control_state_0_size.ToInt32(),FontStyle.Bold);//设置字体与大小
                reform.FillColor = Color.FromName("Transparent");//填充背景颜色--默认
                reform.TitleAlignment = HorizontalAlignment.Center;//文本显示方式
                reform.Radius = 8;//圆角角度
                reform.RectColor = Color.FromName("Highlight");//边框颜色
                control.Add(reform);
            }
        }
        private void Load_Add(List<LedBulb_Class> button_Classes)//填充指示灯类
        {
            //遍历数组
            foreach (LedBulb_Class add in button_Classes)
            {
                LedBulb_reform reform = new LedBulb_reform();//实例化按钮
                reform.Size = new Size(point_or_Size(add.size)[0], point_or_Size(add.size)[1]);//设置大小
                reform.Location = new Point(point_or_Size(add.location)[0], point_or_Size(add.location)[1]);//设置按钮位置
                reform.Name = add.Control_type.Trim();//设置名称
                reform.Text = add.Control_state_0_content.Trim();//设置文本
                reform.Color = Color.FromName(add.Control_state_0_colour.Trim());//获取数据库中颜色名称进行设置
                reform.Font = new Font(add.Control_state_0_typeface.Trim(), add.Control_state_0_size.ToInt32(), FontStyle.Bold);//设置字体与大小
                reform.BackColor = Color.FromName("Transparent");//填充背景颜色--默认
                control.Add((LedBulb_reform)reform);
            }
        }
        private void Load_Add(List<ImageButton_Class> button_Classes)//填充无图按钮类
        {
            //遍历数组
            foreach (ImageButton_Class add in button_Classes)
            {
                ImageButton_reform reform = new ImageButton_reform();//实例化按钮
                reform.Size = new Size(point_or_Size(add.size)[0], point_or_Size(add.size)[1]);//设置大小
                reform.Location = new Point(point_or_Size(add.location)[0], point_or_Size(add.location)[1]);//设置按钮位置
                reform.Name = add.Control_type.Trim();//设置名称
                reform.Text = add.Control_state_0_content.Trim();//设置文本
                reform.ForeColor = Color.FromName(add.Control_state_0_colour.Trim());//获取数据库中颜色名称进行设置
                reform.Font = new Font(add.Control_state_0_typeface.Trim(), add.Control_state_0_size.ToInt32(), FontStyle.Bold);//设置字体与大小
                reform.TextAlign = ContentAlignment_1(add.Control_state_0_aligning.Trim());//设置对齐方式
                reform.BackColor = Color.FromName("Transparent");//默认颜色
                control.Add((ImageButton_reform)reform);
            }
        }
        private void Load_Add(List<ScrollingText_Class> ScrollingText_Classes)//填充报警条类
        {
            //遍历数组
            foreach (ScrollingText_Class add in ScrollingText_Classes)
            {
                ScrollingText_reform reform = new ScrollingText_reform(Form_event);//实例化报警条
                reform.Size = new Size(point_or_Size(add.size)[0], point_or_Size(add.size)[1]);//设置大小
                reform.Location = new Point(point_or_Size(add.location)[0], point_or_Size(add.location)[1]);//设置按钮位置
                reform.Name = add.Control_type.Trim();//设置名称
                reform.Text = add.Control_state_0_content.Trim();//填充文本
                reform.ForeColor = Color.FromName(add.Control_state_0_colour.Trim());//获取数据库中颜色名称进行设置
                reform.Font = new Font(add.Control_state_0_typeface.Trim(), add.Control_state_0_size.ToInt32(), FontStyle.Bold);//设置字体与大小
                reform.Interval = 100;//文本刷新时间
                control.Add(reform);
            }
        }
        private void Load_Add(List<doughnut_Chart_Class> doughnut_Chart_Classes)//填充报警条类
        {
            //遍历数组
            foreach (doughnut_Chart_Class add in doughnut_Chart_Classes)
            {
                doughnut_Chart_reform reform = new doughnut_Chart_reform();//实例化报警条
                reform.Size = new Size(point_or_Size(add.size)[0], point_or_Size(add.size)[1]);//设置大小
                reform.Location = new Point(point_or_Size(add.location)[0], point_or_Size(add.location)[1]);//设置按钮位置
                reform.Name = add.Control_type.Trim();//设置名称
                reform.doughnut_Chart_Text = add.Control_state_0_content.Trim();//设置文本
                reform.ForeColor = Color.FromName(add.Control_state_0_colour.Trim());//获取数据库中颜色名称进行设置
                reform.doughnut_Chart_Font = new Font(add.Control_state_0_typeface.Trim(), add.Control_state_0_size.ToInt32(), FontStyle.Bold);//设置字体与大小
                control.Add(reform);
            }
        }
        private System.Drawing.ContentAlignment ContentAlignment_1(string Name)//获取字体的对齐方式--按钮-文本类
        {
            System.Drawing.ContentAlignment contentAlignment=System.Drawing.ContentAlignment.MiddleCenter;//定义对齐方式
            switch(Name.Trim())
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
        private PropertyInfo PropertyInfo(string Colo_Name)//获取系统颜色--遍历颜色--弃用保留代码
        {
            PropertyInfo[] propInfoList = typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo c in propInfoList)
            {
                if (Colo_Name.Trim().Equals(c.Name)) return c;//返回数据
            }
            return propInfoList[0];//如果查询失败返回默认
        }
        private int[] point_or_Size(string Name)//分割-来自数据库的-位置与大小数据
        {
            string[] segmentation;//定义分割数组
            try
            {
                segmentation = Name.Split('-');
                return new int[] { Convert.ToInt32(segmentation[0] ?? "81"), Convert.ToInt32(segmentation[1] ?? "31") };
            }
            catch(Exception Err)
            {
                MessageBox.Show(Err.Message, "Err");
                return new int[] { Convert.ToInt32(81), Convert.ToInt32(31) };
            }
        }
    }
}
