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
        private List<Mesas> _mesas;
        private List<Requisicao> _filaDeEspera;
        private int _id;

        /// <summary>
        /// Construtor da classe restaurante
        /// </summary>
        /// <param name="id">Recebe como valor um número inteiro para ser o identificador</param>
        public Restaurante(int id)
        {
            _id = id;
            _mesas = new List<Mesas>();
            _filaDeEspera = new List<Requisicao>();
        }

        #endregion /* Fim Atributos */;


        #region  /* Métodos Privados */

        private List<Mesas> buscarMesasLivres() { return _mesas; } //So para nao dar erro de falta de retorno

        #endregion /* Fim Metodo Privado */;


        #region  /* Métodos Publicos */
        public void adicionarMesa(Mesas mesa) { }
        public void atenderCliente(Cliente cliente, int quantidadePessoas) { }
        public void adicionarFilaEspera(Requisicao requisicao) { }
        public void atenderProximoFilaEspera() { }
        public void finalizarRequisicao(Requisicao requisicao) { }

        #endregion /* Fim Metodo Publicos */;

    }
}
