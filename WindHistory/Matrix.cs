using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matlab
{
  public  class Matrix
    {
        private double[,] matrix;
        public double this[int row, int col]
        {
            set
            {
                matrix[row - 1, col - 1] = value;
            }
            get
            {
                return matrix[row - 1, col - 1];
            }
        }

        public Matrix this[string s,int col]
        {
            get
            {
                Matrix m = new Matrix(matrix.GetLength(0), 1);
                for (int i = 1; i <= matrix.GetLength(0); i++)
                {
                    m[i, 1] = this[i, col];
                }
                return m;
            }
        }

        public int RowCount
        {
            get
            {
                return matrix.GetLength(0);
            }
        }
        public int ColumnCount
        {
            get
            {
                return matrix.GetLength(1);
            }
        }



        public Matrix(int row,int col)
        {
            this.matrix = new double[row, col];
        }
        public Matrix(double[,] m)
        {
            this.matrix = m;
        }
        public Matrix(double[] m):this(1,m.Length)
        {
            for (int i = 0; i < m.Length; i++)
            {
                matrix[0, i] = m[i];
            }
        }

        public Matrix Transposition()
        {
            Matrix m = new Matrix(matrix.GetLength(1), matrix.GetLength(0));
            for (int i = 1; i <= matrix.GetLength(1); i++)
            {
                for (int j = 1; j <= matrix.GetLength(0); j++)
                {
                    m[i, j] = this[j, i];
                }
            }
            return m;
        }
    }
}
