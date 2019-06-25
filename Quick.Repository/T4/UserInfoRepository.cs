/* ==============================================================================
* 命名空间：Quick.IRepository
* 类 名 称：UserInfoRepository
* 创 建 者：Qing
* 创建时间：2019/06/25 14:36:02
* CLR 版本：4.0.30319.42000
* 保存的文件名：UserInfoRepository
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
using Quick.Models.Entity.Table;

namespace Quick.Repository
{
	public partial class UserInfoRepository :BaseRepository<UserInfo>,IUserInfoRepository
	{ 

	}
}

