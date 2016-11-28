using Modbus.Device;
using SCB.OrderSorting.BLL.Model;
using SCB.OrderSorting.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCB.OrderSorting.BLL.Service
{
    interface ISlavePortService
    {

        #region LED 0熄灭，1绿 2红 3黄色，4闪烁
        /// <summary>
        /// 设置LED灯
        /// </summary>
        /// <param name="lattice"></param>
        /// <param name="registerValues"></param>
        /// <returns></returns>
        void SetLED(LatticeSetting lattice, ushort value);
        void SetLED(int LEDIndex, ushort value);
        void SetLED(IList<KeyValuePair<int, ushort>> registerChangeList);
        void SetLEDAll(ushort value);
        void ClearLED(ushort LEDIndex);
        void ClearLEDAll();
        #endregion

        #region 光栅
        ushort ReadGratingRegister(ushort gratingIndex);
        ushort[] ReadGratingRegisterAll();
        void SetGratingRegister(ushort gratingIndex, ushort value);
        void SetGratingRegisterAll(ushort value);
        void ClearGratingRegister(ushort gratingIndex,bool isCheck);
        void ClearGratingRegisterAll();
        #endregion

        #region Button 
        ushort[] ReadButtonRegisterAll();
        ushort ReadButtonRegister(ushort buttonIndex);
        void SetButton(LatticeSetting lattice, ushort registerValues);
        void SetButton( int index, ushort registerValues);
        void SetButton( IList<KeyValuePair<int, ushort>> registerChangeList);
        void SetButtonAll(ushort registerValues);
        void ClearButton(int buttonIndex);
        void ClearButtonAll();
        #endregion

        #region 警示灯 0熄灭，1绿 2红 3黄色，4闪烁
        void SetGreenWarningLight(ushort value);
        void SetRedWarningLight(ushort value);
        void SetYellowWarningLight(ushort value);
        void SetTwinkleWarningLight(ushort value);
        void SetAllWarningLight(ushort value);
        void SetAllWarning(ushort value);
        /// <summary>
        /// 红 绿 黄 蜂鸣器
        /// </summary>
        /// <param name="registerValues"></param>
        /// <returns></returns>
        void SetAllWarningLight(ushort red, ushort green, ushort yellow);
        void SetAllWarning(ushort red, ushort green, ushort yellow, ushort Twinkle);
        #endregion
    }
}
