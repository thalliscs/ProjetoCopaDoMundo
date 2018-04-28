using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fp_web_aula_1_core.Models
{
    public class Jogador
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Posicao { get; set; }

        public DateTime Nascimento { get; set; }

        public int Camisa { get; set; }

        public int TimeId { get; set; }

        public Time Time { get; set; }

        public void Atualizar(DateTime nascimento, string nome, string posicao, int timeId, int camisa)
        {
            Nascimento = nascimento;
            Nome = nome;
            Posicao = posicao;
            TimeId = timeId;
            Camisa = camisa;
        }
    }
}
