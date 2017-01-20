using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Matlab
{
    public static class MatlabMethod
    {
        /// <summary>
        /// 生成线性等分的一维数组
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="n">等分数目，缺省值为100</param>
        /// <returns></returns>5
        public static Matrix Linspace(double x1, double x2, int n = 100)
        {
            double[] R = new double[n];
            for (int i1 = 0; i1 < n; i1++)
            {
                R[i1] = (x2 - x1) * (i1 / n) + x1;

            }
            return new Matrix(R);/////////////////////////////////////////////////////
        }

        public static Matrix MatrixMultiply(Matrix a, Matrix b)
        {
            if (a.ColumnCount == b.RowCount)
            {
                Matrix c = new Matrix(a.RowCount, b.ColumnCount);
                for (int i = 1; i <= a.RowCount; i++)
                {
                    for (int j = 1; j <= b.ColumnCount; j++)
                    {

                        for (int k = 1; k <= a.ColumnCount; k++)
                        {
                            c[i, j] += a[i, k] * b[k, j];//////////////////////////////////////this
                        }
                    }
                }
                return c;
            }
            else
            {
                throw new Exception("两数组无法相乘");
            }


        }
    }
}
