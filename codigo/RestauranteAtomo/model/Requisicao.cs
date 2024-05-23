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
        /// Representa o produto da requisição
        /// </summary>
        private Pedido pedido;

        #endregion



        #region Constructor

        /// <summary>
        ///  Cria a requisição a partir de uma certa quantidade de lugares escolhido
        /// </summary>
        /// <param name="quantLugares"></param>
        public Requisicao(int quantLugares)
        {
            this.quantLugares = quantLugares;
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
        /// Cria uma variável double que recebe o total do pedido chamndo o método de fecharConta(),
        /// faz a divisão do total do pediod para o total de pessoas na mesa.
        /// </summary>
        /// <returns>Retorna o valor a ser pago por cada cliente</returns>
        public double valorPorCliente()
        {
            double total = fecharConta();
            double valorPorCliente = total / quantLugares;
            return valorPorCliente;
        }

        /// <summary>
        /// É chamado o método da classe Pedido que calcular o total do pedido, 
        /// chama o método de fechar a conta e retorna o total.
        /// </summary>
        /// <returns>Retorna o total do pedido</returns>
        public double fecharConta()
        {
            double total = pedido.CalcularValorTotal();
            pedido.fechar();
            return total;
        }        

        /// <summary>
        /// Recebe o produto da classe Produto
        /// </summary>
        /// <param name="produto">produto dado como parâmetro para ser adicionado</param>
        public void receberItemSolicitado(Produto produto)
        {
            pedido.adicionarItem(produto);
        }
        #endregion
    }
}
