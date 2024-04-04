using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAtomo.model
{
    internal class Restaurante
    {

        #region  /* Atributos */


        private const int _maxMesas = 10;
        private List<Mesa> _mesas;
        private Queue<Requisicao> _filaDeEspera;
        private int _id;

        /// <summary>
        /// Construtor da classe restaurante
        /// </summary>
        /// <param name="id">Recebe como valor um número inteiro para ser o identificador</param>
        public Restaurante(int id)
        {
            _id = id;
            _mesas = new List<Mesa>();
            _filaDeEspera = new Queue<Requisicao>();
        }

        #endregion /* Fim Atributos */;


        #region  /* Métodos Privados */

        private List<Mesa> buscarMesasLivres() 
        {
           return _mesas;
        }

        private void alocarMesa(Requisicao requisicao)
        {
            foreach (Mesa mesas in _mesas)
            {
                if (mesas.validaAlocao(requisicao.QuantLugares))
                {
                    mesas.ocupar();
                }
                else
                {
                    adicionarFilaEspera(requisicao);
                }
            }
        }

        #endregion /* Fim Metodo Privado */;


        #region  /* Métodos Publicos */

        
        /// <summary>
        /// Registra uma nova mesa para o restaurante
        /// </summary>
        /// <param name="mesa">Parametro para receber os dados do objeto do tipo mesa</param>
        public void adicionarMesa(Mesa mesa) 
        { 
            if(_mesas.Count <= _maxMesas)
            {
                _mesas.Add(mesa);
            }
        }
        /// <summary>
        /// Atende a requisicao feita pelo cliente
        /// </summary>
        /// <param name="cliente">Recebe o cliente que fez a requisicao</param>
        public void atenderCliente(Cliente cliente) 
        {
            alocarMesa(cliente.Requisicao);
        }
        public void adicionarFilaEspera(Requisicao requisicao) 
        { 
            _filaDeEspera.Enqueue(requisicao);
        }
        public void atenderProximoFilaEspera() 
        { 
            foreach(Requisicao atendimento in _filaDeEspera)
            {

            }
        }
        public void finalizarRequisicao(Requisicao requisicao) 
        { 
        
        }

        #endregion /* Fim Metodo Publicos */;

    }
}
