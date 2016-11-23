using SCB.OrderSorting.BLL;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //    OrderSortService.SetLED(14, 1, 1);
                    
                
               // OrderSortService.ReSetGratingOrButton(1);
                Thread.Sleep(1000);
                //LEDTest();
                //GratingTest();
                //ButtonTest();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }

        //private static void LEDTest()
        //{
        //    Console.WriteLine("从机（1,2,3），格口索引（0-11），LED（0绿、1红、2闪、3熄）");
        //    string str = Console.ReadLine();
        //    var strArray = str.Split(',');
        //    OrderSortService.SetLED(Convert.ToUInt16(strArray[0]), Convert.ToUInt16(strArray[1]), Convert.ToUInt16(strArray[2]));
        //    LEDTest();
        //}

        private static void GratingTest()
        {
            SystemSetting systemSetting = OrderSortService.GetSystemSettingCache();
            var slaveConfig = systemSetting.SlaveConfigs;//.Where(s => s.SlaveAddress > 0 && s.CabinetId <= systemSetting.CabinetNumber).ToList();
            int i = 1;
            ReadGratingTest(i, slaveConfig);
        }

        private static void ReadGratingTest(int i, List<SlaveConfig> slaveConfig)
        {
            ////Thread.Sleep(100);
            //Console.WriteLine(i);
            //foreach (var slave in slaveConfig)
            //{
            //    //host sleep
            //    Thread.Sleep(10);
            //    try
            //    {
            //        //read
            //        var registersGrating = OrderSortService.ReadGratingRegisters(slave.SlaveAddress);
            //      //  Thread.Sleep(60);
            //        //write
            //        OrderSortService.ReSetGratingOrButton(1, slaveConfig);
            //        ////read
            //        OrderSortService.ReadGratingRegisters(slave.SlaveAddress);
            //        //var registersGrating = OrderSortService.ReadGratingRegisters(15);
            //        Console.Write(slave.CabinetId + "------：");
            //        int j = 0;
            //        foreach (var rg in registersGrating)
            //        {
            //            Console.Write(rg + "; ");
            //            j++;
            //        }
            //        Console.WriteLine();
            //    }
            //    catch (Exception ex)
            //    {
            //        SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
            //    }
            //}
            ////Thread.Sleep(100);//10:4816+   
            //i++;
            //ReadGratingTest(i, slaveConfig);

        }


        private static void ButtonTest()
        {   
        //    SystemSetting systemSetting = OrderSortService.GetSystemSettingCache();
        //    foreach (var slave in systemSetting.SlaveConfigs.Where(s => s.SlaveAddress > 0).Where(s => s.CabinetId <= systemSetting.CabinetNumber))
        //    {
        //        var registersButton = OrderSortService.ReadButtonRegisters(slave.SlaveAddress);
        //        Console.Write(slave.CabinetId + "：");
        //        foreach (var rg in registersButton)
        //        {
        //            Console.Write(rg + ";");
        //        }
        //        Console.WriteLine();
        //    }
        //    Console.WriteLine();
        //    //Thread.Sleep(100);
        //    ButtonTest();
        }
    }
}
