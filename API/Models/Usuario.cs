using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_avaliacao.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;
    }
}