using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X1ListToMarkdown
{
	public static class Formatter
	{
		/// <summary>
		/// Generate the Markdown for a generic link.
		/// </summary>
		/// <param name="text">Title of the URL.</param>
		/// <param name="url">A Generic URL.</param>
		/// <returns></returns>
		public static string MDLink(string text, string url)
		{
			return string.Format("[{0}]({1})", text, url);
		}

		/// <summary>
		/// Generate the Markdown for a Xbox Store link.
		/// </summary>
		/// <param name="text">Title of the URL.</param>
		/// <param name="url">URL Pointing to the Xbox Store.</param>
		/// <returns></returns>
		public static string MDStoreLink(string text, string url)
		{
			return string.Format("[{0}]({1} \"Xbox Store\")", text, url);
		}

		/// <summary>
		/// Generate the Markdown for a subreddit link.
		/// </summary>
		/// <param name="subreddit">Name of the subreddit.</param>
		/// <returns></returns>
		public static string MDSubreddit(string subreddit)
		{
			if (string.IsNullOrWhiteSpace(subreddit))
				return string.Empty;
			return string.Format("[^(subreddit)]({0} \"Subreddit\")", subreddit);
		}

		/// <summary>
		/// Generate the Markdown for a Metacritic Link.
		/// </summary>
		/// <param name="url">URL to the Metacritic website.</param>
		/// <param name="score">Critic score from Metacritic.</param>
		/// <returns></returns>
		public static string MDMetacritic(string url, string score)
		{
			if (string.IsNullOrWhiteSpace(score))
				return string.Format("[tba](#t)");

			int scoreInt = int.Parse(score.Replace("*", ""));
			string color = "t";
			if (scoreInt > 74)
				color = "g";
			else if (scoreInt > 49)
				color = "y";
			else
				color = "r";

			return string.Format("[{0}]({1}#{2})", score, url, color);
		}

		/// <summary>
		/// Generate the Markdown for a YouTube Trailer Link.
		/// </summary>
		/// <param name="url">URL to the YouTube Trailer video.</param>
		/// <returns></returns>
		public static string MDTrailer(string url)
		{
			if (string.IsNullOrWhiteSpace(url))
				return string.Empty;
			return string.Format("[^Trailer]({0} \"Trailer\")", url);
		}
	}
}
