﻿using Quick.Common.Logging;
using Quick.Common.NoSQL;
using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.SessionState;

namespace Quick.Common.Net
{
    /// <summary>
    /// Web操作扩展
    /// </summary>
    public static class WebExtension
    {
        #region 获取线程内唯一的EF上下文对象

        /// <summary>
        /// 获取线程内唯一的EF上下文对象
        /// </summary>
        /// <typeparam name="T">EF上下文容器对象</typeparam>
        /// <returns>EF上下文容器对象</returns>
        public static T GetDbContext<T>() where T : new()
        {
            T db;
            if (CallContext.GetData("db") == null) //由于CallContext比HttpContext先存在，所以首选CallContext为线程内唯一对象
            {
                db = new T();
                CallContext.SetData("db", db);
            }
            db = (T)CallContext.GetData("db");
            return db;
        }

        #endregion

        #region 写入Session

        /// <summary>
        /// 写Session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void Set(this HttpSessionStateBase session, string key, dynamic value) => session[key] = value;

        /// <summary>
        /// 写Session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="timeout">过期时间(分钟)</param>
        public static void Set(this HttpSessionStateBase session, string key, dynamic value, int timeout)
        {
            session.Timeout = timeout;
            session[key] = value;
        }

        /// <summary>
        /// 写Session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void Set(this HttpSessionState session, string key, dynamic value) => session[key] = value;

        /// <summary>
        /// 写Session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="timeout">过期时间(分钟)</param>
        public static void Set(this HttpSessionState session, string key, dynamic value, int timeout)
        {
            session.Timeout = timeout;
            session[key] = value;
        }

        /// <summary>
        /// 将Session存到Redis，需要先在config中配置链接字符串，连接字符串在config配置文件中的ConnectionStrings节下配置，name固定为RedisHosts，值的格式：127.0.0.1:6379,allowadmin=true，若未正确配置，将按默认值“127.0.0.1:6379,allowadmin=true”进行操作，如：<br/>
        /// &lt;connectionStrings&gt;<br/>
        ///      &lt;add name = "RedisHosts" connectionString="127.0.0.1:6379,allowadmin=true"/&gt;<br/>
        /// &lt;/connectionStrings&gt;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key">键</param>
        /// <param name="obj">需要存的对象</param>
        /// <param name="db">Redis数据库编号</param>
        /// <param name="expire">过期时间，默认20分钟</param>
        /// <returns></returns>
        public static void SetByRedis<T>(this HttpSessionState session, string key, T obj, int db = 1, int expire = 20)
        {
            if (HttpContext.Current is null)
            {
                throw new Exception("请确保此方法调用是在同步线程中执行！");
            }
            var sessionKey = HttpContext.Current.Request.Cookies["SessionID"]?.Value;
            if (string.IsNullOrEmpty(sessionKey))
            {
                sessionKey = Guid.NewGuid().ToString("N");
                HttpCookie cookie = new HttpCookie("SessionID", sessionKey);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }

            if (session != null)
            {
                session[key] = obj;
            }
            try
            {
                using (RedisHelper redisHelper = RedisHelper.GetInstance(db))
                {
                    redisHelper.SetHash("Session:" + sessionKey, key, obj, TimeSpan.FromMinutes(expire)); //存储数据到缓存服务器，这里将字符串"my value"缓存，key 是"test"
                }
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// 将Session存到Redis，需要先在config中配置链接字符串，连接字符串在config配置文件中的ConnectionStrings节下配置，name固定为RedisHosts，值的格式：127.0.0.1:6379,allowadmin=true，若未正确配置，将按默认值“127.0.0.1:6379,allowadmin=true”进行操作，如：<br/>
        /// &lt;connectionStrings&gt;<br/>
        ///      &lt;add name = "RedisHosts" connectionString="127.0.0.1:6379,allowadmin=true"/&gt;<br/>
        /// &lt;/connectionStrings&gt;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key">键</param>
        /// <param name="obj">需要存的对象</param>
        /// <param name="db">Redis数据库编号</param>
        /// <param name="expire">过期时间，默认20分钟</param>
        /// <returns></returns> 
        public static void SetByRedis<T>(this HttpSessionStateBase session, string key, T obj, int db = 1, int expire = 20) where T : class
        {
            if (HttpContext.Current is null)
            {
                throw new Exception("请确保此方法调用是在同步线程中执行！");
            }
            var sessionKey = HttpContext.Current.Request.Cookies["SessionID"]?.Value;
            if (string.IsNullOrEmpty(sessionKey))
            {
                sessionKey = Guid.NewGuid().ToString("N");
                HttpCookie cookie = new HttpCookie("SessionID", sessionKey);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            if (session != null)
            {
                session[key] = obj;
            }

            try
            {
                using (RedisHelper redisHelper = RedisHelper.GetInstance(db))
                {
                    redisHelper.SetHash("Session:" + sessionKey, key, obj, TimeSpan.FromMinutes(expire)); //存储数据到缓存服务器，这里将字符串"my value"缓存，key 是"test"
                }
            }
            catch
            {
                // ignored
            }
        }

        #endregion

        #region 获取Session

        /// <summary>
        /// 获取Session
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="session"></param>
        /// <param name="key">键</param>
        /// <returns>对象</returns>
        public static T Get<T>(this HttpSessionStateBase session, string key) => (T)session[key];

        /// <summary>
        /// 获取Session
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="session"></param>
        /// <param name="key">键</param>
        /// <returns>对象</returns>
        public static T Get<T>(this HttpSessionState session, string key) => (T)session[key];

        /// <summary>
        /// 从Redis取Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_"></param>
        /// <param name="key">键</param>
        /// <param name="db">Redis数据库编号</param>
        /// <param name="expire">过期时间，默认20分钟</param>
        /// <returns></returns> 
        public static T GetByRedis<T>(this HttpSessionState _, string key, int db = 1, int expire = 20) where T : class
        {
            if (HttpContext.Current is null)
            {
                throw new Exception("请确保此方法调用是在同步线程中执行！");
            }

            var sessionKey = HttpContext.Current.Request.Cookies["SessionID"]?.Value;
            if (!string.IsNullOrEmpty(sessionKey))
            {
                T obj = default(T);
                if (_ != default(T))
                {
                    obj = _.Get<T>(key);
                }

                if (obj == default(T))
                {
                    try
                    {
                        sessionKey = "Session:" + sessionKey;
                        using (RedisHelper redisHelper = RedisHelper.GetInstance(db))
                        {
                            if (redisHelper.KeyExists(sessionKey) && redisHelper.HashExists(sessionKey, key))
                            {
                                redisHelper.Expire(sessionKey, TimeSpan.FromMinutes(expire));
                                return redisHelper.GetHash<T>(sessionKey, key);
                            }
                            return default(T);
                        }
                    }
                    catch
                    {
                        return default(T);
                    }
                }
                return obj;
            }
            return default(T);
        }

        /// <summary>
        /// 从Redis取Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_"></param>
        /// <param name="key">键</param>
        /// <param name="db">Redis数据库编号</param>
        /// <param name="expire">过期时间，默认20分钟</param>
        /// <returns></returns>
        public static T GetByRedis<T>(this HttpSessionStateBase _, string key, int db = 1, int expire = 20) where T : class
        {
            if (HttpContext.Current is null)
            {
                throw new Exception("请确保此方法调用是在同步线程中执行！");
            }

            var sessionKey = HttpContext.Current.Request.Cookies["SessionID"]?.Value;
            if (!string.IsNullOrEmpty(sessionKey))
            {
                T obj = default(T);
                if (_ != default(T))
                {
                    obj = _.Get<T>(key);
                }

                if (obj == null)
                {
                    try
                    {
                        sessionKey = "Session:" + sessionKey;
                        using (RedisHelper redisHelper = RedisHelper.GetInstance(db))
                        {
                            if (redisHelper.KeyExists(sessionKey) && redisHelper.HashExists(sessionKey, key))
                            {
                                redisHelper.Expire(sessionKey, TimeSpan.FromMinutes(expire));
                                return redisHelper.GetHash<T>(sessionKey, key);
                            }
                            return default(T);
                        }
                    }
                    catch
                    {
                        return default(T);
                    }
                }
                return obj;
            }
            return default(T);
        }

        /// <summary>
        /// 从Session移除对应键
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        public static void Remove(this HttpSessionStateBase session, string key) => session.Remove(key);

        /// <summary>
        /// 从Session移除对应键
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        public static void Remove(this HttpSessionState session, string key) => session.Remove(key);

        /// <summary>
        /// 从Redis移除对应键的Session
        /// </summary>
        /// <param name="_"></param>
        /// <param name="key"></param>
        /// <param name="db">Redis数据库编号</param>
        /// <returns></returns>
        public static void RemoveByRedis(this HttpSessionStateBase _, string key, int db = 1)
        {
            if (HttpContext.Current is null)
            {
                throw new Exception("请确保此方法调用是在同步线程中执行！");
            }
            var sessionKey = HttpContext.Current.Request.Cookies["SessionID"]?.Value;
            if (!string.IsNullOrEmpty(sessionKey))
            {
                if (_ != null)
                {
                    _[key] = null;
                }

                try
                {
                    sessionKey = "Session:" + sessionKey;
                    using (RedisHelper redisHelper = RedisHelper.GetInstance(db))
                    {
                        if (redisHelper.KeyExists(sessionKey) && redisHelper.HashExists(sessionKey, key))
                        {
                            redisHelper.DeleteHash(sessionKey, key);
                        }
                    }
                }
                catch (Exception e)
                {
                    LogManager.Error(e);
                }
            }
        }

        /// <summary>
        /// 从Redis移除对应键的Session
        /// </summary>
        /// <param name="_"></param>
        /// <param name="key"></param>
        /// <param name="db">Redis数据库编号</param>
        /// <returns></returns>
        public static void RemoveByRedis(this HttpSessionState _, string key, int db = 1)
        {
            if (HttpContext.Current is null)
            {
                throw new Exception("请确保此方法调用是在同步线程中执行！");
            }
            var sessionKey = HttpContext.Current.Request.Cookies["SessionID"]?.Value;
            if (!string.IsNullOrEmpty(sessionKey))
            {
                if (_ != null)
                {
                    _[key] = null;
                }

                try
                {
                    sessionKey = "Session:" + sessionKey;
                    using (RedisHelper redisHelper = RedisHelper.GetInstance(db))
                    {
                        if (redisHelper.KeyExists(sessionKey) && redisHelper.HashExists(sessionKey, key))
                        {
                            redisHelper.DeleteHash(sessionKey, key);
                        }
                    }
                }
                catch (Exception e)
                {
                    LogManager.Error(e);
                }
            }
        }

        /// <summary>
        /// Session个数
        /// </summary>
        /// <param name="session"></param>
        /// <param name="db">Redis数据库编号</param>
        /// <returns></returns>
        public static int SessionCount(this HttpSessionState session, int db = 1)
        {
            try
            {
                using (RedisHelper redisHelper = RedisHelper.GetInstance(db))
                {
                    return redisHelper.GetServer().Keys(1, "Session:*").Count();
                }
            }
            catch (Exception e)
            {
                LogManager.Error(e);
                return 0;
            }
        }
        /// <summary>
        /// Session个数
        /// </summary>
        /// <param name="session"></param>
        /// <param name="db">Redis数据库编号</param>
        /// <returns></returns>
        public static int SessionCount(this HttpSessionStateBase session, int db = 1)
        {
            try
            {
                using (RedisHelper redisHelper = RedisHelper.GetInstance(db))
                {
                    return redisHelper.GetServer().Keys(1, "Session:*").Count();
                }
            }
            catch (Exception e)
            {
                LogManager.Error(e);
                return 0;
            }
        }

        #endregion
    }
}