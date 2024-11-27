using Demo1.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Demo1.Views
{
    public partial class MainWindow
    {
        private IEnumerable<Partner> _partnersList;

        public MainWindow()
        {
            InitializeComponent();
            UpdatePartners();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddEditWindow().ShowDialog();
            UpdatePartners();
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new AddEditWindow((Partner)((FrameworkElement)sender).Tag).ShowDialog();
            UpdatePartners();
        }

        private void HistoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new ProductSalesHistoryWindow((Partner)((FrameworkElement)sender).Tag).ShowDialog();
            UpdatePartners();
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var partner = (Partner)((FrameworkElement)sender).Tag;
            if (MessageBox.Show($"Вы уверены, что хотите удалить партнёра {partner}?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question)
                is MessageBoxResult.Yes)
            {
                App.Context.Partners.Remove(partner);
                App.Context.SaveChanges();
            }
            UpdatePartners();
        }

        private void UpdatePartners()
        {
            _partnersList = App.Context.Partners.ToList();
            ProductListView.ItemsSource = _partnersList;
        }
    }
}