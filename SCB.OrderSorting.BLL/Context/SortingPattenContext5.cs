using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCB.OrderSorting.DAL;
using System.Diagnostics;

namespace SCB.OrderSorting.BLL.Context
{
    /// <summary>
    /// 英国2号仓库分拣
    /// </summary>
    public class SortingPattenContext5 : SortingPattenContext
    {
        /// <summary>
        /// 当前解决方案数据
        /// </summary>
        public List<SolutionZipType> curSolutionZipTypeList { get; private set; }
        /// <summary>
        /// ZipType全部数据
        /// </summary>
        public List<ZipType> zipTypeList { get; private set; }
        public SortingPattenContext5(List<LatticeSetting> settingList) : base(settingList)
        {
        }
        public SortingPattenContext5(List<LatticeSetting> latticeSetting, List<ZipType> ZipTypeList, List<SolutionZipType> curSolutionZipTypeList) : this(latticeSetting)
        {
            this.curSolutionZipTypeList = curSolutionZipTypeList;
            this.zipTypeList = ZipTypeList;
        }
        public override string CreateButtonText(LatticeSetting ls)
        {
            string result = "";
            if (!ls.IsEnable.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                return "未启用";

            var soulutionZipType = curSolutionZipTypeList.Find(spt => spt.LatticeSettingId == ls.ID);
           
            result += string.Format("格号：{0}\r\n渠道：{1}", ls.LatticeId, soulutionZipType?.PostTypeName ?? "");
            if (!string.IsNullOrEmpty(soulutionZipType?.ZipName))
            {
                result += string.Format("\r\n地区：{1}", ls.LatticeId, soulutionZipType.ZipName);
            }
            return result;
        }

        public override LatticeSetting GetLatticeSettingByOrderinfo(OrderInfo info)
        {
            List<LatticeSetting> result = GetLatticeSettingByOrderinfoList(info);
            return result?.First();
        }

        public override List<LatticeSetting> GetLatticeSettingByOrderinfoList(OrderInfo info)
        {
            if (info == null) return null;
           // if (info.CountryId != "237") throw new Exception("该订单不属于英国");
            string zip = info.Zip;
            List<ZipType> specZipTypeList = null;
            //78 Royal Mail Second Class 特殊分区*
            if (info.PostId == "78")
            {
                specZipTypeList = zipTypeList.Where(o => o.Type == "3").ToList();
                return GetLatticeSetting(zip, specZipTypeList);
            }
            else
            {
                return (from s in latticeSettingList
                        join c in curSolutionZipTypeList on s.ID equals c.LatticeSettingId
                        where c.PostTypeId == info.PostId
                        select s).ToList();
            }
        }
        private List<LatticeSetting> GetLatticeSetting(string zip, List<ZipType> specZipTypeList)
        {
            zip = zip.ToUpper();
            var ZipName = "";
            for (int i = 1; i <= zip.Length; i++)
            {
                string subZip = zip.Substring(0, i);
                var zipTye = specZipTypeList.Where(o => o.ZipId == subZip);
                if (zipTye.Any())
                {
                    ZipName = zipTye.First().ZipName;
                }
                else
                {
                    continue;
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
