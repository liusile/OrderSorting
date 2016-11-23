using System.Collections.Generic;
namespace SCB.OrderSorting.BLL.Model
{
    public class SystemSetting
    {
        /// <summary>
        /// Modbus设置
        /// </summary>
        public Modbussetting ModbusSetting { get; set; }
        /// <summary>
        /// 架子数(从机数)
        /// </summary>
        public ushort CabinetNumber { get; set; }
        /// <summary>
        /// 临界重量
        /// </summary>
        public decimal CriticalWeight { get; set; }
        /// <summary>
        /// Box重量
        /// </summary>
        public decimal BoxWeight { get; set; }
        /// <summary>
        /// 警示灯从机
        /// </summary>
        public ushort WarningCabinetId { get; set; }
        /// <summary>
        /// 每一个架子的映射地址(从机地址)
        /// </summary>
        public List<SlaveConfig> SlaveConfigs { get; set; }
        /// <summary>
        /// 分拣模式：1按渠道分拣，2按地区分拣，3按地区和渠道分拣
        /// </summary>
        public int SortingPatten { get; set; }
        /// <summary>
        /// 记录保留天数
        /// </summary>
        public int LogStorageDays { get; set; }
        /// <summary>
        /// 是否飞特
        /// </summary>
        public bool IsFlyt { get; set; }
        /// <summary>
        /// 解决方案
        /// </summary>
        public string SortingSolution { get; set; }
    }

    public class Modbussetting
    {
        /// <summary>
        /// SerialPort的PortName
        /// </summary>
        public string PortName { get; set; }
        /// <summary>
        /// SerialPort的BaudRate
        /// </summary>
        public int BaudRate { get; set; }
        /// <summary>
        /// SerialPort的Parity
        /// </summary>
        public int Parity { get; set; }
        /// <summary>
        /// SerialPort的DataBits
        /// </summary>
        public int DataBits { get; set; }
        /// <summary>
        /// SerialPort的StopBits
        /// </summary>
        public int StopBits { get; set; }
        /// <summary>
        /// LED状态起始映射地址
        /// </summary>
        public ushort LEDStartAddress { get; set; }
        /// <summary>
        /// 光栅计数器起始映射地址
        /// </summary>
        public ushort GratingStartAddress { get; set; }
        /// <summary>
        /// 按钮计数器起始映射地址
        /// </summary>
        public ushort ButtonStartAddress { get; set; }
        /// <summary>
        /// 重置光栅计数器映射地址
        /// </summary>
        public ushort ResetGratingStartAddress { get; set; }
        /// <summary>
        /// 重置按钮计数器映射地址
        /// </summary>
        public ushort ResetButtonStartAddress { get; set; }
        
        /// <summary>
        /// 警示红灯映射地址
        /// </summary>
        public ushort WarningRedLightStartAddress { get; set; }
        /// <summary>
        /// 警示绿灯映射地址
        /// </summary>
        public ushort WarningGreenLightStartAddress { get; set; }
        /// <summary>
        /// 警示黄灯映射地址
        /// </summary>
        public ushort WarningYellowLightStartAddress { get; set; }
        /// <summary>
        /// 警示红警示灯蜂鸣器映射地址
        /// </summary>
        public ushort WarningBuzzerStartAddress { get; set; }
        /// <summary>
        /// 每个架子的格数
        /// </summary>
        public ushort NumberOfPoints { get; set; }
    }

    public class SlaveConfig
    {
        /// <summary>
        /// 柜号
        /// </summary>
        public int CabinetId { get; set; }
        /// <summary>
        /// 架子的映射地址
        /// </summary>
        public byte SlaveAddress { get; set; }
        public string TCPHost { get; set; }
        public int TCPPort { get; set; }
    }

}
