﻿using RestauranteAtomo.model;
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
