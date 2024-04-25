using RestauranteAtomo.model;
using System.Text;
using System.Text.RegularExpressions;

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
            sb.AppendLine("\nMenu");
            sb.AppendLine("1) Novo cliente");
            sb.AppendLine("2) Atender cliente");            
            sb.AppendLine("3) Adicionar mesa ao Restaurante");
            sb.AppendLine("4) Finalizar requisição do cliente");
            sb.AppendLine("0) Sair do programa");

            return sb.ToString();
        }

        /// <summary>
        /// Cria um novo cliente a partir dos dados solicitados.
        /// </summary>
        /// <returns>O cliente cadastrado, caso os dados sejam validos.</returns>
        public static Cliente? registrarCliente(out List<string> erros)
        {
            Console.WriteLine("Informe o nome do cliente: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Informe o contato do cliente (telefone)");
            long contato;

            bool numeroValido = long.TryParse(Console.ReadLine(), out contato);

            erros = new List<string>();

            if(nome.Length < 3)
                erros.Add("Nome inválido ! O nome deve possuir no mínimo 3 caracteres. ");

            if(!numeroValido)
                erros.Add("Contato inválido ! Informe somente os dígitos. ");

            return erros.Count == 0 ? new Cliente(nome, contato.ToString()) : null;
        }

        /// <summary>
        /// Inicia o atendimento a um cliente do restaurante. Solicita a
        /// quantidade de pessoas que vao comer no restaurante e encaminha
        /// para a criacao e atendimento da requisicao.
        /// </summary>
        /// <param name="cliente">O cliente cuja requisicao sera criada e atendida</param>
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
            }while (quantidadePessoas <= 0);
            
            bool atendido = restaurante.atenderCliente(cliente, quantidadePessoas);

            if(atendido)
            {
                Console.WriteLine("Dados da reserva atendida :");
                Console.WriteLine(cliente.ToString());
            }else{
                restaurante.adicionarFilaEspera(cliente.Requisicao);
                Console.WriteLine("A requisição do cliente " + cliente.Nome + " entrou na fila de espera !");
            }
        }

        /// <summary>
        /// Método auxiliar para retornar uma resposta booleana a uma pergunta cuja resposta é somente 'sim' ou 'nao', no caso de iniciar um atendimento
        /// imediatamente após o cadastro de um cliente
        /// </summary>
        private static bool deveIniciarAtendimento(){
            Console.WriteLine("Deseja iniciar o atendimento ? ");
            string pattern = "sim|n[ã|a]o";
            bool respostaValida;
            string resposta;
            do{
                resposta = Console.ReadLine();
                if(Regex.Match(resposta, pattern).Success){
                    respostaValida = true;
                }else{
                    Console.WriteLine("Sua resposta deve ser \'sim\' ou \'não\'");  
                    respostaValida = false;
                }
            }while(!respostaValida);

           return resposta.Contains("sim") ? true : false;
        }

        /// <summary>
        /// inicia a busca de um cliente pelo nome
        /// </summary>
        /// <returns>o primeiro cliente com o nome especificado ou nenhum cliente, caso nao exista</returns>
        private static Cliente? iniciarBuscaCliente()
        {
           Console.WriteLine("Informe o nome do cliente: ");
           string nome = Console.ReadLine();
           return restaurante.encontrarCliente(nome);
        }

        /// <summary>
        /// Método auxiliar que aguarda uma tecla ser digitada
        /// </summary>
        private static void espera()
        {
            Console.WriteLine("\nDigite qualquer tecla para retornar ao menu!");
            Console.ReadKey();
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
            Console.WriteLine("A seguinte requisição foi encerrada: ");
            Console.WriteLine(requisicao);

            Requisicao requisicaoAtendida = restaurante.atenderProximoFilaEspera();
            if(requisicaoAtendida != null)
            {
                Console.WriteLine("A seguinte requisição da liksta de espera foi atendida: ");
                Console.WriteLine(" " + requisicaoAtendida);
            }
        }

        /// <summary>
        ///  O usuário digitar a capacidade da mesa, o numero da mesa, 
        /// criar o objeto mesa com esses dados e chamar o restaurante.adicionarMesa(mesa).
        /// </summary>
        public static void adicionarMesa()
        {
            bool ocupada = false;

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

            Mesa mesa = new Mesa(numero, capacidade, ocupada);
            restaurante.adicionarMesa(mesa);
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
                        List<string> erros;
                        Cliente novoCliente = registrarCliente(out erros);
                        if(novoCliente != null)
                        {
                            restaurante.adicionarCliente(novoCliente);
                            Console.WriteLine(novoCliente);

                            if(restaurante.Mesas.Count > 0 && deveIniciarAtendimento())
                                iniciarAtendimento(novoCliente);
                        }else{ 
                            Console.WriteLine("Cliente não registrado. Informações não preenchidas corretamente:");
                            foreach(string erro in erros)
                                Console.WriteLine(erro + "\n");
                        }
                        espera();
                        break;
                    case 2:
                        if(restaurante.Mesas.Count == 0)
                        {    
                            Console.WriteLine("Não há mesas no restaurante. Utilize a opção 3 para adicionar mesas !");
                        }else{
                            Cliente c = iniciarBuscaCliente();                  
                            if(c != null)
                                iniciarAtendimento(c);
                            else
                                Console.WriteLine("Cliente não encontrado. Favor tentar novamente !\n");
                        }
                        espera();                    
                        break;
                    case 3:
                        adicionarMesa();
                        foreach(Mesa mesa in restaurante.Mesas)
                        {
                            Console.WriteLine(mesa);
                        }
                        break;
                    case 4:
                        iniciarBuscaCliente cliente = iniciarBuscaCliente();
                        if (cliente != null) finalizarRequisicao(cliente.Requisicao);
                        else Console.WriteLine("Cliente não encontrado. Favor tentar novamente! \n");
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
