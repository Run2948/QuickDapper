/* ==============================================================================
* 命名空间：Quick.Services.AutoMapper 
* 类 名 称：ServiceMappingProfile
* 创 建 者：Qing
* 创建时间：2019/06/23 18:05:44
* CLR 版本：4.0.30319.42000
* 保存的文件名：ServiceMappingProfile
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

namespace Quick.Services.AutoMapper
{
    public class ServiceMappingProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public ServiceMappingProfile()
        {
            // CreateMap<User, UserDto>().IgnoreAllNonExisting();
        }
    }
}
