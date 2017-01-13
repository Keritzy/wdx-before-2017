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
        // check if there is picture in the picture box.
        // if not, change the valid to false so that skip any other operations
        private void checkPictureBox1()
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请先载入图片！");
                valid = false;
            }
            else
            {
                valid = true;
            }
        }


        // a slower method
        /*
        private void greymask(int[] a)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请先载入图片！");
                return;
            }
            Bitmap bit = new Bitmap(pictureBox1.Image);
            int w = bit.Width;
            int h = bit.Height;
            Bitmap bmpRtn = new Bitmap(w, h);
            progressBar1.Value = 0;
            progressBar1.Maximum = bit.Height;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (x == 0 || x == w - 1 || y == 0 || y == h - 1)
                    {
                        bmpRtn.SetPixel(x, y, bit.GetPixel(x, y));
                    }
                    else
                    {
                        int[] r = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        int[] g = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        int[] b = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        int[] gr = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        int vR = 0, vGR = 0;

                        int index = 0;
                        for (int i = y - 1; i <= y + 1; i++)
                        {
                            for (int j = x - 1; j <= x + 1; j++)
                            {
                                r[index] = bit.GetPixel(j, i).R;
                                g[index] = bit.GetPixel(j, i).G;
                                b[index] = bit.GetPixel(j, i).B;
                                gr[index] = (r[index] + g[index] + b[index]) / 3;
                                index++;
                            }

                        }

                        for (int i = 0; i < 9; i++)
                        {
                            //vG += a[i] * r[i];
                            //vR += a[i] * g[i];
                            //vB += a[i] * b[i];
                            vGR += a[i] * gr[i];
                        }

                        if (vGR > 0)
                        {
                            vGR = Math.Min(255, vR);
                        }
                        else
                        {
                            vGR = Math.Max(0, vR);
                        }

                        Color c = Color.FromArgb(vGR, vGR, vGR);
                        bmpRtn.SetPixel(x, y, c);

                    }
                }
                progressBar1.Value++;
            }
            progressBar1.Value = progressBar1.Maximum;
            pictureBox2.Refresh();
            pictureBox2.Image = bmpRtn;

        }
         */
    }
}
