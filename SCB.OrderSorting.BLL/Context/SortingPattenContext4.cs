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
        /// <summary>
        /// 当前解决方案数据
        /// </summary>
        public List<SolutionZipType> curSolutionZipTypeList { get; private set; }
        /// <summary>
        /// ZipType全部数据
        /// </summary>
        public List<ZipType> zipTypeList { get; private set; }
        public SortingPattenContext4(List<LatticeSetting> settingList) : base(settingList)
        {
        }
        public SortingPattenContext4(List<LatticeSetting> latticeSetting, List<ZipType> ZipTypeList, List<SolutionZipType> curSolutionZipTypeList) : this(latticeSetting)
        {
            this.curSolutionZipTypeList = curSolutionZipTypeList;
            this.zipTypeList = ZipTypeList;
        }

        public override string CreateButtonText(LatticeSetting ls)
        {
            if (!ls.IsEnable.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                return "未启用";
            var str = string.Join(",", curSolutionZipTypeList.Where(spt => spt.LatticeSettingId == ls.ID).Select(s => s.ZipName));
            if (str.Length > 25)
            {
                str = str.Substring(0, 25) + "..";
            }
            return string.Format("格号：{0}\r\n地区：{1}", ls.LatticeId, str);
        }

        public override LatticeSetting GetLatticeSettingByOrderinfo(OrderInfo info)
        {
            List<LatticeSetting> result = GetLatticeSettingByOrderinfoList(info);
            return result?.First();
        }

        public override List<LatticeSetting> GetLatticeSettingByOrderinfoList(OrderInfo info)
        {
            if (info == null) return null;
            if (info.CountryId != "237") throw new Exception("该订单不属于英国");
            string zip = info.Zip;
            List<ZipType> specZipTypeList = null;
            //142 飞特英国小包特惠派送走特殊分区
            if (info.PostId == "142")
            {
                specZipTypeList = zipTypeList.Where(o => o.Type == "3").ToList();
            }
            else
            {
                specZipTypeList = zipTypeList.Where(o => o.Type == "1").ToList();
            }
            return GetLatticeSetting(zip, specZipTypeList);
        }
        private List<LatticeSetting> GetLatticeSetting(string zip, List<ZipType> specZipTypeList)
        {
            zip = zip.ToUpper();
            var ZipName = "";
            for(int i = 1; i <= zip.Length; i++)
            {
                string subZip = zip.Substring(0, i);
                var zipTye = specZipTypeList.Where(o => o.ZipId == subZip);
                if (zipTye.Any())
                {
                    ZipName = zipTye.First().ZipName;
                } else
                {
                    break;
                }
            }
            if (string.IsNullOrEmpty(ZipName))
            {
                return null;
            }
            else
            {
                var result = (from s in latticeSettingList
                             join c in curSolutionZipTypeList on s.ID equals c.LatticeSettingId
                              where c.ZipName == ZipName
                              select s).ToList();
                return result;
            }
        }
    }
}
