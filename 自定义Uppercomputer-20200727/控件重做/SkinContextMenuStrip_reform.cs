using CCWin.SkinControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 自定义Uppercomputer_20200727.EF实体模型;
using 自定义Uppercomputer_20200727.修改参数界面;
using 自定义Uppercomputer_20200727.修改参数界面.doughnut_Chart图形控件参数;
using 自定义Uppercomputer_20200727.修改参数界面.GroupBox四边形方框;
using 自定义Uppercomputer_20200727.修改参数界面.ImageButton按钮参数;
using 自定义Uppercomputer_20200727.修改参数界面.LedBulb_指示灯参数;
using 自定义Uppercomputer_20200727.修改参数界面.报警条参数;

namespace 自定义Uppercomputer_20200727.控件重做
{
    class SkinContextMenuStrip_reform:SkinContextMenuStrip
    {
        /// <本类主要重写右键菜单的属性-事件-等>
        public string SkinContextMenuStrip_Button_ID { get; set; }//定义控件的ID
        public string SkinContextMenuStrip_Button_type { get; set; }//定义控件的类型
        public object all_purpose { get; set; }//定义通用类型_修改参数传递使用
        public SkinContextMenuStrip_reform()//构造函数
        {
            /// <写入相应参数>
            ToolStripMenuItem toolStrip = new ToolStripMenuItem();
            toolStrip.Text = "修改参数";
            this.Items.Add(toolStrip);
            toolStrip.Click += toolStrip_Click_reform;//注册修改参数事件
            /// <移除控件>
            ToolStripMenuItem toolStrip_1 = new ToolStripMenuItem();
            toolStrip_1.Text = "移除控件";
            this.Items.Add(toolStrip_1);
            toolStrip_1.Click += toolStrip_Click_reform_1;//注册修改参数事件
            ///<把控件移到最上层>
            ToolStripMenuItem toolStrip_2 = new ToolStripMenuItem();
            toolStrip_2.Text = "控件最上层";
            this.Items.Add(toolStrip_2);
            toolStrip_2.Click += stratosphere;//注册修改参数事件
            ///<把控件移到最下层>
            ToolStripMenuItem toolStrip_3 = new ToolStripMenuItem();
            toolStrip_3.Text = "控件最下层";
            this.Items.Add(toolStrip_3);
            toolStrip_3.Click += orlop;//注册修改参数事件

        }
        /// <本方法重写右键点击菜单事件--触发相应修改参数操作>
        private void toolStrip_Click_reform(object sender, EventArgs e)
        {
            switch (SkinContextMenuStrip_Button_type)//判断传递父级的类型
            {
                case "Button_reform":
                    Modification_Button modification = new Modification_Button(SkinContextMenuStrip_Button_ID,this.all_purpose);//弹出修改参数传递
                    modification.ShowDialog();//弹出修改参数窗口
                    break;
                case "SkinLabel_reform":
                    Modification_label modification_Label = new Modification_label(SkinContextMenuStrip_Button_ID, this.all_purpose);//弹出修改参数传递
                    modification_Label.ShowDialog();//弹出修改参数窗口
                    break;
                case "SkinTextBox_reform":
                    Modification_numerical modification_Numerical = new Modification_numerical(SkinContextMenuStrip_Button_ID, this.all_purpose);//弹出修改参数传递
                    modification_Numerical.ShowDialog();//弹出修改参数窗口
                    ((SkinTextBox_reform)all_purpose).Text = "0";//修改完成初始化数据
                    break;
                case "SkinPictureBox_reform":
                    Modification_picture modification_Picture = new Modification_picture(SkinContextMenuStrip_Button_ID, this.all_purpose);//弹出修改参数传递
                    modification_Picture.ShowDialog();//弹出修改参数窗口
                    break;
                case "Switch_reform":
                    Modification_Switch Switch = new Modification_Switch(SkinContextMenuStrip_Button_ID, this.all_purpose);//弹出修改参数传递
                    Switch.ShowDialog();//弹出修改参数窗口
                    break;
                case "LedBulb_reform":
                    Modification_Ledbulb Ledbulb = new Modification_Ledbulb(SkinContextMenuStrip_Button_ID, this.all_purpose);//弹出修改参数传递
                    Ledbulb.ShowDialog();//弹出修改参数窗口
                    break;
                case "GroupBox_reform":
                    Modification_GroupBox GroupBox = new Modification_GroupBox(SkinContextMenuStrip_Button_ID, this.all_purpose);//弹出修改参数传递
                    GroupBox.ShowDialog();//弹出修改参数窗口
                    break;
                case "ImageButton_reform":
                    Modification_ImageButton ImageButton = new Modification_ImageButton(SkinContextMenuStrip_Button_ID, this.all_purpose);//弹出修改参数传递
                    ImageButton.ShowDialog();//弹出修改参数窗口
                    break;
                case "ScrollingText_reform":
                    Modification_ScrollingText ScrollingText_reform = new Modification_ScrollingText(SkinContextMenuStrip_Button_ID, this.all_purpose);//弹出修改参数传递
                    ScrollingText_reform.ShowDialog();//弹出修改参数窗口
                    break;
                case "doughnut_Chart_reform":
                    Modification_doughnut_Chart doughnut_Chart_reform = new Modification_doughnut_Chart(SkinContextMenuStrip_Button_ID, this.all_purpose);//弹出修改参数传递
                    doughnut_Chart_reform.ShowDialog();//弹出修改参数窗口
                    break;
            }
        }
        /// <本方法重写右键点击菜单事件--触发移除控件操作>
        private void toolStrip_Click_reform_1(object sender, EventArgs e)
        {
            switch(SkinContextMenuStrip_Button_type)//判断传递父级的类型
            {
                case "Button_reform":
                    if (MessageBox.Show("确定要删除" + ((Button_reform)all_purpose).Name.Trim() + "吗？", "错误：", MessageBoxButtons.YesNo) == DialogResult.No) return;
                    ((Button_reform)all_purpose).Visible = false;//隐藏控件
                    Button_EF button_EF = new Button_EF();//实例化EF对象
                    button_EF.Button_Parameter_delete(SkinContextMenuStrip_Button_ID + "-" + ((Button_reform)all_purpose).Name);//执行SQL删除操作
                    break;
                case "SkinLabel_reform":
                    if (MessageBox.Show("确定要删除" + ((SkinLabel)all_purpose).Name.Trim() + "吗？", "错误：", MessageBoxButtons.YesNo) == DialogResult.No) return;
                    ((SkinLabel)all_purpose).Visible = false;//隐藏控件
                    label_EF label_EF = new label_EF();//实例化EF对象
                    label_EF.label_Parameter_delete(SkinContextMenuStrip_Button_ID + "-" + ((SkinLabel)all_purpose).Name);//执行SQL删除操作
                    break;
                case "SkinTextBox_reform":
                    if (MessageBox.Show("确定要删除" + ((SkinTextBox_reform)all_purpose).Name.Trim() + "吗？", "错误：", MessageBoxButtons.YesNo) == DialogResult.No) return;
                    ((SkinTextBox_reform)all_purpose).Visible = false;//隐藏控件
                    numerical_EF numerical_EF = new numerical_EF();//实例化EF对象
                    numerical_EF.numerical_Parameter_delete(SkinContextMenuStrip_Button_ID + "- " + ((SkinTextBox_reform)all_purpose).Name);//执行SQL删除操作
                    break;
                case "SkinPictureBox_reform":
                    if (MessageBox.Show("确定要删除" + ((SkinPictureBox)all_purpose).Name.Trim() + "吗？", "错误：", MessageBoxButtons.YesNo) == DialogResult.No) return;
                    ((SkinPictureBox)all_purpose).Visible = false;//隐藏控件
                    picture_EF picture_EF = new picture_EF();//实例化EF对象
                    picture_EF.picture_Parameter_delete(SkinContextMenuStrip_Button_ID + "-" + ((SkinPictureBox)all_purpose).Name);//执行SQL删除操作
                    break;
                case "Switch_reform":
                    if (MessageBox.Show("确定要删除" + ((Switch_reform)all_purpose).Name.Trim() + "吗？", "错误：", MessageBoxButtons.YesNo) == DialogResult.No) return;
                    ((Switch_reform)all_purpose).Visible = false;//隐藏控件
                    Switch_EF Switch_EF = new Switch_EF();//实例化EF对象
                    Switch_EF.Button_Parameter_delete(SkinContextMenuStrip_Button_ID + "-" + ((Switch_reform)all_purpose).Name);//执行SQL删除操作
                    break;
                case "LedBulb_reform":
                    if (MessageBox.Show("确定要删除" + ((LedBulb_reform)all_purpose).Name.Trim() + "吗？", "错误：", MessageBoxButtons.YesNo) == DialogResult.No) return;
                    ((LedBulb_reform)all_purpose).Visible = false;//隐藏控件
                    LedBulb_EF LedBulb_EF = new LedBulb_EF();//实例化EF对象
                    LedBulb_EF.Button_Parameter_delete(SkinContextMenuStrip_Button_ID + "-" + ((LedBulb_reform)all_purpose).Name);//执行SQL删除操作
                    break;
                case "GroupBox_reform":
                    if (MessageBox.Show("确定要删除" + ((GroupBox_reform)all_purpose).Name.Trim() + "吗？", "错误：", MessageBoxButtons.YesNo) == DialogResult.No) return;
                    ((GroupBox_reform)all_purpose).Visible = false;//隐藏控件
                    GroupBox_EF GroupBox_EF = new GroupBox_EF();//实例化EF对象
                    GroupBox_EF.GroupBox_Parameter_delete(SkinContextMenuStrip_Button_ID + "-" + ((GroupBox_reform)all_purpose).Name);//执行SQL删除操作
                    break;
                case "ImageButton_reform":
                    if (MessageBox.Show("确定要删除" + ((ImageButton_reform)all_purpose).Name.Trim() + "吗？", "错误：", MessageBoxButtons.YesNo) == DialogResult.No) return;
                    ((ImageButton_reform)all_purpose).Visible = false;//隐藏控件
                    ImageButton_EF ImageButton_EF = new ImageButton_EF();//实例化EF对象
                    ImageButton_EF.Button_Parameter_delete(SkinContextMenuStrip_Button_ID + "-" + ((ImageButton_reform)all_purpose).Name);//执行SQL删除操作
                    break;
                case "ScrollingText_reform":
                    if (MessageBox.Show("确定要删除" + ((ScrollingText_reform)all_purpose).Name.Trim() + "吗？", "错误：", MessageBoxButtons.YesNo) == DialogResult.No) return;
                    ((ScrollingText_reform)all_purpose).Visible = false;//隐藏控件
                    ScrollingText_EF ScrollingText_EF = new ScrollingText_EF();//实例化EF对象
                    ScrollingText_EF.ScrollingText_Parameter_delete(SkinContextMenuStrip_Button_ID + "-" + ((ScrollingText_reform)all_purpose).Name);//执行SQL删除操作
                    break;
                case "doughnut_Chart_reform":
                    if (MessageBox.Show("确定要删除" + ((doughnut_Chart_reform)all_purpose).Name.Trim() + "吗？", "错误：", MessageBoxButtons.YesNo) == DialogResult.No) return;
                    ((doughnut_Chart_reform)all_purpose).Visible = false;//隐藏控件
                    doughnut_Chart_EF doughnut_Chart_EF = new doughnut_Chart_EF();//实例化EF对象
                    doughnut_Chart_EF.doughnut_Chart_Parameter_delete(SkinContextMenuStrip_Button_ID + "- " + ((doughnut_Chart_reform)all_purpose).Name);//执行SQL删除操作
                    break;
            }
        }
        /// <summary>
        /// 控件最上层选择
        /// </summary>
        private void stratosphere(object send,EventArgs e)
        {
            Control_layer_EF layer_EF = new Control_layer_EF();//实例化最下层EF查询对象
            switch (SkinContextMenuStrip_Button_type)//判断传递父级的类型
            {
                case "Button_reform":
                    ((Button_reform)all_purpose).BringToFront();//将控件放置所有控件最顶层  
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "-" + ((Button_reform)all_purpose).Name, ((Button_reform)all_purpose).Name,1);
                    break;
                case "SkinLabel_reform":
                    ((SkinLabel_reform)all_purpose).BringToFront();//将控件放置所有控件最顶层     
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "-" + ((SkinLabel_reform)all_purpose).Name, ((SkinLabel_reform)all_purpose).Name,1);
                    break;
                case "SkinTextBox_reform":
                    ((SkinTextBox_reform)all_purpose).BringToFront();//将控件放置所有控件最顶层     
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "- " + ((SkinTextBox_reform)all_purpose).Name, ((SkinTextBox_reform)all_purpose).Name, 1);
                    break;
                case "SkinPictureBox_reform":
                    ((SkinPictureBox_reform)all_purpose).BringToFront();//将控件放置所有控件最顶层   
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "-" + ((SkinPictureBox_reform)all_purpose).Name, ((SkinPictureBox_reform)all_purpose).Name, 1);
                    break;
                case "Switch_reform":
                    ((Switch_reform)all_purpose).BringToFront();//将控件放置所有控件最顶层  
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "-" + ((Switch_reform)all_purpose).Name, ((Switch_reform)all_purpose).Name, 1);
                    break;
                case "LedBulb_reform":
                    ((LedBulb_reform)all_purpose).BringToFront();//将控件放置所有控件最顶层    
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "-" + ((LedBulb_reform)all_purpose).Name, ((LedBulb_reform)all_purpose).Name, 1);
                    break;
                case "GroupBox_reform":
                    ((GroupBox_reform)all_purpose).BringToFront();//将控件放置所有控件最顶层   
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "-" + ((GroupBox_reform)all_purpose).Name, ((GroupBox_reform)all_purpose).Name, 1);
                    break;
                case "ImageButton_reform":
                    ((ImageButton_reform)all_purpose).BringToFront();//将控件放置所有控件最顶层
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "-" + ((ImageButton_reform)all_purpose).Name, ((ImageButton_reform)all_purpose).Name, 1);
                    break;
                case "ScrollingText_reform":
                    ((ScrollingText_reform)all_purpose).BringToFront();//将控件放置所有控件最顶层  
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "-" + ((ScrollingText_reform)all_purpose).Name, ((ScrollingText_reform)all_purpose).Name, 1);
                    break;
                case "doughnut_Chart_reform":
                    ((doughnut_Chart_reform)all_purpose).BringToFront();//将控件放置所有控件最顶层 
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "- " + ((doughnut_Chart_reform)all_purpose).Name, ((doughnut_Chart_reform)all_purpose).Name, 1);
                    break;
            }
        }
        /// <summary>
        /// 控件最下层选择
        /// </summary>
        private void orlop(object send, EventArgs e)
        {
            Control_layer_EF layer_EF = new Control_layer_EF();//实例化最下层EF查询对象
            switch (SkinContextMenuStrip_Button_type)//判断传递父级的类型
            {
                case "Button_reform":
                    ((Button_reform)all_purpose).SendToBack();//将控件放置所有控件最底层   
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "-" + ((Button_reform)all_purpose).Name, ((Button_reform)all_purpose).Name,0);
                    break;
                case "SkinLabel_reform":
                    ((SkinLabel_reform)all_purpose).SendToBack();//将控件放置所有控件最底层   
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "-" + ((SkinLabel_reform)all_purpose).Name, ((SkinLabel_reform)all_purpose).Name,0);
                    break;
                case "SkinTextBox_reform":
                    ((SkinTextBox_reform)all_purpose).SendToBack();//将控件放置所有控件最底层    
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "- " + ((SkinTextBox_reform)all_purpose).Name, ((SkinTextBox_reform)all_purpose).Name,0);
                    break;
                case "SkinPictureBox_reform":
                    ((SkinPictureBox_reform)all_purpose).SendToBack();//将控件放置所有控件最底层  
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "-" + ((SkinPictureBox_reform)all_purpose).Name, ((SkinPictureBox_reform)all_purpose).Name,0);
                    break;
                case "Switch_reform":
                    ((Switch_reform)all_purpose).SendToBack();//将控件放置所有控件最底层   
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "-" + ((Switch_reform)all_purpose).Name, ((Switch_reform)all_purpose).Name,0);
                    break;
                case "LedBulb_reform":
                    ((LedBulb_reform)all_purpose).SendToBack();//将控件放置所有控件最底层 
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "-" + ((LedBulb_reform)all_purpose).Name, ((LedBulb_reform)all_purpose).Name,0);
                    break;
                case "GroupBox_reform":
                    ((GroupBox_reform)all_purpose).SendToBack();//将控件放置所有控件最底层  
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "-" + ((GroupBox_reform)all_purpose).Name, ((GroupBox_reform)all_purpose).Name,0);
                    break;
                case "ImageButton_reform":
                    ((ImageButton_reform)all_purpose).SendToBack();//将控件放置所有控件最底层 
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "-" + ((ImageButton_reform)all_purpose).Name, ((ImageButton_reform)all_purpose).Name,0);
                    break;
                case "ScrollingText_reform":
                    ((ScrollingText_reform)all_purpose).SendToBack();//将控件放置所有控件最底层   
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "-" + ((ScrollingText_reform)all_purpose).Name, ((ScrollingText_reform)all_purpose).Name,0);
                    break;
                case "doughnut_Chart_reform":
                    ((doughnut_Chart_reform)all_purpose).SendToBack();//将控件放置所有控件最底层   
                    layer_EF.all_Parameter_Query_Add(SkinContextMenuStrip_Button_ID + "- " + ((doughnut_Chart_reform)all_purpose).Name, ((doughnut_Chart_reform)all_purpose).Name,0);
                    break;
            }
        }
        ~SkinContextMenuStrip_reform()
        {

        }
    }
}
