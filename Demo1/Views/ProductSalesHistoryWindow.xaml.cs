using Demo1.Data;
using System.Linq;
using System.Windows;

namespace Demo1.Views
{
    public partial class ProductSalesHistoryWindow : Window
    {
        private readonly Partner _partner;

        public ProductSalesHistoryWindow(Partner partner)
        {
            InitializeComponent();
            _partner = partner;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_partner is null)
            {
                MessageBox.Show("Выберите партнера, чтобы отобразилась история", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var products = App.Context.PartnerProducts
                .Where(x => x.PartnerID == _partner.ID)
                .ToList();

            ListHistoryProductsSales.ItemsSource = products;

            if (products.Any()) return;
            
            MessageBox.Show("Данный партнер еще не реализовывал продукцию", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
