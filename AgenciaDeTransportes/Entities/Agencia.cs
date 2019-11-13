using System;
using System.Collections.Generic;
using System.Linq;
using static AgenciaDeTransportes.Servicos.ControleDeInputs;

namespace AgenciaDeTransportes.Entities
{
    public class Agencia
    {
        public string NomeAgencia { get; private set; }
        public List<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
        public List<Viagem> Viagens { get; set; } = new List<Viagem>();
        public List<VeiculoViagem> VeiculoViagens { get; set; } = new List<VeiculoViagem>();
        public List<Relatorio> Relatorios { get; set; } = new List<Relatorio>();

        public Agencia(string nomeAgencia) => NomeAgencia = nomeAgencia;

        public void CadastrarVeiculo() // CADASTRA UM VEÍCULO E INSERI NA LISTA
        {
            
            Console.Write("\nQUANTOS VEÍCULOS GOSTARIA DE CADASTRAR? ");
            int quant = ValidarNumeros(Console.ReadLine());

            for (int i = 1; i <= quant; i++)
            {
                Console.Clear();
                Console.Write($"\n--- ESTAMOS CADASTRANDO O {i}º VEÍCULO ---");
                Console.Write($"\nDIGITE A MARCA DO VEÍCULO: ");
                string marca = ValidarLetrasComEspaco(Console.ReadLine().ToUpper());
                Console.Write("\nDIGITE O MODELO DO VEÍCULO: ");
                string modelo = ValidarModelos(Console.ReadLine().ToUpper());
                Console.Write("\nDIGITE A PLACA DO VEÍCULO: ");
                string placa = ValidarPlaca(Console.ReadLine().ToUpper());
                Console.Write("\nDIGITE O ANO DO VEÍCULO: ");
                DateTime ano = ValidarAno(Console.ReadLine());
                Console.Write("\nDIGITE A VELOCIDADE MÉDIA DO VEÍCULO (KM/H): ");
                double velocidadeMaxima = ValidarPontoFlutuante(Console.ReadLine());
                Console.Write("\nDIGITE A CAPACIDADE DO TANQUE: ");
                double capacidadeMaxima = ValidarPontoFlutuante(Console.ReadLine());
                Console.Clear();
                Console.Write("\nCOMO ESTA A SITUAÇÃO DOS PNEUS?\n\nMURCHO[1]\nMODERADO[2]\nCHEIO[3]\n\nESCOLHA UMA OPÇÃO: ");
                int statusPneu = ValidarNumeros1A3(Console.ReadLine());

                Console.Write("\nQUAL O TIPO DO COMBUSTÍVEL?\n\nFLEX[1]\nALCOOL[2]\nGASOLINA[3]\n\nESCOLHA UMA OPÇÃO: ");
                int opcao = ValidarNumeros1A3(Console.ReadLine());

                double autonomiaGasolina, autonomiaAlcool;

                if (opcao == 1) // INSTANCIA UM CARRO FLEX
                {
                    Console.Write("\nDIGITE QUANTOS KM O VEÍCULO FAZ POR LITRO DE GASOLINA: ");
                    autonomiaGasolina = ValidarPontoFlutuante(Console.ReadLine());
                    Console.Write("\nDIGITE QUANTOS KM O VEÍCULO FAZ POR LITRO DE ALCOOL: ");
                    autonomiaAlcool = ValidarPontoFlutuante(Console.ReadLine());
                    Veiculos.Add(new CarroFlex(marca, modelo, placa, ano, velocidadeMaxima, capacidadeMaxima, statusPneu, autonomiaGasolina, autonomiaAlcool));
                    Console.WriteLine("\nVEÍCULO CADASTRADO COM SUCESSO!");
                }
                else if (opcao == 2) // INSTANCIA UM CARRO A ALCOOL
                {
                    Console.Write("\nDIGITE QUANTOS KM O VEÍCULO FAZ POR LITRO DE ALCOOL: ");
                    autonomiaAlcool = ValidarPontoFlutuante(Console.ReadLine());
                    Veiculos.Add(new CarroAlcool(marca, modelo, placa, ano, velocidadeMaxima, capacidadeMaxima, statusPneu, autonomiaAlcool));
                    Console.WriteLine("\nVEÍCULO CADASTRADO COM SUCESSO!");
                }
                else // INSTANCIA UM CARRO A GASOLINA
                {
                    Console.Write("\nDIGITE QUANTOS KM O VEÍCULO FAZ POR LITRO DE GASOLINA: ");
                    autonomiaGasolina = ValidarPontoFlutuante(Console.ReadLine());
                    Veiculos.Add(new CarroPadrao(marca, modelo, placa, ano, velocidadeMaxima, capacidadeMaxima, statusPneu, autonomiaGasolina));
                    Console.WriteLine("\nVEÍCULO CADASTRADO COM SUCESSO!");
                }

            }

        }

        public void CadastrarViagem() // CADASTRA UMA VIAGEM E INSERI NA LISTA
        {
            Console.Write("\nQUANTAS VIAGENS GOSTARIA DE CADASTRAR? ");
            int quant = ValidarNumeros(Console.ReadLine());

            for (int i = 1; i <= quant; i++)
            {
                Console.Clear();
                Console.Write($"\n--- ESTAMOS CADASTRANDO A {i}º VIAGEM ---");
                Console.Write("\nDIGITE A DISTÂNCIA EM KM DESSA VIAGEM: ");
                double distancia = ValidarPontoFlutuante(Console.ReadLine());
                Console.Write("\nDIGITE A DATA DESTA VIAGEM (DIA/MÊS/ANO): ");
                DateTime data = ValidarData(Console.ReadLine());
                Console.Write("\nQUAL A PREVISÃO DO CLIMA PARA ESTA VIAGEM?\n[0] ENSOLARADO\n[1] CHOVENDO\n[2] NEVANDO\n ");
                string clima = ValidarClima(Console.ReadLine());
                Viagens.Add(new Viagem(distancia, data, clima)); // INSTANCIA UMA VIAGEM E ADICIONA NA LISTA
                Console.WriteLine("\nVIAGEM CADASTRADA COM SUCESSO!");
            }
        }

        public void ProgramarViagem() // COMPÕE UMA VIAGEM E UM VEICULO PARA UMA VIAGEM FUTURA
        {
            try
            {
                if (Veiculos.Count > 0)
                {
                    Console.Clear();
                    Console.Write("\nESCOLHA UM VEÍCULO PARA PROGRAMAR UMA VIAGEM!\n");
                    ExibirVeiculos();
                    Console.Write("\nDIGITE O NÚMERO DA PLACA DO VEÍCULO: ");
                    string placa = ValidarPlaca(Console.ReadLine().ToUpper());
                    Veiculo veiculo = Veiculos.Find(x => x.Placa == placa);

                    if (Viagens.Count > 0)
                    {
                        Console.Clear();
                        Console.Write("\nESCOLHA UMA VIAGEM PARA PROGRAMAR!\n");
                        ExibirViagens();
                        Console.Write("DIGITE O CÓDIGO DA VIAGEM QUE DESEJA DIRIGIR: ");
                        int codigo = ValidarNumeros(Console.ReadLine());
                        Viagem viagem = Viagens.Find(x => x.CodigoViagem == codigo);
                        VeiculoViagens.Add(new VeiculoViagem(veiculo, viagem));
                        Veiculos.Remove(veiculo);
                        Viagens.Remove(viagem);
                        Console.WriteLine("\nVIAGEM PROGRAMADA COM SUCESSO!");
                    }
                    else Console.WriteLine("\nNÃO HÁ VIAGENS, CADASTRE UMA PARA DIRIGIR!");
                }
                else Console.WriteLine("\nNÃO HÁ VEÍCULOS, CADASTRE UM PARA DIRIGIR!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERRO INESPERADO! - {e.Message}");
            }
        }
        public void ExecutarViagem() // EXIBE AS VIAGENS PROGRAMADAS, SELECIONA E EXECUTA
        {
            try
            {
                if (VeiculoViagens.Count > 0)
                {
                    Console.Clear();
                    Console.Write("\nESCOLHA UMA VIAGEM PARA PERCORRER!\n");
                    ExibirViagensProgramadas();
                    Console.Write("\nDIGITE O ID DA VIAGEM: ");
                    int id = ValidarNumeros(Console.ReadLine());
                    VeiculoViagem viagem = VeiculoViagens.Find(x => x.IdViagem == id);
                    if (viagem.Viagem.DistanciaVariada <= 0)
                    {
                        Console.WriteLine("ESSA VIAGEM JÁ ESTÁ CONCLUÍDA! VERIFIQUE O RELATÓRIO");
                        return;
                    }
                    Console.Write("\n COMO DESEJA PERCORRER ESSA VIAGEM?\n[1]AUTOMÁTICO [2]MANUAL\n");
                    int opcao = ValidarNumeros(Console.ReadLine());
                    if (opcao == 1)
                        viagem.PercorrerAutomatico(this);
                    else if (opcao == 2)
                        viagem.PercorrerManual(this);
                    else
                        Console.WriteLine("NENHUMA DAS OPÇÕES FORAM ESCOLHIDAS!");
                }
                else Console.WriteLine("\nNÃO HÁ VIAGENS PROGRAMADAS!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERRO INESPERADO! - {e.Message}");
            }
        }

        public void ExibirVeiculos() // PERCORRE OS VEICULOS DA LISTA E OS IMPRIME
        {
            if (Veiculos.Count == 0)
                Console.Write("\n\nNÃO HÁ VEÍCULOS CADASTRADOS!\n");
            else if (Veiculos.Count == 1)
            {
                Console.Write($"\nHÁ {Veiculos.Count} VEÍCULO CADASTRADO!\n\n");
                Veiculos.ForEach(x => x.ExibirVeiculoResumido());
            }
            else
            {
                Console.Write($"\nHÁ {Veiculos.Count} VEÍCULOS CADASTRADOS!\n\n");
                Veiculos.ForEach(x => x.ExibirVeiculoResumido());
            }
        }
        public void ExibirVeiculoDetalhado()
        {
            Console.Clear();
            ExibirVeiculos();
            Console.Write("\nDIGITE A PLACA DO VEÍCULO QUE DESEJA EXIBIR: ");
            string placa = ValidarPlaca(Console.ReadLine().ToUpper());
            Veiculo veiculo = Veiculos.Where(x => x.Placa == placa).FirstOrDefault();
            Console.WriteLine(veiculo);
        }
        public void ExibirViagens() // PERCORRE AS VIAGENS DA LISTA E AS IMPRIME
        {
            if (Viagens.Count == 0)
                Console.Write("\nNÃO HÁ VIAGENS CADASTRADAS!\n");
            else if (Viagens.Count == 1)
            {
                Console.Write($"\nHÁ {Viagens.Count} VIAGEM CADASTRADA!\n\n");
                Viagens.ForEach(x => Console.WriteLine(x));
            }
            else
            {
                Console.Write($"\nHÁ {Viagens.Count} VIAGENS CADASTRADAS!\n\n");
                Viagens.ForEach(x => Console.WriteLine(x));
            }
        }
        public void ExibirViagensProgramadas() // PERCORRE AS VIAGENS PROGRAMADAS DA LISTA E AS IMPRIME
        {
            if (VeiculoViagens.Count == 0)
                Console.Write("\nNÃO HÁ VIAGENS PROGRAMADAS!\n");
            else if (VeiculoViagens.Count == 1)
            {
                Console.Write($"\nHÁ {VeiculoViagens.Count} VIAGEM PROGRAMADA!\n");
                VeiculoViagens.ForEach(x => Console.WriteLine(x));
            }
            else
            {
                Console.Write($"\nHÁ {VeiculoViagens.Count} VIAGENS PROGRAMADAS!\n");
                VeiculoViagens.ForEach(x => Console.WriteLine(x));
            }
        }
    }
}
