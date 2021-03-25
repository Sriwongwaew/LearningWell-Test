using LearningWell.Models;
using LearningWell.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LearningWell.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParticipationStatisticController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpGet]
        public async Task<List<CountyParticipationStatistic>> GetHighestCountyParticipantByYear()
        {

            var jsonContent = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Controllers/election.json");
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://api.scb.se/OV0104/v1/doris/sv/ssd/START/ME/ME0104/ME0104D/ME0104T4", content);

            var countyParticipationStatistic = new List<CountyParticipationStatistic>();
            string responseBody = await response.Content.ReadAsStringAsync();

            var countyInfo = await CountyService.GetCountyInfo();

            ParticipationStatistics participationStatistic = JsonConvert.DeserializeObject<ParticipationStatistics>(responseBody);

            foreach (var stat in participationStatistic.Data)
            {

                var countyParticipation = new CountyParticipationStatistic()
                {
                    Id = stat.Key[0],
                    Year = int.Parse(stat.Key[1])
                };

                decimal.TryParse(stat.Values[0], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value);
                countyParticipation.Value = value;
                countyParticipation.Name = countyInfo.Where(x => x.Id == countyParticipation.Id).Select(i => i.Name).FirstOrDefault();

                countyParticipationStatistic.Add(countyParticipation);
            }

            var result = countyParticipationStatistic.GroupBy(i => i.Year)
           .Select(x => x.OrderByDescending(y => y.Value).First()).ToList();

            return result;
        }
    }
}
