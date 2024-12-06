using Demo.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Demo
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateListView();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new AddEditWindow().ShowDialog();
            UpdateListView();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem mi && mi.DataContext is Partner partner)
            {
                new AddEditWindow(partner).ShowDialog();
                UpdateListView();
            }
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem mi && mi.DataContext is Partner partner)
                new ProductSalesHistoryWindow(partner).ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem mi && mi.DataContext is Partner partner
                && MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) is MessageBoxResult.Yes)
            {
                App.Context.Partners.Remove(partner);
                App.Context.SaveChanges();
                UpdateListView();
            }
        }

        private void UpdateListView()
            => PartnersListView.ItemsSource = App.Context.Partners
            .ToList();
    }
}
