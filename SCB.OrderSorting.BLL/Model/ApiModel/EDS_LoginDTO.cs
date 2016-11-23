using System.Collections.Generic;

namespace SCB.OrderSorting.BLL.Model
{
    public class EDS_LoginDTO
    {
        public string CNPostName { get; set; }
        public int EBayCategory { get; set; }
        public EDS_User EDS_User { get; set; }
        public string ENPostName { get; set; }
        public List<EDS_T_apitokensinfo> lstEDS_T_apitokensinfo { get; set; }
        public int PostID { get; set; }
        public string TypeCode { get; set; }
        public int Version { get; set; }
    }
}
