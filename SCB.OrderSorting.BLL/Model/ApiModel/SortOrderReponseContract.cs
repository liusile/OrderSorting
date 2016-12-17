using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCB.OrderSorting.BLL.Model
{
    public class SortOrderReponseContract
    {
        public string OrderId { get; set; }
        public string TraceId { get; set; }

        
        public string CountryId { get; set; }
        public string CountryCnName { get; set; }
        public string PostId { get; set; }
        public string PostCnName { get; set; }
        public string Zip { get; set; }
        public decimal Weight { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
