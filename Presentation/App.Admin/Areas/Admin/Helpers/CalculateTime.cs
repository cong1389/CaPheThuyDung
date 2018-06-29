using System;

namespace App.Admin.Helpers
{
	public class CalculateTime
	{
		public int Years;

		public int Months;

		public int Days;

		public CalculateTime(DateTime currentTime)
		{
			this.Count(currentTime);
		}

		public CalculateTime(DateTime currentTime, DateTime Cday)
		{
			this.Count(currentTime, Cday);
		}

		public CalculateTime Count(DateTime currentTime)
		{
			return this.Count(currentTime, DateTime.Today);
		}

		public CalculateTime Count(DateTime currentTime, DateTime Cday)
		{
			if (Cday.Year - currentTime.Year <= 0 && (Cday.Year - currentTime.Year != 0 || currentTime.Month >= Cday.Month && (currentTime.Month != Cday.Month || currentTime.Day > Cday.Day)))
			{
				return null;
			}
			int num = DateTime.DaysInMonth(currentTime.Year, currentTime.Month);
			int day = Cday.Day + (num - currentTime.Day);
			if (Cday.Month > currentTime.Month)
			{
				this.Years = Cday.Year - currentTime.Year;
				this.Months = Cday.Month - (currentTime.Month + 1) + Math.Abs(day / num);
				this.Days = (day % num + num) % num;
			}
			else if (Cday.Month != currentTime.Month)
			{
				this.Years = Cday.Year - 1 - currentTime.Year;
				this.Months = Cday.Month + (11 - currentTime.Month) + Math.Abs(day / num);
				this.Days = (day % num + num) % num;
			}
			else if (Cday.Day < currentTime.Day)
			{
				this.Years = Cday.Year - 1 - currentTime.Year;
				this.Months = 11;
				this.Days = DateTime.DaysInMonth(currentTime.Year, currentTime.Month) - (currentTime.Day - Cday.Day);
			}
			else
			{
				this.Years = Cday.Year - currentTime.Year;
				this.Months = 0;
				this.Days = Cday.Day - currentTime.Day;
			}
			return this;
		}
	}
}