using Demo.Data;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;

namespace Demo
{
    public partial class AddEditWindow
    {
        private Partner _partner;
        private readonly string[] _types = { "ПАО", "ООО", "ЗАО", "ОАО" };

        public AddEditWindow(Partner partner = null)
        {
            InitializeComponent();
            TypeComboBox.ItemsSource = _types;
            _partner = partner ?? new Partner { Type = _types[0] };
            DataContext = _partner;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
            => DataContext = _partner = new Partner
            {
                ID = _partner.ID,
                Type = _types[0]
            };

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var message = ValidatePartner();

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show("Вы уверены, что указали все, что хотели?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) is MessageBoxResult.Yes)
            {
                App.Context.Partners.AddOrUpdate(_partner);
                App.Context.SaveChanges();
                Close();
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
            => Close();

        private string ValidatePartner()
        {
            try
            {
                if (string.IsNullOrEmpty(_partner.Title))
                    throw new InvalidOperationException("Введите название партнера");
                
                if (string.IsNullOrEmpty(_partner.LegalAddress))
                    throw new InvalidOperationException("Введите адрес партнёра");
                
                if (string.IsNullOrEmpty(_partner.Director))
                    throw new InvalidOperationException("Введите ФИО директора");
                
                if (string.IsNullOrEmpty(_partner.Phone))
                    throw new InvalidOperationException("Введите телефон партнёра");
                    
                if (string.IsNullOrEmpty(_partner.Email))
                    throw new InvalidOperationException("Введите почту партнёра");
                
                if (string.IsNullOrEmpty(_partner.INN))
                    throw new InvalidOperationException("Введите ИНН партнёра");

                if (_partner.Director.Length < 4)
                    throw new InvalidOperationException("ФИО директора слишком короткое");

                if (_partner.Rating <= 0)
                    throw new InvalidOperationException("Рейтинг должен быть больше 0");

                if (_partner.Phone.Length != 13)
                    throw new InvalidOperationException("Телефон партнера слишком длинный");

                // Проверка что номер телефона не содержит букв
                if (_partner.Phone.Any(char.IsLetter))
                    throw new InvalidOperationException("Телефон партнера не должен содержать буквы");
                
                // Проверка что email содержит символ @
                if (!_partner.Email.Contains("@"))
                    throw new InvalidOperationException("Email введен неверно");
                
                // Проверка что ИНН имеет необходимое количество цифр
                if (_partner.INN.Length != 10)
                    throw new InvalidOperationException("ИНН партнера введен неверно");
                
                // Проверка что ИНН является числом
                if (_partner.INN.Any(char.IsLetter))
                    throw new InvalidOperationException("ИНН партнера должен содержать только цифры");
            }
            catch (InvalidOperationException ioe)
            {
                return ioe.Message;
            }

            return string.Empty;
        }
    }
}
