using SCB.OrderSorting.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCB.OrderSorting.BLL.Model.CacheModel
{
    public class ThreadSortOrder
    {
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

    }
}
