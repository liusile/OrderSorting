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
using SCB.OrderSorting.BLL.Common;
using System.Diagnostics;
using System.Net;

namespace SCB.OrderSorting.BLL.Service
{
    public class TCPPortService : ISlavePortService
    {
        private SlaveConfig slaveConfig { get; set; }
        private Modbussetting modbus { get; set; }
        private int writeTimeout { get; set; } = 60;
        private int readTimeout { get; set; } = 60;
        private int tryCount { get; set; } = 1000;
        public bool isMaster { get; private set; }
        public IModbusSerialMaster masterSocket { get; set; }

        public TCPPortService(Modbussetting modbus, SlaveConfig slaveConfig,bool isMaster)
        {
            this.modbus = modbus;
            this.slaveConfig = slaveConfig;
            this.isMaster = isMaster;
          
        }
    
        public bool CreateSocket()
        {
            try
            {
                //socket
                //Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //IPEndPoint point = new IPEndPoint(IPAddress.Parse(slaveConfig.TCPHost), Convert.ToInt32(slaveConfig.TCPPort));
                //socketSend.Connect(point);
                //masterSocket = ModbusSerialMaster.CreateRtu(socketSend);
                //masterSocket.Transport.WriteTimeout = writeTimeout;
                //masterSocket.Transport.ReadTimeout = readTimeout;

                //tcp
                TcpClient client = new TcpClient(slaveConfig.TCPHost, slaveConfig.TCPPort);
                masterSocket = ModbusSerialMaster.CreateRtu(client);
                masterSocket.Transport.WriteTimeout = writeTimeout;
                masterSocket.Transport.ReadTimeout = readTimeout;
              
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public void ClearGratingRegister(ushort gratingIndex,bool isCheck=true)
        {

            var address = modbus.ResetGratingStartAddress + gratingIndex;
            var addressRead = modbus.GratingStartAddress + gratingIndex;
            ushort[] data = {0};
            //第一种方式：标准验证是否投递完成
            //while (true)
            //{
            //    var read1 = ReadRegisters((ushort)addressRead, 1)[0];
            //    var read2 = ReadRegisters((ushort)addressRead, 1)[0];
            //    if (read1 == read2)
            //    {
            //        break;
            //    }
            //}
            //第二种方式：默认投递过程最长为30毫秒
            //Thread.Sleep(30);

            WriteRegisters((ushort)address, data);
            if (isCheck)
            {
                //第三种方式：清除后再检查是否清除成功，不成功则循环再清
                var isSuccess = ReadRegistersCheck((ushort)addressRead, 1);
                if (!isSuccess)
                {
                    SaveErrLogHelper.SaveErrorLog("清除计数器后在验证有没有清除成功","没有成功，正在重试！");
                    ClearGratingRegister(gratingIndex);
                }
            }
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
            ushort[] data = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };//5不处理
            foreach (var kv in registerChangeList)
            {
                int index = kv.Key;
                data[index] = kv.Value;
              
            }
            WriteRegisters((ushort)addressS, data);
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
           // Stopwatch sw = Stopwatch.StartNew();
            WriteRegisters((ushort)address, data);
          //  SaveErrLogHelper.SaveErrorLog("WriteRegistersLED用时", sw.ElapsedMilliseconds.ToString());
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
                    //using (TcpClient client = new TcpClient(slaveConfig.TCPHost, slaveConfig.TCPPort))
                    //{
                    //    IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(client);
                    //    master.Transport.WriteTimeout = writeTimeout;
                    //    master.Transport.ReadTimeout = readTimeout;
                    //    master.WriteMultipleRegisters(slaveConfig.SlaveAddress, address, data);
                    //    //发布时需删除
                    //     SaveErrLogHelper.SaveErrorLog($"成功需要写的次数:{i}", $"从机：{slaveConfig.SlaveAddress},地址：{address},数据：{string.Join(",",data.Select(o=>o.ToString()))}");
                    //    return;
                    //}
                    //using (Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                    //{
                    //    IPEndPoint point = new IPEndPoint(IPAddress.Parse(slaveConfig.TCPHost), Convert.ToInt32(slaveConfig.TCPPort));
                    //    socketSend.Connect(point);
                    //    IModbusSerialMaster masterSocket = ModbusSerialMaster.CreateRtu(socketSend);
                    //    masterSocket.Transport.WriteTimeout = writeTimeout;
                    //    masterSocket.Transport.ReadTimeout = readTimeout;
                    //    masterSocket.WriteMultipleRegisters(slaveConfig.SlaveAddress, address, data);
                    //}
                    Debug.WriteLine($"{address}正在尝试写，当前次数：{i}");
                    masterSocket.WriteMultipleRegisters(slaveConfig.SlaveAddress, address, data);
                    if (i > 1)
                    {
                        SaveErrLogHelper.SaveErrorLog($"{address}写的次数：{i}", string.Join(",", data.Select(o => o.ToString())));
                    }
                    return;
                }
                catch { }
                
            }
            masterSocket.WriteMultipleRegisters(slaveConfig.SlaveAddress, address, data);
            //using (Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            //{
            //    IPEndPoint point = new IPEndPoint(IPAddress.Parse(slaveConfig.TCPHost), Convert.ToInt32(slaveConfig.TCPPort));
            //    socketSend.Connect(point);
            //    IModbusSerialMaster masterSocket = ModbusSerialMaster.CreateRtu(socketSend);
            //    masterSocket.Transport.WriteTimeout = writeTimeout;
            //    masterSocket.Transport.ReadTimeout = readTimeout;
            //    masterSocket.WriteMultipleRegisters(slaveConfig.SlaveAddress, address, data);
            //}
           
            //using (TcpClient client = new TcpClient(slaveConfig.TCPHost, slaveConfig.TCPPort))
            //{
            //    IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(client);
            //    master.Transport.WriteTimeout = writeTimeout;
            //    master.Transport.ReadTimeout = readTimeout;
            //    master.WriteMultipleRegisters(slaveConfig.SlaveAddress, address, data);
            //}

        }
     
        private ushort[] ReadRegisters(ushort address, ushort num)
        {
            for (int i = 0; i < tryCount; i++)
            {
                try
                {
                    //using (TcpClient client = new TcpClient(slaveConfig.TCPHost, slaveConfig.TCPPort))
                    //{
                    //    IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(client);
                    //    master.Transport.WriteTimeout = writeTimeout;
                    //    master.Transport.ReadTimeout = readTimeout;
                    //    return master.ReadHoldingRegisters(slaveConfig.SlaveAddress, address, num);
                    //}
                    //using (Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                    //{
                    //    IPEndPoint point = new IPEndPoint(IPAddress.Parse(slaveConfig.TCPHost), Convert.ToInt32(slaveConfig.TCPPort));
                    //    socketSend.Connect(point);
                    //    IModbusSerialMaster masterSocket = ModbusSerialMaster.CreateRtu(socketSend);
                    //    masterSocket.Transport.WriteTimeout = writeTimeout;
                    //    masterSocket.Transport.ReadTimeout = readTimeout;
                    //    return masterSocket.ReadHoldingRegisters(slaveConfig.SlaveAddress, address, num);
                    //}
                    return masterSocket.ReadHoldingRegisters(slaveConfig.SlaveAddress, address, num);

                }
                catch { }
                
            }
            return masterSocket.ReadHoldingRegisters(slaveConfig.SlaveAddress, address, num);
        }
        private bool ReadRegistersCheck(ushort address, ushort num)
        {
            for (int i = 0; i < tryCount; i++)
            {
                try
                {
                    //using (TcpClient client = new TcpClient(slaveConfig.TCPHost, slaveConfig.TCPPort))
                    //{
                    //    IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(client);
                    //    master.Transport.WriteTimeout = writeTimeout;
                    //    master.Transport.ReadTimeout = readTimeout;
                    //    var result= master.ReadHoldingRegisters(slaveConfig.SlaveAddress, address, num);
                    //    if (result[0] > 0)
                    //    {

                    //    }
                    //    else
                    //    {
                    //        return true;
                    //    }
                    //}
                    var result = masterSocket.ReadHoldingRegisters(slaveConfig.SlaveAddress, address, num);
                    if (result[0] <= 0)
                    {
                        return true;
                    }
                }
                catch { }
            }
            return masterSocket.ReadHoldingRegisters(slaveConfig.SlaveAddress, address, num)[0] <= 0 ? true : false;
        }
        public bool isCollect()
        {
           return CreateSocket();
        }
        
    }
}
