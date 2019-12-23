using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Part3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int ColumnIndex = 0;

        int RowIndex = 0;
        bool check = false;

        private void button1_Click(object sender, EventArgs e)
        {
          
            if (check)
            {
                richTextBox1.Clear();
                int n = ColumnIndex;
              //  int n = 4;
              /*  int[,] adjMatrix = {
                     { 0, 7, 0, 0, 7,3 },
                     { 3, 0, 1, 0,0,8 },
                     { 0, 8, 0, 1,0,1 },
                     { 0, 0, 5, 0, 3,5 },
                     { 8,0,0,2,0,2},
                     {2,7,0,1,0,0 }
                     };
                     */
               /* int[,] adjMatrix = {
                { 0, 10, 15, 20 },
                { 10, 0, 35, 25 },
                { 15, 35, 0, 30 },
                { 20, 25, 30, 0 }
                };*/
               /* int[,] adjMatrix = {
                     { 0, 3, 0, 0, 3,3 },
                     { 3, 0, 0, 0,0,3 },
                     { 0, 8, 0, 8,0,3 },
                     { 0, 0, 1, 0, 3,5 },
                     { 1,0,0,1,0,4},
                     {0,3,1,0,0,0 }
                     };*/
                     //заполняем матрицу смежности из датыгридвью
                int[,] adjMatrix = new int[n, n];
                  for (int i = 0; i < n; i++)
                  {
                      for (int j = 0; j < n; j++)
                      {
                          adjMatrix[j, i] = Convert.ToInt32(dataGridView1.Rows[j].Cells[i].Value);
                      }
                  }

                  //заполняем граф относительно матрицы смежности
                Graph graph = new Graph();
                graph = graph.CreateGraph(n, adjMatrix);

                TSP nearestNeighbor = new TSP();
                nearestNeighbor.graph = graph;
                nearestNeighbor.FindShortestPathFromAll();//находим кратчайший цикл 
                if (nearestNeighbor.minDistance != 0)

                {
                    foreach (Vertex vertex in nearestNeighbor.ShortestPath)
                    {
                        richTextBox1.AppendText(vertex.index + " ");
                    }

                    richTextBox1.AppendText("\nDistance: " + nearestNeighbor.minDistance);
                }
                else
                    richTextBox1.AppendText("нет цикла");
            }
            else
            {
                richTextBox1.AppendText("Создайте и заполните матрицу смежности!!" );
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                try
                {

                    if (!check)
                    {
                        dataGridView1.AllowUserToAddRows = false;
                        check = true;
                        for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
                            dataGridView1.Columns.Add($"P{ColumnIndex}", $"P{ColumnIndex++}");

                        for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
                        { dataGridView1.Rows.Add(); dataGridView1.Rows[i].HeaderCell.Value = $"{i + 1}"; RowIndex++; }
                    }
                }
                catch (Exception ex)
                {
                    richTextBox1.AppendText("Заполните матрицу смежности!!");
                     check = false;
                }
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
