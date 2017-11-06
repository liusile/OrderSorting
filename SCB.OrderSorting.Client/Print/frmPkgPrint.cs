using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using System;
using System.Linq;
using System.Windows.Forms;
using SCB.OrderSorting.BLL.Model;
using System.Collections.Generic;
using SCB.OrderSorting.DAL;

namespace SCB.OrderSorting.Client
{
    public partial class frmPkgPrint : Form
    {
        private UserInfo _UserInfo;

        public frmPkgPrint(UserInfo _UserInfo)
        {
            this._UserInfo = _UserInfo;
            InitializeComponent();
        }

        private void btnReprintByLatticeId_Click(object sender, EventArgs e)
        {
            try
            {   
                if (string.IsNullOrWhiteSpace(txtLatticeId.Text))
                    return;
                var printNum = int.Parse(txtNum.Text.Trim());
                var latticeIdArray = txtLatticeId.Text.Split(Environment.NewLine.ToArray());
                List<LatticeOrdersCache> latticeInfo;
                var pkg = OrderSortService.CreatePackingLog(latticeIdArray, _UserInfo,out latticeInfo);
                if (pkg != null)
                {
                    for (int i = 0; i < printNum; i++)
                    {
                        //打印包牌
                        if (OrderSortService.GetSystemSettingCache().PrintFormat == 0)
                        {
                            new PackingLabelPrintDocument().PrintSetup(pkg);
                        }//打印二维码
                        else if(OrderSortService.GetSystemSettingCache().PrintFormat == 1)
                        {
                            new PackingLabelPrintDocument2().PrintSetup(pkg);
                        }
                        else if (OrderSortService.GetSystemSettingCache().PrintFormat == 2)
                        {
                            new PackingLabelPrintDocument().PrintSetup(pkg);
                        }
                        else if (OrderSortService.GetSystemSettingCache().PrintFormat == 3)
                        {
                            new PackingLabelPrintDocument2().PrintSetup(pkg);
                        }
                    }
                    if (OrderSortService.GetSystemSettingCache().PrintFormat == 2 || OrderSortService.GetSystemSettingCache().PrintFormat == 3)
                    {
                        
                      
                        if (latticeInfo.Count > 1)
                        {
                            latticeInfo.Add(new LatticeOrdersCache
                            {
                                CountryName = "袋子（箱子）",
                                Weight = OrderSortService.GetSystemSettingCache().BoxWeight
                            });
                            new PackingCountryItemsPrintDocument().PrintSetup(pkg, latticeInfo);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
    }
}
