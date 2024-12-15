using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace МО_ИДЗ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<double[], double> objectiveFunction5 = v => Function5(v[0], v[1]);
            Func<double[], double> objectiveFunction4 = v => Function4(v[0], v[1]);
            //Нелдкр мид
            Console.WriteLine("\n-------------------Нелдер-мид-------------------\n");
            double[] x0 = { 3.0, 3.0 };  // Начальная точка
            List<double[]> simplex = NelderMead.InitializeSimplex(x0, 1.0);

            double[] result = NelderMead.Search(objectiveFunction5, simplex);

            //дихотомия
            Console.WriteLine("\n-------------------Дихотомия-------------------\n");
            Console.WriteLine(Dichotomy.Search(Function1,0,10,0.001,0.0005));

            //золотое сечение
            Console.WriteLine("\n-------------------Золотое сечение-------------------\n");
            Console.WriteLine(GoldenSection.Search(Function1, 0.618, 0.382, 0.001, 0, 10));

            //наискорейший спуск
            Console.WriteLine("\n-------------------Наискорейший спуск-------------------\n");
            double[] initialPoint = {1.5,1.5};
            Console.WriteLine(SteepestDescent.Search(objectiveFunction5, initialPoint, 0.001));
        }

        public static double Function1 (double x)
        {
            return 8 - Math.Exp(-Math.Pow((x - 5) / 2,2));
        }
        public static double Function2 (double x)
        {
            return 9 * (x - 1) * (x - 2) * (x - 5);
        }
        public static double Function3 (double x)
        {
            return (x / 2) + 2 * Math.Sin(3 * Math.PI * x + 1);
        }
        public static double Function4 (double x1, double x2)
        {
            return Math.Pow((x1 - 1), 2) + 3 * Math.Pow((x2 - 1), 2);
        }
        public static double Function5 (double x1, double x2)
        {
            return Math.Pow((x1 - 2),2) + Math.Pow((x2 -1), 2) + 30 * Math.Pow((x2 + x1 - 6),2) + 9.7;
        }
        public static double FunctionTest(double x)
        {
            return Math.Abs(x - 2);
        }
    }
}
