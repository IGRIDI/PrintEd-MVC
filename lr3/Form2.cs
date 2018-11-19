using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lr3
{
     public partial class Form2 : Form
    {
        public Publication Result;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Publication p)
        {
                InitializeComponent();
                button1.Text = "Сохранить";
                textBox1.Text = p.Name; 
                textBox2.Text = p.Cost.ToString(); ;
                textBox3.Text = p.Author;
                textBox4.Text = p.Purchases.ToString();
                DialogResult = DialogResult.OK;
        }

       private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text;
                int cost = Convert.ToInt16(textBox2.Text);
                string author = textBox3.Text;
                int purchases = Convert.ToInt16(textBox4.Text);

                 //data = DateTime.Now;

                Result = new Publication(textBox1.Text, Convert.ToInt16(textBox2.Text), textBox3.Text, Convert.ToInt16(textBox4.Text), DateTime.Now);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                MessageBox.Show("Данные введены неверно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.BMP; *.JPG; *.GIF; *.PNG)|*.BMP;*.JPG; *.GIF; *.PNG|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
