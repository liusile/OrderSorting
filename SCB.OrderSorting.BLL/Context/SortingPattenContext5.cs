using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCB.OrderSorting.DAL;

namespace SCB.OrderSorting.BLL.Context
{
    /// <summary>
    /// 英国2号仓库分拣
    /// </summary>
    public class SortingPattenContext5 : SortingPattenContext
    {
        public SortingPattenContext5(List<LatticeSetting> settingList) : base(settingList)
        {
        }

        public override string CreateButtonText(LatticeSetting setting)
        {
            throw new NotImplementedException();
        }

        public override LatticeSetting GetLatticeSettingByOrderinfo(OrderInfo info)
        {
            throw new NotImplementedException();
        }

        public override List<LatticeSetting> GetLatticeSettingByOrderinfoList(OrderInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
