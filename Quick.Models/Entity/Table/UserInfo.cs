/* ==============================================================================
* 命名空间：Quick.Models.Entity.Table 
* 类 名 称：UserInfo
* 创 建 者：Qing
* 创建时间：2019/06/23 21:40:17
* CLR 版本：4.0.30319.42000
* 保存的文件名：UserInfo
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

using System.ComponentModel.DataAnnotations;

namespace Quick.Models.Entity.Table
{
    [DbDescription("用户表")]
    public class UserInfo : GenerateId<long>
    {
        [StringLength(32)]
        [DbDescription("登录账号")]
        public string UserName { get; set; }

        [StringLength(32)]
        [DbDescription("登录密码")]
        public string Password { get; set; }
    }
}
