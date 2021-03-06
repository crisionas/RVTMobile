﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RVTMobileAPP.Interfaces;
using RVTMobileAPP.Models.Results;

namespace RVTMobileAPP.Services
{
    public static class ResultsServices
    {
        public static async Task<ResultsResponse> Results(string id)
        {
            return await Task.Run(() =>
            {

                var data_req = JsonConvert.SerializeObject(id);
                var content = new StringContent(data_req, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    //CHANGE ADDRESS FOR CLOUD
                    client.BaseAddress = new Uri("https://rvt-administratorapi.conveyor.cloud/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.PostAsync("api/Results/Results", content);
                    var regresp = new ResultsResponse();

                    try
                    {
                        var data_resp = response.Result.Content.ReadAsStringAsync();
                        regresp = JsonConvert.DeserializeObject<ResultsResponse>(data_resp.Result);

                    }
                    catch
                    {

                    }

                    return regresp;
                }
            });
        }
    }
}
