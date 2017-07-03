using BarcodeLib;
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
    public class PackingLabelPrintDocument2 : PrintDocument
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
                Font Regularfont = new Font("微软雅黑", 11F, FontStyle.Regular, GraphicsUnit.Point, 134);
                var x = 10;
                e.Graphics.DrawString("BG国家/分区：" + _packingLog.CountryNames, Regularfont, Brushes.Black, x, 20);
                e.Graphics.DrawImage(b.Encode(type, _packingLog.PackNumber, 180, 60), 20, 70);
                e.Graphics.DrawImage(GenerateQRByQrCodeNet(_packingLog.PackNumber), 140, 173);
                e.Graphics.DrawString(_packingLog.PackNumber, Regularfont, Brushes.Black, 53, 133);
                e.Graphics.DrawString("运输方式：" + _packingLog.PostTypeNames, Regularfont, Brushes.Black, x, 157);
                e.Graphics.DrawString("日期：" + _packingLog.OperationTime.ToString("yyyy/MM/dd"), Regularfont, Brushes.Black, x, 181);
                e.Graphics.DrawString("操作人：" + _packingLog.UserName, Regularfont, Brushes.Black, x, 206);
                e.Graphics.DrawString("件数：" + _packingLog.OrderQty, Regularfont, Brushes.Black, x, 229);
                e.Graphics.DrawString("重量：" + _packingLog.Weight + " Kg", Regularfont, Brushes.Black, x, 253);
                e.HasMorePages = false;
                Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Image GenerateQRByQrCodeNet(string content)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(content, out qrCode);

            GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(3, QuietZoneModules.Two), Brushes.Black, Brushes.White);

            using (MemoryStream ms = new MemoryStream())
            {
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
                Image img = Image.FromStream(ms);
                return img;
            }
        }
    }
}
