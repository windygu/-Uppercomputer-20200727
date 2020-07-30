using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 自定义Uppercomputer_20200727.PLC选择
{
    /// <PLC硬件选择>
    /// <PLC选择枚举> 
    public enum PLC
    {
        Mitsubishi,
        Siemens,
        Modbus_TCP
    }
    /// <PLC各可访问软元件>
    public enum Mitsubishi_bit
    { 
        LCS,LCC,SM,X,Y,M,L,F,B,TS,SS,SC,CS,CC,SB,S,D_Bit,SD_Bit,R_Bit,SW_Bit,W_Bit
    }
    public enum Mitsubishi_D
    {
        LCN,LZ,SD,D,R,W,TN,SN,CN,SW,Z
    }
    public enum Siemens_bit
    {
       I, Q, M
    }
    public enum Siemens_D
    {
       DB
    }
    public enum Modbus_TCP_bit
    {
        LCS, LCC, SM, X, Y, M, L, F, B, TS, SS, SC, CS, CC, SB, S, D_Bit
    }
    public enum Modbus_TCP_D
    {
        LCN, LZ, SD, D, R, W, TN, SN, CN
    }
    /// <PLC--按钮状态>
    public enum Button_state
    {
        Off,ON
    }
    public enum numerical_format
    {
        BCD_16_Bit, BCD_32_Bit, Hex_16_Bit, Hex_32_Bit, Binary_16_Bit, Binary_32_Bit, Unsigned_16_Bit, Signed_16_Bit
            , Unsigned_32_Bit, Signed_32_Bit, Float_32_Bit
    }
    public enum numerical_type
    {
        数值
    }
    class PLCselect
    {

    }
}
