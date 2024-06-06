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
            sb.AppendLine("Menu: ");
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
            produtos.Add(new Produto("Pizza", 12.00));
            produtos.Add(new Produto("Burguer", 7.00));
            produtos.Add(new Produto("Açai", 8.50));
            produtos.Add(new Produto("Carbonara",20.0));
        }

        #endregion


    }
}
