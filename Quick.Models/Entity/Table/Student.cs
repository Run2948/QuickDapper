using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quick.Models.Entity.Table
{
    [DbDescription("学生表")]
    [Table(nameof(Student))]
    public class Student : GenerateId<int>
    {
        [DbDescription("姓名")]
        public string Name { get; set; }

        [DbDescription("年纪")]
        public int Age { get; set; }
    }
}
