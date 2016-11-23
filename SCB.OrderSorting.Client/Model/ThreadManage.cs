using SCB.OrderSorting.BLL.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SCB.OrderSorting.Client.Model
{
    public class ThreadManage
    {
        //从机
        public SlaveConfig SlaveConfig { get; set; }
        public BlockingCollection<ThreadWriteMsg> QueueWrite { get; set; }
        
    }
}
