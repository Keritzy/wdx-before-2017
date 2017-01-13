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
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show the Open File dialog. If the user clicks OK, load
            // the picture that the user chose.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                backup =  pictureBox1.Image;
                //bufferpic = pictureBox1.Image
                
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox2.Image == null)
                    MessageBox.Show("请先选择效果再进行保存");
                else
                    pictureBox2.Image.Save(saveFileDialog1.FileName);
            }
        }

        private void 保存为左图toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox2.Image == null)
                    MessageBox.Show("请先选择效果再进行保存");
                else
                {
                    pictureBox2.Image.Save(saveFileDialog1.FileName);
                    pictureBox1.Image = pictureBox2.Image;
                    pictureBox2.Image = pictureBox3.Image = pictureBox4.Image = null;
                }
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Close the form.
            this.Close();
        }
    }

}
