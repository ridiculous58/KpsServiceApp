using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIForm.Auth;

namespace UIForm
{
    public partial class Login : Form
    {
        private AuthService _authSerivice;
        public Login()
        {
            InitializeComponent();
            _authSerivice = new AuthService();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _authSerivice.Login(new Models.Request.LoginModel { Email = txtEmail.Text, Password = txtPassword.Text });
            MessageBox.Show("Hoş Geldiniz : " + (Thread.CurrentPrincipal.Identity as Identity).Name);
        }
    }
}
