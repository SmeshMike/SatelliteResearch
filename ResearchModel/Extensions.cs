using System;
using System.Collections.Generic;
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

        public static void SaveToExcel(List<double> data,string funcType, string satSystem, string explType, bool heightExplr)
        {
            var filename = "";
            if (funcType.Contains("dm"))
                filename = "DM";
            else if(funcType.Contains("dd"))
                filename = "DD";
            else
                filename = "SUM";

            var newFile = $@"..\..\..\..\{filename}.xls";
            FileStream fs;
            if (!System.IO.File.Exists(newFile))
            {
                fs = new FileStream(newFile, FileMode.Create, FileAccess.Write);
                IWorkbook tmpWorkbook = new HSSFWorkbook();
                ISheet tmpSheet = tmpWorkbook.CreateSheet("Sheet");
                var row = tmpSheet.CreateRow(1);
                var cellX = row.CreateCell(1);
                tmpWorkbook.Write(fs);
                fs.Close();
            }

            HSSFWorkbook hssfwb;
            int columnX = 0;
            using (fs = new FileStream(newFile, FileMode.Open, FileAccess.Read))
            {
                fs.Position = 0;
                
                hssfwb = new HSSFWorkbook(fs);
                fs.Close();
            }

            ISheet sheet;//= hssfwb.GetSheet(0);

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
                                columnX = 11;
                                break;
                        }
                    }
                    else if (satSystem == "Storm")
                    {
                        switch (funcType)
                        {
                            case "dmSpace":
                                columnX = 31;
                                break;
                            case "dmEarth":
                                columnX = 41;
                                break;
                        }
                    }
                    if (explType == "D")
                        columnX += 60;
                }
                else if (filename == "SUM")
                {
                    if (heightExplr)
                    {

                        switch (funcType)
                        {
                            case "sumSpace":
                                columnX = 21;
                                break;
                            case "sumEarth":
                                columnX = 1;
                                break;
                            case "sumEarthWithMap":
                                columnX = 11;
                                break;
                        }
                    
                        if (satSystem == "Storm")
                            columnX += 60;
                    }
                    else
                    {
                        if (explType == "D")
                        {
                            switch (funcType)
                            {
                                case "sumEarth":
                                    columnX = 1;
                                    break;
                                case "sumSpace":
                                    columnX = 31;
                                    break;
                            }
                        }
                        else if (satSystem == "Coord")
                        {
                            switch (funcType)
                            {
                                case "sumEarth":
                                    columnX = 11;
                                    break;
                                case "sumSpace":
                                    columnX = 41;
                                    break;
                            }
                        }
                    }

                    if (satSystem == "Storm")
                        columnX += 60;
                }

                bool newSheet = true;

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
                    var row = newSheet ? sheet.CreateRow(1) : sheet.GetRow(1);
                    var cellX = row.CreateCell(columnX);
                    cellX.SetCellValue(satSystem);
                    row = newSheet ? sheet.CreateRow(2) : sheet.GetRow(2);

                    cellX = row.GetCell(columnX) != null ? row.GetCell(columnX) : row.CreateCell(columnX);
                    cellX.SetCellValue(explType);
                }


                for (int rowIndex = 5; rowIndex < data.Count + 5; rowIndex++)
                {
                    var row = newSheet ? sheet.CreateRow(rowIndex) : sheet.GetRow(rowIndex);
                    var cellX = row.GetCell(columnX) != null ? row.GetCell(columnX) : row.CreateCell(columnX);
                    cellX.SetCellValue(rowIndex-5);

                    var cellY = row.GetCell(columnY) != null ? row.GetCell(columnY) : row.CreateCell(columnY);
                    cellY.SetCellValue(data[rowIndex - 5]);
                }
                sheet.AutoSizeColumn(0);
                sheet.AutoSizeColumn(1);
                using ( fs = new FileStream(newFile, FileMode.Open, FileAccess.Write))
                {
                    hssfwb.Write(fs);
                    fs.Close();
                }
        }
    }
}
