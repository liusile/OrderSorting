using Newtonsoft.Json;
using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.BLL.Model;
using SCB.OrderSorting.DAL;
using SCB.WHP.EDS.SocketServer.Common.CommonHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace SCB.OrderSorting.BLL.API
{
    public static class API_Helper
    {
        private static string _processCenterID { get; set; }
        private static string _deliverAddress { get; set; }

        private static HttpHelper _httpHelper = new HttpHelper();

        private static bool _isTest = false;

        private static string _FlytApi_Host = _isTest ? "http://14.23.92.186:9918/" : "http://oldsystemservice.sellercube.com/";

        private static string _ForeignApi_Host = "http://foreignapi.eds.sellercube.com/";  //给其它部门用的API


        internal static void SetProcessCenterID(string processCenterId, string deliverAddress)
        {
            _processCenterID = processCenterId;
            _deliverAddress = deliverAddress;
        }

        /// <summary>
        /// 调用外部接口，根据订单号获取订单数据（国家、渠道）
        /// </summary>
        /// <param name="processCenterID"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        internal static SortOrderReponseContract GetPostForSortOrder(string orderId)
        {
            try
            {
                //_processCenterID = "918";
                //http://foreignapi.eds.sellercube.com/OrderParent/GetPostForSortOrder?processCenterID=722&orderId=A00051160505001A
                //http://en.fds.sellercube.com/FDS_OrderAPI/VerifyOrder
                string host = "http://en.fds.sellercube.com/";
                string Action = "FDS_OrderAPI";// "OrderParent";
                string Function = "/VerifyOrder";// "/GetPostForSortOrder";
                //string Parameters = string.Format("processCenterID={0}&orderId={1}", _processCenterID, orderId);
                string Parameters = string.Format("ProcessCenterID={0}&OrderId={1}", _processCenterID, orderId);
                string result = _httpHelper.QueryData(host + Action + Function, Parameters, "Get", HttpHelper.SelectType.Select);
                System.Diagnostics.Debug.WriteLine(result);
                var obj = JsonConvert.DeserializeObject<SortOrderReponseContract>(result);
                if (!obj.Success)
                {
                    throw new Exception(obj.Message);
                }
                //if (obj.Content == null || obj.Content.Count < 1)
                //{
                //    throw new Exception("根据订单号" + orderId + "获取数据为空！");
                //}
                var info = obj;
                return info;
            }
            catch { throw; }
        }

        /// <summary>
        /// 获取OA的全部邮寄方式
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetPostList()
        {
            try
            {
                //http://foreignapi.eds.sellercube.com/Post/GetPostList
                string Action = "Post";
                string Function = "/GetPostList";
                string result = _httpHelper.QueryData(_ForeignApi_Host + Action + Function, "", "Get", HttpHelper.SelectType.Select);
                var obj = JsonConvert.DeserializeObject<ResponseDataModel<DataTable>>(result);
                if (obj == null)
                {
                    return null;
                }
                if (!obj.IsSuccess)
                {
                    throw new Exception(obj.ErrorMsg);
                }
                return obj.Content;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 用户登录（EDS）
        /// </summary>
        /// <param name="email">帐号</param>
        /// <param name="pwd">密码</param>
        /// <param name="userInfo">用户对象</param>
        /// <param name="errorMsg">错误消息</param>
        /// <returns></returns>
        internal static UserInfo CheckLogin(string email, string pwd, ref string errorMsg)
        {
            try
            {
                string Action = "User";
                string Function = "/CheckLoginUser";
                string Parameters = string.Format("userAccount={0}&userPassword={1}", email, pwd);
                string result = _httpHelper.QueryData(_ForeignApi_Host + Action + Function, Parameters, "Post");
                ResponseModel<EDS_LoginDTO> dataResult = JsonConvert.DeserializeObject<ResponseModel<EDS_LoginDTO>>(result);
                if (dataResult == null)
                {
                    errorMsg = "获取用户信息异常, 请检查服务是否已关闭!";
                    return null;
                }
                else if (dataResult.IsSuccess && dataResult.Data != null && dataResult.Data.EDS_User != null)
                {
                    return new UserInfo
                    {
                        UserId = dataResult.Data.EDS_User.UserId,
                        UserName = dataResult.Data.EDS_User.UserName,
                        ReceivePointId = dataResult.Data.EDS_User.DeliverAddress,
                        Pcid = dataResult.Data.EDS_User.ProcessCenterID
                    };
                }
                else if (dataResult.EDS_ResponseError != null)
                {
                    errorMsg = dataResult.EDS_ResponseError.Message;
                    return null;
                }
                else
                {
                    errorMsg = "获取用户信息异常！";
                    return null;
                }

            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// 获取包牌号
        /// </summary>
        /// <returns>包牌号</returns>
        internal static string GetFlytPackageLabelID()
        {
            try
            {
                //http://foreignapi.eds.sellercube.com/APi/GetFlytPackageLabelID
                string packageLabelID = string.Empty;
                string Action = "APi";
                string Function = "/GetFlytPackageLabelID";
                string result = _httpHelper.QueryData(_ForeignApi_Host + Action + Function, "", "Get");
                var obj = JsonConvert.DeserializeObject<ResponseDataModel>(result);
                if (obj != null && obj.IsSuccess)
                {
                    packageLabelID = obj.Content;
                }
                if (string.IsNullOrWhiteSpace(packageLabelID))
                {
                    throw new Exception("服务器错误，接口APi/GetFlytPackageLabelID无法访问，请联系技术人员！");
                }
                return packageLabelID;
            }
            catch (Exception) { throw; }
        }
        /// <summary>
        /// 调用物流接口，根据订单号获取订单信息
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        internal static VerifyOrderResponseContract VerifyOrder(string orderId, UserInfo user)
        {
            try
            {
                string url = _FlytApi_Host + "Picker/VerifyOrder";
                var postData = new VerifyOrderRequestContract
                {
                    Token = "5A9C85B6E068F2236A039E6157C5DF5B",
                    OperatorId = user.UserId,
                    OperatorName = user.UserName,
                    OrderId = orderId
                };
                return _httpHelper.Post<VerifyOrderResponseContract>(url, postData);
            }
            catch (Exception ) { throw ; }
        }
        internal static VerifyOrderResponseContract VerifyOrderForHangZhou(string orderId, UserInfo user)
        {
            try
            {
                string host = "http://foreignapi.eds.sellercube.com";
                string Action = "/orderparent";
                string Function = "/GetOrderVerification";
                string Parameters = string.Format("Token={0}&OperatorId={1}&OperatorName={2}&OrderId={3}&ProcessCenterID={4}", "f959d57cf4e94b6f93c129e2a504860d", user.UserId, user.UserName, orderId, user.Pcid);
                string result = _httpHelper.QueryData(host + Action + Function, Parameters, "Get");
                Debug.Write(result);
                return JsonConvert.DeserializeObject<VerifyOrderResponseContract>(result);
            }
            catch (Exception ex) {
                string error = ex.ToString();
                Debug.Write(error);
                throw; }
        }
        /// <summary>
        /// 把装箱信息上传到物流系统
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="pkgLog">装箱记录</param>
        /// <param name="logList">明细</param>
        /// <returns></returns>
        public static BaseResponseContract BatchOutbound(UserInfo userInfo, PackingLog pkgLog, List<LatticeOrdersCache> logList)
        {
            try
            {
                string url = _FlytApi_Host + "Picker/BatchOutbound";
                var outboudRequest = new BatchOutboudRequestContract
                {
                    Token = "5A9C85B6E068F2236A039E6157C5DF5B",
                    OperatorId = userInfo.UserId,
                    OperatorName = userInfo.UserName,
                    Pkg = pkgLog.PackNumber,
                    OutboundPostId = pkgLog.PostTypeIds.Split(',')[0],
                    ReceivePoint = userInfo.ReceivePointId,
                    ProcessCenterId = userInfo.Pcid,
                    OutboudDetails = new List<OrderOutboudDetailContract>()
                };
                logList.ForEach(lg =>
                {
                    outboudRequest.OutboudDetails.Add(new OrderOutboudDetailContract
                    {
                        OrderId = lg.OrderId,
                        TraceId = lg.TraceId,
                        Weight = lg.Weight,
                        CountryId = lg.CountryId,
                        Reason = 0
                    });
                });
               // SaveErrLogHelper.SaveErrorLog("",JsonConvert.SerializeObject(outboudRequest) + "url:" + url);
                return _httpHelper.Post<BaseResponseContract>(url, outboudRequest);
            }
            catch (Exception ex) { throw new Exception("装箱信息上传到物流系统时出错:"+ex.ToString());
               
            }
        }

        /// <summary>
        /// 飞特物流登录接口
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static UserInfo Login(string email, string pwd, ref string errorMsg)
        {
            try
            {
                //http://oldsystemservice.sellercube.com/Account/Login
                string url = _FlytApi_Host + "Account/Login";
                var request = new LoginRequestContract
                {
                    Token = "5A9C85B6E068F2236A039E6157C5DF5B",
                    LoginName = email,
                    Password = pwd
                };
                var response = _httpHelper.Post<LoginResponseContract>(url, request);
                if (response == null)
                {
                    errorMsg = "获取用户信息异常, 请检查服务是否已关闭!";
                    return null;
                }
                if (!response.Success)
                {
                    errorMsg = response.Message;
                    return null;
                }
                return new UserInfo
                {
                    UserId = Convert.ToInt32(response.userID),
                    UserName = response.userName,
                    ReceivePointId = response.ReceivePointId.ToString(),
                    RepName = response.RepName,
                    Pcid = response.Pcid,
                    PcName = response.PcName
                };
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return null;
            }
        }
    }
}
