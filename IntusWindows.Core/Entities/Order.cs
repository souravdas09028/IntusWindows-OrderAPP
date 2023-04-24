using IntusWindows.Common;
using IntusWindows.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntusWindows.Core.Entities
{
    public class Order : BaseModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public USState State { get; set; }

        public IEnumerable<Window> Windows { get; set; } = new List<Window>();
    }
}
