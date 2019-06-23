using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    public class DbDescriptionAttribute : Attribute
    {
        public string Description { get; set; }

        public DbDescriptionAttribute(string description)
        {
            this.Description = description;
        }

    }
}
