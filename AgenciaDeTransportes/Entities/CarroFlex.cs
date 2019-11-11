using System;
using System.Text;
using System.Globalization;
using static AgenciaDeTransportes.ControleDeInputs;

namespace AgenciaDeTransportes.Entities
{
    class CarroFlex : Veiculo
    {
        public double AutonomiaGasolina { get; set; }
        public double AutonomiaGasolinaVariada { get; set; }
        public double QuantidadeGasolina { get; set; }
        public double AutonomiaAlcool { get; private set; }
        public double AutonomiaAlcoolVariada { get; set; }
        public double QuantidadeAlcool { get; set; }
        public bool Flex { get; set; }

        public CarroFlex(string marca, string modelo, string placa, DateTime ano, double velocidadeMedia, double capacidadeTanque, int statusPneu, double autonomiaGasolina, double autonomiaAlcool)
            : base(marca, modelo, placa, ano, velocidadeMedia, capacidadeTanque, statusPneu)
        {
            AutonomiaGasolina = autonomiaGasolina;
            AutonomiaAlcool = autonomiaAlcool;
            Flex = true;
        }
        public override void Abastecer()
        {
            Console.Write("\nDESEJA ABASTECER EM ALCOOL[1] OU GASOLINA[2]? ESCOLHA UMA OPÇÃO: ");
            int n = ValidarNumeros(Console.ReadLine());
            if (n == 1)
            {
                Console.Write("\nDIGITE A QUANTIDADE EM LITROS A SER ABASTECIDO EM ALCOOL OU [0] PARA COMPLETAR: ");
                double litros = ValidarLitrosAbastecimento(Console.ReadLine());
                if (litros == 0.0)
                {
                    QuantidadeAlcool = CapacidadeTanque - QuantidadeGasolina - QuantidadeAlcool;
                    Console.WriteLine("\nABASTECIDO COM SUCESSO!");
                }
                else if (CapacidadeTanque >= QuantidadeGasolina + QuantidadeAlcool + litros)
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
                double litros = ValidarLitrosAbastecimento(Console.ReadLine());
                if (litros == 0.0)
                {
                    QuantidadeGasolina = CapacidadeTanque - QuantidadeAlcool - QuantidadeGasolina;
                    Console.WriteLine("\nABASTECIDO COM SUCESSO!");
                }
                else if (CapacidadeTanque >= QuantidadeGasolina + QuantidadeAlcool + litros)
                {
                    QuantidadeGasolina += litros;
                    Console.WriteLine("\nABASTECIDO COM SUCESSO!");
                }
                else
                    Console.Write("\nIMPOSSÍVEL ABASTECER! QUANTIDADE SOLICITADA MAIOR DO QUE A CAPACIDADE DO TANQUE..");
            }
            else Console.WriteLine("\nVOCÊ NÃO SELECIONOU NENHUMA OPÇÃO, OPERAÇÃO CANCELADA!");
        }

        public override void AutonomiaClima(Viagem viagem)
        {
            if (viagem.Clima.Equals("ENSOLARADO"))
            {
                AutonomiaGasolinaVariada = AutonomiaGasolina;
                AutonomiaAlcoolVariada = AutonomiaAlcool;
            }
            else if (viagem.Clima.Equals("CHOVENDO"))
            {
                AutonomiaGasolinaVariada = AutonomiaGasolina - AutonomiaGasolina * 0.12;
                AutonomiaAlcoolVariada = AutonomiaGasolinaVariada - AutonomiaGasolinaVariada * 0.30;
            }
            else
            {
                AutonomiaGasolinaVariada = AutonomiaGasolina - AutonomiaGasolina * 0.19;
                AutonomiaAlcoolVariada = AutonomiaGasolinaVariada - AutonomiaGasolinaVariada * 0.30;
            }
        }

        public override void AutonomiaPneu()
        {
            if (StatusPneu == 2)
            {
                AutonomiaGasolinaVariada -= AutonomiaGasolina * 0.0725;
                AutonomiaAlcoolVariada -= AutonomiaAlcool * 0.0725;
            }
            else if (StatusPneu == 1)
            {
                AutonomiaGasolinaVariada -= AutonomiaGasolina * 0.0915;
                AutonomiaAlcoolVariada -= AutonomiaAlcool * 0.0915;
            }
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
