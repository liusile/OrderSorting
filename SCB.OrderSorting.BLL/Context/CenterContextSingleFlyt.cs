using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCB.OrderSorting.BLL.Model;
using SCB.OrderSorting.DAL;
using SCB.OrderSorting.BLL.API;
using System.Data.Entity.Migrations;

namespace SCB.OrderSorting.BLL.Context
{
    internal class CenterContextSingleFlyt : CenterContext
    {
        /// <summary>
        /// 创建装箱记录（operationType：1自动满格，2手动满格，3打印包牌号）
        /// </summary>
        /// <param name="lattice">柜格</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="boxWeight">箱子重量</param>
        /// <param name="operationType">操作类型：1自动满格，2手动满格，3打印包牌号</param>
        /// <returns></returns>
        internal override PackingLog CreatePackingLog(LatticeSetting lattice, UserInfo userInfo, decimal boxWeight, out List<LatticeOrdersCache> latticeInfo, int operationType = 3)
        {
            latticeInfo = null;
            using (var db = new OrderSortingDBEntities())
            {
                var logCache = db.LatticeOrdersCache.Where(o => o.LatticesettingId == lattice.ID);
                if (logCache == null || logCache.Count() < 1)
                {
                   
                    return null;
                }
                var logList = logCache.ToList();
                latticeInfo = logList;
                PackingLog pkgLog = NewPackingLog(lattice, userInfo, operationType, logList, boxWeight);
                db.PackingLog.Add(pkgLog);
                db.LatticeOrdersCache.RemoveRange(logList);
                var response = API_Helper.BatchOutboundBySingleFlyt(userInfo, pkgLog, logList);
                if (response != null && !response.Success && !string.IsNullOrWhiteSpace(response.Message))
                {
                    throw new Exception(response.Message);
                }
                db.SaveChanges();
                return pkgLog;
            }
        }
        /// <summary>
        /// 创建装箱记录（多格口打印一个PKG标签）
        /// </summary>
        /// <param name="latticeIdArray">格口号</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="criticalWeight">临界重量</param>
        /// <param name="boxWeight">箱子重量</param>
        /// <returns></returns>
        internal override PackingLog CreatePackingLog(string[] latticeIdArray, UserInfo userInfo, decimal criticalWeight, decimal boxWeight, out List<LatticeOrdersCache> latticeInfo, int operationType = 3)
        {
            using (var db = new OrderSortingDBEntities())
            {
                //根据格口号获取格口信息
                var LatticesettingIds = db.LatticeSetting.Where(ls => latticeIdArray.Contains(ls.LatticeId)).Select(ls => ls.ID);
                if (LatticesettingIds.Count() != latticeIdArray.Where(o => !string.IsNullOrWhiteSpace(o)).Count())
                {
                    throw new Exception("输入的格口号有误，注意：多个格口间需使用回车键分隔！");
                }
                //根据格口信息获取相关的快件信息
                var logCache = db.LatticeOrdersCache.Where(o => LatticesettingIds.Contains(o.LatticesettingId));
                if (logCache == null || logCache.Count() < 1)
                {
                    throw new Exception("没有分拣记录，装箱记录生成失败！");
                }
                var logList = logCache.ToList();
                latticeInfo = logList;
                //装箱记录
                PackingLog pkgLog = NewPackingLog(null, userInfo, operationType, logList, boxWeight);
                if (pkgLog.Weight > criticalWeight)
                {
                    throw new Exception(string.Format("总重量{0}Kg，临界重量{1}Kg，装箱记录生成失败！", pkgLog.Weight, criticalWeight));
                }
                db.PackingLog.Add(pkgLog);
                db.LatticeOrdersCache.RemoveRange(logList);
                //把装箱信息上传到物流系统
                var response = API_Helper.BatchOutboundBySingleFlyt(userInfo, pkgLog, logList);
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
                //var info = db.OrderInfo.Find(orderId);
                //if (info != null)
                //{
                //    return info;
                //}

                var info = CreateFlytOrderInfo(orderId, userInfo);
                if (info != null)
                {
                    db.OrderInfo.AddOrUpdate(info);
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
            VerifyOrderResponseContract order = API_Helper.VerifyOrderBySingleFlyt(orderId, userInfo);
            if (((!order.Success ?? false) || (!order?.Sucess ?? false)) && !string.IsNullOrWhiteSpace(order.Message))
                throw new Exception(order.Message);
            return new OrderInfo
            {
                OrderId = order.OrderId,
                TraceId = string.IsNullOrWhiteSpace(order.TraceId) ? "" : order.TraceId,
                CountryId = order.CountryId,
                CountryName = string.IsNullOrWhiteSpace(order.CountryCnName) ? "" : order.CountryCnName,
                PostId = order.PostId,
                PostName = order.PostCnName,
                Zip = string.IsNullOrWhiteSpace(order.Zip) ? "" : order.Zip,
                Weight = order.Weight,
                CreateTime = DateTime.Now
            };
        }
        protected  new  PackingLog NewPackingLog(LatticeSetting lattice, UserInfo userInfo, int operationType, List<LatticeOrdersCache> logList, decimal boxWeight)
        {
            string PackNumber = API_Helper.GetFlytPackageLabelIDBySingleFlyt();

            var pkgLog = new PackingLog()
            {
                ID = Guid.NewGuid().ToString(),
                PackNumber = PackNumber,
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
