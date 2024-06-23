using RestauranteAtomo.model;
using System.Text.RegularExpressions;
using System.Text;

namespace RestauranteAtomo
{
    internal class Program
    {

        #region atributo estático
        static Estabelecimento estabelecimento;
        const int RESTAURANTE = 1;
        const int CAFE = 2;
        #endregion
        
        /// <summary>
        /// Método que contém as opcoes do menu de selecao do estabelecimento
        /// </summary>
        public static string menuInicial(){
            StringBuilder stringBuilder= new StringBuilder();
            stringBuilder.AppendLine("Escolha um estabelecimento:");
            stringBuilder.AppendLine("1) Restaurante");
            stringBuilder.AppendLine("2) Café");
            stringBuilder.AppendLine("0) Sair");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Método que permite a escolha do estabelecimento 
        /// para executar as operacoes do sistema
        /// </summary>
        public static void escolherEstabelecimento(){

            int codigo;
            do{
                try{
                    menuInicial();
                    codigo = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch(codigo)
                    {
                       /* case RESTAURANTE: 
                            if(estabelecimento == null){
                                estabelecimento = new Restaurante(codigo, "Restaurante Átomo");
                                mockDados();
                            }
                            executarOperacoesRestaurante();
                            break;*/
                        case CAFE:
                            if(estabelecimento == null){
                              estabelecimento = new Cafe(codigo, "Café Átomo");
                            }
                            executarOperacoesCafe();
                            break;
                        case 0:
                            Console.WriteLine("Adeus!");
                            break;
                        default: 
                            Console.WriteLine("Opção inválida!");
                            break;
                    }
                }catch(FormatException){
                    Console.WriteLine("Opção inválida!");
                    codigo = -1;
                }
            }while(codigo != 0);       
        }

        /// <summary>
        /// Método com dados de teste
        /// </summary>
        private static void mockDados(){
            for (int i = 1; i <= 10; i++) {
                Cliente cliente = new Cliente("Cliente" + i, "8888"+i);
                estabelecimento.adicionarCliente(cliente);
                if(i <= 4){
                    estabelecimento.atenderCliente(cliente, 4);
                }else if(i > 4 && i <= 8){
                    estabelecimento.atenderCliente(cliente, 6);
                }else{
                    estabelecimento.atenderCliente(cliente, 8);
                }
            }
        }

        /// <summary>
        /// método de chamar o atendimento da próxima pessoa da fila de espera
        /// </summary>
        public static void atenderFilaDeEspera(){
            Restaurante restaurante = (Restaurante) estabelecimento;
            
            if(restaurante.listaDeEsperaVazia()){
                Console.WriteLine("Não há requisições na fila de espera!");
                return;
            }
            bool atendida = restaurante.atenderProximoFilaEspera();

            if(atendida){
                Console.WriteLine("A fila de espera foi atualizada!");
                Console.WriteLine(restaurante.exibirListaRequisicoes());
            }else{
                Console.WriteLine("A fila não foi atualizada. Não houve mudanças!");
            }
        }

        #region metodos auxiliares e de execucao dos processos
        /// <summary>
        /// Retorna as opcoes de menu do sistema.
        /// </summary>
        public static string menu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(estabelecimento.ToString());
            sb.AppendLine("\nMenu");
            sb.AppendLine("1) Novo cliente");
            sb.AppendLine("2) Atender cliente");
            sb.AppendLine("3) Adicionar mesa ao Restaurante");
            sb.AppendLine("4) Atender Solicitação de item do cardápio");
            sb.AppendLine("5) Fechar conta");
            
            if(estabelecimento.GetHashCode() == RESTAURANTE){
                sb.AppendLine("6) Atender próximo da fila de espera");
            }

            sb.AppendLine("7) Exibir Lista de Requisições");
            sb.AppendLine("8) Exibir Lista de Mesas");
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

            if (nome.Length < 3)
                erros.Add("Nome inválido ! O nome deve possuir no mínimo 3 caracteres. ");

            if (!numeroValido)
                erros.Add("Contato inválido ! Informe somente os dígitos. ");

            return erros.Count == 0 ? new Cliente(nome, contato.ToString()) : null;
        }

        /// <summary>
        /// Inicia o atendimento a um cliente do estabelecimento. Solicita a
        /// quantidade de pessoas que vao comer no estabelecimento e encaminha
        /// para a criacao e atendimento da requisicao.
        /// </summary>
        /// <param name="cliente">O cliente cuja requisicao sera criada e atendida</param>
        public static void iniciarAtendimento(Cliente cliente, string mensagemAtendimentoNegado)
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
            } while (quantidadePessoas <= 0);

            bool atendido = estabelecimento.atenderCliente(cliente, quantidadePessoas);

            if (atendido)
            {
                Requisicao requisicao = estabelecimento.findRequisicaoAtendidaCliente(cliente);
                if (requisicao != null)
                {
                    Console.WriteLine("Requisição atendida :");
                    Console.WriteLine(requisicao.ToString());
                }
            }else{
                Console.WriteLine(mensagemAtendimentoNegado);
            }
        }

        /// <summary>
        /// inicia a busca de um cliente pelo nome
        /// </summary>
        /// <returns>o primeiro cliente com o nome especificado ou nenhum cliente, caso nao exista</returns>
        private static Cliente? iniciarBuscaCliente()
        {
            Console.WriteLine("Informe o id do cliente: ");
            int idCliente = int.Parse(Console.ReadLine());

            return estabelecimento.localizarCliente(idCliente);
        }

        /// <summary>
        /// Método auxiliar que aguarda uma tecla ser digitada
        /// </summary>
        private static void espera()
        {
            Console.WriteLine("\nDigite qualquer tecla para retornar ao menu!");
            Console.ReadKey();
            Console.Clear();
        }

        // Hayanne
        /// <summary>
        /// Chama o método finalizarRequisição() da classe Rstaurante.
        /// </summary>
        /// <param name="requisicao">Recebe um cliente como parametro, cuja requisição será finalizada</param>
        /// <returns></returns>
        public static void finalizarRequisicao(Requisicao requisicao)
        {
            if (requisicao != null)
            {
                estabelecimento.finalizarRequisicao(requisicao);
                Console.WriteLine("===========Requisição finalizada===========");
                Console.WriteLine($"{requisicao.Mesa.ToString()} \nStatus: Mesa Liberada");
            }
        }

        /// <summary>
        ///  O usuário digitar a capacidade da mesa, o numero da mesa, 
        /// criar o objeto mesa com esses dados e chamar o estabelecimento.adicionarMesa(mesa).
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
            } while (capacidade <= 0);

            Console.WriteLine("Informe o número da mesa:");
            int numero;
            List<Mesa> mesasExistentes = new List<Mesa>();
            do
            {
                numero = int.Parse(Console.ReadLine());
                mesasExistentes = estabelecimento.Mesas.Where((m) => m.Numero == numero).ToList();
                if (numero <= 0 || mesasExistentes.Count > 0)
                {
                    Console.WriteLine("O número tem que ser diferente de 0 ou o número da mesa já existe. Digite novamente.");
                }
                else break;
            } while (numero <= 0 || mesasExistentes.Count > 0);

            Mesa mesa = new Mesa(numero, capacidade, ocupada);
            bool adicionada = estabelecimento.adicionarMesa(mesa);
            
            if(adicionada){
                Console.WriteLine("Mesa adicionada!");
                Console.WriteLine(estabelecimento.exibirMesas());
            }
            else
                Console.WriteLine("Mesa não adicionada! O estabelecimento já possui o limite de mesas!");
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliente"></param>
        public static void atenderSolicitacaoItem(Cliente cliente)
        {
            Requisicao requisicaoAtual = estabelecimento.findRequisicaoAtendidaCliente(cliente);
            if (requisicaoAtual != null)   {
                Console.WriteLine(estabelecimento.ExibirCardapio());
                
                Console.WriteLine("Informe o código do item solicitado: ");
                int codigoEscolhido = int.Parse(Console.ReadLine());
         
                Produto produtoAdcionado = estabelecimento.AtenderSolicitacaoItem(codigoEscolhido, requisicaoAtual);

                if(produtoAdcionado != null){
                    Console.WriteLine("O produto abaixo foi adicionado à requisição do cliente:\n" + produtoAdcionado.ToString() + "\n"
                     + cliente.ToString());
                }else{
                    Console.WriteLine("Produto não encontrado!");
                }
            }else{
                Console.WriteLine("O cliente não possui requisição ativa atualmente!");
            }
        }

        public static void exibirConta(Cliente cliente)
        {
            Requisicao requisicao = estabelecimento.findRequisicaoAtendidaCliente(cliente);

            if(requisicao != null){
                finalizarRequisicao(requisicao);
                Console.WriteLine(requisicao.resumoPedido());
                requisicao.fecharConta();
            }else{
                 Console.WriteLine("O cliente não possui requisições passíveis de finalização");
            }
        }
        
        public static void executarCadastro(Cliente novoCliente, List<string> erros){

            if (novoCliente != null)
            {
               bool adicionado = estabelecimento.adicionarCliente(novoCliente);
              
               if(adicionado){
                  Console.WriteLine(novoCliente);
               }else{
                    Console.WriteLine("Cliente não adicionado. Tente novamente !");
               }
            }
            else
            {
               foreach (string erro in erros)
                      Console.WriteLine(erro + "\n");
            }
           espera();
        }

        public static void executarOperacoesRestaurante(){
            Restaurante restaurante = (Restaurante) estabelecimento;
            int opcao;
            do
            {
                Console.WriteLine(menu());
                Console.WriteLine("Digite a opção desejada: ");
                bool valido = false;
                do{
                    valido = int.TryParse(Console.ReadLine(), out opcao);
                    if(!valido) Console.WriteLine("Digite novamente !");
                }while(!valido);
                Console.Clear();

                switch (opcao)
                {
                    case 1:
                        List<string> erros;
                        Cliente novoCliente = registrarCliente(out erros);
                        executarCadastro(novoCliente, erros);
                        break;
                    case 2:
                        Cliente c = iniciarBuscaCliente();
                        if (c != null && restaurante.findRequisicaoAtendidaCliente(c) == null && restaurante.findRequisicaoNaoAtendidaCliente(c) == null)
                            iniciarAtendimento(c, "Requsição não atendida. O cliente entrou na fila de espera!");
                        else
                        {
                            string mensagem = "Cliente não encontrado ou cliente possui requisição pendente.\n";
                            mensagem += "Para cadastrar nova requisição, a requisição existente deve ser finalizada primeiro!";
                            Console.WriteLine(mensagem);
                        }
                        espera();
                        break;
                    case 3:
                        adicionarMesa();
                        espera();
                        break;
                    case 4:
                        Cliente c2 = iniciarBuscaCliente();
                        if (c2 != null)
                        {
                            atenderSolicitacaoItem(c2);
                        }
                        else Console.WriteLine("Cliente não encontrado ou o cliente não possui requisição ativa. Favor tentar novamente! \n");
                        espera();
                        break;
                    case 5:
                        Cliente c3 = iniciarBuscaCliente();
                        if (c3 != null)
                        {
                            exibirConta(c3);
                        }
                        else Console.WriteLine("Cliente não encontrado ou o cliente não possui requisição ativa. Favor tentar novamente! \n");
                        espera();
                        break;
                    case 6:
                        atenderFilaDeEspera();
                        espera();
                        break;
                    case 7:
                        Console.WriteLine(estabelecimento.exibirListaRequisicoes());
                        espera();
                        break;
                    case 8:
                        Console.WriteLine("divider");
                        Console.WriteLine(estabelecimento.exibirMesas());
                        espera();
                        break;
                    case 0:
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            } while (opcao != 0);
        }

        public static void executarOperacoesCafe(){
            Cafe cafe = (Cafe)estabelecimento;
            int opcao;
            do
            {
                Console.WriteLine(menu());
                Console.WriteLine("Digite a opção desejada: ");
                bool valido = false;
                do
                {
                    valido = int.TryParse(Console.ReadLine(), out opcao);
                    if (!valido) Console.WriteLine("Digite novamente !");
                } while (!valido);
                Console.Clear();

                switch (opcao)
                {
                    case 1:
                        List<string> erros;
                        Cliente novoCliente = registrarCliente(out erros);
                        executarCadastro(novoCliente, erros);
                        break;
                    case 2:
                        Cliente c = iniciarBuscaCliente();
                        if (c != null && cafe.findRequisicaoAtendidaCliente(c) == null)
                            iniciarAtendimento(c, "O estabelecimento está lotado. Peça para o cliente voltar em outro momento!");
                        else
                        {
                            string mensagem = "Cliente não encontrado ou cliente possui requisição pendente.\n";
                            mensagem += "Para cadastrar nova requisição, a requisição existente deve ser finalizada primeiro!";
                            Console.WriteLine(mensagem);
                        }
                        espera();
                        break;
                    case 3:
                        adicionarMesa();
                        espera();
                        break;
                    case 4:
                        Cliente c2 = iniciarBuscaCliente();
                        if (c2 != null)
                        {
                            atenderSolicitacaoItem(c2);
                        }
                        else Console.WriteLine("Cliente não encontrado ou o cliente não possui requisição ativa. Favor tentar novamente! \n");
                        espera();
                        break;
                    case 5:
                        Cliente c3 = iniciarBuscaCliente();
                        if (c3 != null)
                        {
                            exibirConta(c3);
                        }
                        else Console.WriteLine("Cliente não encontrado ou o cliente não possui requisição ativa. Favor tentar novamente! \n");
                        espera();
                        break;
                    case 7:
                        Console.WriteLine(estabelecimento.exibirListaRequisicoes());
                        espera();
                        break;
                    case 8:
                        Console.WriteLine(estabelecimento.exibirMesas());
                        espera();
                        break;
                    case 0:
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            } while (opcao != 0);
        }

        static void Main(string[] args)
        {
            Console.Clear();
            escolherEstabelecimento();    
        }
    }
}