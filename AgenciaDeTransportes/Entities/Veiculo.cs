using System;

namespace AgenciaDeTransportes.Entities
{
    abstract class Veiculo
    {
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public string Placa { get; private set; }
        public DateTime Ano { get; private set; }
        public double VelocidadeMedia { get; private set; }
        public double CapacidadeTanque { get; private set; }
        public int StatusPneu { get; set; }

        protected Veiculo(string marca, string modelo, string placa, DateTime ano, double velocidadeMedia, double capacidadeTanque, int statusPneu)
        {
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
            Ano = ano;
            VelocidadeMedia = velocidadeMedia;
            CapacidadeTanque = capacidadeTanque;
            StatusPneu = statusPneu;
        }

        public abstract void Abastecer();
        public abstract void Calibrar(bool clima);
        public abstract void Percorrer(Viagem viagem);
    }
}
