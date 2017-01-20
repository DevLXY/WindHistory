using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matlab
{
    public class Matrix
    {

        private double[,] InnerVar;
        #region 属性
        public double this[int row, int col]                                //假如Matrix类实例化之后的名字叫x，则x[]（中括号内输入指定参数）即代表这个属性
        {
            set
            {
                InnerVar[row - 1, col - 1] = value;
            }
            get
            {
                return InnerVar[row - 1, col - 1];
            }
        }



        /// <summary>
        /// 提取矩阵第col列
        /// </summary>
        /// <param name="s">任意字符串（带双引号）</param>
        /// <param name="col">第col列</param>
        /// <returns></returns>
        public Matrix this[string s, int col]                               //Matrix this是double this的重载，重载的返回值可以不一样，但是重载的输入参数必须不一样
        {
            get
            {
                Matrix m = new Matrix(InnerVar.GetLength(0), 1);
                for (int i = 1; i <= InnerVar.GetLength(0); i++)
                {
                    m[i, 1] = this[i, col];                               //this表示这个属性的前面一个重载
                }
                return m;
            }
        }

        public Matrix this[int row, string s]
        {
            get
            {
                Matrix m = new Matrix(1, InnerVar.GetLength(1));
                for (int i = 1; i <= InnerVar.GetLength(1); i++)
                {
                    m[1, i] = this[row, i];
                }
                return m;
            }

        }
        public double this[int i]
        {
            get
            {
                if (this.ColumnCount!=1&&this.RowCount!=1)
                {
                    throw new Exception("不是行向量或列向量");
                }
                else if (this.RowCount == 1)
                {
                    return this[1, i];
                }
                else
                {
                    return this[i, 1];
                }
            }
        }


        public int RowCount
        {
            get
            {
                return InnerVar.GetLength(0);
            }
        }
        public int ColumnCount
        {
            get
            {
                return InnerVar.GetLength(1);
            }
        }

        #endregion 

        public Matrix(int row, int col)                              //Matrix a = new Matrix（int x, int y）,新生成一个x*y的矩阵
        {
            this.InnerVar = new double[row, col];
        }
        public Matrix(int row, int col, double a) : this(row, col)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    this.InnerVar[i, j] = a;
                }
            }
        }
        public Matrix(double[,] m)                                 //Matrix a = new Matrix（double[,] x）,将二维数组x变为矩阵a                
        {
            this.InnerVar = m;
        }
        public Matrix(double[] m) : this(1, m.Length)                 //Matrix a = new Matrix（double[] x）,将一维数组x变为1行的矩阵，继承自第一个构造函数的重载
        {

            for (int i = 0; i < m.Length; i++)
            {
                InnerVar[0, i] = m[i];                                     //因为继承自第一个构造函数重载，所以double[,] InnerVar在第一个构造函数里已经初始化了，可以直接用
            }
        }




        /// <summary>
        /// 二维矩阵转置
        /// </summary>
        /// <returns>Matrix类</returns>
        public Matrix Transposition()
        {
            Matrix m = new Matrix(InnerVar.GetLength(1), InnerVar.GetLength(0));        //只要不在Matrix类构造函数里实例化Matrix就不会死循环
            for (int i = 1; i <= InnerVar.GetLength(1); i++)
            {
                for (int j = 1; j <= InnerVar.GetLength(0); j++)
                {
                    m[i, j] = this[j, i];                                           //m[i,j]是this属性的调用，this是调用这个方法的Matrix X
                }
            }
            return m;
        }

        /// <summary>
        /// 将Matrix转换为double二维数组
        /// </summary>
        /// <returns>double二维数组</returns>
        public double[,] ToArray()
        {
            return InnerVar;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < InnerVar.GetLength(0); i++)
            {
                for (int j = 0; j < InnerVar.GetLength(1); j++)
                {
                    //s += string.Format("{0,10}", InnerVar[i, j]);
                    s += InnerVar[i, j].ToString(".####").PadLeft(10);
                }
                s += "\n";
            }
            return s;
        }
    }
}
