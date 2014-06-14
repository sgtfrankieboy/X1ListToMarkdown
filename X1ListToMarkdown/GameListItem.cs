using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X1ListToMarkdown
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
	}

	public class GwGItem
	{
		public string Title { get; set; }
		public DateTime Date { get; set; }
		public string XboxStoreURL { get; set; }
		public string Retail { get; set; }
	}
}
