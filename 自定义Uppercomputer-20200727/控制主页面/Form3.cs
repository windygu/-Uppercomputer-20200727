using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 自定义Uppercomputer_20200727.控制主页面
{
    public partial class Form3 : Form2
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("该窗口是主窗口是否要退出程序？", "Err", MessageBoxButtons.YesNo) == DialogResult.Yes)
                System.Environment.Exit(0);
            else
                e.Cancel = true;//取消
        }
    }
}
