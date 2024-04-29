using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tech_chapter_intern_challenge
{
    public interface IHolidayCalendar
    {
        bool IsHoliday(DateTime date);

        ICollection<DateTime> GetHolidays(DateTime startDate, DateTime endDate);
    }
}
