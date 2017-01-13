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
        private void 设置背景色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show the color dialog box. If the user clicks OK, change the
            // PictureBox control's background to the color the user chose.
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog1.Color;
                pictureBox2.BackColor = colorDialog1.Color;
            }  
        }

        private void bilinerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;

            Bitmap bit = new Bitmap(pictureBox1.Image);

            PassValueForm form = new PassValueForm();
            form.Owner = this;
            form.Description = "输入要变化成的分辨率， 上面为宽，下面为长";
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                pictureBox2.Refresh();
                int width = Convert.ToInt32(form.Value);
                int height = Convert.ToInt32(form.Value1);
                pictureBox2.Image = GraphicClass.bilinearInterpolation(bit, width, height);
            }
        }

        private void 图片平均ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Files (*.jpg)|*.jpg|BMP Files (*.bmp)|*.bmp|PNG Files (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String[] paths = openFileDialog.FileNames;//获取所选文件路径
                pictureBox1.Load(paths[0]);
                pictureBox2.Image = pictureBox1.Image;
                for (int i = 1; i < paths.Length; i++)
                {
                    //Thread.Sleep(500);
                    Bitmap bit2 = new Bitmap(paths[i]);
                    pictureBox2.Image = bit2;
                    Bitmap bit1 = new Bitmap(pictureBox1.Image);
                    pictureBox1.Image = GraphicClass.AvgPicture(bit1, bit2, i);

                }



            }
        }

        private void 字符画ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return;
            pictureBox2.Refresh();
            StringBuilder sb = new StringBuilder();

            sb.Length = 0;
            char[] asciiChars = { 'M', 'N', 'B', 'D', 'c', 'h', 'z', 's', '=', '+', 't', 'l', '(', '~', '-', 'e', '.', ' ' };
            Bitmap bit = new Bitmap(pictureBox1.Image);

            for (int H = 0; H < pictureBox1.Image.Height; H++)
            {
                for (int W = 0; W < pictureBox1.Image.Width; W++)
                {
                    Color pixelColor = bit.GetPixel(W, H);
                    //算出颜色的灰度
                    int grey = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    Color grayColor = Color.FromArgb(grey, grey, grey);
                    int index = (grayColor.R * (asciiChars.Length - 1)) / 255;
                    sb.Append(asciiChars[index]);

                }
                sb.Append("\r\n");
            }

            string Path = "\\temp.html";
            StringBuilder outHtml = new StringBuilder();
            outHtml.Append("<html>");
            outHtml.Append("<head>");
            outHtml.Append("<title>字符画效果</title>");
            outHtml.Append("<style type=\"text/css\">");
            outHtml.Append(".asciichar { line-height: 4px; font-family: \"Console\"; font-size: 8px; color: #666666}");
            outHtml.Append("</style>");
            outHtml.Append("</head>");
            outHtml.Append("<body leftmargin=\"0\", topmargin=\"0\", marginwidth=\"0\", marginheight=\"0\">");
            outHtml.Append("<table width=\"100%\" height=\"100%\">");
            outHtml.Append("<tr><td align=\"center\" valign=\"middle\">");
            outHtml.Append("<p><pre>");
            outHtml.Append("<font class=\"asciichar\">");
            outHtml.Append(sb.ToString());
            outHtml.Append("</font>");
            outHtml.Append("</pre></p>");
            outHtml.Append("</td></tr>");
            outHtml.Append("</table>");
            outHtml.Append("</body>");
            outHtml.Append("</html>");

            CreateFile(Path, outHtml.ToString());

            MessageBox.Show("已经成功生成文件！" + Path);
            webBrowser1.Navigate(Path);
            tabControl1.SelectedIndex = 3;
        }
    }
              
}
