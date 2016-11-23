using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCB.OrderSorting.DAL;
using SCB.OrderSorting.BLL.Model;
using System.Net.Sockets;
using Modbus.Device;
using System.Threading;

namespace SCB.OrderSorting.BLL.Service
{
    public class TCPPortService : ISlavePortService
    {
        private SlaveConfig slaveConfig { get; set; }
        private Modbussetting modbus { get; set; }
        private int writeTimeout { get; set; } = 20;
        private int readTimeout { get; set; } = 20;
        private int tryCount { get; set; } = 10;
        public bool isMaster { get; private set; }

        public TCPPortService(Modbussetting modbus, SlaveConfig slaveConfig,bool isMaster)
        {
            this.modbus = modbus;
            this.slaveConfig = slaveConfig;
            this.isMaster = isMaster;
        }
        public void ClearGratingRegister(ushort gratingIndex)
        {

            var address = modbus.ResetGratingStartAddress + gratingIndex;
            var addressRead = modbus.GratingStartAddress + gratingIndex;
            ushort[] data = {0};
            while (true)
            {
                var read1 = ReadRegisters((ushort)addressRead, 1)[0];
                var read2 = ReadRegisters((ushort)addressRead, 1)[0];
                if (read1 == read2)
                {
                    //throw new Exception("不稳定数据！");
                    break;
                }
            }
            WriteRegisters((ushort)address, data);
            //var result = ReadRegistersCheck((ushort)addressRead, 1);
            //if (!result)
            //{
            //    throw new Exception("清除光栅失败！");
            //}
        }

        public void ClearGratingRegisterAll()
        {
            var address = modbus.ResetGratingStartAddress;
            ushort[] data = { 0,0,0,0,0,0,0,0,0,0,0,0 };
            WriteRegisters((ushort)address, data);
        }

        public void ClearLED(ushort LEDIndex)
        {
            var address = modbus.LEDStartAddress + LEDIndex;
            ushort[] data = { 0 };
            WriteRegisters((ushort)address, data);
        }

        public void ClearLEDAll()
        {
            var address = modbus.LEDStartAddress;
            ushort[] data = { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
            WriteRegisters((ushort)address, data);
        }

        public ushort ReadButtonRegister(ushort buttonIndex)
        {
            var address = modbus.ResetButtonStartAddress + buttonIndex;
            ushort num = 1;
            return ReadRegisters((ushort)address, num)[0];
        }

        public ushort[] ReadButtonRegisterAll()
        {
            var address = modbus.ButtonStartAddress;
            ushort num = modbus.NumberOfPoints;
            return ReadRegisters((ushort)address, num);
        }

        public ushort ReadGratingRegister(ushort gratingIndex)
        {
            var address = modbus.GratingStartAddress+ gratingIndex;
            ushort num = 1;
            return ReadRegisters((ushort)address, num)[0];
        }

        public ushort[] ReadGratingRegisterAll()
        {
            //var address = modbus.GratingStartAddress;
            //ushort num = modbus.NumberOfPoints;
            //return ReadRegisters((ushort)address, num);
            while (true)
            {
                var address = modbus.GratingStartAddress;
                ushort num = modbus.NumberOfPoints;
                var result1 = ReadRegisters((ushort)address, num);
                var result2 = ReadRegisters((ushort)address, num);
                var eqnum = result1.Where(o => result2.Contains(o)).Count();
                if (eqnum == result1.Count())
                {
                    return result1;
                }
            }
        }

        public void SetAllWarning(ushort value)
        {
            //40112   警示灯红
            //40113   警示灯绿
            //40114   警示灯黄
            //40115   警示灯蜂鸣器
            var address = modbus.WarningRedLightStartAddress;//红灯是起始地址
            ushort[] data = { value, value, value, value };
            WriteRegisters((ushort)address, data);
        }
        public void SetAllWarning(ushort red, ushort green, ushort yellow, ushort Twinkle)
        {
            //40112   警示灯红
            //40113   警示灯绿
            //40114   警示灯黄
            //40115   警示灯蜂鸣器
            var address = modbus.WarningRedLightStartAddress;//红灯是起始地址
            ushort[] data = { red, green, yellow, Twinkle };
            WriteRegisters((ushort)address, data);
        }
        public void SetAllWarningLight(ushort red,ushort green,ushort yellow)
        {
            var address = modbus.WarningRedLightStartAddress;//红灯是起始地址
            ushort[] data = { red, green, yellow };
            WriteRegisters((ushort)address, data);
        }

        public void SetAllWarningLight(ushort value)
        {
            var address = modbus.WarningRedLightStartAddress;//红灯是起始地址
            ushort[] data = { value, value, value };
             WriteRegisters((ushort)address, data);
        }

        public void SetButton(IList<KeyValuePair<int, ushort>> registerChangeList)
        {
            var address = modbus.ResetButtonStartAddress;
            ushort[] data = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach(KeyValuePair<int,ushort> kv in registerChangeList)
            {
                int index = kv.Key;
                data[index] = kv.Value;
            }
            WriteRegisters((ushort)address, data);
        }

        public void SetButton(LatticeSetting lattice, ushort value)
        {
            var address = modbus.ResetButtonStartAddress + lattice.ButtonIndex;
            ushort[] data = { value };
            WriteRegisters((ushort)address, data);
        }

        public void SetButton( int buttonIndex, ushort value)
        {
            var address = modbus.ResetButtonStartAddress + buttonIndex;
            ushort[] data = { value };
            WriteRegisters((ushort)address, data);
        }

        public void SetButtonAll(ushort value)
        {
            var address = modbus.ResetButtonStartAddress;
            ushort[] data = { value, value, value, value, value, value, value, value, value, value, value, value };
            WriteRegisters((ushort)address, data);
        }
        public void ClearButton(int buttonIndex)
        {
            var address = modbus.ResetButtonStartAddress + buttonIndex;
            ushort[] data = { 0 };
            WriteRegisters((ushort)address, data);
        }
        public void ClearButtonAll()
        {
            var address = modbus.ResetButtonStartAddress;
            ushort[] data = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
             WriteRegisters((ushort)address, data);
        }
        public void SetGratingRegister(ushort gratingIndex, ushort value)
        {
            var address = modbus.ResetGratingStartAddress + gratingIndex;
            ushort[] data = { value };
            WriteRegisters((ushort)address, data);
        }

        public void SetGratingRegisterAll(ushort value)
        {
            var address = modbus.ResetGratingStartAddress;
            ushort[] data = { value, value, value, value, value, value, value, value, value, value, value, value };
            WriteRegisters((ushort)address, data);
        }

        public void SetGreenWarningLight(ushort value)
        {
            var address = modbus.WarningGreenLightStartAddress;
            ushort[] data = { value };
            WriteRegisters((ushort)address, data);
        }

        public void SetLED( IList<KeyValuePair<int, ushort>> registerChangeList)
        {
            var addressS = modbus.LEDStartAddress;
            
            foreach(var kv in registerChangeList)
            {
                int index = kv.Key;
                var address = addressS + index;
                ushort[] value = { kv.Value };
                WriteRegisters((ushort)address, value);
            }
           
        }

        public void SetLED(LatticeSetting lattice, ushort value)
        {
            var address = modbus.ButtonStartAddress + lattice.ButtonIndex;
            ushort[] data = { value };
             WriteRegisters((ushort)address, data);
        }

        public void SetLED(int LEDIndex, ushort value)
        {
            var address = modbus.LEDStartAddress + LEDIndex;
            ushort[] data = { value };
             WriteRegisters((ushort)address, data);
        }

        public void SetLEDAll(ushort value)
        {
            var address = modbus.LEDStartAddress;
            ushort[] data = { value, value, value, value, value, value, value, value, value, value, value, value };
             WriteRegisters((ushort)address, data);
        }

        public void SetRedWarningLight(ushort value)
        {
            var address = modbus.WarningRedLightStartAddress;
            ushort[] data = { value };
             WriteRegisters((ushort)address, data);
        }

        public void SetTwinkleWarningLight(ushort value)
        {
            var address = modbus.WarningBuzzerStartAddress;
            ushort[] data = { value };
             WriteRegisters((ushort)address, data);
        }

        public void SetYellowWarningLight(ushort value)
        {
            var address = modbus.WarningYellowLightStartAddress ;
            ushort[] data = { value };
             WriteRegisters((ushort)address, data);
        }

        private void WriteRegisters(ushort address, ushort[] data)
        {
            for (int i = 0; i < tryCount; i++)
            {
                try
                {
                    using (TcpClient client = new TcpClient(slaveConfig.TCPHost, slaveConfig.TCPPort))
                    {
                        IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(client);
                        master.Transport.WriteTimeout = writeTimeout;
                        master.Transport.ReadTimeout = readTimeout;
                        master.WriteMultipleRegisters(slaveConfig.SlaveAddress, address, data);
                        return;
                    }
                }
                catch { }
            }
            using (TcpClient client = new TcpClient(slaveConfig.TCPHost, slaveConfig.TCPPort))
            {
                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(client);
                master.Transport.WriteTimeout = writeTimeout;
                master.Transport.ReadTimeout = readTimeout;
                master.WriteMultipleRegisters(slaveConfig.SlaveAddress, address, data);
            }
        }
     
        private ushort[] ReadRegisters(ushort address, ushort num)
        {
            for (int i = 0; i < tryCount; i++)
            {
                try
                {
                    using (TcpClient client = new TcpClient(slaveConfig.TCPHost, slaveConfig.TCPPort))
                    {
                        IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(client);
                        master.Transport.WriteTimeout = writeTimeout;
                        master.Transport.ReadTimeout = readTimeout;
                        return master.ReadHoldingRegisters(slaveConfig.SlaveAddress, address, num);
                    }
                }
                catch { }
            }
            using (TcpClient client = new TcpClient(slaveConfig.TCPHost, slaveConfig.TCPPort))
            {
                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(client);
                master.Transport.WriteTimeout = writeTimeout;
                master.Transport.ReadTimeout = readTimeout;
                return master.ReadHoldingRegisters(slaveConfig.SlaveAddress, address, num);
            }
        }
        private bool ReadRegistersCheck(ushort address, ushort num)
        {
            for (int i = 0; i < tryCount; i++)
            {
                try
                {
                    using (TcpClient client = new TcpClient(slaveConfig.TCPHost, slaveConfig.TCPPort))
                    {
                        IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(client);
                        master.Transport.WriteTimeout = writeTimeout;
                        master.Transport.ReadTimeout = readTimeout;
                        var result= master.ReadHoldingRegisters(slaveConfig.SlaveAddress, address, num);
                        if (result.Max() > 0)
                        {
                            Thread.Sleep(10);
                        }
                    }
                }
                catch { }
            }
            using (TcpClient client = new TcpClient(slaveConfig.TCPHost, slaveConfig.TCPPort))
            {
                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(client);
                master.Transport.WriteTimeout = writeTimeout;
                master.Transport.ReadTimeout = readTimeout;
                var result = master.ReadHoldingRegisters(slaveConfig.SlaveAddress, address, num);
                if (result.Max() > 0)
                {
                    return false;
                }else
                {
                    return true;
                }
            }
        }
        public bool isCollect()
        {
            using (TcpClient client = new TcpClient(slaveConfig.TCPHost, slaveConfig.TCPPort))
            {
                return true;
            }
        }
        
    }
}
