using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIForm
{
    public partial class Index : Form
    {
        public Index()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var register = new Register();
            register.Show();
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var login = new Login();
            login.Show();
            
        }
    }
}
