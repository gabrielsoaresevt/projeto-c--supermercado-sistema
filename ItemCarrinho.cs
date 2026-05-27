using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Supermercado
{
    public class ItemCarrinho
    {
        public int IdProduto { get; set; }
        public string Código { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PreçoUnitário { get; set; }
        public decimal TotalItem => Quantidade * PreçoUnitário; // Propriedade calculada automaticamente
    }
}
