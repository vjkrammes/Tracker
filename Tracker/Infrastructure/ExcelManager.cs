using System.Collections.Generic;
using System.IO;
using System.Linq;

using OfficeOpenXml;

using Tracker.Interfaces;
using Tracker.Models;

namespace Tracker.Infrastructure
{
    public class ExcelManager : IExcelManager
    {
        private void SetMileageHeadings(ExcelWorksheet ws)
        {
            Dictionary<string, string> _headings = new Dictionary<string, string>
            {
                ["A1"] = "Client",
                ["B1"] = "Date",
                ["C1"] = "Mileage",
                ["D1"] = "Description"
            };
            foreach (KeyValuePair<string, string> kvp in _headings)
            {
                ws.Cells[kvp.Key].Value = kvp.Value;
            }
            ws.Cells["A1:D1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;
        }

        private void SetHoursHeadings(ExcelWorksheet ws)
        {
            Dictionary<string, string> _headings = new Dictionary<string, string>
            {
                ["A1"] = "Client",
                ["B1"] = "Date",
                ["C1"] = "Hours",
                ["D1"] = "Description"
            };
            foreach (KeyValuePair<string, string> kvp in _headings)
            {
                ws.Cells[kvp.Key].Value = kvp.Value;
            }
            ws.Cells["A1:D1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;
        }

        public void Create(FileInfo info, IEnumerable<HoursModel> hours, IEnumerable<MileageModel> mileage)
        {
            if (File.Exists(info.FullName))
            {
                File.Delete(info.FullName);
            }
            string numberformat = "###,###,##0.00";
            string dateformat = "yyyy-mm-dd";
            ExcelPackage pck = new ExcelPackage(info);
            ExcelWorksheet hoursws = pck.Workbook.Worksheets.Add("Hours");
            ExcelWorksheet milesws = pck.Workbook.Worksheets.Add("Mileage");
            if (hours != null && hours.Any())
            {
                SetHoursHeadings(hoursws);
                string currentClient = string.Empty;
                int row = 2;
                foreach (var model in hours)
                {
                    if (model.Client != currentClient)
                    {
                        currentClient = model.Client;
                        hoursws.Cells[row, 1].Value = currentClient;
                    }
                    hoursws.Cells[row, 2].Style.Numberformat.Format = dateformat;
                    hoursws.Cells[row, 2].Value = model.Date;
                    hoursws.Cells[row, 3].Style.Numberformat.Format = numberformat;
                    hoursws.Cells[row, 3].Value = model.Time;
                    hoursws.Cells[row, 4].Value = model.Description;
                    row++;
                }
                string cellrange = $"A1:D{row - 1}";
                hoursws.Cells[cellrange].AutoFitColumns();
                for (int ix = 1; ix <= 4; ix++)
                {
                    hoursws.Column(ix).Width += 5;
                }
            }
            else
            {
                pck.Workbook.Worksheets.Delete(hoursws);
            }
            if (mileage != null && mileage.Any())
            {
                SetMileageHeadings(milesws);
                string currentClient = string.Empty;
                int row = 2;
                foreach (var model in mileage)
                {
                    if (model.Client != currentClient)
                    {
                        currentClient = model.Client;
                        milesws.Cells[row, 1].Value = currentClient;
                    }
                    milesws.Cells[row, 2].Style.Numberformat.Format = dateformat;
                    milesws.Cells[row, 2].Value = model.Date;
                    milesws.Cells[row, 3].Style.Numberformat.Format = numberformat;
                    milesws.Cells[row, 3].Value = model.Miles;
                    milesws.Cells[row, 4].Value = model.Description;
                    row++;
                }
                string cellrange = $"A1:D{row - 1}";
                milesws.Cells[cellrange].AutoFitColumns();
                for (int ix = 1; ix <= 4; ix++)
                {
                    milesws.Column(ix).Width += 5;
                }
            }
            else
            {
                pck.Workbook.Worksheets.Delete(milesws);
            }
            pck.Save();
            hoursws.Dispose();
            milesws.Dispose();
            pck.Dispose();
        }
    }
}
