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
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
		}

		private void btnCsvPath_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Excel Files|*.xlsx";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				txtPath.Text = ofd.FileName;
			}
		}

		

		private void btnConvertToWiki_Click(object sender, EventArgs e)
		{
			AllowInteraction(false);
			ExcelReader reader = new ExcelReader(txtPath.Text);

			var games = reader.GetGames();
			var betas = reader.GetBetas();
			var gwg = reader.GetGwG();
			var dwg = reader.GetDwG();
            var gwg360 = reader.GetGwG360();
            DateTime currentDate = DateTime.UtcNow;



			StringBuilder builder = new StringBuilder();


			/*********************\
			|* Deals of the Week *|
			\*********************/

			var cdwgGames = dwg.Where(_ => _.StartDate.GetDateTime() <= DateTime.UtcNow && _.EndDate.GetDateTime() >= DateTime.Today);

			if (cdwgGames.Any())
			{
				builder.AppendLine("## Deals of the Week");
                builder.AppendLine("| Name | Start Date | End Date | % Off |");
                builder.AppendLine("|:- |:-:|:-:|:-:|");

				foreach (var cdwgGame in cdwgGames)
				{
					builder.AppendLine(string.Format(
                            "| {0} | {1} | {2} | {3} |",
							Formatter.MDStoreLink(cdwgGame.Title, cdwgGame.StoreURL),
							cdwgGame.StartDate.ToString(),
							cdwgGame.EndDate.ToString(),
                            cdwgGame.PercentageOff.ToString()
							));
				}
			}

			/*******************\
			|* GAMES WITH GOLD *|
			\*******************/

			builder.AppendLine("## Games with Gold");
			builder.AppendLine("| Name | Date | Retail |");
			builder.AppendLine("|:- |:-:|:-:|");

			var cgwgGames = gwg
				.GroupBy(_ => _.Date.ToString("yyyy MMMM"));
			foreach(var cgwgGame in cgwgGames.FirstOrDefault()) {
				builder.AppendLine(string.Format(
						"| {0} | {1:MMM dd} | {2} |",
						Formatter.MDStoreLink(cgwgGame.Title, cgwgGame.StoreURL),
						cgwgGame.Date,
						cgwgGame.Retail
						));
			}

			/**************\
			|* GAME BETAS *|
			\**************/

			var betaGames = betas
				.Where(_ => _.EndDate.GetDateTime() >= DateTime.UtcNow || _.EndDate.Year == 9999);

			if (betaGames.Any())
			{
				builder.AppendLine("## Game Betas");
				builder.AppendLine("| Name | Type | Start Date | End Date |");
				builder.AppendLine("|:-|:-|:-:|:-:|");

				foreach (var betaGame in betaGames.OrderBy(_ => _.StartDate.GetDateTime()))
				{
					builder.AppendLine(string.Format(
						"| {0} | {1} | {2} | {3}",
						Formatter.MDLink(betaGame.Title, betaGame.Link),
						betaGame.Type,
						betaGame.StartDate.ToString(),
						betaGame.EndDate.ToString()));
				}
			}

			/******************\
			|* RELEASED GAMES *|
			\******************/


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
				builder.AppendLine("|:- |:- |:-:|:-:|:-:|:-:|:-:|:-:|");

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
							Formatter.MDStoreLink(game.Title, game.XboxStoreURL),
							Formatter.MDSubreddit(game.Subreddit),
							game.Developers,
							game.Retail,
							game.Exclusive,
							game.Kinect,
							game.ReleaseDateUS.ToString(),
							game.ReleaseDateEU.ToString(),
							Formatter.MDMetacritic(game.MetacriticURL, game.MetacriticScore)
						));
					}
				}
			}

			/******************\
			|* UPCOMING GAMES *|
			\******************/

			var upcomingGameYears = games
					.Where(_ => _.ReleaseDateUS.GetDateTime() > currentDate && _.ReleaseDateUS.GetDateTime().Year < 9999)
					.GroupBy(_ => _.ReleaseDateUS.GroupByFormatYear());

			builder.AppendLine("# Upcoming Games");

			
			foreach (var upcomingGameYear in upcomingGameYears.OrderBy(_ => _.Key))
			{
				var yearInt = upcomingGameYear.FirstOrDefault().ReleaseDateUS.Year;
				builder.AppendLine(string.Format("## {0}", yearInt == null ? "To Be Announced" : yearInt.Value.ToString()));
				builder.AppendLine("| Title | Developer(s) | Exclusive | NA Date | EU Date | Trailer |");
				builder.AppendLine("|:- |:- |:-:|:-:|:-:|:-:|:-:|");


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
							Formatter.MDSubreddit(game.Subreddit),
							game.Developers,
							game.Exclusive,
							game.ReleaseDateUS.ToString(),
							game.ReleaseDateEU.ToString(),
							Formatter.MDTrailer(game.Trailers)
						));
					}
				}
			}

			/**************************\
			|* GAMES TO BE ANNOUNCED  *|
			\**************************/

			builder.AppendLine("## TBA");
			builder.AppendLine("| Title | Developer(s) | Exclusive |  Trailer |");
			builder.AppendLine("|:- |:- |:-:|:-:|");

			var tbaGames = games
					.Where(_ => _.ReleaseDateUS.GetDateTime() > currentDate && _.ReleaseDateUS.GetDateTime().Year == 9999);
			foreach (var game in tbaGames.OrderByDescending(_ => _.ReleaseDateUS.GetDateTime().Day).ThenBy(_ => _.Title))
			{
				// Build the game row.
				builder.AppendLine(string.Format(
					"| {0} {1} | {2} | {3} | {4} |",
					game.Title,
					Formatter.MDSubreddit(game.Subreddit),
					game.Developers,
					game.Exclusive,
					Formatter.MDTrailer(game.Trailers)
				));
			}

			/****************************\
			|* PREVIOUS GAMES WITH GOLD *|
			\****************************/

			builder.AppendLine("# Previous Games with Gold");

			var gwgGameYears = gwg
					.GroupBy(_ => _.Date.Year);
            foreach (var gwgGameYear in gwgGameYears.OrderByDescending(_ => _.Key))
			{
				int year = gwgGameYear.FirstOrDefault().Date.Year;

				builder.AppendLine("## " + year);
				builder.AppendLine("| Name | Date | Retail |");
				builder.AppendLine("|:- |:-:|:-:|");


				var gwgGameMonths = gwgGameYear
					.GroupBy(_ => _.Date.Month);
				foreach (var gwgGameMonth in gwgGameMonths.OrderByDescending(_ => _.Key))
				{
					string month = gwgGameMonth.FirstOrDefault().Date.ToString("MMMM");

                    builder.AppendLine(string.Format("| ***{0}*** | ~~-~~ | ~~-~~ | ~~-~~ |", month));

                    foreach (var game in gwgGameMonth.OrderByDescending(_ => _.Date).ThenBy(_ => _.Title))
					{
						builder.AppendLine(string.Format(
							"| {0} | {1:MMM dd} | {2} |",
							Formatter.MDStoreLink(game.Title, game.StoreURL),
							game.Date,
							game.Retail
							));
					}
				}

			}


			/******************************\
			|* PREVIOUS DEALS OF THE WEEK *|
			\******************************/

			builder.AppendLine("# Previous Deals of the Week");

			var dwgGameYears = dwg
					.GroupBy(_ => _.StartDate.Year);
			foreach (var dwgGameYear in dwgGameYears.OrderByDescending(_ => _.Key))
			{
				int year = dwgGameYear.FirstOrDefault().StartDate.Year.Value;

				builder.AppendLine("## " + year);
				builder.AppendLine("| Name | Start Date | End Date | % Off |");
                builder.AppendLine("|:- |:-:|:-:|:-:|");


				var dwgGameMonths = dwgGameYear
					.GroupBy(_ => _.StartDate.Month);
				foreach (var dwgGameMonth in dwgGameMonths.OrderByDescending(_ => _.Key))
				{
					string month = dwgGameMonth.FirstOrDefault().StartDate.HeaderName();

                    builder.AppendLine(string.Format("| ***{0}*** | ~~-~~ | ~~-~~ | ~~-~~ |", month));

                    foreach (var game in dwgGameMonth.OrderByDescending(_ => _.StartDate.GetDateTime()).ThenBy(_ => _.Title))
					{
						builder.AppendLine(string.Format(
                            "| {0} | {1} | {2} | {3} |",
							Formatter.MDStoreLink(game.Title, game.StoreURL),
							game.StartDate.ToString(),
							game.EndDate.ToString(),
                            game.PercentageOff.ToString()
						));
					}
				}

            /***********************\
			|* 360 GAMES WITH GOLD *|
			\***********************/

                builder.AppendLine("***");
                builder.AppendLine("# Xbox 360 Games with Gold List");
                builder.AppendLine("| Name | Date | Retail |");
                builder.AppendLine("|:- |:-:|:-:|");

                var cgwg360Games = gwg360
                .GroupBy(_ => _.Date.ToString("yyyy MMMM"));
                foreach (var cgwg360Game in cgwg360Games.FirstOrDefault())
                {
                    builder.AppendLine(string.Format(
                            "| {0} | {1:MMM dd} | {2} |",
                            Formatter.MDStoreLink(cgwg360Game.Title, cgwg360Game.StoreURL),
                            cgwg360Game.Date,
                            cgwg360Game.Retail
                            ));
                }

            /********************************\
			|* 360 PREVIOUS GAMES WITH GOLD *|
			\********************************/

                builder.AppendLine("# Previous Xbox 360 Games with Gold");

                var gwg360GameYears = gwg360
                        .GroupBy(_ => _.Date.Year);
                foreach (var gwg360GameYear in gwg360GameYears.OrderByDescending(_ => _.Key))
                {
                    int year360 = gwg360GameYear.FirstOrDefault().Date.Year;

                    builder.AppendLine("### " + year360);
                    builder.AppendLine("| Name | Date | Retail |");
                    builder.AppendLine("|:- |:-:|:-:|");


                    var gwg360GameMonths = gwg360GameYear
                       .GroupBy(_ => _.Date.Month);
                    foreach (var gwg360GameMonth in gwg360GameMonths.OrderByDescending(_ => _.Key))
                    {
                        string month = gwg360GameMonth.FirstOrDefault().Date.ToString("MMMM");

                        builder.AppendLine(string.Format("| ***{0}*** | ~~-~~ | ~~-~~ |", month));

                        foreach (var game in gwg360GameMonth.OrderByDescending(_ => _.Date).ThenBy(_ => _.Title))
                        {
                            builder.AppendLine(string.Format(
                                "| {0} | {1:MMM dd} | {2} |",
                                Formatter.MDStoreLink(game.Title, game.StoreURL),
                                game.Date,
                                game.Retail
                                ));
                        }
                    }

                }

                builder.AppendLine("# Games with Gold FAQ");
                builder.AppendLine("![](%%gwg%%)");
                builder.AppendLine("#### When are games given out?");
                builder.AppendLine("Games are given out on the 1st and 16th of each month.  If you don't 'purchase' them (for free) in that two week window, you will not be able to at a later time (i.e. they will be back at full price).");
                builder.AppendLine("#### What games are given out?");
                builder.AppendLine("The games range from retail to small titles.  Currently, two Xbox 360 games and one Xbox One game are given out per month.");
                builder.AppendLine("#### Do you lose the games when your Gold subscription ends?");
                builder.AppendLine("Yes (for Xbox One) and no (for Xbox 360).  On Xbox One, you lose access to Games with Gold games until you subscribe to gold, at which point you regain access to all previous titles you purchased for free.  On Xbox 360, every game you purchase from Games with Gold for free is there regardless of membership status.");
                builder.AppendLine("#### Is the service temporary?");
                builder.AppendLine("No.  Originally, Games with Gold was planned to end December 31st, 2013 but, after being well-received, the service [has become permanent.](http://news.xbox.com/2013/10/xbox-360-games-with-gold)");

			}
			

			txtResult.Text = builder.ToString();

			AllowInteraction(true);
		}


		private void AllowInteraction(bool status)
		{
			btnPath.Enabled = status;
			btnConvertToWiki.Enabled = status;
			btnConvertToSidebar.Enabled = status;
			btnClear.Enabled = status;
			btnCopy.Enabled = status;
			txtResult.Enabled = status;
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(txtResult.Text, TextDataFormat.UnicodeText);
		}

		private void txtResult_TextChanged(object sender, EventArgs e)
		{
			btnCopy.Enabled = !string.IsNullOrWhiteSpace(txtResult.Text);
			btnClear.Enabled = !string.IsNullOrWhiteSpace(txtResult.Text);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			txtResult.Clear();
		}

		private void txtPath_TextChanged(object sender, EventArgs e)
		{
			btnConvertToWiki.Enabled = !string.IsNullOrWhiteSpace(txtPath.Text);
			btnConvertToSidebar.Enabled = !string.IsNullOrWhiteSpace(txtPath.Text);
		}
	}
}
