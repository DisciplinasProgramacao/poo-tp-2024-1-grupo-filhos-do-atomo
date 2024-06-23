using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAtomo.model
{
    internal class Cafe : Estabelecimento
    {

        public Cafe(int _id,string nomeEstabelecimento):base(_id,nomeEstabelecimento)
        {
            _mesas = new List<Mesa>
        {
            new Mesa(1, 4,false),
            new Mesa(2, 4,false),
            new Mesa(3, 4,false),
            new Mesa(4, 4,false),
            new Mesa(5, 6,false),
            new Mesa(6, 6,false),
            new Mesa(7, 6,false),
            new Mesa(8, 6,false),
            new Mesa(9, 8,false),
            new Mesa(10, 8,false)
        };
            _cardapio = new CardapioCafe();
        }

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
