using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.DAL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    public partial class frmSelectCountrys : Form
    {
        private LatticeSetting _latticesetting;

        public frmSelectCountrys(LatticeSetting _latticesetting)
        {
            this._latticesetting = _latticesetting;
            InitializeComponent();
        }

        private void frmSelectCountrys_Load(object sender, EventArgs e)
        {
            try
            {
                var sysSetting = OrderSortService.GetSystemSettingCache();
               
                //clsCountrys
                var countrysList = OrderSortService.GetCountrysList();
               
                if (sysSetting.InterfaceType == BLL.Model.InterfaceType.SigleFlyt) {
                    countrysList.ForEach(o => o.ID = o.EnShorting);

                }
                clsCountrys.DataSource = countrysList;
                clsCountrys.ValueMember = "ID";
                clsCountrys.DisplayMember = "CnName";
                SetDefaultChecked();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void SetDefaultChecked()
        {
            try
            {
                var solution = OrderSortService.GetSolutionCountryListByLatticeSettingId(_latticesetting.ID);
                if (solution == null || solution.Count < 1)
                    return;
                //选项
                for (int i = 0; i < clsCountrys.Items.Count; i++)
                {
                    var pt = clsCountrys.Items[i] as Countrys;
                    if (solution.Exists(s => s.CountryId == pt.ID))
                    {
                        clsCountrys.SetItemChecked(i, true);
                        clsCountrys.SetSelected(i, true);
                    }
                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                txtSearch.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string str = string.Empty;
                var list = new List<Countrys>();
                foreach (var item in clsCountrys.CheckedItems)
                {
                    list.Add(item as Countrys);
                }
                OrderSortService.UpdateSolutionCountry(_latticesetting, list);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    for (int i = 0; i < clsCountrys.Items.Count; i++)
                    {
                        var ptStr = clsCountrys.GetItemText(clsCountrys.Items[i]);
                        if (ptStr.Equals(txtSearch.Text))
                        {
                            clsCountrys.SetSelected(i, true);
                            return;
                        }
                        if (ptStr.Contains(txtSearch.Text))
                        {
                            clsCountrys.SetSelected(i, true);
                            return;
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
