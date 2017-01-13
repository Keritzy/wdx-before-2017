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
        private void 红ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return; 

            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.getRedChannel(bit);
        }

        private void 绿ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return; 

            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.getGreenChannel(bit);
        }

        private void 蓝ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPictureBox1();
            if (!valid)
                return; 

            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.getBlueChannel(bit);
        }

        
    }

}
