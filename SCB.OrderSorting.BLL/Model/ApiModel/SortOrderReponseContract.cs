using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCB.OrderSorting.BLL.Model
{
    public class SortOrderReponseContract
    {
        public string Id { get; set; }
        public string countryID { get; set; }
        public string enname { get; set; }
        public string posttype { get; set; }
        public string type { get; set; }
        public int IsSort { get; set; }
        public decimal weight2 { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
