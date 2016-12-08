using SCB.OrderSorting.BLL.Common;
using SCB.OrderSorting.BLL.Context;
using SCB.OrderSorting.BLL.Model;
using SCB.OrderSorting.BLL.Model.CacheModel;
using SCB.OrderSorting.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCB.OrderSorting.BLL.Service
{
    internal class SortingService
    {
        #region private
        private SortingPattenContext _sortingPattenWorker;
        private SizeChangeContext _sizeContext;
        private CenterContext _centerContext;
        /// <summary>
        /// 从机数量（1-4）
        /// </summary>
        private int _cabinetNumber;
        /// <summary>
        /// 分拣模式：1按渠道分拣，2按地区分拣，3按地区和渠道分拣
        /// </summary>
        private int _sortingPatten;
        /// <summary>
        /// 分拣方案id
        /// </summary>
        private string _sortingSolution;
        /// <summary>
        /// 是否调用飞特接口
        /// </summary>
        private bool _isFlyt;
        /// <summary>
        /// 包装箱重量
        /// </summary>
        private decimal _boxWeight;
        /// <summary>
        /// 格口与国家地区的关联
        /// </summary>
        private List<SolutionCountry> _solutionCountryList;
        /// <summary>
        /// 格口与邮寄方式的关联
        /// </summary>
        private List<SolutionPostType> _solutionPostTypeList;
        /// <summary>
        /// 格口与邮寄方式地区的关联
        /// </summary>
        private List<SolutionPostArea> _solutionPostAreaList;
        /// <summary>
        /// 格口设置信息
        /// </summary>
        private List<LatticeSetting> _latticeSettingList;
        /// <summary>
        /// 分拣统计信息
        /// </summary>
        private List<TotalSortingData> _totalTotalSortingDataList;
        #endregion

        #region internal
        /// <summary>
        /// 分拣数据服务构造函数
        /// </summary>
        /// <param name="number">从机数</param>
        /// <param name="patten">分拣模式</param>
        /// <param name="solution">分拣方案id</param>
        /// <param name="isFlyt">是否调用飞特接口</param>
        /// <param name="boxWeight"></param>
        internal SortingService(int number, int patten, string solution, bool isFlyt, decimal boxWeight)
        {
            try
            {
                _cabinetNumber = number;
                _sortingPatten = patten;
                _sortingSolution = solution;
                _isFlyt = isFlyt;
                _boxWeight = boxWeight;
                //重新统计分拣信息
                LoadTotalSortingDataList();
                CreateFormSizeChangeContext();
                CreateCenterContext();
                LoadLatticeSetting();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CreateCenterContext()
        {
            if (_isFlyt)
                _centerContext = new CenterContextFlyt();
            else
                _centerContext = new CenterContextDefault();
        }

        private void CreateSortingPattenWorker()
        {
            switch (_sortingPatten)
            {
                case 1:
                    _sortingPattenWorker = new SortingPattenContext1(_latticeSettingList, _solutionPostTypeList,_solutionPostAreaList);
                    break;
                case 2:
                    _sortingPattenWorker = new SortingPattenContext2(_latticeSettingList, _solutionCountryList);
                    break;
                default:
                    _sortingPattenWorker = new SortingPattenContext3(_latticeSettingList, _solutionPostTypeList, _solutionCountryList);
                    break;
            }
        }

        internal List<DayReportViewModel> GetDayReportByPageSize(DateTime startDate, DateTime endDate, int pageIndex, int pageSize, ref int recordCount)
        {
            try
            {
                //分页查询，默认每页20条
                string sql1 = @"SELECT substr(OperationTime,0,11) as '操作时间',count(*) as '总计',
                                sum(CASE WHEN OperationType = '1' THEN 1 ELSE 0 END) AS '待投递', 
                                sum(CASE WHEN OperationType = '2' THEN 1 ELSE 0 END) AS '已投递', 
                                sum(CASE WHEN OperationType = '3' THEN 1 ELSE 0 END) AS '投递异常', 
                                sum(CASE WHEN OperationType = '4' THEN 1 ELSE 0 END) AS '重复扫描'
                                FROM OrderSortingLog
                                ";
                //计算总数
                string sql2 = "SELECT COUNT(*) rowCount FROM (SELECT id FROM  OrderSortingLog ";
                string where = " WHERE (OperationTime between datetime(@startDate) and datetime(@endDate))";
                //参数
                var sqlParams = new List<SQLiteParameter>()
                {
                    new SQLiteParameter("@startDate", startDate.ToString("yyyy-MM-dd")),
                    new SQLiteParameter("@endDate", endDate.AddDays(1).ToString("yyyy-MM-dd"))
                    
                };
                var list = new List<OrderSortingLog>();
                
                sql1 += where + " group by substr(OperationTime, 0, 11) "+ string.Format(" Order By OperationTime Desc limit {0} offset {1} ",
                    pageSize, pageSize * (pageIndex - 1));
                sql2 += where + " group by substr(OperationTime, 0, 11) )";
                using (var db = new OrderSortingDBEntities())
                {
                    recordCount = db.Database.SqlQuery<int>(sql2, sqlParams.ToArray()).First();
                    return db.Database.SqlQuery<DayReportViewModel>(sql1, sqlParams.ToArray()).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CreateFormSizeChangeContext()
        {
            switch (_cabinetNumber)
            {
                case 1:
                    _sizeContext = new SizeChangeContext12();
                    break;
                case 2:
                    _sizeContext = new SizeChangeContext24();
                    break;
                default:
                    _sizeContext = new SizeChangeContext48();
                    break;
            }
        }
        /// <summary>
        /// 添加登录记录
        /// </summary>
        /// <param name="userName">用户名</param>
        internal void AddLoginLog(string userName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName)) return;
                using (var db = new OrderSortingDBEntities())
                {
                    LoginLog log = new LoginLog();
                    log.Id = Guid.NewGuid().ToString();
                    log.UserName = userName;
                    log.LoginTime = DateTime.Now;
                    db.LoginLog.Add(log);
                    db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
            }
        }
        /// <summary>
        /// 更新格口与邮寄方式的关联
        /// </summary>
        /// <param name="latticesetting">格口信息</param>
        /// <param name="posttypesList">邮寄方式</param>
        internal void UpdateSolutionPostType(LatticeSetting latticesetting, List<Posttypes> posttypeslist)
        {
            try
            {
                var newData = (from pt in posttypeslist
                               select new SolutionPostType
                               {
                                   Id = Guid.NewGuid().ToString(),
                                   SortingSolutionId = _sortingSolution,
                                   CabinetId = latticesetting.CabinetId,
                                   LatticeSettingId = latticesetting.ID,
                                   PostTypeId = pt.PostID,
                                   PostTypeName = pt.CnPostName,
                               });
                using (var db = new OrderSortingDBEntities())
                {
                    var oldData = db.SolutionPostType.Where(sp => sp.LatticeSettingId == latticesetting.ID && sp.SortingSolutionId == _sortingSolution);
                    db.SolutionPostType.RemoveRange(oldData);
                    db.SolutionPostType.AddRange(newData);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _solutionPostTypeList = GetSolutionPostTypeList();
            }
        }
        /// <summary>
        ///  更新格口与邮寄方式地区的关联
        /// </summary>
        /// <param name="latticesetting"></param>
        /// <param name="posttypeslist"></param>
        internal void UpdateSolutionPostArea(LatticeSetting latticesetting, List<PostArea> postArealist, string PostTypeId)
        {
            try
            {
                var newData = (from pt in postArealist
                               select new SolutionPostArea
                               {
                                   ID = Guid.NewGuid().ToString(),
                                   SortingSolutionId = _sortingSolution,
                                   Type=pt.Type,
                                   Area=pt.Area,
                                   Flag=pt.Flag,
                                   PostTypeId=pt.PostTypeId,
                                   LactticeSettingId= latticesetting.ID,
                                   CabinetId = latticesetting.CabinetId
                               });
                using (var db = new OrderSortingDBEntities())
                {
                    var oldData = db.SolutionPostArea.Where(sp => sp.LactticeSettingId == latticesetting.ID && sp.CabinetId == latticesetting.CabinetId && sp.PostTypeId == PostTypeId && sp.SortingSolutionId == _sortingSolution);
                    db.SolutionPostArea.RemoveRange(oldData);
                    db.SolutionPostArea.AddRange(newData);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _solutionPostAreaList = GetSolutionPostAreaList();
            }
        }
        /// <summary>
        /// 更新格口与国家地区的关联
        /// </summary>
        /// <param name="latticesetting">格口信息</param>
        /// <param name="countrysList">国家地区</param>
        internal void UpdateSolutionCountry(LatticeSetting latticesetting, List<Countrys> countrysList)
        {
            try
            {
                var newData = (from pt in countrysList
                               select new SolutionCountry
                               {
                                   Id = Guid.NewGuid().ToString(),
                                   SortingSolutionId = _sortingSolution,
                                   CabinetId = latticesetting.CabinetId,
                                   LatticeSettingId = latticesetting.ID,
                                   CountryId = pt.ID,
                                   CountryName = pt.CnName
                               });
                using (var db = new OrderSortingDBEntities())
                {
                    var oldData = db.SolutionCountry.Where(sp => sp.LatticeSettingId == latticesetting.ID && sp.SortingSolutionId == _sortingSolution);
                    db.SolutionCountry.RemoveRange(oldData);
                    db.SolutionCountry.AddRange(newData);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _solutionCountryList = GetSolutionCountryList();
            }
        }

        private List<SolutionCountry> GetSolutionCountryList()
        {
            try
            {
                using (var db = new OrderSortingDBEntities())
                {
                    return db.SolutionCountry.Where(ls => ls.SortingSolutionId == _sortingSolution && ls.CabinetId <= _cabinetNumber).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<SolutionPostType> GetSolutionPostTypeList()
        {
            try
            {
                using (var db = new OrderSortingDBEntities())
                {
                    return db.SolutionPostType.Where(ls => ls.SortingSolutionId == _sortingSolution && ls.CabinetId <= _cabinetNumber).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private List<SolutionPostArea> GetSolutionPostAreaList()
        {
            try
            {
                using (var db = new OrderSortingDBEntities())
                {
                    return db.SolutionPostArea.Where(ls => ls.SortingSolutionId == _sortingSolution).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 根据格口Id获取格口与国家地区的关联关系
        /// </summary>
        /// <param name="latticeSettingId">格口Id</param>
        internal List<SolutionCountry> GetSolutionCountryListByLatticeSettingId(int latticeSettingId)
        {
            try
            {
                return _solutionCountryList.Where(sc => sc.LatticeSettingId == latticeSettingId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 根据格号撤回分拣
        /// </summary>
        /// <param name="latticeId"></param>
        /// <returns></returns>
        internal string DeleteOrderCacheByLatticeId(string latticeId)
        {
            try
            {
                using (var db = new OrderSortingDBEntities())
                {
                    var lattice = db.LatticeSetting.FirstOrDefault(ls => ls.LatticeId == latticeId);
                    var logCache = db.LatticeOrdersCache.Where(o => o.LatticesettingId == lattice.ID);
                    db.LatticeOrdersCache.RemoveRange(logCache);
                    db.SaveChanges();
                    //重新统计分拣信息
                    LoadTotalSortingDataList(db);
                }
                return "撤回成功！";
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 根据订单号撤回分拣
        /// </summary>
        /// <param name="orderIdArra"></param>
        /// <returns></returns>
        internal string DeleteOrderCacheByOrderId(string[] orderIdArra)
        {
            try
            {
                using (var db = new OrderSortingDBEntities())
                {
                   
                    //验证
                    foreach (string orderID in orderIdArra)
                    {
                        if (orderID == "") continue;
                        if (db.LatticeOrdersCache.Where(o => o.OrderId == orderID).Count() <= 0)
                        {
                            return $"撤回失败，未找到订单号:{orderID}，请核对订单号是否正确！";
                        }
                    }
                  
                    var logCache = db.LatticeOrdersCache.Where(o => orderIdArra.Contains(o.OrderId));
                    
                    db.LatticeOrdersCache.RemoveRange(logCache);
                    db.SaveChanges();
                    //重新统计分拣信息
                    LoadTotalSortingDataList(db);
                  
                }
                return $"撤回成功！";
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 根据格口Id获取格口与邮寄方式关联关系
        /// </summary>
        /// <param name="latticeSettingId">格口Id</param>
        internal List<SolutionPostType> GetSolutionPostTypeListByLatticeSettingId(int latticeSettingId)
        {
            try
            {
                return _solutionPostTypeList.Where(sc => sc.LatticeSettingId == latticeSettingId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        internal List<LatticeSetting> GetLatticeSettingByOrderinfoList(OrderInfo info)
        {
            try
            {
                return _sortingPattenWorker.GetLatticeSettingByOrderinfoList(info);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 根据格口Id获取格口与邮寄方式地区关联关系
        /// </summary>
        /// <param name="latticeSettingId"></param>
        /// <returns></returns>
        internal List<SolutionPostArea> GetSolutionPostAreaListByLatticeSettingId(string PostTypeId, int latticeSettingId)
        {
            try
            {
                return _solutionPostAreaList.Where(sc => sc.PostTypeId == PostTypeId && sc.LactticeSettingId== latticeSettingId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取格口设置信息
        /// </summary>
        /// <returns></returns>
        internal List<LatticeSetting> GetLatticeSettingList()
        {
            try
            {
                return _latticeSettingList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 加载格口设置信息
        /// </summary>
        /// <returns></returns>
        internal List<LatticeSetting> LoadLatticeSetting()
        {
            try
            {
                using (var db = new OrderSortingDBEntities())
                {
                    _latticeSettingList = db.LatticeSetting.Where(ls => ls.CabinetId <= _cabinetNumber).ToList();
                }
                _solutionCountryList = GetSolutionCountryList();
                _solutionPostTypeList = GetSolutionPostTypeList();
                _solutionPostAreaList = GetSolutionPostAreaList();
                CreateSortingPattenWorker();
                return _latticeSettingList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 判断格口号是否已存在
        /// </summary>
        /// <param name="latticeId">格口号</param>
        internal bool IsLatticeIdExists(string latticeId)
        {
            try
            {
                using (var db = new OrderSortingDBEntities())
                {
                    return db.LatticeSetting.Any(als => als.LatticeId == latticeId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据订单信息获取匹配的格口
        /// </summary>
        /// <param name="info">订单信息</param>
        internal LatticeSetting GetLatticeSettingByOrderinfo(OrderInfo info)
        {
            try
            {
                return _sortingPattenWorker.GetLatticeSettingByOrderinfo(info);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 新建分拣方案
        /// </summary>
        /// <param name="solutionName"></param>
        /// <returns></returns>
        internal string CreateNewSolution(string solutionName)
        {
            try
            {
                var SolutionId = Guid.NewGuid().ToString();
                using (var db = new OrderSortingDBEntities())
                {
                    if (db.SortingSolutions.Any(ss => ss.Name == solutionName))
                        throw new Exception("方案名重复！");
                    //保存方案名
                    SortingSolutions solution = new SortingSolutions();
                    solution.Id = SolutionId;
                    solution.Name = solutionName;
                    db.SortingSolutions.Add(solution);
                    ////方案另存为
                    //_solutionCountryList.ForEach(sc =>
                    //{
                    //    sc.SortingSolutionId = SolutionId;
                    //    sc.Id = Guid.NewGuid().ToString();
                    //});
                    //db.SolutionCountry.AddRange(_solutionCountryList);
                    //_solutionPostTypeList.ForEach(sc =>
                    //{
                    //    sc.SortingSolutionId = SolutionId;
                    //    sc.Id = Guid.NewGuid().ToString();
                    //});
                    //db.SolutionPostType.AddRange(_solutionPostTypeList);
                    db.SaveChanges();
                }
                _solutionCountryList.Clear();
                _solutionPostTypeList.Clear();
                return SolutionId;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 方案重命名
        /// </summary>
        /// <param name="sortingSolutionId">方案Id</param>
        /// <param name="solutionName">方案新名称</param>
        internal void RenameSolution(string sortingSolutionId, string solutionName)
        {
            try
            {
                using (var db = new OrderSortingDBEntities())
                {
                    var solution = db.SortingSolutions.Find(sortingSolutionId);
                    if (solution != null)
                    {
                        solution.Name = solutionName;
                        db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 根据格口id获取格口设置信息
        /// </summary>
        /// <param name="latticesettingId">格口id</param>
        internal LatticeSetting GetLatticeSettingById(int latticesettingId)
        {
            try
            {
                return _latticeSettingList.Find(ls => ls.ID == latticesettingId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存格口设置信息
        /// </summary>
        /// <param name="latticesetting"></param>
        internal void SaveLatticeSetting(LatticeSetting latticesetting)
        {
            try
            {
                using (var db = new OrderSortingDBEntities())
                {
                    db.LatticeSetting.AddOrUpdate(latticesetting);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取界面格口按钮的定位和大小属性（窗体SizeChange时调用）
        /// </summary>
        /// <param name="formWidth">窗体宽度</param>
        /// <param name="formHeight">窗体高度</param>
        internal List<ButtonPointModel> CreateButtonPointModelList(int formWidth, int formHeight)
        {
            return DelegateCreateList(CreateButtonPointModel, formWidth, formHeight);
        }

        /// <summary>
        /// 获取界面格口按钮的定位和大小属性
        /// </summary>
        /// <param name="ls">格口设置信息</param>
        /// <returns></returns>
        private ButtonPointModel CreateButtonPointModel(LatticeSetting ls)
        {
            return new ButtonPointModel()
            {
                LatticesettingId = ls.ID,
                ButtonLocation = new Point(_sizeContext.X, _sizeContext.Y),
                ButtonSize = new Size(_sizeContext.BtnWidth, _sizeContext.BtnHeight),
                ButtonFont = new Font("微软雅黑", _sizeContext.EmSize, FontStyle.Bold, GraphicsUnit.Point, 134)
            };
        }
        /// <summary>
        /// 获取界面格口按钮（窗体OnLoad时调用）
        /// </summary>
        /// <param name="formWidth">窗体宽度</param>
        /// <param name="formHeight">窗体高度</param>
        internal List<Button> CreateButtonList(int formWidth, int formHeight)
        {
            return DelegateCreateList(CreateButton, formWidth, formHeight);
        }
        /// <summary>
        /// 创建界面格口按钮
        /// </summary>
        /// <param name="ls">格口设置信息</param>
        /// <returns></returns>
        private Button CreateButton(LatticeSetting ls)
        {
            return new Button()
            {
                Name = ls.ID.ToString(),//button的命名
                TabIndex = ls.ID,//按下tab的切换顺序索引
                Text = GetLatticeNewText(ls),//button中text所显示的内容 
                Location = new Point(_sizeContext.X, _sizeContext.Y),
                Size = new Size(_sizeContext.BtnWidth, _sizeContext.BtnHeight),
                Font = new Font("微软雅黑", _sizeContext.EmSize, FontStyle.Bold, GraphicsUnit.Point, 134)
            };
        }

        public List<T> DelegateCreateList<T>(Func<LatticeSetting, T> funcCreate, int formWidth, int formHeight)
        {
            var list = new List<T>();
            _sizeContext.BeforeAdd(formWidth, formHeight);
            _latticeSettingList.ForEach(ls =>
            {
                list.Add(funcCreate(ls));
                _sizeContext.AfterAdd();
            });
            return list;
        }
        /// <summary>
        /// 获取（要显示到分拣作业窗体按钮上的）格口信息
        /// </summary>
        /// <param name="ls">格口设置信息</param>
        /// <returns></returns>
        internal string GetLatticeNewText(LatticeSetting ls)
        {
            try
            {
                //获取格口信息
                var text = _sortingPattenWorker.CreateButtonText(ls);
                //获取统计信息
                var total = _totalTotalSortingDataList.FirstOrDefault(tsd => tsd.LatticesettingId == ls.ID);
                if (total != null)
                {
                    text += Environment.NewLine + string.Format("数量：{0}\r\n重量：{1}Kg", total.TotalNum, _boxWeight + total.TotalWeight);
                }
                return text;
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        /// <summary>
        /// 创建装箱记录（operationType：1自动满格，2手动满格，3打印包牌号）
        /// </summary>
        /// <param name="lattice">柜格</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="boxWeight">箱子重量</param>
        /// <param name="operationType">操作类型：1自动满格，2手动满格，3打印包牌号</param>
        /// <returns></returns>
        internal PackingLog CreatePackingLog(LatticeSetting lattice, UserInfo userInfo, decimal boxWeight, int operationType = 3)
        {
            try
            {
                Debug.WriteLine("CreatePackingLog begin");
                return _centerContext.CreatePackingLog(lattice, userInfo, boxWeight, operationType);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //重新统计分拣信息
                LoadTotalSortingDataList();
            }
        }

        /// <summary>
        /// 创建装箱记录（多格口打印一个PKG标签）
        /// </summary>
        /// <param name="latticeIdArray">格口号</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="criticalWeight">临界重量</param>
        /// <param name="boxWeight">箱子重量</param>
        /// <returns></returns>
        internal PackingLog CreatePackingLog(string[] latticeIdArray, UserInfo userInfo, decimal criticalWeight, decimal boxWeight)
        {
            try
            {
                return _centerContext.CreatePackingLog(latticeIdArray, userInfo, criticalWeight, boxWeight);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //重新统计分拣信息
                LoadTotalSortingDataList();
            }
        }
        internal bool CreateErrorOrderSortingLog(List<ThreadSortOrder> ThreadSortOrderList, UserInfo userInfo, int operationType)
        {
            try
            {
                //记录分拣日志

                using (var db = new OrderSortingDBEntities())
                {
                    foreach (ThreadSortOrder entity in ThreadSortOrderList)
                    {
                        for (int i = 0; i < entity.TargetLattice.Count; i++)
                        {
                            OrderSortingLog sortingLog = NewOrderSortingLog(entity.OrderInfo, entity.TargetLattice[i], entity.ResultLattice, userInfo, operationType, 3);
                            db.OrderSortingLog.Add(sortingLog);
                        }
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 调用外部接口，根据订单号获取订单数据（国家、渠道）
        /// </summary>
        /// <param name="processCenterID"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        internal OrderInfo GetOrderInfoById(string orderId, UserInfo userInfo, ref string errorMsg)
        {
            try
            {
                return _centerContext.GetOrderInfoById(orderId, userInfo);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// 根据格口Id获取格口内的订单信息
        /// </summary>
        /// <param name="latticesettingId"></param>
        /// <returns></returns>
        internal List<LatticeOrdersCache> GetLatticeOrdersListByLatticesettingId(int latticesettingId)
        {
            try
            {
                using (var db = new OrderSortingDBEntities())
                {
                    return db.LatticeOrdersCache.Where(lo => lo.LatticesettingId == latticesettingId).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 创建分拣记录
        /// </summary>
        /// <param name="info">订单信息</param>
        /// <param name="targetLattice">目标柜格</param>
        /// <param name="resultLattice">投入柜格</param>
        /// <param name="operationType">操作类型：1扫描，2投递，3异常恢复</param>
        /// <param name="status">状态：1待投递，2已投递，3投递异常,4重复扫描</param>
        internal bool CreateOrderSortingLog(OrderInfo info, LatticeSetting targetLattice, LatticeSetting resultLattice, UserInfo userInfo, int operationType, int status)
        {
            try
            {
                //记录分拣日志
                OrderSortingLog sortingLog = NewOrderSortingLog(info, targetLattice, resultLattice, userInfo, operationType, status);
                using (var db = new OrderSortingDBEntities())
                {
                    db.OrderSortingLog.Add(sortingLog);
                    //正确投入，计数
                    if (status == 2 && !db.LatticeOrdersCache.Any(o => o.OrderId == info.OrderId))
                    {
                        var countryEnName = db.Countrys.Where(cntr => cntr.ID == info.CountryId).Select(cnt => cnt.EnName).FirstOrDefault();
                        db.LatticeOrdersCache.Add(new LatticeOrdersCache()
                        {
                            LatticesettingId = resultLattice.ID,
                            OrderId = info.OrderId,
                            TraceId = info.TraceId,
                            Weight = info.Weight,
                            CountryId = info.CountryId,
                            CountryName = countryEnName,
                            PostId = info.PostId,
                            PostName = info.PostName
                        });
                    }
                    db.SaveChanges();
                    //重新统计分拣信息
                    LoadTotalSortingDataList(db);
                    return true;
                }
            }
            catch (Exception ex)
            {
                SaveErrLogHelper.SaveErrorLog(string.Empty, ex.ToString());
                return false;
            }
        }

        private OrderSortingLog NewOrderSortingLog(OrderInfo info, LatticeSetting targetLattice, LatticeSetting resultLattice, UserInfo userInfo, int operationType, int status)
        {
            var sortingLog = new OrderSortingLog()
            {
                ID = Guid.NewGuid().ToString(),
                OrderId = info.OrderId,
                TargetCabinetId = targetLattice.CabinetId.ToString(),
                TargetLatticeId = targetLattice.LatticeId,
                ResultCabinetId = "",
                ResultLatticeId = "",
                OperationType = operationType,
                OperationTime = DateTime.Now,
                Status = status,
                UserId = userInfo.UserId,
                UserName = userInfo.UserName,
                Weight = info.Weight
            };
            if (resultLattice != null)
            {
                sortingLog.ResultCabinetId = resultLattice.CabinetId.ToString();
                sortingLog.ResultLatticeId = resultLattice.LatticeId;
            }
            return sortingLog;
        }

        /// <summary>
        /// 查询分拣记录
        /// </summary>
        internal List<OrderSortingLog> GetOrderSortingLogByPageSize(string orderId, int operationType, int status, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, ref int recordCount)
        {
            try
            {
                //分页查询，默认每页20条
                string sql1 = "SELECT * FROM OrderSortingLog o ";
                //计算总数
                string sql2 = "SELECT COUNT(ID) rowCount FROM OrderSortingLog o ";
                string where = " WHERE (OperationTime between datetime(@startDate) and datetime(@endDate))";
                //参数
                var sqlParams = new List<SQLiteParameter>()
                {
                    new SQLiteParameter("@startDate", startDate.ToString("yyyy-MM-dd")),
                    new SQLiteParameter("@endDate", endDate.AddDays(1).ToString("yyyy-MM-dd"))
                };
                var list = new List<OrderSortingLog>();
                //判断有没有输入OrderID
                if (!string.IsNullOrWhiteSpace(orderId))
                {
                    where += " and o.OrderId = @orderId ";
                    sqlParams.Add(new SQLiteParameter("@orderId", orderId));
                }
                if (operationType > 0)
                {
                    where += " and o.OperationType = @operationType ";
                    sqlParams.Add(new SQLiteParameter("@operationType", operationType));
                }
                if (status > 0)
                {
                    where += " and o.Status = @status ";
                    sqlParams.Add(new SQLiteParameter("@status", status));
                }
                sql1 += where + string.Format(" Order By o.OperationTime Desc limit {0} offset {1} ",
                    pageSize, pageSize * (pageIndex - 1));
                sql2 += where;
                using (var db = new OrderSortingDBEntities())
                {
                    recordCount = db.Database.SqlQuery<int>(sql2, sqlParams.ToArray()).First();
                    return db.Database.SqlQuery<OrderSortingLog>(sql1, sqlParams.ToArray()).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取最新的一条分拣记录
        /// </summary>
        /// <returns></returns>
        internal OrderSortingLog GetTheLastOrderSortingLog()
        {
            try
            {
                using (var db = new OrderSortingDBEntities())
                {
                    return db.OrderSortingLog.OrderByDescending(o => o.OperationTime).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 清除格口内快件的分拣记录
        /// </summary>
        internal void ClearLatticeOrdersCache()
        {
            try
            {
                using (var db = new OrderSortingDBEntities())
                {
                    db.Database.ExecuteSqlCommandAsync("DELETE FROM LatticeOrdersCache", new List<SQLiteParameter>());
                    db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 重新统计分拣信息
        /// </summary>
        /// <param name="db"></param>
        internal void LoadTotalSortingDataList(OrderSortingDBEntities db)
        {
            try
            {
                var sql = @"
SELECT   ls.ID AS LatticesettingId, IFNULL(SUM(loc.Weight), 0) AS TotalWeight, COUNT(loc.LatticesettingId) AS TotalNum
FROM      LatticeSetting ls LEFT OUTER JOIN
                LatticeOrdersCache loc ON ls.ID = loc.LatticesettingId
WHERE   (ls.CabinetId <= @number)
GROUP BY ls.ID";
                var paramter = new SQLiteParameter("@number", _cabinetNumber);
                _totalTotalSortingDataList = db.Database.SqlQuery<TotalSortingData>(sql, paramter).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 重新统计分拣信息
        /// </summary>
        internal void LoadTotalSortingDataList()
        {
            try
            {
                using (var db = new OrderSortingDBEntities())
                {
                    LoadTotalSortingDataList(db);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取分拣统计信息
        /// </summary>
        /// <returns></returns>
        internal List<TotalSortingData> GetTotalSortingDataList()
        {
            return _totalTotalSortingDataList;
        }
        /// <summary>
        /// 获取装箱记录（用于PKG重打）
        /// </summary>
        /// <param name="latticeId">格号</param>
        /// <param name="pkgCode">PKG号</param>
        internal PackingLog GetPackingLog(string latticeId, string pkgCode = "")
        {
            try
            {
                using (var db = new OrderSortingDBEntities())
                {
                    if (!string.IsNullOrWhiteSpace(latticeId))
                    {
                        return db.PackingLog.OrderByDescending(pkg => pkg.OperationTime).FirstOrDefault(pkg => pkg.LatticeId == latticeId);
                    }
                    if (!string.IsNullOrWhiteSpace(pkgCode))
                    {
                        return db.PackingLog.FirstOrDefault(p => p.PackNumber == pkgCode);
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 查询打包记录
        /// </summary>
        internal List<PackingLog> GetPackingLogByPageSize(DateTime startDate, DateTime endDate, int pageIndex, int pageSize, ref int recordCount)
        {
            try
            {
                //分页查询，默认每页20条
                string sql1 = @"SELECT * 
                                FROM packinglog o  
                                WHERE (OperationTime between datetime(@startDate) and datetime(@endDate))";
                //计算总数
                string sql2 = @"SELECT COUNT(o.ID) rowCount FROM packinglog o 
                                WHERE (OperationTime between datetime(@startDate) and datetime(@endDate))";
                //参数
                var sqlParams = new SQLiteParameter[2]
                {
                    new SQLiteParameter("@startDate", startDate.ToString("yyyy-MM-dd")),
                    new SQLiteParameter("@endDate", endDate.AddDays(1).ToString("yyyy-MM-dd"))
                };
                sql1 += string.Format(" Order By o.OperationTime Desc limit {0} offset {1} ",
                    pageSize, pageSize * (pageIndex - 1));
                using (var db = new OrderSortingDBEntities())
                {
                    recordCount = db.Database.SqlQuery<int>(sql2, sqlParams).First();
                    return db.Database.SqlQuery<PackingLog>(sql1, sqlParams).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
