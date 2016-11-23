using System.Collections.Generic;

namespace SCB.OrderSorting.BLL.Model
{
    public class BatchOutboudRequestContract
    {
        public string Token { get; set; }
        public int OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string Pkg { get; set; }
        public string OutboundPostId { get; set; }
        public string ReceivePoint { get; set; }
        public string ProcessCenterId { get; set; }
        public List<OrderOutboudDetailContract> OutboudDetails { get; set; }
    }
}
