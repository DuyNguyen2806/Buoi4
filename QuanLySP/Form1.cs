using QuanLySP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySP
{
    public partial class Form1 : Form
    {
        Model1 context = new Model1();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBox1.Checked)
            {
                int selectedDate = dateTimePicker1.Value.Month;
                var query = context.Invoices.Where(o=> o.OrderDate.Month == selectedDate).ToList();


                var rs = context.Orders
               .Select(o => new
               {
                   OrderNumber = o.InvoiceNo,
                   STT = o.No,
                   OrderDate = o.Invoice.OrderDate,
                   DeliveryDate = o.Invoice.DeliveryDate,
                   TotalAmount = o.Price
               }).Where(o => o.OrderDate.Month == selectedDate).ToList();
               foreach ( var o in rs )
                {
                    int newRow = dataGridView1.Rows.Add();
                    dataGridView1.Rows[newRow].Cells[0].Value = o.STT;
                    dataGridView1.Rows[newRow].Cells[1].Value = o.OrderNumber;
                    dataGridView1.Rows[newRow].Cells[2].Value = o.OrderDate;
                    dataGridView1.Rows[newRow].Cells[3].Value = o.DeliveryDate;
                    dataGridView1.Rows[newRow].Cells[4].Value = o.TotalAmount;
                }

            }
            
        }
    }
}
