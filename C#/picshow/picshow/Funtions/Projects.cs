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
        private void halftoningToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return; 
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.halftoning(GraphicClass.convertRGB2Gray(bit));
        }

        private void 生成测试图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            pictureBox1.Image = GraphicClass.generateTestPicture(1);
        }

        private void 直方图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return; 
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox3.Image = GraphicClass.drawHistogram(bit);
            pictureBox1.Refresh();
            pictureBox1.Image = GraphicClass.convertRGB2Gray(bit);
        }

        private void 直方图均衡化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return; 

            Bitmap original = new Bitmap(pictureBox1.Image);
            pictureBox3.Image = GraphicClass.drawHistogram(original);
            
            Bitmap result = GraphicClass.histogramEqualization(original);
            pictureBox1.Image = GraphicClass.convertRGB2Gray(original);

            pictureBox2.Image = result;         
            pictureBox4.Image = GraphicClass.drawHistogram(result);
        }

        private void 未标定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return; 

            float[] a = { 0, 1, 0, 1, -4, 1, 0, 1, 0 };
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.mask(a, bit); 
            
        }

        private void 标定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return; 

            float[] a = { 1, 1, 1, 1, -8, 1, 1, 1, 1 };
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.maskWithScaling(a, bit); 
        }

        private void 拉普拉斯四邻域ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return; 

            float[] a = { 0, -1, 0, -1, 5, -1, 0, -1, 0 };
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.mask(a, bit); 
        }

        private void 拉普拉斯八邻域ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return; 

            float[] a = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.mask(a, bit); 
        }

        private void 高提升滤波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return; 

            //float tmp = (float)(1.0 / 9.0);
            //float[] a = { tmp, tmp, tmp, tmp, tmp, tmp, tmp, tmp, tmp };
            float tmp = (float)(1.0 / 16.0);
            float[] a = { tmp, 2 * tmp, tmp, 2 * tmp, 4 * tmp, 2 * tmp, tmp, 2 * tmp, tmp };
            Bitmap bit = new Bitmap(pictureBox1.Image);
            Bitmap gmask = GraphicClass.subtractPicture(bit, GraphicClass.mask(a, bit));
            
            PassValueForm form = new PassValueForm();
            form.Owner = this;
            form.Description = "输入参数";
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                pictureBox2.Refresh();
                double ar = Convert.ToDouble(form.Value);
                pictureBox2.Image = GraphicClass.addPicture(bit, gmask, (int)ar);
            }
        }

        private void 高斯模糊ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return; 

            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.gaussian(bit, 3.0);
        }

        private void maskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return; 

            float tmp = (float)(1.0 / 16.0);
            float[] a = { tmp, 2 * tmp, tmp, 2 * tmp, 4 * tmp, 2 * tmp, tmp, 2 * tmp, tmp };
            Bitmap bit = new Bitmap(pictureBox1.Image);
            Bitmap gmask = GraphicClass.subtractPicture(bit, GraphicClass.mask(a, bit));
            pictureBox2.Image = gmask;
        }


        /* 利用双线性插值把图片缩放为2的幂次方
        * 寻找最近的值来进行缩放，表已经在初始化时计算好了
        */
        static public Bitmap scale2SquareSize(Bitmap bit)
        {
            // 找出长宽的大值，用于缩放
            int tempM = 0;
            // 存放目标分辨率大小
            int size = 0;
            int width = bit.Width;
            int height = bit.Height;
            if (width <= height)
            {
                tempM = height;
            }
            else
            {
                tempM = width;
            }

            for (int i = 0; i < 16; i++)
            {
                if (tempM == array4two[i])
                {
                    size = array4two[i];
                    break;
                }
                if (tempM > array4two[i])
                    continue;

                if (tempM - array4two[i - 1] > array4two[i] - tempM)
                {
                    size = array4two[i];
                    break;
                }
                else
                {
                    size = array4two[i - 1];
                    break;
                }
            }
            return GraphicClass.bilinearInterpolation(bit, size, size);
        }
        

        private void 高斯低通滤波10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;

            Bitmap bit = new Bitmap(backup);
            bit = scale2SquareSize(bit);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.ifft2d(GraphicClass.gaussianLowpassFilter(bit, 10.0), bit.Width, bit.Height, true);
            //pictureBox2.Image = GraphicClass.complex2Gray(GraphicClass.gaussianLowpassFilter(bit, 15.0), bit.Width, bit.Height);
        }

        private void 高斯低通滤波30ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;

            Bitmap bit = new Bitmap(pictureBox1.Image);
            bit = scale2SquareSize(bit);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.ifft2d(GraphicClass.gaussianLowpassFilter(bit, 30.0), bit.Width, bit.Height, true);
        }

        private void 高斯低通滤波60ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;

            Bitmap bit = new Bitmap(pictureBox1.Image);
            bit = scale2SquareSize(bit);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.ifft2d(GraphicClass.gaussianLowpassFilter(bit, 60.0), bit.Width, bit.Height, true);
        }


        private void 高斯低通滤波160ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;

            Bitmap bit = new Bitmap(pictureBox1.Image);
            bit = scale2SquareSize(bit);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.ifft2d(GraphicClass.gaussianLowpassFilter(bit, 160.0), bit.Width, bit.Height, true);
        }

        private void 高斯低通滤波360ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;

            Bitmap bit = new Bitmap(pictureBox1.Image);
            bit = scale2SquareSize(bit);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.ifft2d(GraphicClass.gaussianLowpassFilter(bit, 360.0), bit.Width, bit.Height, true);
        }

        private void 高斯高通滤波器30ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;

            Bitmap bit = new Bitmap(pictureBox1.Image);
            bit = scale2SquareSize(bit);
            pictureBox2.Refresh();

           //锐化 
          //pictureBox2.Image = GraphicClass.addPicture(GraphicClass.ifft2d(GraphicClass.gaussianHighpassFilter(bit, 30.0), bit.Width, bit.Height, true), bit, 1);
            pictureBox2.Image = GraphicClass.ifft2d(GraphicClass.gaussianHighpassFilter(bit, 30.0), bit.Width, bit.Height, true); 
        }

        private void 高斯高通滤波器60ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;

            Bitmap bit = new Bitmap(pictureBox1.Image);
            bit = scale2SquareSize(bit);
            pictureBox2.Refresh();

            //锐化
            //pictureBox2.Image = GraphicClass.addPicture(GraphicClass.ifft2d(GraphicClass.gaussianHighpassFilter(bit, 60.0), bit.Width, bit.Height, true), bit, 1);
            pictureBox2.Image = GraphicClass.ifft2d(GraphicClass.gaussianHighpassFilter(bit, 60.0), bit.Width, bit.Height, true); 
        }

        private void 高斯高通滤波器160ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;

            Bitmap bit = new Bitmap(pictureBox1.Image);
            bit = scale2SquareSize(bit);
            pictureBox2.Refresh();

           //锐化
           // pictureBox2.Image = GraphicClass.addPicture(GraphicClass.ifft2d(GraphicClass.gaussianHighpassFilter(bit, 160.0), bit.Width, bit.Height, true), bit, 1);
            pictureBox2.Image = GraphicClass.ifft2d(GraphicClass.gaussianHighpassFilter(bit, 160.0), bit.Width, bit.Height, true);  
        }

        private void 傅里叶平均值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;

            Bitmap bit = new Bitmap(pictureBox1.Image);
            bit = scale2SquareSize(bit);
            pictureBox2.Refresh();

            Bitmap graybit = GraphicClass.convertRGB2Gray(bit);
            int width = graybit.Width;
            int height = graybit.Height;

            label5.Text = width.ToString() + " × " + height.ToString();
            label6.Text = GraphicClass.aveValue(bit, GraphicClass.fft2d(bit, false)).ToString();

            pictureBox2.Image = GraphicClass.complex2Gray(GraphicClass.fft2d(bit, true), width, height);

            // 跳到对应信息显示区域
            tabControl1.SelectedIndex = 1;
        }

        private void 高斯噪声ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;

            Bitmap bit = new Bitmap(pictureBox1.Image);

            PassValueForm form = new PassValueForm();
            form.Owner = this;
            form.Description = "输入高斯噪声参数（均值，标准差）";
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                pictureBox2.Refresh();
                double value1 = Convert.ToDouble(form.Value);
                double value2 = Convert.ToDouble(form.Value1);
                //noise generator
                pictureBox2.Image = GraphicClass.noiseGenerator(bit, value1, value2, 0);
                pictureBox3.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox1.Image));
                pictureBox4.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox2.Image));
            }
        }

        private void 椒盐噪声ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;

            Bitmap bit = new Bitmap(pictureBox1.Image);

            PassValueForm form = new PassValueForm();
            form.Owner = this;
            form.Description = "输入椒盐噪声参数（含椒量，含盐量【相加不能超过1】）";
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                pictureBox2.Refresh();
                double value1 = Convert.ToDouble(form.Value);
                double value2 = Convert.ToDouble(form.Value1);
                //noise generator
                pictureBox2.Image = GraphicClass.noiseGenerator(bit, value1, value2, 1);
                pictureBox3.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox1.Image));
                pictureBox4.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox2.Image));
            }
        }

        private void rayleigh噪声ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;

            Bitmap bit = new Bitmap(pictureBox1.Image);

            PassValueForm form = new PassValueForm();
            form.Owner = this;
            form.Description = "输入Rayleigh噪声参数（参数a与参数b）";
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                pictureBox2.Refresh();
                double value1 = Convert.ToDouble(form.Value);
                double value2 = Convert.ToDouble(form.Value1);
                //noise generator
                pictureBox2.Image = GraphicClass.noiseGenerator(bit, value1, value2, 2);
                pictureBox3.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox1.Image));
                pictureBox4.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox2.Image));
            }
        }

        private void 指数噪声ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;

            Bitmap bit = new Bitmap(pictureBox1.Image);

            PassValueForm form = new PassValueForm();
            form.setTextBox2_unvisible();
            form.Owner = this;
            form.Description = "输入指数噪声参数（0.01~0.2之间最佳）";
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                pictureBox2.Refresh();
                double value1 = Convert.ToDouble(form.Value);
                double value2 = Convert.ToDouble(form.Value1);
                //noise generator
                pictureBox2.Image = GraphicClass.noiseGenerator(bit, value1, value2, 3);
                pictureBox3.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox1.Image));
                pictureBox4.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox2.Image));
            }
        }

        private void 中值滤波器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = GraphicClass.medianFiltering(bit);
            pictureBox3.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox1.Image));
            pictureBox4.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox2.Image));
        }

        private void 最大值滤波器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = GraphicClass.greatValueFiltering(bit, 0);
            pictureBox3.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox1.Image));
            pictureBox4.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox2.Image));
        }

        private void 最小值滤波器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = GraphicClass.greatValueFiltering(bit, 1);
            pictureBox3.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox1.Image));
            pictureBox4.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox2.Image));
        }

        private void 中点滤波器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Image = GraphicClass.greatValueFiltering(bit, 2);
            pictureBox3.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox1.Image));
            pictureBox4.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox2.Image));
        }

        private void 算术均值滤波器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;
            float temp = (float)(1.0 / 9.0);
            float[] a = { temp, temp, temp, temp, temp, temp, temp, temp, temp };
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.AvgFilter(bit, 0);
            pictureBox3.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox1.Image));
            pictureBox4.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox2.Image));
        }

        private void 几何均值滤波器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.AvgFilter(bit, 1);
            pictureBox3.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox1.Image));
            pictureBox4.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox2.Image));
        }

        private void 谐波均值滤波器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.AvgFilter(bit, 2);
            pictureBox3.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox1.Image));
            pictureBox4.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox2.Image));
        }

        private void 逆谐波均值滤波器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;

            PassValueForm form = new PassValueForm();
            form.setTextBox2_unvisible();
            form.Owner = this;
            form.Description = "输入逆谐波参数Q";
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                int parameter = Convert.ToInt32(form.Value);

                Bitmap bit = new Bitmap(pictureBox1.Image);
                pictureBox2.Refresh();
                pictureBox2.Image = GraphicClass.AvgFilter(bit, 3, parameter);

                pictureBox3.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox1.Image));
                pictureBox4.Image = GraphicClass.drawHistogram(new Bitmap(pictureBox2.Image));
            }
        }

        private void 伪彩色图像处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;
            //由用户输入参数
            PassValueForm1 form = new PassValueForm1();
            form.Owner = this;
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                double boundary = Convert.ToDouble(form.Value); //获取分界值
                int lowORhigh = Convert.ToInt32(form.Value1); //获取区间选项
                Color specified = form.Value2; //获取高显颜色
                Bitmap bit = new Bitmap(pictureBox1.Image);
                pictureBox2.Refresh();

                pictureBox2.Image = GraphicClass.PseudoColor(bit, boundary, specified, lowORhigh);
            }
        }

        private void rGB分别均衡化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;
            
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            // 根据RGB的三个分量分别均衡化
            pictureBox2.Image = GraphicClass.ColorEqualization(bit, 0);

            // 画出三个分量均衡化前后的直方图
            red_radio.Visible = true;
            green_radio.Visible = true;
            blue_radio.Visible = true;
            red_radio.Checked = true;
            pictureBox3.Image = GraphicClass.drawColorHistogram(bit, Color.Red);
            pictureBox4.Image = GraphicClass.drawColorHistogram((Bitmap)pictureBox2.Image, Color.Red);
        }

        private void rGB平均均衡化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;

            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            // 使用平均直方图进行均衡化
            pictureBox2.Image = GraphicClass.ColorEqualization(bit, 1);

            red_radio.Visible = false;
            green_radio.Visible = false;
            blue_radio.Visible = false;
            pictureBox3.Image = GraphicClass.drawColorHistogram(bit, Color.Red, true);
            pictureBox4.Image = GraphicClass.drawColorHistogram((Bitmap)pictureBox2.Image, Color.Red, true);
        }

        private void 红_CheckedChanged(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid || pictureBox2.Image == null)
                return;
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox3.Image = GraphicClass.drawColorHistogram(bit, Color.Red);
            pictureBox4.Image = GraphicClass.drawColorHistogram((Bitmap)pictureBox2.Image, Color.Red);
        }

        private void 绿_CheckedChanged(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid || pictureBox2.Image == null)
                return;
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox3.Image = GraphicClass.drawColorHistogram(bit, Color.Green);
            pictureBox4.Image = GraphicClass.drawColorHistogram((Bitmap)pictureBox2.Image, Color.Green);
        }

        private void 蓝_CheckedChanged(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid || pictureBox2.Image == null)
                return;
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox3.Image = GraphicClass.drawColorHistogram(bit, Color.Blue);
            pictureBox4.Image = GraphicClass.drawColorHistogram((Bitmap)pictureBox2.Image, Color.Blue);
        }
    }
              
}



