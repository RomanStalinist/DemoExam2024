using Demo.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Demo
{
    public partial class ProductSalesHistoryWindow
    {
        private readonly Partner _partner;
        private readonly List<PartnerProduct> _deals;

        public ProductSalesHistoryWindow(Partner partner)
        {
            InitializeComponent();
            _partner = partner;
            _deals = App.Context.PartnerProducts
                .Where(pp => pp.PartnerID == _partner.ID)
                .ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_deals.Any())
            {
                MessageBox.Show("Реализации у партнера отсутствуют", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }

            ListHistoryProductsSales.ItemsSource = _deals;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
            => Close();
    }
}
