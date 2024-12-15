using System;
using System.Collections.Generic;
using System.IO;

namespace МО_ИДЗ
{
    internal class NelderMead
    {
        public static double[] Search(Func<double[], double> objectiveFunction, List<double[]> simplex, double tolerance = 0.0001)
        {
            int iteration = 0;
            using (StreamWriter writer = new StreamWriter("nelder_mead_data.csv"))
            {
                writer.WriteLine("Iteration,x1,x2,f(x)"); // Заголовок CSV
                while (iteration < 10000)
                {
                    simplex.Sort((a, b) => objectiveFunction(a).CompareTo(objectiveFunction(b)));

                    var x_light = simplex[0];  //легкая точка
                    var x_mode = simplex[1];   //средняя точка
                    var x_heavy = simplex[2];  //тжелвя точка
                    var centroid = Centroid(x_light, x_mode);

                    // Отражение
                    var x_reflected = Reflect(1, centroid, x_heavy);

                    if (objectiveFunction(x_reflected) < objectiveFunction(x_light))
                    {
                        // Расширение
                        var x_expanded = Expand(centroid, 2, x_reflected);
                        simplex[2] = objectiveFunction(x_expanded) < objectiveFunction(x_light) ? x_expanded : x_reflected;
                    }
                    else if (objectiveFunction(x_reflected) >= objectiveFunction(x_mode))
                    {
                        if (objectiveFunction(x_reflected) >= objectiveFunction(x_heavy))
                        {
                            simplex = Reduct(simplex, 0.5, x_light);
                        }
                        else
                        {
                            // Сжатие
                            var x_contracted = Conrtact(centroid, 0.5, x_heavy);
                            simplex[2] = objectiveFunction(x_contracted) < objectiveFunction(x_heavy) ? x_contracted : x_heavy;
                        }
                    }
                    else
                    {
                        simplex[2] = x_reflected;
                    }

                    // Вывод данных на каждой итерации
                    Console.WriteLine($"Итерация {iteration}: Текущий минимум ({simplex[0][0]:F3}, {simplex[0][1]:F3}) — Значение: {objectiveFunction(simplex[0]):F3}");

                    // Запись данных в файл
                    writer.WriteLine($"{iteration},{simplex[0][0]},{simplex[0][1]},{objectiveFunction(simplex[0])}");

                    if (RMSE(simplex, objectiveFunction) < tolerance || Distance(simplex[0], simplex[2]) < tolerance)
                        break;

                    iteration++;
                }
            }
            return simplex[0];
        }

        // Вычисление центроида
        static double[] Centroid(double[] x_light, double[] x_mode)
        {
            return Vector.Multiply(0.5, Vector.Add(x_light, x_mode));
        }

        // Отражение
        static double[] Reflect(double alpha, double[] centroid, double[] x_heavy)
        {
            return Vector.Add(centroid, Vector.Multiply(alpha, Vector.Subtract(centroid, x_heavy)));
        }

        // Расширение
        static double[] Expand(double[] centroid, double gamma, double[] x_reflected)
        {
            return Vector.Add(centroid, Vector.Multiply(gamma, Vector.Subtract(x_reflected, centroid)));
        }

        // Сжатие
        static double[] Conrtact(double[] centroid, double beta, double[] x_heavy)
        {
            return Vector.Add(centroid, Vector.Multiply(beta, Vector.Subtract(x_heavy, centroid)));
        }

        // Редукция
        static List<double[]> Reduct(List<double[]> simplex, double sigma, double[] x_best)
        {
            for (int i = 1; i < simplex.Count; i++)
            {
                simplex[i] = Vector.Add(x_best, Vector.Multiply(sigma, Vector.Subtract(simplex[i], x_best)));
            }
            return simplex;
        }

        // Расчёт RMSE
        static double RMSE(List<double[]> simplex, Func<double[], double> f)
        {
            double bestValue = f(simplex[0]);
            double sumSquaredErrors = 0;

            for (int i = 1; i < simplex.Count; i++)
            {
                double error = f(simplex[i]) - bestValue;
                sumSquaredErrors += error * error;
            }

            return Math.Sqrt(sumSquaredErrors / (simplex.Count - 1));
        }

        // Вычисление расстояния между точками
        static double Distance(double[] a, double[] b)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
                sum += Math.Pow(a[i] - b[i], 2);
            return Math.Sqrt(sum);
        }

        // Инициализация симплекса
        public static List<double[]> InitializeSimplex(double[] x0, double t)
        {
            int n = x0.Length;
            double b = t * (Math.Sqrt(n + 1) - 1) / (Math.Sqrt(2) * n);
            double a = t * (Math.Sqrt(n + 1) + n - 1) / (Math.Sqrt(2) * n);

            List<double[]> simplex = new List<double[]>();
            simplex.Add((double[])x0.Clone());

            for (int i = 0; i < n; i++)
            {
                double[] xi = (double[])x0.Clone();
                xi[i] += a;
                simplex.Add(xi);
            }
            return simplex;
        }
    }
}