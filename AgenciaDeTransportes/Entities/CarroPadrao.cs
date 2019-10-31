using System;
using System.Text;
using System.Globalization;

namespace AgenciaDeTransportes.Entities
{
    class CarroPadrao : Veiculo
    {
        public double AutonomiaGasolina { get; set; }
        public double QuantidadeGasolina { get; set; }
        public bool Flex { get; set; }

        public CarroPadrao(string marca, string modelo, string placa, DateTime ano, double velocidadeMaxima, double capacidadeTanque, double autonomiaGasolina)
            : base(marca, modelo, placa, ano, velocidadeMaxima, capacidadeTanque)
        {
            AutonomiaGasolina = autonomiaGasolina;
            Flex = false;
        }
        public override void Abastecer()
        {
            Console.Write("\nDIGITE A QUANTIDA A SER ABASTECIDO DE GASOLINA (LITROS) OU [0] PARA COMPLETAR: ");
            double litros = ControleDeInputs.ValidarLitrosAbastecimento(Console.ReadLine());

            if (litros == 0.0)
            {
                QuantidadeGasolina = CapacidadeTanque - QuantidadeGasolina;
                Console.WriteLine("\nABASTECIDO COM SUCESSO!");
            }
            else if (CapacidadeTanque >= litros && CapacidadeTanque >= QuantidadeGasolina)
            {
                QuantidadeGasolina += litros;
                Console.WriteLine("\nABASTECIDO COM SUCESSO!");
            }
            else
                Console.WriteLine("\nIMPOSSÍVEL ABASTECER! QUANTIDADE SOLICITADA MAIOR DO QUE A CAPACIDADE DO TANQUE..");
        }
        public override double Percorrer(double kilometros)
        {
            double percorrido;
            double distancia = kilometros;

            while (distancia > 0.0)
            {
                if (distancia >= QuantidadeGasolina * AutonomiaGasolina)
                {
                    percorrido = QuantidadeGasolina * AutonomiaGasolina;
                    QuantidadeGasolina -= percorrido / AutonomiaGasolina;

                    if (percorrido == distancia)
                        Console.Write("\nVOCÊ CONCLUIU O PERCURSO!");
                    else
                    {
                        Console.Write($"\nVOCÊ PERCORREU {percorrido}KM..");
                        if (QuantidadeGasolina == 0.0)
                        {
                            Console.Write("\nO VEÍCULO ESTÁ SEM COMBUSTIVEL, DESEJA ABASTECER? (S/N): ");
                            string s = ControleDeInputs.ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                            if (s == "S")
                                Abastecer();
                            else
                            {
                                Console.WriteLine("\nOK, A VIAGEM FICARÁ PAUSADA!");
                                return distancia -= percorrido;
                            }
                        }
                    }
                    distancia -= percorrido;
                }
                else
                {
                    QuantidadeGasolina -= distancia / AutonomiaGasolina;
                    Console.Write($"\nVOCÊ CONCLUIU O PERCURSO E O TANQUE AINDA ESTÁ COM {(QuantidadeGasolina / CapacidadeTanque * 100).ToString("F2",CultureInfo.InvariantCulture)}% DE COMBUSTÍVEL!");
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
            printCarroPadrao.AppendLine($"ANO: {Ano.ToString("yyyy")} - FLEX: {(Flex ? "SIM" : "NÃO")} - VELOCIDADE MAX: {VelocidadeMaxima.ToString("F2", CultureInfo.InvariantCulture)} KM/H");
            printCarroPadrao.AppendLine($"CAPACIDADE DO TANQUE: {CapacidadeTanque}L - STATUS TANQUE: {(QuantidadeGasolina / CapacidadeTanque * 100).ToString("F2", CultureInfo.InvariantCulture)}%");
            printCarroPadrao.AppendLine($"AUTONOMIA GASOLINA {AutonomiaGasolina.ToString("F2", CultureInfo.InvariantCulture)} KM/L");
            printCarroPadrao.Append("-----------------------------------------------------------------------------");
            return printCarroPadrao.ToString();
        }
    }
}
