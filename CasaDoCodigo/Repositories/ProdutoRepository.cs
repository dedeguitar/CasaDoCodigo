using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private ICategoriaRepository CategoriaRepository { get; set; }

        public ProdutoRepository(ApplicationContext contexto, ICategoriaRepository categoriaRepository) : base(contexto)
        {
            this.CategoriaRepository = categoriaRepository;
        }

        public async Task<IList<Produto>> GetProdutos()
        {
            return await dbSet.Include(p => p.Categoria).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> GetProdutos(string filtro)
        {
            if (string.IsNullOrEmpty(filtro))
            {
                return await this.GetProdutos();
            }
            else
            {
                return await dbSet
                    .Include(p => p.Categoria)
                    .Where(w => w.Nome.Contains(filtro) || w.Categoria.Nome.Contains(filtro))
                    .ToListAsync();
            }
        }

        public async Task SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                Categoria categoria = this.CategoriaRepository.Get(livro.Categoria);

                Produto produto = dbSet.Where(p => p.Codigo == livro.Codigo).FirstOrDefault();
                if (produto == null)
                {
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria));
                }
                else
                {
                    if (string.IsNullOrEmpty(produto.Categoria?.Nome) && !string.IsNullOrEmpty(livro.Categoria))
                    {
                        produto.SetCategoria(categoria);
                        dbSet.Update(produto);
                    }
                }
            }
            await contexto.SaveChangesAsync();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
