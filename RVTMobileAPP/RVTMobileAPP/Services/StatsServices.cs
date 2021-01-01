using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RVTMobileAPP.Interfaces;
using RVTMobileAPP.Models.Registration;
using RVTMobileAPP.Models.Results;

namespace RVTMobileAPP.Services
{
    public class StatsServices
    {
        public async Task<StatisticsResponse> Statistics(string id)
        {
            var data_req = JsonConvert.SerializeObject(id);
            var content = new StringContent(data_req, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                //CHANGE ADDRESS FOR CLOUD
                client.BaseAddress = new Uri("https://rvt-administratorapi.conveyor.cloud/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync("api/Results/Statistics", content);
                var regresp = new StatisticsResponse();

                try
                {
                    var data_resp = await response.Result.Content.ReadAsStringAsync();
                    regresp = JsonConvert.DeserializeObject<StatisticsResponse>(data_resp);

                }
                catch
                {

                }

                return regresp;
            }
        }
    }
}

