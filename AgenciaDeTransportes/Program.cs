using System;
using AgenciaDeTransportes.Entities;

namespace AgenciaDeTransportes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("BEM VINDO!");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("POR FAVOR DIGITE O NOME DA SUA AGÊNCIA DE TRANSPORTES: ");
            Agencia agencia = new Agencia(ControleDeInputs.ValidarLetrasComEspaco(Console.ReadLine()));
            Menu.MenuInicial(agencia);
        }
    }
}
