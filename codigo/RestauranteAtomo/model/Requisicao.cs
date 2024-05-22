using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAtomo.model
{
    internal class Requisicao
    {

        #region Atributos

        /// <summary>
        ///  Representa a mesa na qual a requisição foi atendida
        /// </summary>
        private Mesa mesa;
        /// <summary>
        /// Quantidade de lugares necessários para atender a requisição
        /// </summary>
        private int quantLugares;
        /// <summary>
        /// Data e hora de início do atendimento
        /// </summary>
        private DateTime chegada;
        /// <summary>
        /// Data e Hora do fim do atendimento
        /// </summary>
        private DateTime saida;
        /// <summary>
        /// Representa se a requisição ja foi atendida ou não
        /// </summary>
        private bool atendida;
        /// <summary>
        /// Lista que pode conter objetos do tipo Produto.
        /// </summary>
        private List<Produto> produtos;

        #endregion



        #region Constructor

        /// <summary>
        ///  Cria a requisição a partir de uma certa quantidade de lugares escolhido
        /// </summary>
        /// <param name="quantLugares"></param>
        public Requisicao(int quantLugares)
        {
            this.quantLugares = quantLugares;
            this.produtos = new List<Produto>();
            atendida = false;
            chegada = DateTime.Now;
        }
        #endregion



        #region Propriedades

        /// <summary>
        ///  propriedade get para ver a quantidade de lugares da requisicao
        /// </summary>
        /// <returns> Returna a  quantidade de lugares</returns>
        public int QuantLugares
        {
            get => this.quantLugares;
        }

        public Mesa Mesa
        {
            get => this.mesa;
        }

        #endregion



        #region Metódos


        /// <summary>
        ///  Metodo de ver(get) se a requisição ja foi atendida ou não
        /// </summary>
        /// <returns> Returna a condição atual da requisição</returns>
        public bool foiAtendida()
        {
            return atendida;
        }


        /// <summary>
        ///  Aloca mesa passada como parametro  para atender a requisição
        /// </summary>
        /// <param name="mesa">Mesa do restaurante passada por parametro</param>
        public void alocarMesa(Mesa mesa)
        {
           
                this.mesa = mesa;
                atendida = true;
                this.mesa.Ocupar();

        }

        /// <summary>
        /// Após a reuqisição ser atendida e o cliente acabar, é registrada a hora de saída
        /// </summary>
        public void registrarHoraSaida()
        {
            saida = DateTime.Now;
        }

        /// <summary>
        /// Adiciona um novo produto à lista 'produtos'
        /// </summary>
        /// <param name="produto">Produto passado como parâmetro</param>
        public void adicionarProduto(Produto produto)
        {
            produtos.Add(produto);
        }

        /// <summary>
        /// Finalizar o pedido de uma mesa, 
        /// calcular o total da conta com a taxa de serviço e dividir o valor igualmente entre os clientes.
        /// </summary>
        public void fecharConta()
        {
            registrarHoraSaida();
            double total = CalcularTotal();
            double totalComServico = total * 1.10;
            double valorPorCliente = totalComServico / quantLugares;

            Console.WriteLine("Conta fechada:");
            Console.WriteLine($"Total: R$ {total:F2}");
            Console.WriteLine($"Total com serviço (10%): R$ {totalComServico:F2}");
            Console.WriteLine($"Valor por cliente: R$ {valorPorCliente:F2}");
        }

        /// <summary>
        /// Soma o preço de todos os produtos na lista para calcular o total.
        /// </summary>
        /// <returns>Retorna o valor total</returns>
        private double CalcularTotal()
        {
            return produtos.Sum(p => p.Preco);
        }
        #endregion
    }
}
