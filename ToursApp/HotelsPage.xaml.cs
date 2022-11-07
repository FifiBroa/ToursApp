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
    /// Логика взаимодействия для HotelsPage.xaml
    /// </summary>
    public partial class HotelsPage : Page
    {

        public HotelsPage()
        {
            InitializeComponent();
        }

       

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Hotel));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = DGridHotels.SelectedCells.Cast<Hotel>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следущие {hotelsForRemoving.Count()} элементов ? ", "Внимание" ,
                MessageBoxButton.YesNo ,MessageBoxImage.Question ) == MessageBoxResult.Yes)
            {
                try
                {
                    ApplicationDbContext.GetContext().Hotels.RemoveRange(hotelsForRemoving);
                    ApplicationDbContext.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DGridHotels.ItemsSource = ApplicationDbContext.GetContext().Hotels.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ApplicationDbContext.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridHotels.ItemsSource = ApplicationDbContext.GetContext().Hotels.ToList();
            }
        }
    }
}
