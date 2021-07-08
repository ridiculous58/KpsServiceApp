using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIForm.Auth;
using UIForm.Models.Request;
using UIForm.Models.Result;
namespace UIForm
{
    public partial class Register : Form
    {
        private AuthService _authService;
        public Register()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var request = new RestRequest("/auth/register");
            var registerModel = new RegisterModel
            {
                Email = txtEmail.Text,
                Password = txtPassword.Text,
                TcNo = txtTcNo.Text

            };
            var result = _authService.Register(registerModel);

            if (result.Success)
            {
                MessageBox.Show((Thread.CurrentPrincipal.Identity as Identity).Name);
                MessageBox.Show((Thread.CurrentPrincipal.Identity as Identity).Roles.First());
            }

        }



    }
}
