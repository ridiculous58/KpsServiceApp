using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIForm.SingletonPattern
{
    public class Singleton
    {
        private static RestClient _restClient;
        
        public static RestClient GetRestClient()
        {
            if (_restClient == null)
            {
                _restClient = new RestClient();
                _restClient.BaseUrl = new Uri("https://localhost:44351/api");
            }
            return _restClient;
        }
    }
}
