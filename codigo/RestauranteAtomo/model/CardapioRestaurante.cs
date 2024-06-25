using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAtomo.model
{
    internal class CardapioRestaurante : Cardapio
    {
        public CardapioRestaurante() : base() { }

        /// <summary>
        /// Popula a lista de produtos com itens específicos do restaurante.
        /// </summary>
        protected override void carregarCardapio()
        {
            produtos.Add(new Produto("Moqueca de Palmito", 32.00));
            produtos.Add(new Produto("Falafel Assado", 20.00));
            produtos.Add(new Produto("Salada Primavera com Macarrão Konjac", 25.00));
            produtos.Add(new Produto("Escondidinho de Inhame", 18.00));
            produtos.Add(new Produto("Strogonoff de Cogumelos", 35.00));
            produtos.Add(new Produto("Caçarola de legumes", 22.00));
            produtos.Add(new Produto("Água", 3.00));
            produtos.Add(new Produto("Copo de suco", 7.00));
            produtos.Add(new Produto("Refrigerante orgânico", 7.00));
            produtos.Add(new Produto("Cerveja vegana", 9.00));
            produtos.Add(new Produto("Taça de vinho vegano", 18.00));
        }
    }
}
