/* ==============================================================================
* 命名空间：Quick.Models.Entity.Table 
* 类 名 称：Product
* 创 建 者：Qing
* 创建时间：2019/06/23 13:49:26
* CLR 版本：4.0.30319.42000
* 保存的文件名：Product
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

namespace Quick.Models.Entity.Table
{
    //[DbDescription("产品表")]
    public class Product : GenerateId<long>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
