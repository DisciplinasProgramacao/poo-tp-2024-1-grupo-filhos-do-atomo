﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAtomo.model
{
    internal class Pedido
    {
        #region Atributo

        private const double _TAXA_SERVICO = 0.1;
        private double _total;
        private List<Produto> _itens;
        private bool _aberto;

        #endregion

        #region Propriedades
        public double Total { get => _total; }
        public bool Aberto { get => _aberto; }

        #endregion



        #region Metodos

        public Pedido() 
        {
            this._itens = new List<Produto>();
        }

        /// <summary>
        /// Meetodo para calcular o total da soma dos itens do pediddo
        /// </summary>
        /// <returns>Retorna o valor da soma</returns>
        public double calcularValorTotal()
        {
            foreach (Produto pd in _itens)
            {
                _total += pd.Preco;
            }
            return _total;
        }

        /// <summary>
        /// Adiciona na lista de produtos o item
        /// </summary>
        /// <param name="produto">Rebece um parametro do tipo produto</param>
        public void adicionarItem(Produto produto)
        {
            _itens.Add(produto);
        }

        /// <summary>
        /// Mostra a lista de produtos dentro do pedido
        /// </summary>
        /// <returns>Retorna uma string dos dados dos produtos</returns>
        public string resumoPedido()
        {
            StringBuilder relatorio = new StringBuilder("Resumo Pedido: \n");

            foreach (Produto dado in _itens)
            {
                relatorio.AppendLine($"{dado} - R${dado.Preco}\n");
            }
            return relatorio.ToString();

        }

        /// <summary>
        ///  Atualiza o status do pedido.
        /// </summary>
        public void fechar()
        {
            this._aberto = false;
        }

        #endregion
    }
}
