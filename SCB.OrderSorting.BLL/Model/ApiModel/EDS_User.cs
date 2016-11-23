using PetaPoco;
using System;
using System.ComponentModel;

namespace SCB.OrderSorting.BLL.Model
{
    [DefaultProperty("Users┋EDS_Users")]
    [Description("Primary:UserId")]
    [PrimaryKey("UserId", autoIncrement = true)]
    [TableName("Users")]
    public class EDS_User
    {
        public const string PRIMARY_IN_DB = "UserId";
        public const string TABLENAME_IN_DB = "Users";

        public int CompanyID { get; set; }
        public string DeliverAddress { get; set; }
        public string DepartID { get; set; }
        public string Email { get; set; }
        public string EPacketType { get; set; }
        public bool IsAdmin { get; set; }
        public bool Islocked { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string PassWord { get; set; }
        public string ProcessCenterID { get; set; }
        public string RoleID { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
