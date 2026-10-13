namespace Bai46
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvStudents;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.Label lblStudentId;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.TextBox txtStudentId;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.ComboBox cboGender;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Button btnChooseAvatar;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnExportCsv;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvStudents = new System.Windows.Forms.DataGridView();
            grpInfo = new System.Windows.Forms.GroupBox();
            lblStudentId = new System.Windows.Forms.Label();
            lblFullName = new System.Windows.Forms.Label();
            lblAge = new System.Windows.Forms.Label();
            lblGender = new System.Windows.Forms.Label();
            txtStudentId = new System.Windows.Forms.TextBox();
            txtFullName = new System.Windows.Forms.TextBox();
            txtAge = new System.Windows.Forms.TextBox();
            cboGender = new System.Windows.Forms.ComboBox();
            picAvatar = new System.Windows.Forms.PictureBox();
            btnChooseAvatar = new System.Windows.Forms.Button();
            btnAdd = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            btnUpdate = new System.Windows.Forms.Button();
            btnExportCsv = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            SuspendLayout();

            dgvStudents.Location = new System.Drawing.Point(20, 20);
            dgvStudents.Size = new System.Drawing.Size(750, 230);
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.ReadOnly = true;
            dgvStudents.MultiSelect = false;
            dgvStudents.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.RowHeadersVisible = false;

            grpInfo.Text = "Thông tin sinh viên";
            grpInfo.Location = new System.Drawing.Point(20, 265);
            grpInfo.Size = new System.Drawing.Size(500, 220);

            lblStudentId.Text = "Mã SV:";
            lblStudentId.Location = new System.Drawing.Point(20, 35);
            lblStudentId.AutoSize = true;
            txtStudentId.Location = new System.Drawing.Point(110, 30);
            txtStudentId.Size = new System.Drawing.Size(160, 27);

            lblFullName.Text = "Họ tên:";
            lblFullName.Location = new System.Drawing.Point(20, 75);
            lblFullName.AutoSize = true;
            txtFullName.Location = new System.Drawing.Point(110, 70);
            txtFullName.Size = new System.Drawing.Size(160, 27);

            lblAge.Text = "Tuổi:";
            lblAge.Location = new System.Drawing.Point(20, 115);
            lblAge.AutoSize = true;
            txtAge.Location = new System.Drawing.Point(110, 110);
            txtAge.Size = new System.Drawing.Size(160, 27);

            lblGender.Text = "Giới tính:";
            lblGender.Location = new System.Drawing.Point(20, 155);
            lblGender.AutoSize = true;
            cboGender.Location = new System.Drawing.Point(110, 150);
            cboGender.Size = new System.Drawing.Size(160, 28);
            cboGender.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            picAvatar.Location = new System.Drawing.Point(300, 30);
            picAvatar.Size = new System.Drawing.Size(150, 140);
            picAvatar.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;
            picAvatar.SizeMode =
                System.Windows.Forms.PictureBoxSizeMode.Zoom;

            btnChooseAvatar.Text = "Chọn ảnh";
            btnChooseAvatar.Location = new System.Drawing.Point(320, 175);
            btnChooseAvatar.Size = new System.Drawing.Size(110, 30);
            btnChooseAvatar.Click += btnChooseAvatar_Click;

            grpInfo.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblStudentId, txtStudentId,
                lblFullName, txtFullName,
                lblAge, txtAge,
                lblGender, cboGender,
                picAvatar, btnChooseAvatar
            });

            btnAdd.Text = "Thêm";
            btnAdd.Location = new System.Drawing.Point(550, 285);
            btnAdd.Size = new System.Drawing.Size(100, 35);
            btnAdd.Click += btnAdd_Click;

            btnDelete.Text = "Xóa";
            btnDelete.Location = new System.Drawing.Point(665, 285);
            btnDelete.Size = new System.Drawing.Size(100, 35);
            btnDelete.Click += btnDelete_Click;

            btnUpdate.Text = "Cập nhật";
            btnUpdate.Location = new System.Drawing.Point(550, 335);
            btnUpdate.Size = new System.Drawing.Size(100, 35);
            btnUpdate.Click += btnUpdate_Click;

            btnExportCsv.Text = "Xuất CSV";
            btnExportCsv.Location = new System.Drawing.Point(665, 335);
            btnExportCsv.Size = new System.Drawing.Size(100, 35);
            btnExportCsv.Click += btnExportCsv_Click;

            Text = "Bài 4.6 - Quản lý sinh viên";
            StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;
            ClientSize = new System.Drawing.Size(800, 510);
            MinimumSize = new System.Drawing.Size(820, 550);

            Controls.Add(dgvStudents);
            Controls.Add(grpInfo);
            Controls.Add(btnAdd);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnExportCsv);

            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            grpInfo.ResumeLayout(false);
            grpInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ResumeLayout(false);
        }
    }
}