using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using SCB.OrderSorting.DAL;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;

namespace SCB.OrderSorting.Client
{
    public class TestPrintDocument : PrintDocument
    {
        private OrderInfo _orderinfo;
        BarcodeLib.Barcode b = new BarcodeLib.Barcode();
        BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
        public void PrintSetup(OrderInfo info)
        {
           
            _orderinfo = info;
            //页边距设置
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
                e.Graphics.DrawImage(b.Encode(type, _orderinfo.OrderId, 300, 70), 10, 52);
                e.Graphics.DrawString(_orderinfo.OrderId, Regularfont, Brushes.Black, 80, 130);
                e.Graphics.DrawString("地区：" + _orderinfo.CountryName, Regularfont, Brushes.Black, 30, 175);
                e.Graphics.DrawString("渠道：" + _orderinfo.PostName, Regularfont, Brushes.Black, 30, 220);
                e.Graphics.DrawString("重量：" + _orderinfo.Weight + " Kg", Regularfont, Brushes.Black, 30, 265);
                e.HasMorePages = false;
                Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void GenerateQRByQrCodeNet(string content)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(content, out qrCode);

            GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);

            using (MemoryStream ms = new MemoryStream())
            {
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
                Image img = Image.FromStream(ms);
                img.Save("C:/Img/RRR.png");
            }
        }

    }
}
