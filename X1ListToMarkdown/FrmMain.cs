using Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace X1ListToMarkdown
{
	public partial class FrmMain : Form
	{
		public FrmMain()
		{
			InitializeComponent();
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
		}

		private void btnCsvPath_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Excel Files|*.xlsx";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				txtCsvPath.Text = ofd.FileName;
			}
		}

		public IEnumerable<GameItem> GetGames(string path)
		{
			FileStream stream = File.OpenRead(path);

			IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

			excelReader.IsFirstRowAsColumnNames = true;
			DataSet result = excelReader.AsDataSet();
			stream.Close();

			foreach (DataRow row in result.Tables[0].Rows)
			{
				if (string.IsNullOrWhiteSpace(row["Title"].ToString()))
					continue;


				yield return new GameItem()
				{
					Title = row["Title"].ToString(),
					Developers = row["Developer(s)"].ToString(),
					Subreddit = row["Subreddit"].ToString(),
					Exclusive = row["Exclusive"].ToString(),
					Kinect = row["Kinect"].ToString(),
					Retail = row["Retail"].ToString(),
					ReleaseDateEU = new SpecialDate(row["Release Date EU"].ToString()),
					ReleaseDateUS = new SpecialDate(row["Release Date US"].ToString()),
					MetacriticScore = row["Metacritic Score"].ToString(),
					XboxStoreURL = row["Xbox Store URL"].ToString(),
					MetacriticURL = row["Metacritic Link"].ToString(),
					Trailers = row["Trailers"].ToString(),
				};
			}
		}

		public IEnumerable<GwGItem> GetGwG(string path)
		{
			FileStream stream = File.OpenRead(path);

			IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

			excelReader.IsFirstRowAsColumnNames = true;
			DataSet result = excelReader.AsDataSet();
			stream.Close();

			foreach (DataRow row in result.Tables[1].Rows)
			{
				if (string.IsNullOrWhiteSpace(row["Title"].ToString()))
					continue;


				yield return new GwGItem()
				{
					Title = row["Title"].ToString(),
					Date = DateTime.Parse(row["Date"].ToString()),
					Retail = row["Retail"].ToString(),
					XboxStoreURL = row["Xbox Store URL"].ToString()
				};
			}
		}

		private void btnConvert_Click(object sender, EventArgs e)
		{
			btnCsvPath.Enabled = false;
			var games = GetGames(txtCsvPath.Text);
			var gwg = GetGwG(txtCsvPath.Text);
			DateTime currentDate = DateTime.UtcNow;



			StringBuilder builder = new StringBuilder();


			builder.AppendLine("## Games with Gold");
			builder.AppendLine("| Name | Date | Retail |");
			builder.AppendLine("|:- |:- |:-:|");

			var cgwgGames = gwg
				.GroupBy(_ => _.Date.ToString("yyyy MMMM"));
			foreach(var cgwgGame in cgwgGames.FirstOrDefault()) {
				builder.AppendLine(string.Format(
						"| {0} | {1:MMM dd} | {2} |",
						MDStoreLink(cgwgGame.Title, cgwgGame.XboxStoreURL),
						cgwgGame.Date,
						cgwgGame.Retail
						));
			}


			// Group all the released games by year.
			var releasedGameYears = games
					.Where(_ => _.ReleaseDateUS.IsQuarter == false && _.ReleaseDateUS.GetDateTime() < currentDate)
					.GroupBy(_ => _.ReleaseDateUS.Year.GetValueOrDefault());

			builder.AppendLine("# Released Games");

			foreach (var releasedGameYear in releasedGameYears.OrderByDescending(_ => _.Key))
			{
				// Build the release table header.
				builder.AppendLine("## " + releasedGameYear.FirstOrDefault().ReleaseDateUS.Year.GetValueOrDefault());
				builder.AppendLine("| Title | Developer(s) | Retail | Exclusive | Kinect | NA Date | EU Date | Score |");
				builder.AppendLine("| - |:- |:-:|:-:|:-:|:-:|:-:|:-:|");

				// Group all the released games by month inside the year.
				var releasedGameMonths = releasedGameYear
					.GroupBy(_ => _.ReleaseDateUS.Month.GetValueOrDefault());
				foreach (var releasedGameMonth in releasedGameMonths.OrderByDescending(_ => _.Key))
				{
					// Build the month subheader.
					builder.AppendLine(string.Format(
						"| ***{0:MMMM}*** | ~~-~~  | ~~-~~ | ~~-~~ | ~~-~~ | ~~-~~ | ~~-~~ | ~~-~~ | ~~-~~ |",
						new DateTime(1, releasedGameMonth.FirstOrDefault().ReleaseDateUS.Month.GetValueOrDefault(), 1)));

					// Loop through all the games.
					foreach (var game in releasedGameMonth.OrderByDescending(_ => _.ReleaseDateUS.Day).ThenBy(_ => _.Title))
					{
						// Build the game row.
						builder.AppendLine(string.Format(
							"| {0} {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} |",
							MDStoreLink(game.Title, game.XboxStoreURL),
							MDSubreddit(game.Subreddit),
							game.Developers,
							game.Retail,
							game.Exclusive,
							game.Kinect,
							game.ReleaseDateUS.ToString(),
							game.ReleaseDateEU.ToString(),
							MDMetacritic(game.MetacriticURL, game.MetacriticScore)
						));
					}
				}
			}

			var upcomingGameYears = games
					.Where(_ => _.ReleaseDateUS.GetDateTime() > currentDate && _.ReleaseDateUS.GetDateTime().Year < 9999)
					.GroupBy(_ => _.ReleaseDateUS.GroupByFormatYear());

			builder.AppendLine("# Upcoming Games");

			
			foreach (var upcomingGameYear in upcomingGameYears.OrderBy(_ => _.Key))
			{
				var yearInt = upcomingGameYear.FirstOrDefault().ReleaseDateUS.Year;
				builder.AppendLine(string.Format("## {0}", yearInt == null ? "To Be Announced" : yearInt.Value.ToString()));
				builder.AppendLine("| Title | Developer(s) | Exclusive | NA Date | EU Date | Trailer |");
				builder.AppendLine("| - |:- |:-:|:-:|:-:|:-:|:-:|");


				var upcomingGameMonths = upcomingGameYear
					.GroupBy(_ => _.ReleaseDateUS.GroupByFormatMonth());
				foreach (var upcomingGameMonth in upcomingGameMonths.OrderBy(_ => _.Key))
				{
					var name = upcomingGameMonth.FirstOrDefault().ReleaseDateUS.HeaderName();

					builder.AppendLine(string.Format("| ***{0}*** | ~~-~~ | ~~-~~ | ~~-~~ | ~~-~~ | ~~-~~ |", name));

					foreach (var game in upcomingGameMonth.OrderBy(_ => _.ReleaseDateUS.GetDateTime().Day).ThenBy(_ => _.Title))
					{
						// Build the game row.
						builder.AppendLine(string.Format(
							"| {0} {1} | {2} | {3} | {4} | {5} | {6} |",
							game.Title,
							MDSubreddit(game.Subreddit),
							game.Developers,
							game.Exclusive,
							game.ReleaseDateUS.ToString(),
							game.ReleaseDateEU.ToString(),
							MDYouTube(game.Trailers)
						));
					}
				}
			}


			builder.AppendLine("## TBA");
			builder.AppendLine("| Title | Developer(s) | Exclusive |  Trailer |");
			builder.AppendLine("| - |:- |:-:|:-:|:-:|");

			var tbaGames = games
					.Where(_ => _.ReleaseDateUS.GetDateTime() > currentDate && _.ReleaseDateUS.GetDateTime().Year == 9999);
			foreach (var game in tbaGames.OrderByDescending(_ => _.ReleaseDateUS.GetDateTime().Day).ThenBy(_ => _.Title))
			{
				// Build the game row.
				builder.AppendLine(string.Format(
					"| {0} {1} | {2} | {3} | {4} |",
					game.Title,
					MDSubreddit(game.Subreddit),
					game.Developers,
					game.Exclusive,
					MDYouTube(game.Trailers)
				));
			}



			builder.AppendLine("# Previous GwG");

			var gwgGameYears = gwg
					.GroupBy(_ => _.Date.Year);
			foreach (var gwgGameYear in gwgGameYears.OrderBy(_ => _.Key))
			{
				int year = gwgGameYear.FirstOrDefault().Date.Year;

				builder.AppendLine("## " + year);
				builder.AppendLine("| Name | Date | Retail |");
				builder.AppendLine("|:- |:- |:-:|");


				var gwgGameMonths = gwgGameYear
					.GroupBy(_ => _.Date.Month);
				foreach (var gwgGameMonth in gwgGameMonths.OrderBy(_ => _.Key))
				{
					string month = gwgGameMonth.FirstOrDefault().Date.ToString("MMMM");

					builder.AppendLine(string.Format("| ***{0}*** | ~~-~~ | ~~-~~ |", month));

					foreach (var game in gwgGameMonth.OrderBy(_ => _.Date).ThenBy(_ => _.Title))
					{
						builder.AppendLine(string.Format(
							"| {0} | {1:MMM dd} | {2} |",
							MDStoreLink(game.Title, game.XboxStoreURL),
							game.Date,
							game.Retail
							));
					}
				}

			}
			

			txtResult.Text = builder.ToString();

			btnCsvPath.Enabled = true;
		}

		/// <summary>
		/// Generate the Markdown for a generic link.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="url"></param>
		/// <returns></returns>
		public string MDStoreLink(string text, string url)
		{
			return string.Format("[{0}]({1} \"Xbox Store\")", text, url);
		}

		/// <summary>
		/// Generate the Markdown for a subreddit link.
		/// </summary>
		/// <param name="subreddit"></param>
		/// <returns></returns>
		public string MDSubreddit(string subreddit)
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
		public string MDMetacritic(string url, string score)
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
		/// Generate the Markdown for a YouTube Link.
		/// </summary>
		/// <param name="url">URL to the YouTube video.</param>
		/// <returns></returns>
		public string MDYouTube(string url)
		{
			if (string.IsNullOrWhiteSpace(url))
				return string.Empty;
			return string.Format("[^Trailer]({0} \"Trailer\")", url);
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(txtResult.Text, TextDataFormat.UnicodeText);
		}
	}
}
