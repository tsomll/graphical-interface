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

        private BindingList<Worker> dataList = new BindingList<Worker>();//����� ��������� ���������� ����� � ���������� � �������
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dataList;
            dataList.Add(new Worker(2, "������� �.�.", "�(�)��", true));
            dataList.Add(new Worker(9, "������� �.�.", "�����", false));
            dataList.Add(new Worker(3, "�������� �.�.", "���", true));
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

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f2 = new EditForm2();//(�����)� ���� ������������ ����� �� ���������
            if (f2.ShowDialog(this) == DialogResult.OK) //���������� ����� �������� ���� � � ���� ������ ������ ����� �� ���� ���� ������������, ���� ������� ������
            {
                dataList.Add(f2.UserData);//���� ��� �� �� �������� �������� � ������
            }
        }

        private void �������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f2 = new EditForm2(dataList[dataGridView1.SelectedRows[0].Index]);//�������� ������ �� ������
            f2.ShowDialog(this);
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("�� �������, ��� ������ ������� ������ �� ������?", "��������!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                        dataList.RemoveAt(dataGridView1.SelectedRows[0].Index);
                        MessageBox.Show("�������!", "��� ����� ���������!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
            }

            MessageBox.Show("�� � ������!", "��� ����� ���������!",
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