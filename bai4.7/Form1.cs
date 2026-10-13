
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bai47
{
    public partial class FormMain : Form
    {
        private System.Windows.Forms.Timer clockTimer;

        public FormMain()
        {
            InitializeComponent();

            lblUser.Text = "Người dùng: Admin";
            lblStatus.Text = "Trạng thái: Sẵn sàng";
            lblTime.Text = "";

            clockTimer = new System.Windows.Forms.Timer();
            clockTimer.Interval = 1000;
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();

            this.FormClosing += FormMain_FormClosing;
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clockTimer != null)
            {
                clockTimer.Stop();
                clockTimer.Dispose();
            }
        }

        // Form con gọi hàm này để cập nhật StatusStrip
        public void UpdateStatus(string message)
        {
            lblStatus.Text = "Trạng thái: " + message;
        }

        // Chỉ mở một FormStudent
        private void OpenStudent()
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child is FormStudent)
                {
                    child.Activate();
                    if (child.WindowState == FormWindowState.Minimized)
                        child.WindowState = FormWindowState.Normal;
                    return;
                }
            }

            FormStudent form = new FormStudent();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();

            UpdateStatus("Đã mở quản lý sinh viên");
        }

        // Chỉ mở một FormProduct
        private void OpenProduct()
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child is FormProduct)
                {
                    child.Activate();
                    if (child.WindowState == FormWindowState.Minimized)
                        child.WindowState = FormWindowState.Normal;
                    return;
                }
            }

            FormProduct form = new FormProduct();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();

            UpdateStatus("Đã mở quản lý sản phẩm");
        }

        private void menuStudent_Click(object sender, EventArgs e)
        {
            OpenStudent();
        }

        private void menuProduct_Click(object sender, EventArgs e)
        {
            OpenProduct();
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            OpenStudent();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            OpenProduct();
        }

        private void menuCascade_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
            UpdateStatus("Đã sắp xếp cửa sổ kiểu Cascade");
        }

        private void menuHorizontal_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
            UpdateStatus("Đã xếp cửa sổ theo chiều ngang");
        }

        private void menuVertical_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
            UpdateStatus("Đã xếp cửa sổ theo chiều dọc");
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn đăng xuất không?",
                "Đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (Form child in this.MdiChildren)
                    child.Close();

                lblUser.Text = "Người dùng: Chưa đăng nhập";
                UpdateStatus("Đã đăng xuất");
            }
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctxOpenStudent_Click(object sender, EventArgs e)
        {
            OpenStudent();
        }

        private void ctxOpenProduct_Click(object sender, EventArgs e)
        {
            OpenProduct();
        }
    }

    // =====================================================
    // FORM QUẢN LÝ SINH VIÊN
    // =====================================================

    public class StudentItem
    {
        public string StudentId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }

    public class FormStudent : Form
    {
        private BindingList<StudentItem> students;
        private BindingSource source;
        private DataGridView dgv;
        private TextBox txtId;
        private TextBox txtName;
        private TextBox txtAge;
        private ComboBox cboGender;

        public FormStudent()
        {
            students = new BindingList<StudentItem>();
            source = new BindingSource();

            InitializeStudentForm();
        }

        private void InitializeStudentForm()
        {
            Text = "Quản lý sinh viên";
            Size = new Size(850, 500);

            Label lblId = new Label();
            lblId.Text = "Mã sinh viên:";
            lblId.Location = new Point(20, 20);
            lblId.AutoSize = true;

            txtId = new TextBox();
            txtId.Location = new Point(125, 16);
            txtId.Width = 180;

            Label lblName = new Label();
            lblName.Text = "Họ tên:";
            lblName.Location = new Point(20, 55);
            lblName.AutoSize = true;

            txtName = new TextBox();
            txtName.Location = new Point(125, 51);
            txtName.Width = 180;

            Label lblAge = new Label();
            lblAge.Text = "Tuổi:";
            lblAge.Location = new Point(20, 90);
            lblAge.AutoSize = true;

            txtAge = new TextBox();
            txtAge.Location = new Point(125, 86);
            txtAge.Width = 180;

            Label lblGender = new Label();
            lblGender.Text = "Giới tính:";
            lblGender.Location = new Point(350, 20);
            lblGender.AutoSize = true;

            cboGender = new ComboBox();
            cboGender.Location = new Point(430, 16);
            cboGender.Width = 150;
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGender.Items.Add("Nam");
            cboGender.Items.Add("Nữ");

            Button btnAdd = new Button();
            btnAdd.Text = "Thêm";
            btnAdd.Location = new Point(350, 51);
            btnAdd.Size = new Size(85, 30);
            btnAdd.Click += BtnAdd_Click;

            Button btnDelete = new Button();
            btnDelete.Text = "Xóa";
            btnDelete.Location = new Point(445, 51);
            btnDelete.Size = new Size(85, 30);
            btnDelete.Click += BtnDelete_Click;

            Button btnClear = new Button();
            btnClear.Text = "Nhập mới";
            btnClear.Location = new Point(540, 51);
            btnClear.Size = new Size(95, 30);
            btnClear.Click += BtnClear_Click;

            dgv = new DataGridView();
            dgv.Location = new Point(20, 140);
            dgv.Size = new Size(790, 280);
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            source.DataSource = students;
            dgv.DataSource = source;

            // Menu chuột phải trên bảng
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Xóa dòng", null, ContextDelete_Click);
            contextMenu.Items.Add("Làm mới dữ liệu", null, ContextRefresh_Click);
            contextMenu.Items.Add("Xem chi tiết", null, ContextDetails_Click);
            dgv.ContextMenuStrip = contextMenu;

            dgv.SelectionChanged += Dgv_SelectionChanged;

            Controls.Add(lblId);
            Controls.Add(txtId);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblAge);
            Controls.Add(txtAge);
            Controls.Add(lblGender);
            Controls.Add(cboGender);
            Controls.Add(btnAdd);
            Controls.Add(btnDelete);
            Controls.Add(btnClear);
            Controls.Add(dgv);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string id = txtId.Text.Trim();
            string name = txtName.Text.Trim();
            int age;

            if (id == "" || name == "")
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên và họ tên!");
                return;
            }

            if (!int.TryParse(txtAge.Text.Trim(), out age) || age <= 0)
            {
                MessageBox.Show("Tuổi phải là số nguyên dương!");
                return;
            }

            if (cboGender.Text == "")
            {
                MessageBox.Show("Vui lòng chọn giới tính!");
                return;
            }

            foreach (StudentItem s in students)
            {
                if (string.Equals(s.StudentId, id,
                    StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại!");
                    return;
                }
            }

            students.Add(new StudentItem
            {
                StudentId = id,
                FullName = name,
                Age = age,
                Gender = cboGender.Text
            });

            source.ResetBindings(false);
            ClearInput();

            FormMain parent = MdiParent as FormMain;
            if (parent != null)
                parent.UpdateStatus("Thêm sinh viên thành công: " + id);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelected();
        }

        private void ContextDelete_Click(object sender, EventArgs e)
        {
            DeleteSelected();
        }

        private void DeleteSelected()
        {
            if (dgv.CurrentRow == null ||
                dgv.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!");
                return;
            }

            StudentItem student =
                dgv.CurrentRow.DataBoundItem as StudentItem;

            if (student == null)
                return;

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa sinh viên " + student.FullName + "?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            students.Remove(student);
            source.ResetBindings(false);
            ClearInput();

            FormMain parent = MdiParent as FormMain;
            if (parent != null)
                parent.UpdateStatus("Xóa sinh viên thành công: " + student.StudentId);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
            dgv.ClearSelection();
        }

        private void ClearInput()
        {
            txtId.Clear();
            txtName.Clear();
            txtAge.Clear();
            cboGender.SelectedIndex = -1;
        }

        private void Dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null)
                return;

            StudentItem student =
                dgv.CurrentRow.DataBoundItem as StudentItem;

            if (student == null)
                return;

            txtId.Text = student.StudentId;
            txtName.Text = student.FullName;
            txtAge.Text = student.Age.ToString();
            cboGender.Text = student.Gender;
        }

        private void ContextRefresh_Click(object sender, EventArgs e)
        {
            source.ResetBindings(false);

            FormMain parent = MdiParent as FormMain;
            if (parent != null)
                parent.UpdateStatus("Đã làm mới danh sách sinh viên");
        }

        private void ContextDetails_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null ||
                dgv.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên!");
                return;
            }

            StudentItem student =
                dgv.CurrentRow.DataBoundItem as StudentItem;

            if (student == null)
                return;

            MessageBox.Show(
                "Mã sinh viên: " + student.StudentId +
                "\nHọ tên: " + student.FullName +
                "\nTuổi: " + student.Age +
                "\nGiới tính: " + student.Gender,
                "Chi tiết sinh viên");
        }
    }

    // =====================================================
    // FORM QUẢN LÝ SẢN PHẨM
    // =====================================================

    public class ProductItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }

    public class FormProduct : Form
    {
        private BindingList<ProductItem> products;
        private BindingSource source;
        private DataGridView dgv;
        private TextBox txtId;
        private TextBox txtName;
        private TextBox txtPrice;

        public FormProduct()
        {
            products = new BindingList<ProductItem>();
            source = new BindingSource();

            InitializeProductForm();
        }

        private void InitializeProductForm()
        {
            Text = "Quản lý sản phẩm";
            Size = new Size(800, 450);

            Label lblId = new Label();
            lblId.Text = "Mã sản phẩm:";
            lblId.Location = new Point(20, 20);
            lblId.AutoSize = true;

            txtId = new TextBox();
            txtId.Location = new Point(125, 16);
            txtId.Width = 180;

            Label lblName = new Label();
            lblName.Text = "Tên sản phẩm:";
            lblName.Location = new Point(20, 55);
            lblName.AutoSize = true;

            txtName = new TextBox();
            txtName.Location = new Point(125, 51);
            txtName.Width = 180;

            Label lblPrice = new Label();
            lblPrice.Text = "Giá:";
            lblPrice.Location = new Point(350, 20);
            lblPrice.AutoSize = true;

            txtPrice = new TextBox();
            txtPrice.Location = new Point(400, 16);
            txtPrice.Width = 150;

            Button btnAdd = new Button();
            btnAdd.Text = "Thêm";
            btnAdd.Location = new Point(350, 51);
            btnAdd.Size = new Size(85, 30);
            btnAdd.Click += BtnAdd_Click;

            Button btnDelete = new Button();
            btnDelete.Text = "Xóa";
            btnDelete.Location = new Point(445, 51);
            btnDelete.Size = new Size(85, 30);
            btnDelete.Click += BtnDelete_Click;

            dgv = new DataGridView();
            dgv.Location = new Point(20, 110);
            dgv.Size = new Size(730, 260);
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            source.DataSource = products;
            dgv.DataSource = source;

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Xóa dòng", null, ContextDelete_Click);
            contextMenu.Items.Add("Làm mới dữ liệu", null, ContextRefresh_Click);
            contextMenu.Items.Add("Xem chi tiết", null, ContextDetails_Click);
            dgv.ContextMenuStrip = contextMenu;

            Controls.Add(lblId);
            Controls.Add(txtId);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblPrice);
            Controls.Add(txtPrice);
            Controls.Add(btnAdd);
            Controls.Add(btnDelete);
            Controls.Add(dgv);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string id = txtId.Text.Trim();
            string name = txtName.Text.Trim();
            decimal price;

            if (id == "" || name == "")
            {
                MessageBox.Show("Vui lòng nhập mã và tên sản phẩm!");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out price) || price < 0)
            {
                MessageBox.Show("Giá phải là số không âm!");
                return;
            }

            foreach (ProductItem p in products)
            {
                if (string.Equals(p.ProductId, id,
                    StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã sản phẩm đã tồn tại!");
                    return;
                }
            }

            products.Add(new ProductItem
            {
                ProductId = id,
                ProductName = name,
                Price = price
            });

            source.ResetBindings(false);
            txtId.Clear();
            txtName.Clear();
            txtPrice.Clear();

            FormMain parent = MdiParent as FormMain;
            if (parent != null)
                parent.UpdateStatus("Thêm sản phẩm thành công: " + id);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelected();
        }

        private void ContextDelete_Click(object sender, EventArgs e)
        {
            DeleteSelected();
        }

        private void DeleteSelected()
        {
            if (dgv.CurrentRow == null ||
                dgv.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!");
                return;
            }

            ProductItem product =
                dgv.CurrentRow.DataBoundItem as ProductItem;

            if (product == null)
                return;

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa sản phẩm " + product.ProductName + "?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            products.Remove(product);
            source.ResetBindings(false);

            FormMain parent = MdiParent as FormMain;
            if (parent != null)
                parent.UpdateStatus("Xóa sản phẩm thành công: " + product.ProductId);
        }

        private void ContextRefresh_Click(object sender, EventArgs e)
        {
            source.ResetBindings(false);

            FormMain parent = MdiParent as FormMain;
            if (parent != null)
                parent.UpdateStatus("Đã làm mới danh sách sản phẩm");
        }

        private void ContextDetails_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null ||
                dgv.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!");
                return;
            }

            ProductItem product =
                dgv.CurrentRow.DataBoundItem as ProductItem;

            if (product == null)
                return;

            MessageBox.Show(
                "Mã sản phẩm: " + product.ProductId +
                "\nTên sản phẩm: " + product.ProductName +
                "\nGiá: " + product.Price.ToString("N0"),
                "Chi tiết sản phẩm");
        }
    }
}
