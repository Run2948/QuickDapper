/* ==============================================================================
* 命名空间：Quick.Models.Entity 
* 类 名 称：GenerateId
* 创 建 者：Qing
* 创建时间：2019/06/20 19:57:05
* CLR 版本：4.0.30319.42000
* 保存的文件名：GenerateId
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Models.Entity
{
    public class GenerateId<TKey> where TKey : struct
    {
        [DbDescription("自增主键")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }
    }
}
