using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace lr3
{
    public partial class Form1 : Form
    {
        public int ChildrenOpen { get; set; }
        public Form1()
        {
            ChildrenOpen = 0;
            InitializeComponent();
          //  Text = "Publication";
            IsMdiContainer = true;
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void открытьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Publication[] tigers;
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    //FileStream fs = File.Open(ofd.FileName, FileMode.OpenOrCreate);

                    FileStream fs = new FileStream(ofd.FileName, FileMode.OpenOrCreate); //создаем файловый поток
                    StreamWriter writer = new StreamWriter(fs); //создаем «потоковый писат
                   
                    tigers = (Publication[])formatter.Deserialize(fs);
                    foreach (Publication p in tigers)
                    {
                        p.Data = DateTime.Now;
                        DateTime data = DateTime.Now;
                    }
                    writer.Close();


                }
                catch (IOException)
                {
                    return;
                }
                catch (NullReferenceException)
                {
                    return;
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Неверный тип файла", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Form3 table = new Form3(tigers);
                ChildrenOpen++;
                table.MdiParent = this;
                table.Text = "Перечень публикаций № " + ChildrenOpen;
                table.Show();
            }

        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null)
            {
                MessageBox.Show("Не открыто ни одной таблицы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            Form3 table = (Form3)ActiveMdiChild;
            sfd.Filter = "dat-файл(*.dat)|*.dat";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                BinaryFormatter formatter = new BinaryFormatter();
              //  FileStream fs = File.Open(sfd.FileName, FileMode.Create);
               
               FileStream fs = new FileStream(sfd.FileName, FileMode.Create); //создаем файловый поток
                StreamWriter writer = new StreamWriter(fs); //создаем «потоковый писатель» и связываем его с файловым потоком 
              //  writer.Write("текст"); //записываем в файл
              //  writer.Close(); //закрываем поток. Не закрыв поток, в файл ничего не запишется 


                formatter.Serialize(fs, table.Publications.ToArray());
                fs.Close();
            }
        }

        private void добавитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK && form.Result != null)
            {

                if (ActiveMdiChild == null)
                {
                    открытьToolStripMenuItem_Click_1(sender, e);
                }

                var table = (Form3)ActiveMdiChild;
                table?.Publications.Add(form.Result);
            }
        }

        private void изменитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null)
            {
                MessageBox.Show("Не открыто ни одной формы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Form3 table = (Form3)ActiveMdiChild;
           Publication t = (Publication)table.DataGridView.CurrentRow?.DataBoundItem;
            if (t != null)
            {
                int index = table.Publications.IndexOf(t);
                using (Form2 editForm = new Form2(t))
                {
                    var result = editForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        table.Publications[index] = editForm.Result;
                    }
                }
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null)
            {
                MessageBox.Show("Не открыто ни одной формы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Form3 table = (Form3)ActiveMdiChild;
            var currentRow = table.DataGridView.CurrentRow;
           Publication t = (Publication)currentRow?.DataBoundItem;
            if (t != null)
            {
                table.Publications.Remove(t);
            }
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 table = new Form3();
            ChildrenOpen++;
            table.MdiParent = this;
            table.Text = "Перечень публиаций № " + ChildrenOpen;
            table.Show();
        }
    }
}
