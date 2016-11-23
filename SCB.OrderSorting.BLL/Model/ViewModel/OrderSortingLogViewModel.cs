namespace SCB.OrderSorting.BLL.Model
{
    public class OrderSortingLogViewModel
    {
        public string 订单号 { get; set; }
        public string 目标柜号 { get; set; }
        public string 目标格号 { get; set; }
        public string 投入柜号 { get; set; }
        public string 投入格号 { get; set; }
        public string 操作类型 { get; set; }
        public string 状态 { get; set; }
        public string 操作时间 { get; set; }    
    }
}
