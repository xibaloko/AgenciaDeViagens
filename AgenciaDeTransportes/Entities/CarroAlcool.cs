using System;
using System.Text;
using System.Globalization;
using static AgenciaDeTransportes.ControleDeInputs;

namespace AgenciaDeTransportes.Entities
{
    class CarroAlcool : Veiculo
    {
        public double AutonomiaAlcool { get; set; }
        public double AutonomiaAlcoolVariada { get; set; }
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
            double litros = ValidarLitrosAbastecimento(Console.ReadLine());

            if (litros == 0.0)
            {
                QuantidadeAlcool = CapacidadeTanque - QuantidadeAlcool;
                Console.WriteLine("\nABASTECIDO COM SUCESSO!");
            }
            else if (CapacidadeTanque >= QuantidadeAlcool + litros)
            {
                QuantidadeAlcool += litros;
                Console.WriteLine("\nABASTECIDO COM SUCESSO!");
            }
            else
                Console.WriteLine("\nIMPOSSÍVEL ABASTECER! QUANTIDADE SOLICITADA MAIOR DO QUE A CAPACIDADE DO TANQUE..");
        }

        public override void AutonomiaClima(Viagem viagem)
        {
            if (viagem.Clima.Equals("ENSOLARADO"))
                AutonomiaAlcoolVariada = AutonomiaAlcool;
            else if (viagem.Clima.Equals("CHOVENDO"))
                AutonomiaAlcoolVariada = AutonomiaAlcool - AutonomiaAlcool * 0.156;
            else
                AutonomiaAlcoolVariada = AutonomiaAlcool - AutonomiaAlcool * 0.247;
        }

        public override void AutonomiaPneu()
        {
            if (StatusPneu == 2)
                AutonomiaAlcoolVariada -= AutonomiaAlcool * 0.0725;
            else if (StatusPneu == 1)
                AutonomiaAlcoolVariada -= AutonomiaAlcool * 0.0915;
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
