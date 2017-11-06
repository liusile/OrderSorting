using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCB.OrderSorting.BLL.Model.ApiModel
{
    public class ResponseDataModelBySingleFlyt
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public List<Content> Data { get; set; }
    }
    public class Content
    {
        public string Id { get; set; }
        public string Type { get; set; }
    }
}
