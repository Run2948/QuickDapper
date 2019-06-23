/* ==============================================================================
* 命名空间：Quick.Repository.AutoMapper 
* 类 名 称：RepositoryMappingProfile
* 创 建 者：Qing
* 创建时间：2019/06/23 18:03:23
* CLR 版本：4.0.30319.42000
* 保存的文件名：RepositoryMappingProfile
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Quick.Repository.AutoMapper
{
    public class RepositoryMappingProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public RepositoryMappingProfile()
        {
            // CreateMap<User, UserDto>().IgnoreAllNonExisting();
        }
    }
}
