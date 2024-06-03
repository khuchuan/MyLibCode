using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ClosedXML.Excel;


namespace TestExportExcell2
{
    public class ExportExcelBus
    {
        public void ExportFileBC(int startRow)
        {
            try
            {
                // Get the current directory
                string currentDirectory = Directory.GetCurrentDirectory();

                // Navigate to the project directory (assuming the app runs from bin/Debug/net6.0)
                string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;

                Console.WriteLine($"Current Directory: {currentDirectory}");
                Console.WriteLine($"Project Directory: {projectDirectory}");

                string inputFilePath = $"{projectDirectory}\\BCMau.xlsx";
                string outputFilePath = $"{projectDirectory}\\FileExports\\Report_{DateTime.Now.ToString("yyMM")}.xlsx";

                // Sample data to be inserted
                List<List<decimal>> data = new List<List<decimal>>()
                {
                    new List<decimal> { 1000000, 2300000, 3100000, 4000040, 5000000, 1000700, 7006000, 8006300 }
                };

                // Load the existing Excel file
                XLWorkbook workbook = new XLWorkbook();

                // Check exit file
                if (File.Exists(outputFilePath))
                    workbook = new XLWorkbook(outputFilePath);
                else
                    workbook = new XLWorkbook(inputFilePath);
                var worksheet = workbook.Worksheet(1); // Assuming data is in the first worksheet

                for (int i = 0; i < data.Count; i++)
                {
                    for (int j = 1; j < data[i].Count; j++)
                    {
                        worksheet.Cell(startRow + i, j + 1).Value = data[i][j];
                    }
                }

                // Save the modified Excel file
                workbook.SaveAs(outputFilePath);
                Console.WriteLine($"Data has been successfully filled starting from row {startRow}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }

        }

    
        public void ExportBC(int startRow)
        {
            try
            {
                // Get the current directory
                string currentDirectory = Directory.GetCurrentDirectory();

                // Navigate to the project directory (assuming the app runs from bin/Debug/net6.0)
                string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;

                Console.WriteLine($"Current Directory: {currentDirectory}");
                Console.WriteLine($"Project Directory: {projectDirectory}");

                string inputFilePath = $"{projectDirectory}\\BCMau.xlsx";
                string outputFilePath = $"{projectDirectory}\\FileExports\\Report_{DateTime.Now.ToString("yyMM")}.xlsx";



                

                // Create a new workbook
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Sheet1");

                // Set values and format cells manually
                // Row 1
                worksheet.Cell(1, 1).Value = "MOBI";
                worksheet.Cell(1, 2).Value = "VINAPHONE";
                worksheet.Cell(1, 3).Value = "MẠNG KHÁC";
                worksheet.Range(1, 1, 1, 3).Merge().Style.Fill.BackgroundColor = XLColor.LightBlue;
                worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Row 2
                worksheet.Cell(2, 1).Value = "";
                worksheet.Cell(2, 2).Value = "KHÁCH HÀNG - TOPUP";
                worksheet.Cell(2, 3).Value = "Subheader3";
                worksheet.Range(2, 1, 2, 3).Merge().Style.Fill.BackgroundColor = XLColor.LightGreen;
                worksheet.Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Row 3
                worksheet.Cell(3, 1).Value = "Col1";
                worksheet.Cell(3, 2).Value = "Col2";
                worksheet.Cell(3, 3).Value = "Col3";

                // Row 4 (Example Data)
                worksheet.Cell(4, 1).Value = "Data1_Row4_Col1";
                worksheet.Cell(4, 2).Value = "Data1_Row4_Col2";
                worksheet.Cell(4, 3).Value = "Data1_Row4_Col3";

                // Row 5 (Example Data)
                worksheet.Cell(5, 1).Value = "Data2_Row5_Col1";
                worksheet.Cell(5, 2).Value = "Data2_Row5_Col2";
                worksheet.Cell(5, 3).Value = "Data2_Row5_Col3";

                // Additional formatting
                worksheet.Cell(4, 1).Style.Fill.BackgroundColor = XLColor.Yellow;
                worksheet.Cell(5, 1).Style.Fill.BackgroundColor = XLColor.Cyan;

                // Adjust column widths
                worksheet.Columns().AdjustToContents();

                // Save the modified Excel file
                workbook.SaveAs(outputFilePath);

                Console.WriteLine($"Data has been successfully filled and saved to {outputFilePath}.");



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }

        }   
    
    }
}
