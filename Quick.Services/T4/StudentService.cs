/* ==============================================================================
* 命名空间：Quick.Services
* 类 名 称：StudentService
* 创 建 者：Qing
* 创建时间：2019/06/23 15:48:12
* CLR 版本：4.0.30319.42000
* 保存的文件名：StudentService
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
	public partial class StudentService :BaseService<Student>,IStudentService
	{ 
		public virtual IStudentRepository StudentRepository { get; set; }
	}
}

