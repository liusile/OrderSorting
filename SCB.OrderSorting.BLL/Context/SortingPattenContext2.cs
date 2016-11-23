using SCB.OrderSorting.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCB.OrderSorting.BLL.Context
{
    public class SortingPattenContext2 : SortingPattenContext
    {
        private List<SolutionCountry> solutionCountryList;

        public SortingPattenContext2(List<LatticeSetting> latticeSetting) : base(latticeSetting)
        {
        }

        public SortingPattenContext2(List<LatticeSetting> latticeSetting, List<SolutionCountry> solutionCountryList) : this(latticeSetting)
        {
            this.solutionCountryList = solutionCountryList;
        }

        public override string CreateButtonText(LatticeSetting ls)
        {
            if (!ls.IsEnable.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                return "未启用";
            var str = string.Join(",", solutionCountryList.Where(spt => spt.LatticeSettingId == ls.ID).Select(s => s.CountryName));
            if (str.Length > 25)
            {
                str = str.Substring(0, 25) + "..";
            }
            return string.Format("格号：{0}\r\n地区：{1}", ls.LatticeId, str);
        }

        /// <summary>
        /// 根据订单信息查找对应的柜格
        /// </summary>
        /// <param name="info">订单信息</param>
        public override LatticeSetting GetLatticeSettingByOrderinfo(OrderInfo info)
        {
            //var solutionCountry = solutionCountryList.Find(sp => sp.CountryId == info.countryID);
            //if (solutionCountry != null)
            //{
            //    return latticeSettingList.Find(ls => ls.ID == solutionCountry.LatticeSettingId);
            //}
            //return null;
            return (from sc in solutionCountryList
                    from ls in latticeSettingList
                    where sc.LatticeSettingId == ls.ID && sc.CountryId == info.CountryId && ls.IsEnable.Equals("true", System.StringComparison.CurrentCultureIgnoreCase)
                    select ls).FirstOrDefault();
        }
        /// <summary>
        /// 根据订单信息查找对应的柜格
        /// </summary>
        /// <param name="info">订单信息</param>
        public override List<LatticeSetting> GetLatticeSettingByOrderinfoList(OrderInfo info)
        {
            //var solutionCountry = solutionCountryList.Find(sp => sp.CountryId == info.countryID);
            //if (solutionCountry != null)
            //{
            //    return latticeSettingList.Find(ls => ls.ID == solutionCountry.LatticeSettingId);
            //}
            //return null;
            return (from sc in solutionCountryList
                    from ls in latticeSettingList
                    where sc.LatticeSettingId == ls.ID && sc.CountryId == info.CountryId && ls.IsEnable.Equals("true", System.StringComparison.CurrentCultureIgnoreCase)
                    select ls).ToList();
        }
        //internal override List<SolutionCountry> GetSolutionCountryListByLatticeSettingId(int latticeSettingId)
        //{
        //    return solutionCountryList.Where(sc => sc.LatticeSettingId == latticeSettingId).ToList();
        //}

        //internal override List<SolutionPostType> GetSolutionPostTypeListByLatticeSettingId(int latticeSettingId)
        //{
        //    return new List<SolutionPostType>();
        //}
    }
}
