using System;
using System.IO;

namespace МО_ИДЗ
{
    internal class GoldenSection
    {
        public static double Search(Func<double, double> function, double alpha, double beta, double tolerance, double a, double b)
        {
            double xBeta, xAlpha, ans;
            int iteration = 0;

            using (StreamWriter writer = new StreamWriter("golden_section_data.csv"))
            {
                writer.WriteLine("Iteration,a,b,xBeta,xAlpha,f(xBeta),f(xAlpha)"); // Заголовок CSV

                while ((b - a) > tolerance)
                {
                    xBeta = a + beta * (b - a);
                    xAlpha = a + alpha * (b - a);

                    // Вывод в консоль
                    //Console.WriteLine($"Итерация {iteration}: a={a:F3}, b={b:F3}, xBeta={xBeta:F3}, xAlpha={xAlpha:F3}, f(xBeta)={function(xBeta):F3}, f(xAlpha)={function(xAlpha):F3}");

                    // Запись в CSV
                    writer.WriteLine($"{iteration},{a},{b},{xBeta},{xAlpha},{function(xBeta)},{function(xAlpha)}");

                    if (function(xBeta) >= function(xAlpha))
                    {
                        a = xBeta;
                    }
                    else
                    {
                        b = xAlpha;
                    }
                    iteration++;
                }

                // Определяем минимум
                if (function(a) < function(b))
                {
                    ans = a;
                }
                else
                {
                    ans = b;
                }

                // Финальный вывод в консоль
                Console.WriteLine($"Итерация {iteration}, x^min={ans:F3}, f(x^min) = {function(ans):F3}");
            }

            return ans;
        }
    }
}