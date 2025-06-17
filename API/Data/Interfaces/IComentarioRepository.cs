using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_avaliacao.Models;

namespace api_avaliacao.Interfaces;
    public interface IComentarioRepository
    {
        Task<List<Comentario>> ListarPorItemAsync(int itemId);
        Task<Comentario> CriarAsync(Comentario comentario);
        Task<bool> ExcluirAsync(int comentarioId);
    }
