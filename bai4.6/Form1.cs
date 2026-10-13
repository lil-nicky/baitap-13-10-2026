using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Bai46
{
    public class Student
    {
        public string StudentId { get; set; } = "";
        public string FullName { get; set; } = "";
        public int Age { get; set; }
        public string Gender { get; set; } = "";
        public string AvatarPath { get; set; } = "";
    }

    public partial class Form1 : Form
    {
        private BindingList<Student> students =
            new BindingList<Student>();

        private BindingSource bindingSource =
            new BindingSource();

        private string avatarPath = "";

        public Form1()
        {
            InitializeComponent();

            bindingSource.DataSource = students;
            dgvStudents.DataSource = bindingSource;

            cboGender.Items.Clear();
            cboGender.Items.Add("Nam");
            cboGender.Items.Add("Nữ");
            cboGender.DropDownStyle =
                ComboBoxStyle.DropDownList;

            dgvStudents.ReadOnly = true;
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.MultiSelect = false;
            dgvStudents.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvStudents.SelectionChanged +=
                DgvStudents_SelectionChanged;

            ClearInput();
        }

        // Chọn sinh viên trong bảng để sửa
        private void DgvStudents_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null)
                return;

            Student student =
                dgvStudents.CurrentRow.DataBoundItem as Student;

            if (student == null)
                return;

            txtStudentId.Text = student.StudentId;
            txtFullName.Text = student.FullName;
            txtAge.Text = student.Age.ToString();
            cboGender.SelectedItem = student.Gender;

            avatarPath = student.AvatarPath;
            ShowAvatar(avatarPath);
        }

        // THÊM SINH VIÊN
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id = txtStudentId.Text.Trim();
            string name = txtFullName.Text.Trim();
            string gender = cboGender.Text;

            if (id == "" || name == "")
            {
                MessageBox.Show(
                    "Vui lòng nhập mã sinh viên và họ tên!");
                return;
            }

            if (!int.TryParse(txtAge.Text.Trim(), out int age)
                || age <= 0)
            {
                MessageBox.Show("Tuổi phải là số nguyên dương!");
                return;
            }

            if (gender != "Nam" && gender != "Nữ")
            {
                MessageBox.Show("Vui lòng chọn giới tính!");
                return;
            }

            foreach (Student s in students)
            {
                if (string.Equals(
                    s.StudentId, id,
                    StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại!");
                    return;
                }
            }

            Student newStudent = new Student
            {
                StudentId = id,
                FullName = name,
                Age = age,
                Gender = gender,
                AvatarPath = avatarPath
            };

            students.Add(newStudent);
            bindingSource.ResetBindings(false);

            MessageBox.Show("Thêm sinh viên thành công!");

            ClearInput();
            dgvStudents.ClearSelection();
        }

        // CẬP NHẬT SINH VIÊN ĐANG CHỌN
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null)
            {
                MessageBox.Show("Hãy chọn sinh viên cần cập nhật!");
                return;
            }

            Student selected =
                dgvStudents.CurrentRow.DataBoundItem as Student;

            if (selected == null)
            {
                MessageBox.Show("Hãy chọn sinh viên cần cập nhật!");
                return;
            }

            string id = txtStudentId.Text.Trim();
            string name = txtFullName.Text.Trim();
            string gender = cboGender.Text;

            if (id == "" || name == "")
            {
                MessageBox.Show(
                    "Mã sinh viên và họ tên không được để trống!");
                return;
            }

            if (!int.TryParse(txtAge.Text.Trim(), out int age)
                || age <= 0)
            {
                MessageBox.Show("Tuổi phải là số nguyên dương!");
                return;
            }

            if (gender != "Nam" && gender != "Nữ")
            {
                MessageBox.Show("Vui lòng chọn giới tính!");
                return;
            }

            foreach (Student s in students)
            {
                if (s != selected &&
                    string.Equals(
                        s.StudentId, id,
                        StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại!");
                    return;
                }
            }

            selected.StudentId = id;
            selected.FullName = name;
            selected.Age = age;
            selected.Gender = gender;
            selected.AvatarPath = avatarPath;

            bindingSource.ResetBindings(false);
            dgvStudents.Refresh();

            MessageBox.Show("Cập nhật sinh viên thành công!");
        }

        // XÓA SINH VIÊN
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null)
            {
                MessageBox.Show("Hãy chọn sinh viên cần xóa!");
                return;
            }

            Student selected =
                dgvStudents.CurrentRow.DataBoundItem as Student;

            if (selected == null)
                return;

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa sinh viên này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                students.Remove(selected);
                bindingSource.ResetBindings(false);

                ClearInput();
                dgvStudents.ClearSelection();
            }
        }

        // CHỌN ẢNH ĐẠI DIỆN
        private void btnChooseAvatar_Click(
            object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Chọn ảnh đại diện";
                dialog.Filter =
                    "Image files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    avatarPath = dialog.FileName;
                    ShowAvatar(avatarPath);
                }
            }
        }

        // HIỂN THỊ ẢNH
        private void ShowAvatar(string path)
        {
            if (picAvatar.Image != null)
            {
                Image oldImage = picAvatar.Image;
                picAvatar.Image = null;
                oldImage.Dispose();
            }

            if (!string.IsNullOrWhiteSpace(path) &&
                File.Exists(path))
            {
                using (Image source = Image.FromFile(path))
                {
                    picAvatar.Image = new Bitmap(source);
                }
            }
        }

        // XUẤT CSV
        private void btnExportCsv_Click(
            object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Xuất danh sách sinh viên";
                dialog.Filter = "CSV files (*.csv)|*.csv";
                dialog.FileName = "DanhSachSinhVien.csv";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    using (StreamWriter writer =
                        new StreamWriter(
                            dialog.FileName,
                            false,
                            new UTF8Encoding(true)))
                    {
                        writer.WriteLine(
                            "MaSinhVien,HoTen,Tuoi,GioiTinh,DuongDanAnh");

                        foreach (Student s in students)
                        {
                            writer.WriteLine(
                                Csv(s.StudentId) + "," +
                                Csv(s.FullName) + "," +
                                s.Age + "," +
                                Csv(s.Gender) + "," +
                                Csv(s.AvatarPath));
                        }
                    }

                    MessageBox.Show("Xuất CSV thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Lỗi khi xuất CSV: " + ex.Message);
                }
            }
        }

        private string Csv(string value)
        {
            value = value ?? "";
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        // NHẬP MỚI
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
            dgvStudents.ClearSelection();
        }

        private void ClearInput()
        {
            txtStudentId.Clear();
            txtFullName.Clear();
            txtAge.Clear();
            cboGender.SelectedIndex = -1;

            avatarPath = "";
            ShowAvatar("");

        }

        protected override void OnFormClosed(

            FormClosedEventArgs e)

        {

            if (picAvatar.Image != null)

            {

                picAvatar.Image.Dispose();

                picAvatar.Image = null;

            }

            base.OnFormClosed(e);

        }

    }

}
