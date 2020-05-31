using BillingSystem.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BillingSystem
{
    public partial class UserDashBoard : Form
    {
        public UserDashBoard()
        {
            InitializeComponent();
        }

        private void UserDashBoard_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
        }

        private void UserDashBoard_Load(object sender, EventArgs e)
        {
            lblUserName.Text = frmLogin.loggedIn;
        }
    }
}
