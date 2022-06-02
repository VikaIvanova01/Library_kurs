using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library_kurs.DataClasses;
using Library_kurs.Database;

namespace Library_kurs.GUI
{
    public partial class ReportSent : Form
    {
        private List<DataClasses.Message> mess = new();
        public ReportSent()
        {
            InitializeComponent();
            dateTimePicker1.Text = "01/01/2020 12:00:00 AM";
            LoadReport();
        }

        private void LoadReport()
        {
            dataGridView1.AutoGenerateColumns = true;
            var list = Database.DBClass.GetMessageInfo();
            var bindindList = new BindingList<DataClasses.MessageInfo>(list);
            dataGridView1.DataSource = bindindList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
