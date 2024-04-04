using RestauranteAtomo.model;
using System.Text;

namespace RestauranteAtomo
{
    internal class Program
    {
        #region atributo estático - restaurante
        static Restaurante restaurante = new Restaurante(1);
        #endregion

        #region metodos auxiliares e de execucao dos processos
        /// <summary>
        /// Retorna as opcoes de menu do sistema.
        /// </summary>
        public static String menu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Menu");
            sb.AppendLine("1) Atender novo cliente");
            sb.AppendLine("0) Sair do programa");

            return sb.ToString();
        }

        /// <summary>
        /// Cria um novo cliente a partir dos dados solicitados.
        /// </summary>
        /// <returns>O cliente cadastrado, caso os dados sejam validos.</returns>
        public static Cliente registrarCliente()
        {
            Console.WriteLine("Informe o nome do cliente: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Informe o contato do cliente (telefone)");
            string contato = Console.ReadLine();

            if(nome.Length <= 3 && contato.Length == 0)
                return null;
     
            return new Cliente();
        }

        /// <summary>
        /// Inicia o atendimento a um cliente do restaurante. Solicita a
        /// quantidade de pessoas que vao comer no restaurante e encaminha
        /// para a criacao e atendimento da requisicao.
        /// </summary>
        /// <param name="c">O cliente cuja requisicao sera criada e atendida</param>
        public static void iniciarAtendimento(Cliente cliente)
        {
            Console.WriteLine("Reserva para quantas pessoas ?");
            int quantidadePessoas;
            do
            {
                quantidadePessoas = int.Parse(Console.ReadLine());
                if (quantidadePessoas <= 0)
                {
                    Console.WriteLine("Quantidade de pessoas deve ser maior que 0. Digite novamente");
                }
                else
                {
                   restaurante.atenderCliente(cliente, quantidadePessoas);
                }
            }while (quantidadePessoas <= 0);
        }

        // Hayanne

        /// <summary>
        /// verifica se a fila de espera não está vazia, e remove a primeira requisição da fila.
        /// o Find é utilizado para encontrar uma mesa disponível na lista de mesas (mesas) 
        /// que está associada ao restaurante. O critério de busca é uma expressão lambda 
        /// que verifica se a mesa está liberada (m.ocupada == false).Se uma mesa liberada for encontrada, 
        /// ela é retornada pelo Find e armazenada na variável mesa. Se nenhuma mesa desocupada for encontrada, 
        /// o Find retorna null, indicando que não há mesas disponíveis para atender à requisição.
        /// </summary>
        /// <param name="requisicao">Recebe um objeto Requisicao</param>
        /// <returns>Retorna true se a requisição foi finalizada com sucesso, retorna false se não havia nenhuma requisição ou não foi encontrada nenhuma mesa ocupada</returns>
        public bool finalizarRequisicao(Requisicao requisicao)
        {
            if (restaurante.getFilaDeEspera.Count > 0)
            {
                restaurante.getFilaDeEspera.Dequeue();
                Mesa mesa = mesas.Find(m => m.ocupada == true);
                if (mesa != null) mesa.liberar();
                return true;
            }
            return false;
        }

        /// <summary>
        /// É instaciado um lista mesas, se a posicao da mesa estiver liberada, 
        /// ela é adicionada a lista de mesas.
        /// </summary>
        /// <param name="mesa">Recebe uma mesa do tipo Mesa</param>
        public void adicionarMesa(Mesa mesa)
        {
            if (mesas.liberar()) mesas.Add(mesa);
        }

        /// <summary>
        /// É instanciado uma nova lista de Mesa com nome "mesasLivres", 
        /// um for percorre a lista de mesas ja usada e verifica a cada posição se a mesa está liberada, 
        /// se for verdadeiro é adicionada a nova lista de mesas liberadas.
        /// </summary>
        /// <returns>Retorna a lista de mesas liberadas</returns>
        private List<Mesa> buscarMesaLivre()
        {
            List<Mesa> mesasLivres = new List<Mesa>();

            for (int i = 0; i < mesas.Count; i++)
            {
                if (mesas[i].liberar())
                {
                    mesasLivres.Add(mesas[i]);
                }
            }
            return mesasLivres;
        }
        #endregion

        static void Main(string[] args)
        {
            int opcao;
            do
            {
                Console.WriteLine(menu());
                Console.WriteLine("Digite a opção desejada: ");
                opcao = int.Parse(Console.ReadLine());
                switch (opcao)
                {
                    case 1:
                        Cliente cliente = registrarCliente();

                        if(cliente != null){
                            iniciarAtendimento(cliente);
                        }else {
                            Console.WriteLine("Cliente não registrado. Informações não preenchidas corretamente!");
                        }
                        break;
                    case 0:
                        break;
                    default: 
                        Console.WriteLine("Opção inválida!\n");
                        break;
                }
            } while (opcao != 0);
        }
    }
}
