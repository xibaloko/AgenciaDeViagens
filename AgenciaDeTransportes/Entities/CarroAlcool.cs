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
            else if (CapacidadeTanque >= litros && CapacidadeTanque >= QuantidadeAlcool)
            {
                QuantidadeAlcool += litros;
                Console.WriteLine("\nABASTECIDO COM SUCESSO!");
            }
            else
                Console.WriteLine("\nIMPOSSÍVEL ABASTECER! QUANTIDADE SOLICITADA MAIOR DO QUE A CAPACIDADE DO TANQUE..");
        }

        public override void AutonomiaClima(Viagem viagem)
        {
            throw new NotImplementedException();
        }

        public override void AutonomiaPneu()
        {
            throw new NotImplementedException();
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
