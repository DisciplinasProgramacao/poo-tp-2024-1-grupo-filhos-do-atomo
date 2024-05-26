﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAtomo.model
{
    internal class Restaurante
    {
        #region  /* Atributos */
        
        
        private const int _maxMesas = 10;
        private List<Mesa> _mesas;
        private Queue<Requisicao> _filaDeEspera;
        private int _id;

        /// <summary>
        /// Construtor da classe restaurante
        /// </summary>
        /// <param name="id">Recebe como valor um número inteiro para ser o identificador</param>
        public Restaurante(int id)
        {
            _id = id;
            _mesas = new List<Mesa>();
            _filaDeEspera = new Queue<Requisicao>();
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
            if(_mesas.Count <= _maxMesas)
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
            Requisicao requisicao = cliente.fazerRequisicao(quantPessoas);
            return realizarAlocacaoMesa(requisicao);
            
        }
        /// <summary>
        /// Metodo para adicionar a requisicao a uma fila de espera
        /// </summary>
        /// <param name="requisicao">requisicao feita pelo cliente</param>
        public void adicionarFilaEspera(Requisicao requisicao) 
        { 
            _filaDeEspera.Enqueue(requisicao);
        }
        /// <summary>
        /// Metodo para remover a requisicao atendida da fila de espera
        /// </summary>
        public bool atenderProximoFilaEspera() 
        { 
            bool retiradaFilaEspera = false;
            if(_filaDeEspera.Count > 0)
            {
                Requisicao atenderFila = _filaDeEspera.Peek();
                bool atendida = realizarAlocacaoMesa(atenderFila);
                if (atendida)
                {
                    _filaDeEspera.Dequeue();
                    retiradaFilaEspera = true;
                }
            }
            return retiradaFilaEspera;
        }
        /// <summary>
        /// Metodo para finalizar a requisicao do cliente
        /// </summary>
        /// <param name="requisicao">requisicao feita pelo cliente</param>
        public void finalizarRequisicao(Requisicao requisicao) 
        {
            requisicao.registrarHoraSaida();
            requisicao.Mesa.Liberar();
        }

        #endregion /* Fim Metodo Publicos */;



        /// <summary>
        /// Permite ao cliente solicitar um item do cardápio e adicioná-lo à sua requisição.
        /// </summary>
        /// <param name="codigoProduto">Código único do produto no cardápio</param>
        /// <param name="requisicao">Requisição do cliente que está fazendo o pedido</param>
        public void AtenderSolicitacaoItem(string codigoProduto, Requisicao requisicao)
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
            cardapioTexto.AppendLine("**Restaurante Atomo - Cardápio**");

            // 2. Adicionar seção de pratos
            cardapioTexto.AppendLine("\n**Pratos:**");
            foreach (Prato prato in _cardapio.ObterPratos())
            {
                cardapioTexto.AppendLine($"Código: {prato.Id} - Nome: {prato.Nome} - Preço: R${prato.Preco}");
            }

            // 3. Adicionar seção de bebidas
            cardapioTexto.AppendLine("\n**Bebidas:**");
            foreach (Bebida bebida in _cardapio.ObterBebidas())
            {
                cardapioTexto.AppendLine($"Código: {bebida.Id} - Nome: {bebida.Nome} - Preço: R${bebida.Preco}");
            }

            return cardapioTexto.ToString();
        }



        /// <summary>
        /// Obtém um produto do cardápio com base no código fornecido.
        /// </summary>
        /// <param name="codigoProduto">Código único do produto no cardápio</param>
        /// <returns>Instância do `Produto` encontrado ou `null` se não encontrado</returns>
        private Produto ObterProdutoDoCardapio(string codigoProduto)
        {
            try
            {
                int idProduto = int.Parse(codigoProduto); // Tentar converter o código para inteiro
                Prato pratoBuscado = _cardapio.ObterPratoPorId(idProduto);
                Bebida bebidaBuscada = _cardapio.ObterBebidaPorId(idProduto);

                if (pratoBuscado != null)
                {
                    return pratoBuscado;
                }
                else if (bebidaBuscada != null)
                {
                    return bebidaBuscada;
                }
                else
                {
                    return null; // Produto não encontrado
                }
            }
            catch (FormatException) // Erro na conversão do código para inteiro
            {
                // Tratar o erro de formato de código (opcional)
                Console.WriteLine("Codigo Inválido");
                return null;
            }
        }


    }
}




