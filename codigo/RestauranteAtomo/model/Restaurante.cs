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
            List<Mesa> mesasLivres = new List<Mesa>();
            foreach(Mesa mesas in _mesas)
            {
                if (!mesas.Ocupada)
                {
                    mesasLivres.Add(mesas);
                }
            }
            return mesasLivres;
        }
        
        private void realizarAlocacaoMesa(Requisicao requisicao)
        {
            List<Mesa> mesasLivres = buscarMesasLivres();
        
            foreach(Mesa mesa in mesasLivres)
            {
                if (mesa.validaAlocacao(requisicao.QuantLugares) && !mesa.Ocupada)
                {
                    requisicao.alocarMesa(mesa);
                    return;
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
        /// <param name="quantPessoas">Quantidade de pessoas na mesa</param>
        public void atenderCliente(Cliente cliente, int quantPessoas) 
        {
            cliente.fazerRequisicao(quantPessoas);
            realizarAlocacaoMesa(cliente.Requisicao);
        }
        public void adicionarFilaEspera(Requisicao requisicao) 
        { 
            _filaDeEspera.Enqueue(requisicao);
        }
        public void atenderProximoFilaEspera() 
        { 
            Requisicao atenderFila = _filaDeEspera.Dequeue();
            realizarAlocacaoMesa(atenderFila);
        }
        public void finalizarRequisicao(Requisicao requisicao) 
        {
            requisicao.foiAtendida();
        }
        
        #endregion /* Fim Metodo Publicos */;
    }
}
