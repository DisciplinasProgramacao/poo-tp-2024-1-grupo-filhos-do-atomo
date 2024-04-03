using RestauranteAtomo.model;
using System.Text;

namespace RestauranteAtomo
{
    class Program
    {
        static Restaurante restaurante = new Restaurante(1);

        public static String menu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Menu");
            sb.AppendLine("1) Atender novo cliente");
            sb.AppendLine("4) Sair do programa");

            return sb.ToString();
        }

        public static Cliente registrarCliente()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Informe o nome do cliente: ");
            cliente.Nome = Console.ReadLine();

            Console.WriteLine("Informe o contato do cliente (telefone)");
            cliente.Contato = Console.ReadLine();

            return cliente;
        }

        public static void atenderRequisicaoCliente(Cliente c)
        {
            Console.WriteLine("Reserva para quantas pessoas ?");
            int quantidadePessoas = int.Parse(Console.ReadLine());
            c.fazerRequisicao(quantidadePessoas);
            restaurante.atenderCliente(c, quantidadePessoas);
        }

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
                        atenderRequisicaoCliente(cliente);
                        break;
                    case 2:
                        break;
                    case 4: 
                        break;
                    default: 
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            } while (opcao != 4);
        }
    }
}
