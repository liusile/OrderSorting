using SCB.OrderSorting.BLL.API;
using SCB.OrderSorting.BLL.Model;
using SCB.OrderSorting.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCB.OrderSorting.BLL.Context
{
    internal abstract class CenterContext
    {
        /// <summary>
        /// 根据订单号获取订单数据（国家、渠道）
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        internal abstract OrderInfo GetOrderInfoById(string orderId, UserInfo userInfo);
        /// <summary>
        /// 创建装箱记录（operationType：1自动满格，2手动满格，3打印包牌号）
        /// </summary>
        /// <param name="lattice">柜格</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="boxWeight">箱子重量</param>
        /// <param name="operationType">操作类型：1自动满格，2手动满格，3打印包牌号</param>
        internal abstract PackingLog CreatePackingLog(LatticeSetting lattice, UserInfo userInfo, decimal boxWeight, int operationType = 3);
        /// <summary>
        /// 创建装箱记录（多格口打印一个PKG标签）
        /// </summary>
        /// <param name="latticeIdArray">格口号</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="criticalWeight">临界重量</param>
        /// <param name="boxWeight">箱子重量</param>
        /// <returns></returns>
        internal abstract PackingLog CreatePackingLog(string[] latticeIdArray, UserInfo userInfo, decimal criticalWeight, decimal boxWeight, int operationType = 3);

        protected PackingLog NewPackingLog(LatticeSetting lattice, UserInfo userInfo, int operationType, List<LatticeOrdersCache> logList, decimal boxWeight)
        {
            var pkgLog = new PackingLog()
            {
                ID = Guid.NewGuid().ToString(),
                PackNumber = API_Helper.GetFlytPackageLabelID(),
                CabinetId = "",
                LatticeId = "",
                OrderIds = string.Join(",", logList.Select(l => l.OrderId)),
                OrderQty = logList.Count,
                Weight = boxWeight + logList.Sum(o => o.Weight),
                OperationType = operationType,
                OperationTime = DateTime.Now,
                UserId = userInfo.UserId,
                UserName = userInfo.UserName
            };
            if (lattice != null)
            {
                pkgLog.CabinetId = lattice.CabinetId.ToString();
                pkgLog.LatticeId = lattice.LatticeId;
            }
            pkgLog.PostTypeIds = string.Join(",", logList.Select(l => l.PostId).Distinct());
            var postTypeNames = logList.Select(l => l.PostName).Distinct();
            pkgLog.PostTypeNames = postTypeNames.Count() > 2 ? "MIX" : string.Join(",", postTypeNames);
            pkgLog.CountryIds = string.Join(",", logList.Select(s => s.CountryId).Distinct());
            var countryNames = logList.Select(s => s.CountryName).Distinct();
            pkgLog.CountryNames = countryNames.Count() > 2 ? "MIX" : string.Join(",", countryNames);
            return pkgLog;
        }
    }
}
