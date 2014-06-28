using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X1ListToMarkdown.Models
{
	public class DwGItem
	{
		public string Title { get; set; }
		public SpecialDate StartDate { get; set; }
		public SpecialDate EndDate { get; set; }
		public int PercentageOff { get; set; }
		public string StoreURL { get; set; }

		public DwGItem(DataRow row)
		{
			Title = row["Title"].ToString();
			StartDate = new SpecialDate(row["Start Date"].ToString());
			EndDate = new SpecialDate(row["End Date"].ToString());
			PercentageOff = int.Parse(row["% Off"].ToString());
			StoreURL = row["Xbox Store URL"].ToString();
		}
	}
}
