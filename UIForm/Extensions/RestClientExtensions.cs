using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UIForm.Auth;

namespace UIForm.Extensions
{
    public static class RestClientExtensions
    {
        public static void AddAuth(this RestClient restClient)
        {
            restClient.AddDefaultParameter("Authorization", $"Bearer {(Thread.CurrentPrincipal.Identity as Identity).Token}");
            
        }
    }
}
