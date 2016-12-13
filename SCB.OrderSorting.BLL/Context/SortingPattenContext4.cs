using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCB.OrderSorting.DAL;

namespace SCB.OrderSorting.BLL.Context
{
    /// <summary>
    /// 英国伯明翰仓库分拣
    /// </summary>
    public class SortingPattenContext4 : SortingPattenContext
    {
        
        public List<SolutionZipType> solutionZipTypeList { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ZipType> zipTypeList { get; private set; }
        public SortingPattenContext4(List<LatticeSetting> settingList) : base(settingList)
        {
        }
        public SortingPattenContext4(List<LatticeSetting> latticeSetting, List<SolutionZipType> solutionZipTypeList) : this(latticeSetting)
        {
            this.solutionZipTypeList = solutionZipTypeList;
        }

        public override string CreateButtonText(LatticeSetting ls)
        {
            if (!ls.IsEnable.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                return "未启用";
            var str = string.Join(",", solutionZipTypeList.Where(spt => spt.LatticeSettingId == ls.ID).Select(s => s.ZipName));
            if (str.Length > 25)
            {
                str = str.Substring(0, 25) + "..";
            }
            return string.Format("格号：{0}\r\n地区：{1}", ls.LatticeId, str);
        }

        public override LatticeSetting GetLatticeSettingByOrderinfo(OrderInfo info)
        {
            if (info == null) return null;
            if (info.CountryId != "154") throw new Exception("该订单不属于英国");
            string zip = info.Zip;

            //1031 飞特英国小包特惠派送走特殊分区
            if (info.PostId == "1031")
            {
               var specZipTypeList= zipTypeList.Where(o => o.Type == "3");
            }
            else
            {

            }
            return (from sc in solutionZipTypeList
                    from ls in latticeSettingList
                    where sc.LatticeSettingId == ls.ID && sc.CountryId == info.CountryId && ls.IsEnable.Equals("true", System.StringComparison.CurrentCultureIgnoreCase)
                    select ls).FirstOrDefault();
        }

        public override List<LatticeSetting> GetLatticeSettingByOrderinfoList(OrderInfo info)
        {
            throw new NotImplementedException();
        }
        private LatticeSetting GetLatticeSetting(string zip,ZipType specZipTypeList)
        {
            for(int i = 1; i <= zip.Length; i++)
            {
                string subZip = zip.Substring(0, i);
                specZipTypeList
            }
        }

    }
}
