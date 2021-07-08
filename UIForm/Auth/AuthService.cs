using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UIForm.Extensions;
using UIForm.Models.Request;
using UIForm.Models.Response;
using UIForm.Models.Result;
using UIForm.SingletonPattern;

namespace UIForm.Auth
{
    public class AuthService
    {
        private RestClient _restClient;
        public AuthService()
        {
            _restClient = Singleton.GetRestClient();
        }
        public IResult Register(RegisterModel registerModel)
        {
            var request = new RestRequest("/auth/register");

            request.AddJsonBody(registerModel);
            var response = _restClient.Post(request);
            var result = JsonConvert.DeserializeObject<DataResult<AccessToken>>(response.Content);
            
            var userModel = GetRoles(registerModel.Email);
           
            var identity = new Identity
            {
                Email = registerModel.Email,
                Token = result.Data.Token,
                AuthenticationType = "JWT",
                IsAuthenticated = true,
                Roles = userModel.Data.Roles,
                Name = $"{userModel.Data.FirstName} {userModel.Data.LastName}"
            };
            Thread.CurrentPrincipal = new GenericPrincipal(identity, identity.Roles);
            
            _restClient.AddAuth();
            
            return new Result(true);
        }
        public IResult Login(LoginModel loginModel)
        {
            var request = new RestRequest("/auth/login");
            request.AddJsonBody(loginModel);
            var response = _restClient.Post(request);
            var result = JsonConvert.DeserializeObject<DataResult<AccessToken>>(response.Content);

            var userModel = GetRoles(loginModel.Email);

            var identity = new Identity
            {
                Email = loginModel.Email,
                Token = result.Data.Token,
                AuthenticationType = "JWT",
                IsAuthenticated = true,
                Roles = userModel.Data.Roles,
                Name = $"{userModel.Data.FirstName} {userModel.Data.LastName}"
            };
            Thread.CurrentPrincipal = new GenericPrincipal(identity, identity.Roles);

            _restClient.AddAuth();

            return new Result(true);
        }
        public IDataResult<UserModel> GetRoles(string mail)
        {
            var request = new RestRequest($"/user/getuserbymail");
            request.AddParameter("mail", mail);
            var response = _restClient.Get(request);
            var userModel = JsonConvert.DeserializeObject<DataResult<UserModel>>(response.Content);
            return userModel;
        }
    }
}
