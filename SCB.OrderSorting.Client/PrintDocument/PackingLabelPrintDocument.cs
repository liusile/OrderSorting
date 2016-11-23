using BarcodeLib;
using SCB.OrderSorting.DAL;
using System;
using System.Drawing;
using System.Drawing.Printing;

namespace SCB.OrderSorting.Client
{
    public class PackingLabelPrintDocument : PrintDocument
    {
        private PackingLog _packingLog;
        Barcode b = new Barcode();
        TYPE type = TYPE.CODE128;

        public void PrintSetup(PackingLog packingLog)
        {
            _packingLog = packingLog;
            DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            //页面设置
            DefaultPageSettings.PaperSize = new PaperSize("printProcess", 320, 320);
            Print();
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            try
            {     
                b.IncludeLabel = false;
                Font Regularfont = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
                var x = 30;
                e.Graphics.DrawString("BG国家/分区：" + _packingLog.CountryNames, Regularfont, Brushes.Black, x, 20);
                e.Graphics.DrawImage(b.Encode(type, _packingLog.PackNumber, 275, 60), 20, 70);
                e.Graphics.DrawString(_packingLog.PackNumber, Regularfont, Brushes.Black, 90, 133);
                e.Graphics.DrawString("运输方式：" + _packingLog.PostTypeNames, Regularfont, Brushes.Black, x, 162);
                e.Graphics.DrawString("日期：" + _packingLog.OperationTime.ToString("yyyy/MM/dd"), Regularfont, Brushes.Black, x, 192);
                e.Graphics.DrawString("操作人：" + _packingLog.UserName, Regularfont, Brushes.Black, x, 222);
                e.Graphics.DrawString("件数：" + _packingLog.OrderQty, Regularfont, Brushes.Black, x, 252);
                e.Graphics.DrawString("重量：" + _packingLog.Weight + " Kg", Regularfont, Brushes.Black, x, 282);
                e.HasMorePages = false;
                Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
