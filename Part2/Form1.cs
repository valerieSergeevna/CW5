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

namespace Part2
{

    public partial class Form1 : Form
    {
        
        int ColumnIndex = 0;
        
        int RowIndex = 0;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
                dataGridView1.Columns.Add($"P{ColumnIndex}", $"P{ColumnIndex++}");

            for (int i = 0; i < Convert.ToInt32(textBox2.Text); i++)
            { dataGridView1.Rows.Add(); dataGridView1.Rows[i].HeaderCell.Value = $"{i+1}"; RowIndex++; }

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[,] MA = new int[RowIndex, ColumnIndex];
            for (int i = 0; i < ColumnIndex; i++)
            {
                for (int j = 0; j < RowIndex; j++)
                {
                    MA[j, i] = Convert.ToInt32(dataGridView1.Rows[j].Cells[i].Value);
                }
            }

            string[] str = textBox3.Text.Split(' ');
            int[] RA = new int[str.Length];
            for (int i = 0; i < str.Length; i++)
                RA[i] = Convert.ToInt32(str[i]);
            TPR table = new TPR(RowIndex,ColumnIndex, MA,RA);
            table.PriorityAlgorythm();
            printRank(table);
           
        }

        private void printInFile(TPR table)
        {

            int[,] Rank = new int[RowIndex, ColumnIndex];
            int[,] Value = new int[RowIndex, ColumnIndex];
            table.CopyRank(ref Rank);
            table.CopyValue(ref Value);
            using (StreamWriter output = new StreamWriter(@"C:\Users\Lera\source\repos\CW5\Part2\result.txt", false, System.Text.Encoding.Default)) {
                for (int i = 0; i < RowIndex; i++)
                {
                    for (int j = 0; j < ColumnIndex; j++)
                    {
                        output.Write($"оценка:{Value[i, j]} ранг:{Rank[i, j]} ");

                    }
                    output.WriteLine();
                }
            }
        /*    using (StreamWriter output = new StreamWriter(@"C:\Users\Lera\source\repos\CW5\Part2\resultSort.txt", false, System.Text.Encoding.Default))

            {
                for (int rank = 1; rank <= RowIndex; rank++)
                    for (int j = 0; j < RowIndex; j++)
                    {
                        if (Rank[j, ColumnIndex - 1] == rank)

                        {
                            int res = j + 1;
                            output.WriteLine($"{res}");
                        }
                    }
            }*/
        }


        private void printRank(TPR table)
        {
            int[,] Rank = new int[RowIndex,ColumnIndex];
            table.CopyRank(ref Rank);
            Form2 f2 = new Form2();
           
            f2.dataGridView1.ColumnCount = ColumnIndex+1;
            f2.dataGridView1.RowCount = RowIndex+1;

            for (int i = 0; i < ColumnIndex; i++)
            {
                f2.dataGridView1.Columns[i].HeaderText = $"P{i}";
                for (int j = 0; j < RowIndex; j++)
                {
                    f2.dataGridView1.Rows[j].HeaderCell.Value = $"{j + 1}";
                    f2.dataGridView1.Rows[j].Cells[i].Value = Rank[j,i];
                }
            }
            f2.dataGridView2.RowCount = RowIndex + 1;
            f2.dataGridView2.Columns[0].HeaderText = "ранжировка";

            for (int rank = 1; rank <= RowIndex; rank++)
                for (int j = 0; j < RowIndex; j++)
                {
                    if (Rank[j, ColumnIndex-1] == rank)
                        f2.dataGridView2.Rows[rank - 1].Cells[0].Value = j+1;
                }

            f2.ShowDialog();
            printInFile(table);

        }

       
    }
}
