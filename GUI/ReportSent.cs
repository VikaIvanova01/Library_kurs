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

            var readers = Database.DBClass.GetReaders(true);
            cmbReaders.DataSource = readers;
            cmbReaders.DisplayMember = "FIO";


            dataGridView1.RowPrePaint += onRowPrePaint;

            LoadReport();
        }

        private void onRowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = dataGridView1.Rows[e.RowIndex];
            var messageInfo = row.DataBoundItem as MessageInfo;
            if (messageInfo.MessageType == "Штраф")
                row.DefaultCellStyle.BackColor = Color.Red;
            if (messageInfo.MessageType == "Уведомление")
                row.DefaultCellStyle.BackColor = Color.Orange;
            if (messageInfo.MessageType == "Напоминание")
                row.DefaultCellStyle.BackColor = Color.Yellow;
        }

        private void LoadReport()
        {
            dataGridView1.AutoGenerateColumns = true;
            var list = Database.DBClass.GetMessageInfo((cmbReaders.SelectedValue as DataClasses.Reader).id);
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

        private void button3_Click(object sender, EventArgs e)
        {
            Database.DBClass.DepartureMessages();
            LoadReport();
        }

        private void cmbReaders_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadReport();
        }
    }
}
