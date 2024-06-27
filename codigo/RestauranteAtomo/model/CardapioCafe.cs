using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAtomo.model
{
    internal class CardapioCafe : Cardapio
    {
        public CardapioCafe() : base() { }

        /// <summary>
        /// Popula a lista de produtos com itens específicos do café.
        /// </summary>
        protected override void carregarCardapio()
        {
            produtos.Add(new Produto("Não de queijo", 5.00));
            produtos.Add(new Produto("Bolinha de cogumelo", 7.00));
            produtos.Add(new Produto("Rissole de palmito", 7.00));
            produtos.Add(new Produto("Coxinha de carne de jaca", 8.00));
            produtos.Add(new Produto("Fatia de queijo de caju", 9.00));
            produtos.Add(new Produto("Biscoito amanteigado", 3.00));
            produtos.Add(new Produto("Cheesecake de frutas vermelhas", 15.00));
            produtos.Add(new Produto("Água", 3.00)); 
            produtos.Add(new Produto("Copo de suco", 7.00));
            produtos.Add(new Produto("Café espresso orgânico", 6.00));
        }
    }
}
