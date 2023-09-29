using QLSV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class frmTimKiem : Form
    {
        Model1 context = new Model1();
        public frmTimKiem()
        {
            InitializeComponent();
            
            
        }
        private void frmTimKiem_Load(object sender, EventArgs e)
        {
            List<Faculty> faculties = context.Faculties.ToList();
     
            FillDataCCB(faculties);

            cbbKhoa.SelectedItem = null;
            btnTimKiem.Click += btnTimKiem_Click;
           
            
        }
        private void FillDataCCB(List<Faculty> listFaculty)
        {
         
            cbbKhoa.DataSource = listFaculty;
            cbbKhoa.DisplayMember = "FacultyName";
            cbbKhoa.ValueMember = "FacultyID";
            
        }
        public void reset()
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string studentCode = txtMSSV.Text.Trim();
            string studentName = txtTen.Text.Trim();
            string facultyName = cbbKhoa.Text.Trim(); 

            var query = from student in context.Students
                        where (string.IsNullOrEmpty(studentCode) || student.StudentID.Contains(studentCode))
                              && (string.IsNullOrEmpty(studentName) || student.FullName.Contains(studentName))
                              && (string.IsNullOrEmpty(facultyName) || student.Faculty.FacultyName.Contains(facultyName))
                        select student;

            dataGridView1.Rows.Clear(); 

            foreach (var student in query)
            {
                int Rownew = dataGridView1.Rows.Add();
                dataGridView1.Rows[Rownew].Cells[0].Value = student.StudentID;
                dataGridView1.Rows[Rownew].Cells[1].Value = student.FullName;
                dataGridView1.Rows[Rownew].Cells[2].Value = student.Faculty.FacultyName;
                dataGridView1.Rows[Rownew].Cells[3].Value = student.AvrScore;
            }
            


        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtMSSV.Text = string.Empty;
            txtTen.Text = string.Empty;
            cbbKhoa.SelectedIndex = -1;
            dataGridView1.DataSource = null;
        }
    }
    }

