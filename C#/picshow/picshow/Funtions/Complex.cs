using System;
using System.Collections.Generic;
using System.Text;

namespace picshow
{
    class Complex
    {
        // 复数的实部
        private double real = 0.0;

        // 复数的虚部
        private double imaginary = 0.0;

        // 实部的属性
        public double Real
        {
            get
            {
                return real;
            }
            set 
            {
                real = value;
            }
        }

        // 虚部的属性
        public double Imaginary
        {
            get
            {
                return imaginary;
            }
            set
            {
                imaginary = value;
            }
        }

        // 基本构造函数
        public Complex() 
        {
            real = 0.0;
            imaginary = 0.0;
        }

        // 带指定值的构造函数
        public Complex(double r, double i) 
        {
            real = r;
            imaginary = i;
        }

        // 复制构造函数
        public Complex(Complex other)
        {
            real = other.real;
            imaginary = other.imaginary;
        }

        //重载 + 运算符
        public static Complex operator +(Complex c1, Complex c2)
        {
            return c1.Add(c2);
        }

        // 重载 - 运算符
        public static Complex operator -(Complex c1, Complex c2)
        {
            return c1.Subtract(c2);
        }

        // 重载 * 运算符
        public static Complex operator *(Complex c1, Complex c2)
        {
            return c1.Multiply(c2);
        }

        // 实现复数加法
        public Complex Add(Complex c)
        {
            double r = real + c.real;
            double i = imaginary + c.imaginary;
            return new Complex(r, i);
        }

        // 实现复数减法
        public Complex Subtract(Complex c)
        {
            double r = real - c.real;
            double i = imaginary - c.imaginary;
            return new Complex(r, i);
        }

        //实现复数乘法
        public Complex Multiply(Complex c)
        {
            double r = real * c.real - imaginary * c.imaginary;
            double i = real * c.imaginary + imaginary * c.real;
            return new Complex(r, i);
        }

        // 求幅度
        public double Abs()
        { 
            // 实部的绝对值
            double r = Math.Abs(real);
            // 虚部的绝对值
            double i = Math.Abs(imaginary);

            // 特殊情况
            if (real == 0.0)
                return i;
            if (imaginary == 0.0)
                return r;
            // 计算模
            if (r > i)
                return (r * Math.Sqrt(1 + (i / r) * (i / r)));
            else
                return (i * Math.Sqrt(1 + (r / i) * (r / i)));
        }

        // 求相位角
        public double Angle()
        {
            if (real == 0.0 && imaginary == 0.0)
                return 0;

            if (real == 0.0)
            {
                if (imaginary > 0)
                    return Math.PI / 2;
                else
                    return -Math.PI / 2;

            }
            else
            {
                if (real > 0)
                {
                    return Math.Atan2(imaginary, real);
                }
                else
                {
                    if (imaginary >= 0)
                        return Math.Atan2(imaginary, real) + Math.PI;
                    else
                        return Math.Atan2(imaginary, real) - Math.PI;
                }
            }
        }

        // 共轭复数
        public Complex Conjugate()
        {
            return new Complex(real, -imaginary);
        }
    }
}
