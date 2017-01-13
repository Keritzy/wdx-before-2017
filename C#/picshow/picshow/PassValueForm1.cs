using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace picshow
{
    public partial class PassValueForm1 : Form
    {       
        private String parameter;
        private String parameter1;
        private Color parameter2;
        public String Value
        {
            get
            {
                return parameter;
            }
            set
            {
                parameter = value;
            }
        }
        public String Value1
        {
            get
            {
                return parameter1;
            }
            set
            {
                parameter1 = value;
            }
        }
        public Color Value2
        {
            get
            {
                return parameter2;
            }
            set
            {
                parameter2 = value;
            }
        }

       
        public PassValueForm1()
        {
            InitializeComponent();
        }

        private void PassValueForm1_Load(object sender, EventArgs e)
        {
            this.parameter = Convert.ToString(50);
            this.parameter1 = Convert.ToString(0);
            this.parameter2 = Color.Yellow;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.parameter =textBox1.Text;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.parameter1 = Convert.ToString(0);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            this.parameter1 = Convert.ToString(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = (Form1)this.Owner;
            form.Para = parameter;
            form.Para1 = parameter1;
            form.Para2 = parameter2;
            this.Close();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.parameter2 = Color.Yellow;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            this.parameter2 = Color.Magenta;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            this.parameter2 = Color.Cyan;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            this.parameter2 = Color.Blue;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            this.parameter2 = Color.Green;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            this.parameter2 = Color.Red;
        }
    }
}
