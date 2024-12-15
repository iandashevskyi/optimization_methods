using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace МО_ИДЗ
{
    internal class SteepestDescent
    {
        public static double[] Search(Func<double[], double> function, double[] initialPoint, double epsilon)
        {
            double[] x = (double[])initialPoint.Clone();
            int iteration = 0;
            using (StreamWriter writer = new StreamWriter("steepest_descent_data.csv"))
            {
                writer.WriteLine("Iteration,x1,x2,f(x)"); // Заголовок CSV
                while (iteration < 10000)
                {
                    double[] gradient = Gradient(function, x);

                    bool stop = true;
                    for (int i = 0; i < gradient.Length; i++)
                    {
                        if (Math.Abs(gradient[i]) >= epsilon)
                        {
                            stop = false;
                            break;
                        }
                    }
                    if (stop)
                    {
                        break;
                    }

                    Func<double, double> phi = tau => function(Vector.Subtract(x, Vector.Multiply(tau, gradient)));
                    double t = GoldenSection.Search(phi, 0.618, 0.382, epsilon, 0, 1);
                    x = Vector.Subtract(x, Vector.Multiply(t, gradient));

                    iteration++;

                    Console.WriteLine($"Итерация {iteration}: x = [{x[0]:F3}, {x[1]:F3}], f(x) = {function(x):F3}");
                    writer.WriteLine($"{iteration},{x[0]:F3},{x[1]:F3},{function(x):F3}");
                }
            }

            return x;
        }
        private static double[] Gradient(Func<double[], double> function, double[] x)
        {
            int n = x.Length;
            double[] grad = new double[n];
            double h = 0.0001;

            for (int i = 0; i < n; i++)
            {
                double[] xPlus = (double[])x.Clone();
                double[] xMinus = (double[])x.Clone();

                xPlus[i] += h;
                xMinus[i] -= h;

                grad[i] = (function(xPlus) - function(xMinus)) / (2 * h);
            }

            return grad;
        }
    }
}
