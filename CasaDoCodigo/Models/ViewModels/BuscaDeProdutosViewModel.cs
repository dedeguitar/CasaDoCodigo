using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    public class BuscaDeProdutosViewModel
    {
        public IEnumerable<IGrouping<string, Produto>> ListaProdutos { get; set; }

        public string Pesquisa { get; set; }
    }
}
