using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAtomo.model
{
    internal class Mesa
    {

        private int numero;
        private int capacidade;
        private bool ocupada;
        /// <summary>
        /// Metodo Construtor
        /// </summary>
        /// <param name="numero">Numero da mesa</param>
        /// <param name="capacidade"> Capacidade da mesa</param>
        /// <param name="ocupada"> Se está Ocupada ou não</param>
        public Mesa(int numero, int capacidade, bool ocupada)
        {
            this.numero = numero;
            this.capacidade = capacidade;
            this.ocupada = ocupada;
        }

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        public int Capacidade
        {
            get { return capacidade; }
            set { capacidade = value; }
        }
        public bool Ocupada
        {
            get { return ocupada; }
            set { ocupada = value; }
        }
        /// <summary>
        /// Valida a alocação Se a quantidade de pessoas for menor do que a capacidade e a mesa não estiver ocupada...
        /// </summary>
        /// <param name="quantidadePessoas"> Quantidade de pessoas é recebido como parametro</param>
        /// <returns></returns>
        public bool ValidaAlocacao(int quantidadePessoas)
        {
            if (quantidadePessoas <= Capacidade && !ocupada)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// O metodo para liberar uma mesa quando não estiver mais ocupada...
        /// </summary>
        public void Liberar()
        {
            ocupada = false;
        }
        /// <summary>
        /// O metodo para Ocupar a mesa caso seja aceita pelo metodo validar Alocação...
        /// </summary>
        public void Ocupar()
        {
            ocupada = true;
        }

        public override string ToString()
        {
            return $"Num.Mesa:{numero} \n" +
                   $"Capacidade:{capacidade}";
        }


    }
}
