using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public Categoria Get(string nome)
        {
            return dbSet.FirstOrDefault(f => f.Nome.Equals(nome));
        }

        public async Task Save(Categoria categoria)
        {
            if (!dbSet.Where(c => c.Nome.Equals(categoria.Nome)).Any())
            {
                dbSet.Add(categoria);
                await this.contexto.SaveChangesAsync();
            }
        }

        public async Task Save(IEnumerable<Categoria> categorias)
        {
            foreach (var categoria in categorias)
            {
                await this.Save(categoria);
            }
        }
    }
}
