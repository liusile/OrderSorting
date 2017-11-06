namespace SCB.OrderSorting.BLL.Model
{
    public class LoginResponseContract
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 登录用户ID
        /// </summary>
        public string userID { get; set; }
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 用户所属的发货中心ID
        /// </summary>
        public string Pcid { get; set; }
        /// <summary>
        /// 用户所属的发货中心名称
        /// </summary>
        public string PcName { get; set; }
        /// <summary>
        /// 用户所属的收货点ID
        /// </summary>
        public string ReceivePointId { get; set; }
        /// <summary>
        /// 用户所属的收货点名称
        /// </summary>
        public string RepName { get; set; }
        /// <summary>
        /// 用户所属的公司ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 可用的邮递方式集合
        /// </summary>
        public PostTypeModel[] PostArray { get; set; }
        /// <summary>
        /// 收货点集合
        /// </summary>
        public DeliverAddressModel[] DeliverAddressArray { get; set; }
    }

    public class PostTypeModel
    {
        /// <summary>
        /// 邮递方式ID
        /// </summary>
        public string PtId { get; set; }
        /// <summary>
        /// 邮递方式名称
        /// </summary>
        public string PostName { get; set; }
        /// <summary>
        /// 是否为退件邮递方式
        /// </summary>
        public bool IsReturnOrder { get; set; }
    }

    public class DeliverAddressModel
    {
        /// <summary>
        /// 收货点ID
        /// </summary>
        public string DId { get; set; }
        /// <summary>
        /// 收货点名称
        /// </summary>
        public string DName { get; set; }
    }
}
