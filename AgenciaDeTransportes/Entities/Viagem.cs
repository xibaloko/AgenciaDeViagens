using System;
using System.Text;
using System.Globalization;

namespace AgenciaDeTransportes.Entities
{
    public class Viagem
    {
        public int CodigoViagem { get; private set; } = new Random().Next(1000, 9999);
        public double Distancia { get; set; }
        public double DistanciaVariada { get; set; }
        public DateTime Data { get; private set; }
        public TimeSpan Duracao { get; set; }
        public string Clima { get; private set; }
        public string ClimaVariado { get; set; }

        public Viagem(double distancia, DateTime data, string clima)
        {
            Distancia = distancia;
            DistanciaVariada = distancia;
            Data = data;
            Clima = clima;
        }

        public void MudarClima()
        {
            int random = new Random().Next(0, 3);
            if (random == 0)
                ClimaVariado = "ENSOLARADO";
            else if (random == 1)
                ClimaVariado = "CHOVENDO";
            else 
                ClimaVariado = "NEVANDO";
        }

        public void DuracaoPrevista(Veiculo veiculo) => Duracao = TimeSpan.FromHours(Distancia / veiculo.VelocidadeMedia);

        public override string ToString()
        {
            StringBuilder printViagem = new StringBuilder();
            printViagem.AppendLine($"CÓDIGO DA VIAGEM: {CodigoViagem}");
            printViagem.AppendLine($"DISTÂNCIA: {Distancia.ToString("F2", CultureInfo.InvariantCulture)} KM - DATA: {Data.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}");
            printViagem.AppendLine($"STATUS VIAGEM: {(DistanciaVariada <= 0 ? "CONCLUIDA" : "PENDENTE")} - PREVISÃO DO TEMPO: {Clima}");
            return printViagem.ToString();
        }
    }
}
