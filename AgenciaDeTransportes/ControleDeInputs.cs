using System;
using System.Text.RegularExpressions;
using System.Globalization;

namespace AgenciaDeTransportes
{
    public class ControleDeInputs
    {
        public static int ValidarNumeros(string s) // VALIDAR INPUTS DE NUMEROS INTEIROS MAIORES QUE 0
        {
            int result;
            while (!int.TryParse(s, out result) || result <= 0)
            {
                Console.Write("\nVALOR INVÁLIDO, DIGITE APENAS VALORES INTEIROS POSITIVOS: ");
                s = Console.ReadLine();
            }
            return result;
        }
        public static int ValidarNumeros1A3(string s) // VALIDAR INPUTS DE NUMEROS INTEIROS DE 1 A 3
        {
            int result;
            while (!int.TryParse(s, out result) || result < 1 || result > 3)
            {
                Console.Write("\nVALOR INVÁLIDO, DIGITE APENAS VALORES INTEIROS POSITIVOS DE [1] A [3]: ");
                s = Console.ReadLine();
            }
            return result;
        }
        public static double ValidarPontoFlutuante(string s) // VALIDAR INPUTS DE NUMEROS DECIMAIS
        {
            double result;
            while (!double.TryParse(s, out result) || result <= 0.0)
            {
                Console.Write("\nVALOR INVÁLIDO, DIGITE APENAS VALORES REAIS POSITIVOS: ");
                s = Console.ReadLine();
            }
            return result;
        }
        public static double ValidarLitrosAbastecimento(string s) // VALIDAR INPUTS DE NUMEROS DECIMAIS INCLUINDO 0
        {
            double result;
            while (!double.TryParse(s, out result) || result < 0.0)
            {
                Console.Write("\nVALOR INVÁLIDO, DIGITE APENAS VALORES REAIS POSITIVOS: ");
                s = Console.ReadLine();
            }
            return result;
        }

        public static string ValidarLetrasComEspaco(string s) // VALIDAR APENAS LETRAS
        {
            while (!Regex.IsMatch(s, @"^[a-z A-Z]+$") || string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
            {
                Console.Write("\nVALOR INVÁLIDO, DIGITE APENAS LETRAS [A-Z]: ");
                s = Console.ReadLine();
            }
            return s;
        }
        public static string ValidarModelos(string s) // VALIDAR LETRAS E NÚMEROS
        {
            while (!Regex.IsMatch(s, @"^[a-z A-Z0-9]+$") || string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
            {
                Console.Write("\nVALOR INVÁLIDO, DIGITE APENAS LETRAS [A-Z]: ");
                s = Console.ReadLine();
            }
            return s;
        }
        public static string ValidarPlaca(string s) // VALIDAR PLACA
        {
            while (!Regex.IsMatch(s, @"^[a-zA-Z]{3}-[0-9]{4}$") || string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
            {
                Console.Write("\nVALOR INVÁLIDO, DIGITE UMA PLACA [LLL-NNNN]: ");
                s = Console.ReadLine().ToUpper();
            }
            return s;
        }
        public static DateTime ValidarAno(string s) // VALIDAR APENAS ANO
        {
            DateTime data;
            while (!DateTime.TryParseExact(s, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data))
            {
                Console.Write("\nVALOR INVÁLIDO, DIGITE UMA DATA VÁLIDA [dd/MM/yyyy]: ");
                s = Console.ReadLine();
            }
            return data;
        }
        public static DateTime ValidarData(string s) // VALIDAR DATA
        {
            DateTime data;
            while (!DateTime.TryParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data))
            {
                Console.Write("\nVALOR INVÁLIDO, DIGITE UMA DATA VÁLIDA [dd/MM/yyyy]: ");
                s = Console.ReadLine();
            }
            return data;
        }
        public static string ValidarOpcoes(string s) // VALIDAR OPÇÕES DO TIPO DE COMBUSTIVEL
        {
            while (s != "F" && s != "A" && s != "G" && s != "C")
            {
                Console.Write("\nVALOR INVÁLIDO, ESCOLHA ENTRE AS OPÇÕES [F]-[A]-[G]-[C]: ");
                Console.WriteLine();
                s = Console.ReadLine().ToUpper();
            }
            return s;
        }
        public static string ValidarOpcoesSOuN(string s) // VALIDAR INPUTS SIM OU NAO
        {
            while (s != "S" && s != "N")
            {
                Console.Write("\nVALOR INVÁLIDO, ESCOLHA ENTRE AS OPÇÕES [S/N]: ");
                Console.WriteLine();
                s = Console.ReadLine().ToUpper();
            }
            return s;
        }
    }
}
