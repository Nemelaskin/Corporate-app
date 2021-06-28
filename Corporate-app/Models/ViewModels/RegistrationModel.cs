using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate_app.Models.ViewModels
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Не верно указан Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не верно указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не соответсвуют")]
        public string ConfirmPassword { get; set; }
        
        [Required(ErrorMessage ="Пустое имя")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пустое имя")]
        [DataType(DataType.Text)]
        public string Surname { get; set; }

        public string Phone { get; set; }
    }
}
