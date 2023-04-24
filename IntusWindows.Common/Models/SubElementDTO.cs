using IntusWindows.Common.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IntusWindows.Common.Models
{
    public class SubElementDTO : BaseDTO
    {
        public int Element { get; set; }

        [Required(ErrorMessage = "Please enter width.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive width for this element.")]
        public int Width { get; set; }

        [Required(ErrorMessage = "Please enter height.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive height for this element.")]
        public int Height { get; set; }
    
        [Required(ErrorMessage = "Please select elemnt type.")]
        [Range(typeof(ElementType), "Windows", "Doors", ErrorMessage="Please select valid elemnt type.")]
        public ElementType ElementType { get; set; }

        [Required(ErrorMessage = "Please enter an elemnt window.")]
        public int WindowId { get; set; }
    }
}
