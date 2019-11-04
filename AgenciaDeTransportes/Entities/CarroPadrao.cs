using System;
using System.Text;
using System.Globalization;

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
        public override void Calibrar(bool clima)
        {
            Console.Write("\nDESEJA CALIBRAR OS PNEUS? (S/N): ");
            string s = ControleDeInputs.ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
            if (s == "S")
            {
                Console.Write("\nCOMO DESEJA CALIBRAR OS PNEUS?\n\nCHEIO[1]\nMODERADO[2]\nMURCHO[3]\n\nESCOLHA UMA OPÇÃO: ");
                StatusPneu = ControleDeInputs.ValidarNumeros1A3(Console.ReadLine());

                AutonomiaGasolinaVariada = (clima ? AutonomiaGasolina : AutonomiaGasolina - AutonomiaGasolina * 0.12);

                if (StatusPneu == 2)
                    AutonomiaGasolinaVariada -= AutonomiaGasolina * 0.0725;
                else if (StatusPneu == 3)
                    AutonomiaGasolinaVariada -= AutonomiaGasolina * 0.0915;
            }
        }
        public override void Percorrer(Viagem viagem)
        {
            viagem.AdicionarVeiculo(this);
            double percorrido;

            AutonomiaGasolinaVariada = (viagem.Clima ? AutonomiaGasolina : AutonomiaGasolina - AutonomiaGasolina * 0.12);

            if (StatusPneu == 2)
                AutonomiaGasolinaVariada -= AutonomiaGasolina * 0.0725;
            else if (StatusPneu == 3)
                AutonomiaGasolinaVariada -= AutonomiaGasolina * 0.0915;

            while (viagem.Distancia > 0.0)
            {
                if (viagem.Distancia >= QuantidadeGasolina * AutonomiaGasolinaVariada)
                {
                    percorrido = QuantidadeGasolina * AutonomiaGasolinaVariada;
                    QuantidadeGasolina -= percorrido / AutonomiaGasolinaVariada;

                    if (percorrido == viagem.Distancia)
                        Console.Write("\nVOCÊ CONCLUIU O PERCURSO!");
                    else
                    {
                        Console.Write($"\nVOCÊ PERCORREU {percorrido.ToString("F2", CultureInfo.InvariantCulture)}KM..");
                        if (QuantidadeGasolina == 0.0)
                        {
                            Console.Write("\nO VEÍCULO ESTÁ SEM COMBUSTIVEL, DESEJA ABASTECER? (S/N): ");
                            string s = ControleDeInputs.ValidarOpcoesSOuN(Console.ReadLine().ToUpper());
                            if (s == "S")
                            {
                                Abastecer();
                                Calibrar(viagem.Clima);
                            }
                            else
                            {
                                Console.WriteLine("\nOK, A VIAGEM FICARÁ PAUSADA!");
                                viagem.Distancia -= percorrido; return;
                            }
                        }
                    }
                    viagem.Distancia -= percorrido;
                }
                else
                {
                    QuantidadeGasolina -= viagem.Distancia / AutonomiaGasolinaVariada;
                    Console.Write($"\nVOCÊ CONCLUIU O PERCURSO E O TANQUE AINDA ESTÁ COM {(QuantidadeGasolina / CapacidadeTanque * 100).ToString("F2", CultureInfo.InvariantCulture)}% DE COMBUSTÍVEL!");
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
            printCarroPadrao.AppendLine($"CAPACIDADE DO TANQUE: {CapacidadeTanque}L - STATUS TANQUE: {(QuantidadeGasolina / CapacidadeTanque * 100).ToString("F2", CultureInfo.InvariantCulture)}%");
            printCarroPadrao.AppendLine($"AUTONOMIA GASOLINA {AutonomiaGasolina.ToString("F2", CultureInfo.InvariantCulture)} KM/L");
            printCarroPadrao.Append("-----------------------------------------------------------------------------");
            return printCarroPadrao.ToString();
        }
    }
}
