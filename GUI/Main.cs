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
    public partial class Main : Form
    {
        private List<DataClasses.Record> apps = new();
        public Main()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;

            var readers = Database.DBClass.GetReaders();


            cmbReaders.DataSource = readers;
            cmbReaders.DisplayMember = "FIO";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReportSent a = new();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var currentReader = cmbReaders.SelectedValue as DataClasses.Reader;
            InfoRecord form = new(currentReader.id);
            form.ShowDialog();
            LoadCurrentReaderBooks();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void LoadCurrentReaderBooks()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            if (cmbReaders.SelectedValue != null)
            {
                var list = Database.DBClass.GetBooksInfo((cmbReaders.SelectedValue as DataClasses.Reader).id);
                var bindindList = new BindingList<DataClasses.BookInfoStruct>(list);
                dataGridView1.DataSource = bindindList;
            }
        }

        private void cmbReaders_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadCurrentReaderBooks();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                return;
            }

            var currentReader = cmbReaders.SelectedValue as DataClasses.Reader;
            var currentBookInfo = dataGridView1.SelectedCells[0].OwningRow.DataBoundItem as DataClasses.BookInfoStruct;
            Database.DBClass.ReturnBook(currentReader.id, currentBookInfo.idBook);
            LoadCurrentReaderBooks();
        }
    }
}
