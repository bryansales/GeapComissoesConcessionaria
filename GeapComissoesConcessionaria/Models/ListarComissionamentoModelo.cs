using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeapComissoesConcessionaria.Models
{
    public class ListarComissionamentoModelo
    {
        public string NomeVendedor { get; set; }
        public int IdVenda { get; set; }
        public decimal Valor { get; set; }
        public bool ValeCombustivel { get; set; }
        public Nullable<decimal> Comissionamento { get; set; }
        public int QtdVendas { get; set; }
    }
}