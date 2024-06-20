using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAtomo.model
{
    internal abstract class Estabelecimento
    {
        #region  /* Atributos */
        static int ultimo_id = 1;
        private int _id;
        private string _nomeEstabelecimento;
        protected List<Mesa> _mesas;
        protected List<Cliente> _clientes;
        protected List<Requisicao> historicoRequisicoes;
        protected Cardapio _cardapio;

        /// <summary>
        /// Construtor da classe restaurante
        /// </summary>
        protected Estabelecimento()
        {
            init();
        }

        protected Estabelecimento(string nomeEstabelecimento)
        {
            this._nomeEstabelecimento = nomeEstabelecimento;
            init();
        }

        private void init(){
            this._id = ultimo_id++;
            this._mesas = new List<Mesa>();
            this._clientes = new List<Cliente>();
            this.historicoRequisicoes = new List<Requisicao>();
        }

        public List<Mesa> Mesas { get => _mesas;}
        
        #endregion /* Fim Atributos */;
        
        
        #region  /* Métodos Privados */
        
        private List<Mesa> buscarMesasLivres() 
        {
            List<Mesa> mesasLivres = new List<Mesa>();
            foreach(Mesa mesas in Mesas)
            {
                if (!mesas.Ocupada)
                {
                    mesasLivres.Add(mesas);
                }
            }
            return mesasLivres;
        }
        
        
        #endregion /* Fim Metodo Privado */;
        
        
        #region  /* Métodos Publicos */
                
        /// <summary>
        /// Atende a requisicao feita pelo cliente
        /// </summary>
        /// <param name="cliente">Recebe o cliente que fez a requisicao</param>
        /// <param name="quantPessoas">Quantidade de pessoas na mesa</param>
        public abstract bool atenderCliente(Cliente cliente, int quantPessoas);

        /// <summary>
        /// Metodo para finalizar a requisicao do cliente
        /// </summary>
        /// <param name="requisicao">requisicao feita pelo cliente</param>
        public void finalizarRequisicao(Requisicao requisicao) 
        {
            requisicao.finalizar();
        }

        public Requisicao findRequisicaoAtendidaCliente(Cliente cliente){
            return historicoRequisicoes.Find(r => r.MeuCliente.Equals(cliente) && r.isAberto());
        }
        
        public String exibirMesas(){ 
            StringBuilder descMesas = new StringBuilder();
            foreach(Mesa mesa in _mesas)
            {
                descMesas.AppendLine("\nMesa " + mesa.Numero + ": capacidade para " + mesa.Capacidade + " pessoas.");
            }
            return descMesas.ToString();
        }

        protected virtual String exibirListaRequisicoes(){
            StringBuilder descEspera = new StringBuilder();
            descEspera.AppendLine("\n----Histórico de Requisições Atendidas----");
            foreach(Requisicao req in historicoRequisicoes){
                descEspera.AppendLine(req.MeuCliente.Nome + " : " + req.Mesa + " - Pessoas: " + req.QuantLugares);
            }
            return descEspera.ToString();
        }


        /// <summary>
        /// Permite ao cliente solicitar um item do cardápio e adicioná-lo à sua requisição.
        /// </summary>
        /// <param name="codigoProduto">Código único do produto no cardápio</param>
        /// <param name="requisicao">Requisição do cliente que está fazendo o pedido</param>
        public void AtenderSolicitacaoItem(int codigoProduto, Requisicao requisicao)
        {
            // 1. Obter o produto do cardápio com base no código fornecido
            Produto produto = ObterProdutoDoCardapio(codigoProduto);

            // 2. Verificar se o produto existe no cardápio
            if (produto != null)
            {
                // 3. Adicionar o produto à requisição do cliente
                requisicao.receberItemSolicitado(produto);
            }
        }

        /// <summary>
        /// Exibe o conteúdo do cardápio para o cliente.
        /// </summary>
        /// <returns>String contendo a lista de pratos e bebidas do cardápio</returns>
        public string ExibirCardapio()
        {
            StringBuilder cardapioTexto = new StringBuilder();
            // 1. Adicionar título do cardápio
            cardapioTexto.AppendLine(nomeEstabelecimento + " Cardápio**");
            return cardapioTexto.ToString() + _cardapio.menuFormatado();
        }

        /// <summary>
        /// Obtém um produto do cardápio com base no código fornecido.
        /// </summary>
        /// <param name="codigoProduto">Código único do produto no cardápio</param>
        /// <returns>Instância do `Produto` encontrado ou `null` se não encontrado</returns>
        private Produto ObterProdutoDoCardapio(int codigoProduto)
        {
            Produto produtoBuscado = _cardapio.LocalizarProduto(codigoProduto);
            return produtoBuscado; 
        }
        #endregion /* Fim Metodo Publicos */;
    }
}




