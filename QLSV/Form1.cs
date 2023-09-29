using QLSV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLSV.Models;
using System.Xml.Linq;
using System.Data.Entity.Migrations;

namespace QLSV
{
    public partial class Form1 : Form
    {
        Model1 sv = new Model1();
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
           List<Faculty> listFaculty = sv.Faculties.ToList();

            FillDataCCB(listFaculty);
            LoadData();

        }

        private void LoadData()
        {
            dataGridView1.Rows.Clear();
            List<Student> listStu = sv.Students.ToList();

            foreach (var item in listStu)
            {
                int newRow = dataGridView1.Rows.Add();
                dataGridView1.Rows[newRow].Cells[0].Value = item.StudentID;
                dataGridView1.Rows[newRow].Cells[1].Value = item.FullName;
                dataGridView1.Rows[newRow].Cells[2].Value = item.Faculty.FacultyName;
                dataGridView1.Rows[newRow].Cells[3].Value = item.AvrScore;
            }
        }

        private void FillDataCCB(List<Faculty> listFaculty)
        {
            cbbKhoa.DataSource = listFaculty;
            cbbKhoa.DisplayMember = "FacultyName";
            cbbKhoa.ValueMember = "FacultyID";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMSSV.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            txtTen.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            cbbKhoa.Text = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            txtDiem.Text = dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbbKhoa.SelectedItem == null)
            {
                MessageBox.Show("Chua chon khoa cho sv", "Loi khi them",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
          

            Student s = new Student();
             s.StudentID = txtMSSV.Text.Trim();
            
            
            s.FullName = txtTen.Text.Trim();
            s.FacultyID =Convert.ToInt32(cbbKhoa.SelectedValue);
            s.AvrScore = Convert.ToDouble(txtDiem.Text.Trim());

            try
            {
                sv.Students.AddOrUpdate(s);
                MessageBox.Show("Da them sinh vien:  " + txtTen.Text.Trim(),
                           "Thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);

                sv.SaveChanges();
                LoadData();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private bool CheckDataInput(string newID)
        {
           List<Student> s = new List<Student>();
            foreach (Student student in sv.Students)
            {
                if(newID.ToArray().Length ==10 && student.StudentID != newID)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var rs = sv.Students.FirstOrDefault(s=>s.StudentID == txtMSSV.Text.Trim());
            if (rs != null)
            {
                rs.FullName = txtTen.Text.Trim();
                rs.FacultyID = Convert.ToInt32(cbbKhoa.SelectedValue);
                rs.AvrScore = Convert.ToDouble(txtDiem.Text.Trim());
            }
            try
            {
                sv.SaveChanges();
                MessageBox.Show("Da sua sinh vien:  " + txtTen.Text.Trim(),
                           "Thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();


            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var rs = sv.Students.FirstOrDefault(s => s.StudentID == txtMSSV.Text.Trim());
            if(rs != null)
            {
                sv.Students.Remove(rs);
                MessageBox.Show("Da xoa sinh vien:  " + txtTen.Text.Trim(),
                          "Thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sv.SaveChanges() ;
                LoadData();
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void stbKhoa_Click(object sender, EventArgs e)
        {
            frmFalcuty f = new frmFalcuty();
            f.ShowDialog();
        }

        private void tsbTimKiem_Click(object sender, EventArgs e)
        {
            frmTimKiem f = new frmTimKiem();
            f.ShowDialog();
        }
    }
}
