using BarcodeLib;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using SCB.OrderSorting.BLL;
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

        //protected override void OnPrintPage(PrintPageEventArgs e)
        //{
        //    try
        //    {
        //        b.IncludeLabel = false;
        //        Font Regularfont = new Font("微软雅黑", 11F, FontStyle.Regular, GraphicsUnit.Point, 134);
        //        var x = 10;
        //        e.Graphics.DrawString("BG国家/分区：" + _packingLog.CountryNames, Regularfont, Brushes.Black, x, 20);
        //        e.Graphics.DrawImage(b.Encode(type, _packingLog.PackNumber, 180, 60), 20, 70);
        //        e.Graphics.DrawImage(GenerateQRByQrCodeNet(_packingLog.PackNumber), 140, 173);
        //        e.Graphics.DrawString(_packingLog.PackNumber, Regularfont, Brushes.Black, 53, 133);
        //        e.Graphics.DrawString("运输方式：" + _packingLog.PostTypeNames, Regularfont, Brushes.Black, x, 157);
        //        e.Graphics.DrawString("日期：" + _packingLog.OperationTime.ToString("yyyy/MM/dd"), Regularfont, Brushes.Black, x, 181);
        //        e.Graphics.DrawString("操作人：" + _packingLog.UserName, Regularfont, Brushes.Black, x, 206);
        //        e.Graphics.DrawString("件数：" + _packingLog.OrderQty, Regularfont, Brushes.Black, x, 229);
        //        e.Graphics.DrawString("重量：" + _packingLog.Weight + " Kg", Regularfont, Brushes.Black, x, 253);
        //        e.HasMorePages = false;
        //        Dispose();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
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
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            //条码高度和宽度
            try
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                Font Regularfont = new System.Drawing.Font("Times New Roman, Times, serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                Font Regularfont1 = new System.Drawing.Font("Times New Roman, Times, serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                Font Regularfont2 = new System.Drawing.Font("Times New Roman, Times, serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                Font Regularfont3 = new System.Drawing.Font("Times New Roman, Times, serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                Font Regularfont4 = new System.Drawing.Font("Times New Roman, Times, serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                Font Regularfont5 = new System.Drawing.Font("Times New Roman, Times, serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                Font Regularfont6 = new System.Drawing.Font("Times New Roman, Times, serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                Font Regularfont7 = new System.Drawing.Font("Times New Roman, Times, serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                Font Regularfont8 = new System.Drawing.Font("Times New Roman, Times, serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                Font Regularfont9 = new System.Drawing.Font("Times New Roman, Times, serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                Font Regularfont10 = new System.Drawing.Font("Times New Roman, Times, serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                BarcodeLib.Barcode b = new BarcodeLib.Barcode();
                BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
                b.IncludeLabel = false;
                if (_packingLog.PostTypeNames.Length > 12)
                {
                    _packingLog.PostTypeNames = _packingLog.PostTypeNames.Insert(12, "\n");
                }
                string _countryArea = "BG国家/分区 : ";
                if (_packingLog.CountryNames.IndexOf(",") < 0)
                {
                    _countryArea += _packingLog.CountryNames;
                }
                var PostTypeNames = OrderSortService.GetPostTypeName(_packingLog.PostTypeIds);

                e.Graphics.DrawString(_countryArea, Regularfont4, Brushes.Black, 30, 30);
                string _dayPackage = _packingLog.OperationTime.ToString("MM/dd");
                e.Graphics.DrawImage(b.Encode(type, _packingLog.PackNumber, 150, 30), 80, 90);
                e.Graphics.DrawString(_packingLog.PackNumber, Regularfont7, Brushes.Black, 105, 125);
                e.Graphics.DrawString("运输方式："+ PostTypeNames, Regularfont4, Brushes.Black, 30, 150);
                e.Graphics.DrawString("日期："+ _dayPackage, Regularfont4, Brushes.Black, 30, 190);
                e.Graphics.DrawString("操作人：", Regularfont4, Brushes.Black, 30, 220);
                e.Graphics.DrawString("件数："+ _packingLog.OrderQty, Regularfont4, Brushes.Black, 30, 250);
                e.Graphics.DrawString("BGHZ", Regularfont4, Brushes.Black, 30, 280);
                e.Graphics.DrawString("重量：", Regularfont4, Brushes.Black, 130, 280);
                var image = GenerateQRByQrCodeNet(_packingLog.PackNumber);
                e.Graphics.DrawImage(image, 130, 170,100, 100);

                this.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
