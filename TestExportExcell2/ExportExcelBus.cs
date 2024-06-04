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
                string outputFilePath = $"{projectDirectory}\\FileExports\\Report_{DateTime.Now.ToString("yyMMHHmm")}.xlsx";


                // Create a new workbook
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Sheet1");


                // Set values and format cells manually           
                // Row 1
                worksheet.Cell(1, 2).Value = "VIETTEL";
                worksheet.Range(1, 2, 1, 14).Merge().Style.Fill.BackgroundColor = XLColor.LightGreen;
                worksheet.Cell(1, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(1, 15).Value = "MOBI";
                worksheet.Range(1, 15, 1, 26).Merge().Style.Fill.BackgroundColor = XLColor.Pink;
                worksheet.Cell(1, 15).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(1, 27).Value = "VINAPHONE";
                worksheet.Range(1, 27, 1, 36).Merge().Style.Fill.BackgroundColor = XLColor.LightGreen;
                worksheet.Cell(1, 27).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(1, 37).Value = "MẠNG KHÁC";
                worksheet.Range(1, 37, 1, 41).Merge().Style.Fill.BackgroundColor = XLColor.Pink;
                worksheet.Cell(1, 37).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Row 2
                List<string> listCusTopup = new List<string> { "MSB", "OCB", "NAB", "Nhất Trần", "GAPIT", "VPAY", "MB" }; // DL bind vao row3

                worksheet.Cell(2, 2).Value = "KHÁCH HÀNG - TOPUP";
                //worksheet.Range(2, 2, 2, 8).Merge();
                worksheet.Range(2, 2, 2, listCusTopup.Count + 1).Merge();
                // Set width of cell

                int stepNext = 7; // Se lay tu BD khi co DL

                worksheet.Cell(2, 2 + stepNext).Value = "VTL topup";
                worksheet.Range(2, 2 + stepNext, 3, 2 + stepNext).Merge().Style.Fill.BackgroundColor = XLColor.AshGrey;
                //worksheet.Cell(2, 2).Style.Fill.BackgroundColor = XLColor.AshGrey;

                int postionCallNCCTopup = 2 + stepNext + 1;
                worksheet.Cell(2, postionCallNCCTopup).Value = "NCC - TOPUP";
                worksheet.Range(2, postionCallNCCTopup, 2, postionCallNCCTopup + 1).Merge();

                int postionCellKHBill = postionCallNCCTopup + 2;
                worksheet.Cell(2, postionCellKHBill).Value = "KHÁCH HÀNG - BILL";
                int numberCusBill = 2; // Se lay tu BD khi co DL
                worksheet.Range(2, postionCellKHBill, 2, postionCellKHBill + numberCusBill - 1).Merge();

                int postionCellNCCBill = postionCellKHBill + numberCusBill;
                worksheet.Cell(2, postionCellNCCBill).Value = "VTL paybill";
                //worksheet.Cell(2, postionCellNCCBill).Style.Fill.BackgroundColor = XLColor.AshGrey;
                worksheet.Range(2, postionCellNCCBill, 3, postionCellNCCBill).Merge().Style.Fill.BackgroundColor = XLColor.AshGrey;

                worksheet.Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Row 3
                worksheet.Cell(3, 1).Value = "Ngay";
                int numberDayInMonth = 31;
                for (int i = 1; i <= numberDayInMonth; i++)
                {
                    worksheet.Cell(i + 3, 1).Value = i;
                }

                // Danh border table
                worksheet.Range(1, 1, numberDayInMonth + 3, 41).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range(1, 1, numberDayInMonth + 3, 41).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range(1, 1, numberDayInMonth + 3, 41).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Range(1, 1, numberDayInMonth + 3, 41).Style.Border.RightBorder = XLBorderStyleValues.Thin;


                // KH - TOPUP
                for (int i = 1; i <= listCusTopup.Count; i++)
                {
                    worksheet.Cell(3, 1 + i).Value = listCusTopup[i - 1];
                }

                //postionCellKHBill               
                List<string> listProviderTopupViettel = new List<string> { "VDS", "VTT" };
                int numsProvidersTopupViettel = listProviderTopupViettel.Count();
                for (int i = 0; i < listProviderTopupViettel.Count(); i++)
                {
                    worksheet.Cell(3, postionCallNCCTopup + i).Value = listProviderTopupViettel[i];
                }

                List<string> listProviderBillViettel = new List<string> { "MSB BILL", "MB BILL" };
                for (int i = 0; i < listProviderTopupViettel.Count(); i++)
                {
                    worksheet.Cell(3, postionCellKHBill + i).Value = listProviderBillViettel[i];
                }

                // Row 4 (Example Data)            
                worksheet.Cell(4, 2).Value = 200060;
                worksheet.Cell(4, 3).Value = 3000220;
                worksheet.Cell(4, 4).Value = 400020231;
                worksheet.Cell(4, 5).Value = 5020060;
                worksheet.Cell(4, 6).Value = 6030220;
                worksheet.Cell(4, 7).Value = 74230231;
                worksheet.Cell(4, 8).Value = 80462231;

                worksheet.Cell(5, 2).Value = 200060;
                worksheet.Cell(5, 3).Value = 3000220;
                worksheet.Cell(5, 4).Value = 400020231;
                worksheet.Cell(5, 5).Value = 5020060;
                worksheet.Cell(5, 6).Value = 6030220;
                worksheet.Cell(5, 7).Value = 74230231;
                worksheet.Cell(5, 8).Value = 80462231;

                // Adjust column widths
                //worksheet.Columns().AdjustToContents();

                // How to set width of column
                int withCellDefault = 10;
                for (int i = 1; i <= 41; i++)
                {
                    worksheet.Column(i).Width = withCellDefault;
                }

                // Sum total    
                for (int i = 2; i <= 41; i++)
                {
                    worksheet.Cell(35, i).FormulaA1 = $"SUM({worksheet.Cell(4, i).Address}:{worksheet.Cell(34, i).Address})";
                }

                // Save the modified Excel file
                workbook.SaveAs(outputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }

        }

        // Load file template and fill data
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



    }
}
