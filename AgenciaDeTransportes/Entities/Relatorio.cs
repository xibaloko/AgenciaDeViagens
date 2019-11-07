using System;
using System.Collections.Generic;
using System.Text;

namespace AgenciaDeTransportes.Entities
{
    class Relatorio
    { 
        public VeiculoViagem VeiculoViagem { get; private set; }
        public List<string> MundancasClimaticas { get; private set; } = new List<string>();
        public List<string> MudancasPneu { get; private set; } = new List<string>();
        public double LitrosConsumido { get; private set; }
        public double DistanciaPercorrida { get; private set; }
        public int ParadasAbastecimento { get; private set; }
        public int ParadasCalibragem { get; private set; }

        public Relatorio(VeiculoViagem veiculoViagem)
        {
            VeiculoViagem = veiculoViagem;
        }

        public void AdicionarOcorrenciaClimatica(double km, string clima)
        {
            string ocorrencia = $"MUDANÇA CLIMÁTICA NO KM {km}, CLIMA: {clima}";
            MundancasClimaticas.Add(ocorrencia);
        }
        public void AdicionarOcorrenciaPneus(double km, int statusPneu)
        {
            string pneu = statusPneu == 1 ? "CHEIO" : statusPneu == 2 ? "MODERADO" : "MURCHO";
            string ocorrencia = $"PNEU DESCALIBRADO NO KM {km}, FOI CALIBRADO PARA ({pneu})";
            MudancasPneu.Add(ocorrencia);
        }
        public void AdicionarDistanciaPercorrida(double km) => DistanciaPercorrida = km;

        public void AdicionarParadasAbastecimento(int vezes) => ParadasAbastecimento = vezes;

        public void AdicionarCombustivelGasto(double totalCombustivel) => LitrosConsumido = totalCombustivel;

        public void AdicionarParadasCalibragem(int vezes) => ParadasCalibragem = vezes;

        public override string ToString()
        {
            StringBuilder relatorio = new StringBuilder();
            relatorio.AppendLine($"COMBUSTÍVEL CONSUMIDO: {LitrosConsumido} DISTÂNCIA PERCORRIDA: {DistanciaPercorrida} KM");
            relatorio.AppendLine($"PARADAS ABASTECIMENTO: {ParadasAbastecimento} - PARADAS CALIBRAGEM: {ParadasCalibragem}");
            relatorio.AppendLine("-- MUDANÇAS CLIMÁTICAS --");
            MundancasClimaticas.ForEach(x => relatorio.AppendLine(x));
            relatorio.AppendLine("-- DESCALIBRAGENS --");
            MudancasPneu.ForEach(x => relatorio.AppendLine(x));
            return relatorio.ToString();
        }
    }
}
