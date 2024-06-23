using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAtomo.model
{
    internal class Cafe : Estabelecimento
    {

        public Cafe(int _id,string nomeEstabelecimento):base(_id,nomeEstabelecimento){}

        public override bool atenderCliente(Cliente cliente,int quantPessoas)
        {
            Requisicao requisicao = new Requisicao(cliente, quantPessoas);
            bool atendido = realizarAlocacaoMesa(requisicao);
            if (atendido)
            {
                historicoRequisicoes.Add(requisicao);
                return true;
            }

            return false;
        }
    }
}
