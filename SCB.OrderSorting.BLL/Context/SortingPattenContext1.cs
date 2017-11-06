using SCB.OrderSorting.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCB.OrderSorting.BLL.Context
{
    /// <summary>
    /// 根据渠道分拣
    /// </summary>
    public class SortingPattenContext1 : SortingPattenContext
    {
        private List<SolutionPostType> solutionPostTypeList;
        private List<SolutionPostArea> solutionPostAreaList;

        public SortingPattenContext1(List<LatticeSetting> latticeSetting) : base(latticeSetting)
        {
        }

        public SortingPattenContext1(List<LatticeSetting> latticeSetting, List<SolutionPostType> solutionPostTypeList) : this(latticeSetting)
        {
            this.solutionPostTypeList = solutionPostTypeList;
        }
        public SortingPattenContext1(List<LatticeSetting> latticeSetting, List<SolutionPostType> solutionPostTypeList,List<SolutionPostArea> solutionPostAreaList) : this(latticeSetting, solutionPostTypeList)
        {
            this.solutionPostAreaList = solutionPostAreaList;
        }

        public override string CreateButtonText(LatticeSetting ls)
        {
            if (!ls.IsEnable.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                return "未启用";
            var str = string.Join(",", solutionPostTypeList.Where(spt => spt.LatticeSettingId == ls.ID).Select(s => s.PostTypeName));
            if (str.Length > 25)
            {
                str = str.Substring(0, 25) + ".."; 
            }
            string value= string.Format("格号：{0}\r\n渠道：{1}", ls.LatticeId, str);

            var PostArea = string.Join(",", solutionPostAreaList.Where(sp => sp.LactticeSettingId == ls.ID).Select(o=>o.Area));
            if (!string.IsNullOrWhiteSpace(PostArea))
            {
                if (value.Length > 25)
                {
                    value = value.Substring(0, 25) + "..";
                }
                value += string.Format("\r\n 地区：{0}", PostArea);
            }
            return value;
        }

        /// <summary>
        /// 根据订单信息查找对应的柜格
        /// </summary>
        /// <param name="info">订单信息</param>
        public override LatticeSetting GetLatticeSettingByOrderinfo(OrderInfo info)
        {
            return (from spt in solutionPostTypeList
                    from ls in latticeSettingList
                    where spt.LatticeSettingId == ls.ID && (spt.PostTypeId == info.PostId) && ls.IsEnable.Equals("true", System.StringComparison.CurrentCultureIgnoreCase)
                    select ls).FirstOrDefault();

            //var solutionPost = solutionPostTypeList.Find(sp => sp.PostTypeId == info.posttype);v
            //if (solutionPost != null)
            //{
            //    return latticeSettingList.Find(ls => ls.ID == solutionPost.LatticeSettingId);
            //}
            //return null;
            //var PostAreaList = (from spt in solutionPostAreaList
            //                     from ls in latticeSettingList
            //                     where spt.LactticeSettingId == ls.ID && spt.PostTypeId == info.PostId && ls.IsEnable.Equals("true", System.StringComparison.CurrentCultureIgnoreCase)
            //                     select spt).ToList();
            //LatticeSetting result =null;
            //int? type = PostAreaList.FirstOrDefault()?.Type;
            //if (type == 1)//1.跟踪单号
            //{
            //    result=(from ls in latticeSettingList
            //            where ls.ID == PostAreaList.Find(o => o.Flag == info.TraceId.Substring(0, 3))?.LactticeSettingId
            //            select ls).FirstOrDefault();
            //}
            //else if (type == 2)//2.邮编方式
            //{
                
            //    result =(from ls in latticeSettingList where ls.ID == PostAreaList.Find(o => o.Flag.Split(',').Contains(info.Zip.Substring(0, 1)))?.LactticeSettingId
            //    select ls).FirstOrDefault();
            //}
            //if (result == null) { 
            //    return (from spt in solutionPostTypeList
            //            from ls in latticeSettingList
            //            where spt.LatticeSettingId == ls.ID && spt.PostTypeId == info.PostId && ls.IsEnable.Equals("true", System.StringComparison.CurrentCultureIgnoreCase)
            //            select ls).FirstOrDefault();
            //}
            //else
            //{
            //    return result;
            //}
        }

        public override List<LatticeSetting> GetLatticeSettingByOrderinfoList(OrderInfo info)
        {
            //var solutionPost = solutionPostTypeList.Find(sp => sp.PostTypeId == info.posttype);v
            //if (solutionPost != null)
            //{
            //    return latticeSettingList.Find(ls => ls.ID == solutionPost.LatticeSettingId);
            //}
            //return null;
            var PostAreaList = (from spt in solutionPostAreaList
                                from ls in latticeSettingList
                                where spt.LactticeSettingId == ls.ID && spt.PostTypeId == info.PostId && ls.IsEnable.Equals("true", System.StringComparison.CurrentCultureIgnoreCase)
                                select spt).ToList();
            List<LatticeSetting> result = null;
            int? type = PostAreaList.FirstOrDefault()?.Type;
            if (type == 1)//1.跟踪单号
            {
                result = (from ls in latticeSettingList
                          where ls.ID == PostAreaList.Find(o => o.Flag == info.TraceId.Substring(0, 3))?.LactticeSettingId
                          select ls).ToList();
            }
            else if (type == 2)//2.邮编方式
            {
                result = (from ls in latticeSettingList
                          where ls.ID == PostAreaList.Find(o => o.Flag.Split(',').Contains(info.Zip.Substring(0, 1)))?.LactticeSettingId
                          select ls).ToList();
            }
            if (result == null)
            {
                return (from spt in solutionPostTypeList
                        from ls in latticeSettingList
                        where spt.LatticeSettingId == ls.ID && spt.PostTypeId == info.PostId && ls.IsEnable.Equals("true", System.StringComparison.CurrentCultureIgnoreCase)
                        select ls).ToList();
            }
            else
            {
                return result;
            }
        }
        //internal override List<SolutionCountry> GetSolutionCountryListByLatticeSettingId(int latticeSettingId)
        //{
        //    return new List<SolutionCountry>();
        //}

        //internal override List<SolutionPostType> GetSolutionPostTypeListByLatticeSettingId(int latticeSettingId)
        //{
        //    return solutionPostTypeList.Where(sc => sc.LatticeSettingId == latticeSettingId).ToList();
        //}
    }
}
