using System;
using static AgenciaDeTransportes.Servicos.ControleDeInputs;

namespace AgenciaDeTransportes.Entities
{
    public abstract class Veiculo
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

        public void Calibrar()
        {
            Console.Write("\nCOMO DESEJA CALIBRAR OS PNEUS?\n\nMURCHO[1]\nMODERADO[2]\nCHEIO[3]\n\nESCOLHA UMA OPÇÃO: ");
            StatusPneu = ValidarNumeros1A3(Console.ReadLine());
        }

        public void DescalibrarPneu()
        {
            int chanceDescalibrar = new Random().Next(0, 2);
            if (chanceDescalibrar == 0)
                StatusPneu--;
        }

        public abstract void Abastecer();

        public abstract void AutonomiaClima(Viagem viagem);

        public abstract void AutonomiaPneu();

        public void ExibirVeiculoResumido() => Console.WriteLine($"PLACA: {Placa} - MARCA: {Marca} - MODELO: {Modelo}");
        
    }
}
