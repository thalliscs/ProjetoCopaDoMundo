using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fp_web_aula_1.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Usuário*")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string UserName { get; set; }

        [Display(Name = "Senha*")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Password { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool IsPersistent { get; set; }
    }
}
