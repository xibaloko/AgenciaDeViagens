using System;
using System.Text;
using System.Globalization;

namespace AgenciaDeTransportes.Entities
{
    class CarroFlex : CarroPadrao
    {
        public double AutonomiaAlcool { get; private set; }
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
            else
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
        }
        public override double Percorrer(double kilometros, bool clima)
        {
            double percorridoAlcool;
            double percorridoGasolina;
            double distancia = kilometros;

            AutonomiaAlcool = (clima ? AutonomiaAlcool : AutonomiaAlcool -= AutonomiaAlcool * 0.135);
            AutonomiaGasolina = (clima ? AutonomiaGasolina : AutonomiaGasolina -= AutonomiaGasolina * 0.12);

            while (distancia > 0.0)
            {
                /*------------------------------- ALCOOL -------------------------------*/
                if (distancia >= QuantidadeAlcool * AutonomiaAlcool)
                {
                    percorridoAlcool = QuantidadeAlcool * AutonomiaAlcool;
                    QuantidadeAlcool -= percorridoAlcool / AutonomiaAlcool;

                    if (percorridoAlcool == distancia)
                        Console.Write("\nVOCÊ CONCLUIU O PERCURSO!");
                    else
                    {
                        Console.Write($"\nVOCÊ PERCORREU {percorridoAlcool}KM USANDO ALCOOL..");
                        if (QuantidadeAlcool == 0.0 && QuantidadeGasolina == 0.0)
                        {
                            Console.Write("\nO VEÍCULO ESTÁ SEM COMBUSTIVEL, DESEJA ABASTECER? (S/N): ");
                            string s = ControleDeInputs.ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                            if (s == "S")
                                Abastecer();
                            else
                            {
                                Console.WriteLine("\nOK, A VIAGEM FICARÁ PAUSADA!");
                                return distancia -= percorridoAlcool;
                            }
                        }
                    }
                    distancia -= percorridoAlcool;
                }
                else
                {
                    QuantidadeAlcool -= distancia / AutonomiaAlcool;
                    Console.Write($"\nVOCÊ CONCLUIU O PERCURSO E O TANQUE AINDA ESTÁ COM {(QuantidadeGasolina + QuantidadeAlcool) / CapacidadeTanque * 100}% DE COMBUSTÍVEL!");
                    return 0.0;
                }
                /*------------------------------- GASOLINA -------------------------------*/
                if (distancia >= QuantidadeGasolina * AutonomiaGasolina)
                {
                    percorridoGasolina = QuantidadeGasolina * AutonomiaGasolina;
                    QuantidadeGasolina -= percorridoGasolina / AutonomiaGasolina;

                    if (percorridoGasolina == distancia)
                        Console.Write("\nVOCÊ CONCLUIU O PERCURSO!");
                    else
                    {
                        Console.Write($"\nVOCÊ PERCORREU {percorridoGasolina}KM USANDO GASOLINA..");
                        if (QuantidadeAlcool == 0.0 && QuantidadeGasolina == 0.0)
                        {
                            Console.Write("\nO VEÍCULO ESTÁ SEM COMBUSTIVEL, DESEJA ABASTECER? (S/N): ");
                            string s = ControleDeInputs.ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                            if (s == "S")
                                Abastecer();
                            else
                            {
                                Console.WriteLine("\nOK, A VIAGEM FICARÁ PAUSADA!");
                                return distancia -= percorridoGasolina;
                            }
                        }
                    }
                    distancia -= percorridoGasolina;
                }
                else
                {
                    QuantidadeGasolina -= distancia / AutonomiaGasolina;
                    Console.Write($"\nVOCÊ CONCLUIU O PERCURSO E O TANQUE AINDA ESTÁ COM {((QuantidadeGasolina + QuantidadeAlcool) / CapacidadeTanque * 100).ToString("F2",CultureInfo.InvariantCulture)}% DE COMBUSTÍVEL!");
                    return 0.0;
                }
            }
            Console.Write($"\nVOCÊ CONCLUIU O PERCURSO!");
            return distancia;
        }
        public override string ToString()
        {
            StringBuilder printCarroPadrao = new StringBuilder();
            printCarroPadrao.AppendLine("-----------------------------------------------------------------------------");
            printCarroPadrao.AppendLine($"PLACA: {Placa.ToUpper()} - MARCA: {Marca.ToUpper()} - MODELO: {Modelo.ToUpper()}");
            printCarroPadrao.AppendLine($"ANO: {Ano.ToString("yyyy")} - FLEX: {(Flex ? "SIM" : "NÃO")} - VELOCIDADE MAX: {VelocidadeMedia.ToString("F2", CultureInfo.InvariantCulture)} KM/H");
            printCarroPadrao.AppendLine($"CAPACIDADE DO TANQUE: {CapacidadeTanque}L - STATUS TANQUE: {((QuantidadeAlcool + QuantidadeGasolina) / CapacidadeTanque * 100).ToString("F2", CultureInfo.InvariantCulture)}%");
            printCarroPadrao.AppendLine($"AUTONOMIA GASOLINA {AutonomiaGasolina.ToString("F2", CultureInfo.InvariantCulture)} - AUTONOMIA ALCOOL {AutonomiaAlcool.ToString("F2", CultureInfo.InvariantCulture)}");
            printCarroPadrao.Append("-----------------------------------------------------------------------------");
            return printCarroPadrao.ToString();
        }
    }
}
