using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FFTandIFFT
{
    /// <summary>
    /// 做FFT和IFFT的类
    /// </summary>
    public class FFT_IFFT
    {
        private IntPtr pin, pout, fplan;
        private double[] input;
        private bool invert;
        /// <summary>
        /// 生成实例
        /// </summary>
        /// <param name="input">实数信号输入</param>
        /// <param name="inver">false:进行fft，true:进行ifft</param>
        public FFT_IFFT(double[] input, bool invert)
        {
            double[] din = new double[input.Length * 2];
            for (int i = 0; i < din.Length; i++)
            {
                if (i % 2 == 0)
                {
                    din[i] = input[i / 2];
                }
                else
                {
                    din[i] = 0;
                }
            }
            this.input = din;
            this.invert = invert;
        }
        /// <summary>
        /// 生成实例
        /// </summary>
        /// <param name="input">复数信号输入，只能进行fft变化</param>
        public FFT_IFFT(Complex[] input)
        {
            double[] din = new double[input.Length * 2];
            for (int i = 0; i < input.Length; i++)
            {
                din[i * 2] = input[i].Real;
                din[i * 2 + 1] = input[i].Image;
            }
            this.input = din;
            this.invert = false;
        }

        public Complex[] FFT()
        {
            if (invert)
            {
                throw new Exception("该类不能进行FFT");
            }
            else
            {
                int n = input.Length / 2;
                double[] dout = new double[n * 2];
                GCHandle hdin = GCHandle.Alloc(input, GCHandleType.Pinned);
                GCHandle hdout = GCHandle.Alloc(dout, GCHandleType.Pinned);
                fplan = fftw.dft_1d(n, hdin.AddrOfPinnedObject(), hdout.AddrOfPinnedObject(), fftw_direction.Forward, fftw_flags.Estimate);
                fftw.execute(fplan);
                fftw.destroy_plan(fplan);
                hdin.Free();
                hdout.Free();
                Complex[] output = new Complex[n];
                for (int i = 0; i < n; i++)
                {
                    output[i] = new Complex(dout[2 * i], dout[2 * i + 1]);
                }
                return output;
            }
        }
        public double[] IFFT()
        {
            if (invert)
            {
                int n = input.Length / 2;
                double[] dout = new double[n];
                GCHandle hdin = GCHandle.Alloc(input, GCHandleType.Pinned);
                GCHandle hdout = GCHandle.Alloc(dout, GCHandleType.Pinned);
                fplan = fftw.dft_c2r_1d(n, hdin.AddrOfPinnedObject(), hdout.AddrOfPinnedObject(), fftw_flags.Estimate);
                fftw.execute(fplan);
                fftw.destroy_plan(fplan);
                hdin.Free();
                hdout.Free();
                for (int i = 0; i < n; i++)
                {
                    dout[i] /= n;
                }
                return dout;
            }
            else
            {
                throw new Exception("该类不能进行IFFT");
            }
        }
    }
}
