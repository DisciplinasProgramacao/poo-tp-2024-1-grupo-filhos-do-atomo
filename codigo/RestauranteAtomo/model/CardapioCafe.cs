using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAtomo.model
{
    internal class CardapioCafe : Cardapio
    {
        /// <summary>
        ///  É uma lista que guarda vários objetos do tipo Produto.
        /// </summary>
        private List<Produto> produtos;

        /// <summary>
        ///  Inicializa a classe e carrega os produtos.
        /// </summary>
        public CardapioCafe()
        {
            produtos = new List<Produto>();
            CarregarProdutos();
        }

        /// <summary>
        /// Popula a lista de produtos com itens específicos do café.
        /// </summary>
        public override void CarregarProdutos()
        {
            produtos.Add(new Produto("Pão de queijo", 5.00m));
            produtos.Add(new Produto("Bolinha de cogumelo", 7.00m));
            produtos.Add(new Produto("Rissole de palmito", 7.00m));
            produtos.Add(new Produto("Coxinha de carne de jaca", 8.00m));
            produtos.Add(new Produto("Fatia de queijo de caju", 9.00m));
            produtos.Add(new Produto("Biscoito amanteigado", 3.00m));
            produtos.Add(new Produto("Cheesecake de frutas vermelhas", 15.00m));
            produtos.Add(new Produto("Água", 3.00m));
            produtos.Add(new Produto("Copo de suco", 7.00m));
            produtos.Add(new Produto("Café espresso orgânico", 6.00m));
        }
    }
}
