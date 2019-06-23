using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = Path.GetDirectoryName(@"‪C:\Users\Qing\Desktop\新建文本文档.txt");
            Console.WriteLine(Path.GetDirectoryName(dir));
            Console.ReadKey();
        }
    }
}
