using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fp_web_aula_1_core.Models
{

    public class Time
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }

        public string Site { get; set; }

        public string Bandeira { get; set; }

        public void Atualizar(string bandeira, string nome, string site)
        {
            this.Bandeira = bandeira;
            this.Nome = nome;
            this.Site = site;
        }
    }
}
