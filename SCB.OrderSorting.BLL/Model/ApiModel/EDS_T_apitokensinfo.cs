using PetaPoco;
using System;
using System.ComponentModel;

namespace SCB.OrderSorting.BLL.Model
{
    [Description("Primary:ID")]
    [PrimaryKey("ID", autoIncrement = true)]
    [TableName("t_apitokensinfo")]
    public class EDS_T_apitokensinfo
    {
        public const string PRIMARY_IN_DB = "ID";
        public const string TABLENAME_IN_DB = "t_apitokensinfo";

        public string Access_Token { get; set; }
        public DateTime? Access_Token_TimeOut { get; set; }
        public string ApiType { get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public string EbayName { get; set; }
        public long ID { get; set; }
        public long OAID { get; set; }
        public string Pwd { get; set; }
        public string Refresh_Token { get; set; }
        public DateTime? Refresh_Token_TimeOut { get; set; }
        public string UserName { get; set; }
    }
}
