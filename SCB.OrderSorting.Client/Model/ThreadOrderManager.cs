using SCB.OrderSorting.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SCB.OrderSorting.Client.Model.SortingOrder_EnumManager;

namespace SCB.OrderSorting.Client.Model
{
    /// <summary>
    /// 操作中的订单信息
    /// </summary>
    public class ThreadSortOrderManager
    {
        List<KeyValuePair<string, ThreadSortOrder>> ThreadSortOrderList = new List<KeyValuePair<string, ThreadSortOrder>>();
        public void Add(string key)
        {
            ThreadSortOrderList.Add(new KeyValuePair<string, ThreadSortOrder>(key, new ThreadSortOrder()));
        }
        public void Add(string key, ThreadSortOrder ThreadSortOrder)
        {
            ThreadSortOrderList.Add(new KeyValuePair<string, ThreadSortOrder>(key, ThreadSortOrder));
        }
        public List<ThreadSortOrder> GetOther(string key)
        {
            return ThreadSortOrderList.FindAll(o => o.Key != key).Select(o=>o.Value).ToList();
        }
        public ThreadSortOrder Get(string key)
        {
            return ThreadSortOrderList.Find(o => o.Key == key).Value;
        }
        public List<ThreadSortOrder> Get()
        {
            return ThreadSortOrderList.Select(o => o.Value).ToList();
        }
        public void SetResultLatticeAll(LatticeSetting LatticeSetting)
        {
            ThreadSortOrderList.ForEach(o => o.Value.ResultLattice = LatticeSetting);
        }
    }
    public class ThreadSortOrder
    {
        /// <summary>
        /// 当前订单分拣状态
        /// </summary>
        public SortStatus_Enum SortStatus { get; set; }
        /// <summary>
        /// 之前订单分拣状态
        /// </summary>
        public SortStatus_Enum PreSortStatus { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string SortOrderNo { get; set; }
        /// <summary>
        /// 订单信息
        /// </summary>
        public OrderInfo OrderInfo { get; set; }
        /// <summary>
        /// 目标格口
        /// </summary>
        public List<LatticeSetting> TargetLattice { get; set; }
        /// <summary>
        /// 结果格口
        /// </summary>
        public LatticeSetting ResultLattice { get; set; }
        /// <summary>
        /// 从机号
        /// </summary>
        public int CabinetId { get; set; }
        /// <summary>
        /// 待投颜色
        /// </summary>
        public LED_Enum WaitPutColor { get; set; }
        /// <summary>
        /// 是否暂停
        /// </summary>
        public bool IsStop { get; set; }
    }

}
