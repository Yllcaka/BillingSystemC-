using BillingSystem.BLL;
using BillingSystem.DAL;
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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();
        public static string loggedIn;
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            l.username = txtUserName.Text.Trim();
            l.password = txtPassword.Text.Trim();
            l.user_type = cmbUserType.Text.Trim();

            bool sucess = dal.loginCheck(l);
            MessageBox.Show(sucess ? "Login dul i suksesshum" : "Login deshtoi. Provo serish");
            if (sucess) {

                loggedIn = l.username;

                switch (l.user_type) {
                    case "Admin":
                        {
                            frmAdminDashboard admin = new frmAdminDashboard();
                            admin.Show();
                            this.Hide();
                        }
                        break;
                    case "User":
                        {
                            UserDashBoard user = new UserDashBoard();
                            user.Show();
                            this.Hide();
                        }
                        break;
                    default:
                        MessageBox.Show("User Type nuk eshte valid");
                        break;
                }
            }
        }
    }
}
