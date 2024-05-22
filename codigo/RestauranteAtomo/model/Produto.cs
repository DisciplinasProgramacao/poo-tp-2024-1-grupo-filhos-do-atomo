using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAtomo.model
{
    internal class Produto
    {
        #region Atributos

        /// <summary>
        /// Representa o nome do produto
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Representa preço do produto
        /// </summary>
        public double Preco { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Criar o produto a partir do nome e seu preço.
        /// </summary>
        /// <param name="nome">Nome dado ao produto</param>
        /// <param name="preco">Preço dado ao produto</param>
        public Produto(string nome, double preco)
        {
            Nome = nome;
            Preco = preco;
        }
        #endregion

    }
}
