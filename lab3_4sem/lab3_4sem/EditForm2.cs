using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3_4sem
{
    public partial class EditForm2 : Form
    {
        public Worker UserData { get; private set; }
        private Binding v2binding;
        // Резервная копия данных объекта, полученная из основной формы
        private readonly Worker _userBackupData;

        // Информация об исключении при редактировании данных (при наличии ошибок)
        private BindingException? _bindingException;
        public EditForm2(Worker? ud = null)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;//значит , что пользоваткль откзался что либо вводить
                                               // Производим связывание данных между графическими элементами и свойством с хранимой информацией об объекте
                                               // Если переданые данные из главной формы...
            if (ud is not null)
                UserData = ud; // сохраняем их в свойстве с данными
            else UserData = new Worker(); // ... иначе - создаем новое свойство с данными

            // Делаем резервную копию исходно переданных данных на случай отмены радактирования
            _userBackupData = UserData.Copy();

            // Производим связывание данных между графическими элементами и свойством с хранимой информацией об объекте
            numericUpDown1.DataBindings.Add("Value", UserData, "Id");
            textBox1.DataBindings.Add("Text", UserData, "Value1");
            var v2Binding = textBox2.DataBindings.Add("Text", UserData, "Value2");
            // Включаем поддержку форматирования ввода
            // (обеспечивает контроль ошибок при вводе данных)
            v2Binding.FormattingEnabled = true; // !!!!
                                                // Назначаем метод, который будет вызываться для анализа
                                                // введенных в проверяемое поле данных
            v2Binding.BindingComplete += V2BindingComplete;
            checkBox1.DataBindings.Add("Checked", UserData, "IsChecked");
        }
        private void V2BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            // Вызывается при изменении данных
            if (e.BindingCompleteState != BindingCompleteState.Success)
            {
                // Если изменения прошли с ошибкой (сработало исключение в TableRowData)
                textBox2.BackColor = Color.OrangeRed;
                textBox2.Focus();
            }

            // Сохраняем информацию о произошедшем исключении в поле класса
            _bindingException = e.Exception as BindingException;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (_bindingException is not null)//кнопка сохранить
            {
                // Выводим сообщение об ошибке
                MessageBox.Show(
                    _bindingException.Message,
                    _bindingException.ErrorField,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                // и не даем окну закрыться. 
                return;
            }

            // Сюда попадем только если ошибок нет и данные можно сохранять
            // Установим результат работы с диалоговым окном
            DialogResult = DialogResult.OK;

            // И закроем окно.
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _userBackupData.CopyTo(UserData);
            Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Если пользователь редактирует неверное поле, убираем подсветку,
            // сигнализирующую об ошибке. 
            textBox2.BackColor = Color.White;
        }
    }
}
