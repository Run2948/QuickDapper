/* ==============================================================================
* 命名空间：Quick.Services 
* 类 名 称：BaseService
* 创 建 者：Qing
* 创建时间：2019/06/23 9:17:27
* CLR 版本：4.0.30319.42000
* 保存的文件名：BaseService
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using Quick.IRepository;
using Quick.IServices;

namespace Quick.Services
{
    public partial class BaseService<T> : IBaseService<T> where T : class
    {
        public virtual IBaseRepository<T> BaseRepository { get; set; }

        /// <summary>
        /// 插入单条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public object Insert(T entity, IDbTransaction transaction = null, int? commandTimeout = null, string sql = null,
            object param = null)
        {
            return BaseRepository.Insert(entity, transaction, commandTimeout, sql, param);
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
        public IEnumerable<object> InsertBatch(IEnumerable<T> entityList, IDbTransaction transaction = null, int? commandTimeout = null,
            string sql = null, object param = null)
        {
            return BaseRepository.InsertBatch(entityList, transaction, commandTimeout, sql, param);
        }

        /// <summary>
        /// 更新单条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool Update(T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return BaseRepository.Update(entity, transaction, commandTimeout);
        }

        /// <summary>
        ///  批量更新
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool UpdateBatch(IEnumerable<T> entityList, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return BaseRepository.UpdateBatch(entityList, transaction, commandTimeout);
        }

        /// <summary>
        ///  删除单条记录
        /// </summary>
        /// <param name="primaryId"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool DeleteById(object primaryId, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return BaseRepository.DeleteById(primaryId, transaction, commandTimeout);
        }

        /// <summary>
        /// 根据条件删除实体集
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool Delete(object predicate, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return BaseRepository.Delete(predicate, transaction, commandTimeout);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool DeleteBatch(IEnumerable<object> ids, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return BaseRepository.DeleteBatch(ids, transaction, commandTimeout);
        }

        /// <summary>
        /// 统计记录总数
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public int Count(object predicate, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return BaseRepository.Count(predicate, transaction, commandTimeout);
        }

        /// <summary>
        /// 获取全部数据集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Get(object predicate = null, IList<ISort> sort = null, IDbTransaction transaction = null,
            int? commandTimeout = null, bool buffered = false)
        {
            return BaseRepository.Get(predicate, sort, transaction, commandTimeout, buffered);
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public T GetById(object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return BaseRepository.GetById(id, transaction, commandTimeout);
        }

        /// <summary>
        /// 根据sql语句获取第一个实体
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public T FirstOrDefault(string sql, object param)
        {
            return BaseRepository.FirstOrDefault(sql, param);
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
        public IEnumerable<T> GetByIds(IList<object> ids, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null, bool buffered = false)
        {
            return BaseRepository.GetByIds(ids, transaction, commandTimeout, commandType, buffered);
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
        public IEnumerable<T> Get(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null, bool buffered = false)
        {
            return BaseRepository.Get(sql,param,transaction,commandTimeout,commandType);
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
        public IEnumerable<object> Get(string sql, object param = null, bool buffered = false, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            return BaseRepository.Get(sql, param, buffered, transaction, commandTimeout, commandType);
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
        public IEnumerable<TReturn> Get<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null,
            bool buffered = false, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return BaseRepository.Get<TFirst, TSecond, TReturn>(sql, map, param, transaction, buffered, splitOn,
                commandTimeout, commandType);
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
        public IEnumerable<TReturn> Get<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null,
            IDbTransaction transaction = null, bool buffered = false, string splitOn = "Id", int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return BaseRepository.Get<TFirst, TSecond, TThird, TReturn>(sql, map, param, transaction, buffered, splitOn,
                commandTimeout, commandType);
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
        public IEnumerable<T> GetPageList(int pageIndex, int pageSize, out long allRowsCount, object predicate = null, IList<ISort> sort = null,
            IDbTransaction transaction = null, int? commandTimeout = null, bool buffered = false)
        {
            return BaseRepository.GetPageList(pageIndex,pageSize,out allRowsCount,predicate,sort,transaction,commandTimeout,buffered);
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
        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return BaseRepository.Execute(sql,param,transaction,commandTimeout,commandType);
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
        public SqlMapper.GridReader GetMultiple(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return BaseRepository.GetMultiple(sql, param,transaction,commandTimeout,commandType);
        }

        /// <summary>
        /// 根据sql语句获取2个结果集
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Tuple<IEnumerable<T1>, IEnumerable<T2>> Query<T1, T2>(string sql, object param)
        {
            return BaseRepository.Query<T1, T2>(sql, param);
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
        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> Query<T1, T2, T3>(string sql, object param)
        {
            return BaseRepository.Query<T1, T2, T3>(sql, param);
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
        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> Query<T1, T2, T3, T4>(string sql, object param)
        {
            return BaseRepository.Query<T1, T2, T3, T4>(sql, param);
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
        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> Query<T1, T2, T3, T4, T5>(string sql, object param)
        {
            return BaseRepository.Query<T1, T2, T3, T4, T5>(sql, param);
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
        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> Query<T1, T2, T3, T4, T5, T6>(string sql, object param)
        {
            return BaseRepository.Query<T1, T2, T3, T4, T5, T6>(sql, param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public TEntity Get<TEntity>(object id, string colName)
        {
            return BaseRepository.Get<TEntity>(id, colName);
        }
    }
}
