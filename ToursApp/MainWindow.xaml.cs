using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToursApp.Models;

namespace ToursApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string SetFile; //TODO ДОБАВИТЬ НАДО ПУТЬ К ЕКСЕЛЮ
        private string SetImageFile; //TODO ДОБАВИТЬ НАДО ПУТЬ К ЕКСЕЛЮ
        ApplicationDbContext Db;
        public MainWindow( )
        {
            InitializeComponent();
            Db = new ApplicationDbContext();
            MainFrame.Navigate(new ToursPage());
            Manager.MainFrame = MainFrame;
        }
        private void ImportTours()
        {
            var fillData = File.ReadAllLines($@"{SetFile}");
            var images = Directory.GetFiles($@"{SetImageFile}");
            foreach (var line in fillData)
            {
                var data = line.Split('\t');
                var tempTour = new Tour
                {
                    Name = data[0].Replace("\"", ""),
                    TicketCount = int.Parse(data[2]),
                    Price = decimal.Parse(data[3]),
                    IsActual = (data[4] != "0") ? false : true
                };

                foreach (var tourType in data[5].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var currentType = ApplicationDbContext.GetContext().TypeOfTours.ToList().FirstOrDefault(p => p.Tour.Name == tourType);
                    if (currentType != null)
                        tempTour.TypeOfTours.Add(currentType);
                }
                try
                {
                    tempTour.ImagePreview = File.ReadAllBytes(images.FirstOrDefault(p => p.Contains(tempTour.Name)));
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                ApplicationDbContext.GetContext().Tours.Add(tempTour);
                ApplicationDbContext.GetContext().SaveChanges();

            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
            }
            else
            {
                BtnBack.Visibility = Visibility.Hidden;
            }
        }
    }
}
