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
        public string Clima { get; private set; }
        public string ClimaVariado { get; set; }

        public Viagem(double distancia, DateTime data, string clima)
        {
            Distancia = distancia;
            Data = data;
            Clima = clima;
        }

        public void MudarClima()
        {
            int random = new Random().Next(0, 2);
            ClimaVariado = random == 0 ? "ENSOLARADO" : random == 1 ? "CHOVENDO" : "NEVANDO";
        }

        public override string ToString()
        {
            StringBuilder printViagem = new StringBuilder();
            printViagem.AppendLine($"CÓDIGO DA VIAGEM: {CodigoViagem}");
            printViagem.AppendLine($"DISTÂNCIA A PERCORRER: {Distancia.ToString("F2", CultureInfo.InvariantCulture)} KM - DATA: {Data.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}");
            printViagem.AppendLine($"STATUS VIAGEM: {(Distancia == 0 ? "CONCLUIDA" : "PENDENTE")} - CLIMA: {Clima}");
            printViagem.AppendLine($"DURAÇÃO ESTIMADA: {Duracao}");

            return printViagem.ToString();
        }
    }
}
