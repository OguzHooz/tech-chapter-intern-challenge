using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using tech_chapter_intern_challenge.Models;

namespace tech_chapter_intern_challenge
{
    public class HolidayCalendar : IHolidayCalendar
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

        private string _bearerToken;

        public bool IsHoliday(DateTime date)
        {
            _bearerToken = config["SALLING_API"];
            string holidayUrl = $"https://api.sallinggroup.com/v1/holidays/is-holiday?date={date:yyyy-MM-dd}";

            HttpClient client = new();

            var request = new HttpRequestMessage(HttpMethod.Get, holidayUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
            
            var response = client.SendAsync(request);
            
            var responseData = response.Result.Content.ReadAsStringAsync();
            return bool.Parse(responseData.Result);
        }


        public ICollection<DateTime> GetHolidays(DateTime startDate, DateTime endDate)
        {
            _bearerToken = config["SALLING_API"];
            string holdidayPeriodUrl = $"https://api.sallinggroup.com/v1/holidays?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}";

            HttpClient client = new();

            var request = new HttpRequestMessage(HttpMethod.Get, holdidayPeriodUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

            var response = client.SendAsync(request);
            var responseData = response.Result.Content.ReadAsStringAsync();

            var holiday = JsonSerializer.Deserialize<ICollection<HolidayModel>>(responseData.Result);
            

            ICollection<DateTime> holidaysList = [];

            foreach (var item in holiday)
            {
                if (item.nationalHoliday)
                {
                    holidaysList.Add(Convert.ToDateTime(item.date));
                }
            }

            return holidaysList;
        }
    }
}
