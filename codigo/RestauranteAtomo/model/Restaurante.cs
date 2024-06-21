using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAtomo.model
{
    internal class Restaurante : Estabelecimento
    {
        #region  /* Atributos */
        
        
        private const int _maxMesas = 10;

        private List<Mesa> _mesas;
        private List<Requisicao> _filaDeEspera;
        private int _id;
        private List<Requisicao> historicoRequisicoes;
        private Cardapio _cardapio;

        /// <summary>
        /// Construtor da classe restaurante
        /// </summary>
        /// <param name="id">Recebe como valor um número inteiro para ser o identificador</param>
        public Restaurante(int id)
        {
            _id = id;
            _mesas = new List<Mesa>
        {
           /* new Mesa(1, 4,false), */
           /* new Mesa(2, 4,false), 
            new Mesa(3, 4,false), 
            new Mesa(4, 4,false),
            new Mesa(5, 6,false), 
            new Mesa(6, 6,false), 
            new Mesa(7, 6,false), 
            new Mesa(8, 6,false),
            new Mesa(9, 8,false), 
            new Mesa(10, 8,false)*/
        };
            _filaDeEspera = new List<Requisicao>();
            historicoRequisicoes = new List<Requisicao>();
            _cardapio = new Cardapio();
        }

        internal List<Mesa> Mesas { get => _mesas;}
        
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
        
        /// <summary>
        /// Metodo para realizar a alocacao do cliente a uma mesa ou fila de espera
        /// </summary>
        /// <param name="requisicao">requisicao feita pelo cliente</param>
        private bool realizarAlocacaoMesa(Requisicao requisicao)
        {
            List<Mesa> mesasLivres = buscarMesasLivres();
        
            foreach(Mesa mesa in mesasLivres)
            {
                if (mesa.ValidaAlocacao(requisicao.QuantLugares))
                {
                    requisicao.alocarMesa(mesa);
                    return true;
                }
            }
            return false;
        }

        #endregion /* Fim Metodo Privado */;


        #region  /* Métodos Publicos */


        /// <summary>
        /// Registra uma nova mesa para o restaurante
        /// </summary>
        /// <param name="mesa">Parametro para receber os dados do objeto do tipo mesa</param>
        public void adicionarMesa(Mesa mesa)
        {
            if (_mesas.Count <= _maxMesas)
            {
                _mesas.Add(mesa);
            }
        }

        /// <summary>
        /// Atende a requisicao feita pelo cliente
        /// </summary>
        /// <param name="cliente">Recebe o cliente que fez a requisicao</param>
        /// <param name="quantPessoas">Quantidade de pessoas na mesa</param>
        public bool atenderCliente(Cliente cliente, int quantPessoas) 
        {
            Requisicao requisicao = new Requisicao(cliente, quantPessoas);
            bool atendido = realizarAlocacaoMesa(requisicao);
            if(atendido){
                historicoRequisicoes.Add(requisicao);
            }else{
                adicionarFilaEspera(requisicao);
            }
            return atendido;
        }

        /// <summary>
        /// Metodo para adicionar a requisicao a uma fila de espera
        /// </summary>
        /// <param name="requisicao">requisicao feita pelo cliente</param>(
        public void adicionarFilaEspera(Requisicao requisicao) 
        { 
            _filaDeEspera.Add(requisicao);
        }

        /// <summary>
        /// Metodo para remover a requisicao atendida da fila de espera
        /// </summary>
        public bool atenderProximoFilaEspera() 
        { 
            bool atendida = false;
            int posicao = -1;
            Requisicao proxima = null;
            for(int i = 0; i < _filaDeEspera.Count && atendida == false; i++){
                proxima = _filaDeEspera[i];
                atendida = realizarAlocacaoMesa(_filaDeEspera[i]);
                posicao = i;
            }

            if(atendida && proxima != null){
                _filaDeEspera.RemoveAt(posicao);
                historicoRequisicoes.Add(proxima);
            }

            return atendida;
        }

        /// <summary>
        /// Metodo para finalizar a requisicao do cliente
        /// </summary>
        /// <param name="requisicao">requisicao feita pelo cliente</param>
        public void finalizarRequisicao(Requisicao requisicao) 
        {
            requisicao.finalizar();
        }


        public Requisicao findRequisicaoAtendidaCliente(Cliente cliente){
            return historicoRequisicoes.Find(r => r.MeuCliente.Equals(cliente) && r.isAberta());
        }

        public Requisicao findRequisicaoNaoAtendidaCliente(Cliente cliente){
            return _filaDeEspera.Find(r => r.MeuCliente.Equals(cliente) && !r.foiAtendida());
        }
        
        public String exibirMesas(){ 
            StringBuilder descMesas = new StringBuilder();
            foreach(Mesa mesa in _mesas)
            {
                descMesas.AppendLine("\nMesa " + mesa.Numero + ": capacidade para " + mesa.Capacidade + " pessoas.");
            }
            return descMesas.ToString();
        }

        public String exibirListaRequisicoes(){
            StringBuilder descEspera = new StringBuilder("\n----Fila de Espera----\n");
            foreach(Requisicao req in _filaDeEspera){
                descEspera.AppendLine(req.MeuCliente.Nome + " : " + req.Mesa + " - Pessoas: " + req.QuantLugares);
            }

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
            else
            {
                // 4. Tratar o erro de produto não encontrado (opcional)
                Console.WriteLine("O produto Desejado Não existe..");
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
            cardapioTexto.AppendLine("============Cardápio do Restaurante============");
            return cardapioTexto.ToString() + _cardapio.menuFormatado();
        }



        /// <summary>
        /// Obtém um produto do cardápio com base no código fornecido.
        /// </summary>
        /// <param name="codigoProduto">Código único do produto no cardápio</param>
        /// <returns>Instância do `Produto` encontrado ou `null` se não encontrado</returns>
        private Produto ObterProdutoDoCardapio(int codigoProduto)
        {
            try
            {
                Produto produtoBuscado = _cardapio.LocalizarProduto(codigoProduto);
                return produtoBuscado;
            }
            catch (FormatException) // Erro na conversão do código para inteiro
            {
                // Tratar o erro de formato de código (opcional)
                Console.WriteLine("Codigo Inválido");
                return null;
            }
        }
        #endregion /* Fim Metodo Publicos */;
    }
}




