namespace SCB.OrderSorting.BLL.Model
{
    public class ResponseDataModel<T> where T : new()
    {
        public string FunnctionName { get; set; }
        public bool IsSuccess { get; set; }
        public T Content { get; set; }
        public string ErrorMsg { get; set; }
        public string SendDateTime { get; set; }
    }

    public class ResponseDataModel
    {

        public string Content { get; set; }
        public string ErrorMsg { get; set; }
        public bool IsSuccess { get; set; }
        public string SendDateTime { get; set; }
    }
}
