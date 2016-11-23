namespace SCB.OrderSorting.BLL.Model
{
    internal class VerifyOrderResponseContract
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 跟踪号
        /// </summary>
        public string TraceId { get; set; }
        /// <summary>
        /// 邮递方式ID
        /// </summary>
        public string PostId { get; set; }
        public string PostCnName { get; set; }
        /// <summary>
        /// 国家ID
        /// </summary>
        public string CountryId { get; set; }
        public string CountryCnName { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// 交寄/收货重量
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Message { get; set; }
    }
}
