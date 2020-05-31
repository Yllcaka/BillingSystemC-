using BillingSystem.BLL;
using BillingSystem.DAL;
using BillingSystem.MYSTUFF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BillingSystem.UI
{
    public partial class frmCategories : Form
    {
        public frmCategories()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
        }
        categoriesBLL c = new categoriesBLL();
        categoriesDAL dal = new categoriesDAL();
        userDAL udal = new userDAL();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;

            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);

            c.added_by = usr.id;

            bool success = dal.Insert(c);

            MessageBox.Show(success ? "Categoria e re eshte futur me Sukses" : "Futja e Categorise Deshtoi.");
            if (success) ClearText();
        }
        public void refreshDataGridView(string type = null)
        {
            DataTable dt = type != null ? dal.Search(type) : dal.Select();
            dgvCategories.DataSource = dt;
        }
        public void ClearText() {
            txtCategoryID.Text = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";
            refreshDataGridView();
        }
        private void frmCategories_Load(object sender, EventArgs e)
        {
            refreshDataGridView();
        }

        private void dgvCategories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            Func<int, string> currentRow = x => dgvCategories.Rows[rowIndex].Cells[x].Value.ToString();
            txtCategoryID.Text = currentRow(0);
            txtTitle.Text = currentRow(1);
            txtDescription.Text = currentRow(2);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            c.id = int.Parse(txtCategoryID.Text);
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;

            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);

            c.added_by = usr.id;

            bool success = dal.Update(c);
            MessageBox.Show(success ? "Categoria eshte bere Update me Sukses" : "Update e Categoris Deshtoi.");
            if (success) ClearText();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            c.id = int.Parse(txtCategoryID.Text);

            bool success = dal.Delete(c);

            MessageBox.Show(success ? "Categoria eshte shlyer me Sukses" : "Shlyerja e Categoris Deshtoi.");
            if (success) ClearText();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;
            refreshDataGridView(keywords);
        }
    }
}
