namespace SCB.OrderSorting.BLL.Model
{
    public class UserInfo
    {
        /// <summary>
        /// 登录用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户所属的收货点ID
        /// </summary>
        public string ReceivePointId { get; set; }
        /// <summary>
        /// 用户所属的收货点名称
        /// </summary>
        public string RepName { get; set; }
        /// <summary>
        /// 用户所属的发货中心ID
        /// </summary>
        public string Pcid { get; set; }
        /// <summary>
        /// 用户所属的发货中心名称
        /// </summary>
        public string PcName { get; set; }
    }
}
