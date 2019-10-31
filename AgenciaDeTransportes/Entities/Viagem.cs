using System;
using System.Text;
using System.Globalization;

namespace AgenciaDeTransportes.Entities
{
    class Viagem
    {
        public int CodigoViagem { get; set; } = new Random().Next(1000, 9999);
        public double Distancia { get; private set; }
        public DateTime Data { get; private set; }
        public Veiculo Veiculo { get; private set; }

        public Viagem(double distancia, DateTime data)
        {
            Distancia = distancia;
            Data = data;
        }
        public void AdicionarVeiculo(Veiculo veiculo) => Veiculo = veiculo;

        public void AtualizarDistancia(double distanciaRestante) => Distancia = distanciaRestante;

        public override string ToString()
        {
            StringBuilder printViagem = new StringBuilder();
            printViagem.AppendLine($"CÓDIGO DA VIAGEM: {CodigoViagem}");
            printViagem.AppendLine($"DISTÂNCIA A PERCORRER: {Distancia.ToString("F2", CultureInfo.InvariantCulture)} KM - DATA: {Data.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}");
            printViagem.AppendLine($"STATUS VIAGEM: {(Distancia == 0 ? "CONCLUIDA" : "PENDENTE")}");
            if (Veiculo != null)
                printViagem.AppendLine($"VEÍCULO USADO: {Veiculo.Marca.ToUpper()} - {Veiculo.Modelo.ToUpper()} - PLACA: {Veiculo.Placa.ToUpper()}");
            else
                printViagem.AppendLine("NENHUM VEÍCULO PROGRAMADO PARA ESSA VIAGEM!");
            return printViagem.ToString();
        }
    }
}
