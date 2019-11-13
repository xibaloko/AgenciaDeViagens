using System;
using System.Globalization;
using static AgenciaDeTransportes.Servicos.ControleDeInputs;

namespace AgenciaDeTransportes.Entities
{
    public class VeiculoViagem
    {
        public int IdViagem { get; set; } = new Random().Next(1000, 9999);
        public Veiculo Veiculo { get; set; }
        public Viagem Viagem { get; set; }
        public VeiculoViagem(Veiculo veiculo, Viagem viagem)
        {
            Veiculo = veiculo;
            Viagem = viagem;
            viagem.DuracaoPrevista(veiculo);
        }
        public void PercorrerAutomatico(Agencia agencia)
        {
            int quantidadesDeAbastecimento = 0;
            int quantidadesDeCalibragem = 0;
            double cont = 0.0;
            double percorrido = 0.0;
            double combustivelGasto = 0.0;

            Relatorio relatorio = new Relatorio(this);

            switch (Veiculo)
            {
                case CarroPadrao carroPadrao:

                    void PreencherRelatorioGasolina()
                    {
                        relatorio.AdicionarDistanciaPercorrida(percorrido);
                        relatorio.AdicionarCombustivelGasto(combustivelGasto / carroPadrao.CapacidadeTanque * 100.0);
                        relatorio.AdicionarParadasAbastecimento(quantidadesDeAbastecimento);
                        relatorio.AdicionarParadasCalibragem(quantidadesDeCalibragem);
                    }

                    while (Viagem.DistanciaVariada > 0.0)
                    {
                        carroPadrao.AutonomiaClima(Viagem);
                        carroPadrao.AutonomiaPneu();

                        if (carroPadrao.QuantidadeGasolina <= 0.0)
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
                                PreencherRelatorioGasolina();
                                agencia.Relatorios.Add(relatorio);
                                return;
                            }
                        }

                        if (carroPadrao.StatusPneu == 0)
                        {
                            Console.Write("\nOS PNEUS ESTÃO MUITO MURCHOS, DESEJA CALIBRAR? (S/N): ");
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
                                PreencherRelatorioGasolina();
                                agencia.Relatorios.Add(relatorio);
                                return;
                            }
                        }

                        Viagem.DistanciaVariada -= 1.0;
                        percorrido++;

                        carroPadrao.QuantidadeGasolina -= 1.0 / carroPadrao.AutonomiaGasolinaVariada;
                        combustivelGasto += 1.0 / carroPadrao.AutonomiaGasolinaVariada;

                        cont++;

                        if (cont == 100.0)
                        {
                            Console.WriteLine("100 KM PERCORRIDOS!");
                            Viagem.MudarClima();
                            relatorio.AdicionarOcorrenciaClimatica(percorrido, Viagem.ClimaVariado);
                            carroPadrao.DescalibrarPneu();
                            cont = 0.0;
                        }
                    }
                    Console.WriteLine("VIAGEM CONCLUÍDA! OS DETALHES ESTÃO NO RELATÓRIO..");
                    PreencherRelatorioGasolina();
                    agencia.Relatorios.Add(relatorio);
                    break;

                case CarroAlcool carroAlcool:

                    void PreencherRelatorioAlcool()
                    {
                        relatorio.AdicionarDistanciaPercorrida(percorrido);
                        relatorio.AdicionarCombustivelGasto(combustivelGasto / carroAlcool.CapacidadeTanque * 100.0);
                        relatorio.AdicionarParadasAbastecimento(quantidadesDeAbastecimento);
                        relatorio.AdicionarParadasCalibragem(quantidadesDeCalibragem);
                    }

                    while (Viagem.DistanciaVariada > 0.0)
                    {
                        carroAlcool.AutonomiaClima(Viagem);
                        carroAlcool.AutonomiaPneu();

                        if (carroAlcool.QuantidadeAlcool <= 0.0)
                        {
                            Console.Write("\nO VEÍCULO ESTÁ SEM COMBUSTIVEL, DESEJA ABASTECER? (S/N): ");
                            string s = ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                            if (s == "S")
                            {
                                carroAlcool.Abastecer();
                                quantidadesDeAbastecimento++;
                            }
                            else
                            {
                                Console.WriteLine("\nOK, A VIAGEM FICARÁ PAUSADA!");
                                PreencherRelatorioAlcool();
                                agencia.Relatorios.Add(relatorio);
                                return;
                            }
                        }

                        if (carroAlcool.StatusPneu == 0)
                        {
                            Console.Write("\nOS PNEUS ESTÃO MUITO MURCHOS, DESEJA CALIBRAR? (S/N): ");
                            string s = ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                            if (s == "S")
                            {
                                carroAlcool.Calibrar();
                                relatorio.AdicionarOcorrenciaPneus(percorrido, carroAlcool.StatusPneu);
                                quantidadesDeCalibragem++;
                            }
                            else
                            {
                                Console.WriteLine("\nOK, A VIAGEM FICARÁ PAUSADA!");
                                PreencherRelatorioAlcool();
                                agencia.Relatorios.Add(relatorio);
                                return;
                            }
                        }

                        Viagem.DistanciaVariada -= 1.0;
                        percorrido++;

                        carroAlcool.QuantidadeAlcool -= 1.0 / carroAlcool.AutonomiaAlcoolVariada;
                        combustivelGasto += 1.0 / carroAlcool.AutonomiaAlcoolVariada;

                        cont++;

                        if (cont == 100.0)
                        {
                            Console.WriteLine("100 KM PERCORRIDOS!");
                            Viagem.MudarClima();
                            relatorio.AdicionarOcorrenciaClimatica(percorrido, Viagem.ClimaVariado);
                            carroAlcool.DescalibrarPneu();
                            cont = 0.0;
                        }
                    }
                    Console.WriteLine("VIAGEM CONCLUÍDA! OS DETALHES ESTÃO NO RELATÓRIO..");
                    PreencherRelatorioAlcool();
                    agencia.Relatorios.Add(relatorio);
                    break;

                case CarroFlex carroFlex:

                    void PreencherRelatorioFlex()
                    {
                        relatorio.AdicionarDistanciaPercorrida(percorrido);
                        relatorio.AdicionarCombustivelGasto(combustivelGasto / carroFlex.CapacidadeTanque * 100.0);
                        relatorio.AdicionarParadasAbastecimento(quantidadesDeAbastecimento);
                        relatorio.AdicionarParadasCalibragem(quantidadesDeCalibragem);
                    }

                    while (Viagem.DistanciaVariada > 0.0)
                    {
                        carroFlex.AutonomiaClima(Viagem);
                        carroFlex.AutonomiaPneu();

                        if (carroFlex.QuantidadeAlcool <= 0.0 && carroFlex.QuantidadeGasolina <= 0.0)
                        {
                            Console.Write("\nO VEÍCULO ESTÁ SEM COMBUSTIVEL, DESEJA ABASTECER? (S/N): ");
                            string s = ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                            if (s == "S")
                            {
                                carroFlex.Abastecer();
                                quantidadesDeAbastecimento++;
                            }
                            else
                            {
                                Console.WriteLine("\nOK, A VIAGEM FICARÁ PAUSADA!");
                                PreencherRelatorioFlex();
                                agencia.Relatorios.Add(relatorio);
                                return;
                            }
                        }

                        if (carroFlex.StatusPneu == 0)
                        {
                            Console.Write("\nOS PNEUS ESTÃO MUITO MURCHOS, DESEJA CALIBRAR? (S/N): ");
                            string s = ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                            if (s == "S")
                            {
                                carroFlex.Calibrar();
                                relatorio.AdicionarOcorrenciaPneus(percorrido, carroFlex.StatusPneu);
                                quantidadesDeCalibragem++;
                            }
                            else
                            {
                                Console.WriteLine("\nOK, A VIAGEM FICARÁ PAUSADA!");
                                PreencherRelatorioFlex();
                                agencia.Relatorios.Add(relatorio);
                                return;
                            }
                        }

                        Viagem.DistanciaVariada -= 1.0;
                        percorrido++;

                        if (carroFlex.QuantidadeAlcool > 0.0)
                        {
                            carroFlex.QuantidadeAlcool -= 1.0 / carroFlex.AutonomiaAlcoolVariada;
                            combustivelGasto += 1.0 / carroFlex.AutonomiaAlcoolVariada;
                        }
                        else
                        {
                            carroFlex.QuantidadeGasolina -= 1.0 / carroFlex.AutonomiaGasolinaVariada;
                            combustivelGasto += 1.0 / carroFlex.AutonomiaGasolinaVariada;
                        }

                        cont++;

                        if (cont == 100.0)
                        {
                            Console.WriteLine("100 KM PERCORRIDOS!");
                            Viagem.MudarClima();
                            relatorio.AdicionarOcorrenciaClimatica(percorrido, Viagem.ClimaVariado);
                            carroFlex.DescalibrarPneu();
                            cont = 0.0;
                        }
                    }
                    Console.WriteLine("VIAGEM CONCLUÍDA! OS DETALHES ESTÃO NO RELATÓRIO..");
                    PreencherRelatorioFlex();
                    agencia.Relatorios.Add(relatorio);
                    break;
            }
        }
        public void PercorrerManual(Agencia agencia)
        {
            int quantidadesDeAbastecimento = 0;
            int quantidadesDeCalibragem = 0;
            double percorrido = 0.0;
            double percorrerManual;
            double combustivelGasto = 0.0;

            Relatorio relatorio = new Relatorio(this);

            switch (Veiculo)
            {
                case CarroPadrao carroPadrao:

                    void PreencherRelatorioGasolina()
                    {
                        relatorio.AdicionarDistanciaPercorrida(percorrido);
                        relatorio.AdicionarCombustivelGasto(combustivelGasto / carroPadrao.CapacidadeTanque * 100.0);
                        relatorio.AdicionarParadasAbastecimento(quantidadesDeAbastecimento);
                        relatorio.AdicionarParadasCalibragem(quantidadesDeCalibragem);
                    }

                    while (Viagem.DistanciaVariada > 0.0)
                    {
                        Console.Write("\nQUANTOS KM VOCÊ DESEJA PERCORRER? ");
                        percorrerManual = ValidarPontoFlutuante(Console.ReadLine());

                        for (int i = 1; i <= percorrerManual; i++)
                        {
                            carroPadrao.AutonomiaClima(Viagem);
                            carroPadrao.AutonomiaPneu();

                            if (carroPadrao.QuantidadeGasolina <= 0.0)
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
                                    PreencherRelatorioGasolina();
                                    agencia.Relatorios.Add(relatorio);
                                    return;
                                }
                            }

                            if (carroPadrao.StatusPneu == 0)
                            {
                                Console.Write("\nOS PNEUS ESTÃO MUITO MURCHOS, DESEJA CALIBRAR? (S/N): ");
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
                                    PreencherRelatorioGasolina();
                                    agencia.Relatorios.Add(relatorio);
                                    return;
                                }
                            }

                            if (Viagem.DistanciaVariada <= 0.0)
                                break;

                            Viagem.DistanciaVariada -= 1.0;
                            percorrido++;

                            carroPadrao.QuantidadeGasolina -= 1.0 / carroPadrao.AutonomiaGasolinaVariada;
                            combustivelGasto += 1.0 / carroPadrao.AutonomiaGasolinaVariada;

                            if (percorrido % 100.0 == 0 && percorrido > 0)
                            {
                                Console.WriteLine("100 KM PERCORRIDOS!");
                                Viagem.MudarClima();
                                relatorio.AdicionarOcorrenciaClimatica(percorrido, Viagem.ClimaVariado);
                                carroPadrao.DescalibrarPneu();
                            }
                        }
                        if (Viagem.DistanciaVariada <= 0.0) break;
                        Console.Write($"{percorrido.ToString("F2", CultureInfo.InvariantCulture)} KM PERCORRIDOS ATÉ O MOMENTO!\nDESEJA CONTINUAR A VIAGEM? (S/N): ");
                        string opcao = ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                        if (opcao != "S") return;
                    }
                    Console.WriteLine("VIAGEM CONCLUÍDA! OS DETALHES ESTÃO NO RELATÓRIO..");
                    PreencherRelatorioGasolina();
                    agencia.Relatorios.Add(relatorio);
                    break;

                case CarroAlcool carroAlcool:

                    void PreencherRelatorioAlcool()
                    {
                        relatorio.AdicionarDistanciaPercorrida(percorrido);
                        relatorio.AdicionarCombustivelGasto(combustivelGasto / carroAlcool.CapacidadeTanque * 100.0);
                        relatorio.AdicionarParadasAbastecimento(quantidadesDeAbastecimento);
                        relatorio.AdicionarParadasCalibragem(quantidadesDeCalibragem);
                    }

                    while (Viagem.DistanciaVariada > 0.0)
                    {
                        Console.Write("\nQUANTOS KM VOCÊ DESEJA PERCORRER? ");
                        percorrerManual = ValidarPontoFlutuante(Console.ReadLine());

                        for (int i = 1; i <= percorrerManual; i++)
                        {
                            carroAlcool.AutonomiaClima(Viagem);
                            carroAlcool.AutonomiaPneu();

                            if (carroAlcool.QuantidadeAlcool <= 0.0)
                            {
                                Console.Write("\nO VEÍCULO ESTÁ SEM COMBUSTIVEL, DESEJA ABASTECER? (S/N): ");
                                string s = ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                                if (s == "S")
                                {
                                    carroAlcool.Abastecer();
                                    quantidadesDeAbastecimento++;
                                }
                                else
                                {
                                    Console.WriteLine("\nOK, A VIAGEM FICARÁ PAUSADA!");
                                    PreencherRelatorioAlcool();
                                    agencia.Relatorios.Add(relatorio);
                                    return;
                                }
                            }

                            if (carroAlcool.StatusPneu == 0)
                            {
                                Console.Write("\nOS PNEUS ESTÃO MUITO MURCHOS, DESEJA CALIBRAR? (S/N): ");
                                string s = ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                                if (s == "S")
                                {
                                    carroAlcool.Calibrar();
                                    relatorio.AdicionarOcorrenciaPneus(percorrido, carroAlcool.StatusPneu);
                                    quantidadesDeCalibragem++;
                                }
                                else
                                {
                                    Console.WriteLine("\nOK, A VIAGEM FICARÁ PAUSADA!");
                                    PreencherRelatorioAlcool();
                                    agencia.Relatorios.Add(relatorio);
                                    return;
                                }
                            }

                            if (Viagem.DistanciaVariada <= 0.0)
                                break;

                            Viagem.DistanciaVariada -= 1.0;
                            percorrido++;

                            carroAlcool.QuantidadeAlcool -= 1.0 / carroAlcool.AutonomiaAlcoolVariada;
                            combustivelGasto += 1.0 / carroAlcool.AutonomiaAlcoolVariada;

                            if (percorrido % 100.0 == 0 && percorrido > 0)
                            {
                                Console.WriteLine("100 KM PERCORRIDOS!");
                                Viagem.MudarClima();
                                relatorio.AdicionarOcorrenciaClimatica(percorrido, Viagem.ClimaVariado);
                                carroAlcool.DescalibrarPneu();
                            }
                        }
                        if (Viagem.DistanciaVariada <= 0.0) break;
                        Console.Write($"{percorrido.ToString("F2", CultureInfo.InvariantCulture)} KM PERCORRIDOS ATÉ O MOMENTO!\nDESEJA CONTINUAR A VIAGEM? (S/N): ");
                        string opcao = ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                        if (opcao != "S") return;
                    }
                    Console.WriteLine("VIAGEM CONCLUÍDA! OS DETALHES ESTÃO NO RELATÓRIO..");
                    PreencherRelatorioAlcool();
                    agencia.Relatorios.Add(relatorio);
                    break;

                case CarroFlex carroFlex:

                    void PreencherRelatorioFlex()
                    {
                        relatorio.AdicionarDistanciaPercorrida(percorrido);
                        relatorio.AdicionarCombustivelGasto(combustivelGasto / carroFlex.CapacidadeTanque * 100.0);
                        relatorio.AdicionarParadasAbastecimento(quantidadesDeAbastecimento);
                        relatorio.AdicionarParadasCalibragem(quantidadesDeCalibragem);
                    }

                    while (Viagem.DistanciaVariada > 0.0)
                    {
                        Console.Write("\nQUANTOS KM VOCÊ DESEJA PERCORRER? ");
                        percorrerManual = ValidarPontoFlutuante(Console.ReadLine());

                        for (int i = 1; i <= percorrerManual; i++)
                        {
                            carroFlex.AutonomiaClima(Viagem);
                            carroFlex.AutonomiaPneu();

                            if (carroFlex.QuantidadeAlcool <= 0.0 && carroFlex.QuantidadeGasolina <= 0.0)
                            {
                                Console.Write("\nO VEÍCULO ESTÁ SEM COMBUSTIVEL, DESEJA ABASTECER? (S/N): ");
                                string s = ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                                if (s == "S")
                                {
                                    carroFlex.Abastecer();
                                    quantidadesDeAbastecimento++;
                                }
                                else
                                {
                                    Console.WriteLine("\nOK, A VIAGEM FICARÁ PAUSADA!");
                                    PreencherRelatorioFlex();
                                    agencia.Relatorios.Add(relatorio);
                                    return;
                                }
                            }

                            if (carroFlex.StatusPneu == 0)
                            {
                                Console.Write("\nOS PNEUS ESTÃO MUITO MURCHOS, DESEJA CALIBRAR? (S/N): ");
                                string s = ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                                if (s == "S")
                                {
                                    carroFlex.Calibrar();
                                    relatorio.AdicionarOcorrenciaPneus(percorrido, carroFlex.StatusPneu);
                                    quantidadesDeCalibragem++;
                                }
                                else
                                {
                                    Console.WriteLine("\nOK, A VIAGEM FICARÁ PAUSADA!");
                                    PreencherRelatorioFlex();
                                    agencia.Relatorios.Add(relatorio);
                                    return;
                                }
                            }

                            if (Viagem.DistanciaVariada <= 0.0)
                                break;

                            Viagem.DistanciaVariada -= 1.0;
                            percorrido++;

                            if (carroFlex.QuantidadeAlcool > 0.0)
                            {
                                carroFlex.QuantidadeAlcool -= 1.0 / carroFlex.AutonomiaAlcoolVariada;
                                combustivelGasto += 1.0 / carroFlex.AutonomiaAlcoolVariada;
                            }
                            else
                            {
                                carroFlex.QuantidadeGasolina -= 1.0 / carroFlex.AutonomiaGasolinaVariada;
                                combustivelGasto += 1.0 / carroFlex.AutonomiaGasolinaVariada;
                            }

                            if (percorrido % 100.0 == 0 && percorrido > 0)
                            {
                                Console.WriteLine("100 KM PERCORRIDOS!");
                                Viagem.MudarClima();
                                relatorio.AdicionarOcorrenciaClimatica(percorrido, Viagem.ClimaVariado);
                                carroFlex.DescalibrarPneu();
                            }
                        }
                        if (Viagem.DistanciaVariada <= 0.0) break;
                        Console.Write($"{percorrido.ToString("F2", CultureInfo.InvariantCulture)} KM PERCORRIDOS ATÉ O MOMENTO!\nDESEJA CONTINUAR A VIAGEM? (S/N): ");
                        string opcao = ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                        if (opcao != "S") return;
                    }
                    Console.WriteLine("VIAGEM CONCLUÍDA! OS DETALHES ESTÃO NO RELATÓRIO..");
                    PreencherRelatorioFlex();
                    agencia.Relatorios.Add(relatorio);
                    break;
            }
        }
        public override string ToString() =>
            $"\nID: {IdViagem}\n" +
            $"VEÍCULO - PLACA: {Veiculo.Placa} - MARCA: {Veiculo.Marca} - MODELO: {Veiculo.Modelo}\n" +
            $"VIAGEM - DISTÂNCIA: {Viagem.Distancia.ToString("F2", CultureInfo.InvariantCulture)} KM - DURAÇÃO ESTIMADA: {Viagem.Duracao}\n" +
            $"------------------------------------------------------------";
    }
}

