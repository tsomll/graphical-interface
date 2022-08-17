using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
namespace lab3_4sem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private BindingList<Worker> dataList = new BindingList<Worker>();//будет связывать содержимое здесь с содержимым в таблице
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dataList;
            dataList.Add(new Worker(2, "Иванова П.Р.", "К(П)ФУ", true));
            dataList.Add(new Worker(9, "Зайцева О.Д.", "СПБГУ", false));
            dataList.Add(new Worker(3, "Сидорова И.А.", "МГУ", true));
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                var filename = saveFileDialog1.FileName;
                using var file = new FileStream(filename, FileMode.Create);
                using var sw = new StreamWriter(file, Encoding.UTF8);
                var jso = new JsonSerializerOptions();
                jso.WriteIndented = false;
                foreach (var elem in dataList)
                {
                    sw.WriteLine(JsonSerializer.Serialize<Worker>(elem, jso));
                }
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f2 = new EditForm2();//(снизу)и если пользователь нажал на сохранить
            if (f2.ShowDialog(this) == DialogResult.OK) //диалоговый режим открытия окна и в этом случае первая форма не дает себя использовать, пока открыта вторая
            {
                dataList.Add(f2.UserData);//если все ок то передаем значения в список
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f2 = new EditForm2(dataList[dataGridView1.SelectedRows[0].Index]);//передаем данные из списка
            f2.ShowDialog(this);
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить данные из списка?", "Внимание!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                        dataList.RemoveAt(dataGridView1.SelectedRows[0].Index);
                        MessageBox.Show("Удалила!", "Вам новое сообщение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
            }

            MessageBox.Show("Ну и хорошо!", "Вам новое сообщение!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                var filename = openFileDialog1.FileName;
                using var sr = new StreamReader(filename, Encoding.UTF8);
                dataList.Clear();
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine() ?? "";
                    var obj = JsonSerializer.Deserialize<Worker>(line);
                    if (obj is not null)
                    {
                        dataList.Add(obj);
                    }
                }
            }
        }
    }
}