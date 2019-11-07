using System;
using System.Collections.Generic;
using System.Linq;
using static AgenciaDeTransportes.ControleDeInputs;

namespace AgenciaDeTransportes.Entities
{
    class Agencia
    {
        public string NomeAgencia { get; private set; }
        public List<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
        public List<Viagem> Viagens { get; set; } = new List<Viagem>();
        public List<VeiculoViagem> VeiculoViagens { get; set; } = new List<VeiculoViagem>();
        public List<Relatorio> Relatorios { get; set; } = new List<Relatorio>();

        public Agencia(string nomeAgencia) => NomeAgencia = nomeAgencia;

        public void CadastrarVeiculo() // CADASTRA UM VEÍCULO E INSERI NA LISTA
        {
            Console.Write("\nDIGITE A MARCA DO VEÍCULO: ");
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
            int statusPneu = ValidarNumeros0A2(Console.ReadLine());

            Console.Write("\nQUAL O TIPO DO COMBUSTÍVEL?\n\nFLEX[F]\nALCOOL[A]\nGASOLINA[G]\n\nESCOLHA UMA OPÇÃO: ");
            string opcao = ValidarOpcoes(Console.ReadLine().ToUpper());

            double autonomiaGasolina, autonomiaAlcool;

            if (opcao == "F") // INSTANCIA UM CARRO FLEX
            {
                Console.Write("\nDIGITE QUANTOS KM O VEÍCULO FAZ POR LITRO DE GASOLINA: ");
                autonomiaGasolina = ValidarPontoFlutuante(Console.ReadLine());
                Console.Write("\nDIGITE QUANTOS KM O VEÍCULO FAZ POR LITRO DE ALCOOL: ");
                autonomiaAlcool = ValidarPontoFlutuante(Console.ReadLine());
                Veiculos.Add(new CarroFlex(marca, modelo, placa, ano, velocidadeMaxima, capacidadeMaxima, statusPneu, autonomiaGasolina, autonomiaAlcool));
                Console.WriteLine("\nVEÍCULO CADASTRADO COM SUCESSO!");
            }
            else if (opcao == "A") // INSTANCIA UM CARRO A ALCOOL
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

        public void CadastrarViagem() // CADASTRA UMA VIAGEM E INSERI NA LISTA
        {
            Console.Write("\nDIGITE A DISTÂNCIA EM KM DESSA VIAGEM: ");
            double distancia = ValidarPontoFlutuante(Console.ReadLine());
            Console.Write("\nDIGITE A DATA DESTA VIAGEM (DIA/MÊS/ANO): ");
            DateTime data = ValidarData(Console.ReadLine());
            Console.Write("\nQUAL A PREVISÃO DO CLIMA PARA ESTA VIAGEM?\n[0] ENSOLARADO\n[1] CHOVENDO\n[2] NEVANDO\n ");
            string clima = ValidarClima(Console.ReadLine());
            Viagens.Add(new Viagem(distancia, data, clima)); // INSTANCIA UMA VIAGEM E ADICIONA NA LISTA
            Console.WriteLine("\nVIAGEM CADASTRADA COM SUCESSO!");
        }

        public void ProgramarViagem() // COMPÕE UMA VIAGEM E UM VEICULO PARA UMA VIAGEM FUTURA
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
                }
                else Console.WriteLine("\nNÃO HÁ VIAGENS, CADASTRE UMA PARA DIRIGIR!");
            }
            else Console.WriteLine("\nNÃO HÁ VEÍCULOS, CADASTRE UM PARA DIRIGIR!");
        }
        public void ExecutarViagem()
        {
            if (VeiculoViagens.Count > 0)
            {
                Console.Clear();
                Console.Write("\nESCOLHA UMA VIAGEM PARA PERCORRER!\n");
                ExibirViagensProgramadas();
                Console.Write("\nDIGITE O ID DA VIAGEM: ");
                int id = ValidarNumeros(Console.ReadLine());
                VeiculoViagem viagem = VeiculoViagens.Find(x => x.IdViagem == id);
                viagem.Percorrer(this);
            }
            else Console.WriteLine("\nNÃO HÁ VIAGENS PROGRAMADAS!");
        }

        public void ExibirVeiculos() // PERCORRE OS VEICULOS DA LISTA E OS IMPRIME
        {
            if (Veiculos.Count == 0)
                Console.Write("\nNÃO HÁ VEÍCULOS CADASTRADOS!\n");
            else if (Veiculos.Count == 1)
            {
                Console.Write($"\nHÁ {Veiculos.Count} VEÍCULO CADASTRADO!\n");
                Veiculos.ForEach(x => Console.WriteLine(x));
            }
            else
            {
                Console.Write($"\nHÁ {Veiculos.Count} VEÍCULOS CADASTRADOS!\n");
                Veiculos.ForEach(x => Console.WriteLine(x));
            }
        }
        public void ExibirViagens() // PERCORRE AS VIAGENS DA LISTA E AS IMPRIME
        {
            if (Viagens.Count == 0)
                Console.Write("\nNÃO HÁ VIAGENS CADASTRADAS!\n");
            else if (Viagens.Count == 1)
            {
                Console.Write($"\nHÁ {Viagens.Count} VIAGEM CADASTRADA!\n");
                Viagens.ForEach(x => Console.WriteLine(x));
            }
            else
            {
                Console.Write($"\nHÁ {Viagens.Count} VIAGENS CADASTRADAS!\n");
                Viagens.ForEach(x => Console.WriteLine(x));
            }
        }
        public void ExibirViagensProgramadas()
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
