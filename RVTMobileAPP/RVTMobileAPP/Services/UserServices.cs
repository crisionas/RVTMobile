using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RVTMobileAPP.Interfaces;
using RVTMobileAPP.Models.Registration;
using Xamarin.Forms;

[assembly: Dependency(typeof(RVTMobileAPP.Services.UserServices))]
namespace RVTMobileAPP.Services
{
    public class UserServices:IUser
    {
        
        public async Task<RegistrationResponse> Registration(RegistrationMessage model)
        {

            var data_req = JsonConvert.SerializeObject(model);
            var content = new StringContent(data_req, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            //CHANGE ADDRESS AND INSTALL CERTIFICATES TO ACCES IT
            client.BaseAddress = new Uri("https://rvt-administratorapi.conveyor.cloud/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.PostAsync("api/Identity/Registration", content);
            var regresp = new RegistrationResponse();

            try
            {
                var data_resp = await response.Result.Content.ReadAsStringAsync();
                regresp = JsonConvert.DeserializeObject<RegistrationResponse>(data_resp);

            }
            catch
            {
                regresp.Message = "Error! Încercați mai tîrziu.";
            }

            return regresp;

        }
    }
}
