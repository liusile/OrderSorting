using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.BLL.Model;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Forms;

namespace SCB.OrderSorting.Client
{
    public partial class frmSystemSetting : Form
    {
        private SystemSetting _systemSetting;
        private List<KeyValuePair<int, string>> parityList = new List<KeyValuePair<int, string>>();
        private List<KeyValuePair<int, string>> stopbitsList = new List<KeyValuePair<int, string>>();
        private List<KeyValuePair<int, string>> pattenList = new List<KeyValuePair<int, string>>();
        private List<KeyValuePair<int, string>> daysList = new List<KeyValuePair<int, string>>();
        private bool _isLoaded = false;

        public frmSystemSetting()
        {
            InitializeComponent();
            initializeControl();
        }

        private void initializeControl()
        {
            try
            {
                //初始化Parity选项   
                var parityArray = Enum.GetValues(typeof(Parity));
                foreach (var enumItem in parityArray)
                {
                    parityList.Add(new KeyValuePair<int, string>((int)enumItem, enumItem.ToString()));
                }
                cbParity.DataSource = parityList;
                cbParity.DisplayMember = "Value";
                cbParity.ValueMember = "Key";
                cbParity.SelectedValue = 0;
                //初始化StopBitsList选项   
                var stopbitsArray = Enum.GetValues(typeof(StopBits));
                foreach (var enumItem in stopbitsArray)
                {
                    stopbitsList.Add(new KeyValuePair<int, string>((int)enumItem, enumItem.ToString()));
                }
                cbStopBits.DataSource = stopbitsList;
                cbStopBits.DisplayMember = "Value";
                cbStopBits.ValueMember = "Key";
                cbStopBits.SelectedValue = 0;
                //初始化投递模式
                var pattenArray = Enum.GetValues(typeof(SortingPatten_Enum));
                foreach (var enumItem in pattenArray)
                {
                    pattenList.Add(new KeyValuePair<int, string>((int)enumItem, enumItem.ToString()));
                }
                cbSortingPatten.DataSource = pattenList;
                cbSortingPatten.DisplayMember = "Value";
                cbSortingPatten.ValueMember = "Key";
                cbSortingPatten.SelectedValue = 0;
                //初始化cbSortingSolution
                var gssList = OrderSortService.GetSortingSolutionsList();
                cbSortingSolution.DataSource = gssList;
                cbSortingSolution.ValueMember = "Id";
                cbSortingSolution.DisplayMember = "Name";
                //初始化记录保留天数
                var dayArray = Enum.GetValues(typeof(SystemSetting_Status_Enum));
                foreach (var enumItem in dayArray)
                {
                    daysList.Add(new KeyValuePair<int, string>((int)enumItem, enumItem.ToString()));
                }
                cbLogStorageDays.DataSource = daysList;
                cbLogStorageDays.DisplayMember = "Value";
                cbLogStorageDays.ValueMember = "Key";
                cbLogStorageDays.SelectedValue = 0;
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
                _systemSetting.ModbusSetting.PortName = txtPortName.Text;
                _systemSetting.ModbusSetting.BaudRate = Convert.ToInt32(txtBaudRate.Text);
                _systemSetting.ModbusSetting.Parity = Convert.ToInt32(cbParity.SelectedValue);
                _systemSetting.ModbusSetting.DataBits = Convert.ToInt32(txtDataBits.Text);
                _systemSetting.ModbusSetting.StopBits = Convert.ToInt32(cbStopBits.SelectedValue);
                _systemSetting.ModbusSetting.LEDStartAddress = Convert.ToUInt16(txtLEDStartAddress.Text);
                _systemSetting.ModbusSetting.GratingStartAddress = Convert.ToUInt16(txtGratingStartAddress.Text);
                _systemSetting.ModbusSetting.ButtonStartAddress = Convert.ToUInt16(txtButtonStartAddress.Text);
                _systemSetting.ModbusSetting.ResetGratingStartAddress = Convert.ToUInt16(txtResetGratingStartAddress.Text);
                _systemSetting.ModbusSetting.WarningRedLightStartAddress = Convert.ToUInt16(txtWarningRedLightStartAddress.Text);
                _systemSetting.ModbusSetting.WarningGreenLightStartAddress = Convert.ToUInt16(txtWarningGreenLightStartAddress.Text);
                _systemSetting.ModbusSetting.WarningYellowLightStartAddress = Convert.ToUInt16(txtWarningYellowLightStartAddress.Text);
                _systemSetting.ModbusSetting.WarningBuzzerStartAddress = Convert.ToUInt16(txtWarningBuzzerStartAddress.Text);
                _systemSetting.SortingPatten = Convert.ToInt32(cbSortingPatten.SelectedValue);
                _systemSetting.SortingSolution = cbSortingSolution.SelectedValue.ToString();
                _systemSetting.CabinetNumber = Convert.ToUInt16(cbCabinetNumber.Text);
                _systemSetting.CriticalWeight = Convert.ToDecimal(txtCriticalWeight.Text);
                _systemSetting.BoxWeight = Convert.ToDecimal(txtBoxWeight.Text);
                _systemSetting.WarningCabinetId = Convert.ToUInt16(cbWarningCabinetId.Text);
                _systemSetting.LogStorageDays = Convert.ToInt32(cbLogStorageDays.SelectedValue);
                _systemSetting.IsFlyt = cbIsFlyt.Text == "是";
                _systemSetting.SlaveConfigs.Find(s => s.CabinetId == 1).SlaveAddress = Convert.ToByte(txtCabinetId1_SlaveAddress.Text);
                _systemSetting.SlaveConfigs.Find(s => s.CabinetId == 2).SlaveAddress = Convert.ToByte(txtCabinetId2_SlaveAddress.Text);
                _systemSetting.SlaveConfigs.Find(s => s.CabinetId == 3).SlaveAddress = Convert.ToByte(txtCabinetId3_SlaveAddress.Text);
                _systemSetting.SlaveConfigs.Find(s => s.CabinetId == 4).SlaveAddress = Convert.ToByte(txtCabinetId4_SlaveAddress.Text);
                OrderSortService.SaveSystemSetting(_systemSetting);
               
                Close();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void frmModbusSetting_Load(object sender, EventArgs e)
        {
            try
            {
                //数据加载
                LoadModbusSetting();
                _isLoaded = true;
                SetCabinetTextAnable();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadModbusSetting()
        {
            _systemSetting = OrderSortService.GetSystemSettingCache();
            txtPortName.Text = _systemSetting.ModbusSetting.PortName;
            txtBaudRate.Text = _systemSetting.ModbusSetting.BaudRate.ToString();
            cbParity.SelectedValue = _systemSetting.ModbusSetting.Parity;
            txtDataBits.Text = _systemSetting.ModbusSetting.DataBits.ToString();
            cbStopBits.SelectedValue = _systemSetting.ModbusSetting.StopBits;
            txtLEDStartAddress.Text = _systemSetting.ModbusSetting.LEDStartAddress.ToString();
            txtGratingStartAddress.Text = _systemSetting.ModbusSetting.GratingStartAddress.ToString();
            txtButtonStartAddress.Text = _systemSetting.ModbusSetting.ButtonStartAddress.ToString();
            txtResetGratingStartAddress.Text = _systemSetting.ModbusSetting.ResetGratingStartAddress.ToString();
            txtWarningRedLightStartAddress.Text = _systemSetting.ModbusSetting.WarningRedLightStartAddress.ToString();
            txtWarningGreenLightStartAddress.Text = _systemSetting.ModbusSetting.WarningGreenLightStartAddress.ToString();
            txtWarningYellowLightStartAddress.Text = _systemSetting.ModbusSetting.WarningYellowLightStartAddress.ToString();
            txtWarningBuzzerStartAddress.Text = _systemSetting.ModbusSetting.WarningBuzzerStartAddress.ToString();
            txtNumberOfPoints.Text = _systemSetting.ModbusSetting.NumberOfPoints.ToString();
            cbSortingPatten.SelectedValue = _systemSetting.SortingPatten;
            cbSortingSolution.SelectedValue = _systemSetting.SortingSolution;
            cbCabinetNumber.Text = _systemSetting.CabinetNumber.ToString();
            cbWarningCabinetId.Text = _systemSetting.WarningCabinetId.ToString();
            txtCriticalWeight.Text = _systemSetting.CriticalWeight.ToString("0.000");
            txtBoxWeight.Text = _systemSetting.BoxWeight.ToString("0.000");
            cbLogStorageDays.SelectedValue = _systemSetting.LogStorageDays;
            cbIsFlyt.Text = _systemSetting.IsFlyt ? "是" : "否";
            txtCabinetId1_SlaveAddress.Text = _systemSetting.SlaveConfigs.Find(s => s.CabinetId == 1).SlaveAddress.ToString();
            txtCabinetId2_SlaveAddress.Text = _systemSetting.SlaveConfigs.Find(s => s.CabinetId == 2).SlaveAddress.ToString();
            txtCabinetId3_SlaveAddress.Text = _systemSetting.SlaveConfigs.Find(s => s.CabinetId == 3).SlaveAddress.ToString();
            txtCabinetId4_SlaveAddress.Text = _systemSetting.SlaveConfigs.Find(s => s.CabinetId == 4).SlaveAddress.ToString();
        }

        private void cbCabinetNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!_isLoaded)
                    return;
                SetCabinetTextAnable();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void SetCabinetTextAnable()
        {
            switch (cbCabinetNumber.Text)
            {
                case "1":
                    txtCabinetId1_SlaveAddress.Enabled = true;
                    txtCabinetId2_SlaveAddress.Enabled = false;
                    txtCabinetId3_SlaveAddress.Enabled = false;
                    txtCabinetId4_SlaveAddress.Enabled = false;
                    break;
                case "2":
                    txtCabinetId1_SlaveAddress.Enabled = true;
                    txtCabinetId2_SlaveAddress.Enabled = true;
                    txtCabinetId3_SlaveAddress.Enabled = false;
                    txtCabinetId4_SlaveAddress.Enabled = false;
                    break;
                case "3":
                    txtCabinetId1_SlaveAddress.Enabled = true;
                    txtCabinetId2_SlaveAddress.Enabled = true;
                    txtCabinetId3_SlaveAddress.Enabled = true;
                    txtCabinetId4_SlaveAddress.Enabled = false;
                    break;
                case "4":
                    txtCabinetId1_SlaveAddress.Enabled = true;
                    txtCabinetId2_SlaveAddress.Enabled = true;
                    txtCabinetId3_SlaveAddress.Enabled = true;
                    txtCabinetId4_SlaveAddress.Enabled = true;
                    break;
            }
        }

        private void btnSortingSolution_Click(object sender, EventArgs e)
        {
            frmSelectSolution frm = new frmSelectSolution(_systemSetting.SortingSolution);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                cbSortingSolution.SelectedValue = frm.GetSelectedSolution();
            }
        }
    }
}
