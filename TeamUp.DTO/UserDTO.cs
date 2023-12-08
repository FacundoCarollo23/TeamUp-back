using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.DTO
{
    public class UserDTO
    {

        public int UserId { get; set; }

        [Required, MinLength(3), MaxLength(20), RegularExpression(@"^(^[a-zA-Z]+$)")]
        public string UserName { get; set; } = null!;

        [Required, MinLength(3), MaxLength(20), RegularExpression(@"^(^[a-zA-Z]+$)")]
        public string UserLastname { get; set; }

        //[Required, DataType(DataType.Date), Range(typeof(DateTime), "1/2/1980", DateTime.Today.AddYears(),
        //ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public string DateOfBirthText { get; set; }

        [EmailAddress]
        public string Email { get; set; } = null!;
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$",
        ErrorMessage = "La contraseña debe contener al menos una minúscula, una mayúscula, un número y un símbolo."), 
        StringLength(15, ErrorMessage = "La contraseña debe tener un mínimo de 8 caracteres y un máximo de 15", MinimumLength = 8)]
        public string Password { get; set; } = null!;

        public string? Alias { get; set; }

    }
}
