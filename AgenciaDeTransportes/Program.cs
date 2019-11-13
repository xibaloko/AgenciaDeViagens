using System;
using AgenciaDeTransportes.Entities;
using AgenciaDeTransportes.Database;
using static AgenciaDeTransportes.Servicos.ControleDeInputs;
using static AgenciaDeTransportes.Servicos.Menu;


namespace AgenciaDeTransportes
{
    public class Program
    {
        public static Agencia _agencia { get; set; }
        static void Main(string[] args)
        {
            JsonBase jsonBase = new JsonBase();
            _agencia = jsonBase.BuscarDados();

            if (_agencia == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("BEM VINDO!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("POR FAVOR DIGITE O NOME DA SUA AGÊNCIA DE TRANSPORTES: ");
                _agencia = new Agencia(ValidarLetrasComEspaco(Console.ReadLine()));
            }

            MenuInicial(_agencia, jsonBase);
        }
    }
}
