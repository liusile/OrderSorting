using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCB.OrderSorting.Client.Model
{
    /// <summary>
    /// 分拣架枚举管理类
    /// </summary>
    public class SortingOrder_EnumManager
    {
        /// <summary>
        /// 分拣状态
        /// </summary>
        public enum SortStatus_Enum
        {
            /// <summary>
            /// 重复投递
            /// </summary>
            RepeatError = 1,
            /// <summary>
            /// 未扫描且投递
            /// </summary>
            NotScanAndPut = 2,
            /// <summary>
            /// 格档
            /// </summary>
            Blocked = 3,
            /// <summary>
            /// 未开始投递
            /// </summary>
            None = 4,
            /// <summary>
            /// 投递错误
            /// </summary>
            LocationError = 5,
            /// <summary>
            /// 投递成功
            /// </summary>
            Success = 6,
            /// <summary>
            /// 待投递
            /// </summary>
            WaitPut = 7,
            /// <summary>
            /// 已满
            /// </summary>
            OverWeight = 8,
            /// <summary>
            /// 未确定
            /// </summary>
            NotComfirm = 9,
            /// <summary>
            ///打印
            /// </summary>
            Print = 10,
            /// <summary>
            /// 返回之前的状态
            /// </summary>
            BackPreStatus = 11,
            /// <summary>
            /// 暂停
            /// </summary>
            Stop=12,
            /// <summary>
            /// 投递进行中
            /// </summary>
            WaitPuting = 13

        }
        /// <summary>
        /// 重置光栅计数器类型
        /// </summary>
        public enum ReSetCounterType_Enum
        {
            /// <summary>
            /// 光栅
            /// </summary>
            Grating = 1,
            /// <summary>
            /// 按钮
            /// </summary>
            Button = 2,
            /// <summary>
            /// 光栅和按钮
            /// </summary>
            Grating2Button = 3,

        }
        /// <summary>
        /// 线程写类型
        /// </summary>
        public enum ThreadWriteType_Enum
        {
            /// <summary>
            /// 计数器
            /// </summary>
            ReSetCount = 1,
            /// <summary>
            /// 警示灯
            /// </summary>
            WarningLight = 2,
            /// <summary>
            /// LED灯
            /// </summary>
            LED = 3

        }
        /// <summary>
        /// 线程读类型
        /// </summary>
        public enum ThreadReadType_Enum
        {
            /// <summary>
            /// 光栅
            /// </summary>
            Grating = 1,
            /// <summary>
            /// 按钮
            /// </summary>
            Button = 2
        }
        /// <summary>
        /// LED枚举
        /// </summary>
        public enum LED_Enum : ushort
        {
            /// <summary>
            /// 绿灯
            /// </summary>
            Green = 0,
            /// <summary>
            /// 红灯但不闪烁
            /// </summary>
            Red = 1,
            /// <summary>
            /// 黄灯
            /// </summary>
            Yellow=2,
            /// <summary>
            /// 黄灯且闪烁
            /// </summary>
            YellowFlash = 3,
            /// <summary>
            /// 熄灭
            /// </summary>
            None = 4
        }
        /// <summary>
        /// 警示灯枚举
        /// </summary>
        public enum WarningLight_Enum : byte
        {
            /// <summary>
            /// 红灯
            /// </summary>
            Red = 1,
            /// <summary>
            /// 绿灯
            /// </summary>
            Green = 2,
            /// <summary>
            /// 黄灯
            /// </summary>
            Yellow = 3,
            /// <summary>
            /// 蜂鸣声
            /// </summary>
            WarningSound = 4
        }
        /// <summary>
        /// 灯的开闭状态
        /// </summary>
        public enum LightOperStatus_Enum : ushort
        {
            /// <summary>
            /// 开
            /// </summary>
            On = 1,
            /// <summary>
            /// 关
            /// </summary>
            Off = 0
        }
    }
}
