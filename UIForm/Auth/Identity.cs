using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace UIForm.Auth
{
    public class Identity : IIdentity
    {
        public string Name { get; set; }

        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public string[] Roles { get; set; }

    }
}
