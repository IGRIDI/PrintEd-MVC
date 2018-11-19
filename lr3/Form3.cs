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
    public partial class Form3 : Form
    {
        public BindingList<Publication> Publications = new BindingList<Publication>();
        public DataGridView DataGridView { get { return dataGridView1; } }
        public Form3()
        {
            InitializeComponent();
            dataGridView1.DataSource = Publications;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Closing += (sender, e) =>
            {
                Form1 c = (Form1)MdiParent;
                c.ChildrenOpen--;
            };
        }

        public Form3(IEnumerable<Publication> publicationCollection) : this()
        {
            foreach (Publication tiger in publicationCollection)
            {
                Publications.Add(tiger);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DataGridView.RowCount; i++)
                if (DataGridView[0, i].FormattedValue.ToString().Contains(textBox1.Text.Trim()))
                {
                    try
                    {
                        DataGridView.CurrentCell = DataGridView[0, i];
                        return;
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка ввода", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
             }
        }
    }
}
