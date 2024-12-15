using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace МО_ИДЗ
{
    internal static class Vector
    {
        public static double[] Add(double[] a, double[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("размерность ебло");

            double[] result = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] + b[i];
            }
            return result;
        }
        public static double[] Subtract(double[] a, double[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("размерность ебло");

            double[] result = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] - b[i];
            }
            return result;
        }

        public static double[] Multiply(double scalar, double[] a)
        {
            double[] result = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = scalar * a[i];
            }
            return result;
        }

        public static double DotProduct(double[] a, double[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("размерность ебло");

            double result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result += a[i] * b[i];
            }
            return result;
        }
    }
}
