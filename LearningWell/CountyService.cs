using LearningWell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace LearningWell.Services
{
    public class CountyService
    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task<List<CountyInfo>> GetCountyInfo()
        {
            var countyResponse = await client.GetAsync("http://api.scb.se/OV0104/v1/doris/sv/ssd/START/ME/ME0104/ME0104D/ME0104T4");

            var countyInfo = new List<CountyInfo>();


            string countyResponseBody = countyResponse.Content.ReadAsStringAsync().Result;

            CountyData countyData = JsonConvert.DeserializeObject<CountyData>(countyResponseBody);


            foreach (var item in countyData.Variables)
            {
                if (item.Code == "Region")
                {
                    for (int i = 0; i < item.Values.Count; i++)
                    {

                        var county = new CountyInfo();
                        county.Id = item.Values[i];
                        county.Name = item.ValueTexts[i];

                        countyInfo.Add(county);
                    }
                }
            }
            return countyInfo;
        }
    }
}
