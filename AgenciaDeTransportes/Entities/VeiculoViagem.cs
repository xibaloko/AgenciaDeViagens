using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using static AgenciaDeTransportes.ControleDeInputs;

namespace AgenciaDeTransportes.Entities
{
    class VeiculoViagem
    {
        public int IdViagem { get; set; } = new Random().Next(1000, 9999);
        public Veiculo Veiculo { get; set; }
        public Viagem Viagem { get; set; }
        public VeiculoViagem(Veiculo veiculo, Viagem viagem)
        {
            Veiculo = veiculo;
            Viagem = viagem;
        }
        public void Percorrer(Agencia agencia)
        {
            switch (Veiculo)
            {
                case CarroPadrao carroPadrao:

                    int quantidadesDeAbastecimento = 0;
                    int quantidadesDeCalibragem = 0;
                    double cont = 0.0;
                    double percorrido = 0.0;

                    Relatorio relatorio = new Relatorio(this);
                    relatorio.AdicionarOcorrenciaClimatica(percorrido, Viagem.Clima);

                    void PreencherRelatorio()
                    {
                        relatorio.AdicionarDistanciaPercorrida(percorrido);
                        relatorio.AdicionarParadasAbastecimento(quantidadesDeAbastecimento);
                        relatorio.AdicionarParadasCalibragem(quantidadesDeCalibragem);
                    }

                    while (Viagem.Distancia > 0.0)
                    {
                        carroPadrao.AutonomiaClima(Viagem);
                        carroPadrao.AutonomiaPneu();

                        if (carroPadrao.QuantidadeGasolina == 0.0)
                        {
                            Console.Write("\nO VEÍCULO ESTÁ SEM COMBUSTIVEL, DESEJA ABASTECER? (S/N): ");
                            string s = ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                            if (s == "S")
                            {
                                carroPadrao.Abastecer();
                                quantidadesDeAbastecimento++;
                            }
                            else
                            {
                                Console.WriteLine("\nOK, A VIAGEM FICARÁ PAUSADA!");
                                PreencherRelatorio();
                                return;
                            }
                        }

                        if (carroPadrao.StatusPneu == 0)
                        {
                            Console.Write("\nOS PNEUS ESTÃO MURCHOS, DESEJA CALIBRAR? (S/N): ");
                            string s = ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                            if (s == "S")
                            {
                                carroPadrao.Calibrar();
                                relatorio.AdicionarOcorrenciaPneus(percorrido, carroPadrao.StatusPneu);
                                quantidadesDeCalibragem++;
                            }
                            else
                            {
                                Console.WriteLine("\nOK, A VIAGEM FICARÁ PAUSADA!");
                                PreencherRelatorio();
                                return;
                            }
                        }

                        Viagem.Distancia -= 1.0;
                        carroPadrao.QuantidadeGasolina -= 1.0 / carroPadrao.AutonomiaGasolinaVariada;
                        percorrido ++;
                        cont ++;

                        if (cont == 100.0)
                        {
                            Viagem.MudarClima();
                            relatorio.AdicionarOcorrenciaClimatica(percorrido, Viagem.ClimaVariado);
                            carroPadrao.DescalibrarPneu();
                            cont = 0.0;
                        }
                    }
                    PreencherRelatorio();
                    
                    agencia.Relatorios.Add(relatorio);
                    break;
            }

        }
        public override string ToString() => $"ID: {IdViagem}\nVEÍCULO: {Veiculo}\nVIAGEM: {Viagem}";
    }
}

