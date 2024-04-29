using Microsoft.Extensions.Configuration;

namespace tech_chapter_intern_challenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HolidayCalendar holidayCalendar = new();

            Console.WriteLine(holidayCalendar.IsHoliday(DateTime.Now));

            var test = holidayCalendar.GetHolidays(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-04-30"));

            for (int i = 0; i < test.Count; i++)
            {
                Console.WriteLine(test.ElementAt(i));
            }
        }
    }
}
