using System;
using System.Collections.Generic;
using System.Linq;

namespace RunnerBLL.Extension
{
	public static class DateTimeExtension
	{
		public static DateTime StartOfWeek(this DateTime dateTime)
		{
			int dayOfWeek = (int)dateTime.DayOfWeek;
			dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
			DateTime startOfWeek = dateTime.AddDays(1 - dayOfWeek);
			return startOfWeek;
		}

		public static bool Intersects(this DateTime startDate, DateTime endDate, DateTime intersectingStartDate, DateTime intersectingEndDate)
		{
			return (intersectingEndDate >= startDate && intersectingStartDate <= endDate);
		}

		public static IEnumerable<DateTime> GetDateRangeTo(this DateTime self, DateTime toDate)
		{
			IEnumerable<int> range = Enumerable.Range(0, new TimeSpan(toDate.Ticks - self.Ticks).Days);

			return from p in range
				   select self.Date.AddDays(p);
		}

		public static bool IsWeekend(this DateTime value)
		{
			return (value.DayOfWeek == DayOfWeek.Sunday || value.DayOfWeek == DayOfWeek.Saturday);
		}

		public static int Age(this DateTime dateOfBirth)
		{
			if (DateTime.Today.Month < dateOfBirth.Month ||
			DateTime.Today.Month == dateOfBirth.Month &&
			 DateTime.Today.Day < dateOfBirth.Day)
			{
				return DateTime.Today.Year - dateOfBirth.Year - 1;
			}
			else
			{
				return DateTime.Today.Year - dateOfBirth.Year;
			}
		}

		public static bool IsBetween(this DateTime dt, DateTime startDate, DateTime endDate, bool compareTime = false)
		{
			return compareTime ?
			   dt >= startDate && dt <= endDate :
			   dt.Date >= startDate.Date && dt.Date <= endDate.Date;
		}

		public static bool IsInRange(this DateTime currentDate, DateTime beginDate, DateTime endDate)
		{
			return (currentDate > beginDate && currentDate < endDate);
		}
	}
}
