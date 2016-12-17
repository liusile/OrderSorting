using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCB.OrderSorting.BLL.Model
{
    internal class VerifyOrderRequestContract
    {
        public int OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string OrderId { get; set; }
        public string Token { get; set; }
        /// <summary>
        /// 处理中心
        /// </summary>
        public string ProcessCenterID { get; set; }
    }
}
