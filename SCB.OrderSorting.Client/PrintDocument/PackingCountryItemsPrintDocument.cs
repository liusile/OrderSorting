using BarcodeLib;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using SCB.OrderSorting.BLL;
using SCB.OrderSorting.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;

namespace SCB.OrderSorting.Client
{
    public class PackingCountryItemsPrintDocument : PrintDocument
    {
        private PackingLog _packingLog;
        private List<LatticeOrdersCache> _latticeInfoList;
        Barcode b = new Barcode();
        TYPE type = TYPE.CODE128;

        public void PrintSetup(PackingLog packingLog, List<LatticeOrdersCache> latticeInfoList)
        {
            _packingLog = packingLog;
            _latticeInfoList = latticeInfoList;
            DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            //页面设置
            DefaultPageSettings.PaperSize = new PaperSize("printProcess", 320, 320);
            Print();
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
           var data = (from p in _latticeInfoList
                group p by p.CountryName into g
                select
                new
                {
                    Countrys = g.Key,
                    Weight = g.Sum(p => p.Weight),
                    Items = g.Count()
                }).ToList();
            if (data.Count() <= 2)
                return;
            //条码高度和宽度
            try
            {
                Pen myPen = new Pen(Color.FromArgb(255, Color.Black), 1.0F);
                //画表格竖线
                int height = 25;
                int width1 = 150;
                int width2 = 60;
                int width3 = 60;
                //画横线
                for (int i = 0; i <= data.Count()+2; i ++)
                {
                    e.Graphics.DrawLine(myPen, new Point(10, (i* height+20)), new Point(width1+ width2+ width3+30, (i * height+ 20)));
                   
                }
                //画竖线
                e.Graphics.DrawLine(myPen, new Point( 10, 20), new Point(10, (data.Count() + 2) * height+ 20));
                e.Graphics.DrawLine(myPen, new Point(width1 + 10, 20), new Point(width1 + 10, (data.Count() + 2) * height + 20));
                e.Graphics.DrawLine(myPen, new Point(width1+width2 + 10, 20), new Point(width1+width2 + 10, (data.Count() + 2) * height + 20));
                e.Graphics.DrawLine(myPen, new Point(width1+ width2+width3 + 30, 20), new Point(width1+ width2+width3 + 30, (data.Count() + 2) * height + 20));


                Font Regularfont = new System.Drawing.Font("Times New Roman, Times, serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(50)));
                Font Regularfont1 = new System.Drawing.Font("Times New Roman, Times, serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                e.Graphics.DrawString("COUNTRY", Regularfont, Brushes.Black, 11, 20);
                e.Graphics.DrawString("WEIGHT\n    KG", Regularfont, Brushes.Black, width1  + 10, 20);
                e.Graphics.DrawString("REGISTERED\n    ITEMS", Regularfont, Brushes.Black, width1+ width2+10, 20);
                for (int i = 0; i < data.Count(); i++)
                {
                    var country = data[i].Countrys;
                    //if (country.Length > 8)
                    //{
                    //    country = country.Insert(8, "\n");
                    //}
                    e.Graphics.DrawString(country, Regularfont1, Brushes.Black, 20, height * (i+1) + 30);
                    e.Graphics.DrawString(data[i].Weight.ToString(), Regularfont1, Brushes.Black, width1 + 20, height * (i + 1) + 30);
                    if (country == "袋子（箱子）")
                    {
                        e.Graphics.DrawString("", Regularfont1, Brushes.Black, width1 + width2 + 20, height * (i + 1) + 30);
                    }
                    else
                    {
                        e.Graphics.DrawString(data[i].Items.ToString(), Regularfont1, Brushes.Black, width1 + width2 + 20, height * (i + 1) + 30);
                    }
                }
                e.Graphics.DrawString("TOTAL", Regularfont1, Brushes.Black, 20, height * (data.Count()+1) + 30);
                e.Graphics.DrawString(data.Sum(o=>o.Weight).ToString(), Regularfont1, Brushes.Black, width1 + 20, height * (data.Count() + 1) + 30);
                e.Graphics.DrawString((data.Sum(o=>o.Items)-1).ToString(), Regularfont1, Brushes.Black, width1 + width2 + 20, height * (data.Count() + 1) + 30);
                this.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
