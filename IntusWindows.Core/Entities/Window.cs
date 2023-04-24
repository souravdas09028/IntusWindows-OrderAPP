using IntusWindows.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntusWindows.Core.Entities
{
    public class Window : BaseModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; }
        public IEnumerable<SubElement> SubElements { get; set; } = new List<SubElement>();
    }
}
