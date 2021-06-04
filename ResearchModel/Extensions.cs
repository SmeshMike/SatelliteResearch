using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Action = System.Action;


namespace ResearchModel
{
    public class Extensions
    {
        public static bool ExecuteWithTimeLimit(TimeSpan timeSpan, Action codeBlock)
        {
            try
            {
                Task task = Task.Factory.StartNew(() => codeBlock());
                task.Wait(timeSpan);
                return task.IsCompleted;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerExceptions[0];
            }
        }

        public static void SaveToExcel(List<double> data, string funcType, string satSystem,bool specialResearch, List<double> distances, string explType)
        {
            var filename = "";
            if (funcType.Contains("dm"))
                filename = "DM";
            else if (funcType.Contains("dd"))
                filename = "DD";
            else
                filename = "SUM";

            string ifSpecial = "";
            if (specialResearch)
                ifSpecial = "Special";
            var newFile = $@"..\..\..\..\SatteliteData\{filename+ ifSpecial}.xls";
            FileStream fs;
            if (!System.IO.File.Exists(newFile))
            {
                fs = new FileStream(newFile, FileMode.Create, FileAccess.Write);
                IWorkbook tmpWorkbook = new HSSFWorkbook();
                ISheet tmpSheet = tmpWorkbook.CreateSheet("Sheet");
                var tmpRow = tmpSheet.CreateRow(1);
                var tmpCellX = tmpRow.CreateCell(1);
                tmpWorkbook.Write(fs);
                fs.Close();
            }

            HSSFWorkbook hssfwb;
            int columnX = 1;
            using (fs = new FileStream(newFile, FileMode.Open, FileAccess.Read))
            {
                fs.Position = 0;

                hssfwb = new HSSFWorkbook(fs);
                fs.Close();
            }

            

            if (filename == "DM")
            {
                if (satSystem == "GLONASS")
                {
                    switch (funcType)
                    {
                        case "dmSpace":
                            columnX = 1;
                            break;
                        case "dmEarth":
                            columnX = 5;
                            break;
                        case "dmEarthWithMap":
                            columnX = 9;
                            break;
                    }
                }
                else if (satSystem == "Storm")
                {
                    switch (funcType)
                    {
                        case "dmSpace":
                            columnX = 13;
                            break;
                        case "dmEarth":
                            columnX = 17;
                            break;
                        case "dmEarthWithMap":
                            columnX = 21;
                            break;
                    }
                }

                if (explType == "DT")
                    columnX += 24;
            }
            else if (filename == "SUM")
            {

                if (satSystem == "GLONASS")
                {
                    switch (funcType)
                    {
                        case "sumSpace":
                            columnX = 1;
                            break;
                        case "sumEarth":
                            columnX = 5;
                            break;
                        case "sumEarthWithMap":
                            columnX = 9;
                            break;
                    }
                }
                else if (satSystem == "Storm")
                {
                    switch (funcType)
                    {
                        case "sumSpace":
                            columnX = 13;
                            break;
                        case "sumEarth":
                            columnX = 17;
                            break;
                        case "sumEarthWithMap":
                            columnX = 21;
                            break;
                    }
                }

                if (explType == "DT + DW")
                    columnX += 24;
            }

            bool newSheet = true;
            ISheet sheet; 
            var columnY = columnX + 1;
            try
            {
                sheet = hssfwb.GetSheet("Sheet");
                if (sheet.GetRow(5) != null)
                    newSheet = false;
            }
            catch (Exception)
            {
                newSheet = true;
                sheet = hssfwb.CreateSheet("Sheet");
            }


            var row = newSheet ? sheet.CreateRow(0) : sheet.GetRow(0);
            var cellX = row.CreateCell(columnX);
            cellX.SetCellValue(satSystem);
            row = newSheet ? sheet.CreateRow(1) : sheet.GetRow(1);
            cellX = row.GetCell(columnX) != null ? row.GetCell(columnX) : row.CreateCell(columnX);
            cellX.SetCellValue(explType);
            row = newSheet ? sheet.CreateRow(2) : sheet.GetRow(2);
            cellX = row.GetCell(columnX) != null ? row.GetCell(columnX) : row.CreateCell(columnX);
            cellX.SetCellValue(funcType);

            row = newSheet ? sheet.CreateRow(5) : sheet.GetRow(5);
            cellX = row.CreateCell(columnX);
            cellX.SetCellValue(distances[0]);
            row = newSheet ? sheet.CreateRow(6) : sheet.GetRow(6);
            cellX = row.GetCell(columnX) != null ? row.GetCell(columnX) : row.CreateCell(columnX);
            cellX.SetCellValue(distances[1]);
            row = newSheet ? sheet.CreateRow(7) : sheet.GetRow(7);
            cellX = row.GetCell(columnX) != null ? row.GetCell(columnX) : row.CreateCell(columnX);
            cellX.SetCellValue(distances[2]);


            for (int rowIndex = 12; rowIndex < data.Count + 12; rowIndex++)
            {
                row = newSheet ? sheet.CreateRow(rowIndex) : sheet.GetRow(rowIndex);
                cellX = row.GetCell(columnX) != null ? row.GetCell(columnX) : row.CreateCell(columnX);
                cellX.SetCellValue(rowIndex - 12);

                var cellY = row.GetCell(columnY) != null ? row.GetCell(columnY) : row.CreateCell(columnY);
                cellY.SetCellValue(data[rowIndex - 12]);
            }

            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            using (fs = new FileStream(newFile, FileMode.Open, FileAccess.Write))
            {
                hssfwb.Write(fs);
                fs.Close();
            }
        }
    }
}
