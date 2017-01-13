using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;
using System.IO;

namespace picshow
{
    public partial class Form1 : Form
    {
        private Image backup;
        private string parameter;
        private string parameter1;
        private Color parameter2;
        private Boolean valid;
        // 用来存放2的幂次方的表
        private static int[] array4two = new int[16];

        Image bufferpic;//加快GDI读取用缓存图片
        Point M_pot_p = new Point();//原始位置 
        int M_int_mx = 0, M_int_my = 0;//下次能继续 
        int M_int_maxX, M_int_maxY;//加快读取用
        

        public string Para
        {
            set 
            {
                parameter = value;
            }
        }
        public string Para1
        {
            set
            {
                parameter1 = value;
            }
        }

        public Color Para2
        {
            set
            {
                parameter2 = value;
            }
        }

        public Form1()
        {
            InitializeComponent();
            valid = true;
            //计算出2的幂次方，到2的15次为止
            array4two[0] = 1;
            for (int i = 1; i < 16; i++)
            {
                array4two[i] = array4two[i - 1] * 2;
            }
            panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel1_DragEnter);
            panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel1_DragDrop);
            /*
            pictureBox1.MouseUp += new MouseEventHandler(pictureBox1_MouseUp);
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MouseMove);
            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);
            pictureBox2.MouseUp += new MouseEventHandler(pictureBox2_MouseUp);
            pictureBox2.MouseDown += new MouseEventHandler(pictureBox2_MouseDown);
            pictureBox2.MouseMove += new MouseEventHandler(pictureBox2_MouseMove);
            pictureBox2.Paint += new PaintEventHandler(pictureBox2_Paint);
            
            //双缓存
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.Selectable, true);
            */
            richTextBox1.LoadFile("./History.rtf");
        }

        void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image == null)
                return;
            bufferpic = pictureBox1.Image;
            
            if (e.Button == MouseButtons.Left)//当按左键的时候 
            {
                //算差值 
                M_int_mx = M_int_mx - M_pot_p.X + e.X;
                M_int_my = M_int_my - M_pot_p.Y + e.Y;
                //锁定范围 
                M_int_mx = Math.Min(0, 1 + Math.Max(M_int_maxX, M_int_mx));
                M_int_my = Math.Min(0, 1 + Math.Max(M_int_maxY, M_int_my));
                pictureBox1.Invalidate();
                M_pot_p = e.Location;

            }
             

        }

        void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image == null)
                return;
            bufferpic = pictureBox1.Image;
            M_pot_p = e.Location;
            M_int_maxX = pictureBox1.Width - bufferpic.Width;
            M_int_maxY = pictureBox1.Height - bufferpic.Height;
            pictureBox1.Cursor = Cursors.Hand;
            
        }

        void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Cursor = Cursors.Default;
        }

        void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image == null)
                return;
            bufferpic = pictureBox1.Image;
            if (bufferpic != null)
            {
               
                
                e.Graphics.DrawImage(bufferpic,  e.ClipRectangle,
                    new Rectangle(-M_int_mx, -M_int_my, pictureBox1.Width, pictureBox1.Height),
                    GraphicsUnit.Pixel);
            }
        }

        void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox2.Image == null)
                return;
            bufferpic = pictureBox2.Image;
            if (e.Button == MouseButtons.Left)//当按左键的时候 
            {
                //算差值 
                M_int_mx = M_int_mx - M_pot_p.X + e.X;
                M_int_my = M_int_my - M_pot_p.Y + e.Y;
                //锁定范围 
                M_int_mx = Math.Min(0, Math.Max(M_int_maxX, M_int_mx));
                M_int_my = Math.Min(0, Math.Max(M_int_maxY, M_int_my));
                pictureBox2.Invalidate();
                M_pot_p = e.Location;
            }
        }

        void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBox2.Image == null)
                return;
            bufferpic = pictureBox2.Image;
            M_pot_p = e.Location;
            M_int_maxX = pictureBox2.Width - bufferpic.Width;
            M_int_maxY = pictureBox2.Height - bufferpic.Height;
            pictureBox2.Cursor = Cursors.Hand;

        }

        void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox2.Cursor = Cursors.Default;
        }

        void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox2.Image == null)
                return;
            bufferpic = pictureBox2.Image;
            if (bufferpic != null)
            {
                e.Graphics.DrawImage(bufferpic, e.ClipRectangle,
                    new Rectangle(-M_int_mx, -M_int_my, pictureBox2.Width, pictureBox2.Height),
                    GraphicsUnit.Pixel);
            }
        }


        // if the combo box of the gray level is changed, these method will be called
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkPictureBox1();
            Bitmap bit = new Bitmap(pictureBox1.Image, pictureBox1.Image.Width, pictureBox1.Image.Height);
            pictureBox2.Refresh();
            pictureBox2.Image =  GraphicClass.changeGrayLevel(Convert.ToInt32(comboBox1.Text), bit);
        }

        // if the combo box of the zoom-in-or-out is changed, these method will be called
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkPictureBox1();
            Bitmap bit = new Bitmap(pictureBox1.Image);
            pictureBox2.Refresh();
            pictureBox2.Image = GraphicClass.zoomPicture(Convert.ToInt32(comboBox2.Text), bit);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            //如果拖动的文件，设置拖放效果是链接方式（图片显示使用的是图片路径的链接）
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            //获取被拖动的文件名称数组（因为系统允许多文件拖动）
            string[] fileList = (string[])(e.Data.GetData(DataFormats.FileDrop));
            if (fileList.Length > 0)
            {
                //使用第一个文件的的（图片）文件名，包含全路径
                string fileName = fileList[0];
                try
                {
                    Image image = Image.FromFile(fileName);
                    pictureBox1.Image = image;
                    bufferpic = backup = pictureBox1.Image = image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("您拖放的不是可识别的图片格式！", "程序提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //程序记录错误日志
                }
            }
        }

        private void 顺时针90ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
                return;
            Image i = pictureBox1.Image;
            i.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox1.Image = i;
        }

        private void 镜面反转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
                return;
            Image i = pictureBox1.Image;
            i.RotateFlip(RotateFlipType.Rotate180FlipY);
            pictureBox1.Image = i;
        }

        

        

        public static void CreateFile(string path, string Html)
        {
            using (FileStream f = new FileStream(path, FileMode.OpenOrCreate))
            {
                StreamWriter sw = new StreamWriter(f);
                sw.WriteLine(Html);
                sw.WriteLine();
                sw.Close();
                f.Close();
            }
        }

        private void kirsch算子ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
                return;

            Bitmap bit = new Bitmap(pictureBox1.Image);
            Bitmap graybit = GraphicClass.convertRGB2Gray(bit);

            PassValueForm form = new PassValueForm();
            form.Owner = this;
            form.Description = "输入阙值";
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                pictureBox2.Refresh();
                double value1 = Convert.ToDouble(form.Value);

                pictureBox2.Image = GraphicClass.kirsch(graybit, value1);
            }

            
           
        }



                   
    }
}
