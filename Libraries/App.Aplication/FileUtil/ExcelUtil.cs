using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace App.Aplication.FileUtil
{
    public static class ExcelUtil
	{
		public static void ListToExcel<T>(List<T> query)
		{
			using (ExcelPackage excelPackage = new ExcelPackage())
			{
				string dataFormat = DateTime.Now.ToString("dd-MM-yyyy");

				ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(dataFormat);
				PropertyInfo[] properties = typeof(T).GetProperties();
				for (int i = 0; i < properties.Length; i++)
				{
					object[] customAttributes = properties[i].GetCustomAttributes(typeof(DisplayAttribute), true);
					if (customAttributes.Length == 0)
					{
						worksheet.Cells[1, i + 1].Value = properties[i].Name;
					}
					else
					{
						string name = ((DisplayAttribute)customAttributes[0]).Name;
						worksheet.Cells[1, i + 1].Value = name;
					}
				}

				if (query.IsAny())
				{
					worksheet.Cells["A2"].LoadFromCollection(query);
				}

				using (ExcelRange item = worksheet.Cells["A1:BZ1"])
				{
					item.Style.Font.Bold = true;
					item.Style.Fill.PatternType = ExcelFillStyle.Solid;
					item.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
					item.Style.Font.Color.SetColor(Color.White);
				}

				HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
				HttpContext.Current.Response.AddHeader("content-disposition", string.Concat("attachment;  filename=", dataFormat, ".xlsx"));
				HttpContext.Current.Response.BinaryWrite(excelPackage.GetAsByteArray());
				HttpContext.Current.Response.End();
			}
		}
	}
}