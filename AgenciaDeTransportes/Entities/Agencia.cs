using System;
using System.Collections.Generic;

namespace AgenciaDeTransportes.Entities
{
    class Agencia
    {
        public string NomeAgencia { get; private set; }
        public List<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
        public List<Viagem> Viagens { get; set; } = new List<Viagem>();
        public Agencia(string nomeAgencia) => NomeAgencia = nomeAgencia;
        public void CadastrarVeiculo()
        {
            Console.Write("\nDIGITE A MARCA DO VEÍCULO: ");
            string marca = ControleDeInputs.ValidarLetrasComEspaco(Console.ReadLine().ToUpper());
            Console.Write("\nDIGITE O MODELO DO VEÍCULO: ");
            string modelo = ControleDeInputs.ValidarModelos(Console.ReadLine().ToUpper());
            Console.Write("\nDIGITE A PLACA DO VEÍCULO: ");
            string placa = ControleDeInputs.ValidarPlaca(Console.ReadLine().ToUpper());
            Console.Write("\nDIGITE O ANO DO VEÍCULO: ");
            DateTime ano = ControleDeInputs.ValidarAno(Console.ReadLine());
            Console.Write("\nDIGITE A VELOCIDADE MÉDIA DO VEÍCULO (KM/H): ");
            double velocidadeMaxima = ControleDeInputs.ValidarPontoFlutuante(Console.ReadLine());
            Console.Write("\nDIGITE A CAPACIDADE DO TANQUE: ");
            double capacidadeMaxima = ControleDeInputs.ValidarPontoFlutuante(Console.ReadLine());
            Console.Clear();
            Console.Write("\nCOMO ESTA A SITUAÇÃO DOS PNEUS?\n\nCHEIO[1]\nMODERADO[2]\nMURCHO[3]\n\nESCOLHA UMA OPÇÃO: ");
            int statusPneu = ControleDeInputs.ValidarNumeros1A3(Console.ReadLine());

            Console.Write("\nQUAL O TIPO DO COMBUSTÍVEL?\n\nFLEX[F]\nALCOOL[A]\nGASOLINA[G]\n\nESCOLHA UMA OPÇÃO: ");
            string opcao = ControleDeInputs.ValidarOpcoes(Console.ReadLine().ToUpper());

            double autonomiaGasolina, autonomiaAlcool;

            if (opcao == "F") // INSTANCIA UM CARRO FLEX
            {
                Console.Write("\nDIGITE QUANTOS KM O VEÍCULO FAZ POR LITRO DE GASOLINA: ");
                autonomiaGasolina = ControleDeInputs.ValidarPontoFlutuante(Console.ReadLine());
                Console.Write("\nDIGITE QUANTOS KM O VEÍCULO FAZ POR LITRO DE ALCOOL: ");
                autonomiaAlcool = ControleDeInputs.ValidarPontoFlutuante(Console.ReadLine());
                Veiculos.Add(new CarroFlex(marca, modelo, placa, ano, velocidadeMaxima, capacidadeMaxima, statusPneu, autonomiaGasolina, autonomiaAlcool));
                Console.WriteLine("\nVEÍCULO CADASTRADO COM SUCESSO!");
            }
            else if (opcao == "A") // INSTANCIA UM CARRO A ALCOOL
            {
                Console.Write("\nDIGITE QUANTOS KM O VEÍCULO FAZ POR LITRO DE ALCOOL: ");
                autonomiaAlcool = ControleDeInputs.ValidarPontoFlutuante(Console.ReadLine());
                Veiculos.Add(new CarroAlcool(marca, modelo, placa, ano, velocidadeMaxima, capacidadeMaxima, statusPneu, autonomiaAlcool));
                Console.WriteLine("\nVEÍCULO CADASTRADO COM SUCESSO!");
            }
            else // INSTANCIA UM CARRO A GASOLINA
            {
                Console.Write("\nDIGITE QUANTOS KM O VEÍCULO FAZ POR LITRO DE GASOLINA: ");
                autonomiaGasolina = ControleDeInputs.ValidarPontoFlutuante(Console.ReadLine());
                Veiculos.Add(new CarroPadrao(marca, modelo, placa, ano, velocidadeMaxima, capacidadeMaxima, statusPneu, autonomiaGasolina));
                Console.WriteLine("\nVEÍCULO CADASTRADO COM SUCESSO!");
            }
            
        }

        public void CadastrarViagem()
        {
            if (Veiculos.Count > 0)
            {
                Console.Write("\nDIGITE A DISTÂNCIA EM KM DESSA VIAGEM: ");
                double distancia = ControleDeInputs.ValidarPontoFlutuante(Console.ReadLine());
                Console.Write("\nDIGITE A DATA DESTA VIAGEM (dd/MM/yyyy): ");
                DateTime data = ControleDeInputs.ValidarData(Console.ReadLine());
                Console.Write("\nA PREVISÃO DO CLIMA PARA ESTA VIAGEM É BOA? (S/N): ");
                bool clima = ControleDeInputs.ValidarOpcoesSOuN(Console.ReadLine().ToUpper()) == "S" ? true : false;
                Console.Clear();
                Console.Write("\nESCOLHA UM VEÍCULO PARA ESSA VIAGEM!\n");
                ExibirVeiculos();
                Console.Write("\nDIGITE O NÚMERO DA PLACA DO VEÍCULO PARA ESSA VIAGEM: ");
                string placa = ControleDeInputs.ValidarPlaca(Console.ReadLine().ToUpper());
                Veiculo veiculo = Veiculos.Find(x => x.Placa == placa);
                Viagens.Add(new Viagem(distancia, data, clima, veiculo)); // INSTANCIA UMA VIAGEM E ADICIONA NA LISTA
                Console.WriteLine("\nVIAGEM CADASTRADA COM SUCESSO!");
            }
            else Console.WriteLine("\nNÃO HÁ VEÍCULOS CADASTRADOS!");
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
    }
}
