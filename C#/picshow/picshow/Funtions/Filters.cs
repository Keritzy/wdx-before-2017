using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace picshow
{
    public partial class Form1 : Form
    {
        private void 锐化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            float[] a = { 0, -1, 0, -1, 5, -1, 0, -1, 0 };
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.mask(a, bit);
        }

        private void 平滑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            float tmp = (float)(1.0 / 16.0);
            float[] a = { tmp, 2 * tmp, tmp, 2 * tmp, 4 * tmp, 2 * tmp, tmp, 2 * tmp, tmp };
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.mask(a, bit);
        }


        private void 反色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            Bitmap bit = new Bitmap(pictureBox1.Image, pictureBox1.Image.Width, pictureBox1.Image.Height);
            Color c = new Color();
            Color c1 = new Color();

            for (int x = 0; x < bit.Width; x++)
            {
                for (int y = 0; y < bit.Height; y++)
                {
                    c = bit.GetPixel(x, y);
                    c1 = Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B);
                    bit.SetPixel(x, y, c1);
                }
            }
            pictureBox2.Refresh();
            pictureBox2.Image = bit;
        }

        

        private void 镶嵌ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请先载入图片！");
                return;
            }
            Color c = new Color();
            Color cc = new Color();
            Bitmap b = new Bitmap(pictureBox1.Image);
            Bitmap b1 = new Bitmap(b.Width, b.Height);
            int rr, gg, bb, k1, k2;

            progressBar1.Value = 0;
            progressBar1.Maximum = b.Width;
            for (int i = 0; i < b.Width - 3; i += 3)
            {
                for (int j = 0; j < b.Height - 4; j += 3)
                {
                    rr = 0; bb = 0; gg = 0;
                    for (k1 = 0; k1 <= 3; k1++)
                    {
                        for (k2 = 0; k2 <= 3; k2++)
                        {
                            c = b.GetPixel(i + k1, j + k2);
                            rr += c.R;
                            gg += c.G;
                            bb += c.B;
                        }
                    }
                    rr = rr / 25;
                    gg = gg / 25;
                    bb = bb / 25;
                    if (rr < 0) rr = 0;
                    if (rr > 255) rr = 255;
                    if (gg < 0) gg = 0;
                    if (gg > 255) gg = 255;
                    if (bb < 0) bb = 0;
                    if (bb > 255) bb = 255;
                    for (k1 = 0; k1 <= 3; k1++)
                    {
                        for (k2 = 0; k2 <= 3; k2++)
                        {
                            cc = Color.FromArgb(rr, gg, bb);
                            b1.SetPixel(i + k1, j + k2, cc);
                        }
                    }
                }
                progressBar1.Value++;
            }
            progressBar1.Value = progressBar1.Maximum;
            pictureBox2.Refresh();
            pictureBox2.Image = b1;
        }

        private void 浮雕ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请先载入图片！");
                return;
            }
            Color c = new Color();
            Color cc = new Color();
            Bitmap b = new Bitmap(pictureBox1.Image);
            Bitmap b1 = new Bitmap(b.Width, b.Height);
            int rr, gg, bb;

            progressBar1.Value = 0;
            progressBar1.Maximum = b.Width;
            for (int i = 1; i < b.Width - 1; i++)
            {
                for (int j = 1; j < b.Height - 1; j++)
                {

                    c = b.GetPixel(i, j);
                    cc = b.GetPixel(i - 1, j - 1);
                    rr = Math.Abs(cc.R - c.R + 128);
                    gg = cc.G - c.G + 128;
                    bb = cc.B - c.B + 128;
                    if (rr < 0) rr = 0;
                    if (rr > 255) rr = 255;
                    if (gg < 0) gg = 0;
                    if (gg > 255) gg = 255;
                    if (bb < 0) bb = 0;
                    if (bb > 255) bb = 255;
                    Color cS = Color.FromArgb(rr, gg, bb);
                    b1.SetPixel(i, j, cS);
                }
                progressBar1.Value++;
            }
            progressBar1.Value = progressBar1.Maximum;
            pictureBox2.Refresh();
            pictureBox2.Image = b1;
        }
 

        private void 霓虹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请先载入图片！");
                return;
            }
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Bitmap newbmp = new Bitmap(pictureBox1.Image);
            Color c1 = new Color();
            Color NewC = new Color();
            int r1, r2, r3, g1, g2, g3, b1, b2, b3, rr, gg, bb;


            progressBar1.Value = 0;
            progressBar1.Maximum = bmp.Width;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    c1 = bmp.GetPixel(i, j);
                    r1 = c1.R;
                    g1 = c1.G;
                    b1 = c1.B;
                    if (i + 1 == bmp.Width)
                    {
                        c1 = bmp.GetPixel(i, j);
                    }
                    else
                    {
                        c1 = bmp.GetPixel(i + 1, j);
                    }
                    r2 = c1.R;
                    g2 = c1.G;
                    b2 = c1.B;
                    if (j + 1 == bmp.Height)
                    {
                        c1 = bmp.GetPixel(i, j);
                    }
                    else
                    {
                        c1 = bmp.GetPixel(i, j + 1);
                    }
                    r3 = c1.R;
                    g3 = c1.G;
                    b3 = c1.B;
                    rr = (int)(255 - 2 * Math.Sqrt((r1 - r2) * (r1 - r2) + (r1 - r3) * (r1 - r3)));
                    gg = (int)(255 - 2 * Math.Sqrt((g1 - g2) * (g1 - g2) + (g1 - g3) * (g1 - g3)));
                    bb = (int)(255 - 2 * Math.Sqrt((b1 - b2) * (b1 - b2) + (b1 - b3) * (b1 - b3)));
                    NewC = Color.FromArgb((Byte)rr, (Byte)gg, (Byte)bb);
                    newbmp.SetPixel(i, j, NewC);
                }
                progressBar1.Value++;
            }
            progressBar1.Value = progressBar1.Maximum;
            pictureBox2.Refresh();
            pictureBox2.Image = newbmp;
        }

        private void 怀旧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请先载入图片！");
                return;
            }
            Color c = new Color();
            Bitmap b = new Bitmap(pictureBox1.Image);
            Bitmap b1 = new Bitmap(b.Width, b.Height);
            int r, g, bl;

            progressBar1.Value = 0;
            progressBar1.Maximum = b.Width;
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    c = b.GetPixel(i, j);
                    r = (int)(0.393 * c.R + 0.769 * c.G + 0.189 * c.B);
                    g = (int)(0.349 * c.R + 0.686 * c.G + 0.168 * c.B);
                    bl = (int)(0.272 * c.R + 0.534 * c.G + 0.131 * c.B);
                    if (r > 255)
                        r = 255;
                    if (g > 255)
                        g = 255;
                    if (bl > 255)
                        bl = 255;
                    Color c1 = Color.FromArgb(r, g, bl);
                    b1.SetPixel(i, j, c1);
                }
                progressBar1.Value++;
            }
            progressBar1.Value = progressBar1.Maximum;
            pictureBox2.Refresh();
            pictureBox2.Image = b1;
        }

        private void 卡通ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请先载入图片！");
                return;
            }
            Color c = new Color();
            Bitmap b = new Bitmap(pictureBox1.Image);
            Bitmap b1 = new Bitmap(b.Width, b.Height);
            int r, g, bl;

            progressBar1.Value = 0;
            progressBar1.Maximum = b.Width;
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    c = b.GetPixel(i, j);
                    r = (int)(Math.Abs(c.G - c.B + c.G + c.R) * c.R / 256);
                    g = (int)(Math.Abs(c.B - c.G + c.B + c.R) * c.R / 256);
                    bl = (int)(Math.Abs(c.B - c.G + c.B + c.R) * c.G / 256);
                    if (r > 255)
                        r = 255;
                    if (g > 255)
                        g = 255;
                    if (bl > 255)
                        bl = 255;
                    double grey = 0.3 * r + 0.59 * g + 0.11 * bl;
                    Color c1 = Color.FromArgb((int)grey, (int)grey, (int)grey);
                    b1.SetPixel(i, j, c1);
                }
                progressBar1.Value++;
            }
            progressBar1.Value = progressBar1.Maximum;
            pictureBox2.Refresh();
            pictureBox2.Image = b1;
        }

        private void 油画ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请先载入图片！");
                return;
            }
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            int width = bmp.Width;
            int height = bmp.Height;

            Color color = new Color();
            Random rand = new Random();

            progressBar1.Value = 0;
            progressBar1.Maximum = bmp.Width;
            for (int i = 0; i < width - 5; i++)
            {
                for (int j = 0; j < height - 5; j++)
                {
                    int a = rand.Next(5);
                    color = bmp.GetPixel(i + a, j + a);
                    bmp.SetPixel(i, j, color);
                }
                progressBar1.Value++;
            }
            progressBar1.Value = progressBar1.Maximum;
            pictureBox2.Refresh();
            pictureBox2.Image = bmp;
        }

        unsafe public int GetThreshValue(Bitmap image)
        {
            BitmapData bd = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
            byte* pt = (byte*)bd.Scan0;
            int[] pixelNum = new int[256];           //图象直方图，共256个点
            byte color;
            byte* pline;
            int n, n1, n2;
            int total;                              //total为总和，累计值
            double m1, m2, sum, csum, fmax, sb;     //sb为类间方差，fmax存储最大方差值
            int k, t, q;
            int threshValue = 1;                      // 阈值
            int step = 1;
            switch (image.PixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    step = 3;
                    break;
                case PixelFormat.Format32bppArgb:
                    step = 4;
                    break;
                case PixelFormat.Format8bppIndexed:
                    step = 1;
                    break;
            }
            //生成直方图
            for (int i = 0; i < image.Height; i++)
            {
                pline = pt + i * bd.Stride;
                for (int j = 0; j < image.Width; j++)
                {
                    color = *(pline + j * step);   //返回各个点的颜色，以RGB表示
                    pixelNum[color]++;            //相应的直方图加1
                }
            }
            //直方图平滑化
            for (k = 0; k <= 255; k++)
            {
                total = 0;
                for (t = -2; t <= 2; t++)              //与附近2个灰度做平滑化，t值应取较小的值
                {
                    q = k + t;
                    if (q < 0)                     //越界处理
                        q = 0;
                    if (q > 255)
                        q = 255;
                    total = total + pixelNum[q];    //total为总和，累计值
                }
                pixelNum[k] = (int)((float)total / 5.0 + 0.5);    //平滑化，左边2个+中间1个+右边2个灰度，共5个，所以总和除以5，后面加0.5是用修正值
            }
            //求阈值
            sum = csum = 0.0;
            n = 0;
            //计算总的图象的点数和质量矩，为后面的计算做准备
            for (k = 0; k <= 255; k++)
            {
                sum += (double)k * (double)pixelNum[k];     //x*f(x)质量矩，也就是每个灰度的值乘以其点数（归一化后为概率），sum为其总和
                n += pixelNum[k];                       //n为图象总的点数，归一化后就是累积概率
            }

            fmax = -1.0;                          //类间方差sb不可能为负，所以fmax初始值为-1不影响计算的进行
            n1 = 0;
            for (k = 0; k < 255; k++)                  //对每个灰度（从0到255）计算一次分割后的类间方差sb
            {
                n1 += pixelNum[k];                //n1为在当前阈值遍前景图象的点数
                if (n1 == 0) { continue; }            //没有分出前景后景
                n2 = n - n1;                        //n2为背景图象的点数
                if (n2 == 0) { break; }               //n2为0表示全部都是后景图象，与n1=0情况类似，之后的遍历不可能使前景点数增加，所以此时可以退出循环
                csum += (double)k * pixelNum[k];    //前景的“灰度的值*其点数”的总和
                m1 = csum / n1;                     //m1为前景的平均灰度
                m2 = (sum - csum) / n2;               //m2为背景的平均灰度
                sb = (double)n1 * (double)n2 * (m1 - m2) * (m1 - m2);   //sb为类间方差
                if (sb > fmax)                  //如果算出的类间方差大于前一次算出的类间方差
                {
                    fmax = sb;                    //fmax始终为最大类间方差（otsu）
                    threshValue = k;              //取最大类间方差时对应的灰度的k就是最佳阈值
                }
            }
            image.UnlockBits(bd);
            return threshValue;
        }


        private void 素描ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请先载入图片！");
                return;
            }

            Bitmap bit = new Bitmap(pictureBox1.Image);
            int w = bit.Width;
            int h = bit.Height;

            Bitmap bmpRtn = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            Bitmap grayp = new Bitmap(w, h, PixelFormat.Format24bppRgb);

            BitmapData srcData = bit.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dstData = bmpRtn.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            BitmapData gryData = grayp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            progressBar1.Value = 0;
            progressBar1.Maximum = bit.Height;

            unsafe
            {
                byte* pIn = (byte*)srcData.Scan0.ToPointer();
                byte* pOut = (byte*)dstData.Scan0.ToPointer();
                byte* pgry = (byte*)gryData.Scan0.ToPointer();

                int stride = srcData.Stride;
                byte* p;

                // gray the picture first
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        pgry[0] = pgry[1] = pgry[2] = (byte)(0.3 * pIn[0] + 0.59 * pIn[1] + 0.11 * pIn[3]);
                        pIn += 3;
                        pgry += 3;
                    }// end of x
                    pIn += srcData.Stride - w * 3;
                    pgry += srcData.Stride - w * 3;
                } // end of y

                grayp.UnlockBits(gryData);
                float gap = GetThreshValue(grayp) - 35;
                gryData = grayp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                // reset the pointer
                pOut = (byte*)dstData.Scan0.ToPointer();
                pgry = (byte*)gryData.Scan0.ToPointer();

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {

                        //取周围9点的值。位于边缘上的点不做改变。
                        if (x == 0 || x == w - 1 || y == 0 || y == h - 1)
                        {
                            //不做
                            pOut[0] = 255;
                            pOut[1] = 255;
                            pOut[2] = 255;
                        }
                        else
                        {
                            int r1, r2, r3, r4, r5, r6, r7, r8, r0; // for gray image, one red value is enough

                            float diff;

                            //左上
                            p = pgry - stride - 3;
                            r1 = p[2];

                            //正上
                            p = pgry - stride;
                            r2 = p[2];

                            //右上
                            p = pgry - stride + 3;
                            r3 = p[2];

                            //左侧
                            p = pgry - 3;
                            r4 = p[2];

                            //右侧
                            p = pgry + 3;
                            r5 = p[2];

                            //左下
                            p = pgry + stride - 3;
                            r6 = p[2];

                            //正下
                            p = pgry + stride;
                            r7 = p[2];

                            //右下
                            p = pgry + stride + 3;
                            r8 = p[2];

                            //自己
                            p = pgry;
                            r0 = p[2];

                            diff = Math.Abs((float)r0 - (float)(r1 + r2 + r3 + r4 + r5 + r6 + r7 + r8) / 8);

                            diff = diff + Math.Abs(r0 - r6) + Math.Abs(r0 - r3) + Math.Abs(r0 - r1) + Math.Abs(r0 - r8) + Math.Abs(r0 - r2) + Math.Abs(r0 - r4) + Math.Abs(r0 - r5) + Math.Abs(r0 - r7);

                            if (diff > gap)
                            {
                                diff = 0;
                            }
                            else
                            {
                                diff = 255;
                            }

                            pOut[0] = (byte)diff;
                            pOut[1] = (byte)diff;
                            pOut[2] = (byte)diff;

                        }

                        pOut += 3;
                        pgry += 3;
                    }// end of x
                    progressBar1.Value++;
                    pOut += srcData.Stride - w * 3;
                    pgry += srcData.Stride - w * 3;
                } // end of y
            }

            bit.UnlockBits(srcData);
            bmpRtn.UnlockBits(dstData);
            grayp.UnlockBits(gryData);

            progressBar1.Value = progressBar1.Maximum;
            pictureBox2.Refresh();
            pictureBox2.Image = bmpRtn;
            //pictureBox2.Image = grayp;
        }

        private void halftoningToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 对数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请先载入图片！");
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入参数！");
                return;
            }

            double ar = Convert.ToDouble(textBox1.Text);
            Bitmap bit = new Bitmap(pictureBox1.Image, pictureBox1.Image.Width, pictureBox1.Image.Height);
            Bitmap bmp = new Bitmap(bit.Width, bit.Height);
            Color c = new Color();
            Color c1 = new Color();

            int rb, gb, bb;
            progressBar1.Value = 0;
            progressBar1.Maximum = bit.Width;
            for (int x = 0; x < bit.Width; x++)
            {
                for (int y = 0; y < bit.Height; y++)
                {
                    c = bit.GetPixel(x, y);
                    rb = (int)(ar * Math.Log(1 + c.R));
                    gb = (int)(ar * Math.Log(1 + c.G));
                    bb = (int)(ar * Math.Log(1 + c.B));

                    if (rb > 255)
                        rb = 255;
                    if (gb > 255)
                        gb = 255;
                    if (bb > 255)
                        bb = 255;
                    c1 = Color.FromArgb(rb, gb, bb);
                    bmp.SetPixel(x, y, c1);
                }
                progressBar1.Value++;
            }
            progressBar1.Value = progressBar1.Maximum;
            pictureBox2.Refresh();
            pictureBox2.Image = bmp;
        }

        private void 伽马ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请先载入图片！");
                return;
            }
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("请输入参数！");
                return;
            }

            double ar = Convert.ToDouble(textBox1.Text);
            double rr = Convert.ToDouble(textBox2.Text);
            Bitmap bit = new Bitmap(pictureBox1.Image, pictureBox1.Image.Width, pictureBox1.Image.Height);
            Bitmap bmp = new Bitmap(bit.Width, bit.Height);
            Color c = new Color();
            Color c1 = new Color();

            int rb, gb, bb;
            progressBar1.Value = 0;
            progressBar1.Maximum = bit.Width;
            for (int x = 0; x < bit.Width; x++)
            {
                for (int y = 0; y < bit.Height; y++)
                {
                    c = bit.GetPixel(x, y);
                    rb = (int)(ar * Math.Pow(c.R, rr));
                    gb = (int)(ar * Math.Pow(c.G, rr));
                    bb = (int)(ar * Math.Pow(c.B, rr));

                    if (rb > 255)
                        rb = 255;
                    if (gb > 255)
                        gb = 255;
                    if (bb > 255)
                        bb = 255;
                    c1 = Color.FromArgb(rb, gb, bb);
                    bmp.SetPixel(x, y, c1);
                }
                progressBar1.Value++;
            }
            progressBar1.Value = progressBar1.Maximum;
            pictureBox2.Refresh();
            pictureBox2.Image = bmp;
        }
    }

}
