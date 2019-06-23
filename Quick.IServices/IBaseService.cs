/* ==============================================================================
* 命名空间：Quick.IServices 
* 类 名 称：IBaseService
* 创 建 者：Qing
* 创建时间：2019/06/23 9:16:01
* CLR 版本：4.0.30319.42000
* 保存的文件名：IBaseService
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

namespace Quick.IServices
{
    public partial interface IBaseService<T> where T : class
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
        object Insert(T entity, IDbTransaction transaction = null, int? commandTimeout = null,
            string sql = null, object param = null);

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<object> InsertBatch(IEnumerable<T> entityList, IDbTransaction transaction = null,
            int? commandTimeout = null, string sql = null, object param = null);

        /// <summary>
        /// 更新单条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool Update(T entity, IDbTransaction transaction = null, int? commandTimeout = null);
        /// <summary>
        ///  批量更新
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool UpdateBatch(IEnumerable<T> entityList, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        ///  删除单条记录
        /// </summary>
        /// <param name="primaryId"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool DeleteById(object primaryId, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 根据条件删除实体集
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool Delete(object predicate, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool DeleteBatch(IEnumerable<object> ids, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 统计记录总数
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        int Count(object predicate, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 获取全部数据集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Get(object predicate = null, IList<ISort> sort = null, IDbTransaction transaction = null,
            int? commandTimeout = null, bool buffered = false);

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        T GetById(object id, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 根据sql语句获取第一个实体
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        T FirstOrDefault(string sql, object param);

        /// <summary>
        /// 根据多个Id获取多个实体
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        IEnumerable<T> GetByIds(IList<object> ids, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null, bool buffered = false);

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
        IEnumerable<T> Get(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, bool buffered = false);

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
        IEnumerable<object> Get(string sql, object param = null, bool buffered = false,
            IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);

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
        IEnumerable<TReturn> Get<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map,
            object param = null, IDbTransaction transaction = null, bool buffered = false, string splitOn = "Id",
            int? commandTimeout = null, CommandType? commandType = null);

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
        IEnumerable<TReturn> Get<TFirst, TSecond, TThird, TReturn>(string sql,
            Func<TFirst, TSecond, TThird, TReturn> map,
            object param = null, IDbTransaction transaction = null, bool buffered = false, string splitOn = "Id",
            int? commandTimeout = null, CommandType? commandType = null);

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
        IEnumerable<T> GetPageList(int pageIndex, int pageSize, out long allRowsCount,
            object predicate = null, IList<ISort> sort = null, IDbTransaction transaction = null, int? commandTimeout = null, bool buffered = false);

        /// <summary>
        /// 执行sql操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 获取多实体集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        SqlMapper.GridReader GetMultiple(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 根据sql语句获取2个结果集
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Tuple<IEnumerable<T1>, IEnumerable<T2>> Query<T1, T2>(string sql, object param);

        /// <summary>
        /// 根据sql语句获取3个结果集
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> Query<T1, T2, T3>(string sql, object param);

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
        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> Query<T1, T2, T3, T4>(
            string sql, object param);

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
        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> Query<T1, T2,
            T3, T4, T5>(string sql, object param);

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
        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>,
            IEnumerable<T6>> Query<T1, T2, T3, T4, T5, T6>(string sql, object param);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        TEntity Get<TEntity>(object id, string colName);

    }
}
