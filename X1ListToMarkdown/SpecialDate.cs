using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X1ListToMarkdown
{
	public class SpecialDate
	{
		public int? Day { get; set; }
		public int? Month { get; set; }
		public int? Quarter { get; set; }
		public int? Year { get; set; }

		public bool IsQuarter
		{
			get
			{
				return Quarter.HasValue;
			}
		}

		public DateTime GetDateTime()
		{
			// Return the Date. If there are no values return December 31rd 9999
			if (!IsQuarter)
			{
				return new DateTime(Year.HasValue ? Year.Value : 9999, Month.HasValue ? Month.Value : 12, Day.HasValue ? Day.Value : DateTime.DaysInMonth(Year.HasValue ? Year.Value : 9999, Month.HasValue ? Month.Value : 12));
			}
			else
			{
				int month = 3;
				if (Quarter == 2)
					month = 6;
				else if (Quarter == 3)
					month = 9;
				else if (Quarter == 4)
					month = 12;

				return new DateTime(Year.HasValue ? Year.Value : 9999, month, Day.HasValue ? Day.Value : DateTime.DaysInMonth(Year.HasValue ? Year.Value : 9999, month));
			}
		}

		public SpecialDate(string date)
		{
			var items = date.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

			if (items.Count() == 3)
			{
				Day = int.Parse(items[0]);
				Month = int.Parse(items[1]);
				Year = int.Parse(items[2]);
			}
			else if (items.Count() == 2)
			{
				if (items[0].StartsWith("q", StringComparison.OrdinalIgnoreCase))
				{
					Quarter = int.Parse(items[0].Substring(1));
					Year = int.Parse(items[1]);
				}
				else
				{
					Month = int.Parse(items[0]);
					Year = int.Parse(items[1]);
				}
			}
			else if (items.Count() == 1)
			{
				Year = int.Parse(items[0]);
			}
		}

		public string HeaderName()
		{
			if (IsQuarter)
			{
				switch (Quarter)
				{
					case 1:
						return "1st Quarter";
					case 2:
						return "2nd Quarter";
					case 3:
						return "3rd Quarter";
					case 4:
						return "4th Quarter";
				}
			}
			else if (Month.HasValue)
			{
				return new DateTime(1, Month.Value, 1).ToString("MMMM");
			}
			return "To Be Announced";
		}

		public int GroupByFormatYear()
		{
			if (Year.HasValue)
				return Year.Value;
			return 9999;
		}

		public int GroupByFormatMonth()
		{
			if (IsQuarter)
			{
				switch (Quarter)
				{
					case 1:
						return 4;
					case 2:
						return 8;
					case 3:
						return 12;
					case 4:
						return 16;
				}
			}
			else if (Month.HasValue)
			{
				switch (Month.Value)
				{
					case 1:
						return 1;
					case 2:
						return 2;
					case 3:
						return 3;
					case 4:
						return 5;
					case 5:
						return 6;
					case 6:
						return 7;
					case 7:
						return 9;
					case 8:
						return 10;
					case 9:
						return 11;
					case 10:
						return 13;
					case 11:
						return 14;
					case 12:
						return 15;
				}
			}
			else if (Year.HasValue)
				return 17;
			return 18;
		}

		public override string ToString()
		{
			var str = "";
			if (!IsQuarter)
			{

				if (Day.HasValue)
				{
					string day = Day.Value.ToString("D2");
					if (day == "00")
						day = "--";

					str += day;
				}

				if (Month.HasValue)
				{
					if (string.IsNullOrWhiteSpace(str))
						str = string.Format("{0:MMM} --", GetDateTime());
					else
						str = string.Format("{0:MMM} ", GetDateTime()) + str;
				}

				if (string.IsNullOrWhiteSpace(str))
				{
					if (Year.HasValue)
					{
						str += string.Format("TBA {0}", Year.Value);
					}
				}
			} else {
				str = string.Format("Q{0} {1}", Quarter, Year);
			}

			if (string.IsNullOrWhiteSpace(str))
				str = "TBA";

			return str;
		}
	}
}
