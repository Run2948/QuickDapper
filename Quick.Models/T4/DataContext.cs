/* ==============================================================================
* 命名空间：Quick.Models 
* 类 名 称：DataContext
* 创 建 者：Qing
* 创建时间：2019/06/23 15:48:12
* CLR 版本：4.0.30319.42000
* 保存的文件名：DataContext
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

using Quick.Models.Entity.Table;
using System.Data.Entity;

namespace Quick.Models
{
    public partial class DataContext
    {
		public virtual DbSet<Product> Product { get; set; }

		public virtual DbSet<Student> Student { get; set; }

       
    }
}
