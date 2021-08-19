# GeapComissoesConcessionaria
Comissões concessionaria

1 - Foi criado uma solução chamada GeapComissoesConcessionaria onde foi incluido dois projetos, um projeto de Class Library e outro .NET MVC5.

2 - O projeto Class Library se chama DAL onde é feito a implementação e mapeamento da procedure ListarComissoesVendedores

3 - Nos dois projetos foi adicionado a referencia do entity framework

4 - No projeto MVC foi criado um controller chamado Home onde foi chamado o metodo ListarComissoesVendedores, esse metodo está implementado na pasta de repositorios na class VendasRepositorio dentro do mesmo projeto.

A procedure tem como intuito retornar o comissionamento de cada vendedor de acordo com as vendas seguindo as regras de calculo:

Cada venda será comissionada.
Cada vendedor é comissionado apenas por suas vendas.
O valor da comissão é de 1% do valor da venda.
Se na venda foi emitido um vale combustível para o veículo ao comprador, a comissão desta venda sofrerá um desconto de acordo com o combustivel do veiculo vendido:
Combustível do veículo vendido	Desconto da comissão
Gasolina	R$ 200,00
Álcool	R$ 180,00
Diesel	R$ 150,00

A procedure tem o select para retornar o calculo de acordo com a regra(O cod da procedure foi adicionado ao projeto na pasta "Procedure"):

SELECT DISTINCT  
vnd.NmeVendedor as NomeVendedor,
v.IdeVenda as IdVenda,
v.VlrPrecoVenda as Valor,
v.StaValeCombustivel as ValeCombustivel,
case v.StaValeCombustivel when 0 then v.VlrPrecoVenda * 0.01 else 
case c.IdeCombustivel 
when 1 then (case when((v.VlrPrecoVenda * 0.01) - 200) <= 0 then 0 else (v.VlrPrecoVenda * 0.01) - 200 end)  
when 2 then (case when((v.VlrPrecoVenda * 0.01) - 180) <= 0 then 0 else (v.VlrPrecoVenda * 0.01) - 180 end) 
when 3 then  (case when((v.VlrPrecoVenda * 0.01) - 150) <= 0 then 0 else (v.VlrPrecoVenda * 0.01) - 150 end)end end as Comissionamento

FROM dbo.VND002_VENDA v 
join dbo.VND001_VENDEDOR vnd on vnd.IdeVendedor = v.IdeVendedor
join dbo.VEI004_MODELO_VERSAO versao on versao.IdeModeloVersao = v.IdeModeloVersao
join dbo.VEI003_COMBUSTIVEL c on c.IdeCombustivel = versao.IdeCombustivel

O retorno da procedure criada terá as definições da classe abaixo, gerada pelo proprio entity, classe que se encontra no projeto DAL:

    public partial class ListarComissoesVendedores_Result
    {
        public string NomeVendedor { get; set; }
        public int IdVenda { get; set; }
        public decimal Valor { get; set; }
        public bool ValeCombustivel { get; set; }
        public Nullable<decimal> Comissionamento { get; set; }
    }
    
5 - Dentro da classe VendasRepositorio do repositorio o retorno foi tratado com um metodo "MapearModelo" ultilizando a classe ListarComissionamentoModelo(localizada na pasta models), abaixo está as propriedades da mesma:

public class ListarComissionamentoModelo
    {
        public string NomeVendedor { get; set; }
        public int IdVenda { get; set; }
        public decimal Valor { get; set; }
        public bool ValeCombustivel { get; set; }
        public Nullable<decimal> Comissionamento { get; set; }
    }
  
  
 ao mapear sistema vai retornar para a view RelatorioVendas o resultado das comissões por vendedor, o resultado é exibido em uma tabela, foi ultilizado o DataTables que  é um plug-in para a biblioteca jQuery, para melhorar a visualização, ordenar colunas e permitir pesquisar os dados da tabela.

6 -E por fim foi implementado usando chart.js um grafico para exibir a quantidade de vendas por vendedor, ultilizando o mesmo metodo ListarComissoesVendedores chamado no controller home na action Grafico. 


