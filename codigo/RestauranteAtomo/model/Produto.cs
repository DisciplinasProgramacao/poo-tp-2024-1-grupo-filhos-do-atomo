using RestauranteAtomo.model;
﻿using System;
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
        /// Codigo único do produto
        /// </summary>
        private int _codigo;
        /// <summary>
        /// Variável de controle do código
        /// </summary>
        private static int _proxCodigo = 1;
        /// <summary>
        /// Representa a descrição do produto
        /// </summary>
        private string _descricao;
        /// <summary>
        /// Preco em reais do produto
        /// </summary>
        private double _preco;
        #endregion


        #region Propriedade(s)

        /// <summary>
        /// Método get que retorna o codigo do produto
        /// </summary>
        public int Codigo
        {
            get { return _codigo; }
        }

        /// <summary>
        /// Método get que retorna o preço do produto
        /// </summary>
        public double Preco
        {
            get { return _preco; }
        }
        #endregion


        #region Construtor

        /// <summary>
        /// Método que inicializa/cria o produto de acordo coms os parâmetros passados
        /// </summary>
        /// <param name="descricao">Descrição do produto</param>
        /// <param name="preco">Preço do produto</param>
        private void init(string descricao, double preco)
        {
            _codigo = _proxCodigo;
            _proxCodigo++;
            _descricao = descricao;
            _preco = preco;
        }

        /// <summary>
        /// Método que construtor do produto que passa como parametro para o "init" 
        /// a descrição e o preço do produto
        /// </summary>
        /// <param name="descricao">Descrição do produto</param>
        /// <param name="preco">Preço do produto</param>
        public Produto(string descricao, double preco)
        {

            init(descricao, preco);
        }

        /// <summary>
        /// Método que construtor do produto que passa como parametro para o "init" 
        /// somente a descrição do produto, passando um preço default/pré definido
        /// para produtos que o preço não foi informado
        /// </summary>
        /// <param name="descricao">Descrição do produto</param>
        public Produto(string descricao)
        {

            init(descricao, 5.0);

        }
        #endregion


        #region Métodos

        /// <summary>
        /// Método de sobreescrita ToString 
        /// </summary>
        /// <returns>Retorna uma string formatada dos atributos do proprio produto</returns>
        public override string ToString()
        {
            return
                $"+------------------------------+" +
                $"Código: {_codigo},\nDescrição: {_descricao},\nPreço: {_preco:C}" +
                $"+------------------------------+";
        }
        #endregion


    }
}