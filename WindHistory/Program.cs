using Matlab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindHistory
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix m1 =new Matrix (new double[,] { { 1, 2, 3 }, { 4,5,6} });
            Matrix m2 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 },{ 4, 5, 6 } });
            Matrix m3 = MatlabMethod.MatrixMultiply(m1, m2);
        }
        
        //private void WindHistory(double f_sup,double alpha,double k,double[,]info,string savePath)
        //{
        //    MatlabMethod MM = new MatlabMethod();
        //    int np = info.GetLength(1);
        //    int nf = 4096;

        //    double[] z_j = new double[np];
        //    for (int i1 = 0; i1 < np; i1++)
        //    {
        //        z_j[i1] = info[i1, 1];
        //    }
        //    double[] z_k = new double[np];
        //    for (int i1 = 0; i1 < np; i1++)
        //    {
        //        z_k[i1] = info[i1, 2];
        //    }
        //    double[] f = MM.Linspace(f_sup / nf, f_sup, nf);
        //    int z_b = 10;
        //    int c_z = 10;
        //}

    }
}
