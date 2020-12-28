using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
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

        public static void SaveToExcel(List<double> data,string fileName)
        {
            var newFile = $@"..\..\..\..\{fileName}.xlsx";

            using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
            {

                IWorkbook workbook = new XSSFWorkbook();

                ISheet sheet1 = workbook.CreateSheet("Sheet1");

                
                
                for (int rowIndex = 0; rowIndex < data.Count; rowIndex++)
                {
                    var row = sheet1.CreateRow(rowIndex);
                    var cellX = row.CreateCell(0);
                    cellX.SetCellValue(rowIndex);

                    var cellY = row.CreateCell(1);
                    cellY.SetCellValue(data[rowIndex]);
                }
                sheet1.AutoSizeColumn(0);
                sheet1.AutoSizeColumn(1);

                workbook.Write(fs);
            }
        }
    }
}
