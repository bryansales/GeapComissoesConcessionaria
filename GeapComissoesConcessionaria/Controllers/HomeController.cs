using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeapComissoesConcessionaria.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {



            return View();
        }


        public ActionResult Grafico()
        {
            GeapComissoesConcessionaria.Repositorio.VendasRepositorio vendasRepo = new GeapComissoesConcessionaria.Repositorio.VendasRepositorio();

            var vendas = vendasRepo.ListarComissoesVendedores().OrderBy(x => x.NomeVendedor);
            List<object> iDados = new List<object>();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("Vendedor", System.Type.GetType("System.String"));
            dt.Columns.Add("Valor", System.Type.GetType("System.Int32"));

            DataRow dr = dt.NewRow();

            foreach (var item in vendas.GroupBy(x => x.NomeVendedor))
            {
                var ValorVendas = item.Sum(x => x.Valor);
                dr = dt.NewRow();
                dr["Vendedor"] = item.Key;
                dr["Valor"] = item.Sum(x => x.Valor);
                dt.Rows.Add(dr);
            }

            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iDados.Add(x);
            }

            return Json(iDados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GraficoVendas()
        {

            return View("GraficoVendas");
        }

        public ActionResult RelatorioVendas()
        {

            GeapComissoesConcessionaria.Repositorio.VendasRepositorio vendasRepo = new GeapComissoesConcessionaria.Repositorio.VendasRepositorio();

            var vendas = vendasRepo.ListarComissoesVendedores();

            return View("RelatorioVendas", vendas);
        }
    }
}