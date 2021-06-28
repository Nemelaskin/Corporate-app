using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate_app.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не верно указан Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не верно указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
