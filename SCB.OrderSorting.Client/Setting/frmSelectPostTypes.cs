using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.DAL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    public partial class frmSelectPostTypes : Form
    {
        private LatticeSetting _latticesetting;

        public frmSelectPostTypes(LatticeSetting latticesetting)
        {
            this._latticesetting = latticesetting;
            InitializeComponent();
        }

        private void frmSelectPostTypes_Load(object sender, EventArgs e)
        {
            try
            {
               var sysSetting = OrderSortService.GetSystemSettingCache();
                //clsPosttypes
                var posttypeList = OrderSortService.GetPostTypesList();
                if (sysSetting.InterfaceType == BLL.Model.InterfaceType.SigleFlyt)
                {
                    posttypeList = posttypeList.FindAll(o => o.PostID == o.EnPostCode);
                }
                else
                {
                    posttypeList = posttypeList.FindAll(o => o.PostID != o.EnPostCode);
                }
                clsPosttypes.DataSource = posttypeList;
                clsPosttypes.ValueMember = "PostID";
                clsPosttypes.DisplayMember = "CnPostName";
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
                var solution = OrderSortService.GetSolutionPostTypeListByLatticeSettingId(_latticesetting.ID);
                if (solution == null || solution.Count < 1)
                    return;
                //选项
                for (int i = 0; i < clsPosttypes.Items.Count; i++)
                {
                    var pt = clsPosttypes.Items[i] as Posttypes;
                    if (solution.Exists(s => s.PostTypeId == pt.PostID))
                    {
                        clsPosttypes.SetItemChecked(i, true);
                        clsPosttypes.SetSelected(i, true);
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
                var list = new List<Posttypes>();
                foreach (var item in clsPosttypes.CheckedItems)
                {
                    list.Add(item as Posttypes);
                }
                OrderSortService.UpdateSolutionPostType(_latticesetting, list);
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
                    for (int i = 0; i < clsPosttypes.Items.Count; i++)
                    {
                        var ptStr = clsPosttypes.GetItemText(clsPosttypes.Items[i]);
                        if (ptStr.Equals(txtSearch.Text))
                        {
                            clsPosttypes.SetSelected(i, true);
                            return;
                        }
                        if (ptStr.StartsWith(txtSearch.Text))
                        {
                            clsPosttypes.SetSelected(i, true);
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
