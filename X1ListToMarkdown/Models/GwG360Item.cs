using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X1ListToMarkdown.Models
{
	public class GwG360Item
	{
		public string Title { get; set; }
		public SpecialDate Date { get; set; }
		public string StoreURL { get; set; }
		public string Retail { get; set; }


		public GwG360Item(DataRow row)
		{
			Title = row["Title"].ToString();
			Date = new SpecialDate(row["Date"].ToString());
			Retail = row["Retail"].ToString();
			StoreURL = row["Xbox Store URL"].ToString();
		}
	}
}
