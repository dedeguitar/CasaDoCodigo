using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Task Save(Categoria categoria);

        Task Save(IEnumerable<Categoria> categorias);

        Categoria Get(string nome);
    }
}
