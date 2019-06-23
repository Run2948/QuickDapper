/* ==============================================================================
* 命名空间：Quick.Repository 
* 类 名 称：BaseRepository
* 创 建 者：Qing
* 创建时间：2019/06/22 13:50:19
* CLR 版本：4.0.30319.42000
* 保存的文件名：BaseRepository
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/

using Dapper;
using DapperExtensions;
using Quick.IRepository;
using Quick.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Quick.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        /// <summary>
        /// 插入单条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual object Insert(T entity, IDbTransaction transaction = null, int? commandTimeout = null, string sql = null,
            object param = null)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                if (!string.IsNullOrEmpty(sql))
                {
                    conn.Execute(sql, param);
                }
                return conn.Insert<T>(entity, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual IEnumerable<object> InsertBatch(IEnumerable<T> entityList, IDbTransaction transaction = null, int? commandTimeout = null,
            string sql = null, object param = null)
        {
            IEnumerable<object> list = new List<object>();
            foreach (var entity in entityList)
            {
                list.Append(Insert(entity, transaction, commandTimeout));
            }
            return list;
        }

        /// <summary>
        /// 更新单条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual bool Update(T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.Update<T>(entity, transaction, commandTimeout);
            }
        }

        /// <summary>
        ///  批量更新
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual bool UpdateBatch(IEnumerable<T> entityList, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            bool all = false;
            foreach (var entity in entityList)
            {
                all = all || Update(entity, transaction, commandTimeout);
            }
            return all;
        }

        /// <summary>
        ///  删除单条记录
        /// </summary>
        /// <param name="primaryId"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual bool DeleteById(object primaryId, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                var entity = GetById(primaryId);
                return conn.Delete(entity, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// 根据条件删除实体集
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual bool Delete(object predicate, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.Delete<T>(predicate, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual bool DeleteBatch(IEnumerable<object> ids, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            bool all = false;
            foreach (var id in ids)
            {
                all = all || DeleteById(id, transaction, commandTimeout);
            }
            return all;
        }

        /// <summary>
        /// 统计记录总数
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual int Count(object predicate, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.Count<T>(predicate, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// 获取全部数据集合
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> Get(object predicate = null, IList<ISort> sort = null, IDbTransaction transaction = null, int? commandTimeout = null, bool buffered = false)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.GetList<T>(predicate, sort, transaction, commandTimeout, buffered);
            }
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual T GetById(object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.Get<T>(id, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// 根据sql语句获取第一个实体
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual T FirstOrDefault(string sql, object param)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.Query<T>(sql, param).FirstOrDefault();
            }
        }

        /// <summary>
        /// 根据多个Id获取多个实体
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetByIds(IList<object> ids, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, bool buffered = false)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                var sql = $"select * from @table where Id in @ids";
                return conn.Query<T>(sql, new { typeof(T).Name, ids }, transaction, buffered, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 根据条件筛选出数据集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> Get(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null, bool buffered = false)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 根据条件筛选出动态数据集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual IEnumerable<object> Get(string sql, object param = null, bool buffered = false, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.Query(sql, param, transaction, buffered, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 根据表达式筛选
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual IEnumerable<TReturn> Get<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null,
            bool buffered = false, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.Query(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 根据表达式筛选
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual IEnumerable<TReturn> Get<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null,
            IDbTransaction transaction = null, bool buffered = false, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.Query(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="allRowsCount"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetPageList(int pageIndex, int pageSize, out long allRowsCount, object predicate = null, IList<ISort> sort = null,
            IDbTransaction transaction = null, int? commandTimeout = null, bool buffered = false)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                if (sort == null) sort = new List<ISort>();
                allRowsCount = conn.Count<T>(predicate);
                return conn.GetPage<T>(predicate, sort, pageIndex, pageSize, transaction, commandTimeout, buffered);
            }
        }

        /// <summary>
        /// 执行sql操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.Execute(sql, param, transaction, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 获取多实体集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual SqlMapper.GridReader GetMultiple(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 根据sql语句获取2个结果集
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual Tuple<IEnumerable<T1>, IEnumerable<T2>> Query<T1, T2>(string sql, object param)
        {
            IEnumerable<T1> item1 = null; IEnumerable<T2> item2 = null;
            if (!string.IsNullOrEmpty(sql))
            {
                using (var conn = ConnectionFactory.GetConnection())
                {
                    using (var multi = conn.QueryMultiple(sql, param))
                    {
                        item1 = multi.Read<T1>();
                        item2 = multi.Read<T2>();
                    }
                }
            }
            return Tuple.Create(item1, item2);
        }

        /// <summary>
        /// 根据sql语句获取3个结果集
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> Query<T1, T2, T3>(string sql, object param)
        {
            IEnumerable<T1> item1 = null; IEnumerable<T2> item2 = null; IEnumerable<T3> item3 = null;
            if (!string.IsNullOrEmpty(sql))
            {
                using (var conn = ConnectionFactory.GetConnection())
                {
                    using (var multi = conn.QueryMultiple(sql, param))
                    {
                        item1 = multi.Read<T1>();
                        item2 = multi.Read<T2>();
                        item3 = multi.Read<T3>();
                    }
                }
            }
            return Tuple.Create(item1, item2, item3);
        }

        /// <summary>
        /// 根据sql语句获取4个结果集
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> Query<T1, T2, T3, T4>(string sql, object param)
        {
            IEnumerable<T1> item1 = null; IEnumerable<T2> item2 = null; IEnumerable<T3> item3 = null; IEnumerable<T4> item4 = null;
            if (!string.IsNullOrEmpty(sql))
            {
                using (var conn = ConnectionFactory.GetConnection())
                {
                    using (var multi = conn.QueryMultiple(sql, param))
                    {
                        item1 = multi.Read<T1>();
                        item2 = multi.Read<T2>();
                        item3 = multi.Read<T3>();
                        item4 = multi.Read<T4>();
                    }
                }
            }
            return Tuple.Create(item1, item2, item3, item4);
        }

        /// <summary>
        /// 根据sql语句获取5个结果集
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> Query<T1, T2, T3, T4, T5>(string sql, object param)
        {
            IEnumerable<T1> item1 = null; IEnumerable<T2> item2 = null; IEnumerable<T3> item3 = null; IEnumerable<T4> item4 = null; IEnumerable<T5> item5 = null;
            if (!string.IsNullOrEmpty(sql))
            {
                using (var conn = ConnectionFactory.GetConnection())
                {
                    using (var multi = conn.QueryMultiple(sql, param))
                    {
                        item1 = multi.Read<T1>();
                        item2 = multi.Read<T2>();
                        item3 = multi.Read<T3>();
                        item4 = multi.Read<T4>();
                        item5 = multi.Read<T5>();
                    }
                }
            }
            return Tuple.Create(item1, item2, item3, item4, item5);
        }

        /// <summary>
        /// 根据sql语句获取6个结果集
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> Query<T1, T2, T3, T4, T5, T6>(string sql, object param)
        {
            IEnumerable<T1> item1 = null; IEnumerable<T2> item2 = null; IEnumerable<T3> item3 = null; IEnumerable<T4> item4 = null; IEnumerable<T5> item5 = null; IEnumerable<T6> item6 = null;
            if (!string.IsNullOrEmpty(sql))
            {
                using (var conn = ConnectionFactory.GetConnection())
                {
                    using (var multi = conn.QueryMultiple(sql, param))
                    {
                        item1 = multi.Read<T1>();
                        item2 = multi.Read<T2>();
                        item3 = multi.Read<T3>();
                        item4 = multi.Read<T4>();
                        item5 = multi.Read<T5>();
                        item6 = multi.Read<T6>();
                    }
                }
            }
            return Tuple.Create(item1, item2, item3, item4, item5, item6);
        }

        /// <summary>
        /// 根据Id获取字段值
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public virtual TEntity Get<TEntity>(object id, string colName)
        {
            var sql = $"select {colName} from {typeof(T).Name} where Id = @id";
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.Query<TEntity>(sql,  new{ id } ).SingleOrDefault();
            }
        }
    }
}
