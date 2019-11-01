using System;
using System.Text;
using System.Globalization;

namespace AgenciaDeTransportes.Entities
{
    class CarroAlcool : Veiculo
    {
        public double AutonomiaAlcool { get; set; }
        public double QuantidadeAlcool { get; set; }
        public bool Flex { get; set; }

        public CarroAlcool(string marca, string modelo, string placa, DateTime ano, double velocidadeMedia, double capacidadeTanque, int statusPneu, double autonomiaAlcool)
            : base(marca, modelo, placa, ano, velocidadeMedia, capacidadeTanque, statusPneu)
        {
            AutonomiaAlcool = autonomiaAlcool;
            Flex = false;
        }
        public override void Abastecer()
        {
            Console.Write("\nDIGITE A QUANTIDADE A SER ABASTECIDO EM ALCOOL (LITROS) OU [0] PARA COMPLETAR: ");
            double litros = ControleDeInputs.ValidarLitrosAbastecimento(Console.ReadLine());

            if (litros == 0.0)
            {
                QuantidadeAlcool = CapacidadeTanque - QuantidadeAlcool;
                Console.WriteLine("\nABASTECIDO COM SUCESSO!");
            }
            else if (CapacidadeTanque >= litros && CapacidadeTanque >= QuantidadeAlcool)
            {
                QuantidadeAlcool += litros;
                Console.WriteLine("\nABASTECIDO COM SUCESSO!");
            }
            else
                Console.WriteLine("\nIMPOSSÍVEL ABASTECER! QUANTIDADE SOLICITADA MAIOR DO QUE A CAPACIDADE DO TANQUE..");
        }
        public override double Percorrer(double kilometros, bool clima)
        {
            double percorrido;
            double distancia = kilometros;

            AutonomiaAlcool = (clima ? AutonomiaAlcool : AutonomiaAlcool -= AutonomiaAlcool * 0.135);

            if (StatusPneu == 2)
                AutonomiaAlcool -= AutonomiaAlcool * 0.0725;
            else if (StatusPneu == 3)
                AutonomiaAlcool -= AutonomiaAlcool * 0.0915;
            else
                AutonomiaAlcool = AutonomiaAlcool;

            while (distancia > 0.0)
            {
                if (distancia >= QuantidadeAlcool * AutonomiaAlcool)
                {
                    percorrido = QuantidadeAlcool * AutonomiaAlcool;
                    QuantidadeAlcool -= percorrido / AutonomiaAlcool;

                    if (percorrido == distancia)
                        Console.Write("\nVOCÊ CONCLUIU O PERCURSO!");
                    else
                    {
                        Console.Write($"\nVOCÊ PERCORREU {percorrido}KM..");
                        if (QuantidadeAlcool == 0.0)
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
                    QuantidadeAlcool -= distancia / AutonomiaAlcool;
                    Console.Write($"\nVOCÊ CONCLUIU O PERCURSO E O TANQUE AINDA ESTÁ COM {(QuantidadeAlcool / CapacidadeTanque * 100).ToString("F2",CultureInfo.InvariantCulture)}% DE COMBUSTÍVEL!");
                    return 0.0;
                }
            }
            Console.Write($"\nVOCÊ CONCLUIU O PERCURSO!");
            return distancia;
        }
        public override string ToString()
        {
            StringBuilder printCarroAlcool = new StringBuilder();
            printCarroAlcool.AppendLine("-----------------------------------------------------------------------------");
            printCarroAlcool.AppendLine($"PLACA: {Placa} - MARCA: {Marca} - MODELO: {Modelo}");
            printCarroAlcool.AppendLine($"ANO: {Ano.ToString("yyyy")} - FLEX: {(Flex ? "SIM" : "NÃO")} - VELOCIDADE MÉDIA: {VelocidadeMedia.ToString("F2", CultureInfo.InvariantCulture)} KM/H");
            printCarroAlcool.AppendLine($"CAPACIDADE DO TANQUE: {CapacidadeTanque}L - STATUS TANQUE: {(QuantidadeAlcool / CapacidadeTanque * 100).ToString("F2", CultureInfo.InvariantCulture)}%");
            printCarroAlcool.AppendLine($"AUTONOMIA GASOLINA {AutonomiaAlcool.ToString("F2", CultureInfo.InvariantCulture)} KM/L");
            printCarroAlcool.Append("-----------------------------------------------------------------------------");
            return printCarroAlcool.ToString();
        }
    }
}
