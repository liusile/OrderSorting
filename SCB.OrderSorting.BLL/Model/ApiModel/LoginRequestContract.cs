namespace SCB.OrderSorting.BLL.Model
{
    public class LoginRequestContract
    {
        /// <summary>
        /// 接口请求令牌（物流组提供）5A9C85B6E068F2236A039E6157C5DF5B
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 登录名（邮箱地址）
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperatorId { get; set; }
        /// <summary>
        /// 登录密码(明文)
        /// </summary>
        public string Password { get; set; }
    }
}
