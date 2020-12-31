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
using RVTMobileAPP.Models.AuthUser;
using RVTMobileAPP.Models.Registration;
using RVTMobileAPP.Models.Vote;
using Xamarin.Forms;

[assembly: Dependency(typeof(RVTMobileAPP.Services.UserServices))]

namespace RVTMobileAPP.Services
{
    public class UserServices : IUser
    {
        public async Task<RegistrationResponse> Registration(RegistrationMessage model)
        {

            var data_req = JsonConvert.SerializeObject(model);
            var content = new StringContent(data_req, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                //CHANGE ADDRESS FOR CLOUD
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

        public async Task<AuthResponse> Login(AuthMessage model)
        {
            var data_req = JsonConvert.SerializeObject(model);
            var content = new StringContent(data_req, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                //CHANGE ADDRESS FOR CLOUD
                client.BaseAddress = new Uri("https://rvt-administratorapi.conveyor.cloud/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync("api/Identity/Auth", content);
                var regresp = new AuthResponse();

                try
                {
                    var data_resp = await response.Result.Content.ReadAsStringAsync();
                    regresp = JsonConvert.DeserializeObject<AuthResponse>(data_resp);

                }
                catch
                {
                    regresp.Message = "Error! Încercați mai tîrziu.";
                }

                return regresp;
            }
        }

        public async Task<VoteResponse> Vote(VoteMessage model)
        {
            var data_req = JsonConvert.SerializeObject(model);
            var content = new StringContent(data_req, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                //CHANGE ADDRESS FOR CLOUD
                client.BaseAddress = new Uri("https://rvt-administratorapi.conveyor.cloud/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync("api/Identity/Vote", content);
                var regresp = new VoteResponse();

                try
                {
                    var data_resp = await response.Result.Content.ReadAsStringAsync();
                    regresp = JsonConvert.DeserializeObject<VoteResponse>(data_resp);

                }
                catch
                {
                    regresp.Message = "Error! Încercați mai tîrziu.";
                }

                return regresp;
            }
        }
    }
}

