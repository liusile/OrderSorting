using SCB.OrderSorting.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SCB.OrderSorting.BLL.Context
{
    /// <summary>
    /// 根据国家渠道分拣
    /// </summary>
    public class SortingPattenContext3 : SortingPattenContext
    {
        private List<SolutionCountry> solutionCountryList;
        private List<SolutionPostType> solutionPostTypeList;

        public SortingPattenContext3(List<LatticeSetting> latticeSetting) : base(latticeSetting)
        {
        }

        public SortingPattenContext3(List<LatticeSetting> latticeSetting, List<SolutionPostType> solutionPostTypeList, List<SolutionCountry> solutionCountryList) : this(latticeSetting)
        {
            this.solutionPostTypeList = solutionPostTypeList;
            this.solutionCountryList = solutionCountryList;
        }

        public override string CreateButtonText(LatticeSetting ls)
        {
            if (!ls.IsEnable.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                return "未启用";
            var strPostType = string.Join(",", solutionPostTypeList.Where(spt => spt.LatticeSettingId == ls.ID).Select(s => s.PostTypeName));
            if (strPostType.Length > 12)
            {
                strPostType = strPostType.Substring(0, 12) + "..";
            }
            var strCountry = string.Join(",", solutionCountryList.Where(spt => spt.LatticeSettingId == ls.ID).Select(s => s.CountryName));
            if (strCountry.Length > 12)
            {
                strCountry = strCountry.Substring(0, 12) + "..";
            }
            return string.Format("格号：{0}\r\n地区：{1}\r\n渠道：{2}", ls.LatticeId, strCountry, strPostType);
        }

        /// <summary>
        /// 根据订单信息查找对应的柜格
        /// </summary>
        /// <param name="info">订单信息</param>
        public override LatticeSetting GetLatticeSettingByOrderinfo(OrderInfo info)
        {
            return (from sc in solutionCountryList
                    from spt in solutionPostTypeList
                    from ls in latticeSettingList
                    where sc.LatticeSettingId == ls.ID && spt.LatticeSettingId == ls.ID && sc.CountryId == info.CountryId && spt.PostTypeId == info.PostId && ls.IsEnable.Equals("true", System.StringComparison.CurrentCultureIgnoreCase)
                    select ls).FirstOrDefault();
        }
        /// <summary>
        /// 根据订单信息查找对应的柜格
        /// </summary>
        /// <param name="info">订单信息</param>
        public override List<LatticeSetting> GetLatticeSettingByOrderinfoList(OrderInfo info)
        {
            return (
                    from ls in latticeSettingList
                    join sc in solutionCountryList on ls.ID equals sc.LatticeSettingId into temp
                    from tt in temp.DefaultIfEmpty()
                    join spt in solutionPostTypeList on ls.ID equals spt.LatticeSettingId
                    where (tt==null || tt.CountryId == info.CountryId ) && spt.PostTypeId == info.PostId && ls.IsEnable.Equals("true", System.StringComparison.CurrentCultureIgnoreCase)
                    select ls).ToList();
        }
        //internal override List<SolutionCountry> GetSolutionCountryListByLatticeSettingId(int latticeSettingId)
        //{
        //    return solutionCountryList.Where(sc => sc.LatticeSettingId == latticeSettingId).ToList();
        //}

        //internal override List<SolutionPostType> GetSolutionPostTypeListByLatticeSettingId(int latticeSettingId)
        //{
        //    return solutionPostTypeList.Where(sc => sc.LatticeSettingId == latticeSettingId).ToList();
        //}
    }
}
