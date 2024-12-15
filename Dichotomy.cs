using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace МО_ИДЗ
{
    internal class Dichotomy
    {
        public static double Search(Func<double, double> function, double a, double b, double epsilon, double delta)
        {
            int iteration = 0;
            using (StreamWriter writer = new StreamWriter("dichotomy_data.csv"))
            {
                writer.WriteLine("Iteration,x,f(x),Interval_a,Interval_b"); // Заголовок CSV

                while (Math.Abs(b - a) > epsilon)
                {

                    double x1 = (a + b - delta) / 2;
                    double x2 = (a + b + delta) / 2;

                    double f1 = function(x1);
                    double f2 = function(x2);
                    if (f1 <= f2)
                    {
                        b = x2;
                    }
                    else
                    {
                        a = x1;
                    }
                    // Запись данных в CSV
                    writer.WriteLine($"{iteration},{x1},{f1},{a},{b}");
                    writer.WriteLine($"{iteration},{x2},{f2},{a},{b}");
                    iteration++;
                    Console.WriteLine($"Итерация {iteration}: x = {(a + b) / 2:F3}, f(x) = {function((a + b) / 2):F3}, Интервал: [{a:F3}, {b:F3}]");
                }

                return (a + b) / 2;
            }
        }
    }
}
