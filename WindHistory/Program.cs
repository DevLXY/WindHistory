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
            Matrix info = new Matrix(new double[,] { { 3, 4.5 }, { 2, 4 }, { 1, 4 } });
            WindHistory(4, 0.5, 5, 0.5, info);
        }

        private static void WindHistory(double f_sup, double alpha, double v_10, double k, Matrix info)
        {

            int np = info.RowCount;
            int nf = 4096;




            Matrix z_k = info[":", 1].Transposition();
            Matrix z_j = info[":", 2].Transposition();
            Matrix f = MatlabMethod.Linspace(f_sup / nf, f_sup, nf);
            int z_b = 10;
            int c_z = 10;
            Matrix x = MatlabMethod.MatrixDotMult(1200 / v_10, f);
            //下面两个式子没测试
            Matrix s_ii = MatlabMethod.MatrixDotDiv(MatlabMethod.MatrixDotDiv(MatlabMethod.MatrixDotMult(v_10 * v_10 * 4 * k, MatlabMethod.MatrixPow(x, 2)), f), MatlabMethod.MatrixPow(MatlabMethod.MatrixAdd(1, MatlabMethod.MatrixPow(x, 2)), 4d / 3d));
            Matrix v_zj = MatlabMethod.MatrixDotMult(v_10, MatlabMethod.MatrixPow(MatlabMethod.MatrixDotDiv(z_j, z_b), alpha));
            Matrix[] r_ij = new Matrix[nf];
            Matrix[] s_ij = new Matrix[nf];
            for (int i = 0; i < nf; i++)
            {
                r_ij[i] = new Matrix(np, np);
                s_ij[i] = new Matrix(np, np);
            }
            Matrix theta = new Matrix(np, np);

            for (int ii = 1; ii <= np; ii++)
            {
                for (int jj = 1; jj <= np; jj++)
                {
                    theta[ii, jj] = z_j[ii] / v_zj[ii] + z_j[jj] / v_zj[jj];
                    for (int kk = 0; kk < nf; kk++)
                    {
                        r_ij[kk][ii, jj] = Math.Exp(-f[kk + 1] * c_z * Math.Abs(z_j[ii] - z_j[jj]) / (0.5 * (v_zj[ii] + v_zj[jj])));
                        s_ij[kk][ii, jj] = s_ii[kk + 1] * r_ij[kk][ii, jj];
                    }
                }
            }

        }

    }
}
