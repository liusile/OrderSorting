using SCB.OrderSorting.BLL;
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
    public partial class 格口设置 : Form
    {
        private LatticeSetting ls;
        private SolutionZipType sz;

        public int _latticesettingId { get; private set; }
        public 格口设置(int id)
        {
            _latticesettingId = id;
            InitializeComponent();

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string qu = this.cmb_qu.Text;
            bool isEnable =cbIsEnable.Checked;
            if (isEnable && qu == "请选择")
            {
                MessageBox.Show("请选择区！");
                return;
            }
           
            ls.IsEnable = isEnable.ToString();
            OrderSortService.SaveLatticeSetting(ls);

            if (sz == null)
            {
                sz = new SolutionZipType
                {
                    CabinetId = ls.CabinetId,
                    LatticeSettingId = ls.ID,
                    ZipName = qu == "请选择" ? "" : qu,
                    SortingSolutionId = OrderSortService.GetSystemSettingCache().SortingSolution,
                    Id=Guid.NewGuid().ToString()
                };
            }
            else
            {
                sz.ZipName = qu == "请选择" ? "" : qu;
            }
            OrderSortService.SaveSolutionZipType(sz);
            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
             this.DialogResult = DialogResult.Cancel;
        }

        private void 格口设置_Load(object sender, EventArgs e)
        {
            ls = OrderSortService.GetLatticeSettingById(_latticesettingId);
            sz = OrderSortService.GetSolutionZipType(_latticesettingId);

            cbIsEnable.Checked = Convert.ToBoolean(ls.IsEnable);

            var qu8Array = Enum.GetValues(typeof(qu8_Enum));
            List<KeyValuePair<int, string>> qu8List = new List<KeyValuePair<int, string>>();
            foreach (var enumItem in qu8Array)
            {
                qu8List.Add(new KeyValuePair<int, string>((int)enumItem, enumItem.ToString()));
            }
            cmb_qu.DataSource = qu8List;
            cmb_qu.DisplayMember = "Value";
            cmb_qu.ValueMember = "Key";
            cmb_qu.SelectedValue = 0;
            cmb_qu.SelectedValue = qu8List.Find(o => o.Value == sz?.ZipName).Key;
        }
    }
}
