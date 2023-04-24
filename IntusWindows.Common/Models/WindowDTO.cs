using IntusWindows.Common.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntusWindows.Common.Models
{
    public class WindowDTO : BaseDTO
    {
        [Required(ErrorMessage = "Please enter an window name.")]
        [MaxLength(30, ErrorMessage = "Please enter an window name with a maximum of 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter an window quantity.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive quantity for the window.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please enter an window order.")]
        public int OrderId { get; set; }

        public int? NumberOfSubElements { get
            { return SubElements.Count(); } set { } }
        public List<SubElementDTO> SubElements { get; set; } = new List<SubElementDTO>();
    }
}
