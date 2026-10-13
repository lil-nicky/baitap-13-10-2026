
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai47
{
    public partial class FormMain : Form
    {
        private ToolStripStatusLabel lblUser;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblTime;
        private System.Windows.Forms.Timer clockTimer;

        public FormMain()
        {
            InitializeComponent();
            CreateMainInterface();
        }

        private void CreateMainInterface()
        {
            MenuStrip menu = new MenuStrip();

            ToolStripMenuItem system = new ToolStripMenuItem("Hệ thống");
            ToolStripMenuItem logout = new ToolStripMenuItem("Đăng xuất");
            ToolStripMenuItem exit = new ToolStripMenuItem("Thoát");

            logout.Click += delegate
            {
                foreach (Form child in MdiChildren)
                    child.Close();

                lblUser.Text = "Người dùng: Chưa đăng nhập";
                UpdateStatus("Đã đăng xuất");
            };

            exit.Click += delegate { Close(); };
            system.DropDownItems.Add(logout);
            system.DropDownItems.Add(exit);

            ToolStripMenuItem category = new ToolStripMenuItem("Danh mục");
            ToolStripMenuItem student = new ToolStripMenuItem("Quản lý sinh viên");
            ToolStripMenuItem product = new ToolStripMenuItem("Quản lý sản phẩm");

            student.Click += delegate { OpenChild(typeof(FormStudent)); };
            product.Click += delegate { OpenChild(typeof(FormProduct)); };

            category.DropDownItems.Add(student);
            category.DropDownItems.Add(product);

            ToolStripMenuItem window = new ToolStripMenuItem("Cửa sổ");
            ToolStripMenuItem cascade = new ToolStripMenuItem("Cascade");
            ToolStripMenuItem horizontal = new ToolStripMenuItem("Tile Horizontal");
            ToolStripMenuItem vertical = new ToolStripMenuItem("Tile Vertical");

            cascade.Click += delegate { LayoutMdi(MdiLayout.Cascade); };
            horizontal.Click += delegate { LayoutMdi(MdiLayout.TileHorizontal); };
            vertical.Click += delegate { LayoutMdi(MdiLayout.TileVertical); };

            window.DropDownItems.Add(cascade);
            window.DropDownItems.Add(horizontal);
            window.DropDownItems.Add(vertical);

            menu.Items.Add(system);
            menu.Items.Add(category);
            menu.Items.Add(window);

            ToolStrip tool = new ToolStrip();
            ToolStripButton btnStudent = new ToolStripButton("Quản lý sinh viên");
            ToolStripButton btnProduct = new ToolStripButton("Quản lý sản phẩm");

            btnStudent.Click += delegate { OpenChild(typeof(FormStudent)); };
            btnProduct.Click += delegate { OpenChild(typeof(FormProduct)); };

            tool.Items.Add(btnStudent);
            tool.Items.Add(btnProduct);

            StatusStrip status = new StatusStrip();
            lblUser = new ToolStripStatusLabel("Người dùng: Admin");
            lblStatus = new ToolStripStatusLabel("Trạng thái: Sẵn sàng");
            lblStatus.Spring = true;
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblTime = new ToolStripStatusLabel();

            status.Items.Add(lblUser);
            status.Items.Add(lblStatus);
            status.Items.Add(lblTime);

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Mở quản lý sinh viên", null,
                delegate { OpenChild(typeof(FormStudent)); });
            contextMenu.Items.Add("Mở quản lý sản phẩm", null,
                delegate { OpenChild(typeof(FormProduct)); });

            ContextMenuStrip = contextMenu;

            Controls.Add(status);
            Controls.Add(tool);
            Controls.Add(menu);

            MainMenuStrip = menu;
            menu.Dock = DockStyle.Top;
            tool.Dock = DockStyle.Top;
            status.Dock = DockStyle.Bottom;

            clockTimer = new System.Windows.Forms.Timer();
            clockTimer.Interval = 1000;
            clockTimer.Tick += delegate
            {
                lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            };
            clockTimer.Start();

            FormClosing += delegate
            {
                clockTimer.Stop();
                clockTimer.Dispose();
            };
        }

        public void UpdateStatus(string message)
        {
            lblStatus.Text = "Trạng thái: " + message;
        }

        public void UpdateProgress(string taskName, int percent)
        {
            lblStatus.Text = taskName + ": " + percent + "%";
        }

        private void OpenChild(Type formType)
        {
            foreach (Form child in MdiChildren)
            {
                if (child.GetType() == formType)
                {
                    if (child.WindowState == FormWindowState.Minimized)
                        child.WindowState = FormWindowState.Normal;

                    child.Activate();
                    return;
                }
            }

            Form form = (Form)Activator.CreateInstance(formType);
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }
    }

    // ================= MODEL SINH VIÊN =================

    public class StudentItem
    {
        public string StudentId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }

    // ================= FORM SINH VIÊN =================

    public class FormStudent : Form
    {
        private BindingList<StudentItem> students =
            new BindingList<StudentItem>();

        private BindingSource source = new BindingSource();
        private DataGridView dgv;
        private Button btnLoad;
        private ProgressBar progressBar;
        private Label lblPercent;

        public FormStudent()
        {
            Text = "Quản lý sinh viên - Async/Await";
            Size = new Size(850, 500);

            btnLoad = new Button();
            btnLoad.Text = "Nạp dữ liệu";
            btnLoad.Location = new Point(20, 20);
            btnLoad.Size = new Size(120, 30);
            btnLoad.Click += BtnLoad_Click;

            progressBar = new ProgressBar();
            progressBar.Location = new Point(160, 24);
            progressBar.Size = new Size(400, 22);
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;

            lblPercent = new Label();
            lblPercent.Text = "0%";
            lblPercent.Location = new Point(575, 27);
            lblPercent.AutoSize = true;

            dgv = new DataGridView();
            dgv.Location = new Point(20, 70);
            dgv.Size = new Size(790, 350);
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            source.DataSource = students;
            dgv.DataSource = source;

            ContextMenuStrip context = new ContextMenuStrip();
            context.Items.Add("Làm mới dữ liệu", null,
                async delegate { await LoadData(); });
            context.Items.Add("Xem chi tiết", null, ShowDetails);
            dgv.ContextMenuStrip = context;

            Controls.Add(btnLoad);
            Controls.Add(progressBar);
            Controls.Add(lblPercent);
            Controls.Add(dgv);
        }

        private async Task<List<StudentItem>> LoadStudentsAsync(
            IProgress<int> progress)
        {
            return await Task.Run(async delegate
            {
                List<StudentItem> result = new List<StudentItem>();

                for (int i = 1; i <= 10; i++)
                {
                    await Task.Delay(300);

                    result.Add(new StudentItem
                    {
                        StudentId = "SV" + i.ToString("000"),
                        FullName = "Sinh viên " + i,
                        Age = 18 + i % 5,
                        Gender = i % 2 == 0 ? "Nữ" : "Nam"
                    });

                    progress.Report(i * 10);
                }

                return result;
            });
        }

        private async void BtnLoad_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            btnLoad.Enabled = false;
            progressBar.Value = 0;
            lblPercent.Text = "0%";

            FormMain parent = MdiParent as FormMain;

            try
            {
                IProgress<int> progress = new Progress<int>(delegate (int value)
                {
                    int percent = Math.Max(0, Math.Min(100, value));
                    progressBar.Value = percent;
                    lblPercent.Text = percent + "%";

                    if (parent != null)
                        parent.UpdateProgress("Nạp sinh viên", percent);
                });

                List<StudentItem> data = await LoadStudentsAsync(progress);

                students.Clear();
                foreach (StudentItem item in data)
                    students.Add(item);

                source.ResetBindings(false);

                progressBar.Value = 100;
                lblPercent.Text = "100%";

                if (parent != null)
                    parent.UpdateStatus("Nạp dữ liệu sinh viên hoàn tất");
            }
            catch (Exception ex)
            {
                if (parent != null)
                    parent.UpdateStatus("Nạp sinh viên thất bại");

                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                btnLoad.Enabled = true;
            }
        }

        private void ShowDetails(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null ||
                dgv.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Chưa có sinh viên được chọn.");
                return;
            }

            StudentItem item =
                dgv.CurrentRow.DataBoundItem as StudentItem;

            if (item != null)
            {
                MessageBox.Show(
                    "Mã sinh viên: " + item.StudentId +
                    "\nHọ tên: " + item.FullName +
                    "\nTuổi: " + item.Age +
                    "\nGiới tính: " + item.Gender);
            }
        }
    }

    // ================= MODEL SẢN PHẨM =================

    public class ProductItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }

    // ================= FORM SẢN PHẨM =================

    public class FormProduct : Form
    {
        private BindingList<ProductItem> products =
            new BindingList<ProductItem>();

        private BindingSource source = new BindingSource();
        private DataGridView dgv;
        private Button btnLoad;
        private ProgressBar progressBar;
        private Label lblPercent;

        public FormProduct()
        {
            Text = "Quản lý sản phẩm - Async/Await";
            Size = new Size(820, 480);

            btnLoad = new Button();
            btnLoad.Text = "Nạp dữ liệu";
            btnLoad.Location = new Point(20, 20);
            btnLoad.Size = new Size(120, 30);
            btnLoad.Click += BtnLoad_Click;

            progressBar = new ProgressBar();
            progressBar.Location = new Point(160, 24);
            progressBar.Size = new Size(400, 22);
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;

            lblPercent = new Label();
            lblPercent.Text = "0%";
            lblPercent.Location = new Point(575, 27);
            lblPercent.AutoSize = true;

            dgv = new DataGridView();
            dgv.Location = new Point(20, 70);
            dgv.Size = new Size(760, 330);
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            source.DataSource = products;
            dgv.DataSource = source;

            ContextMenuStrip context = new ContextMenuStrip();
            context.Items.Add("Làm mới dữ liệu", null,
                async delegate { await LoadData(); });
            context.Items.Add("Xem chi tiết", null, ShowDetails);
            dgv.ContextMenuStrip = context;

            Controls.Add(btnLoad);
            Controls.Add(progressBar);
            Controls.Add(lblPercent);
            Controls.Add(dgv);
        }

        private async Task<List<ProductItem>> LoadProductsAsync(
            IProgress<int> progress)
        {
            return await Task.Run(async delegate
            {
                List<ProductItem> result = new List<ProductItem>();

                for (int i = 1; i <= 10; i++)
                {
                    await Task.Delay(300);

                    result.Add(new ProductItem
                    {
                        ProductId = "SP" + i.ToString("000"),
                        ProductName = "Sản phẩm " + i,
                        Price = i * 10000m
                    });

                    progress.Report(i * 10);
                }

                return result;
            });
        }

        private async void BtnLoad_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            btnLoad.Enabled = false;
            progressBar.Value = 0;
            lblPercent.Text = "0%";

            FormMain parent = MdiParent as FormMain;

            try
            {
                IProgress<int> progress = new Progress<int>(delegate (int value)
                {
                    int percent = Math.Max(0, Math.Min(100, value));
                    progressBar.Value = percent;
                    lblPercent.Text = percent + "%";

                    if (parent != null)
                        parent.UpdateProgress("Nạp sản phẩm", percent);
                });

                List<ProductItem> data = await LoadProductsAsync(progress);

                products.Clear();
                foreach (ProductItem item in data)
                    products.Add(item);

                source.ResetBindings(false);

                progressBar.Value = 100;
                lblPercent.Text = "100%";

                if (parent != null)
                    parent.UpdateStatus("Nạp dữ liệu sản phẩm hoàn tất");
            }
            catch (Exception ex)
            {
                if (parent != null)
                    parent.UpdateStatus("Nạp sản phẩm thất bại");

                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                btnLoad.Enabled = true;
            }
        }

        private void ShowDetails(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null ||
                dgv.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Chưa có sản phẩm được chọn.");
                return;
            }

            ProductItem item =
                dgv.CurrentRow.DataBoundItem as ProductItem;

            if (item != null)
            {
                MessageBox.Show(
                    "Mã sản phẩm: " + item.ProductId +
                    "\nTên sản phẩm: " + item.ProductName +
                    "\nGiá: " + item.Price.ToString("N0"));
            }
        }
    }
}
