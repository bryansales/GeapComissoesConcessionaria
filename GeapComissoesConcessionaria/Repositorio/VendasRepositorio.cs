using DAL;
using GeapComissoesConcessionaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeapComissoesConcessionaria.Repositorio
{
    public class VendasRepositorio
    {
        #region ListarComissoesVendedores
        public List<ListarComissionamentoModelo> ListarComissoesVendedores()
        {
            using (DBCONCESSIONARIAEntities db = new DBCONCESSIONARIAEntities())
            {
                var vendas = db.ListarComissoesVendedores();
                return vendas.Select(x => MapearModelo(x)).ToList();
            }

        }
        public ListarComissionamentoModelo MapearModelo(ListarComissoesVendedores_Result obj)
        {
            var modelo = new ListarComissionamentoModelo();

            modelo.NomeVendedor = obj.NomeVendedor;
            modelo.Valor = obj.Valor;
            modelo.Comissionamento = obj.Comissionamento;
            modelo.ValeCombustivel = obj.ValeCombustivel;
            modelo.IdVenda = obj.IdVenda;
            return modelo;


        }
        #endregion


    }
}