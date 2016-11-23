namespace SCB.OrderSorting.BLL.Model
{
    public class OrderOutboudDetailContract
    {
        public string OrderId { get; set; }
        public string TraceId { get; set; }
        public decimal Weight { get; set; }
        public string CountryId { get; set; }
        /// <summary>
        /// 0：无(默认)。1：国外退件。2：包装不符。3：渠道商无法交寄。4：地址问题。5：安检不合格。6：数据不符。7：拦截。8：其他
        /// </summary>
        public int Reason { get; set; }
    }
}
