using Modbus.Device;
using Modbus.Serial;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.BLL.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

namespace SCB.OrderSorting.BLL.Service
{
    public class SerialPortService
    {
        /// <summary>
        /// 表示串行端口资源
        /// </summary>
        private static SerialPort _serialPort { get; set; }
        private static IModbusSerialMaster _modbusSerialMaster { get; set; }
        /// <summary>
        /// modbus设置
        /// </summary>
        private static Modbussetting _ModbusSetting { get; set; }
        /// <summary>
        /// 从机信息
        /// </summary>
        private static List<SlaveConfig> _SlaveConfig { get; set; }
        /// <summary>
        /// 警示灯从机id
        /// </summary>
        private ushort _WarningCabinetId { get; set; }
        /// <summary>
        /// 循环执行次数
        /// </summary>
        public int LoopCount { get; private set; } = 10;
        /// <summary>
        /// 超时时间
        /// </summary>
        private static int TimeoutMilliseconds = 50;

        private SerialPortService()
        {

        }
        /// <summary>
        /// 窗口服务构造函数
        /// </summary>
        /// <param name="modbus">modbus设置</param>[]
        /// <param name="slaveConfig">从机信息</param>
        /// <param name="warningCabinetId">警示灯从机id（1-4）</param>
        /// 
        private SerialPortService(Modbussetting modbus, List<SlaveConfig> slaveConfig, ushort warningCabinetId)
        {
            try
            {
                _ModbusSetting = modbus;
                _SlaveConfig = slaveConfig;
                _WarningCabinetId = warningCabinetId;

                if (_serialPort != null)
                    _serialPort.Dispose();
                _serialPort = new SerialPort(_ModbusSetting.PortName, _ModbusSetting.BaudRate, (Parity)_ModbusSetting.Parity, _ModbusSetting.DataBits, (StopBits)_ModbusSetting.StopBits);
                _modbusSerialMaster = ModbusSerialMaster.CreateRtu(new SerialPortAdapter(_serialPort));

                //设置Modbus通讯的超时时间
                _modbusSerialMaster.Transport.ReadTimeout = TimeoutMilliseconds;
                _modbusSerialMaster.Transport.WriteTimeout = TimeoutMilliseconds;
                _serialPort.Open();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
            }
        }
        public static SerialPortService Instance(Modbussetting modbus, List<SlaveConfig> slaveConfig, ushort warningCabinetId)
        {
            return new SerialPortService( modbus, slaveConfig,  warningCabinetId);
        }
        /// <summary>
        /// 读取光栅计数器
        /// </summary>
        /// <param name="slaveAddress">从机地址</param>
        /// <returns></returns>
        public ushort[] ReadGratingRegisters(byte slaveAddress)
        {
            return ReadRegisters(slaveAddress, _ModbusSetting.GratingStartAddress, _ModbusSetting.NumberOfPoints);      
        }

        /// <summary>
        /// 读取按钮计数器
        /// </summary>
        /// <param name="slaveAddress">从机地址</param>
        /// <returns></returns>
        public ushort[] ReadButtonRegisters(byte slaveAddress)
        {
            return ReadRegisters(slaveAddress, _ModbusSetting.ButtonStartAddress, _ModbusSetting.NumberOfPoints);
        }
        /// <summary>
        /// 重置光栅计数器
        /// </summary>
        /// <param name="registerValue"></param>
        public void ClearGratingRegister(byte slaveAddress,ushort gratingIndex, bool isCheck = true)
        {
            var dataAddress = _ModbusSetting.ResetGratingStartAddress + gratingIndex;
            var addressRead = _ModbusSetting.GratingStartAddress + gratingIndex;
            ushort[] data = { 0 };
            WriteRegisters(slaveAddress, (ushort)dataAddress, data);
            if (isCheck)
            {
                //第三种方式：清除后再检查是否清除成功，不成功则循环再清
                var ReadFirst = ReadRegisters(slaveAddress, (ushort)addressRead, 1)[0];
                if (ReadFirst == 0 || ReadFirst == 200)
                {

                }
                else
                {
                    ClearGratingRegister(slaveAddress, gratingIndex);
                }

            }
        }
        public void ClearGratingRegister(byte slaveAddress, bool isCheck = true)
        {
            var dataAddress = _ModbusSetting.ResetGratingStartAddress ;
            var addressRead = _ModbusSetting.GratingStartAddress ;
            ushort[] data = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            WriteRegisters(slaveAddress, (ushort)dataAddress, data);
            if (isCheck)
            {
                Thread.Sleep(5);
                //第三种方式：清除后再检查是否清除成功，不成功则循环再清
                ushort[] ReadFirst = ReadRegisters(slaveAddress, (ushort)addressRead, _ModbusSetting.NumberOfPoints);
                if (ReadFirst.Max()>0)
                {
                    ClearGratingRegister(slaveAddress);
                }
            }
        }
        public void ClearGratingRegister( bool isCheck = true)
        {
            var dataAddress = _ModbusSetting.ResetGratingStartAddress;
            var addressRead = _ModbusSetting.GratingStartAddress;
            _SlaveConfig.ForEach(o =>
            {
                ushort[] data = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                WriteRegisters(o.SlaveAddress, (ushort)dataAddress, data);
                if (isCheck)
                {
                    Thread.Sleep(5);
                    //第三种方式：清除后再检查是否清除成功，不成功则循环再清
                    ushort[] ReadFirst = ReadRegisters(o.SlaveAddress, (ushort)addressRead, _ModbusSetting.NumberOfPoints);
                    if (ReadFirst.Max()>0)
                    {
                        ClearGratingRegister(o.SlaveAddress);
                    }
                }
            });
        }

        /// <summary>
        /// 重置按扭计数器
        /// </summary>
        /// <param name="slaveAddress"></param>
        /// <param name="buttonIndex"></param>
        /// <param name="isCheck"></param>
        public void ClearButtonRegister(byte slaveAddress, ushort buttonIndex, bool isCheck = true)
        {
            var dataAddress = _ModbusSetting.ResetButtonStartAddress + buttonIndex;
            var addressRead = _ModbusSetting.ButtonStartAddress + buttonIndex;
            ushort[] data = { 0 };
            WriteRegisters(slaveAddress, (ushort)dataAddress, data);
            if (isCheck)
            {
                Thread.Sleep(5);
                //第三种方式：清除后再检查是否清除成功，不成功则循环再清
                var ReadFirst = ReadRegisters(slaveAddress, (ushort)addressRead, 1)[0];
                if (ReadFirst>0)
                {
                    ClearGratingRegister(slaveAddress, buttonIndex);
                }
            }
        }
        public void ClearButtonRegister(byte slaveAddress, bool isCheck = true)
        {
            var dataAddress = _ModbusSetting.ResetButtonStartAddress ;
            var addressRead = _ModbusSetting.ButtonStartAddress ;
            ushort[] data = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            WriteRegisters(slaveAddress, (ushort)dataAddress, data);
            if (isCheck)
            {
                Thread.Sleep(5);
                //第三种方式：清除后再检查是否清除成功，不成功则循环再清
                ushort[] ReadFirst = ReadRegisters(slaveAddress, (ushort)addressRead, _ModbusSetting.NumberOfPoints);
                if (ReadFirst.Max()>0)
                {
                    ClearButtonRegister(slaveAddress);
                }
            }
        }
        public void ClearButtonRegister( bool isCheck = true)
        {
            var dataAddress = _ModbusSetting.ResetButtonStartAddress;
            var addressRead = _ModbusSetting.ButtonStartAddress;
            ushort[] data = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            _SlaveConfig.ForEach(o =>
            {
                WriteRegisters(o.SlaveAddress, (ushort)dataAddress, data);
                if (isCheck)
                {
                    Thread.Sleep(5);
                    //第三种方式：清除后再检查是否清除成功，不成功则循环再清
                    ushort[] ReadFirst = ReadRegisters(o.SlaveAddress, (ushort)addressRead, _ModbusSetting.NumberOfPoints);
                    if (ReadFirst.Max()>0)
                    {
                        ClearButtonRegister(o.SlaveAddress);
                    }
                }
            });
        }
        /// <summary>
        /// 设置LED
        /// </summary>
        /// <param name="registerChangeList"></param>
        public void SetLED(byte salveAddress,IList<KeyValuePair<int, ushort>> registerChangeList)
        {
            var address = _ModbusSetting.LEDStartAddress;
            ushort[] data = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };//5不处理
            foreach (var kv in registerChangeList)
            {
                int index = kv.Key;
                data[index] = kv.Value;

            }
            WriteRegisters(salveAddress,(ushort)address, data);
        }
        
        public void SetLED(byte salveAddress, int LEDIndex, ushort value)
        {
            var address = _ModbusSetting.LEDStartAddress+ LEDIndex;
            ushort[] data = { value};
            WriteRegisters(salveAddress, (ushort)address, data);
        }
        public void SetLED(byte salveAddress, ushort value)
        {
            var address = _ModbusSetting.LEDStartAddress;
            ushort[] data = { value, value, value, value, value, value, value, value, value, value, value, value };
            WriteRegisters(salveAddress, (ushort)address, data);
        }
        public void SetLED( ushort value)
        {
            var address = _ModbusSetting.LEDStartAddress;
            _SlaveConfig.ForEach(o =>
            {
                ushort[] data = { value, value, value, value, value, value, value, value, value, value, value, value };
                WriteRegisters(o.SlaveAddress, (ushort)address, data);
            });
        }
        //写
        private void WriteRegisters(byte slaveAddress, ushort dataAddress, ushort[] data)
        {
            for (int i = 1; i <= LoopCount; i++)
            {
                try
                {
                    _modbusSerialMaster.WriteMultipleRegisters(slaveAddress, dataAddress, data);
                    return;
                }
                catch { }
            }
            _modbusSerialMaster.WriteMultipleRegisters(slaveAddress, dataAddress, data);
        }
        //读
        private ushort[] ReadRegisters(byte slaveAddress, ushort dataAddress, ushort num)
        {
           
            for (int i = 1; i <= LoopCount; i++)
            {
                try
                {
                    return _modbusSerialMaster.ReadHoldingRegisters(slaveAddress, dataAddress, num);

                }
                catch { }

            }
            return _modbusSerialMaster.ReadHoldingRegisters(slaveAddress, dataAddress, num);
            
           
        }

        public bool isCollect()
        {
            if (_serialPort == null|| !_serialPort.IsOpen)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ClearGrating2Button()
        {
            this.ClearGratingRegister();
            this.ClearButtonRegister();
        }

        public void ClearGrating2Button(byte slaveAddress)
        {
            this.ClearGratingRegister(slaveAddress);
            this.ClearButtonRegister(slaveAddress);
        }

        public void SetWarningLightGreen(ushort lightOperStatus)
        {
            var address = _ModbusSetting.WarningGreenLightStartAddress;
            ushort[] data = { lightOperStatus };
            WriteRegisters(_SlaveConfig.Find(o=>o.CabinetId== _WarningCabinetId).SlaveAddress, (ushort)address, data);
          
        }

        public void SetWarningLightRed(ushort lightOperStatus)
        {
            var address = _ModbusSetting.WarningRedLightStartAddress;
            ushort[] data = { lightOperStatus };
            WriteRegisters(_SlaveConfig.Find(o => o.CabinetId == _WarningCabinetId).SlaveAddress, (ushort)address, data);
        }

        public void SetWarningTwinkle(ushort lightOperStatus)
        {
            var address = _ModbusSetting.WarningRedLightStartAddress;
            ushort[] data = { lightOperStatus, lightOperStatus, lightOperStatus, lightOperStatus };
            WriteRegisters(_SlaveConfig.Find(o => o.CabinetId == _WarningCabinetId).SlaveAddress, (ushort)address, data);

        }

        public void SetWarningLightYellow(ushort lightOperStatus)
        {
            var address = _ModbusSetting.WarningYellowLightStartAddress;
            ushort[] data = { lightOperStatus };
            WriteRegisters(_SlaveConfig.Find(o => o.CabinetId == _WarningCabinetId).SlaveAddress, (ushort)address, data);
        }
    }
}
