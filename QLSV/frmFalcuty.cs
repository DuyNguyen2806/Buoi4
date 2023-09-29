using QLSV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLSV.Models;

namespace QLSV
{
    public partial class frmFalcuty : Form
    {
        Model1 context = new Model1 ();
        public frmFalcuty()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void frmFalcuty_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            dataGridView1.Rows.Clear(); 
            List<Faculty> listFal = context.Faculties.ToList();
            foreach (var faculty in listFal)
            {
                int newRow = dataGridView1.Rows.Add();
                dataGridView1.Rows[newRow].Cells[0].Value = faculty.FacultyID;
                dataGridView1.Rows[newRow].Cells[1].Value = faculty.FacultyName;
                dataGridView1.Rows[newRow].Cells[2].Value = faculty.TotalProfessor;
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKhoa.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            txtTenKhoa.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            txtSoGS.Text = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Faculty f = new Faculty();
            if(txtMaKhoa.Text == "" || txtTenKhoa.Text == ""|| txtSoGS.Text == "")
            {

                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Loi khi them",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            f.FacultyID = Convert.ToInt32(txtMaKhoa.Text.Trim());
            f.FacultyName = txtTenKhoa.Text.Trim();
            f.TotalProfessor = Convert.ToInt32(txtSoGS.Text.Trim());
            context.Faculties.Add(f);
            MessageBox.Show("Da them thanh cong:  ",
                           "Thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);

            context.SaveChanges();
            LoadData();

        }

        private bool CheckDataInput(string text)
        {
            return true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var newID = Convert.ToInt32(txtMaKhoa.Text.Trim());
            var rs = context.Faculties.FirstOrDefault(s=> s.FacultyID == newID );
            if(rs != null)
            {
                rs.FacultyName = txtTenKhoa.Text.Trim();
                rs.TotalProfessor = Convert.ToInt32(txtSoGS.Text.Trim());
               
            }
            context.SaveChanges();
            MessageBox.Show("Da sua thanh cong  ",
                       "Thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var newID = Convert.ToInt32(txtMaKhoa.Text.Trim());
            var rs = context.Faculties.FirstOrDefault(s => s.FacultyID == newID);
            if (rs != null)
            {
               context.Faculties.Remove(rs);
                context.SaveChanges();
                MessageBox.Show("Da xoa thanh cong  ",
                       "Thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }
    }
}
