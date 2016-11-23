using Modbus.Device;
using Modbus.Serial;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.BLL.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Net.Sockets;
using System.Threading;

namespace SCB.OrderSorting.BLL.Service
{
    internal class SerialPortService
    {
        /// <summary>
        /// 表示串行端口资源
        /// </summary>
        private static SerialPort _serialPort { get; set; }
       // private static IModbusSerialMaster _modbusSerialMaster { get; set; }

        private static ModbusIpMaster _modbusSerialMaster { get; set; }
        
        /// <summary>
        /// LED灯的初始状态值
        /// </summary>
        private static ushort[][] _defaultRegistersLEDArray
        {
            get
            {
                return new ushort[4][]
                {
                    new ushort[12] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
                    new ushort[12] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
                    new ushort[12] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
                    new ushort[12] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }
                };
            }
        }
        /// <summary>
        /// 当前LED灯状态值
        /// </summary>
        private static ushort[][] _runningRegistersLEDArray { get; set; }
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
        /// 超时时间
        /// </summary>
        private static int TimeoutMilliseconds = 100;//60;
        /// <summary>
        /// 窗口服务构造函数
        /// </summary>
        /// <param name="modbus">modbus设置</param>[]
        /// <param name="slaveConfig">从机信息</param>
        /// <param name="warningCabinetId">警示灯从机id（1-4）</param>
        internal SerialPortService(Modbussetting modbus, List<SlaveConfig> slaveConfig, ushort warningCabinetId)
        {
            try
            {
                _ModbusSetting = modbus;
                _SlaveConfig = slaveConfig;
                _WarningCabinetId = warningCabinetId;
                _runningRegistersLEDArray = _defaultRegistersLEDArray.Clone() as ushort[][];
                //if (_serialPort != null)
                //    _serialPort.Dispose();
                //_serialPort = new SerialPort(_ModbusSetting.PortName, _ModbusSetting.BaudRate, (Parity)_ModbusSetting.Parity, _ModbusSetting.DataBits, (StopBits)_ModbusSetting.StopBits);
                //_modbusSerialMaster = ModbusSerialMaster.CreateRtu(new SerialPortAdapter(_serialPort));
                
                ////设置Modbus通讯的超时时间
                //_modbusSerialMaster.Transport.ReadTimeout = TimeoutMilliseconds;
                //_modbusSerialMaster.Transport.WriteTimeout = TimeoutMilliseconds;
                //_serialPort.Open();

                //TCP
                TcpClient client = new TcpClient("192.168.1.7", 26);
                _modbusSerialMaster = ModbusIpMaster.CreateIp(client);
                _modbusSerialMaster.Transport.ReadTimeout = TimeoutMilliseconds;
                _modbusSerialMaster.Transport.WriteTimeout = TimeoutMilliseconds;

            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
            }
        }

        /// <summary>
        /// 读取光栅计数器
        /// </summary>
        /// <param name="slaveAddress">从机地址</param>
        /// <returns></returns>
        internal ushort[] ReadGratingRegisters(byte slaveAddress,int tryReadCount)
        {
            try
            {
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    Wait_A_Minute();
                   // Stopwatch shopWatch = Stopwatch.StartNew();
                    var result = _modbusSerialMaster.ReadHoldingRegisters(slaveAddress, _ModbusSetting.GratingStartAddress, _ModbusSetting.NumberOfPoints);//60-75ms
                    
                   // shopWatch.Stop();
                    //if (shopWatch.ElapsedMilliseconds > 500) {
                    //    SaveErrLogHelper.SaveErrorLog(string.Empty, "第101行，ReadHoldingRegisters时间："+shopWatch.ElapsedMilliseconds+"ms");
                    //}
                    return result;
                }
                return new ushort[0] { };
            }
            catch (Exception ex)
            {
                var str = string.Format("尝试读取光栅：第{2}次,SlaveAddress={0},GratingStartAddress={1}", slaveAddress, _ModbusSetting.GratingStartAddress, tryReadCount);
                SaveErrLogHelper.SaveErrorLog(str, ex.ToString());
                return new ushort[0] { };
            }
        }

        /// <summary>
        /// 读取按钮计数器
        /// </summary>
        /// <param name="slaveAddress">从机地址</param>
        /// <returns></returns>
        internal ushort[] ReadButtonRegisters(byte slaveAddress,int tryReadCount)
        {
            try
            {
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    Wait_A_Minute();
                   // Stopwatch shopWatch = Stopwatch.StartNew();
                    ushort [] result = _modbusSerialMaster.ReadHoldingRegisters(slaveAddress, _ModbusSetting.ButtonStartAddress, _ModbusSetting.NumberOfPoints);//60-75ms
                    //shopWatch.Stop();
                    //if (shopWatch.ElapsedMilliseconds > 500)
                    //{
                    //    SaveErrLogHelper.SaveErrorLog(string.Empty, "第128行，ReadHoldingRegisters时间：" + shopWatch.ElapsedMilliseconds + "ms");
                    //}
                    return result;
                }
                return new ushort[0] { };
            }
            catch (Exception ex)
            {
                var str = string.Format("尝试读取光栅：第{2}次,SlaveAddress={0},GratingStartAddress={1}", slaveAddress, _ModbusSetting.ButtonStartAddress, tryReadCount);
                SaveErrLogHelper.SaveErrorLog(str, ex.ToString());
                return new ushort[0] { };
            }
        }
        /// <summary>
        /// 重置计数器（registerValue：1重置光栅计数器；2重置按钮计数器；3重置两者）
        /// </summary>
        /// <param name="registerValue">1：重置光栅计数器；2：重置按钮计数器；3：重置两者</param>
        internal void ReSetGratingOrButton(ushort registerValue)
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _SlaveConfig.ForEach(slave =>
                {
                    Wait_A_Minute();
                    ThreadSleep60();
                    try
                    {
                      //  Stopwatch shopWatch = Stopwatch.StartNew();
                        _modbusSerialMaster.WriteMultipleRegisters(slave.SlaveAddress, _ModbusSetting.ResetGratingStartAddress, new ushort[1] { registerValue });//40-55ms
                      //  shopWatch.Stop();
                        //if (shopWatch.ElapsedMilliseconds > 500)
                        //{
                        //    SaveErrLogHelper.SaveErrorLog(string.Empty, "第101行，ReadHoldingRegisters时间：" + shopWatch.ElapsedMilliseconds + "ms");
                        //}
                        
                    }
                    catch (Exception ex)
                    {
                        var str = string.Format("SlaveAddress={0},GratingStartAddress={1}", slave.SlaveAddress, _ModbusSetting.ResetGratingStartAddress);
                        SaveErrLogHelper.SaveErrorLog(str, ex.ToString());
                        throw;
                    }
                });
               // ThreadSleep60();
            }
        }
        internal void ReSetGratingOrButton(ushort registerValue,SlaveConfig slave)
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                try
                {
                    _modbusSerialMaster.WriteMultipleRegisters(slave.SlaveAddress, _ModbusSetting.ResetGratingStartAddress, new ushort[1] { registerValue });//40-55ms
                }
                catch (Exception ex)
                {
                    var str = string.Format("SlaveAddress={0},GratingStartAddress={1}", slave.SlaveAddress, _ModbusSetting.ResetGratingStartAddress);
                    SaveErrLogHelper.SaveErrorLog(str, ex.ToString());
                    throw;
                }
            }
        }
        /// <summary>
        /// 重置LED灯
        /// </summary>
        /// <returns></returns>
        internal bool ReSetLED()
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _runningRegistersLEDArray = _defaultRegistersLEDArray.Clone() as ushort[][];
                _SlaveConfig.ForEach(slave =>
                {
                    Wait_A_Minute();
                    try
                    {
                       // Stopwatch shopWatch = Stopwatch.StartNew();
                        _modbusSerialMaster.WriteMultipleRegisters(slave.SlaveAddress, _ModbusSetting.LEDStartAddress, _runningRegistersLEDArray[slave.CabinetId - 1]);
                        //shopWatch.Stop();
                        //if (shopWatch.ElapsedMilliseconds > 500)
                        //{
                        //    SaveErrLogHelper.SaveErrorLog(string.Empty, "第101行，ReadHoldingRegisters时间：" + shopWatch.ElapsedMilliseconds + "ms");
                        //}
                    }
                    catch (Exception ex)
                    {
                        var str = string.Format("SlaveAddress={0},LEDStartAddress={1}", slave.SlaveAddress, _ModbusSetting.LEDStartAddress);
                        SaveErrLogHelper.SaveErrorLog(str, ex.ToString());
                        throw;
                    }
                });
                return true;
            }
            return false;
        }
        /// <summary>
        /// 设置LED灯颜色
        /// </summary>
        /// <param name="cabinetId">从机号</param>
        /// <param name="index">索引</param>
        /// <param name="registerValues">0绿灯，1红灯，2红灯闪烁，3熄灭</param>
        /// <returns></returns>
        internal bool SetLED(int cabinetId, int index, ushort registerValues)
        {
            using (TcpClient client = new TcpClient("192.168.1.7", 26))
            {

                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(client);

                // read five input values
                ushort startAddress = 40100;
                ushort[] numInputs = { 1,1,1,1};
               
                    master.WriteMultipleRegisters(14,startAddress, numInputs);
                    Thread.Sleep(1000);
               


            }
            return true;
            var slave = _SlaveConfig.Find(s => s.CabinetId == cabinetId);
            try
            {
                //if (_serialPort != null && _serialPort.IsOpen)
                //{
                    _runningRegistersLEDArray[cabinetId - 1][index] = registerValues;
                    Wait_A_Minute();
             
                   // Stopwatch shopWatch = Stopwatch.StartNew();
                    _modbusSerialMaster.WriteMultipleRegisters(slave.SlaveAddress, _ModbusSetting.LEDStartAddress, _runningRegistersLEDArray[cabinetId - 1]);
                    //shopWatch.Stop();
                    //if (shopWatch.ElapsedMilliseconds > 500)
                    //{
                    //    SaveErrLogHelper.SaveErrorLog(string.Empty, "第101行，ReadHoldingRegisters时间：" + shopWatch.ElapsedMilliseconds + "ms");
                    //}
                    return true;
                //}
                return false;
            }
            catch (Exception ex)
            {
                var str = string.Format("SlaveAddress={0},LEDStartAddress={1}", slave.SlaveAddress, _ModbusSetting.LEDStartAddress);
                SaveErrLogHelper.SaveErrorLog(str, ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 设置警示灯
        /// </summary>
        /// <param name="slaveAddress">架子地址</param>
        /// <param name="type">1红灯，2绿灯，3黄灯，4蜂鸣器(+全部灯)</param>
        /// <param name="registerValue">1启动，0关闭</param>
        internal void SetWarningLight(byte slaveAddress, byte type, ushort registerValue)
        {
            try
            {
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    switch (type)
                    {
                        case 1:
                            _modbusSerialMaster.WriteMultipleRegisters(slaveAddress, _ModbusSetting.WarningRedLightStartAddress, new ushort[1] { registerValue });
                            break;
                        case 2:
                            _modbusSerialMaster.WriteMultipleRegisters(slaveAddress, _ModbusSetting.WarningGreenLightStartAddress, new ushort[1] { registerValue });
                            break;
                        case 3:
                            _modbusSerialMaster.WriteMultipleRegisters(slaveAddress, _ModbusSetting.WarningYellowLightStartAddress, new ushort[1] { registerValue });
                            break;
                        case 4:
                            //40112   警示灯红
                            //40113   警示灯绿
                            //40114   警示灯黄
                            //40115   警示灯蜂鸣器
                            _modbusSerialMaster.WriteMultipleRegisters(slaveAddress, _ModbusSetting.WarningRedLightStartAddress, new ushort[4] { registerValue, 1, registerValue, registerValue });
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog("SlaveAddress="+ slaveAddress, ex.ToString());
            }
        }
        /// <summary>
        /// 设置警示灯
        /// </summary>
        /// <param name="type">1红灯，2绿灯，3黄灯，4蜂鸣器(+全部灯)</param>
        /// <param name="registerValue">1启动，0关闭</param>
        internal void SetWarningLight(byte type, ushort registerValue)
        {
            try
            {
                //获取警示灯从机
                SlaveConfig slave = GetWarningSlave();
                Wait_A_Minute();
                SetWarningLight(slave.SlaveAddress, type, registerValue);
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
            }
        }
        /// <summary>
        /// 重置警示灯
        /// </summary>
        /// <returns></returns>
        internal bool ReSetWarningLight()
        {
            try
            {
                //_SlaveConfig.ForEach(slave =>
                //{
                //    Wait_A_Minute();
                //    SetWarningLight(slave.SlaveAddress, 4, 0);
                //});
                //获取警示灯从机
                SlaveConfig slave = GetWarningSlave();
                Wait_A_Minute();
                SetWarningLight(slave.SlaveAddress, 4, 0);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取警示灯从机
        /// </summary>
        /// <returns></returns>
        internal SlaveConfig GetWarningSlave()
        {
            return _SlaveConfig.Find(s => s.CabinetId == _WarningCabinetId);
        }

        /// <summary>
        /// 等待上一次modbus通讯完全结束（访问时间间隔太短，频繁切换从机，会导致无响应）
        /// </summary>
        private static void Wait_A_Minute()
        {
           // Thread.Sleep(TimeoutMilliseconds);
        }
        /// <summary>
        /// sleep 60毫秒
        /// </summary>
        private static void ThreadSleep60()
        {
            Thread.Sleep(60);
        }
    }
}
