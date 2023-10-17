using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string? UserLastname { get; set; }

        public string? DateOfBirthText { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int TrainingLevel { get; set; }

    }
}
