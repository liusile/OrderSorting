using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    /// <summary>
    /// 改成放置资源文件
    /// </summary>
    public  class SoundType_Enum
    {
        private static string BasePath = Application.StartupPath + "\\App_Data/Sound\\";
        /// <summary>
        /// 请检查分拣架设备是否连接正确！
        /// </summary>
        public string ConllectError = BasePath + "CollectError.wav";
        /// <summary>
        /// 未找到相匹配的扫描枪，请检查扫描枪是否设置正确！
        /// </summary>
        public string ScanSettingError = BasePath + "ScanSettingError.wav";
        /// <summary>
        /// 您已扫描过此订单
        /// </summary>
        public string OrderScanOver = BasePath + "OrderScanOver.wav";
        /// <summary>
        /// 您之前扫描的订单还未投递,请先投递！
        /// </summary>
        public string OrderOnlyOne = BasePath + "OrderOnlyOne.wav";
        /// <summary>
        /// 警告：订单重复投递，请将投递错误的订单取出重新扫描！
        /// </summary>
        public string RepeatError = BasePath + "RepeatError.wav";
        /// <summary>
        /// 警告：此订单已投递,请找出错误订单重新扫描！
        /// </summary>
        public string RepeatFindError = BasePath + "RepeatFindError.wav";
        /// <summary>
        /// 警告：订单投错格口，请将投错订单取出并重新扫描！
        /// </summary>
        public string LocationError = BasePath + "LocationError.wav";
        /// <summary>
        /// 格口被阻挡，请先将其推入后再扫描！
        /// </summary>
        public string Blocked = BasePath + "Blocked.wav";
        /// <summary>
        /// 请先解锁您的扫描枪！
        /// </summary>
        public string ScanLockError = BasePath + "ScanLockError.wav";
        /// <summary>
        /// 您的扫描枪已解锁！
        /// </summary>
        public string ScanUnLock = BasePath + "ScanUnLock.wav";
        /// <summary>
        /// 请等待其它人解锁！
        /// </summary>
        public string ScanLockWait = BasePath + "ScanLockWait.wav";
        /// <summary>
        /// 获取订单数据失败,请重试!
        /// </summary>
        public string GetOrderError = BasePath + "GetOrderError.wav";
        /// <summary>
        /// 该订单格口正在工作中，请稍后在扫描！
        /// </summary>
        public string LatticeWait = BasePath + "LatticeWait.wav";
        /// <summary>
        /// 订单对应的格口已满
        /// </summary>
        public string LatticeOver = BasePath + "LatticeOver.wav";
        /// <summary>
        /// 创建扫描记录失败,请重试！
        /// </summary>
        public string CreateOrderLogError = BasePath + "Blocked.wav";
        /// <summary>
        /// 警告：出现未知错误，请联系客服并重新开始分拣！
        /// </summary>
        public string NotFindError = BasePath + "NotFindError.wav";
        /// <summary>
        /// 已解除格挡
        /// </summary>
        public string UnBlocked = BasePath + "UnBlocked.wav";
        /// <summary>
        /// 警告：格口被阻挡！
        /// </summary>
        public string Block = BasePath + "Block.wav";
        /// <summary>
        /// 警告：创建扫描投递记录失败！请联系客服
        /// </summary>
        public string CreateOrderSortingLogError = BasePath + "CreateOrderSortingLogError.wav";
        /// <summary>
        /// 警告：投放位置出错，请重新扫描！
        /// </summary>
        public string LocationingError = BasePath + "LocationingError.wav";
        /// <summary>
        /// 警告：创建扫描投递错误记录失败！请联系客服
        /// </summary>
        public string CreateErrorOrderSortingLogError = BasePath + "CreateErrorOrderSortingLogError.wav";
        /// <summary>
        /// 格口正在操作中，请等待操作完成后在打印
        /// </summary>
        public string ButtonWait = BasePath + "ButtonWait.wav";
        /// <summary>
        /// 警告：创建打包记录失败！请重试
        /// </summary>
        public string CreatePackingLogError = BasePath + "CreatePackingLogError.wav";
   
    }
}
