using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_kurs.GUI
{
    public partial class InfoRecord : Form
    {
        public int readerid;
        public InfoRecord(int readerid)
        {
            InitializeComponent();

            this.readerid = readerid;
            LoadBooks();
        }

        public void LoadBooks()
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = Database.DBClass.GetBooks(this.readerid);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main f = new();
            f.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                return;
            }

            var currentBook = dataGridView1.SelectedCells[0].OwningRow.DataBoundItem as DataClasses.Book;
            Database.DBClass.GiveOutBook(this.readerid, currentBook.id);
            LoadBooks();
        }
    }
}
