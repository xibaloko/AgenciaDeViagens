using System;
using System.Text;
using System.Globalization;

namespace AgenciaDeTransportes.Entities
{
    class CarroFlex : CarroPadrao
    {
        public double AutonomiaAlcool { get; private set; }
        public double AutonomiaAlcoolVariada { get; set; }
        public double QuantidadeAlcool { get; set; }

        public CarroFlex(string marca, string modelo, string placa, DateTime ano, double velocidadeMedia, double capacidadeTanque, int statusPneu, double autonomiaGasolina, double autonomiaAlcool)
            : base(marca, modelo, placa, ano, velocidadeMedia, capacidadeTanque, statusPneu, autonomiaGasolina)
        {
            AutonomiaAlcool = autonomiaAlcool;
            Flex = true;
        }
        public override void Abastecer()
        {
            Console.Write("\nDESEJA ABASTECER EM ALCOOL[1] OU GASOLINA[2]? ESCOLHA UMA OPÇÃO: ");
            int n = ControleDeInputs.ValidarNumeros(Console.ReadLine());
            if (n == 1)
            {
                Console.Write("\nDIGITE A QUANTIDADE EM LITROS A SER ABASTECIDO EM ALCOOL OU [0] PARA COMPLETAR: ");
                double litros = ControleDeInputs.ValidarLitrosAbastecimento(Console.ReadLine());
                if (litros == 0.0)
                {
                    QuantidadeAlcool = CapacidadeTanque - QuantidadeGasolina;
                    Console.WriteLine("\nABASTECIDO COM SUCESSO!");
                }
                else if (CapacidadeTanque >= litros && CapacidadeTanque >= QuantidadeGasolina + QuantidadeAlcool)
                {
                    QuantidadeAlcool += litros;
                    Console.WriteLine("\nABASTECIDO COM SUCESSO!");
                }
                else
                    Console.Write("\nIMPOSSÍVEL ABASTECER! QUANTIDADE SOLICITADA MAIOR DO QUE A CAPACIDADE DO TANQUE..");
            }
            else if (n == 2)
            {
                Console.Write("\nDIGITE A QUANTIDADE EM LITROS A SER ABASTECIDO EM GASOLINA OU [0] PARA COMPLETAR: ");
                double litros = ControleDeInputs.ValidarLitrosAbastecimento(Console.ReadLine());
                if (litros == 0.0)
                {
                    QuantidadeGasolina = CapacidadeTanque - QuantidadeAlcool;
                    Console.WriteLine("\nABASTECIDO COM SUCESSO!");
                }
                else if (CapacidadeTanque >= litros && CapacidadeTanque >= QuantidadeGasolina + QuantidadeAlcool)
                {
                    QuantidadeGasolina += litros;
                    Console.WriteLine("\nABASTECIDO COM SUCESSO!");
                }
                else
                    Console.Write("\nIMPOSSÍVEL ABASTECER! QUANTIDADE SOLICITADA MAIOR DO QUE A CAPACIDADE DO TANQUE..");
            }
            else Console.WriteLine("\nVOCÊ NÃO SELECIONOU NENHUMA OPÇÃO, OPERAÇÃO CANCELADA!");
        }
        public override void Calibrar(bool clima)
        {
            Console.Write("\nDESEJA CALIBRAR OS PNEUS? (S/N): ");
            string s = ControleDeInputs.ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
            if (s == "S")
            {
                Console.Write("\nCOMO DESEJA CALIBRAR OS PNEUS?\n\nCHEIO[1]\nMODERADO[2]\nMURCHO[3]\n\nESCOLHA UMA OPÇÃO: ");
                StatusPneu = ControleDeInputs.ValidarNumeros1A3(Console.ReadLine());

                AutonomiaGasolinaVariada = (clima ? AutonomiaGasolina : AutonomiaGasolina - AutonomiaGasolina * 0.12);
                AutonomiaAlcoolVariada = (clima ? AutonomiaAlcool : AutonomiaAlcool - AutonomiaAlcool * 0.135);

                if (StatusPneu == 2)
                {
                    AutonomiaGasolinaVariada -= AutonomiaGasolina * 0.0725;
                    AutonomiaAlcoolVariada -= AutonomiaAlcool * 0.0725;
                }
                else if (StatusPneu == 3)
                {
                    AutonomiaGasolinaVariada -= AutonomiaGasolina * 0.0915;
                    AutonomiaAlcoolVariada -= AutonomiaAlcool * 0.0915;
                }
            }
        }
        public override void Percorrer(Viagem viagem)
        {
            viagem.AdicionarVeiculo(this);
            double percorridoAlcool;
            double percorridoGasolina;

            AutonomiaGasolinaVariada = (viagem.Clima ? AutonomiaGasolina : AutonomiaGasolina - AutonomiaGasolina * 0.12);
            AutonomiaAlcoolVariada = (viagem.Clima ? AutonomiaAlcool : AutonomiaAlcool - AutonomiaAlcool * 0.135);

            if (StatusPneu == 2)
            {
                AutonomiaGasolinaVariada -= AutonomiaGasolina * 0.0725;
                AutonomiaAlcoolVariada -= AutonomiaAlcool * 0.0725;
            }
            else if (StatusPneu == 3)
            {
                AutonomiaGasolinaVariada -= AutonomiaGasolina * 0.0915;
                AutonomiaAlcoolVariada -= AutonomiaAlcool * 0.0915;
            }

            while (viagem.Distancia > 0.0)
            {
                /*------------------------------- ALCOOL -------------------------------*/
                if (viagem.Distancia >= QuantidadeAlcool * AutonomiaAlcool)
                {
                    percorridoAlcool = QuantidadeAlcool * AutonomiaAlcool;
                    QuantidadeAlcool -= percorridoAlcool / AutonomiaAlcool;

                    if (percorridoAlcool == viagem.Distancia)
                        Console.Write("\nVOCÊ CONCLUIU O PERCURSO!");
                    else
                    {
                        if (percorridoAlcool > 0.0) Console.Write($"\nVOCÊ PERCORREU {percorridoAlcool.ToString("F2", CultureInfo.InvariantCulture)}KM USANDO ALCOOL..");

                        if (QuantidadeAlcool == 0.0 && QuantidadeGasolina == 0.0)
                        {
                            Console.Write("\nO VEÍCULO ESTÁ SEM COMBUSTIVEL, DESEJA ABASTECER? (S/N): ");
                            string s = ControleDeInputs.ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                            if (s == "S")
                                Abastecer();
                            else
                            {
                                Console.WriteLine("\nOK, A VIAGEM FICARÁ PAUSADA!");
                                viagem.Distancia -= percorridoAlcool; return;
                            }
                        }
                    }
                    viagem.Distancia -= percorridoAlcool;
                }
                else
                {
                    QuantidadeAlcool -= viagem.Distancia / AutonomiaAlcool;
                    Console.WriteLine($"\nVOCÊ CONCLUIU O PERCURSO E O TANQUE AINDA ESTÁ COM {(QuantidadeGasolina + QuantidadeAlcool) / CapacidadeTanque * 100}% DE COMBUSTÍVEL!");
                    viagem.Distancia = 0.0; return;
                }
                /*------------------------------- GASOLINA -------------------------------*/
                if (viagem.Distancia >= QuantidadeGasolina * AutonomiaGasolina)
                {
                    percorridoGasolina = QuantidadeGasolina * AutonomiaGasolina;
                    QuantidadeGasolina -= percorridoGasolina / AutonomiaGasolina;

                    if (percorridoGasolina == viagem.Distancia)
                        Console.Write("\nVOCÊ CONCLUIU O PERCURSO!");
                    else
                    {
                        if (percorridoGasolina > 0) Console.Write($"\nVOCÊ PERCORREU {percorridoGasolina.ToString("F2", CultureInfo.InvariantCulture)}KM USANDO GASOLINA..");

                        if (QuantidadeAlcool == 0.0 && QuantidadeGasolina == 0.0)
                        {
                            Console.Write("\nO VEÍCULO ESTÁ SEM COMBUSTIVEL, DESEJA ABASTECER? (S/N): ");
                            string s = ControleDeInputs.ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                            if (s == "S")
                                Abastecer();
                            else
                            {
                                Console.WriteLine("\nOK, A VIAGEM FICARÁ PAUSADA!");
                                viagem.Distancia -= percorridoGasolina; return;
                            }
                        }
                    }
                    viagem.Distancia -= percorridoGasolina;
                }
                else
                {
                    QuantidadeGasolina -= viagem.Distancia / AutonomiaGasolina;
                    Console.WriteLine($"\nVOCÊ CONCLUIU O PERCURSO E O TANQUE AINDA ESTÁ COM {((QuantidadeGasolina + QuantidadeAlcool) / CapacidadeTanque * 100).ToString("F2", CultureInfo.InvariantCulture)}% DE COMBUSTÍVEL!");
                    viagem.Distancia = 0.0; return;
                }
            }
            Console.Write($"\nVOCÊ CONCLUIU O PERCURSO!");
        }
        public override string ToString()
        {
            StringBuilder printCarroPadrao = new StringBuilder();
            printCarroPadrao.AppendLine("-----------------------------------------------------------------------------");
            printCarroPadrao.AppendLine($"PLACA: {Placa} - MARCA: {Marca} - MODELO: {Modelo}");
            printCarroPadrao.AppendLine($"ANO: {Ano.ToString("yyyy")} - FLEX: {(Flex ? "SIM" : "NÃO")} - VELOCIDADE MÉDIA: {VelocidadeMedia.ToString("F2", CultureInfo.InvariantCulture)} KM/H");
            printCarroPadrao.AppendLine($"CAPACIDADE DO TANQUE: {CapacidadeTanque}L - STATUS TANQUE: {((QuantidadeAlcool + QuantidadeGasolina) / CapacidadeTanque * 100).ToString("F2", CultureInfo.InvariantCulture)}%");
            printCarroPadrao.AppendLine($"AUTONOMIA GASOLINA {AutonomiaGasolina.ToString("F2", CultureInfo.InvariantCulture)} KM/L - AUTONOMIA ALCOOL {AutonomiaAlcool.ToString("F2", CultureInfo.InvariantCulture)} KM/L");
            printCarroPadrao.Append("-----------------------------------------------------------------------------");
            return printCarroPadrao.ToString();
        }
    }
}
