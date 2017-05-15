using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.BLL.Model;
using SCB.OrderSorting.BLL.Model.CacheModel;
using SCB.OrderSorting.BLL.Service;
using SCB.OrderSorting.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCB.OrderSorting.BLL
{
    /// <summary>
    /// 分拣系统服务
    /// </summary>
    public static class OrderSortService
    {
        #region Private
        /// <summary>
        /// 系统设置信息
        /// </summary>
        private static SystemSetting _systemSetting { get; set; }
        /// <summary>
        /// 分拣数据服务
        /// </summary>
        private static SortingService sortingService { get; set; }
        /// <summary>
        /// 串口服务
        /// </summary>
        public static SerialPortService SerialPortService { get; set; }
        /// <summary>
        /// 声音服务
        /// </summary>
        private static SoundService soundService { get; set; }
        #endregion

        #region 初始化
        static OrderSortService()
        {
            //加载系统设置信息
            LoadSystemSetting();
            //定时清理/迁移旧数据
            ClearDataService.ClearData(_systemSetting.LogStorageDays);
        }

        /// <summary>
        /// 初始化（OrderSortService初次调用时需要一点时间，所以在登陆窗口先调用一下，进行初始化，避免登陆后卡顿）
        /// </summary>
        public static void Initialize()
        {
            //更新邮寄方式
            try
            {
                BaseDataService.UpdatePostTypes();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
            }
        }

        #endregion

        #region 系统设置
        /// <summary>
        /// 加载系统设置信息
        /// </summary>
        private static void LoadSystemSetting()
        {
            try
            {
                //获取系统设置信息
                _systemSetting = BaseDataService.GetSystemSetting();
                
                //串口服务实例化
                SerialPortService = SerialPortService.Instance(_systemSetting.ModbusSetting, GetSlaveConfig(), _systemSetting.WarningCabinetId);

                 //分拣数据服务实例化
                 sortingService = new SortingService(_systemSetting.CabinetNumber, _systemSetting.SortingPatten, _systemSetting.SortingSolution, _systemSetting.IsFlyt, _systemSetting.BoxWeight);
                //声音服务
                soundService = new SoundService();
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
            }
        }
        #region 报表
        /// <summary>
        /// 获取日报表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static List<DayReportViewModel> GetDayReportByPageSize(DateTime startDate, DateTime endDate, int pageIndex, int pageSize, ref int recordCount)
        {
            return sortingService.GetDayReportByPageSize(startDate, endDate, pageIndex, pageSize, ref recordCount);
        }
        #endregion
       
        /// <summary>
        /// 获取系统设置信息
        /// </summary>
        /// <returns></returns>
        public static SystemSetting GetSystemSettingCache()
        {
            return _systemSetting;
        }
        /// <summary>
        /// 保存Modbus串口设置
        /// </summary>
        public static void SaveSystemSetting(SystemSetting setting)
        {
            _systemSetting = setting;
            BaseDataService.SaveSystemSetting(_systemSetting);
            SerialPortService.PortClose();
            LoadSystemSetting();
            //串口服务实例化
           // SerialPortService = SerialPortService.Instance(_systemSetting.ModbusSetting, GetSlaveConfig(), _systemSetting.WarningCabinetId);
        }
        #endregion
   
        #region Button格口按钮相关
        /// <summary>
        /// 获取界面格口按钮（窗体OnLoad时调用）
        /// </summary>
        /// <param name="formWidth">窗体宽度</param>
        /// <param name="formHeight">窗体高度</param>
        /// <returns></returns>
        public static List<Button> CreateButtonList(int formWidth, int formHeight)
        {
            return sortingService.CreateButtonList(formWidth, formHeight);
        }
        /// <summary>
        /// 获取界面格口按钮的定位和大小属性（窗体SizeChange时调用）
        /// </summary>
        /// <param name="formWidth">窗体宽度</param>
        /// <param name="formHeight">窗体高度</param>
        /// <returns></returns>
        public static List<ButtonPointModel> CreateButtonPointModelList(int formWidth, int formHeight)
        {
            return sortingService.CreateButtonPointModelList(formWidth, formHeight);
        }

        /// <summary>
        /// 获取（要显示到分拣作业窗体按钮上的）格口信息
        /// </summary>
        /// <param name="ls"></param>
        /// <returns></returns>
        public static string GetLatticeNewText(LatticeSetting ls)
        {
            return sortingService.GetLatticeNewText(ls);
        }

        #endregion

        #region LatticeSetting格口设置相关

        /// <summary>
        /// 获取格口设置信息
        /// </summary>
        /// <returns></returns>
        public static List<LatticeSetting> GetLatticeSettingList()
        {
            return sortingService.GetLatticeSettingList();
        }
        /// <summary>
        /// 加载格口设置信息
        /// </summary>
        /// <returns></returns>
        public static List<LatticeSetting> LoadLatticeSetting()
        {
            return sortingService.LoadLatticeSetting();
        }
        /// <summary>
        /// 根据格口id获取格口设置信息
        /// </summary>
        /// <param name="latticesettingId">格口id</param>
        /// <returns></returns>
        public static LatticeSetting GetLatticeSettingById(int latticesettingId)
        {
            return sortingService.GetLatticeSettingById(latticesettingId);
        }
        /// <summary>
        /// 添加登录记录
        /// </summary>
        /// <param name="userName"></param>
        public static void AddLoginLog(string userName)
        {
            sortingService.AddLoginLog(userName);
        }
        /// <summary>
        /// 根据格口Id获取格口与邮寄方式关联关系
        /// </summary>
        /// <param name="latticeSettingId">格口Id</param>
        /// <returns></returns>
        public static List<SolutionPostType> GetSolutionPostTypeListByLatticeSettingId(int latticeSettingId)
        {
            return sortingService.GetSolutionPostTypeListByLatticeSettingId(latticeSettingId);
        }
        /// <summary>
        /// 根据格口Id获取格口与邮寄方式地区关联关系
        /// </summary>
        /// <param name="latticeSettingId"></param>
        /// <returns></returns>
        public static List<SolutionPostArea> GetSolutionPostAreaListByLatticeSettingId(string PostTypeId,int latticeSettingId)
        {
            return sortingService.GetSolutionPostAreaListByLatticeSettingId(PostTypeId, latticeSettingId);
        }


        /// <summary>
        /// 根据格口Id获取格口与国家地区的关联关系
        /// </summary>
        /// <param name="latticeSettingId">格口Id</param>
        public static List<SolutionCountry> GetSolutionCountryListByLatticeSettingId(int latticeSettingId)
        {
            return sortingService.GetSolutionCountryListByLatticeSettingId(latticeSettingId);
        }
        /// <summary>
        /// 保存格口设置信息
        /// </summary>
        /// <param name="latticesetting"></param>
        public static void SaveLatticeSetting(LatticeSetting latticesetting)
        {
            sortingService.SaveLatticeSetting(latticesetting);
        }
        public static SolutionZipType GetSolutionZipType(int LatticeSettingId)
        {
          return  sortingService.GetSolutionZipType(LatticeSettingId);
        }
        public static void SaveSolutionZipType(SolutionZipType solutionZipType)
        {
            sortingService.SolutionZipType(solutionZipType);
        }
        /// <summary>
        /// 判断格口号是否已存在
        /// </summary>
        /// <param name="latticeId">格口号</param>
        /// <returns></returns>
        public static bool IsLatticeIdExists(string latticeId)
        {
            return sortingService.IsLatticeIdExists(latticeId);
        }
        /// <summary>
        /// 根据订单信息获取匹配的格口
        /// </summary>
        /// <param name="info">订单信息</param>
        /// <returns></returns>
        public static LatticeSetting GetLatticeSettingByOrderinfo(OrderInfo info)
        {
            return sortingService.GetLatticeSettingByOrderinfo(info);
        }
        public static List<LatticeSetting> GetLatticeSettingByOrderinfoList(OrderInfo info)
        {
            return sortingService.GetLatticeSettingByOrderinfoList(info);
        }
        /// <summary>
        /// 获取所有国家地区信息
        /// </summary>
        /// <returns></returns>
        public static List<Countrys> GetCountrysList()
        {
            return BaseDataService.GetCountrysList();
        }
        /// <summary>
        /// 获取所有邮寄方式信息
        /// </summary>
        /// <returns></returns>
        public static List<Posttypes> GetPostTypesList()
        {
            return BaseDataService.GetPostTypesList();
        }
        /// <summary>
        /// 跟据渠道获取所有的区
        /// </summary>
        /// <param name="PostTypeId"></param>
        /// <returns></returns>
        public static List<PostArea> GetPostAreaList(string PostTypeId)
        {
            return BaseDataService.GetPostAreaList(PostTypeId);
        }
        
        #endregion

        #region Solution分拣方案相关
        /// <summary>
        /// 获取所有分拣方案
        /// </summary>
        /// <returns></returns>
        public static List<SortingSolutions> GetSortingSolutionsList()
        {
            return BaseDataService.GetSortingSolutionsList();
        }
        /// <summary>
        /// 根据Id删除分拣方案
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteSortingSolutionsById(string id)
        {
            BaseDataService.DeleteSortingSolutionsById(id);
        }

        /// <summary>
        /// 新建分拣方案
        /// </summary>
        /// <param name="solutionName"></param>
        public static void CreateNewSolution(string solutionName)
        {
            _systemSetting.SortingSolution = sortingService.CreateNewSolution(solutionName);
            SaveSystemSetting(_systemSetting);
        }

        /// <summary>
        /// 方案重命名
        /// </summary>
        /// <param name="text"></param>
        public static void RenameSolution(string solutionName)
        {
            sortingService.RenameSolution(_systemSetting.SortingSolution, solutionName);
        }
        /// <summary>
        /// 更新格口与邮寄方式的关联
        /// </summary>
        /// <param name="latticesetting">格口信息</param>
        /// <param name="posttypesList">邮寄方式</param>
        public static void UpdateSolutionPostType(LatticeSetting latticesetting, List<Posttypes> posttypesList)
        {
            sortingService.UpdateSolutionPostType(latticesetting, posttypesList);
        }
        /// <summary>
        /// 更新格口与邮寄方式地区的关联
        /// </summary>
        /// <param name="latticesetting"></param>
        /// <param name="posttypesList"></param>
        public static void UpdateSolutionPostArea(LatticeSetting latticesetting, List<PostArea> posttypesList,string postTypeId)
        {
            sortingService.UpdateSolutionPostArea(latticesetting, posttypesList, postTypeId);
        }
        /// <summary>
        /// 更新格口与国家地区的关联
        /// </summary>
        /// <param name="latticesetting">格口信息</param>
        /// <param name="countrysList">国家地区</param>
        public static void UpdateSolutionCountry(LatticeSetting latticesetting, List<DAL.Countrys> countrysList)
        {
            sortingService.UpdateSolutionCountry(latticesetting, countrysList);
        }
        #endregion

        #region OrderSorting分拣记录相关

        /// <summary>
        /// 调用外部接口，根据订单号获取订单数据（国家、渠道）
        /// </summary>
        /// <param name="processCenterID"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static OrderInfo GetOrderInfoById(string orderId, UserInfo userInfo, ref string errorMsg)
        {
            return sortingService.GetOrderInfoById(orderId, userInfo, ref errorMsg);
        }
        public static OrderInfo GetOrderInfoById(string orderId, UserInfo userInfo)
        {
            return sortingService.GetOrderInfoById(orderId, userInfo);
        }
        public static Task<OrderInfo> GetOrderInfoByIdAsync(string orderId, UserInfo userInfo)
        {
            return Task.Run(()=> sortingService.GetOrderInfoById(orderId, userInfo));
        }
        /// <summary>
        /// 根据格口Id获取格口内的订单信息
        /// </summary>
        /// <param name="latticesettingId"></param>
        /// <returns></returns>
        public static List<LatticeOrdersCache> GetLatticeOrdersListByLatticesettingId(int latticesettingId)
        {
            return sortingService.GetLatticeOrdersListByLatticesettingId(latticesettingId);
        }

        /// <summary>
        /// 创建分拣记录
        /// </summary>
        /// <param name="info">订单信息</param>
        /// <param name="targetLattice">目标柜格</param>
        /// <param name="resultLattice">投入柜格</param>
        /// <param name="operationType">操作类型：1扫描，2投递，3异常恢复</param>
        /// <param name="status">状态：1待投递，2已投递，3投递异常,4重复扫描</param>
        public static bool CreateOrderSortingLog(OrderInfo info, LatticeSetting targetLattice, LatticeSetting resultLattice, UserInfo userInfo, int operationType, int status)
        {
            return sortingService.CreateOrderSortingLog(info, targetLattice, resultLattice, userInfo, operationType, status);
        }
        public static bool CreateErrorOrderSortingLog(List<ThreadSortOrder> ThreadSortOrderList, UserInfo userInfo, int operationType)
        {
            return sortingService.CreateErrorOrderSortingLog(ThreadSortOrderList, userInfo, operationType);
        }
        /// <summary>
        /// 查询分拣记录
        /// </summary>
        public static List<OrderSortingLog> GetOrderSortingLogByPageSize(string orderId, int operationType, int status, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, ref int recordCount)
        {
            return sortingService.GetOrderSortingLogByPageSize(orderId, operationType, status, startDate, endDate, pageIndex, pageSize, ref recordCount);
        }

        /// <summary>
        /// 获取最新的一条分拣记录
        /// </summary>
        /// <returns></returns>
        public static OrderSortingLog GetTheLastOrderSortingLog()
        {
            return sortingService.GetTheLastOrderSortingLog();
        }

        /// <summary>
        /// 清除格口内快件的分拣记录
        /// </summary>
        public static void ClearLatticeOrdersCache()
        {
            sortingService.ClearLatticeOrdersCache();
        }

        /// <summary>
        /// CreatePackingLog（operationType：1自动满格，2手动满格，3打印包牌号）
        /// </summary>
        /// <param name="lattice">柜格</param>
        /// <param name="operationType">操作类型：1自动满格，2手动满格，3打印包牌号</param>
        /// <returns></returns>
        public static PackingLog CreatePackingLog(LatticeSetting lattice, UserInfo userInfo, int operationType = 3)
        {
            return sortingService.CreatePackingLog(lattice, userInfo, _systemSetting.BoxWeight, operationType);
        }

        /// <summary>
        /// 创建装箱记录（多格口打印一个PKG标签）
        /// </summary>
        /// <param name="latticeIdArray">格口号</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public static PackingLog CreatePackingLog(string[] latticeIdArray, UserInfo userInfo)
        {
            return sortingService.CreatePackingLog(latticeIdArray, userInfo, _systemSetting.CriticalWeight, _systemSetting.BoxWeight);
        }
        /// <summary>
        /// 根据订单号撤回分拣
        /// </summary>
        /// <param name="orderIdArray"></param>
        /// <returns></returns>
        public static string DeleteOrderCacheByOrderId(string[] orderIdArray)
        {
            return sortingService.DeleteOrderCacheByOrderId(orderIdArray);
        }
        /// <summary>
        /// 根据格号撤回分拣
        /// </summary>
        /// <param name="latticeId"></param>
        /// <returns></returns>
        public static string DeleteOrderCacheByLatticeId(string latticeId)
        {
            return sortingService.DeleteOrderCacheByLatticeId(latticeId);
        }
        /// <summary>
        /// 获取装箱记录（用于PKG重打）
        /// </summary>
        /// <param name="latticeId">格号</param>
        /// <param name="pkgCode">PKG号</param>
        /// <returns></returns>
        public static PackingLog GetPackingLog(string latticeId, string pkgCode = "")
        {
            return sortingService.GetPackingLog(latticeId, pkgCode);
        }

        /// <summary>
        /// 查询打包记录
        /// </summary>
        public static List<PackingLog> GetPackingLogByPageSize(DateTime startDate, DateTime endDate, int pageIndex, int pageSize, ref int recordCount)
        {
            return sortingService.GetPackingLogByPageSize(startDate, endDate, pageIndex, pageSize, ref recordCount);
        }
        /// <summary>
        /// 判断格口是否已满
        /// </summary>
        /// <param name="latticeSettingId">格口Id</param>
        /// <returns></returns>
        public static bool IsFullLattice(int latticeSettingId)
        {
            var list = sortingService.GetTotalSortingDataList();
            return list == null ? false : list.Exists(sd => sd.LatticesettingId == latticeSettingId && ((sd.TotalWeight + _systemSetting.BoxWeight) > _systemSetting.CriticalWeight));

        }
        public static int GetSortingPatten()
        {
            return _systemSetting.SortingPatten;
        }
        #endregion

        public static List<SlaveConfig> GetSlaveConfig()
        {
            return _systemSetting.SlaveConfigs.Where(s => s.SlaveAddress > 0 && s.CabinetId <= _systemSetting.CabinetNumber).ToList();
        }
        /// <summary>
        /// 播放声音
        /// </summary>
        /// <param name="path"></param>
        public static void SoundAsny(string path)
        {
            soundService.playSoundAsny(path);
        }

        public static bool isCollect()
        {
           LoadSystemSetting();
           return SerialPortService.isCollect();
        }
        public static byte[] DoloadBoard(byte[] data)
        {
           return SerialPortService.DoloadBoard(data);
        }
        
    }
}
