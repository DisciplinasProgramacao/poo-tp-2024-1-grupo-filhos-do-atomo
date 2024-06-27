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
                    Console.WriteLine(menuInicial());
                    codigo = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch(codigo)
                    {
                        case RESTAURANTE: 
                            estabelecimento = new Restaurante(codigo, "Restaurante Átomo");
                            mockDados();
                            executarOperacoesRestaurante();
                            break;
                        case CAFE:
                            estabelecimento = new Cafe(codigo, "Café Átomo");
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
                    Console.Clear();
                    Console.WriteLine("Opção inválida!");
                    codigo = -1;
                }
            }while(codigo != 0);       
        }

        /// <summary>
        /// Método com dados de teste
        /// </summary>
        private static void mockDados(){
            for (int i = 1; i <= 6; i++) {
                Cliente cliente = new Cliente("Cliente" + i, "8888"+i);
                estabelecimento.adicionarCliente(cliente);
                if(i <= 4){
                    estabelecimento.atenderCliente(cliente, 4);
                }else if(i > 4 && i <= 5){
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
            
            if(restaurante.isListaDeEsperaVazia()){
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
            int index = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(estabelecimento.ToString());
            sb.AppendLine("\nMenu");
            sb.AppendLine(++index+") Novo cliente");
            sb.AppendLine(++index+") Atender cliente");
            sb.AppendLine(++index+") Listar clientes");
            sb.AppendLine(++index+") Adicionar mesa ao Restaurante");
            sb.AppendLine(++index + ") Atender Solicitação de item do cardápio");
            sb.AppendLine(++index + ") Fechar conta");
            
            if(estabelecimento.GetHashCode() == RESTAURANTE){
                sb.AppendLine(++index + ") Atender próximo da fila de espera");
            }

            sb.AppendLine(++index + ") Exibir Lista de Requisições");
            sb.AppendLine(++index + ") Exibir Lista de Mesas");
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
        public static void iniciarAtendimento(Cliente cliente)
        {
            Console.WriteLine("Reserva para quantas pessoas ?");
            int quantidadePessoas;
            bool convertido;
            do
            {
                convertido = int.TryParse(Console.ReadLine(), out quantidadePessoas);
                if (quantidadePessoas <= 0 || !convertido)
                {
                    Console.WriteLine("Quantidade de pessoas deve ser maior que 0 ou não é válida. Digite novamente");
                }
            } while (quantidadePessoas <= 0 || !convertido);

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
                Console.WriteLine(estabelecimento.mensagemAtendimentoNegado());
            }
        }

        /// <summary>
        /// inicia a busca de um cliente pelo nome
        /// </summary>
        /// <returns>o primeiro cliente com o nome especificado ou nenhum cliente, caso nao exista</returns>
        private static Cliente? iniciarBuscaCliente()
        {
            Console.WriteLine("Informe o id do cliente: ");
            bool convertido;
            int idCliente;
            do
            {
                convertido = int.TryParse(Console.ReadLine(), out idCliente);
                if(!convertido){
                    Console.WriteLine("Não é um código válido!");
                }
            }while(!convertido);

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
            bool convertido;
            do
            {
                convertido = int.TryParse(Console.ReadLine(), out capacidade);
                if (capacidade <= 0 || !convertido)
                {
                    Console.WriteLine("Capacidade inválida. Digite novamente.");
                }
                else break;
            } while (capacidade <= 0 || !convertido);

            Console.WriteLine("Informe o número da mesa:");
            int numero;
            List<Mesa> mesasExistentes = new List<Mesa>();
            do
            {
                convertido = int.TryParse(Console.ReadLine(), out numero);
                mesasExistentes = estabelecimento.Mesas.Where((m) => m.Numero == numero).ToList();
                if (numero <= 0 || mesasExistentes.Count > 0 || !convertido)
                {
                    Console.WriteLine("O número tem que ser diferente de 0 ou o número da mesa já existe. Digite novamente.");
                }
                else break;
            } while (numero <= 0 || mesasExistentes.Count > 0 || !convertido);

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
                
                bool convertido;
                int codigoEscolhido;
                do{
                    convertido = int.TryParse(Console.ReadLine(), out codigoEscolhido);
                    if(!convertido){
                        Console.WriteLine("Código inválido. Digite novamente");
                    }
                }while(!convertido);
            
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
                 Console.WriteLine("O cliente não possui requisições passíveis de finalização!");
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
                        if (c != null)
                        {
                            if (estabelecimento.findRequisicaoAtendidaCliente(c) == null && ((Restaurante) estabelecimento).findRequisicaoNaoAtendidaCliente(c) == null)
                                iniciarAtendimento(c);
                            else
                                Console.WriteLine("Cliente possui requisição pendente ! Para cadastrar nova requisição, a requisição existente deve ser finalizada primeiro! ");
                        }
                        else
                        {
                            Console.WriteLine("Cliente não encontrado");
                        }
                        espera();
                        break;
                    case 3:
                        Console.WriteLine(estabelecimento.listarClientes());
                        espera();
                        break;
                    case 4:
                        adicionarMesa();
                        espera();
                        break;
                    case 5:
                        Cliente c2 = iniciarBuscaCliente();
                        if (c2 != null)
                        {
                            atenderSolicitacaoItem(c2);
                        }
                        else Console.WriteLine("Cliente não encontrado! \n");
                        espera();
                        break;
                    case 6:
                        Cliente c3 = iniciarBuscaCliente();
                        if (c3 != null)
                        {
                            exibirConta(c3);
                        }
                        else Console.WriteLine("Cliente não encontrado! \n");
                        espera();
                        break;
                    case 7:
                        atenderFilaDeEspera();
                        espera();
                        break;
                    case 8:
                        Console.WriteLine(estabelecimento.exibirListaRequisicoes());
                        espera();
                        break;
                    case 9:
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
                        if (c != null)
                        {
                            if (estabelecimento.findRequisicaoAtendidaCliente(c) == null)
                                iniciarAtendimento(c);
                            else
                                Console.WriteLine("Cliente possui requisição pendente ! Para cadastrar nova requisição, a requisição existente deve ser finalizada primeiro! ");
                        }
                        else
                        {
                            Console.WriteLine("Cliente não encontrado");
                        }
                        espera();
                        break;
                    case 3:
                        Console.WriteLine(estabelecimento.listarClientes());
                        espera();
                        break;
                    case 4:
                        adicionarMesa();
                        espera();
                        break;
                    case 5:
                        Cliente c2 = iniciarBuscaCliente();
                        if (c2 != null)
                        {
                            atenderSolicitacaoItem(c2);
                        }
                        else Console.WriteLine("Cliente não encontrado!\n");
                        espera();
                        break;
                    case 6:
                        Cliente c3 = iniciarBuscaCliente();
                        if (c3 != null)
                        {
                            exibirConta(c3);
                        }
                        else Console.WriteLine("Cliente não encontrado!\n");
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