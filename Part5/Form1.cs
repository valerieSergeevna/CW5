using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Part5
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            richTextBox3.Text = "Первая введенная строка - функция\nОстальные - ограничения\nПример:\n2x1+4x2+x3\nx1 + x2 + 2x3 >= 500\n10x1 + 8x2 + 5x3 >= 2000\n2x1 + x2 >= 100\nЕсли знак >=, то прогамма ищет минимум\nЕсли<=, то максимум  ";
            richTextBox3.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox2.Clear();
                SimplexMethod optimize = new SimplexMethod(richTextBox1.Lines);
                double value = optimize.SimplexCalculate();
                richTextBox2.Text = optimize.Show();

                File.AppendAllText("output.txt", optimize.WriteToFile() + "\n");
            }
            catch (Exception ex)
            {

                richTextBox2.Clear();
                richTextBox2.Text = ex.Message;
            }
        }
    }

      
  }

