using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab3_4sem
{
    public class Worker : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int _id;
        private string _value1;
        private string _value2;
        private bool _isChecked;
        [DisplayName("Номер телефона")]//атрибут который позволяет задать отображаемое имя
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();//таблица может теперь получить информацию о том что свойство меняется

            }
        }

        [DisplayName("ФИО сотрудника:")]
        public string Value1
        {
            get => _value1;
            set
            {
                _value1 = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Образование:")]
        public string Value2
        {
            get => _value2;
            set
            {
                if (value.Trim().Length == 0)
                    throw new BindingException("Образование", "Строка не должна быть пустой");
                _value2 = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Есть знание английского языка?")]
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }

        public Worker(int id, string value1, string value2, bool isChecked)//конструктор для списка
        {
            Id = id;
            Value1 = value1;
            Value2 = value2;
            IsChecked = isChecked;
        }
        // Конструктор по умолчанию удобно использовать для создания новых значений
        public Worker()
        {
            // В нем необходимо предусмотреть, чтобы все свойства имели допустимые значения
            Value2 = "-";
        }

        // Метод для копирования объекта
        public Worker Copy() => new(Id, Value1, Value2, IsChecked);
        // Метод копирования данного объекта в другой, указанный в параметре
        // (можно использовать для восстановления данных из резервной копии
        // при отказе пользователя от сохранения сделанных изменений)
        public void CopyTo(Worker trd)
        {
            if (trd != null)
            {
                trd.Id = Id;
                trd.Value1 = Value1;
                trd.Value2 = Value2;
                trd.IsChecked = IsChecked;
            }
        }
    }
}
