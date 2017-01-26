using Matlab;
using System;
using System.Data;

namespace WindHistory
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] test = { { 3, 4.5 }, { 2, 4 }, { 1, 5 } };
            Matrix info = new Matrix(test);
            
            WindHistory(4, 0.5, 5, 0.5, info);


            //string[,] test1 = new string[test.GetLength(0), test.GetLength(1)];
            //for (int i = 0; i < test.GetLength(0); i++)
            //{
            //    for (int j = 0; j < test.GetLength(1); j++)
            //    {
            //        test1[i, j] = test[i, j].ToString();
            //    }
            //}
            //DataTable dat = MatlabMethod.ConvertToDataTable(test1);
            //double a =Convert.ToDouble( dat.Compute("sum(1)", "1>0"));
            

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
            Matrix x = 1200 * f / v_10;
            Matrix s_ii = v_10 * v_10 * 4 * k * (x * x) / f / ((1 + x * x) ^ (4d / 3d));
            //Matrix s_ii = MatlabMethod.MatrixDotDiv(MatlabMethod.MatrixDotDiv(MatlabMethod.MatrixDotMult(v_10 * v_10 * 4 * k, MatlabMethod.MatrixPow(x, 2)), f), MatlabMethod.MatrixPow(MatlabMethod.MatrixAdd(1, MatlabMethod.MatrixPow(x, 2)), 4d / 3d));
            Matrix v_zj = v_10 * ((z_j / z_b) ^ alpha);
            //Matrix v_zj = MatlabMethod.MatrixDotMult(v_10, MatlabMethod.MatrixPow(MatlabMethod.MatrixDotDiv(z_j, z_b), alpha));
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

            Matrix[] h = new Matrix[nf];
            for (int ii = 0; ii < nf; ii++)                  //nf个矩阵循环
            {
                h[ii] = new Matrix(np, np);
                if (s_ij[ii] != (s_ij[ii]).Transposition())
                {
                    throw new Exception("不是对称方阵，无法进行cholesky分解");
                }
                for (int i = 1; i <= np; i++)
                {
                    h[ii][i, i] = s_ij[ii][i, i];
                    if (i > 1)
                    {
                        for (int kk = 1; kk <= i - 1; kk++)
                        {
                            h[ii][i, i] -= h[ii][kk, i] * h[ii][kk, i];
                        }
                        h[ii][i, i] = Math.Sqrt(h[ii][i, i]);
                    }
                    else
                    {
                        h[ii][i, i] = Math.Sqrt(h[ii][i, i]);
                    }

                    for (int jj = i + 1; jj <= np; jj++)
                    {
                        h[ii][i, jj] = s_ij[ii][i, jj];
                        if (i == 1)
                        {
                            h[ii][i, jj] = h[ii][i, jj] / h[ii][i, i];
                        }
                        else
                        {
                            for (int kk = 1; kk <= i - 1; kk++)
                            {
                                h[ii][i, jj] -= h[ii][kk, i] * h[ii][kk, jj];
                            }
                            h[ii][i, jj] = h[ii][i, jj] / h[ii][i, i];
                        }

                    }
                }
                h[ii] = h[ii].Transposition();
            }
            int nt = 4096;
            double dt = 0.1;
            double dw = f_sup * 2 * (Math.PI) / nf;
            Matrix v = new Matrix(nt, np);
            Random rand = new Random();
            Matrix w_ml = new Matrix(1, nf);
            Matrix n_w_ml = new Matrix(1, nf);
            Matrix phai = new Matrix(1, nf);
            Matrix resharp = new Matrix(1, nf);////////////////
            for (int jj = 1; jj <=np; jj++)
            {
                for (int ii = 1; ii <= nt; ii++)
                {
                    double t = (ii - 1) * dt;
                    for (int m = 1; m <=jj ; m++)
                    {
                        w_ml = (MatlabMethod.Linspace(1, nf, nf) * dw - ((np - (double)m) / np * dw));
                        n_w_ml = MatlabMethod.Round(w_ml / dw);
                        for (int i = 1; i <= nf; i++)
                        {
                            if (n_w_ml[1, i] < 1)
                            {
                                n_w_ml[1, i] = 1;
                            }
                            if (n_w_ml[1, i] >nf )
                            {
                                n_w_ml[1, i] = nf;
                            }
                        }
                        phai = MatlabMethod.rand(1, nf);  

                        for (int i = 1; i <= nf; i++)
                        {
                            resharp[1, i] = h[(int)n_w_ml[1,i]-1][jj, m];
                        }
                        v[ii, jj] += MatlabMethod.sum(resharp * Math.Sqrt(2 * dw) * MatlabMethod.Cos(w_ml * (t + theta[jj, m]) + phai));
                    }
                }
            }
      }
        

    }
}
