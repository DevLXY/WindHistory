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
                R[i1] = (x2 - x1) * (Convert.ToDouble(i1) / (n-1)) + x1;

            }
            return new Matrix(R);/////////////////////////////////////////////////////
        }


        
        #region 矩阵加法
        public static Matrix MatrixAdd(Matrix a, Matrix b)
        {
            if (a.RowCount == b.RowCount && a.ColumnCount == b.ColumnCount)
            {
                Matrix m = new Matrix(a.RowCount, a.ColumnCount);
                for (int i = 1; i <= m.RowCount; i++)
                {
                    for (int j = 1; j <= m.ColumnCount; j++)
                    {
                        m[i, j] = a[i, j] + b[i, j];
                    }
                }
                return m;
            }
            else
            {
                throw new Exception("两数组无法相加");
            }

        }
        public static Matrix MatrixAdd(double a, Matrix b)
        {
            Matrix aa = new Matrix(b.RowCount, b.ColumnCount, a);
            return MatrixAdd(aa, b);
        }
        public static Matrix MatrixAdd(Matrix a, double b)
        {
            return MatrixAdd(b, a);
        }
        #endregion
        #region 矩阵减法
        public static Matrix MatrixSub(Matrix a, Matrix b)
        {
            if (a.RowCount == b.RowCount && a.ColumnCount == b.ColumnCount)
            {
                Matrix m = new Matrix(a.RowCount, a.ColumnCount);
                for (int i = 1; i <= m.RowCount; i++)
                {
                    for (int j = 1; j <= m.ColumnCount; j++)
                    {
                        m[i, j] = a[i, j] - b[i, j];
                    }
                }
                return m;
            }
            else
            {
                throw new Exception("两数组无法相加");
            }

        }
        public static Matrix MatrixSub(double a, Matrix b)
        {
            Matrix aa = new Matrix(b.RowCount, b.ColumnCount, a);
            return MatrixSub(aa, b);
        }
        public static Matrix MatrixSub(Matrix a, double b)
        {
            Matrix bb = new Matrix(a.RowCount, a.ColumnCount, b);
            return MatrixSub(a, bb);
        }
        #endregion
        #region 矩阵乘法
        public static Matrix MatrixMult(Matrix a, Matrix b)             //输入两个实例化的Matrix类a，b，相乘
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
                            c[i, j] += a[i, k] * b[k, j];//////////////////////////////////////
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

        public static Matrix MatrixDotMult(Matrix a, Matrix b)
        {
            if (a.ColumnCount == b.ColumnCount && a.RowCount == b.RowCount)
            {
                Matrix m = new Matrix(a.RowCount, b.ColumnCount);
                for (int i = 1; i <= b.RowCount; i++)
                {
                    for (int j = 1; j <= b.ColumnCount; j++)
                    {
                        m[i, j] = a[i, j] * b[i, j];
                    }
                }
                return m;
            }
            else
            {
                throw new Exception("两数组无法相乘");
            }
        }

        public static Matrix MatrixDotMult(double a, Matrix b)
        {
            Matrix aa= new Matrix(b.RowCount, b.ColumnCount,a);        
            
            return MatrixDotMult(aa,b);
        }

        public static Matrix MatrixDotMult(Matrix a,double b )
        {
            return MatrixDotMult(b, a);
        }

        #endregion
        #region 矩阵除法
        public static Matrix MatrixDotDiv(Matrix a, Matrix b)
        {
            if (a.ColumnCount == b.ColumnCount && a.RowCount == b.RowCount)
            {
                Matrix m = new Matrix(a.RowCount, b.ColumnCount);
                for (int i = 1; i <= b.RowCount; i++)
                {
                    for (int j = 1; j <= b.ColumnCount; j++)
                    {
                        m[i, j] = a[i, j] / b[i, j];
                    }
                }
                return m;
            }
            else
            {
                throw new Exception("两数组无法相乘");
            }
        }

        public static Matrix MatrixDotDiv(double a, Matrix b)
        {
            Matrix aa = new Matrix(b.RowCount, b.ColumnCount, a);

            return MatrixDotDiv(aa, b);
        }

        public static Matrix MatrixDotDiv(Matrix a, double b)
        {
            Matrix bb = new Matrix(a.RowCount, a.ColumnCount, b);

            return MatrixDotDiv(a, bb);
        }
        #endregion
        #region 矩阵指数运算
        public static Matrix MatrixPow(Matrix a,Matrix b)//////////////////////////
        {
            if (a.RowCount == b.RowCount && a.ColumnCount == b.ColumnCount)
            {
                Matrix m = new Matrix(a.RowCount, a.ColumnCount);
                for (int i = 1; i <= m.RowCount; i++)
                {
                    for (int j = 1; j <= m.ColumnCount; j++)
                    {
                        m[i, j] = Math.Pow(a[i, j], b[i, j]);
                    }
                }
                return m;
            }
            else
            {
                throw new Exception("两数组无法进行此指数运算");
            }

        }
        public static Matrix MatrixPow(Matrix a,double b)
        {
            Matrix bb = new Matrix(a.RowCount, a.ColumnCount,b);
            return MatrixPow(a, bb);
        }
        #endregion



    }
}
