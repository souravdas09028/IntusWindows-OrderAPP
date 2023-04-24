using IntusWindows.Common.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntusWindows.Common.Models
{
    public class OrderDTO : BaseDTO
    {
        [Required(ErrorMessage = "Please enter an order name.")]
        [MaxLength(30, ErrorMessage = "Please enter an order name with a maximum of 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select an state.")]
        public USState State { get; set; }

        public List<WindowDTO> Windows { get; set; } = new List<WindowDTO>();
    }
}
