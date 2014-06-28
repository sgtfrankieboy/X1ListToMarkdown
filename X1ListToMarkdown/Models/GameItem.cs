using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X1ListToMarkdown.Models
{
	public class GameItem
	{
		public string Title { get; set; }
		public string Developers { get; set; }
		public string Subreddit { get; set; }
		public string Exclusive { get; set; }
		public string Kinect { get; set; }
		public string Retail { get; set; }
		public SpecialDate ReleaseDateEU { get; set; }
		public SpecialDate ReleaseDateUS { get; set; }
		public string XboxStoreURL { get; set; }

		public string MetacriticScore { get; set; }
		public string MetacriticURL { get; set; }

		public string Trailers { get; set; }

		public GameItem(DataRow row)
		{
			Title = row["Title"].ToString();
			Developers = row["Developer(s)"].ToString();
			Subreddit = row["Subreddit"].ToString();
			Exclusive = row["Exclusive"].ToString();
			Kinect = row["Kinect"].ToString();
			Retail = row["Retail"].ToString();
			ReleaseDateEU = new SpecialDate(row["Release Date EU"].ToString());
			ReleaseDateUS = new SpecialDate(row["Release Date US"].ToString());
			MetacriticScore = row["Metacritic Score"].ToString();
			XboxStoreURL = row["Xbox Store URL"].ToString();
			MetacriticURL = row["Metacritic Link"].ToString();
			Trailers = row["Trailers"].ToString();
		}
	}
}
