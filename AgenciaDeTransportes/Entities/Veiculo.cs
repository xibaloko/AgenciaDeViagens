using System;

namespace AgenciaDeTransportes.Entities
{
    abstract class Veiculo
    {
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public string Placa { get; private set; }
        public DateTime Ano { get; private set; }
        public double VelocidadeMaxima { get; private set; }
        public double CapacidadeTanque { get; private set; }

        protected Veiculo(string marca, string modelo, string placa, DateTime ano, double velocidadeMaxima, double capacidadeTanque)
        {
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
            Ano = ano;
            VelocidadeMaxima = velocidadeMaxima;
            CapacidadeTanque = capacidadeTanque;
        }

        public abstract void Abastecer();
        public abstract double Percorrer(double kilometros);
    }
}
