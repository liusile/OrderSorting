using SCB.OrderSorting.BLL.API;
using SCB.OrderSorting.BLL.Model;
using SCB.OrderSorting.DAL;
using System;
using System.Linq;

namespace SCB.OrderSorting.BLL.Context
{
    internal class CenterContextFlyt : CenterContext
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
                //根据格口号获取格口信息
                var LatticesettingIds = db.LatticeSetting.Where(ls => latticeIdArray.Contains(ls.LatticeId)).Select(ls => ls.ID);
                //根据格口信息获取相关的快件信息
                var logCache = db.LatticeOrdersCache.Where(o => LatticesettingIds.Contains(o.LatticesettingId));
                if (logCache == null || logCache.Count() < 1)
                {
                    throw new Exception("没有分拣记录，装箱记录生成失败！");
                }
                var logList = logCache.ToList();
                //装箱记录
                PackingLog pkgLog = NewPackingLog(null, userInfo, operationType, logList, boxWeight);
                if (pkgLog.Weight > criticalWeight)
                {
                    throw new Exception(string.Format("总重量{0}Kg，临界重量{1}Kg，装箱记录生成失败！", pkgLog.Weight, criticalWeight));
                }
                db.PackingLog.Add(pkgLog);
                db.LatticeOrdersCache.RemoveRange(logList);
                //把装箱信息上传到物流系统
                var response = API_Helper.BatchOutbound(userInfo, pkgLog, logList);
                if (response != null && !response.Success && !string.IsNullOrWhiteSpace(response.Message))
                {
                    throw new Exception(response.Message);
                }
                db.SaveChanges();
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
                var response = API_Helper.BatchOutbound(userInfo, pkgLog, logList);
                if (response != null && !response.Success && !string.IsNullOrWhiteSpace(response.Message))
                {
                    throw new Exception(response.Message);
                }
                db.SaveChanges();
                return pkgLog;
            }
        }
        /// <summary>
        /// 根据订单号获取订单信息
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        internal override OrderInfo GetOrderInfoById(string orderId, UserInfo userInfo)
        {
            using (var db = new OrderSortingDBEntities())
            {
                var info = db.OrderInfo.Find(orderId);
                if (info != null)
                {
                    return info;
                }
                info = CreateFlytOrderInfo(orderId, userInfo);
                if (info != null)
                {
                    db.OrderInfo.Add(info);
                    db.SaveChangesAsync();
                }
                return info;
            }
        }
        /// <summary>
        /// 调用物流接口，根据订单号获取订单信息
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        private OrderInfo CreateFlytOrderInfo(string orderId, UserInfo userInfo)
        {
            //调用物流接口，根据订单号获取订单信息
            var order = API_Helper.VerifyOrder(orderId, userInfo);
            if (!order.Success && !string.IsNullOrWhiteSpace(order.Message))
                throw new Exception(order.Message);
            return new OrderInfo
            {
                OrderId = order.OrderId,
                TraceId = string.IsNullOrWhiteSpace(order.TraceId) ? "" : order.TraceId,
                CountryId = order.CountryId,
                CountryName = order.CountryCnName,
                PostId = order.PostId,
                PostName = order.PostCnName,
                Zip = string.IsNullOrWhiteSpace(order.Zip) ? "" : order.Zip,
                Weight = order.Weight,
                CreateTime = DateTime.Now
            };
        }
    }
}
