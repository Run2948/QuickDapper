/* ==============================================================================
* 命名空间：Quick.Services
* 类 名 称：UserInfoService
* 创 建 者：Qing
* 创建时间：2019/06/25 14:36:02
* CLR 版本：4.0.30319.42000
* 保存的文件名：UserInfoService
*
* 修改历史：
*
*
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
* ==============================================================================
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/

using Quick.IRepository;
using Quick.IServices;
using Quick.Models.Entity.Table;

namespace Quick.Services
{
	public partial class UserInfoService :BaseService<UserInfo>,IUserInfoService
	{ 
		public virtual IUserInfoRepository UserInfoRepository { get; set; }
	}
}

