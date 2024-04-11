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
            sb.AppendLine("2) Adicionar mesa ao Restaurante");
            sb.AppendLine("3) Finalizar requisição do cliente");
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
            long contato;

            bool numeroValido = long.TryParse(Console.ReadLine(), out contato);

            if(nome.Length <= 3 && !numeroValido)
                return null;
     
            return new Cliente(nome, contato.ToString());
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
        /// Chama o método finalizarRequisição() da classe Rstaurante.
        /// </summary>
        /// <param name="requisicao">Recebe uma requisição como parâmetro</param>
        /// <returns></returns>
        public static void finalizarRequisicao(Requisicao requisicao)
        {
            restaurante.finalizarRequisicao(requisicao);
        }

        /// <summary>
        ///  O usuário digitar a capacidade da mesa, o numero da mesa, 
        /// criar o objeto mesa com esses dados e chamar o restaurante.adicionarMesa(mesa).
        /// </summary>
        public static void adicionarMesa()
        {
            bool ocupada = true;

            Console.WriteLine("A mesa possui capacidade para quantas pessoas?");
            int capacidade;
            do
            {
                capacidade = int.Parse(Console.ReadLine());
                if (capacidade <= 0)
                {
                    Console.WriteLine("A capacidade tem que ser maior que 0. Digite novamente.");
                }
                else break;
            } while (capacidade <= 0);

            Console.WriteLine("Informe o número da mesa:");
            int numero;
            do
            {
                numero = int.Parse(Console.ReadLine());
                if (numero <= 0)
                {
                    Console.WriteLine("O número tem que ser diferente de 0. Digite novamente.");
                }
                else break;
            } while (numero <= 0);

            Mesa mesa = new Mesa(capacidade, numero, ocupada);
            restaurante.adicionarMesa(mesa);
        }
        #endregion

        static void Main(string[] args)
        {
            int opcao;
            Cliente cliente = null;
            do
            {
                Console.WriteLine(menu());
                Console.WriteLine("Digite a opção desejada: ");
                opcao = int.Parse(Console.ReadLine());
                switch (opcao)
                {
                    case 1:
                        cliente = registrarCliente();
                        if(cliente != null)
                            iniciarAtendimento(cliente);
                        else 
                            Console.WriteLine("Cliente não registrado. Informações não preenchidas corretamente!");
                        break;
                    case 2:
                        adicionarMesa();
                        break;
                    case 3:
                        if (cliente != null) finalizarRequisicao(cliente.Requisicao);
                        else Console.WriteLine("Cliente não registrado.Inicie o atendimento antes de finalizar uma requisição!");
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
