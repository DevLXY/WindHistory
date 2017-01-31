using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace Matlab
{
    public class MatlabMethod
    {
        #region Linspace函数
        /// <summary>
        /// 生成线性等分的行向量
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="n">等分数目，缺省值为100</param>
        /// <returns>Matrix</returns>5
        public static Matrix Linspace(double x1, double x2, int n = 100)
        {
            double[] R = new double[n];
            for (int i1 = 0; i1 < n; i1++)
            {
                R[i1] = (x2 - x1) * (Convert.ToDouble(i1) / (n - 1)) + x1;

            }
            return new Matrix(R);/////////////////////////////////////////////////////
        }
        #endregion

        #region Round函数
        /// <summary>
        /// 矩阵取整
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Matrix Round(Matrix a)
        {
            Matrix m = new Matrix(a.RowCount, a.ColumnCount);
            for (int i = 1; i <= a.RowCount; i++)
            {
                for (int j = 1; j <= a.ColumnCount; j++)
                {
                    m[i, j] = Math.Round(a[i, j]);
                }
            }
            return m;
        }
        #endregion

        #region Random函数
        /// <summary>
        /// Random矩阵
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>Matrix</returns>
        public static Matrix rand(int row, int col)
        {
            Random rd = new Random();
            Matrix m = new Matrix(row, col);
            for (int i = 1; i <= row; i++)
            {
                for (int j = 1; j <= col; j++)
                {
                    m[i, j] = rd.NextDouble();
                }
            }
            return m;
        }
        #endregion

        #region Sum运算
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns>一个double类型的数</returns>
        public static double sum(Matrix a)
        {
            if (a.RowCount == 1 && a.ColumnCount != 1)
            {
                double sum = 0;
                for (int i = 1; i <= a.ColumnCount; i++)
                {
                    sum += a[1, i];
                }
                return sum;
            }
            else if (a.RowCount != 1 && a.ColumnCount == 1)
            {
                double sum = 0;
                for (int i = 1; i <= a.RowCount; i++)
                {
                    sum += a[i, 1];
                }
                return sum;
            }
            else
            {
                throw new Exception("此数组不能进行sum运算");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b">0代表sum成行向量，1代表sum成列向量</param>
        /// <returns>行向量或列向量</returns>
        public static Matrix sum(Matrix a, int b)
        {
            if (a.RowCount != 1 && a.ColumnCount != 1 && b == 0)          //sum成一个行向量
            {
                Matrix m = new Matrix(1, a.ColumnCount);
                for (int i = 1; i <= a.RowCount; i++)
                {
                    for (int j = 1; j <= a.ColumnCount; j++)
                    {
                        m[1, j] += a[i, j];
                    }
                }
                return m;
            }
            else if (a.RowCount != 1 && a.ColumnCount != 1 && b == 1)
            {
                Matrix m = new Matrix(1, a.ColumnCount);
                for (int i = 1; i <= a.RowCount; i++)
                {
                    for (int j = 1; j <= a.ColumnCount; j++)
                    {
                        m[i, 1] += a[i, j];
                    }
                }
                return m;
            }
            else
            {
                throw new Exception("此数组不能进行sum运算");
            }
        }
        #endregion

        public static Matrix Cos(Matrix a)
        {
            Matrix m = new Matrix(a.RowCount, a.ColumnCount);
            for (int i = 1; i <= a.RowCount; i++)
            {
                for (int j = 1; j <= a.ColumnCount; j++)
                {
                    m[i, j] = Math.Cos(a[i, j]);
                }
            }
            return m;
        }
        /// <summary>
        /// 联合两个代码（这一块有瑕疵）
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix Combine(Matrix a, Matrix b)
        {
            Matrix m = new Matrix(1, a.ColumnCount + b.ColumnCount);
            for (int i = 1; i <= a.ColumnCount; i++)
            {
                m[1, i] = a[1, i];
            }
            for (int j = a.ColumnCount + 1; j <= a.ColumnCount + b.ColumnCount; j++)
            {
                m[1, j] = b[1, a.ColumnCount + b.ColumnCount - j + 1];
            }
            return m;

        }

        //输入x（6）结果为0
        public static Matrix filter(double b, Matrix a, Matrix x)                  //只适合a长度比矩阵x的rowcount少或者比行向量x的columncount少的情况
        {
            Matrix m = new Matrix(x.RowCount, x.ColumnCount);
            if (a.RowCount != 1)
            {
                throw new Exception("a不是行向量");
            }
            else if (x.RowCount != 1)
            {
                for (int ix = 1; ix <= x.RowCount; ix++)                           //计算的n
                {
                    for (int jx = 1; jx <= x.ColumnCount; jx++)
                    {
                        m[ix, jx] = b * x[ix, jx] / a[1, 1];
                        for (int i1 = 1; i1 <= a.ColumnCount; i1++)
                        {
                            if (i1 + 1 > a.ColumnCount || ix - i1 <= 0)
                            {
                                break;
                            }
                            else
                            {
                                m[ix, jx] -= a[1, i1 + 1] * m[ix - i1, jx] / a[1, 1];
                            }
                        }
                    }
                }
                return m;
            }
            else
            {
                for (int jx = 1; jx <= x.ColumnCount; jx++)
                {
                    m[1, jx] = b * x[1, jx] / a[1, 1];
                    for (int i1 = 1; i1 <= a.ColumnCount; i1++)
                    {
                        if (i1 + 1 > a.ColumnCount || jx - i1 <= 0)
                        {
                            break;
                        }
                        else
                        {
                            m[1, jx] -= a[1, i1 + 1] * m[1, jx - i1] / a[1, 1];
                        }
                    }
                }

                return m;
            }

        }     

        /// <summary>
        /// Ac2Poly(Levinson-Durbin)
        /// </summary>
        /// <param name="r">输入的一维数组</param>
        /// <param name="p">数组的个数-1</param>
        /// <returns>变换之后的数组</returns>
        public static double[] Ac2Poly(double[] r, int p)
        {
            double[] a = new double[r.GetLength(0)];
            int i, j;
            double err;

            if (0 == r[0])
            {
                for (i = 0; i < p; i++)
                {
                    a[i] = 0;
                }
                return a;
            }
            a[0] = 1.0;
            err = r[0];
            for (i = 0; i < p; i++)
            {
                double lambda = 0.0;
                for (j = 0; j <= i; j++)
                    lambda -= a[j] * r[i + 1 - j];
                lambda /= err;
                // Update LPC coefficients and total error
                for (j = 0; j <= (i + 1) / 2; j++)
                {
                    double temp = a[i + 1 - j] + lambda * a[j];
                    a[j] = a[j] + lambda * a[i + 1 - j];
                    a[i + 1 - j] = temp;
                }
                err *= (1.0 - lambda * lambda);
            }
            return a;
        }

        /// <summary>
        /// ifftshift函数（如果是奇数，分块时第一块比较小）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Matrix ifftshift(Matrix input)
        {
            int x = input.ColumnCount;
            int y = input.RowCount;
            Matrix m1 = new Matrix(input.RowCount, input.ColumnCount);
            if (x%2==0)
            {
                int x1 = x / 2;
                for (int i = 1; i <= input.RowCount; i++)
                {
                    for (int j = 1; j <= x1; j++)
                    {
                        m1[i, j] = input[i, j + x1];
                        m1[i, j + x1] = input[i, j];
                    }
                }
            }
            else
            {
                int x1 = (int)(x / 2d - 0.5);
                int x2 = (int)(x / 2d + 0.5);
                for (int i = 1; i <= input.RowCount; i++)
                {
                    for (int j = 1; j <= x1; j++)
                    {
                        m1[i, j + x2] = input[i, j];
                    }
                    for (int j = 1; j <= x2; j++)
                    {
                        m1[i, j] = input[i, j+x1];
                    }
                }
            }
            Matrix m2 = new Matrix(input.RowCount, input.ColumnCount);
            if (y % 2 == 0)
            {
                int y1 = y / 2;
                for (int j = 1; j <= input.ColumnCount; j++)
                {
                    for (int i = 1; i <= y1; j++)
                    {
                        m2[i+y1, j] = m1[i, j];
                        m2[i, j ] = m1[i+y1, j];
                    }
                }
            }
            else
            {
                int y1 = (int)(y / 2d - 0.5);
                int y2 = (int)(y / 2d + 0.5);
                for (int j = 1; j <= input.ColumnCount; j++)
                {
                    for (int i = 1; i <= y1; i++)
                    {
                        m2[i+y2, j] = m1[i, j];
                    }
                    for (int i = 1; i <= y2; i++)
                    {
                        m2[i, j] = m1[i+y1, j];
                    }
                }
            }
            return m2;

        }



        #region 一些无用的代码
        //public static DataTable ConvertToDataTable(string[,] arr)
        //{

        //    DataTable dataSouce = new DataTable();
        //    for (int i = 0; i < arr.GetLength(1); i++)
        //    {
        //        DataColumn newColumn = new DataColumn(i.ToString(), arr[0, 0].GetType());
        //        dataSouce.Columns.Add(newColumn);
        //    }
        //    for (int i = 0; i < arr.GetLength(0); i++)
        //    {
        //        DataRow newRow = dataSouce.NewRow();
        //        for (int j = 0; j < arr.GetLength(1); j++)
        //        {
        //            newRow[j.ToString()] = arr[i, j];
        //        }
        //        dataSouce.Rows.Add(newRow);
        //    }
        //    return dataSouce;

        //}


        //#region 矩阵加法
        //public static Matrix MatrixAdd(Matrix a, Matrix b)
        //{
        //    if (a.RowCount == b.RowCount && a.ColumnCount == b.ColumnCount)
        //    {
        //        Matrix m = new Matrix(a.RowCount, a.ColumnCount);
        //        for (int i = 1; i <= m.RowCount; i++)
        //        {
        //            for (int j = 1; j <= m.ColumnCount; j++)
        //            {
        //                m[i, j] = a[i, j] + b[i, j];
        //            }
        //        }
        //        return m;
        //    }
        //    else
        //    {
        //        throw new Exception("两数组无法相加");
        //    }

        //}



        //public static Matrix MatrixAdd(double a, Matrix b)
        //{
        //    Matrix aa = new Matrix(b.RowCount, b.ColumnCount, a);
        //    return MatrixAdd(aa, b);
        //}
        //public static Matrix MatrixAdd(Matrix a, double b)
        //{
        //    return MatrixAdd(b, a);
        //}
        //#endregion

        //#region 矩阵减法
        //public static Matrix MatrixSub(Matrix a, Matrix b)
        //{
        //    if (a.RowCount == b.RowCount && a.ColumnCount == b.ColumnCount)
        //    {
        //        Matrix m = new Matrix(a.RowCount, a.ColumnCount);
        //        for (int i = 1; i <= m.RowCount; i++)
        //        {
        //            for (int j = 1; j <= m.ColumnCount; j++)
        //            {
        //                m[i, j] = a[i, j] - b[i, j];
        //            }
        //        }
        //        return m;
        //    }
        //    else
        //    {
        //        throw new Exception("两数组无法相减");
        //    }

        //}
        //public static Matrix MatrixSub(double a, Matrix b)
        //{
        //    Matrix aa = new Matrix(b.RowCount, b.ColumnCount, a);
        //    return MatrixSub(aa, b);
        //}
        //public static Matrix MatrixSub(Matrix a, double b)
        //{
        //    Matrix bb = new Matrix(a.RowCount, a.ColumnCount, b);
        //    return MatrixSub(a, bb);
        //}
        //#endregion

        //#region 矩阵乘法
        //public static Matrix MatrixMult(Matrix a, Matrix b)             //输入两个实例化的Matrix类a，b，相乘
        //{
        //    if (a.ColumnCount == b.RowCount)
        //    {
        //        Matrix c = new Matrix(a.RowCount, b.ColumnCount);
        //        for (int i = 1; i <= a.RowCount; i++)
        //        {
        //            for (int j = 1; j <= b.ColumnCount; j++)
        //            {

        //                for (int k = 1; k <= a.ColumnCount; k++)
        //                {
        //                    c[i, j] += a[i, k] * b[k, j];//////////////////////////////////////
        //                }
        //            }
        //        }
        //        return c;
        //    }
        //    else
        //    {
        //        throw new Exception("两数组无法相乘");
        //    }


        //}

        //public static Matrix MatrixDotMult(Matrix a, Matrix b)
        //{
        //    if (a.ColumnCount == b.ColumnCount && a.RowCount == b.RowCount)
        //    {
        //        Matrix m = new Matrix(a.RowCount, b.ColumnCount);
        //        for (int i = 1; i <= b.RowCount; i++)
        //        {
        //            for (int j = 1; j <= b.ColumnCount; j++)
        //            {
        //                m[i, j] = a[i, j] * b[i, j];
        //            }
        //        }
        //        return m;
        //    }
        //    else
        //    {
        //        throw new Exception("两数组无法相乘");
        //    }
        //}

        //public static Matrix MatrixDotMult(double a, Matrix b)
        //{
        //    Matrix aa = new Matrix(b.RowCount, b.ColumnCount, a);

        //    return MatrixDotMult(aa, b);
        //}

        //public static Matrix MatrixDotMult(Matrix a, double b)
        //{
        //    return MatrixDotMult(b, a);
        //}

        //#endregion

        //#region 矩阵除法
        //public static Matrix MatrixDotDiv(Matrix a, Matrix b)
        //{
        //    if (a.ColumnCount == b.ColumnCount && a.RowCount == b.RowCount)
        //    {
        //        Matrix m = new Matrix(a.RowCount, b.ColumnCount);
        //        for (int i = 1; i <= b.RowCount; i++)
        //        {
        //            for (int j = 1; j <= b.ColumnCount; j++)
        //            {
        //                m[i, j] = a[i, j] / b[i, j];
        //            }
        //        }
        //        return m;
        //    }
        //    else
        //    {
        //        throw new Exception("两数组无法相乘");
        //    }
        //}

        //public static Matrix MatrixDotDiv(double a, Matrix b)
        //{
        //    Matrix aa = new Matrix(b.RowCount, b.ColumnCount, a);

        //    return MatrixDotDiv(aa, b);
        //}

        //public static Matrix MatrixDotDiv(Matrix a, double b)
        //{
        //    Matrix bb = new Matrix(a.RowCount, a.ColumnCount, b);

        //    return MatrixDotDiv(a, bb);
        //}
        //#endregion

        //#region 矩阵指数运算
        //public static Matrix MatrixPow(Matrix a, Matrix b)//////////////////////////
        //{
        //    if (a.RowCount == b.RowCount && a.ColumnCount == b.ColumnCount)
        //    {
        //        Matrix m = new Matrix(a.RowCount, a.ColumnCount);
        //        for (int i = 1; i <= m.RowCount; i++)
        //        {
        //            for (int j = 1; j <= m.ColumnCount; j++)
        //            {
        //                m[i, j] = Math.Pow(a[i, j], b[i, j]);
        //            }
        //        }
        //        return m;
        //    }
        //    else
        //    {
        //        throw new Exception("两数组无法进行此指数运算");
        //    }

        //}
        //public static Matrix MatrixPow(Matrix a, double b)
        //{
        //    Matrix bb = new Matrix(a.RowCount, a.ColumnCount, b);
        //    return MatrixPow(a, bb);
        //}
        //#endregion
        #endregion


    }
}
