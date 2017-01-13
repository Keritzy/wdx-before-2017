using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
/* 对于32Argb来说，每个像素由四个byte组成
 * 从0到3分别为，b, g, r, A
 */


namespace picshow
{
    class GraphicClass
    {
        /* 改变某一图片的灰度级别
         * 返回处理后的图片
         */
        static public Bitmap changeGrayLevel(int num, Bitmap bit)
        {
            int width = bit.Width;
            int height = bit.Height;
            int level;
            int classes = 256 / num;
            int segment = 256 / (num - 1);
            double grey;

            // 设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            // 锁定bmp到系统内存中
            BitmapData data = bit.LockBits(rect, mode, format);

            // 获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            byte[] rgbValues = new byte[numBytes];

            // 将bmp数据Copy到声明的数组中
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                grey = rgbValues[i] * 0.11 + rgbValues[i + 1] * 0.59 + rgbValues[i + 2] * 0.3;
                level = (int)(grey / classes);
                if (level <= 0)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        rgbValues[i + j] = 0;
                    }
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        rgbValues[i + j] = (byte)(level * segment - 1);
                    }
                }

            }
            // 将数据Copy到内存指针
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            // 从系统内存解锁bmp
            bit.UnlockBits(data);
            return bit;
        }

        /* 改变当前图片显示的大小
         * 返回缩小或放大的图片
         */
        static public Bitmap zoomPicture(int num, Bitmap ori)
        {
            Color c = new Color();
            Bitmap ret = new Bitmap(ori.Width / num + 1, ori.Height / num + 1);
            for (int i = 0; i < ori.Width; i += num)
            {
                for (int j = 0; j < ori.Height; j += num)
                {
                    c = ori.GetPixel(i, j);
                    ret.SetPixel(i / num, j / num, c);
                }
            }
            return ret;
        }

        /* 获取红色通道
         * 返回红色图
         */
        static public Bitmap getRedChannel(Bitmap bit)
        {
            int width = bit.Width;
            int height = bit.Height;

            // 设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            // 锁定bmp到系统内存中
            BitmapData data = bit.LockBits(rect, mode, format);

            // 获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            byte[] rgbValues = new byte[numBytes];

            // 将bmp数据Copy到声明的数组中
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                rgbValues[i + 1] = 0;
                rgbValues[i] = 0;
            }
            // 将数据Copy到内存指针
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            // 从系统内存解锁bmp
            bit.UnlockBits(data);
            return bit;
        }

        /* 获取绿色通道
         * 返回绿色图
         */
        static public Bitmap getGreenChannel(Bitmap bit)
        {
            int width = bit.Width;
            int height = bit.Height;

            // 设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            // 锁定bmp到系统内存中
            BitmapData data = bit.LockBits(rect, mode, format);

            // 获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            byte[] rgbValues = new byte[numBytes];

            // 将bmp数据Copy到声明的数组中
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                rgbValues[i] = 0;
                rgbValues[i + 2] = 0;
            }
            // 将数据Copy到内存指针
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            // 从系统内存解锁bmp
            bit.UnlockBits(data);
            return bit;
        }

        /* 获取蓝色通道
         * 返回蓝色图
         */
        static public Bitmap getBlueChannel(Bitmap bit)
        {
            int width = bit.Width;
            int height = bit.Height;

            // 设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            // 锁定bmp到系统内存中
            BitmapData data = bit.LockBits(rect, mode, format);

            // 获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            byte[] rgbValues = new byte[numBytes];

            // 将bmp数据Copy到声明的数组中
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                rgbValues[i + 1] = 0;
                rgbValues[i + 2] = 0;
            }
            // 将数据Copy到内存指针
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            // 从系统内存解锁bmp
            bit.UnlockBits(data);
            return bit;
        }

        // 声明代理
        public delegate float MyDelegate(float a);

        //检测rgb是否越界
        static public float CheckRange(float a)
        {
            if (a > 0)
            {
                a = Math.Min(255, a);
            }
            else
            {
                a = Math.Max(0, a);
            }
            return a;
        }

        //对差图进行范围标定，标定方法有待研究
        static public float Scaling(float a)
        {
            a = (a + (int)(Math.Pow(2, 8) - 1)) / 2;
            return a;
        }

        // 用 3x3 的掩膜来增强图片
        static public Bitmap mask(float[] a, Bitmap bit)
        {
            MyDelegate normal = new MyDelegate(CheckRange);
            return Mask(a, bit, normal);
        }

        //滤波过程中对差图进行标定
        static public Bitmap maskWithScaling(float[] a, Bitmap bit)
        {
            MyDelegate withScaling = new MyDelegate(Scaling);
            return Mask(a, bit, withScaling);
        }

        //choice为对rgb的处理函数
        static public Bitmap Mask(float[] a, Bitmap bit, MyDelegate choice)
        {
            int w = bit.Width;
            int h = bit.Height;
            Bitmap bmpRtn = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            BitmapData srcData = bit.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dstData = bmpRtn.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* pIn = (byte*)srcData.Scan0.ToPointer();
                byte* pOut = (byte*)dstData.Scan0.ToPointer();
                int stride = srcData.Stride;
                byte* p;

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        int[] r = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        int[] g = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        int[] b = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        float vR = 0, vG = 0, vB = 0;
                        // 不处理边界
                        if (x == 0 || x == w - 1 || y == 0 || y == h - 1)
                        {
                            pOut[0] = pIn[0];
                            pOut[1] = pIn[1];
                            pOut[2] = pIn[2];
                        }
                        else
                        {
                            //左上
                            p = pIn - stride - 3;
                            r[0] = p[2];
                            g[0] = p[1];
                            b[0] = p[0];
                            //正上
                            p = pIn - stride;
                            r[1] = p[2];
                            g[1] = p[1];
                            b[1] = p[0];
                            //右上
                            p = pIn - stride + 3;
                            r[2] = p[2];
                            g[2] = p[1];
                            b[2] = p[0];
                            //左侧
                            p = pIn - 3;
                            r[3] = p[2];
                            g[3] = p[1];
                            b[3] = p[0];
                            //右侧
                            p = pIn + 3;
                            r[5] = p[2];
                            g[5] = p[1];
                            b[5] = p[0];
                            //左下
                            p = pIn + stride - 3;
                            r[6] = p[2];
                            g[6] = p[1];
                            b[6] = p[0];
                            //正下
                            p = pIn + stride;
                            r[7] = p[2];
                            g[7] = p[1];
                            b[7] = p[0];
                            //右下
                            p = pIn + stride + 3;
                            r[8] = p[2];
                            g[8] = p[1];
                            b[8] = p[0];
                            //自己
                            p = pIn;
                            r[4] = p[2];
                            g[4] = p[1];
                            b[4] = p[0];
                        }
                        // 计算掩模
                        for (int i = 0; i < 9; i++)
                        {
                            vR += a[i] * r[i];
                            vG += a[i] * g[i];
                            vB += a[i] * b[i];
                        }

                        vR = choice(vR);
                        vG = choice(vG);
                        vB = choice(vB);

                        pOut[0] = (byte)vB;
                        pOut[1] = (byte)vG;
                        pOut[2] = (byte)vR;

                        pIn += 3;
                        pOut += 3;
                    }// end of x
                    pIn += srcData.Stride - w * 3;
                    pOut += srcData.Stride - w * 3;
                } // end of y
            }

            bit.UnlockBits(srcData);
            bmpRtn.UnlockBits(dstData);
            return bmpRtn;
        }

        /* 半色调处理函数 project02-01
         * 返回处理后的新图片
         */
        static public Bitmap halftoning(Bitmap bit)
        {
            // 10个灰度级的表示，一行表示一个灰度级
            int[,] greylevel = {{    0,    0,    0,    0,    0,    0,    0,    0,    0},    // 0, 黑色
                                        {    0,255,    0,    0,    0,    0,    0,    0,    0},
                                        {    0,255,    0,    0,    0,    0,    0,    0,255},
                                        {255,255,    0,    0,    0,    0,    0,    0,255},
                                        {255,255,    0,    0,    0,    0,255,    0,255},
                                        {255,255,255,    0,    0,    0,255,    0,255},
                                        {255,255,255,    0,    0,255,255,    0,255},
                                        {255,255,255,    0,    0,255,255,255,255},
                                        {255,255,255,255,    0,255,255,255,255},
                                        {255,255,255,255,255,255,255,255,255}};   // 9, 白色

            int level;
            double classes = 256 / 10;
            double grey;

            int width = bit.Width;
            int height = bit.Height;

            // 创建一张新位图存储结果
            Bitmap halfbit = new Bitmap(width * 3, height * 3);

            //设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            Rectangle halfrect = new Rectangle(0, 0, 3 * width, 3 * height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            //锁定bmp到系统内存中
            BitmapData data = bit.LockBits(rect, mode, format);
            BitmapData halfdata = halfbit.LockBits(halfrect, mode, format);

            //获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;
            IntPtr halfptr = halfdata.Scan0;

            int numBytes = width * height * 4;
            int halfnumBytes = numBytes * 9;
            int halfline = width * 3 * 4;
            int line = width * 4;
            int row, col = 0;
            byte[] rgbValues = new byte[numBytes];
            byte[] halfrgbValues = new byte[halfnumBytes];

            //将bmp数据Copy到申明的数组中
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                grey = rgbValues[i];
                level = (int)(grey / classes);
                if (level > 9)
                    level = 9;

                row = i / line;
                col = i % line;

                int index = 0;
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++, index++)
                    {
                        halfrgbValues[(y + row * 3) * halfline + x * 4 + col * 3] =
                            halfrgbValues[(y + row * 3) * halfline + x * 4 + col * 3 + 1] =
                            halfrgbValues[(y + row * 3) * halfline + x * 4 + col * 3 + 2] = (byte)greylevel[level, index];
                        halfrgbValues[(y + row * 3) * halfline + x * 4 + col * 3 + 3] = 255;
                    }
                }
            }
            //将数据Copy到内存指针
            Marshal.Copy(halfrgbValues, 0, halfptr, halfnumBytes);
            //从系统内存解锁bmp
            bit.UnlockBits(data);
            halfbit.UnlockBits(halfdata);
            return halfbit;
        }

        /* 生成一张测试图片
         * 返回该图片
         */
        static public Bitmap generateTestPicture(int num)
        {
            Bitmap bit = new Bitmap(256, 256);
            int width = bit.Width;
            int height = bit.Height;

            //设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            //锁定bmp到系统内存中
            BitmapData data = bit.LockBits(rect, mode, format);

            //获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            byte[] rgbValues = new byte[numBytes];

            //将bmp数据Copy到申明的数组中
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            switch (num)
            {
                case 1:
                    {
                        for (int i = 0, index = 0; i < rgbValues.Length; i += 4)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                rgbValues[i + j] = (byte)index;
                            }
                            rgbValues[i + 3] = 255;
                            index++;
                        }
                        break;
                    }
            }

            //将数据Copy到内存指针
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            //从系统内存解锁bmp
            bit.UnlockBits(data);
            return bit;
        }

        /* 将一张RGB图转换为灰度图
         * 返回这张灰度图
         */
        static public Bitmap convertRGB2Gray(Bitmap bit)
        {
            int width = bit.Width;
            int height = bit.Height;

            //设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            //锁定bmp到系统内存中
            BitmapData data = bit.LockBits(rect, mode, format);

            //获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            byte[] rgbValues = new byte[numBytes];

            //将bmp数据Copy到申明的数组中
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                int value = 0;
                value = (int)((rgbValues[i] + rgbValues[i + 1] + rgbValues[i + 2]) / 3);
                //将数组中存放R、G、B的值修改为计算后的值
                for (int j = 0; j < 3; j++)
                {
                    rgbValues[i + j] = (byte)value;
                }
            }
            //将数据Copy到内存指针
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            //从系统内存解锁bmp
            bit.UnlockBits(data);
            return bit;
        }

        /* 画直方图的函数，进行操作的必须是一张灰度图
         * 返回直方图
         */
        static public Bitmap drawHistogram(Bitmap bit)
        {
            // 防止传入彩图，先将图片转换为灰度图
            Bitmap grayBmp = convertRGB2Gray(bit);

            // 创建一张新位图来存放直方图
            int histo_height = 200;
            int histo_width = 270;
            Bitmap histoBmp = new Bitmap(histo_width, histo_height);
            Pen black_pen = new Pen(Color.Black, 2);
            Pen red_pen = new Pen(Color.Red, 1);
            Graphics g = Graphics.FromImage(histoBmp);

            // 画直方图的坐标轴和背景色
            g.FillRectangle(Brushes.LightYellow, 0, 0, histo_width, histo_height);
            g.DrawLine(black_pen, 2, 2, 2, histo_height - 2);
            g.DrawLine(black_pen, 2, histo_height - 2, histo_width - 2, histo_height - 2);

            // 获取图片的高和宽
            int ph = grayBmp.Height; // height of the picture
            int pw = grayBmp.Width;  // width of the picture
            //设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, pw, ph);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;
            //锁定bmp到系统内存中
            BitmapData data = grayBmp.LockBits(rect, mode, format);
            //获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;
            int numBytes = ph * pw * 4;
            byte[] rgbValues = new byte[numBytes];
            int[] count_array = new int[256];
            int max = 0;

            //将bmp数据Copy到声明的数组中
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            // 初始化计数器
            for (int i = 0; i < 256; i++)
            {
                count_array[i] = 0;
            }

            // 计算每个灰度级的像素个数
            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                int value = rgbValues[i];
                count_array[value]++;
                // 记录最大值
                if (max < count_array[value])
                    max = count_array[value];
            }
            //从系统内存解锁bmp
            grayBmp.UnlockBits(data);

            // 根据最大值来拉伸直方图的高度，便于观察
            for (int i = 0; i < 256; i++)
            {
                int col = count_array[i] * 190 / max;
                g.DrawLine(red_pen, 3 + i, histo_height - 3, 3 + i, histo_height - 3 - col);
            }

            return histoBmp;

        }

        /* 直方图均衡化函数，进行操作的必须是一张灰度图
         * 返回经过均衡化操作的新位图
         */
        static public Bitmap histogramEqualization(Bitmap bit)
        {
            // 防止传入彩图，先将图片转换为灰度图
            Bitmap grayBmp = convertRGB2Gray(bit);

            // 创建一张新位图来存放均衡化结果
            Bitmap resultBmp = new Bitmap(grayBmp.Width, grayBmp.Height);

            // 获取图片的高和宽
            int ph = grayBmp.Height;
            int pw = grayBmp.Width;
            int total = ph * pw;

            //设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, pw, ph);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;
            //锁定bmp到系统内存中
            BitmapData data = grayBmp.LockBits(rect, mode, format);
            BitmapData rltdata = resultBmp.LockBits(rect, mode, format);
            //获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;
            IntPtr rltptr = rltdata.Scan0;
            int numBytes = ph * pw * 4;
            byte[] rgbValues = new byte[numBytes];
            byte[] rltrgbValues = new byte[numBytes];

            //将bmp数据Copy到声明的数组中
            Marshal.Copy(ptr, rgbValues, 0, numBytes);


            // 直方图计数器，均衡化后的计数器，概率密度函数
            int[] count_array = new int[256];
            int[] new_count_array = new int[256];
            double[] pdf = new double[256];

            double gray, newgray;
            int max = 0;

            // 初始化计数器
            for (int i = 0; i < 256; i++)
            {
                count_array[i] = 0;
                new_count_array[i] = 0;
            }

            // 计算每个灰度值的像素个数
            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                int value = rgbValues[i];
                count_array[value]++;
            }
            //从系统内存解锁bmp
            grayBmp.UnlockBits(data);

            // 计算概率密度函数
            pdf[0] = count_array[0] / (double)total;
            for (int i = 1; i < 256; i++)
            {
                pdf[i] = pdf[i - 1] + count_array[i] / (double)total;

            }

            // 根据pdf做直方图均衡化操作
            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                gray = rgbValues[i];
                newgray = 255 * pdf[(int)gray];
                new_count_array[(int)newgray]++;
                if (max < new_count_array[(int)newgray])
                    max = new_count_array[(int)newgray];
                for (int j = 0; j < 3; j++)
                {
                    rltrgbValues[i + j] = (byte)((int)newgray);
                }
                rltrgbValues[i + 3] = 255;
            }
            //将数据Copy到内存指针
            Marshal.Copy(rltrgbValues, 0, rltptr, numBytes);
            //从系统内存解锁bmp
            resultBmp.UnlockBits(data);
            return resultBmp;
        }


        /* 两张图片相减的函数
         * 必须先检查两张图片的维度是否相等
         * 返回相减的结果 bit1 - bit2
         */
        static public Bitmap subtractPicture(Bitmap bit1, Bitmap bit2)
        {
            int width = bit1.Width;
            int height = bit1.Height;
            Bitmap bmp = new Bitmap(bit1.Width, bit1.Height);
            //设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            //锁定bmp到系统内存中
            BitmapData data1 = bit1.LockBits(rect, mode, format);
            BitmapData data2 = bit2.LockBits(rect, mode, format);
            BitmapData data = bmp.LockBits(rect, mode, format);

            //获取位图中第一个像素数据的地址
            IntPtr ptr1 = data1.Scan0;
            IntPtr ptr2 = data2.Scan0;
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            byte[] rgbValues1 = new byte[numBytes];
            byte[] rgbValues2 = new byte[numBytes];
            byte[] rgbValues = new byte[numBytes];

            //将bmp数据Copy到申明的数组中
            Marshal.Copy(ptr1, rgbValues1, 0, numBytes);
            Marshal.Copy(ptr2, rgbValues2, 0, numBytes);
            int r, g, b;

            for (int i = 0; i < rgbValues1.Length; i += 4)
            {
                b = (int)CheckRange(rgbValues1[i] - rgbValues2[i]);
                g = (int)CheckRange(rgbValues1[i + 1] - rgbValues2[i + 1]);
                r = (int)CheckRange(rgbValues1[i + 2] - rgbValues2[i + 2]);

                rgbValues[i] = (byte)b;
                rgbValues[i + 1] = (byte)g;
                rgbValues[i + 2] = (byte)r;
                rgbValues[i + 3] = 255;

            }
            //将数据Copy到内存指针
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            //从系统内存解锁bmp
            bit1.UnlockBits(data1);
            bit2.UnlockBits(data2);
            bmp.UnlockBits(data);
            return bmp;
        }

        /* 两张图片相加的函数
         * 必须先检查两张图片的维度是否相等
         * 返回相加的结果 bit1 + k * bit2
         */
        static public Bitmap addPicture(Bitmap bit1, Bitmap bit2, int k)
        {
            int width = bit1.Width;
            int height = bit1.Height;
            Bitmap bmp = new Bitmap(bit1.Width, bit1.Height);
            //设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            //锁定bmp到系统内存中
            BitmapData data1 = bit1.LockBits(rect, mode, format);
            BitmapData data2 = bit2.LockBits(rect, mode, format);
            BitmapData data = bmp.LockBits(rect, mode, format);

            //获取位图中第一个像素数据的地址
            IntPtr ptr1 = data1.Scan0;
            IntPtr ptr2 = data2.Scan0;
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            byte[] rgbValues1 = new byte[numBytes];
            byte[] rgbValues2 = new byte[numBytes];
            byte[] rgbValues = new byte[numBytes];

            //将bmp数据Copy到申明的数组中
            Marshal.Copy(ptr1, rgbValues1, 0, numBytes);
            Marshal.Copy(ptr2, rgbValues2, 0, numBytes);
            int r, g, b;

            for (int i = 0; i < rgbValues1.Length; i += 4)
            {
                b = (int)CheckRange(rgbValues1[i] + k * rgbValues2[i]);
                g = (int)CheckRange(rgbValues1[i + 1] + k * rgbValues2[i + 1]);
                r = (int)CheckRange(rgbValues1[i + 2] + k * rgbValues2[i + 2]);

                rgbValues[i] = (byte)b;
                rgbValues[i + 1] = (byte)g;
                rgbValues[i + 2] = (byte)r;
                rgbValues[i + 3] = 255;

            }
            //将数据Copy到内存指针
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            //从系统内存解锁bmp
            bit1.UnlockBits(data1);
            bit2.UnlockBits(data2);
            bmp.UnlockBits(data);
            return bmp;
        }

        /* 两张图片相乘的函数
         * 必须先检查两张图片的维度是否相等
         * 返回相加的结果 bit1 *  bit2
         */
        static public Bitmap multiplyPicture(Bitmap bit1, Bitmap bit2)
        {
            int width = bit1.Width;
            int height = bit1.Height;
            Bitmap bmp = new Bitmap(bit1.Width, bit1.Height);
            //设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            //锁定bmp到系统内存中
            BitmapData data1 = bit1.LockBits(rect, mode, format);
            BitmapData data2 = bit2.LockBits(rect, mode, format);
            BitmapData data = bmp.LockBits(rect, mode, format);

            //获取位图中第一个像素数据的地址
            IntPtr ptr1 = data1.Scan0;
            IntPtr ptr2 = data2.Scan0;
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            byte[] rgbValues1 = new byte[numBytes];
            byte[] rgbValues2 = new byte[numBytes];
            byte[] rgbValues = new byte[numBytes];

            //将bmp数据Copy到申明的数组中
            Marshal.Copy(ptr1, rgbValues1, 0, numBytes);
            Marshal.Copy(ptr2, rgbValues2, 0, numBytes);
            int r, g, b;

            for (int i = 0; i < rgbValues1.Length; i += 4)
            {
                b = (int)CheckRange(rgbValues1[i] * rgbValues2[i]);
                g = (int)CheckRange(rgbValues1[i + 1] * rgbValues2[i + 1]);
                r = (int)CheckRange(rgbValues1[i + 2] * rgbValues2[i + 2]);

                b = (int)CheckRange((float)b);
                r = (int)CheckRange((float)r);
                g = (int)CheckRange((float)g);


                rgbValues[i] = (byte)b;
                rgbValues[i + 1] = (byte)g;
                rgbValues[i + 2] = (byte)r;
                rgbValues[i + 3] = 255;

            }
            //将数据Copy到内存指针
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            //从系统内存解锁bmp
            bit1.UnlockBits(data1);
            bit2.UnlockBits(data2);
            bmp.UnlockBits(data);
            return bmp;
        }

        /* 两张图片相除的函数
         * 必须先检查两张图片的维度是否相等
         * 返回相加的结果 bit1 / bit2
         */
        static public Bitmap dividePicture(Bitmap bit1, Bitmap bit2)
        {
            int width = bit1.Width;
            int height = bit1.Height;
            Bitmap bmp = new Bitmap(bit1.Width, bit1.Height);
            //设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            //锁定bmp到系统内存中
            BitmapData data1 = bit1.LockBits(rect, mode, format);
            BitmapData data2 = bit2.LockBits(rect, mode, format);
            BitmapData data = bmp.LockBits(rect, mode, format);

            //获取位图中第一个像素数据的地址
            IntPtr ptr1 = data1.Scan0;
            IntPtr ptr2 = data2.Scan0;
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            byte[] rgbValues1 = new byte[numBytes];
            byte[] rgbValues2 = new byte[numBytes];
            byte[] rgbValues = new byte[numBytes];

            //将bmp数据Copy到申明的数组中
            Marshal.Copy(ptr1, rgbValues1, 0, numBytes);
            Marshal.Copy(ptr2, rgbValues2, 0, numBytes);
            int r, g, b;

            for (int i = 0; i < rgbValues1.Length; i += 4)
            {
                b = (int)CheckRange(rgbValues1[i] / rgbValues2[i]);
                g = (int)CheckRange(rgbValues1[i + 1] / rgbValues2[i + 1]);
                r = (int)CheckRange(rgbValues1[i + 2] / rgbValues2[i + 2]);

                b = (int)CheckRange((float)b);
                r = (int)CheckRange((float)r);
                g = (int)CheckRange((float)g);

                rgbValues[i] = (byte)b;
                rgbValues[i + 1] = (byte)g;
                rgbValues[i + 2] = (byte)r;
                rgbValues[i + 3] = 255;

            }
            //将数据Copy到内存指针
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            //从系统内存解锁bmp
            bit1.UnlockBits(data1);
            bit2.UnlockBits(data2);
            bmp.UnlockBits(data);
            return bmp;
        }


        /* 高斯函数，得到一张模糊的图片
         * 返回处理后的结果
         */
        static public Bitmap gaussian(Bitmap bit, double deviation)
        {
            Color c = new Color();
            double num = 0;
            for (int i = 0; i < bit.Width; i++)
            {
                for (int j = 0; j < bit.Height; j++)
                {
                    c = bit.GetPixel(i, j);
                    num = -1 * (i * i + j * j) / (2 * deviation);
                    num = CheckRange((float)Math.Pow(2.6, num));
                    Color c1 = Color.FromArgb((int)num, (int)num, (int)num);
                    bit.SetPixel(i, j, c1);
                }
            }
            return bit;
        }


        /* 双线性插值算法
         * 由指定的图片变化为指定的大小 
         */
        static public Bitmap bilinearInterpolation(Bitmap bit, int destWidth, int destHeight)
        {
            int sourceWidth = bit.Width;
            int sourceHeight = bit.Height;

            // 创建一张新位图存储结果
            Bitmap halfbit = new Bitmap(destWidth, destHeight);

            //设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, sourceWidth, sourceHeight);
            Rectangle rtrrect = new Rectangle(0, 0, destWidth, destHeight);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            //锁定bmp到系统内存中
            BitmapData data = bit.LockBits(rect, mode, format);
            BitmapData rtrdata = halfbit.LockBits(rtrrect, mode, format);

            //获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;
            IntPtr rtrptr = rtrdata.Scan0;

            int numBytes = sourceWidth * sourceHeight * 4;
            int rtrnumBytes = destWidth * destHeight * 4;
            int rtrline = destWidth * 4;
            int line = sourceWidth * 4;
            byte[] rgbValues = new byte[numBytes];
            byte[] rtrrgbValues = new byte[rtrnumBytes];

            //将bmp数据Copy到申明的数组中
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            double qu = (double)(sourceWidth - 1) / (double)(destWidth - 1);
            double qv = (double)(sourceHeight - 1) / (double)(destHeight - 1);

            for (int i = 0; i < destWidth; i++)
            {
                for (int j = 0; j < destHeight; j++)
                {

                    double destX = qu * i;
                    double destY = qv * j;
                    int ii = (int)destX;
                    int jj = (int)destY;

                    if (i == destWidth - 1 || j == destHeight - 1)
                    {
                        rtrrgbValues[j * rtrline + i * 4] = rgbValues[jj * line + ii * 4];
                        rtrrgbValues[j * rtrline + i * 4 + 1] = rgbValues[jj * line + ii * 4 + 1];
                        rtrrgbValues[j * rtrline + i * 4 + 2] = rgbValues[jj * line + ii * 4 + 2];
                        rtrrgbValues[j * rtrline + i * 4 + 3] = 255;
                        continue;
                    }

                    double u = destX - ii;
                    double v = destY - jj;
                    double n1 = (1 - u) * (1 - v);
                    double n2 = (1 - u) * v;
                    double n3 = u * (1 - v);
                    double n4 = u * v;

                    byte destR = (byte)(n1 * rgbValues[jj * line + ii * 4 + 2] +
                                                    n2 * rgbValues[jj * line + (ii + 1) * 4 + 2] +
                                                    n3 * rgbValues[(jj + 1) * line + ii * 4 + 2] +
                                                    n4 * rgbValues[(jj + 1) * line + (ii + 1) * 4 + 2]);
                    byte destG = (byte)(n1 * rgbValues[jj * line + ii * 4 + 1] +
                                                    n2 * rgbValues[jj * line + (ii + 1) * 4 + 1] +
                                                    n3 * rgbValues[(jj + 1) * line + ii * 4 + 1] +
                                                    n4 * rgbValues[(jj + 1) * line + (ii + 1) * 4 + 1]);
                    byte destB = (byte)(n1 * rgbValues[jj * line + ii * 4] +
                                                    n2 * rgbValues[jj * line + (ii + 1) * 4] +
                                                    n3 * rgbValues[(jj + 1) * line + ii * 4] +
                                                    n4 * rgbValues[(jj + 1) * line + (ii + 1) * 4]);

                    rtrrgbValues[j * rtrline + i * 4] = destB;
                    rtrrgbValues[j * rtrline + i * 4 + 1] = destG;
                    rtrrgbValues[j * rtrline + i * 4 + 2] = destR;
                    rtrrgbValues[j * rtrline + i * 4 + 3] = 255;
                }
            }

            //将数据Copy到内存指针
            Marshal.Copy(rtrrgbValues, 0, rtrptr, rtrnumBytes);
            //从系统内存解锁bmp
            bit.UnlockBits(data);
            halfbit.UnlockBits(rtrdata);
            return halfbit;
        }




        /* 快速傅里叶变换
         * sourceData->待变换的序列
         * count->序列长度
         */
        static public Complex[] fft(Complex[] sourceData, int count)
        {
            // 求fft的级数
            int level = Convert.ToInt32(Math.Log(count, 2));

            // 求加权系数W
            Complex[] W = new Complex[count / 2];
            Complex[] cloneS = new Complex[count];
            Complex[] tempS = new Complex[count];

            cloneS = (Complex[])sourceData.Clone();
            for (int i = 0; i < count / 2; i++)
            {
                double angle = -i * Math.PI * 2 / count;
                W[i] = new Complex(Math.Cos(angle), Math.Sin(angle));
            }

            // 核心部分，进行蝶形变换
            for (int i = 0; i < level; i++)
            {
                int interval = 1 << i;
                int halfL = 1 << (level - i);
                for (int j = 0; j < interval; j++)
                {
                    int gap = j * halfL;
                    for (int k = 0; k < halfL / 2; k++)
                    {
                        tempS[k + gap] = cloneS[k + gap] + cloneS[k + gap + halfL / 2];
                        tempS[k + halfL / 2 + gap] = (cloneS[k + gap] - cloneS[k + gap + halfL / 2]) * W[k * interval];
                    }
                }
                cloneS = (Complex[])tempS.Clone();
            }

            // 对于变换后的序列进行重新排序
            for (uint j = 0; j < count; j++)
            {
                uint rev = 0;
                uint num = j;
                for (int i = 0; i < level; i++)
                {
                    // WTF is it?
                    rev <<= 1;
                    rev |= num & 1;
                    num >>= 1;
                }
                tempS[rev] = cloneS[j];
            }
            return tempS;
        }

        /* 快速傅里叶逆变换
         * sourceData->待变换的序列
         * count->序列长度
         */
        static public Complex[] ifft(Complex[] sourceData, int count)
        {
            for (int i = 0; i < count; i++)
            {
                sourceData[i] = sourceData[i].Conjugate();
            }

            Complex[] tempS = new Complex[count];
            // 利用快速傅立叶变换计算逆变换，因为根据公式逆变换与原变换只相差一个系数和一个负号
            tempS = fft(sourceData, count);

            // 对所有复数的实数部分除以count，虚数部分除以-count
            for (int i = 0; i < count; i++)
            {
                tempS[i] = new Complex(tempS[i].Real / count, -tempS[i].Imaginary / count);
            }
            return tempS;
        }

        /* 2维的傅里叶变换
         * axischange表示是否进行坐标位移变换
         */
        static public Complex[] fft2d(Bitmap bit, bool axischange)
        {
            Bitmap graybit = GraphicClass.convertRGB2Gray(bit);
            int width = graybit.Width;
            int height = graybit.Height;

            // 设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            // 锁定bmp到系统内存中
            BitmapData data = graybit.LockBits(rect, mode, format);

            // 获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            int numPixel = width * height;
            int line = width * 4;

            byte[] grayValues = new byte[numBytes];
            Complex[] tempC = new Complex[numPixel];

            // 将bmp数据Copy到声明的数组中
            Marshal.Copy(ptr, grayValues, 0, numBytes);
            //从系统内存解锁bmp
            graybit.UnlockBits(data);

            // 将图片数据读入复数序列，以便进行快速傅立叶变换
            for (int i = 0, j = 0; i < numBytes; i += 4, j++)
            {
                if (axischange == true)
                {
                    if ((i / line + i % line / 4) % 2 == 0)
                    {
                        tempC[j] = new Complex(grayValues[i], 0);
                    }
                    else
                    {
                        tempC[j] = new Complex(-grayValues[i], 0);
                    }
                }
                else
                {
                    tempC[j] = new Complex(grayValues[i], 0);
                }
            }


            Complex[] tempC1 = new Complex[width];
            Complex[] tempC2 = new Complex[width];
            // 水平方向进行快速傅立叶变换
            for (int i = 0; i < height; i++)
            {
                // 水平方向复数序列
                for (int j = 0; j < width; j++)
                {
                    tempC1[j] = tempC[i * width + j];
                }
                // 一维傅里叶变换
                tempC2 = fft(tempC1, width);
                // 返回结果
                for (int j = 0; j < width; j++)
                {
                    tempC[i * width + j] = tempC2[j];
                }
            }

            Complex[] tempC3 = new Complex[height];
            Complex[] tempC4 = new Complex[height];
            // 垂直方向进行快速傅立叶变换
            for (int i = 0; i < width; i++)
            {
                // 垂直方向复数序列
                for (int j = 0; j < height; j++)
                {
                    tempC3[j] = tempC[j * width + i];
                }
                // 一维傅里叶变换
                tempC4 = fft(tempC3, height);
                // 返回结果
                for (int j = 0; j < height; j++)
                {
                    tempC[j * height + i] = tempC4[j];
                }
            }

            return tempC;

        }


        /* 2维的逆傅里叶变换
         * axischange表示是否进行坐标位移变换
         */
        static public Bitmap ifft2d(Complex[] c, int width, int height, bool axischange)
        {
            // 设定实例BitmapData相关信息
            Bitmap bit = new Bitmap(width, height);
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            // 锁定bmp到系统内存中
            BitmapData data = bit.LockBits(rect, mode, format);

            // 获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            int numPixel = width * height;

            byte[] grayValues = new byte[numBytes];

            Complex[] tempC = new Complex[numPixel];
            tempC = (Complex[])c.Clone();

            // 水平方向快速傅里叶逆变换
            Complex[] tempC1 = new Complex[width];
            Complex[] tempC2 = new Complex[width];
            for (int i = 0; i < height; i++)
            {
                // 水平方向复数序列
                for (int j = 0; j < width; j++)
                {
                    tempC1[j] = tempC[i * width + j];
                }

                // 一维傅里叶逆变换
                tempC2 = ifft(tempC1, width);

                // 返回结果
                for (int j = 0; j < width; j++)
                {
                    tempC[i * width + j] = tempC2[j];
                }
            }

            // 垂直方向快速傅里叶逆变换
            Complex[] tempC3 = new Complex[height];
            Complex[] tempC4 = new Complex[height];
            for (int i = 0; i < width; i++)
            {
                // 垂直方向复数序列
                for (int j = 0; j < height; j++)
                {
                    tempC3[j] = tempC[j * width + i];
                }

                // 一维傅里叶逆变换
                tempC4 = ifft(tempC3, height);

                // 返回结果
                for (int j = 0; j < width; j++)
                {
                    tempC[j * width + i] = tempC4[j];
                }
            }

            // 把复数转化为实数，只保留复数的实数部分
            double tempD;
            for (int i = 0, j = 0; i < numPixel; i++, j += 4)
            {
                if (axischange == true)
                {
                    if ((i / width + i % width) % 2 == 0)
                        tempD = tempC[i].Real;
                    else
                        tempD = -tempC[i].Real;
                }
                else
                {
                    // 不进行坐标变换
                    tempD = tempC[i].Real;
                }

                if (tempD > 255)
                {
                    grayValues[j] = grayValues[j + 1] = grayValues[j + 2] = grayValues[j + 3] = 255;
                }
                else
                {
                    if (tempD < 0)
                    {
                        grayValues[j] = grayValues[j + 1] = grayValues[j + 2] = 0;
                        grayValues[j + 3] = 255;
                    }
                    else
                    {
                        grayValues[j] = grayValues[j + 1] = grayValues[j + 2] = Convert.ToByte(tempD);
                        grayValues[j + 3] = 255;
                    }

                }

            }
            //将数据Copy到内存指针
            Marshal.Copy(grayValues, 0, ptr, numBytes);
            bit.UnlockBits(data);
            return bit;
        }


        /* 把变换得到的复数转换为图片
         * 
         */
        static public Bitmap complex2Gray(Complex[] c, int width, int height)
        {
            Bitmap bit = new Bitmap(width, height);
            //设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            //锁定bmp到系统内存中
            BitmapData data = bit.LockBits(rect, mode, format);

            //获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            byte[] grayValues = new byte[numBytes];

            int numPixel = width * height;
            double[] tempArray = new double[numPixel];

            //计算频谱
            tempArray = spectrum(c, width, height);

            // 灰度值规范化
            double a = 1000.0, b = 0.0;
            double p;
            for (int i = 0; i < numPixel; i++)
            {
                if (a > tempArray[i])
                    a = tempArray[i];
                if (b < tempArray[i])
                    b = tempArray[i];
            }
            p = 255.0 / (b - a);
            for (int i = 0; i < numPixel; i++)
            {
                grayValues[i * 4] = grayValues[i * 4 + 1] = grayValues[i * 4 + 2] = (byte)(p * (tempArray[i] - a) + 0.5);
                grayValues[i * 4 + 3] = 255;
            }

            //将数据Copy到内存指针
            Marshal.Copy(grayValues, 0, ptr, numBytes);
            //从系统内存解锁bmp
            bit.UnlockBits(data);
            return bit;
        }

        /*
         * 用Log变换计算傅里叶频谱
         */
        static public double[] spectrum(Complex[] c, int width, int height)
        {
            int numPixel = width * height;
            double[] tempArray = new double[numPixel];

            for (int i = 0; i < numPixel; i++)
            {
                //tempArray[i] = Math.Log(1 + c[i].Abs(), 2);
                tempArray[i] = c[i].Abs();//注意不需要经过log变换
            }

            return tempArray;
        }

        /*
         * 利用频谱计算图像的平均值
         */
        public static double aveValue(Bitmap bit, Complex[] c)
        {
            Bitmap graybit = GraphicClass.convertRGB2Gray(bit);
            int width = graybit.Width;
            int height = graybit.Height;
            return spectrum(c, width, height)[0] / (width * height);
        }

        /* 高斯低通滤波器
         */
        static public Complex[] gaussianLowpassFilter(Bitmap bit, double radius)
        {
            // 进行二维快速傅立叶变换
            Complex[] Fuv = fft2d(bit, false);

            int width = bit.Width;
            int height = bit.Height;
            int i, j;

            // 计算高斯低通滤波器
            double[] glFilter = new double[width * height];

            for (i = 0; i < height; i++)
            {
                for (j = 0; j < width; j++)
                {
                    int row = (i < height / 2) ? (i + height / 2) : (i - height / 2);
                    int col = (j < width / 2) ? (j + width / 2) : (j - width / 2);
                    // 计算距傅立叶变换原点的距离
                    double distance = Math.Pow((double)(i - height / 2), 2) + Math.Pow((double)(j - width / 2), 2);
                    // 计算高斯曲线的扩展的程度
                    double degree = Math.Pow(radius, 2);

                    glFilter[row * width + col] = Math.Exp(-distance / (2 * degree));
                }
            }

            // 矩阵阵列相乘运算，进行滤波
            for (i = 0; i < width * height; i++)
            {
                Fuv[i].Real = Fuv[i].Real * glFilter[i];
                Fuv[i].Imaginary = Fuv[i].Imaginary * glFilter[i];
            }

            return Fuv;
        }

        /* 高斯高通滤波器
         */
        static public Complex[] gaussianHighpassFilter(Bitmap bit, double radius)
        {
            // 进行二维快速傅立叶变换
            Complex[] Fuv = fft2d(bit, false);
            int width = bit.Width;
            int height = bit.Height;
            int i, j;

            double[] ghFilter = new double[width * height];

            // 计算高斯高通滤波器，基本与低通相似
            for (i = 0; i < height; i++)
            {
                for (j = 0; j < width; j++)
                {
                    int row = (i < height / 2) ? (i + height / 2) : (i - height / 2);
                    int col = (j < width / 2) ? (j + width / 2) : (j - width / 2);
                    // 计算距傅立叶变换原点的距离
                    double distance = Math.Pow((double)(i - height / 2), 2) + Math.Pow((double)(j - width / 2), 2);
                    // 计算高斯曲线的扩展的程度
                    double degree = Math.Pow(radius, 2);

                    ghFilter[row * width + col] = 1 - Math.Exp(-distance / (2 * degree));
                }
            }
            // 矩阵阵列相乘运算，进行滤波
            for (i = 0; i < width * height; i++)
            {
                Fuv[i].Real = Fuv[i].Real * ghFilter[i];
                Fuv[i].Imaginary = Fuv[i].Imaginary * ghFilter[i];

            }
            return Fuv;

        }

        /* 噪声产生器
         * 接受四个参数
         * bit-位图，value1-参数1，value2-参数2，index-噪声模型编号
         * 其中两个参数根据不同模型有不同意义，具体见代码解释
         */
        static public Bitmap noiseGenerator(Bitmap bit, double value1, double value2, int index)
        {
            int width = bit.Width;
            int height = bit.Height;

            bit = GraphicClass.convertRGB2Gray(bit);
            // 设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            // 锁定bmp到系统内存中
            BitmapData data = bit.LockBits(rect, mode, format);

            // 获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            byte[] rgbValues = new byte[numBytes];

            // 将bmp数据Copy到声明的数组中
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            // 得到两个均匀随机数
            Random r1, r2;
            r1 = new Random(unchecked((int)DateTime.Now.Ticks));
            r2 = new Random(~unchecked((int)DateTime.Now.Ticks));

            double v1, v2;
            double temp = 0;

            for (int i = 0; i < numBytes; i += 4)
            {
                switch (index)
                {
                    case 0: // 高斯噪声
                        do
                        {
                            v1 = r1.NextDouble();
                        }
                        while (v1 <= 0.00000000001);
                        v2 = r2.NextDouble();
                        // 应用雅克比变换，直接生成正态分布
                        // value1-均值，value2-方差
                        temp = Math.Sqrt(-2 * Math.Log(v1)) *
                            Math.Cos(2 * Math.PI * v2) * value2 + value1;
                        break;
                    case 1: // 椒盐噪声
                        // 判断两个含量之和是否小于1，不是就设为默认值
                        // value1-含椒量，value2-含盐量
                        if (value1 + value2 >= 1)
                        {
                            value1 = 0.02;
                            value2 = 0.02;
                        }
                        v1 = r1.NextDouble();
                        if (v1 <= value1)
                            temp = -500;
                        else if (v1 >= (1 - value2))
                            temp = 500;
                        else
                            temp = 0;
                        break;
                    case 2: // Rayleigh噪声
                        // value1-参数a，value2-参数b
                        do
                        {
                            v1 = r1.NextDouble();

                        } while (v1 >= 0.999999999999);
                        temp = value1 + Math.Sqrt(-1 * value2 * Math.Log(1 - v1));
                        break;
                    case 3: // 指数噪声
                        do
                        {
                            v1 = r1.NextDouble();
                        }
                        while (v1 >= 0.999999999999);
                        temp = -1 * Math.Log(1 - v1) / value1;
                        break;

                }
                temp = temp + rgbValues[i];
                if (temp > 255)
                {
                    rgbValues[i] = rgbValues[i + 1] = rgbValues[i + 2] = 255;
                }
                else if (temp < 0)
                {
                    rgbValues[i] = rgbValues[i + 1] = rgbValues[i + 2] = 0;
                }
                else
                    rgbValues[i] = rgbValues[i + 1] = rgbValues[i + 2] = Convert.ToByte(temp);
            }
            // 将数据Copy到内存指针
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            // 从系统内存解锁bmp
            bit.UnlockBits(data);
            return bit;
        }

        /* 中值滤波器
         */
        static public Bitmap medianFiltering(Bitmap bit)
        {
            int w = bit.Width;
            int h = bit.Height;
            bit = GraphicClass.convertRGB2Gray(bit);
            Bitmap bmpRtn = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            BitmapData srcData = bit.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dstData = bmpRtn.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* pIn = (byte*)srcData.Scan0.ToPointer();
                byte* pOut = (byte*)dstData.Scan0.ToPointer();
                int stride = srcData.Stride;
                byte* p;

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        byte[] r = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        byte v;
                        // 不处理边界
                        if (x == 0 || x == w - 1 || y == 0 || y == h - 1)
                        {
                            pOut[0] = pIn[0];

                        }
                        else
                        {
                            //左上
                            p = pIn - stride - 3;
                            r[0] = p[2];
                            //正上
                            p = pIn - stride;
                            r[1] = p[2];
                            //右上
                            p = pIn - stride + 3;
                            r[2] = p[2];
                            //左侧
                            p = pIn - 3;
                            r[3] = p[2];
                            //右侧
                            p = pIn + 3;
                            r[5] = p[2];
                            //左下
                            p = pIn + stride - 3;
                            r[6] = p[2];
                            //正下
                            p = pIn + stride;
                            r[7] = p[2];
                            //右下
                            p = pIn + stride + 3;
                            r[8] = p[2];
                            //自己
                            p = pIn;
                            r[4] = p[2];
                        }
                        // 计算掩模
                        Sort(r);
                        v = r[4];

                        pOut[0] = v;
                        pOut[1] = v;
                        pOut[2] = v;

                        pIn += 3;
                        pOut += 3;
                    }// end of x
                    pIn += srcData.Stride - w * 3;
                    pOut += srcData.Stride - w * 3;
                } // end of y
            }

            bit.UnlockBits(srcData);
            bmpRtn.UnlockBits(dstData);
            return bmpRtn;
        }

        /* 最值滤波器
         * 1为最大值，2为最小值，3为中点
         */
        static public Bitmap greatValueFiltering(Bitmap bit, int index)
        {
            int w = bit.Width;
            int h = bit.Height;
            bit = GraphicClass.convertRGB2Gray(bit);
            Bitmap bmpRtn = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            BitmapData srcData = bit.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dstData = bmpRtn.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* pIn = (byte*)srcData.Scan0.ToPointer();
                byte* pOut = (byte*)dstData.Scan0.ToPointer();
                int stride = srcData.Stride;
                byte* p;

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        byte[] r = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        byte v = 0;
                        // 不处理边界
                        if (x == 0 || x == w - 1 || y == 0 || y == h - 1)
                        {
                            pOut[0] = pIn[0];

                        }
                        else
                        {
                            //左上
                            p = pIn - stride - 3;
                            r[0] = p[2];
                            //正上
                            p = pIn - stride;
                            r[1] = p[2];
                            //右上
                            p = pIn - stride + 3;
                            r[2] = p[2];
                            //左侧
                            p = pIn - 3;
                            r[3] = p[2];
                            //右侧
                            p = pIn + 3;
                            r[5] = p[2];
                            //左下
                            p = pIn + stride - 3;
                            r[6] = p[2];
                            //正下
                            p = pIn + stride;
                            r[7] = p[2];
                            //右下
                            p = pIn + stride + 3;
                            r[8] = p[2];
                            //自己
                            p = pIn;
                            r[4] = p[2];
                        }
                        // 计算掩模
                        Sort(r);
                        switch (index)
                        {
                            case 0: // 最大值
                                v = r[8];
                                break;
                            case 1: // 最小值
                                v = r[0];
                                break;
                            case 2: // 中点
                                v = (byte)((r[0] + r[8]) / 2);
                                break;
                        }

                        pOut[0] = v;
                        pOut[1] = v;
                        pOut[2] = v;

                        pIn += 3;
                        pOut += 3;
                    }// end of x
                    pIn += srcData.Stride - w * 3;
                    pOut += srcData.Stride - w * 3;
                } // end of y
            }

            bit.UnlockBits(srcData);
            bmpRtn.UnlockBits(dstData);
            return bmpRtn;
        }

        /* 选择排序，用来找中值，最值等
         */
        static private void Sort(byte[] list)
        {
            int min;
            for (int i = 0; i < list.Length - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < list.Length; j++)
                {
                    if (list[j] < list[min])
                        min = j;
                }
                byte t = list[min];
                list[min] = list[i];
                list[i] = t;
            }
        }

        /* 均值滤波器
         * 0为算术均值, 1为几何均值, 2为谐波均值, 3为逆谐波均值
         */
        static public Bitmap AvgFilter(Bitmap bit, int index, int parameter = 0)
        {
            int w = bit.Width;
            int h = bit.Height;
            bit = GraphicClass.convertRGB2Gray(bit);
            Bitmap bmpRtn = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            BitmapData srcData = bit.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dstData = bmpRtn.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* pIn = (byte*)srcData.Scan0.ToPointer();
                byte* pOut = (byte*)dstData.Scan0.ToPointer();
                int stride = srcData.Stride;
                byte* p;

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        byte[] r = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        byte v;
                        // 不处理边界
                        if (x == 0 || x == w - 1 || y == 0 || y == h - 1)
                        {
                            pOut[0] = pIn[0];

                        }
                        else
                        {
                            //左上
                            p = pIn - stride - 3;
                            r[0] = p[2];
                            //正上
                            p = pIn - stride;
                            r[1] = p[2];
                            //右上
                            p = pIn - stride + 3;
                            r[2] = p[2];
                            //左侧
                            p = pIn - 3;
                            r[3] = p[2];
                            //右侧
                            p = pIn + 3;
                            r[5] = p[2];
                            //左下
                            p = pIn + stride - 3;
                            r[6] = p[2];
                            //正下
                            p = pIn + stride;
                            r[7] = p[2];
                            //右下
                            p = pIn + stride + 3;
                            r[8] = p[2];
                            //自己
                            p = pIn;
                            r[4] = p[2];
                        }
                        double temp = 0.0;
                        switch (index)
                        {
                            case 0: // 算术均值
                                {
                                    temp = 0.0;
                                    for (int i = 0; i < 9; i++)
                                        temp += r[i];
                                    temp /= 9;
                                    break;
                                }
                            case 1: // 几何均值
                                {
                                    temp = 1.0;
                                    for (int i = 0; i < 9; i++)
                                    {
                                        temp *= r[i];
                                        if (r[i] == 0)
                                            break;
                                    }
                                    temp = Math.Pow(temp, 1.0 / 9.0);
                                    break;
                                }
                            case 2: // 谐波均值
                                {
                                    temp = 0.0;
                                    for (int i = 0; i < 9; i++)
                                    {
                                        if (r[i] == 0)
                                            temp += 1;
                                        else
                                            temp += 1.0 / r[i];
                                    }
                                    temp = 9.0 / temp;
                                    if (temp > 255)
                                        temp = 255;
                                    break;
                                }
                            case 3: // 逆谐波均值
                                {
                                    double up = 0.0, down = 0.0;
                                    for (int i = 0; i < 9; i++)
                                    {
                                        up += Math.Pow(r[i], parameter + 1);
                                        down += Math.Pow(r[i], parameter);
                                    }
                                    temp = up / down;
                                    if (temp > 255)
                                        temp = 255;
                                    break;
                                }
                        }

                        pOut[0] = Convert.ToByte(temp);
                        pOut[1] = Convert.ToByte(temp);
                        pOut[2] = Convert.ToByte(temp);

                        pIn += 3;
                        pOut += 3;
                    }// end of x
                    pIn += srcData.Stride - w * 3;
                    pOut += srcData.Stride - w * 3;
                } // end of y
            }

            bit.UnlockBits(srcData);
            bmpRtn.UnlockBits(dstData);
            return bmpRtn;
        }


        /* 两幅图片求平均
         * 用于图片平均
         */
        static public Bitmap AvgPicture(Bitmap bit1, Bitmap bit2, int count)
        {
            int width = bit1.Width;
            int height = bit1.Height;
            Bitmap bmp = new Bitmap(bit1.Width, bit1.Height);
            //设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            //锁定bmp到系统内存中
            BitmapData data1 = bit1.LockBits(rect, mode, format);
            BitmapData data2 = bit2.LockBits(rect, mode, format);
            BitmapData data = bmp.LockBits(rect, mode, format);

            //获取位图中第一个像素数据的地址
            IntPtr ptr1 = data1.Scan0;
            IntPtr ptr2 = data2.Scan0;
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            byte[] rgbValues1 = new byte[numBytes];
            byte[] rgbValues2 = new byte[numBytes];
            byte[] rgbValues = new byte[numBytes];

            //将bmp数据Copy到申明的数组中
            Marshal.Copy(ptr1, rgbValues1, 0, numBytes);
            Marshal.Copy(ptr2, rgbValues2, 0, numBytes);
            int r, g, b;

            for (int i = 0; i < rgbValues1.Length; i += 4)
            {
                b = (rgbValues1[i] * count + rgbValues2[i])/(count + 1);
                g = (rgbValues1[i + 1] * count + rgbValues2[i + 1]) / (count + 1);
                r = (rgbValues1[i + 2] * count + rgbValues2[i + 2]) / (count + 1);

                rgbValues[i] = (byte)b;
                rgbValues[i + 1] = (byte)g;
                rgbValues[i + 2] = (byte)r;
                rgbValues[i + 3] = 255;

            }
            //将数据Copy到内存指针
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            //从系统内存解锁bmp
            bit1.UnlockBits(data1);
            bit2.UnlockBits(data2);
            bmp.UnlockBits(data);
            return bmp;
        }

        //boundary为两个range的界限
        //specifiedColor是高亮颜色
        //lowORhigh是选择高亮区间，0为低灰度，1为高灰度
        static public Bitmap PseudoColor(Bitmap bit, double boundary, Color specifiedColor, int lowORhigh)
        {
            int width = bit.Width;
            int height = bit.Height;

            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            // 锁定bmp到系统内存中
            BitmapData data = bit.LockBits(rect, mode, format);

            // 获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;

            int numBytes = width * height * 4;
            byte[] rgbValues = new byte[numBytes];

            // 将bmp数据Copy到声明的数组中
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            byte r = specifiedColor.R;
            byte g = specifiedColor.G;
            byte b = specifiedColor.B;
            byte[] rgb = { b, g, r };  //Color对象的三色值存储顺序

            byte byteboundary = Convert.ToByte(boundary);


            for (int i = 0; i < numBytes; i += 4)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (lowORhigh == 0) //选择灰度级别低的区间
                        rgbValues[i + j] = (rgbValues[i + j] < byteboundary) ? rgb[j] : rgbValues[i + j];
                    else //选择灰度级别高的区间
                        rgbValues[i + j] = (rgbValues[i + j] > byteboundary) ? rgb[j] : rgbValues[i + j];
                }
            }

            // 将数据Copy到内存指针
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            // 从系统内存解锁bmp
            bit.UnlockBits(data);
            return bit;
        }

        /* 计算RGB模型的彩色图片的分量直方图
         * 返回三个分量的统计数据
         */ 
        static private int[,] CalculateHistogram(Bitmap bit)
        {
            // 获取图片的高和宽
            int height = bit.Height; // height of the picture
            int width = bit.Width;  // width of the picture

            //设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;
            //锁定bmp到系统内存中
            BitmapData data = bit.LockBits(rect, mode, format);
            //获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;

            int numBytes = height * width * 4;
            byte[] rgbValues = new byte[numBytes];
            int[,] count_array = new int[3,256];

            //将bmp数据Copy到声明的数组中
            Marshal.Copy(ptr, rgbValues, 0, numBytes);
            
            // 初始化计数器
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 256; j++ )
                    count_array[i,j] = 0;
                

            // 计算每个通道颜色级的像素个数
            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                count_array[0, rgbValues[i]]++;
                count_array[1, rgbValues[i+1]]++;
                count_array[2, rgbValues[i+2]]++;
            }
            //从系统内存解锁bmp
            bit.UnlockBits(data);
            return count_array;
        }

        /* 均衡化RGB模型分量直方图
         * 0 -- 三个分量分别均衡化， 1 -- 使用平均直方图均衡化
         * 返回均衡化后的统计数据
         */ 
        static private double[] ComponentEqualization(int[] count_array, int total)
        {
            // 直方图计数器，均衡化后的计数器，概率密度函数
            int[] new_count_array = new int[256];
            double[] pdf = new double[256];

            // 计算概率密度函数
            pdf[0] = count_array[0] / (double)total;
            for (int i = 1; i < 256; i++)
                pdf[i] = pdf[i - 1] + count_array[i] / (double)total;

            return pdf;
        }

        /* RGB分量分别均衡化
         * 返回均衡化结果
         */ 
        static public Bitmap ColorEqualization(Bitmap bit, int choice)
        {
            // 创建一张新位图来存放均衡化结果
            Bitmap resultBmp = new Bitmap(bit.Width, bit.Height);

            // 获取图片的高和宽
            int ph = bit.Height;
            int pw = bit.Width;
            int total = ph * pw;

            //设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, pw, ph);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;
            //锁定bmp到系统内存中
            BitmapData data = bit.LockBits(rect, mode, format);
            BitmapData rltdata = resultBmp.LockBits(rect, mode, format);
            //获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;
            IntPtr rltptr = rltdata.Scan0;
            int numBytes = ph * pw * 4;
            byte[] rgbValues = new byte[numBytes];
            byte[] rltrgbValues = new byte[numBytes];

            //将bmp数据Copy到声明的数组中
            Marshal.Copy(ptr, rgbValues, 0, numBytes);
            bit.UnlockBits(data);


            // 计算概率密度函数并进行均衡化操作
            int[,] count_array = CalculateHistogram(bit);
            if (choice == 0)
            {
                double[][] pdf = new double[3][];
                int[] temp1 = new int[256];
                int[] temp2 = new int[256];
                int[] temp3 = new int[256];
                for (int i = 0; i < 256; i++)
                {
                    temp1[i] = count_array[0, i];
                    temp2[i] = count_array[1, i];
                    temp3[i] = count_array[2, i];
                }
                pdf[0] = ComponentEqualization(temp1, pw * ph);
                pdf[1] = ComponentEqualization(temp2, pw * ph);
                pdf[2] = ComponentEqualization(temp3, pw * ph);               

                // 对RGB分量做直方图均衡化操作(根据各自分量的直方图)
                for (int i = 0; i < rgbValues.Length; i += 4)
                {
                    int level = rgbValues[i];
                    double newlevel = 255 * pdf[0][level];
                    rltrgbValues[i] = (byte)((int)newlevel);

                    level = rgbValues[i + 1];
                    newlevel = 255 * pdf[1][level];
                    rltrgbValues[i + 1] = (byte)((int)newlevel);

                    level = rgbValues[i + 2];
                    newlevel = 255 * pdf[2][level];
                    rltrgbValues[i + 2] = (byte)((int)newlevel);

                    rltrgbValues[i + 3] = 255;
                }
            }
            
            else if (choice == 1)
            {
                double[] pdf = new double[256];
                for (int i = 0; i < 256; i++)
                {
                    count_array[0,i] = (count_array[0,i] + count_array[1,i] + count_array[2,i]) / 3;
                }
                int[] temp = new int[256];
                for (int i = 0; i < 256; i++)
                    temp[i] = count_array[0, i];

                pdf = ComponentEqualization(temp, pw * ph);

                // 对RGB分量做直方图均衡化操作(根据平均直方图)
                for (int i = 0; i < rgbValues.Length; i += 4)
                {
                    int level = rgbValues[i];
                    double newlevel = 255 * pdf[level];
                    rltrgbValues[i] = (byte)((int)newlevel);

                    level = rgbValues[i + 1];
                    newlevel = 255 * pdf[level];
                    rltrgbValues[i + 1] = (byte)((int)newlevel);

                    level = rgbValues[i + 2];
                    newlevel = 255 * pdf[level];
                    rltrgbValues[i + 2] = (byte)((int)newlevel);

                    rltrgbValues[i + 3] = 255;
                }
            }
            
            //将数据Copy到内存指针
            Marshal.Copy(rltrgbValues, 0, rltptr, numBytes);
            //从系统内存解锁bmp
            resultBmp.UnlockBits(data);
            return resultBmp;
        }

        /* RGB彩色图像均衡化的直方图
         * avg==false：显示某一分量的直方图；avg==true：显示平均直方图
         * 返回直方图结果
         */ 
        static public Bitmap drawColorHistogram(Bitmap bit, Color color, bool avg = false)
        {
            // 创建一张新位图来存放直方图
            int histo_height = 200;
            int histo_width = 270;
            Bitmap histoBmp = new Bitmap(histo_width, histo_height);
            Pen black_pen = new Pen(Color.Black, 2);
            Pen color_pen = new Pen(color, 1);
            Graphics g = Graphics.FromImage(histoBmp);

            // 画直方图的坐标轴和背景色
            g.FillRectangle(Brushes.LightYellow, 0, 0, histo_width, histo_height);
            g.DrawLine(black_pen, 2, 2, 2, histo_height - 2);
            g.DrawLine(black_pen, 2, histo_height - 2, histo_width - 2, histo_height - 2);

            int [,] count_array = CalculateHistogram(bit);
            int max = 0;
            int channel = 0;

            // 根据直方图统计数据画出直方图
            if (!avg)
            {
                if (Color.Red == color)
                    channel = 2;
                else if (Color.Green == color)
                    channel = 1;
                else if (Color.Blue == color)
                    channel = 0;

                // 计算最大值用于拉伸坐标轴
                for (int i = 0; i < 256; i++)
                {
                    if (max < count_array[channel, i])
                        max = count_array[channel, i];
                }               
            }
            else
            {
                // 计算最大值用于拉伸坐标轴，和平均值
                for (int i = 0; i < 256; i++)
                {
                    count_array[channel, i] = (count_array[0, i] + count_array[1, i] + count_array[2, i]) / 3;
                    if (max < count_array[channel, i])
                        max = count_array[channel, i];
                }
            }

            // 根据最大值来拉伸直方图的高度，便于观察
            for (int i = 0; i < 256; i++)
            {
                int col = count_array[channel, i] * 190 / max;
                g.DrawLine(color_pen, 3 + i, histo_height - 3, 3 + i, histo_height - 3 - col);
            }
            return histoBmp;
        }

        static public Bitmap kirsch(Bitmap bit, double thresholding)
        {
           
            int width = bit.Width;
            int height = bit.Height;
            double gradX, gradY, grad;

            // 设定实例BitmapData相关信息
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageLockMode mode = ImageLockMode.ReadWrite;
            PixelFormat format = PixelFormat.Format32bppArgb;

            // 锁定bmp到系统内存中
            BitmapData data = bit.LockBits(rect, mode, format);

            // 获取位图中第一个像素数据的地址
            IntPtr ptr = data.Scan0;

            int numPixels = width * height;
            int numBytes = width * height * 4;
            byte[] rgbValues = new byte[numBytes];

            // 将bmp数据Copy到声明的数组中
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            byte[] grayValues = new byte[numPixels];
            double[] tempArray = new double[numPixels];


            for (int i = 0, j = 0; i < numBytes; i += 4, j++)
            {
                grayValues[j] = rgbValues[i];
            }


            // 处理函数
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grad = 0;

                    gradX = -5 * grayValues[((Math.Abs(i - 1)) % height) * width + ((Math.Abs(j - 1)) % width)] + 3 * grayValues[((Math.Abs(i - 1)) % height) * width + j] + 3 * grayValues[((Math.Abs(i - 1)) % height) * width + ((j + 1) % width)] - 5 * grayValues[i * width + ((Math.Abs(j - 1)) % width)] +
                        3 * grayValues[i * width + ((j + 1) % width)] - 5 * grayValues[((i + 1) % height) * width + ((Math.Abs(j - 1)) % width)] + 3 * grayValues[((i + 1) % height) * width + j] + 3 * grayValues[((i + 1) % height) * width + ((j + 1) % width)];
                    if (gradX > grad)
                        grad = gradX;

                    gradX = 3 * grayValues[((Math.Abs(i - 1)) % height) * width + ((Math.Abs(j - 1)) % width)] + 3 * grayValues[((Math.Abs(i - 1)) % height) * width + j] + 3 * grayValues[((Math.Abs(i - 1)) % height) * width + ((j + 1) % width)] - 5 * grayValues[i * width + ((Math.Abs(j - 1)) % width)] +
                        3 * grayValues[i * width + ((j + 1) % width)] - 5 * grayValues[((i + 1) % height) * width + ((Math.Abs(j - 1)) % width)] - 5 * grayValues[((i + 1) % height) * width + j] + 3 * grayValues[((i + 1) % height) * width + ((j + 1) % width)];
                    if (gradX > grad)
                        grad = gradX;

                    gradX = 3 * grayValues[((Math.Abs(i - 1)) % height) * width + ((Math.Abs(j - 1)) % width)] + 3 * grayValues[((Math.Abs(i - 1)) % height) * width + j] + 3 * grayValues[((Math.Abs(i - 1)) % height) * width + ((j + 1) % width)] + 3 * grayValues[i * width + ((Math.Abs(j - 1)) % width)] +
                        3 * grayValues[i * width + ((j + 1) % width)] - 5 * grayValues[((i + 1) % height) * width + ((Math.Abs(j - 1)) % width)] - 5 * grayValues[((i + 1) % height) * width + j] - 5 * grayValues[((i + 1) % height) * width + ((j + 1) % width)];
                    if (gradX > grad)
                        grad = gradX;

                    gradX = 3 * grayValues[((Math.Abs(i - 1)) % height) * width + ((Math.Abs(j - 1)) % width)] + 3 * grayValues[((Math.Abs(i - 1)) % height) * width + j] + 3 * grayValues[((Math.Abs(i - 1)) % height) * width + ((j + 1) % width)] + 3 * grayValues[i * width + ((Math.Abs(j - 1)) % width)] -
                        5 * grayValues[i * width + ((j + 1) % width)] + 3 * grayValues[((i + 1) % height) * width + ((Math.Abs(j - 1)) % width)] - 5 * grayValues[((i + 1) % height) * width + j] - 5 * grayValues[((i + 1) % height) * width + ((j + 1) % width)];
                    if (gradX > grad)
                        grad = gradX;

                    gradX = 3 * grayValues[((Math.Abs(i - 1)) % height) * width + ((Math.Abs(j - 1)) % width)] + 3 * grayValues[((Math.Abs(i - 1)) % height) * width + j] - 5 * grayValues[((Math.Abs(i - 1)) % height) * width + ((j + 1) % width)] + 3 * grayValues[i * width + ((Math.Abs(j - 1)) % width)] -
                        5 * grayValues[i * width + ((j + 1) % width)] + 3 * grayValues[((i + 1) % height) * width + ((Math.Abs(j - 1)) % width)] + 3 * grayValues[((i + 1) % height) * width + j] - 5 * grayValues[((i + 1) % height) * width + ((j + 1) % width)];
                    if (gradX > grad)
                        grad = gradX;

                    gradX = 3 * grayValues[((Math.Abs(i - 1)) % height) * width + ((Math.Abs(j - 1)) % width)] - 5 * grayValues[((Math.Abs(i - 1)) % height) * width + j] - 5 * grayValues[((Math.Abs(i - 1)) % height) * width + ((j + 1) % width)] + 3 * grayValues[i * width + ((Math.Abs(j - 1)) % width)] -
                        5 * grayValues[i * width + ((j + 1) % width)] + 3 * grayValues[((i + 1) % height) * width + ((Math.Abs(j - 1)) % width)] + 3 * grayValues[((i + 1) % height) * width + j] + 3 * grayValues[((i + 1) % height) * width + ((j + 1) % width)];
                    if (gradX > grad)
                        grad = gradX;

                    gradX = -5 * grayValues[((Math.Abs(i - 1)) % height) * width + ((Math.Abs(j - 1)) % width)] - 5 * grayValues[((Math.Abs(i - 1)) % height) * width + j] - 5 * grayValues[((Math.Abs(i - 1)) % height) * width + ((j + 1) % width)] + 3 * grayValues[i * width + ((Math.Abs(j - 1)) % width)] +
                        3 * grayValues[i * width + ((j + 1) % width)] + 3 * grayValues[((i + 1) % height) * width + ((Math.Abs(j - 1)) % width)] + 3 * grayValues[((i + 1) % height) * width + j] + 3 * grayValues[((i + 1) % height) * width + ((j + 1) % width)];
                    if (gradX > grad)
                        grad = gradX;

                    gradX = -5 * grayValues[((Math.Abs(i - 1)) % height) * width + ((Math.Abs(j - 1)) % width)] - 5 * grayValues[((Math.Abs(i - 1)) % height) * width + j] + 3 * grayValues[((Math.Abs(i - 1)) % height) * width + ((j + 1) % width)] - 5 * grayValues[i * width + ((Math.Abs(j - 1)) % width)] +
                        3 * grayValues[i * width + ((j + 1) % width)] + 3 * grayValues[((i + 1) % height) * width + ((Math.Abs(j - 1)) % width)] + 3 * grayValues[((i + 1) % height) * width + j] + 3 * grayValues[((i + 1) % height) * width + ((j + 1) % width)];
                    if (gradX > grad)
                        grad = gradX;

                    tempArray[i * width + j] = grad;
                }
            }


            if (thresholding == 0)//不进行阈值处理
            {
                for (int i = 0; i < numPixels; i++)
                {
                    if (tempArray[i] < 0)
                    {
                        rgbValues[i * 4] = 0;
                        rgbValues[i * 4 + 1] = 0;
                        rgbValues[i * 4 + 2] = 0;

                    }
                    else
                    {
                        if (tempArray[i] > 255)
                        {
                            rgbValues[i * 4] = 255;
                            rgbValues[i * 4 + 1] = 255;
                            rgbValues[i * 4 + 2] = 255;
                        }
                        else
                        {
                            rgbValues[i * 4] = Convert.ToByte(tempArray[i]);
                            rgbValues[i * 4 + 1] = Convert.ToByte(tempArray[i]);
                            rgbValues[i * 4 + 2] = Convert.ToByte(tempArray[i]);
                            
                        }
                    }
                }
            }
            else//阈值处理，生成二值边缘图像
            {

                for (int i = 0; i < numPixels; i++)
                {
                    if (tempArray[i] > thresholding)
                    {
                        rgbValues[i * 4] = 255;
                        rgbValues[i * 4 + 1] = 255;
                        rgbValues[i * 4 + 2] = 255;
                    }
                    else
                    {
                        rgbValues[i * 4] = 0;
                        rgbValues[i * 4 + 1] = 0;
                        rgbValues[i * 4 + 2] = 0;
                    }
                }
                
            }



            // 将数据Copy到内存指针
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            // 从系统内存解锁bmp
            bit.UnlockBits(data);
            return bit;

        }
        
    }
}