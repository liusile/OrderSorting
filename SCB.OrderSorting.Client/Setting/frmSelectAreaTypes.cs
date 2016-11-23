using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client.Setting
{
   
    public partial class frmSelectAreaTypes : Form
    {
        private string _postTypeId;
        private LatticeSetting _LatticeSetting;
        public frmSelectAreaTypes(LatticeSetting LatticeSetting, string postTypeId)
        {
            this._postTypeId = postTypeId;
            this._LatticeSetting = LatticeSetting;
            InitializeComponent();
        }

        private void frmSelectAreaTypes_Load(object sender, EventArgs e)
        {
            try
            {
                //clsPosttypes
                var posttypeList = OrderSortService.GetPostAreaList(_postTypeId);
                clsPosttypes.DataSource = posttypeList;
                clsPosttypes.ValueMember = "Flag";
                clsPosttypes.DisplayMember = "Area";
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
                var solution = OrderSortService.GetSolutionPostAreaListByLatticeSettingId(_postTypeId, _LatticeSetting.ID);
                if (solution == null || solution.Count < 1)
                    return;
                //选项
                for (int i = 0; i < clsPosttypes.Items.Count; i++)
                {
                    var pt = clsPosttypes.Items[i] as PostArea;
                    if (solution.Exists(s => s.Flag == pt.Flag))
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
                var list = new List<PostArea>();
                foreach (var item in clsPosttypes.CheckedItems)
                {
                    list.Add(item as PostArea);
                }
                OrderSortService.UpdateSolutionPostArea(_LatticeSetting, list, _postTypeId);
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
                        if (ptStr.Contains(txtSearch.Text))
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
