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
    public class SubElement
    {
        [Key]
        public int ID { get; set; }
        public int Element { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Width { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Height { get; set; }
        [Required]
        public int WindowId { get; set; }
        public Window Window { get; set; }
        public ElementType ElementType { get; set; }
    }
}
