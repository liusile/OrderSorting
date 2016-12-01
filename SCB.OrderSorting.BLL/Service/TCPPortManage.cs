using SCB.OrderSorting.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCB.OrderSorting.BLL.Service
{
    public class TCPPortManage
    {
        private List<KeyValuePair<byte, TCPPortService>> TCPPortServiceList { get; set; } = new List<KeyValuePair<byte, TCPPortService>>();
        private TCPPortManage() { }
        private TCPPortManage(SystemSetting sysSetting)
        {
             sysSetting.SlaveConfigs
                       .Where(s => s.SlaveAddress > 0 && s.CabinetId <= sysSetting.CabinetNumber)
                       .ToList()
                       .ForEach(o => 
                            TCPPortServiceList.Add(new KeyValuePair<byte, TCPPortService>(o.SlaveAddress, new TCPPortService(sysSetting.ModbusSetting, o,sysSetting.WarningCabinetId==o.CabinetId)))
                       );
        }
        public static TCPPortManage Instance(SystemSetting sysSetting)
        {
            return new TCPPortManage(sysSetting);
        }
        public  TCPPortService GetTCPPortService(byte SlaveAddress)
        {
            return TCPPortServiceList.Find(o => o.Key == SlaveAddress).Value;
        }
       
        public TCPPortService GetMasterTCPPortService()
        {
            return TCPPortServiceList.Find(o => o.Value.isMaster==true).Value;
        }
        //--------------------------------------------------------光栅-----------------------------------------------

        public  ushort[] ReadGratingRegisters(byte slaveAddress)
        {
            var TCPPortService = GetTCPPortService(slaveAddress);
            return TCPPortService.ReadGratingRegisterAll();
        }
        public  ushort ReadGratingRegisters(byte slaveAddress, ushort gratingIndex)
        {
            var TCPPortService = GetTCPPortService(slaveAddress);
            return TCPPortService.ReadGratingRegister(gratingIndex);
        }
        public  void ClearGrating(byte slaveAddress)
        {
            var TCPPortService = GetTCPPortService(slaveAddress);
            TCPPortService.ClearGratingRegisterAll();
        }
        public void ClearGrating(byte slaveAddress,ushort gratingIndex,bool isCheck=true)
        {
            var TCPPortService = GetTCPPortService(slaveAddress);
            TCPPortService.ClearGratingRegister(gratingIndex,  isCheck );
        }
        public  void ClearGrating()
        {
            TCPPortServiceList.AsParallel().ForAll(o => o.Value.ClearGratingRegisterAll());
        }
        //--------------------------------------------------------按扭-----------------------------------------------
        public  ushort[] ReadButtonRegisters(byte slaveAddress)
        {
            var TCPPortService = GetTCPPortService(slaveAddress);
            return TCPPortService.ReadButtonRegisterAll();
        }
        public  ushort ReadButtonRegisters(byte slaveAddress, ushort buttonIndex)
        {
            var TCPPortService = GetTCPPortService(slaveAddress);
            return TCPPortService.ReadButtonRegister(buttonIndex);
        }
        public  void ClearButton(byte slaveAddress)
        {
            var TCPPortService = GetTCPPortService(slaveAddress);
            TCPPortService.ClearButtonAll();
        }
        public void ClearButton(byte slaveAddress,int buttonIndex)
        {
            var TCPPortService = GetTCPPortService(slaveAddress);
            TCPPortService.ClearButton(buttonIndex);
        }
        public  void ClearButton()
        {
            TCPPortServiceList.AsParallel().ForAll(o => o.Value.ClearButtonAll());
        }
        //--------------------------------------------------------光栅2按扭-----------------------------------------------
        public  void ClearGrating2Button(byte slaveAddress)
        {
            ClearGrating(slaveAddress);
            ClearButton(slaveAddress);
        }
        public  void ClearGrating2Button()
        {
            TCPPortServiceList.AsParallel().ForAll(o => ClearGrating2Button(o.Key));
        }
        //--------------------------------------------------------LED-----------------------------------------------
        public void ClearLED()
        {
            TCPPortServiceList.AsParallel().ForAll(o => ClearLED(o.Key));
        }
        public  void ClearLED(byte slaveAddress)
        {
            var TCPPortService = GetTCPPortService(slaveAddress);
            TCPPortService.ClearLEDAll();
        }
        public void SetLEDAll(byte slaveAddress,ushort value)
        {
            var TCPPortService = GetTCPPortService(slaveAddress);
            TCPPortService.SetLEDAll(value);
        }
        public void SetLED(byte slaveAddress, int LEDIndex,ushort value)
        {
            var TCPPortService = GetTCPPortService(slaveAddress);
            TCPPortService.SetLED(LEDIndex, value);
        }
        public void SetLED(byte slaveAddress, IList<KeyValuePair<int, ushort>> registerChangeList)
        {
            var TCPPortService = GetTCPPortService(slaveAddress);
            TCPPortService.SetLED(registerChangeList);
        }
        //--------------------------------------------------------警示灯-----------------------------------------------
        public void SetWarningLightRed(ushort value)
        {
            var TCPPortService = GetMasterTCPPortService();
            TCPPortService.SetRedWarningLight(value);
        }
        public void SetWarningTwinkle(ushort value)
        {
            var TCPPortService = GetMasterTCPPortService();
            TCPPortService.SetTwinkleWarningLight(value);
        }
        public void SetWarningLightYellow(ushort value)
        {
            var TCPPortService = GetMasterTCPPortService();
            TCPPortService.SetYellowWarningLight(value);
        }
        public void SetWarningLightGreen(ushort value)
        {
            var TCPPortService = GetMasterTCPPortService();
            TCPPortService.SetGreenWarningLight(value);
        }
        public void SetWarningLightAll(ushort value)
        {
            var TCPPortService = GetMasterTCPPortService();
            TCPPortService.SetAllWarningLight(value);
        }
        public void SetWarningAll(ushort value)
        {
            var TCPPortService = GetMasterTCPPortService();
            TCPPortService.SetAllWarning(value);
        }
        public void SetWarningAll(ushort red, ushort green, ushort yellow, ushort twinkle)
        {
            var TCPPortService = GetMasterTCPPortService();
            TCPPortService.SetAllWarning(red,green,yellow,twinkle);
        }
        public bool isCollect()
        {
            foreach(var kv in TCPPortServiceList) {
                var TCPPortService = GetTCPPortService(kv.Key);
                bool isSuccess=TCPPortService.isCollect();
                if (!isSuccess) return false;
            }
            return true;
        }
    }
}
