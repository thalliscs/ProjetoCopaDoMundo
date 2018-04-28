using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fp_web_aula_1.ViewModel
{
    public class TimeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Site { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Bandeira { get; set; }
    }
}
