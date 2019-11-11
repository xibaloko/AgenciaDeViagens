using System;
using System.Text;
using System.Globalization;
using static AgenciaDeTransportes.ControleDeInputs;

namespace AgenciaDeTransportes.Entities
{
    class CarroPadrao : Veiculo
    {
        public double AutonomiaGasolina { get; set; }
        public double AutonomiaGasolinaVariada { get; set; }
        public double QuantidadeGasolina { get; set; }
        public bool Flex { get; set; }

        public CarroPadrao(string marca, string modelo, string placa, DateTime ano, double velocidadeMedia, double capacidadeTanque, int statusPneu, double autonomiaGasolina)
            : base(marca, modelo, placa, ano, velocidadeMedia, capacidadeTanque, statusPneu)
        {
            AutonomiaGasolina = autonomiaGasolina;
            Flex = false;
        }
        public override void Abastecer()
        {
            Console.Write("\nDIGITE A QUANTIDA A SER ABASTECIDO DE GASOLINA (LITROS) OU [0] PARA COMPLETAR: ");
            double litros = ValidarLitrosAbastecimento(Console.ReadLine());

            if (litros == 0.0)
            {
                QuantidadeGasolina = CapacidadeTanque - QuantidadeGasolina;
                Console.WriteLine("\nABASTECIDO COM SUCESSO!");
            }
            else if (CapacidadeTanque >= QuantidadeGasolina + litros)
            {
                QuantidadeGasolina += litros;
                Console.WriteLine("\nABASTECIDO COM SUCESSO!");
            }
            else
                Console.WriteLine("\nIMPOSSÍVEL ABASTECER! QUANTIDADE SOLICITADA MAIOR DO QUE A CAPACIDADE DO TANQUE..");
        }

        public override void AutonomiaClima(Viagem viagem)
        {
            if (viagem.Clima.Equals("ENSOLARADO"))
                AutonomiaGasolinaVariada = AutonomiaGasolina;
            else if (viagem.Clima.Equals("CHOVENDO"))
                AutonomiaGasolinaVariada = AutonomiaGasolina - AutonomiaGasolina * 0.12;
            else
                AutonomiaGasolinaVariada = AutonomiaGasolina - AutonomiaGasolina * 0.19;
        }

        public override void AutonomiaPneu()
        {
            if (StatusPneu == 2)
                AutonomiaGasolinaVariada -= AutonomiaGasolina * 0.0725;
            else if (StatusPneu == 1)
                AutonomiaGasolinaVariada -= AutonomiaGasolina * 0.0915;
        }

        public override string ToString()
        {
            StringBuilder printCarroPadrao = new StringBuilder();
            printCarroPadrao.AppendLine("-----------------------------------------------------------------------------");
            printCarroPadrao.AppendLine($"PLACA: {Placa} - MARCA: {Marca} - MODELO: {Modelo}");
            printCarroPadrao.AppendLine($"ANO: {Ano.ToString("yyyy")} - FLEX: {(Flex ? "SIM" : "NÃO")} - VELOCIDADE MÉDIA: {VelocidadeMedia.ToString("F2", CultureInfo.InvariantCulture)} KM/H");
            printCarroPadrao.AppendLine($"CAPACIDADE DO TANQUE: {CapacidadeTanque}L - STATUS TANQUE: {(QuantidadeGasolina / CapacidadeTanque * 100).ToString("F2", CultureInfo.InvariantCulture)}%");
            printCarroPadrao.AppendLine($"AUTONOMIA GASOLINA {AutonomiaGasolina.ToString("F2", CultureInfo.InvariantCulture)} KM/L");
            printCarroPadrao.Append("-----------------------------------------------------------------------------");
            return printCarroPadrao.ToString();
        }
    }
}
