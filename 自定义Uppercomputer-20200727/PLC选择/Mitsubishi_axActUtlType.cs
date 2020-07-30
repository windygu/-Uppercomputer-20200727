using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using AxActUtlTypeLib;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using CCWin.SkinClass;

namespace 自定义Uppercomputer_20200727.PLC选择
{
    /// <PLC实现接口--规范定义的方法名称Mitsubishi_realize>
    public interface IPLC_interface//规范定义的方法名称
    {
         bool PLC_ready { get;}//PLC准备好
         int PLCerr_code { get;}//PLC报警代码
         string PLCerr_content { get; }//PLC报警内容
         string PLC_open();//打开PLC
         List<bool>  PLC_read_M_bit(string Name,string id);//读取--位
         List<bool> PLC_write_M_bit(string Name,string id, Button_state button_State);//写入--位
         string PLC_read_D_register(string Name,string id, numerical_format format);//读取--字
         string PLC_write_D_register(string Name,string id,string content, numerical_format format);//读写--字
         List<int> PLC_read_D_register_bit(string id);//读取--字
         List<int> PLC_write_D_register_bit(string id);//读写--字
    }
    class Mitsubishi_axActUtlType : PLC_public_Class, IPLC_interface
    {
        public IPEndPoint IPEndPoint { get; set; }//IP地址
        string pattern;
        static AxActUtlType axActUtlType;//COM组件控件
        static private bool PLC_ready;//内部PLC状态
        static private int PLCerr_code;//内部报警代码
        static private string PLCerr_content;//内部报警内容
        //实现接口的属性
        bool IPLC_interface.PLC_ready { get => PLC_ready; } //PLC状态
        int IPLC_interface.PLCerr_code { get => PLCerr_code; }//PLC报警代码
        string IPLC_interface.PLCerr_content { get => PLCerr_content; }//PLC报警内容
        public Mitsubishi_axActUtlType(IPEndPoint iPEndPoint,string pattern, AxActUtlType axActUtlType)//构造函数---初始化---open
        {
            this.IPEndPoint = iPEndPoint;
            this.pattern = pattern;
            Mitsubishi_axActUtlType.axActUtlType = axActUtlType;
        }
        public Mitsubishi_axActUtlType()//构造函数---多态
        {

        }
        string IPLC_interface.PLC_open()
        {
            try
            {
                //利用Communication DLL库实现
                axActUtlType.ActLogicalStationNumber = Convert.ToInt32(1);//填设置的逻辑站号
                axActUtlType.ActPassword = " ";//填设置的逻辑密码
                if (axActUtlType.Open() > 0)
                {
                    MessageBox.Show("链接PLC异常");
                    PLC_ready = false;//PLC开放异常
                    return "链接PLC异常";//尝试连接PLC，如果连接成功则返回值为0
                }
                else
                    PLC_ready = true;//PLC开放正常
                return "已成功链接到" + this.IPEndPoint.Address;
            }
            catch(Exception e)
            {
                err(e);//异常处理
                return "链接PLC异常";//尝试连接PLC，如果连接成功则返回值为0
            }
        }
        List<bool> IPLC_interface.PLC_read_M_bit(string Name, string id)//读取PLC 位状态 --D_bit这类需要自己在表流获取当前位状态--M这类不需要
        {
            short[] import = new short[2];//寄存器
            string[] segmentation = Name.Split('_');//分割字符串
            string[] segmentation_1 = id.Split('.');//分割字符串
            axActUtlType.ReadDeviceBlock2((segmentation.Length>1? segmentation[0]+ segmentation_1[0]: Name+id), import.Length, out import[0]);//获取三菱PLC输入状态
            //判断是否D-bit类型
            if (segmentation_1.Length > 1)
            {
                List<bool> Romv = bit_public(import);//先获取数据
                for (int i = 0; i < segmentation_1[1].ToInt32(); i++) Romv.RemoveAt(i);//移除不需要的数据
                return Romv;//返回数据
            }
            else
            {
                if(import[0]==-1)return new List<bool>(){ true};//如果读取数据溢出-负数直接返回
                return bit_public(import);//返回数据
            }
        }
        List<bool> IPLC_interface.PLC_write_M_bit(string Name, string id, Button_state button_State)//写入PLC 位状态 --D_bit这类需要自己在表流获取当前位状态--M这类不需要
        {
            short[] import_1 = new short[2];//寄存器
            string[] segmentation = Name.Split('_');//分割字符串
            string[] segmentation_1 = id.Split('.');//分割字符串
            axActUtlType.ReadDeviceBlock2((segmentation.Length > 1 ? segmentation[0] + segmentation_1[0] : Name + id), 1, out import_1[0]);//先读三菱PLC输入状态
            bool[] state = new bool[16];
            state=ConvertIntToBoolArray(import_1[0],16);
            if (segmentation_1.Length > 1)//寄存器指定bit处理方式
            {
                Array.Reverse(state);//先翻转数据--让数据填充
                state[segmentation_1[1].ToInt32()] = true;
                Array.Reverse(state);//填充完毕翻转数据打包--int
            }
            else
                state[15] = Convert.ToBoolean((int)button_State);                       
            import_1[0] =(short)ConvertBoolArrayToInt(state);//BOOL转INT
            axActUtlType.WriteDeviceBlock2((segmentation.Length > 1 ? segmentation[0] + segmentation_1[0] : Name + id), import_1.Length, ref import_1[0]);//写入三菱PLC输入状态
            return bit_public(import_1);//返回数据
        }
        string IPLC_interface.PLC_read_D_register(string Name,string id, numerical_format format)//读寄存器--转换相应类型
        {
            short[] import = new short[2];//寄存器
            string[] segmentation = Name.Split('_');//分割字符串
            string[] segmentation_1 = id.Split('.');//分割字符串
            axActUtlType.ReadDeviceBlock2((segmentation.Length > 1 ? segmentation[0] + segmentation_1[0] : Name + id), import.Length, out import[0]);//获取三菱PLC寄存器状态
            //判断是否其他类型
            return Mitsubishi_to_numerical(new int[] { import[0], import[1] }, format); //返回数据           
        }
        string IPLC_interface.PLC_write_D_register(string Name,string id, string content, numerical_format format)//写寄存器--转换类型--并且写入
        {        
            string[] segmentation = Name.Split('_');//分割字符串
            string[] segmentation_1 = id.Split('.');//分割字符串
            short[] import = numerical_to_Mitsubishi(content, format);
            axActUtlType.WriteDeviceBlock2((segmentation.Length > 1 ? segmentation[0] + segmentation_1[0] : Name + id), import.Length, ref import[0]);//写入三菱PLC输入状态
            return content;       
        }
        List<int> IPLC_interface.PLC_read_D_register_bit(string id)
        {
            return new List<int>() { 1 };
        }
        List<int> IPLC_interface.PLC_write_D_register_bit(string id)
        {
            return new List<int>() { 1 };
        }
        private void err(Exception e)
        {
            PLC_ready = false;//PLC开放异常
            PLCerr_code = e.HResult;
            PLCerr_content = e.Message;
            MessageBox.Show("链接PLC异常");
        }
    }
}
