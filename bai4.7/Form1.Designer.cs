
namespace Bai47
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ContextMenuStrip contextMenuMain;

        private System.Windows.Forms.ToolStripMenuItem menuSystem;
        private System.Windows.Forms.ToolStripMenuItem menuLogout;
        private System.Windows.Forms.ToolStripMenuItem menuExit;

        private System.Windows.Forms.ToolStripMenuItem menuCategory;
        private System.Windows.Forms.ToolStripMenuItem menuStudent;
        private System.Windows.Forms.ToolStripMenuItem menuProduct;

        private System.Windows.Forms.ToolStripMenuItem menuWindow;
        private System.Windows.Forms.ToolStripMenuItem menuCascade;
        private System.Windows.Forms.ToolStripMenuItem menuHorizontal;
        private System.Windows.Forms.ToolStripMenuItem menuVertical;

        private System.Windows.Forms.ToolStripButton btnStudent;
        private System.Windows.Forms.ToolStripButton btnProduct;

        private System.Windows.Forms.ToolStripStatusLabel lblUser;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            menuStrip = new System.Windows.Forms.MenuStrip();
            toolStrip = new System.Windows.Forms.ToolStrip();
            statusStrip = new System.Windows.Forms.StatusStrip();
            contextMenuMain = new System.Windows.Forms.ContextMenuStrip(components);

            menuSystem = new System.Windows.Forms.ToolStripMenuItem();
            menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            menuExit = new System.Windows.Forms.ToolStripMenuItem();

            menuCategory = new System.Windows.Forms.ToolStripMenuItem();
            menuStudent = new System.Windows.Forms.ToolStripMenuItem();
            menuProduct = new System.Windows.Forms.ToolStripMenuItem();

            menuWindow = new System.Windows.Forms.ToolStripMenuItem();
            menuCascade = new System.Windows.Forms.ToolStripMenuItem();
            menuHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            menuVertical = new System.Windows.Forms.ToolStripMenuItem();

            btnStudent = new System.Windows.Forms.ToolStripButton();
            btnProduct = new System.Windows.Forms.ToolStripButton();

            lblUser = new System.Windows.Forms.ToolStripStatusLabel();
            lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            lblTime = new System.Windows.Forms.ToolStripStatusLabel();

            menuStrip.SuspendLayout();
            toolStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();

            // FormMain
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1000, 650);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip;
            MinimumSize = new System.Drawing.Size(800, 500);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Bài 4.7 - Khung ứng dụng MDI";

            // Menu Hệ thống
            menuSystem.Text = "Hệ thống";
            menuLogout.Text = "Đăng xuất";
            menuExit.Text = "Thoát";

            menuLogout.Click +=
                new System.EventHandler(menuLogout_Click);
            menuExit.Click +=
                new System.EventHandler(menuExit_Click);

            menuSystem.DropDownItems.AddRange(
                new System.Windows.Forms.ToolStripItem[]
                {
                    menuLogout,
                    new System.Windows.Forms.ToolStripSeparator(),
                    menuExit
                });

            // Menu Danh mục
            menuCategory.Text = "Danh mục";
            menuStudent.Text = "Quản lý sinh viên";
            menuProduct.Text = "Quản lý sản phẩm";

            menuStudent.Click +=
                new System.EventHandler(menuStudent_Click);
            menuProduct.Click +=
                new System.EventHandler(menuProduct_Click);

            menuCategory.DropDownItems.AddRange(
                new System.Windows.Forms.ToolStripItem[]
                {
                    menuStudent,
                    menuProduct
                });

            // Menu Cửa sổ
            menuWindow.Text = "Cửa sổ";
            menuCascade.Text = "Cascade";
            menuHorizontal.Text = "Tile Horizontal";
            menuVertical.Text = "Tile Vertical";

            menuCascade.Click +=
                new System.EventHandler(menuCascade_Click);
            menuHorizontal.Click +=
                new System.EventHandler(menuHorizontal_Click);
            menuVertical.Click +=
                new System.EventHandler(menuVertical_Click);

            menuWindow.DropDownItems.AddRange(
                new System.Windows.Forms.ToolStripItem[]
                {
                    menuCascade,
                    menuHorizontal,
                    menuVertical
                });

            menuStrip.Items.AddRange(
                new System.Windows.Forms.ToolStripItem[]
                {
                    menuSystem,
                    menuCategory,
                    menuWindow
                });

            menuStrip.Dock = System.Windows.Forms.DockStyle.Top;

            // ToolStrip
            btnStudent.Text = "Quản lý sinh viên";
            btnStudent.DisplayStyle =
                System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            btnStudent.Click +=
                new System.EventHandler(btnStudent_Click);

            btnProduct.Text = "Quản lý sản phẩm";
            btnProduct.DisplayStyle =
                System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            btnProduct.Click +=
                new System.EventHandler(btnProduct_Click);

            toolStrip.Items.AddRange(
                new System.Windows.Forms.ToolStripItem[]
                {
                    btnStudent,
                    btnProduct
                });

            toolStrip.Dock = System.Windows.Forms.DockStyle.Top;

            // StatusStrip
            lblUser.Text = "Người dùng: Admin";
            lblStatus.Text = "Trạng thái: Sẵn sàng";
            lblStatus.Spring = true;
            lblStatus.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;
            lblTime.Text = "";

            statusStrip.Items.AddRange(
                new System.Windows.Forms.ToolStripItem[]
                {
                    lblUser,
                    lblStatus,
                    lblTime
                });

            statusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;

            // ContextMenuStrip của form chính
            contextMenuMain.Items.Add(
                "Mở quản lý sinh viên", null, ctxOpenStudent_Click);
            contextMenuMain.Items.Add(
                "Mở quản lý sản phẩm", null, ctxOpenProduct_Click);

            ContextMenuStrip = contextMenuMain;

            // Thứ tự thêm các thanh giao diện
            Controls.Add(statusStrip);
            Controls.Add(toolStrip);
            Controls.Add(menuStrip);

            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
