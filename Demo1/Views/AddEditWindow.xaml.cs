using Demo1.Data;
using System;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Windows;

namespace Demo1.Views
{
    public partial class AddEditWindow
    {
        private static readonly string[] _types = { "ЗАО", "ООО", "ОАО", "ПАО" };
        private readonly Partner _partner;

        public AddEditWindow(Partner partner = null)
        {
            InitializeComponent();
            _partner = partner ?? new Partner { Type = _types[0] };
            Title = _partner is null ? "Добавление партнёра" : "Редактирование партнёра";
            DataContext = _partner;
            TypeComboBox.ItemsSource = _types;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var title = _partner.Title;
            var type = _partner.Type;
            var legalAddress = _partner.LegalAddress;
            var director = _partner.Director;
            var phone = _partner.Phone;
            var email = _partner.Email;
            var rating = _partner.Rating;
            var inn = _partner.INN;

            try
            {
                // Проверка что введены все данные
                if (string.IsNullOrEmpty(title))
                    throw new InvalidOperationException("Введите название партнера");
                
                if (string.IsNullOrEmpty(type))
                    throw new InvalidOperationException("Выберите тип партнёра");
                    
                if (rating is 0)
                    throw new InvalidOperationException("Введите рейтинг партнёра (Он должен быть больше 0)");
                
                if (string.IsNullOrEmpty(legalAddress))
                    throw new InvalidOperationException("Введите адрес партнёра");
                
                if (string.IsNullOrEmpty(director))
                    throw new InvalidOperationException("Введите ФИО директора");
                
                if (string.IsNullOrEmpty(phone))
                    throw new InvalidOperationException("Введите телефон партнёра");
                    
                if (string.IsNullOrEmpty(email))
                    throw new InvalidOperationException("Введите почту партнёра");
                
                if (string.IsNullOrEmpty(inn))
                    throw new InvalidOperationException("Введите ИНН партнёра");
                
                // Проверка что ФИО директора не слишком короткое
                if (director.Length < 4)
                    throw new InvalidOperationException("ФИО директора слишком Короткое");

                // Проверка что номер телефона имеет необходимое количество цифр
                if (phone.Length > 13 || phone.Length < 10)
                    throw new InvalidOperationException("Телефон партнера слишком длинный");

                // Проверка что номер телефона не содержит букв
                if (phone.Any(char.IsLetter))
                    throw new InvalidOperationException("Телефон партнера не должен содержать буквы");
                
                // Проверка что email содержит символ @
                if (!email.Contains("@"))
                    throw new InvalidOperationException("Email введен неверно");
                
                // Проверка что ИНН имеет необходимое количество цифр
                if (inn.Length != 10)
                    throw new InvalidOperationException("ИНН партнера введен неверно");
                
                // Проверка что ИНН является числом
                if (inn.Any(char.IsLetter))
                    throw new InvalidOperationException("ИНН партнера должен содержать только цифры");

                App.Context.Partners.AddOrUpdate(_partner);
                App.Context.SaveChanges();
                Close();
            }
            catch (InvalidOperationException ioe)
            {
                MessageBox.Show(ioe.Message, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (DbEntityValidationException deve)
            {
                var builder = new StringBuilder().AppendLine("Список ошибок:");
                deve.EntityValidationErrors.ToList()
                    .ForEach(r => r.ValidationErrors.ToList()
                    .ForEach(ir => builder.AppendLine(ir.ErrorMessage)));
                MessageBox.Show(builder.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            AddressBox.Text = string.Empty;
            DirectorBox.Text = string.Empty;
            RatingBox.Text = string.Empty;
            TitleBox.Text = string.Empty;
            PhoneBox.Text = string.Empty;
            TypeComboBox.Text = "ЗАО";
            InnBox.Text = string.Empty;
            EmailBox.Text = string.Empty;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
