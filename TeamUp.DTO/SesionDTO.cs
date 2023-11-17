using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.DTO
{
    public class SesionDTO
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string? UserLastname { get; set; }

        public string? Token {  get; set; }
    }
}
