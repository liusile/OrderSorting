using SCB.OrderSorting.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SCB.OrderSorting.Client.Model.SortingOrder_EnumManager;

namespace SCB.OrderSorting.Client.Model
{
    /// <summary>
    /// 线程写信息
    /// </summary>
    public class ThreadWriteMsg
    {
        /// <summary>
        /// 
        /// </summary>

        /// <summary>
        /// 写类型
        /// </summary>
        public ThreadWriteType_Enum WriteType { get; set; }
        /// <summary>
        /// LED
        /// </summary>
        public WriteLED LED { get; set; }
        /// <summary>
        /// 警示灯
        /// </summary>
        public WriteWarningLight WarningLight { get; set; }
        /// <summary>
        /// 重置计数器
        /// </summary>
        public WriteReSetCount ReSetCount { get; set; }
        /// <summary>
        /// 写完休息毫秒数
        /// </summary>
        public int ThreadSleep { get; set; }
        /// <summary>
        /// 最终执行完成的状态
        /// </summary>
        public FinishStatus FinishStatus { get; set; }
    }

    public class FinishStatus
    {
        public SortStatus_Enum SortStatus_Enum { get; set; }

        public List<ThreadSortOrder> ThreadSortOrderList { get; set; }

        public ThreadSortOrder ThreadSortOrder { get; set; }
    }

    /// <summary>
    /// LED
    /// </summary>
    public class WriteLED
    {
        /// <summary>
        /// 从机
        /// </summary>
        public int CabinetId { get; set; }

        public List<LEDChange> LEDChangeList { get; set; }

        /// <summary>
        /// 重置所有LED
        /// </summary>
        public bool ClearAll { get; set; }
    }

    public class LEDChange
    {  /// <summary>
       /// LED灯索引
       /// </summary>
        public int LEDIndex { get; set; }
        /// <summary>
        /// LED灯状态
        /// </summary>
        public LED_Enum LED { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
    }

    /// <summary>
    /// 警示灯
    /// </summary>
    public class WriteWarningLight
    {
        /// <summary>
        /// 警示灯类型（写警示灯有用）
        /// </summary>
        public WarningLight_Enum WarningLight { get; set; }
        /// <summary>
        /// 操作开关（写警示灯有用）
        /// </summary>
        public LightOperStatus_Enum LightOperStatus { get; set; }
    }
    /// <summary>
    /// 重置计数器
    /// </summary>
    public class WriteReSetCount
    {
        /// <summary>
        /// 重置计数器类型（写计数器有用）
        /// </summary>
        public ReSetCounterType_Enum ReSetCounterType { get; set; }
        /// <summary>
        /// 从机
        /// </summary>
        public SlaveConfig Slave { get; set; }
        /// <summary>
        /// 格子序号
        /// </summary>
        public ushort Index { get; set; }
    }
}
