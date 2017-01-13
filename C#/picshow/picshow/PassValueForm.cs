using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace picshow
{
    public partial class PassValueForm : Form
    {
        private string description;
        private string parameter;
        private string parameter1;
        public string Value
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
        public string Value1
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

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        
        public PassValueForm()
        {
            InitializeComponent();
            
        }

        private void PassValueForm_Load(object sender, EventArgs e)
        {
            label1.Text = description;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = (Form1)this.Owner;
            form.Para = parameter;
            form.Para1 = parameter1;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.parameter = textBox1.Text;
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.parameter1 = textBox2.Text;
        }
        public void setTextBox2_unvisible()
        {
            textBox2.Visible = false;
        }


    }
}
