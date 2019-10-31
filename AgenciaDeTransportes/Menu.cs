using System;
using AgenciaDeTransportes.Entities;

namespace AgenciaDeTransportes
{
    public class Menu
    {
        internal static void MenuInicial(Agencia agencia)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"AGÊNCIA: {agencia.NomeAgencia.ToUpper()}");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("SELECIONE UMA OPÇÃO DE [1] A [2]\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("[1] - VEÍCULO");
                Console.WriteLine("[2] - VIAGEM");
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
                        MenuVeiculo(agencia);
                        break;
                    case ConsoleKey.NumPad2:
                        MenuViagem(agencia);
                        break;
                    default:
                        Console.Write("\nVALOR INVÁLIDO, PRESSIONE QUALQUER TECLA PARA REINICIAR");
                        Console.ReadKey();
                        Console.Clear();
                        MenuInicial(agencia);
                        break;
                }
            };
        }
        internal static void MenuVeiculo(Agencia agencia)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"AGÊNCIA: {agencia.NomeAgencia.ToUpper()}");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("SELECIONE UMA OPÇÃO DE [1] A [4]\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("[1] - CADASTRAR VEÍCULO");
                Console.WriteLine("[2] - ABASTECER VEÍCULO");
                Console.WriteLine("[3] - DIRIGIR");
                Console.WriteLine("[4] - EXIBIR INFORMAÇÕES");
                Console.WriteLine("[ESC] - VOLTAR AO MENU INICIAL");

                ConsoleKeyInfo tecla = Console.ReadKey();
                Veiculo veiculo;

                switch (tecla.Key)
                {
                    case ConsoleKey.Escape:
                        MenuInicial(agencia);
                        break;
                    case ConsoleKey.NumPad1:
                        agencia.CadastrarVeiculo();
                        Console.Write("\nPRESSIONE QUALQUER TECLA PARA VOLTAR..");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.NumPad2:
                        if (agencia.Veiculos.Count > 0)
                        {
                            try
                            {
                                Console.Write("\nESCOLHA UM VEÍCULO PARA ABASTECER!");
                                agencia.ExibirVeiculos();
                                Console.Write("\nDIGITE O NÚMERO DA PLACA DO VEÍCULO PARA ABASTECER: ");
                                string placa = ControleDeInputs.ValidarPlaca(Console.ReadLine().ToUpper());
                                veiculo = agencia.Veiculos.Find(x => x.Placa == placa);
                                veiculo.Abastecer();
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
                            try
                            {
                                Console.Write("\nESCOLHA UM VEÍCULO PARA DIRIGIR!");
                                agencia.ExibirVeiculos();
                                Console.Write("\nDIGITE O NÚMERO DA PLACA DO VEÍCULO PARA DIRIGIR: ");
                                string placa = ControleDeInputs.ValidarPlaca(Console.ReadLine().ToUpper());
                                veiculo = agencia.Veiculos.Find(x => x.Placa == placa);

                                if (agencia.Viagens.Count > 0)
                                {
                                    Console.Write("\nESCOLHA UMA VIAGEM PARA DIRIGIR!");
                                    agencia.ExibirViagens();
                                    Console.Write("\nDIGITE O CÓDIGO DA VIAGEM QUE DESEJA DIRIGIR: ");
                                    int codigo = ControleDeInputs.ValidarNumeros(Console.ReadLine());
                                    Viagem viagem = agencia.Viagens.Find(x => x.CodigoViagem == codigo);
                                    viagem.AdicionarVeiculo(veiculo);
                                    viagem.AtualizarDistancia(veiculo.Percorrer(viagem.Distancia, viagem.Clima));
                                }
                                else Console.WriteLine("\nNÃO HÁ VIAGENS, CADASTRE UMA PARA DIRIGIR!");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"ERRO INESPERADO! - {e.Message}");
                            }
                        }
                        else Console.WriteLine("\nNÃO HÁ VEÍCULOS, CADASTRE UM PARA DIRIGIR!");

                        Console.Write("\nPRESSIONE QUALQUER TECLA PARA VOLTAR..");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.NumPad4:
                        agencia.ExibirVeiculos();
                        Console.Write("\nPRESSIONE QUALQUER TECLA PARA VOLTAR..");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\nPRESSIONE QUALQUER TECLA PARA VOLTAR..");
                        Console.ReadKey();
                        MenuVeiculo(agencia);
                        break;
                }
            }
        }
        internal static void MenuViagem(Agencia agencia)
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
                Console.WriteLine("[2] - EXIBIR INFORMAÇÕES");
                Console.WriteLine("[ESC] - VOLTAR AO MENU INICIAL");

                ConsoleKeyInfo tecla = Console.ReadKey();

                switch (tecla.Key)
                {
                    case ConsoleKey.Escape:
                        MenuInicial(agencia);
                        break;
                    case ConsoleKey.NumPad1:
                        agencia.CadastrarViagem();
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
                        MenuViagem(agencia);
                        break;
                }
            }
        }
    }
}
