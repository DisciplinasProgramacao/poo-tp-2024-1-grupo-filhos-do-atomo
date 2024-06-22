using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAtomo.model
{
    internal class Cardapio
    {

        #region Atributo(s)

        /// <summary>
        /// Lista de que contém os produtos do cardápio
        /// </summary>
        private List<Produto> produtos;
        #endregion

        #region Construtor

        /// <summary>
        /// Inicializa a classe criando uma nova lista de produtos "cardápio"
        /// "Chama" o método carregar cardápio para adicioanr os produtos nele
        /// </summary>
        public Cardapio() {

            produtos = new List<Produto>();
            carregarCardapio();
        }
        #endregion


        #region Método(s) públicos

        /// <summary>
        /// Método que mostra o cardápio em formato de menu
        /// </summary>
        /// <returns>Retorna uma string como menu</returns>
        public string menuFormatado()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in produtos)
            {
                sb.AppendLine(item.ToString());
            } 

            return sb.ToString();
        }

        /// <summary>
        /// Método que localiza determinado produto dentro do cardápio atravez do seu código
        /// </summary>
        /// <param name="id">Código do produto que será procurado </param>
        /// <returns>Se encontrar o produto retorna o mesmo, se não retorna "null"</returns>
        public Produto LocalizarProduto(int id)
        {
            foreach (var item in produtos)
            {
                if (item.Codigo == id)
                {
                    return item;
                }
            }
            return null; 
        }
        #endregion

        #region Método(s) privados

        /// <summary>
        /// Método responsavel por adicionar itens do restaurante no cardápio
        /// </summary>
        private void carregarCardapio()
        {
            produtos.Add(new Produto("Moqueca de Palmito", 32.00));
            produtos.Add(new Produto("Falafel Assado", 20.00));
            produtos.Add(new Produto("Salada Primavera com Macarrão Konjac", 25.00));
            produtos.Add(new Produto("Escondidinho de Inhame", 18.0));
            produtos.Add(new Produto("Strogonoff de Cogumelos", 35.0));
            produtos.Add(new Produto("Caçarola de legumes", 22.0));
            produtos.Add(new Produto("Água", 3.0));
            produtos.Add(new Produto("Copo de suco", 7.0));
            produtos.Add(new Produto("Refrigerante orgânico", 7.0));
            produtos.Add(new Produto("Cerveja vegana", 9.0));
            produtos.Add(new Produto("Taça de vinho vegano", 18.0));
        }

        #endregion


    }
}
