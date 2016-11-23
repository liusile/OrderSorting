using SCB.OrderSorting.DAL;
using System;
using System.Linq;

namespace SCB.OrderSorting.BLL.Service
{
    /// <summary>
    /// 旧数据清除服务
    /// </summary>
    internal static class ClearDataService
    {
        /// <summary>
        /// 数据保留期限：操作时间小于此日期的旧记录，从常用表中清除，迁移到指定的表
        /// </summary>
        private static DateTime oldTime;
        /// <summary>
        /// 定时清理/迁移旧数据
        /// </summary>
        /// <param name="logStorageDays">1:一天；2:三天；3:一周；4:一个月；5:三个月；6:半年；7:一年；8:两年；9:五年</param>
        internal static void ClearData(int logStorageDays)
        {
            //参数
            oldTime = GetLogStorageDateTime(logStorageDays);
            using (var db = new OrderSortingDBEntities())
            {
                ClearOldOrderSortingLog(db);
                ClearOldPackingLog(db);
                ClearOldOrderInfo(db);
                db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 获取具体日期
        /// </summary>
        /// <param name="logStorageDays">1:一天；2:三天；3:一周；4:一个月；5:三个月；6:半年；7:一年；8:两年；9:五年</param>
        /// <returns></returns>
        private static DateTime GetLogStorageDateTime(int logStorageDays)
        {
            switch (logStorageDays)
            {
                case 1://一天
                    return DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
                case 2://三天
                    return DateTime.Parse(DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"));
                case 3://一周
                    return DateTime.Parse(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));
                case 4://一个月
                    return DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
                case 5://三个月
                    return DateTime.Parse(DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd"));
                case 6://半年
                    return DateTime.Parse(DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd"));
                case 7://一年
                    return DateTime.Parse(DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd"));
                case 8://两年
                    return DateTime.Parse(DateTime.Now.AddYears(-2).ToString("yyyy-MM-dd"));
                case 9://五年
                    return DateTime.Parse(DateTime.Now.AddYears(-5).ToString("yyyy-MM-dd"));
                default:
                    return DateTime.Parse(DateTime.Now.AddYears(-100).ToString("yyyy-MM-dd"));
            }
        }

        /// <summary>
        /// 清除旧订单信息
        /// </summary>
        private static void ClearOldOrderInfo(OrderSortingDBEntities db)
        {
            var oldOrderInfo = db.OrderInfo.Where(osl => osl.CreateTime < oldTime).ToList();
            if (oldOrderInfo.Count < 1)
                return;
            var oldOrderInfo2 = (from old in oldOrderInfo
                                 select SetProperties<OrderInfo, OldOrderInfo>(old)).ToList();
            db.OrderInfo.RemoveRange(oldOrderInfo);
            db.OldOrderInfo.AddRange(oldOrderInfo2);
        }

        /// <summary>
        /// 清除旧装箱记录
        /// </summary>
        private static void ClearOldPackingLog(OrderSortingDBEntities db)
        {
            var packingLog = db.PackingLog.Where(osl => osl.OperationTime < oldTime).ToList();
            if (packingLog.Count < 1)
                return;
            var oldPackingLog = (from old in packingLog
                                 select SetProperties<PackingLog, OldPackingLog>(old)).ToList();
            db.PackingLog.RemoveRange(packingLog);
            db.OldPackingLog.AddRange(oldPackingLog);
        }

        /// <summary>
        /// 清除旧分拣记录
        /// </summary>
        private static void ClearOldOrderSortingLog(OrderSortingDBEntities db)
        {
            var orderSortingLog = db.OrderSortingLog.Where(osl => osl.OperationTime < oldTime).ToList();
            if (orderSortingLog.Count < 1)
                return;
            var oldOrderSortingLog = (from old in orderSortingLog
                                      select SetProperties<OrderSortingLog, OldOrderSortingLog>(old)).ToList();
            db.OrderSortingLog.RemoveRange(orderSortingLog);
            db.OldOrderSortingLog.AddRange(oldOrderSortingLog);
        }

        /// <summary>
        /// 把属性值赋给结构相同的类对象
        /// </summary>
        /// <typeparam name="OldType">原类</typeparam>
        /// <typeparam name="NewType">新类</typeparam>
        /// <param name="oldEntity">原类对象</param>
        /// <returns></returns>
        private static NewType SetProperties<OldType, NewType>(OldType oldEntity) where NewType : new()
        {
            if (oldEntity == null)
            {
                return default(NewType);
            }
            System.Reflection.PropertyInfo[] oldProperties = typeof(OldType).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            System.Reflection.PropertyInfo[] newProperties = typeof(NewType).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (oldProperties.Length != newProperties.Length || newProperties.Length == 0)
            {
                return default(NewType);
            }
            NewType newEntity = new NewType();
            foreach (System.Reflection.PropertyInfo newPrt in newProperties)
            {
                foreach (System.Reflection.PropertyInfo oldPrt in oldProperties)
                {
                    if (newPrt.Name == oldPrt.Name)
                    {
                        object value = oldPrt.GetValue(oldEntity);
                        newPrt.SetValue(newEntity, value);
                    }
                }
            }
            return newEntity;
        }
    }
}
