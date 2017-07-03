using Newtonsoft.Json;
using SCB.OrderSorting.BLL.API;
using SCB.OrderSorting.BLL.Model;
using SCB.OrderSorting.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Data.Entity.Migrations;

namespace SCB.OrderSorting.BLL.Service
{
    /// <summary>
    /// 基础数据服务
    /// </summary>
    internal static class BaseDataService
    {
        private static string LoginDataFile = System.Windows.Forms.Application.StartupPath + @"\App_Data\LoginData.dat";
        private static string SystemSettingFile = System.Windows.Forms.Application.StartupPath + @"\App_Data\SystemSetting.xml";
        /// <summary>
        /// 获取所有国家地区信息
        /// </summary>
        /// <returns></returns>
        internal static List<Countrys> GetCountrysList()
        {
            using (var db = new OrderSortingDBEntities())
            {
                return db.Countrys.OrderBy(c => c.EnName).ToList();
            }
        }
        /// <summary>
        /// 获取所有邮寄方式信息
        /// </summary>
        /// <returns></returns>
        internal static List<Posttypes> GetPostTypesList()
        {
            using (var db = new OrderSortingDBEntities())
            {
                return db.Posttypes.OrderBy(pt => pt.CnPostName).ToList();
            }
        }
        /// <summary>
        /// 跟据渠道获取所有的区
        /// </summary>
        /// <returns></returns>
        internal static List<PostArea> GetPostAreaList(string PostTypeId)
        {
            using (var db = new OrderSortingDBEntities())
            {
                return db.PostArea.Where(o=>o.PostTypeId== PostTypeId).OrderBy(pt => pt.Flag).ToList();
            }
        }
        /// <summary>
        /// 更新邮寄方式
        /// </summary>
        internal static void UpdatePostTypes()
        {
            try
            {
                using (var db = new OrderSortingDBEntities())
                {
                    var now = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    var loginCount = db.LoginLog.Count(ll => ll.LoginTime > now);
                    if (loginCount > 0)
                        return;
                    //获取OA的全部邮寄方式
                    var dt = API_Helper.GetPostList();
                    if (dt != null && dt.Rows.Count > 1)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            Posttypes post = new Posttypes();
                            post.PostID = row["id"].ToString();
                            post.CnPostName = row["type"].ToString();
                            post.EnPostCode = row["entype"].ToString();
                            db.Posttypes.AddOrUpdate(post);
                        }
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
        /// 获取所有分拣方案
        /// </summary>
        /// <returns></returns>
        internal static List<SortingSolutions> GetSortingSolutionsList()
        {
            using (var db = new OrderSortingDBEntities())
            {
                return db.SortingSolutions.ToList();
            }
        }
        /// <summary>
        /// 根据Id删除分拣方案
        /// </summary>
        /// <param name="id"></param>
        internal static void DeleteSortingSolutionsById(string id)
        {
            using (var db = new OrderSortingDBEntities())
            {
                var ss = db.SortingSolutions.Find(id);
                db.SortingSolutions.Remove(ss);
                var spt = db.SolutionPostType.Where(sp => sp.SortingSolutionId == id);
                db.SolutionPostType.RemoveRange(spt);
                var sc = db.SolutionCountry.Where(sp => sp.SortingSolutionId == id);
                db.SolutionCountry.RemoveRange(sc);
                db.SaveChanges();
            }
        }

        internal static void SaveSystemSetting(SystemSetting setting)
        {
            using (StreamWriter sw = new StreamWriter(SystemSettingFile, false, Encoding.UTF8))
            {
                new XmlSerializer(typeof(SystemSetting)).Serialize(sw, setting, new XmlSerializerNamespaces());
                sw.Flush();
            }
            //string str = ObjectToBase64String(setting);
            //using (var db = new OrderSortingDBEntities())
            //{
            //    var sp = db.ConfigParamers.Find("SystemSetting");
            //    sp.Value = str;
            //    db.SaveChangesAsync();
            //}
        }

        internal static SystemSetting GetSystemSetting()
        {
            using (StreamReader sr = new StreamReader(SystemSettingFile, Encoding.UTF8))
            {
                return new XmlSerializer(typeof(SystemSetting)).Deserialize(sr) as SystemSetting;
            }
            //using (var db = new OrderSortingDBEntities())
            //{
            //    var sp = db.ConfigParamers.Find("SystemSetting");
            //    return Base64StringToObject<SystemSetting>(sp.Value);
            //}
        }

        internal static List<LoginData> GetLoginName()
        {

            //if (File.Exists(LoginDataFile))
            //{
            return ReadStreamListFromBase64String<List<LoginData>>(LoginDataFile);
            //}
            //else
            //{
            //    using (var db = new OrderSortingDBEntities())
            //    {
            //        var sp = db.ConfigParamers.Find("LoginData");
            //        return Base64StringToObject<List<LoginData>>(sp.Value);
            //    }
            //}
        }

        internal static void SaveLoginName(List<LoginData> users)
        {
            //if (File.Exists(LoginDataFile))
            //{
            WriteStreamToBase64String(LoginDataFile, users);
            //}
            //else
            //{
            //    using (var db = new OrderSortingDBEntities())
            //    {
            //        var str = ObjectToBase64String(users);
            //        var sp = db.ConfigParamers.Find("LoginData");
            //        sp.Value = str;
            //        db.SaveChangesAsync();
            //    }
            //}
        }

        private static string ObjectToBase64String(object obj)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(JsonConvert.SerializeObject(obj)));
        }
        private static T Base64StringToObject<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.Default.GetString(Convert.FromBase64String(str)));
        }

        private static T ReadStreamListFromBase64String<T>(string path)
        {
            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                return Base64StringToObject<T>(sr.ReadToEnd());
            }
        }

        private static void WriteStreamToBase64String(string path, object data)
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.Write(ObjectToBase64String(data));
                sw.Flush();
            }
        }
    }
}
