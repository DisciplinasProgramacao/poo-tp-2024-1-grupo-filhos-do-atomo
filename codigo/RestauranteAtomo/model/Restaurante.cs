﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAtomo.model
{
    internal class Restaurante : Estabelecimento
    {
        #region  /* Atributos */
             
        private List<Requisicao> _filaDeEspera;
        

        /// <summary>
        /// Construtor da classe restaurante
        /// </summary>
        /// <param name="id">Recebe como valor um número inteiro para ser o identificador</param>
        public Restaurante(int id,string nomeEstabelecimento) : base (id, nomeEstabelecimento) 
        {
            _filaDeEspera = new List<Requisicao>();
            _cardapio = new CardapioCafe();//*
        }

        
        #endregion /* Fim Atributos */;
        
        
        #region  /* Métodos Privados */
        
        //Após refatoração, não tem método privado.

        #endregion /* Fim Metodo Privado */;


        #region  /* Métodos Publicos */


      

        /// <summary>
        /// Atende a requisicao feita pelo cliente
        /// </summary>
        /// <param name="cliente">Recebe o cliente que fez a requisicao</param>
        /// <param name="quantPessoas">Quantidade de pessoas na mesa</param>
        /// ....................................................................
        public override bool atenderCliente(Cliente cliente, int quantPessoas) 
        {
            Requisicao requisicao = new Requisicao(cliente, quantPessoas);
            bool atendido = realizarAlocacaoMesa(requisicao);
            if(atendido){
                historicoRequisicoes.Add(requisicao);
            }else{
                adicionarFilaEspera(requisicao);
            }
            return atendido;
        }


        /// <summary>
        /// Metodo para adicionar a requisicao a uma fila de espera
        /// </summary>
        /// <param name="requisicao">requisicao feita pelo cliente</param>(
        public void adicionarFilaEspera(Requisicao requisicao) 
        { 
            _filaDeEspera.Add(requisicao);
        }

        /// <summary>
        /// Metodo para remover a requisicao atendida da fila de espera
        /// </summary>
        public bool atenderProximoFilaEspera() 
        { 
            bool atendida = false;
            int posicao = -1;
            Requisicao proxima = null;
            for(int i = 0; i < _filaDeEspera.Count && atendida == false; i++){
                proxima = _filaDeEspera[i];
                atendida = realizarAlocacaoMesa(_filaDeEspera[i]);
                posicao = i;
            }

            if(atendida && proxima != null){
                _filaDeEspera.RemoveAt(posicao);
                historicoRequisicoes.Add(proxima);
            }

            return atendida;
        }

 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public Requisicao findRequisicaoNaoAtendidaCliente(Cliente cliente){
            return _filaDeEspera.Find(r => r.MeuCliente.Equals(cliente) && !r.foiAtendida());
        }
        

        /// <summary>
        ///  Exibe as requisicoes
        /// </summary>
        /// <returns>Retorna uma string do ToString de requisicao</returns>
        public String exibirListaRequisicoes(){
            StringBuilder descEspera = new StringBuilder("\n----Fila de Espera----\n");
            foreach(Requisicao req in _filaDeEspera){
                descEspera.AppendLine(req.MeuCliente.Nome + " : " + req.Mesa + " - Pessoas: " + req.QuantLugares);
            }

            descEspera.AppendLine("\n----Histórico de Requisições Atendidas----");
            foreach(Requisicao req in historicoRequisicoes){
                descEspera.AppendLine(req.MeuCliente.Nome + " : " + req.Mesa + " - Pessoas: " + req.QuantLugares);
            }

            return descEspera.ToString();
        }


        /// <summary>
        /// Método que verifica se a lista de espera está vazia
        /// </summary>
        /// <returns>Returna true casoa lista esteja vazia</returns>
        public bool isListaDeEsperaVazia()
        {
            bool resposta = false;

            if (_filaDeEspera.Count == 0)
            {
                resposta = true;
               
            }
            return resposta;
        }


        #endregion /* Fim Metodo Publicos */;
    }
}




