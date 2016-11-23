using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCB.OrderSorting.BLL.Model
{
    public class DayReportViewModel
    {
        public string 操作时间 { get; set; }
        public int 待投递 { get; set; }
        public int 已投递 { get; set; }
        public int 投递异常 { get; set; }
        public int 重复扫描 { get; set; }
        public int 总计 { get; set; }
       
    }
}
