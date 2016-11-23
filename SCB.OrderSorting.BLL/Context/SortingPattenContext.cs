using SCB.OrderSorting.DAL;
using System.Collections.Generic;

namespace SCB.OrderSorting.BLL.Context
{
    public abstract class SortingPattenContext
    {
        /// <summary>
        /// 格口设置信息
        /// </summary>
        protected List<LatticeSetting> latticeSettingList;

        public SortingPattenContext(List<LatticeSetting> settingList)
        {
            latticeSettingList = settingList;
        }
        /// <summary>
        /// 获取格口信息
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public abstract string CreateButtonText(LatticeSetting setting);

        /// <summary>
        /// 根据订单信息查找对应的柜格
        /// </summary>
        /// <param name="info">订单信息</param>
        public abstract LatticeSetting GetLatticeSettingByOrderinfo(OrderInfo info);
        /// <summary>
        /// 根据订单信息查找对应的柜格
        /// </summary>
        /// <param name="info">订单信息</param>
        public abstract List<LatticeSetting> GetLatticeSettingByOrderinfoList(OrderInfo info);
        //internal abstract List<SolutionCountry> GetSolutionCountryListByLatticeSettingId(int latticeSettingId);

        //internal abstract List<SolutionPostType> GetSolutionPostTypeListByLatticeSettingId(int latticeSettingId);
    }
}