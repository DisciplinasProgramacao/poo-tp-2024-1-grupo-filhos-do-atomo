using RestauranteAtomo.model;
using System.Text;
using System.Text.RegularExpressions;

namespace RestauranteAtomo
{
    internal class Program
    {
        #region atributo estático
        static Restaurante restaurante = new Restaurante(1);
        static List<Cliente> clientes = new List<Cliente>();
        #endregion

        #region metodos auxiliares e de execucao dos processos
        /// <summary>
        /// Retorna as opcoes de menu do sistema.
        /// </summary>
        public static string menu()
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
                Requisicao requisicao = restaurante.findRequisicaoAtendidaCliente(cliente);
                if(requisicao != null){
                    Console.WriteLine("Requisição atendida :");
                    Console.WriteLine(cliente.ToString() + " - " + requisicao.ToString());
                }
            }else{
                Console.WriteLine("A requisição do cliente " + cliente.Nome + " entrou na fila de espera !");
            }
        }

        /// <summary>
        /// Método auxiliar para retornar uma resposta booleana a uma pergunta cuja resposta é somente 'sim' ou 'nao', no caso de iniciar um atendimento
        /// imediatamente após o cadastro de um cliente
        /// </summary>
        private static bool deveIniciarAtendimento(){
            Console.WriteLine("Deseja iniciar o atendimento ? Responda sim ou nao ");
            string pattern = "sim|nao";
            bool respostaValida;
            string resposta;
            do{
                resposta = Console.ReadLine();
                if(Regex.Match(resposta, pattern).Success){
                    respostaValida = true;
                }else{
                    Console.WriteLine("Sua resposta deve ser \'sim\' ou \'nao\'");  
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
           return clientes.Find(cliente => cliente.Nome.ToLower() == nome.ToLower());
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
        /// <param name="requisicao">Recebe um cliente como parametro, cuja requisição será finalizada</param>
        /// <returns></returns>
        public static void finalizarRequisicao(Cliente cliente)
        {
            Requisicao requisicao = restaurante.findRequisicaoAtendidaCliente(cliente);
            
            if(requisicao != null){

                restaurante.finalizarRequisicao(requisicao);
                Console.WriteLine("Requisição finalizada: Mesa : " + requisicao.Mesa.Numero 
                + " de capacidade " + requisicao.Mesa.Capacidade + " liberada.");
                bool atendida = restaurante.atenderProximoFilaEspera();

                if(atendida)
                {
                    Console.WriteLine("Lista de espera atualizada! ");
                    Console.WriteLine(restaurante.exibirListaRequisicoes());
                }
            }else{
                Console.WriteLine("O cliente " + cliente.ToString() + " não possui requisições passíveis de finalização");
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
            int capacidade = 0;
            do
            {
                capacidade = int.Parse(Console.ReadLine());
                if (capacidade <= 0)
                {
                    Console.WriteLine("Capacidade inválida. Digite novamente.");
                }
                else break;
            } while (capacidade <= 0 );

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
            Console.Clear();
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
                            clientes.Add(novoCliente);
                            Console.WriteLine(novoCliente);

                            if(restaurante.Mesas.Count > 0 && deveIniciarAtendimento())
                                iniciarAtendimento(novoCliente);
                        }else{ 
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
                            if(c != null && restaurante.findRequisicaoAtendidaCliente(c) == null && restaurante.findRequisicaoNaoAtendidaCliente(c) == null)
                                iniciarAtendimento(c);
                            else{
                                string mensagem = "Cliente não ou encontrado ou cliente já possui requisição pendente.\n";
                                mensagem += "Para cadastrar nova requisição, a requisição existente deve ser finalizada primeiro !";
                                Console.WriteLine(mensagem);
                            }
                        }
                        espera();                    
                        break;
                    case 3:
                        adicionarMesa();
                        Console.WriteLine(restaurante.exibirMesas());
                        espera();
                        break;
                    case 4:
                        Cliente cliente = iniciarBuscaCliente();
                        if(cliente != null)
                            finalizarRequisicao(cliente);
                        else Console.WriteLine("Cliente não encontrado. Favor tentar novamente! \n");
                        espera();
                        break;
                    case 0:
                        break;
                    default: 
                        Console.WriteLine("Opção inválida!\n");
                        break;
                }
                Console.Clear();
            } while (opcao != 0);
        }
    }
}
