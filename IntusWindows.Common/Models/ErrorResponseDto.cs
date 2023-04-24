using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntusWindows.Common.Models
{
    public class ErrorResponseDto
    {
        public ErrorResponseDto(string error)
        {
            Error = error;
        }
        public string Error { get; set; }
    }
}
