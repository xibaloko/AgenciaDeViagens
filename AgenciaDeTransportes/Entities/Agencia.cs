﻿using System;
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
            string marca = ControleDeInputs.ValidarLetrasComEspaco(Console.ReadLine());
            Console.Write("\nDIGITE O MODELO DO VEÍCULO: ");
            string modelo = ControleDeInputs.ValidarModelos(Console.ReadLine());
            Console.Write("\nDIGITE A PLACA DO VEÍCULO: ");
            string placa = ControleDeInputs.ValidarPlaca(Console.ReadLine().ToUpper());
            Console.Write("\nDIGITE O ANO DO VEÍCULO: ");
            DateTime ano = ControleDeInputs.ValidarAno(Console.ReadLine());
            Console.Write("\nDIGITE A VELOCIDADE MÁXIMA DO VEÍCULO (KM/H): ");
            double velocidadeMaxima = ControleDeInputs.ValidarPontoFlutuante(Console.ReadLine());
            Console.Write("\nDIGITE A CAPACIDADE DO TANQUE: ");
            double capacidadeMaxima = ControleDeInputs.ValidarPontoFlutuante(Console.ReadLine());

            Console.Write("\nFLEX[F]\nALCOOL[A]\nGASOLINA[G]\nCANCELAR[C]\nESCOLHA UMA OPÇÃO: ");
            string opcao = ControleDeInputs.ValidarOpcoes(Console.ReadLine().ToUpper());

            double autonomiaGasolina, autonomiaAlcool;

            if (opcao == "F")
            {
                Console.Write("\nDIGITE QUANTOS KM O VEÍCULO FAZ POR LITRO DE GASOLINA: ");
                autonomiaGasolina = ControleDeInputs.ValidarPontoFlutuante(Console.ReadLine());
                Console.Write("\nDIGITE QUANTOS KM O VEÍCULO FAZ POR LITRO DE ALCOOL: ");
                autonomiaAlcool = ControleDeInputs.ValidarPontoFlutuante(Console.ReadLine());
                Veiculos.Add(new CarroFlex(marca, modelo, placa, ano, velocidadeMaxima, capacidadeMaxima, autonomiaGasolina, autonomiaAlcool));
                Console.WriteLine("\nVEÍCULO CADASTRADO COM SUCESSO!");
            }
            else if (opcao == "A")
            {
                Console.Write("\nDIGITE QUANTOS KM O VEÍCULO FAZ POR LITRO DE ALCOOL: ");
                autonomiaAlcool = ControleDeInputs.ValidarPontoFlutuante(Console.ReadLine());
                Veiculos.Add(new CarroAlcool(marca, modelo, placa, ano, velocidadeMaxima, capacidadeMaxima, autonomiaAlcool));
                Console.WriteLine("\nVEÍCULO CADASTRADO COM SUCESSO!");
            }
            else if (opcao == "G")
            {
                Console.Write("\nDIGITE QUANTOS KM O VEÍCULO FAZ POR LITRO DE GASOLINA: ");
                autonomiaGasolina = ControleDeInputs.ValidarPontoFlutuante(Console.ReadLine());
                Veiculos.Add(new CarroPadrao(marca, modelo, placa, ano, velocidadeMaxima, capacidadeMaxima, autonomiaGasolina));
                Console.WriteLine("\nVEÍCULO CADASTRADO COM SUCESSO!");
            }
            else
                Console.WriteLine("NENHUM VEÍCULO FOI CADASTRADO!");
        }

        public void CadastrarViagem()
        {
            Console.Write("\nDIGITE A DISTÂNCIA EM KM DESSA VIAGEM: ");
            double distancia = ControleDeInputs.ValidarPontoFlutuante(Console.ReadLine());
            Console.Write("\nDIGITE A DATA DESTA VIAGEM (dd/MM/yyyy): ");
            DateTime data = ControleDeInputs.ValidarData(Console.ReadLine());
            Viagens.Add(new Viagem(distancia, data));
            Console.WriteLine("VIAGEM CADASTRADA COM SUCESSO!");
        }

        public void ExibirVeiculos()
        {
            if (Veiculos.Count == 0)
                Console.Write("\nNÃO HÁ VEÍCULOS CADASTRADOS!\n");
            else if(Veiculos.Count == 1)
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
        public void ExibirViagens()
        {
            if (Viagens.Count == 0)
                Console.Write("\nNÃO HÁ VIAGENS CADASTRADAS!\n");
            else if(Viagens.Count == 1)
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
