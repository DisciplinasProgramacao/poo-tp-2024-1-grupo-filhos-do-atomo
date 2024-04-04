using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAtomo
{
    internal class ClassMesa
    {

        private int Numero;
        private int Capacidade;
        private bool ocupada;


        public Mesa(int numero, int capacidade, bool ocupada)
        {
            this.Numero = numero;
            this.Capacidade = capacidade;
            this.ocupada = ocupada;
        }

        public bool Numero
        {
            get { return Numero; }
            set { Numero = value; }
        }
        public int Capacidade
        {
            get { return capacidade; }
            set {Capacidade =value; }          
        }
        public bool Ocupada
        {
            get { return Ocupada; }
            set { Ocupada = value; }
        }

        public bool ValidaAlocacao(int quantidadePessoas)
        {
            if(quantidadePessoas <= Capacidade && !ocupada)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Liberar()
        {
            ocupada = false;
        }
        public void Ocupar()
        {
            ocupada = true;
        }


    }
}
