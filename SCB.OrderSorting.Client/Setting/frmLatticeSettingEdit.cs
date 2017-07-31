using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.BLL.Model;
using SCB.OrderSorting.Client.Setting;
using SCB.OrderSorting.DAL;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    public partial class frmLatticeSettingEdit : Form
    {
        private int _latticesettingId;
        private LatticeSetting _latticesetting;
        private string PostTypeId;
        public frmLatticeSettingEdit(int id)
        {
            _latticesettingId = id;
            InitializeComponent();
            if (frmOrderSortingWorkNew._UserInfo?.UserName == "admin")
            {
                txtLEDIndex.ReadOnly = false;
                txtGratingIndex.ReadOnly = false;
                txtButtonIndex.ReadOnly = false;
                btnClear.Visible = true;
            }
            //InitializeControl();
        }

        //private void InitializeControl()
        //{
        //    statusList = new List<KeyValuePair<int, string>>();
        //    //初始化问题类型选项   
        //    var enumArray = Enum.GetValues(typeof(SystemSetting_Status_Enum));
        //    foreach (var enumItem in enumArray)
        //    {
        //        statusList.Add(new KeyValuePair<int, string>((int)enumItem, enumItem.ToString()));
        //    }
        //    cbStatus.DataSource = statusList;
        //    cbStatus.DisplayMember = "Value";
        //    cbStatus.ValueMember = "Key";
        //    cbStatus.SelectedValue = 0;
        //}

        private void frmLatticeSettingEdit_Load(object sender, EventArgs e)
        {
            try
            {
                //lattice
                _latticesetting = OrderSortService.GetLatticeSettingById(_latticesettingId);
                txtCabinetId.Text = _latticesetting.CabinetId.ToString();
                txtLatticeId.Text = _latticesetting.LatticeId;
                //cbStatus.SelectedValue = _latticesetting.Status;
                txtLEDIndex.Text = _latticesetting.LEDIndex.ToString();
                txtGratingIndex.Text = _latticesetting.GratingIndex.ToString();
                txtButtonIndex.Text = _latticesetting.ButtonIndex.ToString();
                txt_PrintNum.Text = (_latticesetting.PrintNum??1).ToString();
                cbIsEnable.Checked = Convert.ToBoolean(_latticesetting.IsEnable);
                switch (OrderSortService.GetSortingPatten())
                {
                    case 1:
                        LoadSolutionPostType();
                        break;
                    case 2:
                        LoadSolutionCountry();
                        break;
                    case 3:
                        LoadSolutionPostType();
                        LoadSolutionCountry();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int printNum = int.Parse(txt_PrintNum.Text);
                if (string.IsNullOrWhiteSpace(txtLatticeId.Text))
                {
                    throw new Exception("格号禁止为空！");
                }
                if (txtLatticeId.Text != _latticesetting.LatticeId && OrderSortService.IsLatticeIdExists(txtLatticeId.Text))
                {
                    throw new Exception("存在重复格号" + txtLatticeId.Text);
                }
                
                if (printNum <= 0)
                {
                    throw new Exception("打印数量必须大于0!");
                }
                _latticesetting.PrintNum = printNum;
                _latticesetting.LatticeId = txtLatticeId.Text;
                _latticesetting.LEDIndex = Convert.ToInt32(txtLEDIndex.Text);
                _latticesetting.GratingIndex = Convert.ToInt32(txtGratingIndex.Text);
                _latticesetting.ButtonIndex = Convert.ToInt32(txtButtonIndex.Text);
                _latticesetting.IsEnable = cbIsEnable.Checked.ToString();
                OrderSortService.SaveLatticeSetting(_latticesetting);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                Invoke((MethodInvoker)delegate () { MessageBox.Show(ex.Message); });
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLatticeId.Text = "";
            //cbPosttype.SelectedValue = "0";
            //cbCountry.SelectedValue = "0";
            txtLEDIndex.Text = "-1";
            txtGratingIndex.Text = "-1";
            txtButtonIndex.Text = "-1";
            //cbStatus.SelectedValue = "0";
        }

        private void btnCountrys_Click(object sender, EventArgs e)
        {
            frmSelectCountrys frm = new frmSelectCountrys(_latticesetting);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadSolutionCountry();
            }
        }
        /// <summary>
        /// select postType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPostTypes_Click(object sender, EventArgs e)
        {
            frmSelectPostTypes frm = new frmSelectPostTypes(_latticesetting);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadSolutionPostType();
            }
        }
        //初始化渠道地区
        private void LoadSolutionPostType()
        {
            //渠道
            var solution = OrderSortService.GetSolutionPostTypeListByLatticeSettingId(_latticesetting.ID);
            txtPostTypes.Text = string.Join(",", solution.Select(s => s.PostTypeName));
            PostTypeId = string.Join(",", solution.Select(s => s.PostTypeId));
            //地区
            var solutionArea = OrderSortService.GetSolutionPostAreaListByLatticeSettingId(solution.FirstOrDefault()?.PostTypeId, _latticesettingId);
            txtPostAreas.Text = string.Join(",", solutionArea.Select(s => s.Area));

            btnPostTypes.Enabled = true;
            btnPostAreas.Enabled = true;
        }

        private void LoadSolutionCountry()
        {
            var solution = OrderSortService.GetSolutionCountryListByLatticeSettingId(_latticesetting.ID);
            txtCountrys.Text = string.Join(",", solution.Select(s => s.CountryName));
            btnCountrys.Enabled = true;
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            byte SlaveAddress = OrderSortService.GetSlaveConfig().Find(o => o.CabinetId == _latticesetting.CabinetId).SlaveAddress;
            OrderSortService.SerialPortService.SetLED(SlaveAddress, _latticesetting.LEDIndex,2);
        }

        private void btnTest0_Click(object sender, EventArgs e)
        {
            byte SlaveAddress = OrderSortService.GetSlaveConfig().Find(o => o.CabinetId == _latticesetting.CabinetId).SlaveAddress;
            OrderSortService.SerialPortService.SetLED(SlaveAddress, _latticesetting.LEDIndex, 0);
        }

        private void btnTest1_Click(object sender, EventArgs e)
        {
            byte SlaveAddress = OrderSortService.GetSlaveConfig().Find(o => o.CabinetId == _latticesetting.CabinetId).SlaveAddress;
            OrderSortService.SerialPortService.SetLED(SlaveAddress, _latticesetting.LEDIndex, 1);
        }

        private void btnTest3_Click(object sender, EventArgs e)
        {
            byte SlaveAddress = OrderSortService.GetSlaveConfig().Find(o => o.CabinetId == _latticesetting.CabinetId).SlaveAddress;
            OrderSortService.SerialPortService.SetLED(SlaveAddress, _latticesetting.LEDIndex, 3);
        }
        /// <summary>
        /// 选择渠道地区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPostAreas_Click(object sender, EventArgs e)
        {
          
            if (string.IsNullOrWhiteSpace(PostTypeId)) {
                MessageBox.Show("请先选择渠道！");
                return;
            }
            if (PostTypeId.IndexOf(',')>0)
            {
                MessageBox.Show("只允许为一个渠道选择渠道地区！");
                return;
            }
            frmSelectAreaTypes frm = new frmSelectAreaTypes(_latticesetting,PostTypeId);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadSolutionPostType();
            }
        }

        private void cbIsEnable_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
