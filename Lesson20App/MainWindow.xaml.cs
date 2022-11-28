using Lesson20App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word; 
namespace Lesson20App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppDbContext _context = new AppDbContext();
        public MainWindow()
        {
            InitializeComponent();
            ChartPaymentss.ChartAreas.Add(new ChartArea("Main"));
            var currentSeries = new Series("Payments")
            {
                IsValueShownAsLabel = true
            };
            ChartPaymentss.Series.Add(currentSeries);
            ComboUsers.ItemsSource = _context.Users.ToList();
            ComboChartsType.ItemsSource = Enum.GetValues(typeof(SeriesChartType));
        }

        private void UpdateCharts(object sender, SelectionChangedEventArgs e)
        {
            if (ComboUsers.SelectedItem is User currentUser && 
                ComboChartsType.SelectedItem is SeriesChartType currentType )
            {
                Series currentSeries = ChartPaymentss.Series.FirstOrDefault();
                currentSeries.ChartType = currentType;
                currentSeries.Points.Clear();

                var categoriesList = _context.Categories.ToList();
                foreach (var category in categoriesList)
                {
                    currentSeries.Points.AddXY(category.Name,
                        _context.Payments.ToList().Where(p => p.UserId == currentUser && p.CategoryId == category).Sum(p => p.Price * p.Num));
                }
            }
        }

        private void BtnExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            var allUser = _context.Users.ToList().OrderBy(p => p.FIO).ToList();
            var application = new Excel.Application();
            application.SheetsInNewWorkbook = allUser.Count();
            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
           
            for (int i = 0; i < allUser.Count(); i++)
            {
                int startRowIndex = 1;
                Excel.Worksheet worksheet = application.Worksheets.Item[i + 1];
                worksheet.Name = allUser[i].FIO;
                worksheet.Cells[1][startRowIndex] = "Дата платежа"; 
                worksheet.Cells[2][startRowIndex] = "Название"; 
                worksheet.Cells[3][startRowIndex] = "Стоимость"; 
                worksheet.Cells[4][startRowIndex] = "Колличество"; 
                worksheet.Cells[5][startRowIndex] = "Сумма";

                startRowIndex++;

                var userCategories = allUser[i].Payments.OrderBy(p => p.Date).GroupBy(p => p.CategoryId).OrderBy(p => p.Key.Name);
                foreach (var item in userCategories)
                {
                    Excel.Range headRange= worksheet.Range[worksheet.Cells[1][startRowIndex], worksheet.Cells[5][startRowIndex]];
                    headRange.Merge();
                    headRange.Value = item.Key.Name;
                    headRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    headRange.Font.Italic = true;
                    startRowIndex++;
                    foreach (var payment in item)
                    {
                        worksheet.Cells[1][startRowIndex] = payment.Date.ToString("dd.MM.yyyy HH:mm");
                        worksheet.Cells[2][startRowIndex] = payment.Name;
                        worksheet.Cells[3][startRowIndex] = payment.Price;
                        worksheet.Cells[4][startRowIndex] = payment.Num;

                        worksheet.Cells[5][startRowIndex].Formula = $"C{startRowIndex} *D{startRowIndex}";
                        worksheet.Cells[3][startRowIndex].NumberFormat =
                              worksheet.Cells[3][startRowIndex].NumberFormat = "#,###.00";
                        startRowIndex++;
                    }
                    Excel.Range sumRange = worksheet.Range[worksheet.Cells[1][startRowIndex], worksheet.Cells[4][startRowIndex]];
                    sumRange.Merge();
                    sumRange.Value = "ИТОГО :";
                    sumRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    worksheet.Cells[5][startRowIndex].Formula = $"= SUM(E{startRowIndex - item.Count()}" + $"E{startRowIndex - 1}";
                    sumRange.Font.Bold = worksheet.Cells[5][startRowIndex].NumberFormat = "#,###.00";
                    startRowIndex++;

                    Excel.Range rangeBorders = worksheet.Range[worksheet.Cells[1][1], worksheet.Cells[5][startRowIndex - 1]];
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                    worksheet.Columns.AutoFit();

                }
          
            }
            application.Visible = true;
        }

        private void BtnExportToWord_Click(object sender, RoutedEventArgs e)
        {
            var allUsers = _context.Users.ToList(); 
            var allCategories = _context.Categories.ToList();
            var application = new Word.Application();
            Word.Document document = application.Documents.Add();
            foreach (var user in allUsers)
            {
                Word.Paragraph userParagrapth = document.Paragraphs.Add();
                Word.Range userRange = userParagrapth.Range;
                userRange.Text = user.FIO;
                userParagrapth.set_Style("Title");
                userRange.InsertParagraphAfter();

                Word.Paragraph tableParagraph = document.Paragraphs.Add();
                Word.Range tableRange = tableParagraph.Range;
                Word.Table paymentTable = document.Tables.Add(tableRange, allCategories.Count() + 1, 3);
                paymentTable.Borders.InsideLineStyle = paymentTable.Borders.OutsideLineStyle
                    = Word.WdLineStyle.wdLineStyleSingle;
                paymentTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                Word.Range cellRange;
                cellRange = paymentTable.Cell(1, 1).Range;
                cellRange.Text = "Иконка";
                cellRange = paymentTable.Cell(1, 2).Range;
                cellRange.Text = "Категория";
                cellRange = paymentTable.Cell(1, 3).Range;
                cellRange.Text = "Сумма расходов";

                paymentTable.Rows[1].Range.Bold = 1;
                paymentTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                for (int i = 0; i < allCategories.Count(); i++)
                {
                    var currentCategory = allCategories[i];
                    cellRange = paymentTable.Cell(i + 2, 1 ).Range;
                    Word.InlineShape imageShape= cellRange.InlineShapes.AddPicture(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\" + currentCategory.Icon);
                    imageShape.Width = imageShape.Height = 40;
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = paymentTable.Cell(i + 2, 2).Range;
                    cellRange.Text = currentCategory.Name;

                    cellRange = paymentTable.Cell(i + 2, 3).Range;
                    cellRange.Text = user.Payments.ToList()
                        .Where(p => p.CategoryId == currentCategory).Sum(p=> p.Num *p.Price).ToString("N2") + "руб.";
                }
                Payment maxPayment = user.Payments
                    .OrderByDescending(p => p.Price * p.Num).FirstOrDefault();
                if (maxPayment != null)
                {
                    Word.Paragraph maxPaymentParagraph = document.Paragraphs.Add();
                    Word.Range maxPaymentRange = maxPaymentParagraph.Range;
                    maxPaymentRange.Text = $"Самый дорогой платеж - {maxPayment.Name} за {(maxPayment.Price * maxPayment.Num).ToString("N2")}" +
                        $"руб. от {maxPayment.Date.ToString("dd.MM.yyyy HH:mm")}";
                    maxPaymentParagraph.set_Style("Intense Quote");
                    maxPaymentRange.Font.Color = Word.WdColor.wdColorDarkTeal;
                    maxPaymentRange.InsertParagraphAfter();
                }
                Payment MinPayment = user.Payments
                  .OrderBy(p => p.Price * p.Num).FirstOrDefault();
                if (maxPayment != null)
                {
                    Word.Paragraph minPaymentParagraph = document.Paragraphs.Add();
                    Word.Range minPaymentRange = minPaymentParagraph.Range;
                    minPaymentRange.Text = $"Самый дешевый платеж - {MinPayment.Name} за {(MinPayment.Price * MinPayment.Num).ToString("N2")}" +
                        $"руб. от {MinPayment.Date.ToString("dd.MM.yyyy HH:mm")}";
                    minPaymentParagraph.set_Style("Intense Quote");
                    minPaymentRange.Font.Color = Word.WdColor.wdColorDarkGreen;
                    minPaymentRange.InsertParagraphAfter();
                }
                if (user != allUsers.LastOrDefault())
                    document.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);

                
            }
            application.Visible = true;
            document.SaveAs2(@"D:\Test.docx");
            document.SaveAs2(@"D:\Test.pdf", Word.WdExportFormat.wdExportFormatPDF);
        }
    }
}
