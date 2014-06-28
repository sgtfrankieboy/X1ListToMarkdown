using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X1ListToMarkdown.Models
{
	public class BetaItem
	{
		public string Title { get; set; }
		public SpecialDate StartDate { get; set; }
		public SpecialDate EndDate { get; set; }
		public string Type { get; set; }
		public string Link { get; set; }

		public BetaItem(DataRow row)
		{
			Title = row["Title"].ToString();
			StartDate = new SpecialDate(row["Start Date"].ToString());
			EndDate = new SpecialDate(row["End Date"].ToString());
			Type = row["Type"].ToString();
			Link = row["Link"].ToString();
		}
	}
}
