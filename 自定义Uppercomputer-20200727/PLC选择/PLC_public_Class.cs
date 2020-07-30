﻿using CCWin.SkinClass;
using HslCommunication.Profinet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 自定义Uppercomputer_20200727.PLC选择
{
    //本类是共用类 
    class PLC_public_Class
    {
        public static bool PLC_busy;//指示着其他用户正在访问
        public bool[] ConvertIntToBoolArray(int result, int len)//int转B00L
        {

            if (len > 32 || len < 0)
            {
                Console.WriteLine("bool数组长度应该在0到32之间。");
            }

            bool[] barray2 = new bool[len];

            for (int i = 0; i < len; i++)
            {
                barray2[len - i - 1] = ((result >> i) % 2) == 1;
            }
            return barray2;
        }
        public List<bool> bit_public(short[] import)//处理bit位数据 公有方法
        {
            List<bool> data = new List<bool>();//创建表
            bool[] Inputtheresults = ConvertIntToBoolArray(import[0], 16);//强转BOOL类型
            Array.Reverse(Inputtheresults);//翻转数组
            foreach (bool i in Inputtheresults) data.Add(i);//填充表
            return data;
        }
        public int merge(short[] Resistancedata) //从新合并INT类型
        {
            //合并PLC电阻数据操作
            byte[] transform = BitConverter.GetBytes(Resistancedata[0]);
            byte[] transform_1 = BitConverter.GetBytes(Resistancedata[1]);
            byte[] taran = new byte[transform.Length + transform_1.Length];
            for (int i = 0; i < taran.Length; i++)
            {
                if (i < transform.Length) taran[i] = transform[i]; else taran[i] = transform_1[i - transform.Length];
            }
            return BitConverter.ToInt32(taran,0);
        }
        public Double merge_to_Double(short[] Resistancedata) //从新合并INT类型
        {
            //合并PLC电阻数据操作
            byte[] transform = BitConverter.GetBytes(Resistancedata[0]);
            byte[] transform_1 = BitConverter.GetBytes(Resistancedata[1]);
            byte[] taran = new byte[transform.Length + transform_1.Length];
            for (int i = 0; i < taran.Length; i++)
            {
                if (i < transform.Length) taran[i] = transform[i]; else taran[i] = transform_1[i - transform.Length];
            }
            return BitConverter.ToSingle(taran, 0);//转换成浮点小数
        }
        //byte数组变为 float数值
        private  float ToFloat(byte[] data)//---不推荐代码--
        {
            unsafe//开启不安全代码
            {
                float a = 0.0F;
                byte i;
                byte[] x = data;
                void* pf;
                fixed (byte* px = x)
                {
                    pf = &a;
                    for (i = 0; i < data.Length; i++)
                    {
                        *((byte*)pf + i) = *(px + i);
                    }
                }
                return a;
            }
        }
        public int ConvertBoolArrayToInt(bool[] barray)//BOOL转INT类型
        {
            int result = 0;
            if (barray != null)
            {
                int len = barray.Length;

                if (len < 33)
                {
                    foreach (bool b in barray)
                    {
                        result = (result << 1) + (b ? 1 : 0);
                    }
                }
                else
                {
                    Console.WriteLine("bool数组长度大于32，整数只有32位。");
                }
            }
            else
            {
                Console.WriteLine("bool数组为空。");
            }
            return result;
        }
        public string Mitsubishi_to_numerical(int[] Name, numerical_format format)//转换类型---shorot--string
        {
            string numerical = Name[0].ToString();//初始化寄存器
            switch (format)
            {
                case numerical_format.BCD_16_Bit:
                    numerical = Hex_to_BCD(Name[0]).ToString();//转换成BCD吗
                    break;
                case numerical_format.BCD_32_Bit:
                    numerical = Hex_to_BCD(merge(new short[] { (short)Name[0], (short)Name[1] })).ToString();//转换成BCD吗
                    break;
                case numerical_format.Binary_16_Bit:
                    numerical = Convert.ToString(Name[0],2);//转换成16位二进制数
                    break;
                case numerical_format.Binary_32_Bit:
                    numerical = Convert.ToString(merge(new short[] { (short)Name[0], (short)Name[1] }),2);//转换成32位二进制数
                    break;
                case numerical_format.Float_32_Bit:
                    numerical = merge_to_Double(new short[] { (short)Name[0], (short)Name[1] }).ToString();//转换成浮点小数
                    break;
                case numerical_format.Hex_16_Bit:
                    numerical = Name[0].ToString("X");//16进制转换--16位
                    break;
                case numerical_format.Hex_32_Bit:
                    numerical = merge(new short[] { (short)Name[0], (short)Name[1] }).ToString("X");//16进制转换-32位
                    break;
                case numerical_format.Signed_16_Bit:
                    numerical = Convert.ToInt16(Name[0]).ToString();//有符号-16位
                    break;
                case numerical_format.Signed_32_Bit:
                    numerical = Convert.ToInt32(merge(new short[] { (short)Name[0], (short)Name[1] })).ToString();//有符号 32位
                    break;
                case numerical_format.Unsigned_16_Bit:
                    numerical = Convert.ToUInt16(Name[0]).ToString();//无符号-16位
                    break;
                case numerical_format.Unsigned_32_Bit:
                    numerical = Convert.ToUInt32(merge(new short[] { (short)Name[0], (short)Name[1] })).ToString();//无符号-32位
                    break;
            }
            return numerical;//返回数据
        }
        public short[] numerical_to_Mitsubishi(string Name, numerical_format format)//转换类型---string--shorot
        {
            short[] numerical =new short[32];//初始化寄存器
            switch (format)
            {
                case numerical_format.BCD_16_Bit:
                     numerical = bit32_to_bit32(stringToShort(Name, numerical));//string-转short
                    break;
                case numerical_format.BCD_32_Bit:
                    numerical = stringToShort(Name, numerical);//string-转short
                    break;
                case numerical_format.Binary_16_Bit:
                    numerical = bit32_to_bit32(stringToShort(Convert.ToInt32(Name,2).ToString(), numerical));//16位二进制数-转short
                    break;
                case numerical_format.Binary_32_Bit:
                    numerical = stringToShort(Convert.ToInt32(Name, 2).ToString(), numerical);//16位二进制数-转short
                    break;
                case numerical_format.Float_32_Bit:
                    numerical = float_to_short(Convert.ToSingle(Name));//浮点小数-转short
                    break;
                case numerical_format.Hex_16_Bit:
                    numerical = bit32_to_bit32(stringToShort(Convert.ToInt32(Name,16).ToString(), numerical));//16位二进制数-转short
                    break;
                case numerical_format.Hex_32_Bit:
                    numerical = stringToShort(Convert.ToInt32(Name,16).ToString(),  numerical);//16位二进制数-转short
                    break;
                case numerical_format.Signed_16_Bit:
                    numerical = bit32_to_bit32(stringToShort(Convert.ToInt32(Name).ToString(), numerical));//16位二进制数-转short
                    break;
                case numerical_format.Signed_32_Bit:
                    numerical = stringToShort(Convert.ToInt32(Name).ToString(), numerical);//16位二进制数-转short
                    break;
                case numerical_format.Unsigned_16_Bit:
                    numerical = bit32_to_bit32(stringToShort(Convert.ToInt32(Name).ToString(), numerical));//16位二进制数-转short
                    break;
                case numerical_format.Unsigned_32_Bit:
                    numerical = stringToShort(Convert.ToInt32(Name).ToString(), numerical);//16位二进制数-转short
                    break;
            }
            return numerical;//返回数据
        }
        public short[] float_to_short(float Name)//float-转-short
        {
            byte[] data = new byte[32];//寄存器
            data = BitConverter.GetBytes(Name);//解析数据
            return new short[] { BitConverter.ToInt16(data, 0), BitConverter.ToInt16(data, 2) };//合并数据返回数据
        }
        public short[] stringToShort(string inString, short[] outShort)//string-to-short
        {
            byte[] data = new byte[32];//寄存器
            data = BitConverter.GetBytes(inString.ToInt32());//解析数据
            return new short[] { BitConverter.ToInt16(data, 0), BitConverter.ToInt16(data, 2) };//合并数据返回数据
        }
        public short[] bit32_to_bit32(short[] Name)//移除多余的short
        {
            return new short[] { Name[0] };//返回数据
        }
        public int Hex_to_BCD(int d)//hex-to-bcd
        {
            int de = ((d >> 8) * 100) | ((d >> 4) * 10) | (d & 0x0f);
            return de;
        }
    }
}
