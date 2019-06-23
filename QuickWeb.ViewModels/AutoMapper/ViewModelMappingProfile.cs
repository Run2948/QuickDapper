/* ==============================================================================
* 命名空间：QuickWeb.ViewModels.AutoMapper
* 类 名 称：ViewModelMappingProfile
* 创 建 者：Qing
* 创建时间：2019/06/23 17:32:11
* CLR 版本：4.0.30319.42000
* 保存的文件名：ViewModelMappingProfile
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

using AutoMapper;
using Quick.Common.Mapper;
using Quick.Models.Entity.Table;

namespace QuickWeb.ViewModels.AutoMapper
{
    public class ViewModelMappingProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public ViewModelMappingProfile()
        {
             CreateMap<Student, StudentViewModel>().IgnoreAllNonExisting();
             CreateMap<UserInfo, UserInfoOutputDto>().IgnoreAllNonExisting();
        }
    }
}
