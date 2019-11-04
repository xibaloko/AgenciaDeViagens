using System;
using System.Text;
using System.Globalization;

namespace AgenciaDeTransportes.Entities
{
    class Viagem
    {
        public int CodigoViagem { get; private set; } = new Random().Next(1000, 9999);
        public double Distancia { get; set; }
        public DateTime Data { get; private set; }
        public TimeSpan Duracao { get; set; }
        public Veiculo Veiculo { get; private set; }
        public bool Clima { get; private set; }

        public Viagem(double distancia, DateTime data, bool clima, Veiculo veiculo)
        {
            Distancia = distancia;
            Data = data;
            Clima = clima;
            Veiculo = veiculo;
            Duracao = TimeSpan.FromHours(distancia / veiculo.VelocidadeMedia);
        }

        public void AdicionarVeiculo(Veiculo veiculo)
        {
            Veiculo = veiculo;
            Duracao = TimeSpan.FromHours(Distancia / veiculo.VelocidadeMedia);
        }

        public override string ToString()
        {
            StringBuilder printViagem = new StringBuilder();
            printViagem.AppendLine($"CÓDIGO DA VIAGEM: {CodigoViagem}");
            printViagem.AppendLine($"DISTÂNCIA A PERCORRER: {Distancia.ToString("F2", CultureInfo.InvariantCulture)} KM - DATA: {Data.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}");
            printViagem.AppendLine($"STATUS VIAGEM: {(Distancia == 0 ? "CONCLUIDA" : "PENDENTE")} - CLIMA: {(Clima ? "BOM" : "RUIM")}");
            printViagem.AppendLine($"DURAÇÃO ESTIMADA: {Duracao}");
            printViagem.AppendLine($"VEÍCULO PROGRAMADO: {Veiculo.Marca.ToUpper()} - {Veiculo.Modelo.ToUpper()} - PLACA: {Veiculo.Placa.ToUpper()}");

            return printViagem.ToString();
        }
    }
}
