using System;
using AgenciaDeTransportes.Entities;
using System.Linq;
using static AgenciaDeTransportes.Servicos.ControleDeInputs;
using AgenciaDeTransportes.Database;

namespace AgenciaDeTransportes.Servicos
{
    public class Menu
    {
        internal static void MenuInicial(Agencia agencia, JsonBase jsonBase)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"AGÊNCIA: {agencia.NomeAgencia.ToUpper()}");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("SELECIONE UMA OPÇÃO DE [1] A [4]\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("[1] - VEÍCULOS");
                Console.WriteLine("[2] - VIAGENS");
                Console.WriteLine("[3] - VIAJAR");
                Console.WriteLine("[4] - RELATÓRIOS");
                Console.WriteLine("[ESC] - SAIR");

                ConsoleKeyInfo tecla = Console.ReadKey();

                switch (tecla.Key)
                {
                    case ConsoleKey.Escape:
                        Console.WriteLine("\nOOOBRIGADO! PRESSIONE ENTER PARA SAIR..");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.NumPad1:
                        MenuVeiculo(agencia, jsonBase);
                        break;
                    case ConsoleKey.NumPad2:
                        MenuViagem(agencia, jsonBase);
                        break;
                    case ConsoleKey.NumPad3:
                        MenuViajar(agencia, jsonBase);
                        break;
                    case ConsoleKey.NumPad4:
                        Console.Write("\nESCOLHA UMA VIAGEM PARA EXIBIR O RELATÓRIO!\n");
                        agencia.VeiculoViagens.ForEach(x => Console.WriteLine(x));
                        Console.Write("\nPARA EXIBIR O RELATÓRIO DE UMA VIAGEM, DIGITE O ID DA VIAGEM: ");
                        int id = ValidarNumeros(Console.ReadLine());
                        Relatorio relatorio = agencia.Relatorios.Where(x => x.VeiculoViagem.IdViagem == id).FirstOrDefault();
                        Console.Clear();
                        if (relatorio != null)
                        {
                            if (agencia.Relatorios.Count(x => x.VeiculoViagem.IdViagem == id) > 1)
                            {
                                Console.WriteLine("A VIAGEM FOI INTERROMPIDA DURANTE O PERCURSO, HÁ MAIS DE UM RELATÓRIO!");
                                foreach (Relatorio x in agencia.Relatorios)
                                    if (x.VeiculoViagem.IdViagem == id)
                                        Console.WriteLine(x);
                            }
                            else Console.WriteLine(relatorio);
                        }
                        else Console.WriteLine("NENHUMA VIAGEM FOI REALIZADA AINDA. NÃO HÁ RELATÓRIOS!");
                        Console.Write("\nPRESSIONE QUALQUER TECLA PARA VOLTAR..");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\nVALOR INVÁLIDO, PRESSIONE QUALQUER TECLA PARA REINICIAR");
                        Console.ReadKey();
                        Console.Clear();
                        MenuInicial(agencia, jsonBase);
                        break;
                }
            };
        }
        internal static void MenuVeiculo(Agencia agencia, JsonBase jsonBase)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"AGÊNCIA: {agencia.NomeAgencia.ToUpper()}");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("SELECIONE UMA OPÇÃO DE [1] A [3]\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("[1] - CADASTRAR VEÍCULO");
                Console.WriteLine("[2] - ABASTECER VEÍCULO");
                Console.WriteLine("[3] - LISTAR");
                Console.WriteLine("[ESC] - VOLTAR AO MENU INICIAL");

                ConsoleKeyInfo tecla = Console.ReadKey();
                Veiculo veiculo;

                switch (tecla.Key)
                {
                    case ConsoleKey.Escape:
                        MenuInicial(agencia, jsonBase);
                        break;
                    case ConsoleKey.NumPad1:
                        agencia.CadastrarVeiculo();
                        jsonBase.Salvar(agencia);
                        Console.Write("\nPRESSIONE QUALQUER TECLA PARA VOLTAR..");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.NumPad2:
                        if (agencia.Veiculos.Count > 0)
                        {
                            try
                            {
                                Console.Clear();
                                Console.Write("\nESCOLHA UM VEÍCULO PARA ABASTECER!\n");
                                agencia.ExibirVeiculos();
                                Console.Write("\nDIGITE O NÚMERO DA PLACA DO VEÍCULO PARA ABASTECER: ");
                                string placa = ValidarPlaca(Console.ReadLine().ToUpper());
                                veiculo = agencia.Veiculos.Find(x => x.Placa == placa);
                                veiculo.Abastecer();
                                jsonBase.Salvar(agencia);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"ERRO INESPERADO! - {e.Message}");
                            }
                        }
                        else Console.WriteLine("\nNÃO HÁ VEÍCULOS CADASTRADOS!");

                        Console.Write("\nPRESSIONE QUALQUER TECLA PARA VOLTAR..");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.NumPad3:
                        if (agencia.Veiculos.Count > 0)
                        {
                            agencia.ExibirVeiculos();
                            Console.Write("\nDESEJA EXIBIR ALGUM VEÍCULO ESPECÍFICO? (S/N): ");
                            string opcao = ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                            if (opcao == "S")
                                agencia.ExibirVeiculoDetalhado();
                        }
                        else Console.WriteLine("NÃO HÁ VEÍCULOS CADASTRADOS!");
                        Console.Write("\nPRESSIONE QUALQUER TECLA PARA VOLTAR..");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\nPRESSIONE QUALQUER TECLA PARA VOLTAR..");
                        Console.ReadKey();
                        MenuVeiculo(agencia, jsonBase);
                        break;
                }
            }
        }
        internal static void MenuViagem(Agencia agencia, JsonBase jsonBase)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"AGÊNCIA: {agencia.NomeAgencia.ToUpper()}");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("SELECIONE UMA OPÇÃO DE [1] A [2]\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("[1] - CADASTRAR VIAGEM");
                Console.WriteLine("[2] - LISTAR");
                Console.WriteLine("[ESC] - VOLTAR AO MENU INICIAL");

                ConsoleKeyInfo tecla = Console.ReadKey();

                switch (tecla.Key)
                {
                    case ConsoleKey.Escape:
                        MenuInicial(agencia, jsonBase);
                        break;
                    case ConsoleKey.NumPad1:
                        agencia.CadastrarViagem();
                        jsonBase.Salvar(agencia);
                        Console.Write("\nPRESSIONE QUALQUER TECLA PARA VOLTAR..");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.NumPad2:
                        agencia.ExibirViagens();
                        Console.Write("\nPRESSIONE QUALQUER TECLA PARA VOLTAR..");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\nPRESSIONE QUALQUER TECLA PARA VOLTAR..");
                        Console.ReadKey();
                        MenuViagem(agencia, jsonBase);
                        break;
                }
            }
        }
        internal static void MenuViajar(Agencia agencia, JsonBase jsonBase)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"AGÊNCIA: {agencia.NomeAgencia.ToUpper()}");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("SELECIONE UMA OPÇÃO DE [1] A [2]\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("[1] - PROGRAMAR VIAGEM");
                Console.WriteLine("[2] - EXECUTAR VIAGEM");
                Console.WriteLine("[ESC] - VOLTAR AO MENU INICIAL");

                ConsoleKeyInfo tecla = Console.ReadKey();

                switch (tecla.Key)
                {
                    case ConsoleKey.Escape:
                        MenuInicial(agencia, jsonBase);
                        break;
                    case ConsoleKey.NumPad1:
                        agencia.ProgramarViagem();
                        jsonBase.Salvar(agencia);
                        Console.Write("\nPRESSIONE QUALQUER TECLA PARA VOLTAR..");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.NumPad2:
                        agencia.ExecutarViagem();
                        jsonBase.Salvar(agencia);
                        Console.Write("\nPRESSIONE QUALQUER TECLA PARA VOLTAR..");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\nPRESSIONE QUALQUER TECLA PARA VOLTAR..");
                        Console.ReadKey();
                        MenuViajar(agencia, jsonBase);
                        break;
                }
            };
        }
    }
}
