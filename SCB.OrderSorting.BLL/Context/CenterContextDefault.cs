using SCB.OrderSorting.BLL.API;
using SCB.OrderSorting.BLL.Model;
using SCB.OrderSorting.DAL;
using System;
using System.Diagnostics;
using System.Linq;

namespace SCB.OrderSorting.BLL.Context
{
    internal class CenterContextDefault : CenterContext
    {
        /// <summary>
        /// 创建装箱记录（多格口打印一个PKG标签）
        /// </summary>
        /// <param name="latticeIdArray">格口号</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="criticalWeight">临界重量</param>
        /// <param name="boxWeight">箱子重量</param>
        /// <returns></returns>
        internal override PackingLog CreatePackingLog(string[] latticeIdArray, UserInfo userInfo, decimal criticalWeight, decimal boxWeight, int operationType = 3)
        {
            using (var db = new OrderSortingDBEntities())
            {
                var LatticesettingIds = db.LatticeSetting.Where(ls => latticeIdArray.Contains(ls.LatticeId)).Select(ls => ls.ID);
                var logCache = db.LatticeOrdersCache.Where(o => LatticesettingIds.Contains(o.LatticesettingId));
                if (logCache == null || logCache.Count() < 1)
                {
                    throw new Exception("没有分拣记录，装箱记录生成失败！");
                }
                var logList = logCache.ToList();
                PackingLog pkgLog = NewPackingLog(null, userInfo, operationType, logList, boxWeight);
                if (pkgLog.Weight > criticalWeight)
                {
                    throw new Exception(string.Format("总重量{0}Kg，临界重量{1}Kg，装箱记录生成失败！", pkgLog.Weight, criticalWeight));
                }
                db.PackingLog.Add(pkgLog);
                db.LatticeOrdersCache.RemoveRange(logList);
                db.SaveChangesAsync();
                return pkgLog;
            }
        }
        /// <summary>
        /// 创建装箱记录（operationType：1自动满格，2手动满格，3打印包牌号）
        /// </summary>
        /// <param name="lattice">柜格</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="boxWeight">箱子重量</param>
        /// <param name="operationType">操作类型：1自动满格，2手动满格，3打印包牌号</param>
        /// <returns></returns>
        internal override PackingLog CreatePackingLog(LatticeSetting lattice, UserInfo userInfo, decimal boxWeight, int operationType = 3)
        {
            Debug.WriteLine("CreatePackingLog begin 1");
            using (var db = new OrderSortingDBEntities())
            {
                var logCache = db.LatticeOrdersCache.Where(o => o.LatticesettingId == lattice.ID);
                if (logCache == null || logCache.Count() < 1)
                {
                    return null;
                }
                var logList = logCache.ToList();
                PackingLog pkgLog = NewPackingLog(lattice, userInfo, operationType, logList, boxWeight);
                db.PackingLog.Add(pkgLog);
                db.LatticeOrdersCache.RemoveRange(logList);
                db.SaveChangesAsync();
                return pkgLog;
            }
        }
        /// <summary>
        /// 根据订单号获取订单信息
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="userInfo">用户信息</param>
        internal override OrderInfo GetOrderInfoById(string orderId, UserInfo userInfo)
        {
            using (var db = new OrderSortingDBEntities())
            {
                var info = db.OrderInfo.Find(orderId);
                if (info != null)
                {
                    return info;
                }
                info = CreateOrderInfo(orderId);
                if (info != null)
                {
                    db.OrderInfo.Add(info);
                    db.SaveChangesAsync();
                }
                return info;
            }
        }
        /// <summary>
        /// 调用EDS接口，根据订单号获取订单信息
        /// </summary>
        /// <param name="orderId">订单号</param>
        private OrderInfo CreateOrderInfo(string orderId)
        {
            var order = API_Helper.GetPostForSortOrder(orderId);
            return new OrderInfo
            {
                OrderId = order.Id,
                TraceId = "",
                CountryId = order.countryID,
                CountryName = order.enname,
                PostId = order.posttype,
                PostName = order.type,
                Zip = "",
                Weight = order.weight2,
                CreateTime = DateTime.Now
            };
        }
    }
}
