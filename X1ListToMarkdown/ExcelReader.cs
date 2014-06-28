using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X1ListToMarkdown
{
	public class ExcelReader
	{
		private DataSet dataSet = null;

		public string Path { get; set; }


		public ExcelReader(string path)
		{
			this.Path = path;
		}

		/// <summary>
		/// Open the Excel file and returns the DataSet.
		/// </summary>
		/// <returns></returns>
		private DataRowCollection GetExcelTableRows(int index)
		{
			if (dataSet != null)
				return dataSet.Tables[index].Rows;

			using (FileStream stream = File.OpenRead(Path))
			{
				IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

				excelReader.IsFirstRowAsColumnNames = true;

				dataSet = excelReader.AsDataSet();
				return dataSet.Tables[index].Rows;
			}
		}

		public IEnumerable<Models.GameItem> GetGames()
		{
			foreach (DataRow row in GetExcelTableRows(0))
			{
				if (string.IsNullOrWhiteSpace(row["Title"].ToString()))
					continue;


				yield return new Models.GameItem(row);
			}
		}

		public IEnumerable<Models.GwGItem> GetGwG()
		{
			foreach (DataRow row in GetExcelTableRows(1))
			{
				if (string.IsNullOrWhiteSpace(row["Title"].ToString()))
					continue;


				yield return new Models.GwGItem(row);
			}
		}

		public IEnumerable<Models.DwGItem> GetDwG()
		{
			foreach (DataRow row in GetExcelTableRows(3))
			{
				if (string.IsNullOrWhiteSpace(row["Title"].ToString()))
					continue;

				yield return new Models.DwGItem(row);
			}
		}

		public IEnumerable<Models.BetaItem> GetBetas()
		{
			foreach (DataRow row in GetExcelTableRows(4))
			{
				if (string.IsNullOrWhiteSpace(row["Title"].ToString()))
					continue;

				yield return new Models.BetaItem(row);
			}
		}
	}
}
