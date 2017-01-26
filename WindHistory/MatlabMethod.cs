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
        public static Matrix rand(int row,int col)
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
            if (a.RowCount==1&&a.ColumnCount!=1)
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
        public static Matrix sum(Matrix a,int b)
        {
            if (a.RowCount!=1&&a.ColumnCount!=1&&b==0)          //sum成一个行向量
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
            else if(a.RowCount != 1 && a.ColumnCount != 1 && b == 1)
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

        #region 一些无用的代码
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
