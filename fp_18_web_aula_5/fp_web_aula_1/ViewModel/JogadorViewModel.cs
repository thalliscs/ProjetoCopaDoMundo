using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fp_web_aula_1.ViewModel
{
    public class JogadorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = " Campo obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = " Campo obrigatório")]
        public string Posicao { get; set; }

        [Required(ErrorMessage = " Campo obrigatório")]
        public DateTime? Nascimento { get; set; }

        [Required(ErrorMessage = " Campo obrigatório")]
        public int? Camisa { get; set; }

        [Required(ErrorMessage = " Campo obrigatório")]
        public int TimeId { get; set; }

        public List<TimeViewModel> Times { get; set; }

        public string NomeTime { get; set; }

        public JogadorViewModel()
        {
            this.Times = new List<TimeViewModel>();
        }
    }
}
