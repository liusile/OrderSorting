using SCB.OrderSorting.BLL.API;
using SCB.OrderSorting.BLL.Model;
using SCB.OrderSorting.BLL.Service;
using System.Collections.Generic;
using System.Web.Security;

namespace SCB.OrderSorting.BLL
{
    public static class LoginService
    {
        public static List<LoginData> GetLoginName()
        {
            return BaseDataService.GetLoginName();
        }

        public static void SaveLoginName(List<LoginData> users)
        {
            BaseDataService.SaveLoginName(users);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userAccount">帐号</param>
        /// <param name="userPassword">密码</param>
        /// <param name="lst_temp">获取登录对象</param>
        /// <param name="ErrorMsg">获取错误信息</param>
        /// <returns></returns>
        public static UserInfo CheckLogin(string userAccount, string userPassword, ref string ErrorMsg)
        {
            var systemSetting = BaseDataService.GetSystemSetting();
            if (systemSetting.InterfaceType==InterfaceType.Flyt)
            {
                return API_Helper.Login(userAccount, userPassword, ref ErrorMsg);
            }
            else if(systemSetting.InterfaceType == InterfaceType.General)
            {
                userPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(userPassword, "MD5").ToLower();
                return API_Helper.CheckLogin(userAccount, userPassword, ref ErrorMsg);
            }
            else if (systemSetting.InterfaceType == InterfaceType.SigleFlyt)
            {
                userPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(userPassword, "MD5").ToUpper();
                return API_Helper.LoginBySingleFlyt(userAccount, userPassword, ref ErrorMsg);
            }
            else
            {
                throw new System.Exception("配置出错，未知的对接方！");
            }
        }

        public static void SetProcessCenterID(string processCenterId, string deliverAddress = "9")
        {
            API_Helper.SetProcessCenterID(processCenterId, deliverAddress);
        }
    }
}
