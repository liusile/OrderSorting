using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    public partial class frmSelectPostTypesForZip : Form
    {
        public List<Posttypes> PosttypesList;
        private int latticesettingId;

        public frmSelectPostTypesForZip(int latticesettingId)
        {
            this.latticesettingId = latticesettingId;
            InitializeComponent();
        }

        private void frmSelectPostTypes_Load(object sender, EventArgs e)
        {
            try
            {
                //clsPosttypes
                var posttypeList = OrderSortService.GetPostTypesList();
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
                var solution = OrderSortService.GetSolutionZipType(latticesettingId);
                if (solution == null)
                    return;
                //选项
                for (int i = 0; i < clsPosttypes.Items.Count; i++)
                {
                    var pt = clsPosttypes.Items[i] as Posttypes;
                    if (solution.PostTypeId == pt.PostID)
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
                    PosttypesList = new List<Posttypes>();
                foreach (var item in clsPosttypes.CheckedItems)
                {
                    PosttypesList.Add(item as Posttypes);
                }
                
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
