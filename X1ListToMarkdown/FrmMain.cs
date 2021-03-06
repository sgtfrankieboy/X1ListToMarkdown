﻿using Excel;
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


			/**************\
			|* GAME BETAS *|
			\**************/

			var betaGames = betas
				.Where(_ => _.EndDate.GetDateTime() >= DateTime.UtcNow || _.EndDate.Year == 9999);

			if (betaGames.Any())
			{
				builder.AppendLine("# Game Betas");
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

			

			txtResult.Text = builder.ToString();

			AllowInteraction(true);
		}

		private void btnConvertToDwGWiki_Click(object sender, EventArgs e)
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

			builder.AppendLine("# Deals of the Week");

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
			}



			txtResult.Text = builder.ToString();

			AllowInteraction(true);
		}

		private void btnConvertToGwGWiki_Click(object sender, EventArgs e)
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

			/*******************\
			|* GAMES WITH GOLD *|
			\*******************/

			builder.AppendLine("# Xbox One");

			var gwgGameYears = gwg
					.GroupBy(_ => _.Date.Year);
			foreach (var gwgGameYear in gwgGameYears.OrderByDescending(_ => _.Key))
			{
				int year = gwgGameYear.FirstOrDefault().Date.Year.Value;

				builder.AppendLine("## " + year);
				builder.AppendLine("| Name | Date | Retail |");
				builder.AppendLine("|:- |:-:|:-:|");


				var gwgGameMonths = gwgGameYear
					.GroupBy(_ => _.Date.Month);
				foreach (var gwgGameMonth in gwgGameMonths.OrderByDescending(_ => _.Key))
				{
					string month = gwgGameMonth.FirstOrDefault().Date.HeaderName();

					builder.AppendLine(string.Format("| ***{0}*** | ~~-~~ | ~~-~~ | ~~-~~ |", month));

					foreach (var game in gwgGameMonth.OrderByDescending(_ => _.Date.GetDateTime()).ThenBy(_ => _.Title))
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

			/***********************\
			|* 360 GAMES WITH GOLD *|
			\***********************/

			builder.AppendLine("# Xbox 360");

			var gwg360GameYears = gwg360
					.GroupBy(_ => _.Date.Year);
			foreach (var gwg360GameYear in gwg360GameYears.OrderByDescending(_ => _.Key))
			{
				int year360 = gwg360GameYear.FirstOrDefault().Date.Year.Value;

				builder.AppendLine("### " + year360);
				builder.AppendLine("| Name | Date | Retail |");
				builder.AppendLine("|:- |:-:|:-:|");


				var gwg360GameMonths = gwg360GameYear
				   .GroupBy(_ => _.Date.Month);
				foreach (var gwg360GameMonth in gwg360GameMonths.OrderByDescending(_ => _.Key))
				{
					string month = gwg360GameMonth.FirstOrDefault().Date.HeaderName();

					builder.AppendLine(string.Format("| ***{0}*** | ~~-~~ | ~~-~~ |", month));

					foreach (var game in gwg360GameMonth.OrderByDescending(_ => _.Date.GetDateTime()).ThenBy(_ => _.Title))
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


			txtResult.Text = builder.ToString();

			AllowInteraction(true);
		}

		private void AllowInteraction(bool status)
		{
			btnPath.Enabled = status;
			btnConvertToGamesWiki.Enabled = status;
			btnConvertToSidebar.Enabled = status;
			btnConvertToDwGWiki.Enabled = status;
			btnConvertToGwGWiki.Enabled = status;
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
			btnConvertToGamesWiki.Enabled = !string.IsNullOrWhiteSpace(txtPath.Text);
			btnConvertToSidebar.Enabled = !string.IsNullOrWhiteSpace(txtPath.Text);
			btnConvertToGwGWiki.Enabled = !string.IsNullOrWhiteSpace(txtPath.Text);
			btnConvertToDwGWiki.Enabled = !string.IsNullOrWhiteSpace(txtPath.Text);
		}

		
	}
}
