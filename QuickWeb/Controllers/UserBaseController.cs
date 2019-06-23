/* ==============================================================================
* 命名空间：Quick.Common.Mvc.Controllers 
* 类 名 称：UserBaseController
* 创 建 者：Qing
* 创建时间：2019/06/23 21:37:38
* CLR 版本：4.0.30319.42000
* 保存的文件名：UserBaseController
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

using Quick.Common;
using Quick.Common.Mapper;
using Quick.Common.Mvc.Controllers;
using Quick.Common.Net;
using Quick.Models.Entity.Table;
using QuickWeb.Filters;
using QuickWeb.ViewModels;

namespace QuickWeb.Controllers
{
    [QuickPermission]
    public class UserBaseController : BaseController
    {
        #region 用户Session相关操作

        protected bool IsUserLogin()
        {
            return GetUserSession() != null;
        }

        protected UserInfoOutputDto GetUserSession()
        {
            if (IsDebug)
            {
                UserInfoOutputDto dto = new UserInfoOutputDto() { Id = 2 };
                System.Web.HttpContext.Current.Session.Set(QuickKeys.UserSession, dto, 60 * 12);
                return dto;
            }
            else
                return System.Web.HttpContext.Current.Session.Get<UserInfoOutputDto>(QuickKeys.UserSession);
        }

        protected void SetUserSession(UserInfo user, int timeout = 20)
        {
            UserInfoOutputDto dto = user.Mapper<UserInfoOutputDto>();
            System.Web.HttpContext.Current.Session.Set(QuickKeys.UserSession, dto, timeout);
        }

        protected void SetUserLogOut()
        {
            System.Web.HttpContext.Current.Session.Remove(QuickKeys.UserSession);
            System.Web.HttpContext.Current.Session.Abandon();
        }

        #endregion   
    }
}
