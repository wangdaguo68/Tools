using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Data.Entity.Validation;
using System.Collections;

namespace King.Tools
{
    /// <summary>
    /// Content:在EF的基础上通过泛型进行二次封装，代码重用，增删改查一步到位，同时封装了EF较为缺陷的存储过程调用
    ///         多表联查可以使用linq表达式经行，并不会有什么影响
    ///         Ex：var item = from a in db.Set<FKW_CompleteInfo>()
    ///             join b in db.Set<FKW_SurveyInfo>()
    ///             on a.TaskNo equals b.TaskNo
    ///             where a.IsValid == "1"
    ///             select new Queryable
    ///             {
    ///
    ///             };
    /// Author:王达国
    /// Time:2015.11.11
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EFHelp<T> where T : class
    {

        /// <summary>
        /// 批量插入实体列表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="modelEntity"></param>
        public static void Add(DbContext db, params T[] modelEntity)
        {
            foreach (var model in modelEntity)
            {
                db.Entry(model).State = EntityState.Added;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 插入单个实体
        /// </summary>
        /// <param name="db"></param>
        /// <param name="model"></param>
        public static void Add(DbContext db, T model)
        {
            db.Entry(model).State = EntityState.Added;
            db.SaveChanges();
        }

        /// <summary>
        /// 简单查询
        /// </summary>
        /// <param name="where"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetSearchList(Expression<Func<T, bool>> where, DbContext db)
        {
            var item = db.Set<T>().Where(where).ToList();
            return item;
        }

        /// <summary>
        /// 查询并转成DataTable
        /// </summary>
        /// <param name="where"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static System.Data.DataTable GetSearchTable(Expression<Func<T, bool>> where, DbContext db)
        {
            var item = db.Set<T>().Where(where).ToList();
            return ListToDataTable(item);
        }

        /// <summary>
        /// 实体分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetSearchListByPage<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderBy, int pageSize, int pageIndex, DbContext db)
        {
            var item = db.Set<T>().Where(where).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return item;
        }

        /// <summary>
        /// 泛型 +反射 实现动态Update
        /// </summary>
        /// <param name="db"></param>
        /// <param name="modelEntity"></param>
        public static void Update(DbContext db, params T[] modelEntity)
        {
            try
            {
                foreach (var model in modelEntity)
                {
                    Type type = typeof(T);
                    var item = db.Entry(model);
                    string fieldName = type.GetProperties()[0].Name;
                    string fileValue = item.Property(fieldName).CurrentValue.ToString();
                    db.Database.ExecuteSqlCommand("delete from " + typeof(T).Name + " where " + fieldName + "= '" + fileValue + "'");
                    db.Entry(model).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException dx) { }
        }

        /// <summary>
        /// 修改单个实体
        /// </summary>
        /// <param name="db"></param>
        /// <param name="model"></param>
        public static void Update(DbContext db, T model)
        {
            try
            {
                Type type = typeof(T);
                var item = db.Entry(model);
                string fieldName = type.GetProperties()[0].Name;
                string fileValue = item.Property(fieldName).CurrentValue.ToString();
                db.Database.ExecuteSqlCommand("delete from " + typeof(T).Name + " where " + fieldName + "= '" + fileValue + "'");
                db.Entry(model).State = EntityState.Added;
                db.SaveChanges();
            }
            catch (DbEntityValidationException dx) { }
        }

        /// <summary>
        /// 泛型根据lambda表达式去执行删除
        /// </summary>
        /// <param name="db"></param>
        /// <param name="where"></param>
        public static int Delete(DbContext db, Expression<Func<T, bool>> where)
        {
            try
            {
                var model = db.Set<T>().Where(where).FirstOrDefault();
                db.Entry(model).State = EntityState.Deleted;
                db.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 泛型根据实体列表去删除
        /// </summary>
        /// <param name="db"></param>
        /// <param name="modelEntity"></param>
        public static void Delete(DbContext db, T[] modelEntity)
        {
            foreach (var model in modelEntity)
            {
                db.Entry(model).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 泛型根据单个实体删除
        /// </summary>
        /// <param name="db"></param>
        /// <param name="modelEntity"></param>
        public static void Delete(DbContext db, T modelEntity)
        {
            db.Entry(modelEntity).State = EntityState.Deleted;
            db.SaveChanges();
        }

        /// <summary>
        /// 执行存储过程返回DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parms"></param>
        /// <param name="procName"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static System.Data.DataTable ExecProcToDataTable(SqlParameter[] parms, string procName, DbContext db)
        {
            try
            {
                string sql = procName + " ";
                for (int i = 0; i < parms.Length; i++)
                {
                    if (i == 0)
                    {
                        sql += parms[i].ParameterName;
                    }
                    else
                    {
                        sql += ", " + parms[i].ParameterName;
                    }
                }
                var result = db.Database.SqlQuery<T>(sql, parms).ToList<T>();
                System.Data.DataTable dt = ListToDataTable(result);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行存储过程返回Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parms"></param>
        /// <param name="procName"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static T ExecProcToModel(SqlParameter[] parms, string procName, DbContext db)
        {
            try
            {
                string sql = procName + " ";
                for (int i = 0; i < parms.Length; i++)
                {
                    if (i == 0)
                    {
                        sql += parms[i].ParameterName;
                    }
                    else
                    {
                        sql += ", " + parms[i].ParameterName;
                    }
                }
                return db.Database.SqlQuery<T>(sql, parms).ToList<T>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行存储过程返回Model集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parms"></param>
        /// <param name="procName"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static IEnumerable<T> ExecProcToModelList(SqlParameter[] parms, string procName, DbContext db)
        {
            try
            {
                string sql = procName + " ";
                for (int i = 0; i < parms.Length; i++)
                {
                    if (i == 0)
                    {
                        sql += parms[i].ParameterName;
                    }
                    else
                    {
                        sql += ", " + parms[i].ParameterName;
                    }
                }
                return db.Database.SqlQuery<T>(sql, parms).ToList<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行存储过程无查询
        /// </summary>
        /// <param name="parms"></param>
        /// <param name="procName"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int ExecProcNoQuery(SqlParameter[] parms, string procName, DbContext db)
        {
            try
            {
                string sql = procName + " ";
                for (int i = 0; i < parms.Length; i++)
                {
                    if (i == 0)
                    {
                        sql += parms[i].ParameterName;
                    }
                    else
                    {
                        sql += ", " + parms[i].ParameterName;
                    }
                }
                return db.Database.ExecuteSqlCommand(sql, parms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// Author:王达国
        /// Time:2015.10.28
        /// Content:将集合类转换成DataTable  
        /// </summary>  
        /// <param name="list">集合</param>  
        /// <returns></returns>  
        public static System.Data.DataTable ListToDataTable(IList list)
        {
            System.Data.DataTable result = new System.Data.DataTable(typeof(T).Name);
            result = CreateData();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        /// <summary>
        /// 根据实体类得到表结构
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        private static System.Data.DataTable CreateData()
        {
            System.Data.DataTable dataTable = new System.Data.DataTable(typeof(T).Name);
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                dataTable.Columns.Add(new System.Data.DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
            }
            return dataTable;
        }

        /// <summary>
        /// 执行sql脚本
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int ExecSqlNoQuery(string sql, DbContext db)
        {
            try
            {
                return db.Database.ExecuteSqlCommand(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行sql脚本并查询返回List
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static List<T> ExecSqlQuery(string sql, DbContext db)
        {
            try
            {
                return db.Database.SqlQuery(typeof(T), sql).Cast<T>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region
        /*
        public static void Update(Expression<Func<T, bool>> where, Dictionary<string, object> dic, DbContext db)
        {
            IEnumerable<T> result = db.Set<T>().Where(where).ToList();
            Type type = typeof(T);
            List<PropertyInfo> propertyList = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).ToList();
            //遍历结果集
            foreach (T entity in result)
            {
                foreach (PropertyInfo propertyInfo in propertyList)
                {
                    string propertyName = propertyInfo.Name;
                    if (dic.ContainsKey(propertyName))
                    {
                        //设置值
                        propertyInfo.SetValue(entity, dic[propertyName], null);
                    }
                }
            }
            db.SaveChanges();
        }
         * */
        #endregion


    }
}
