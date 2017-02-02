using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTandIFFT
{
    /// <summary>
    /// 复数类
    /// </summary>
    public class Complex
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Complex()
            : this(0, 0)
        {
        }

        /// <summary>
        /// 只有实部的构造函数
        /// </summary>
        /// <param name="real">实部</param>
        public Complex(double real)
            : this(real, 0) { }

        /// <summary>
        /// 由实部和虚部构造
        /// </summary>
        /// <param name="real">实部</param>
        /// <param name="image">虚部</param>
        public Complex(double real, double image)
        {
            this.real = real;
            this.image = image;
        }

        private double real;
        /// <summary>
        /// 复数的实部
        /// </summary>
        public double Real
        {
            get { return real; }
            set { real = value; }
        }

        private double image;
        /// <summary>
        /// 复数的虚部
        /// </summary>
        public double Image
        {
            get { return image; }
            set { image = value; }
        }

        ///复数加法
        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.real + c2.real, c1.image + c2.image);
        }

        ///复数与实数的加法1
        public static Complex operator +(Complex c, double d)
        {
            return c + new Complex(d);
        }

        ///复数与实数的加法2
        public static Complex operator +(double d, Complex c)
        {
            return c + d;
        }

        ///复数减法
        public static Complex operator -(Complex c1, Complex c2)
        {
            return new Complex(c1.real - c2.real, c1.image - c2.image);
        }

        ///复数与实数的减法1
        public static Complex operator -(Complex c, double d)
        {
            return c - new Complex(d);
        }

        ///复数与实数的减法2
        public static Complex operator -(double d, Complex c)
        {
            return c - d;
        }

        ///复数乘法
        public static Complex operator *(Complex c1, Complex c2)
        {
            return new Complex(c1.real * c2.real - c1.image * c2.image, c1.image * c2.real + c1.real * c2.image);
        }


        /// 复数与实数乘法1
        public static Complex operator *(Complex c, double d)
        {
            return c * new Complex(d);
        }

        /// 复数与实数乘法2
        public static Complex operator *(double d, Complex c)
        {
            return c * d;
        }

        /// <summary>
        /// 求共轭复数
        /// </summary>
        /// <returns></returns>
        public Complex Conjugate()
        {
            return new Complex(this.real, -this.image);
        }

        /// 复数除以实数
        public static Complex operator /(Complex c, double d)
        {
            return c * (1 / d);
        }

        ///复数除法
        public static Complex operator /(Complex c1, Complex c2)
        {
            return c1 * c2.Conjugate() / (c2.real * c2.real + c2.image * c2.image);
        }

        ///实数除以复数
        public static Complex operator /(double d, Complex c2)
        {
            return new Complex(d) / c2;
        }

        /// <summary>
        /// 求复数的模
        /// </summary>
        /// <returns>模</returns>
        public double ToModul()
        {
            return Math.Sqrt(real * real + image * image);
        }

        /// <summary>
        /// 重载ToString方法
        /// </summary>
        /// <returns>打印字符串</returns>
        public override string ToString()
        {
            if (Real == 0 && Image == 0)
            {
                return string.Format("{0}", 0);
            }
            if (Real == 0 && (Image != 1 && Image != -1))
            {
                return string.Format("{0} i", Image);
            }
            if (Image == 0)
            {
                return string.Format("{0}", Real);
            }
            if (Image == 1)
            {
                return string.Format("i");
            }
            if (Image == -1)
            {
                return string.Format("- i");
            }
            if (Image < 0)
            {
                return string.Format("{0} - {1} i", Real, -Image);
            }
            return string.Format("{0} + {1} i", Real, Image);
        }
    }
}
