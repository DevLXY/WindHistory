using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matlab
{
    public abstract class DotCalculate
    {

        public Matrix DotCal(Matrix a,Matrix b)
        {
            Matrix m = new Matrix(a.RowCount, a.ColumnCount);
            if (a.RowCount==b.RowCount&&a.ColumnCount==b.ColumnCount)
            {
                for (int i = 1; i <= a.RowCount; i++)
                {
                    for (int j = 0; j < a.ColumnCount; j++)
                    {
                        m[i, j] = DotFuck(a[i,j], b[i,j]);
                    }
                }
                return m;
            }
            else
            {
                throw new Exception("1111");
            }

        }

        public Matrix DotCal(double a,Matrix b)
        {
            Matrix aa = new Matrix(b.RowCount, b.ColumnCount, a);

            return DotCal(aa, b);
        }

        public Matrix DotCal(Matrix a, double b)
        {
            return DotCal(b, a);
        }
        protected abstract double DotFuck(double a, double b);
     
    }

    public class DotMult : DotCalculate
    {
        protected override double DotFuck(double a, double b)
        {
            return a * b;
        }
    }
    
    /*
    调用这个重写类的方法：
    Matrix m2 = new Matrix(m);
    DotMult a = new DotMult();
    Matrix X = a.DotCal(2, m2);
    */


}
