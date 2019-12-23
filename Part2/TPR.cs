using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Part2
{
    class Element
    {
        private int mark; //оценка
        private int cur_rank;//текущий ранг
        private int prev_rank;//ранг в предыдцщем столбце
        public Element(int mark)
        {
            this.mark = mark;
            cur_rank = 0;
            prev_rank = 0;
        }

        public int Mark
        {
            get { return mark; }
            set { }
        }

        public int Cur_rank
        {
            get { return cur_rank; }
            set { cur_rank = value; }
        }

        public int Prev_rank
        {
            get { return prev_rank; }
            set { prev_rank = value; }
        }

    }
    public class TPR
    {
        private Element[,] table;//таблица с оценками
        private int Rows, Columns;




        public TPR(int rows, int colums, int[,] mark_table, int[] rankArray)
        {
            
            this.Rows = rows;
            this.Columns = colums;

            Element[,] array = new Element[rows, colums];//заполняем таблицу оценок 

            for (int i = 0; i < rankArray.Length; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    array[j, rankArray[i] - 1] = new Element(mark_table[j, i]);
                }
            }


            this.table = array;
        }

        public void InitFirstColumn()
        {

            SetFirstRank(0);
            int current_rank = 0;
            for (int r = 1; r <= Rows; r++)
            {

                SetRank(0, r, ref current_rank);
            }


        }

      

        public void PriorityAlgorythm()
        {
            InitFirstColumn();//задаем ранги первому столбцу

            for (int i = 1; i < Columns; i++)
            {

                int current_rank = 0;
                SetFirstRank(i);//устанавливаем начальные ранги для элементов столбца

                for (int r = 1; r <= Rows; r++)
                {

                    SetRank(i, r, ref current_rank);//пересчитывваем ранги
                }
            }

        }

        void DegAnotherRank(int column, int min, int rnk, int max)
        {
            for (int i = 0; i < Rows; i++)
            {

                if (table[i, column].Cur_rank > rnk)
                    table[i, column].Cur_rank++;

            }
        }
        void SetFirstRank(int column)
        {
            for (int i = 0; i < Rows; i++)
            {

                if (column != 0) //если колонка не первая, то для текущего элемента записываем параметр предыдцщего ранга,
                    //как текущий ранг у элемента предыдущего столбца и его же присваеваем к параметру текцщий ранг у текущего элемента
                { table[i, column].Prev_rank = table[i, column - 1].Cur_rank;
                    table[i, column].Cur_rank = table[i, column].Prev_rank;
                }
                else { table[i, column].Prev_rank = 1; table[i, column].Cur_rank = 1; }//если это первая колонка, то все записываем как ранг 1
            }
        }

        void SetRank(int column, int rnk, ref int current_rank)
        {

            int min = 0;
            int count = 0;
            int max = find_max(column, rnk, ref count);//находим максимальную оценку для данного ранга
            if (count > 1)
            {
                DegAnotherRank(column, min, rnk, max);//увеличиваем ранги у всех элеменов, у которых ранг больше rnk 
                compare_with_max_same_rank(max, column, rnk, ref min, count);//сравниваем элементы с одинаковым рангом и
                //увеличиваем ранги у тех, у кого оценка меньше , чем max
            }



        }

        int find_max(int column, int rnk, ref int count)
        {
            int max = 0;
            for (int i = 0; i < Rows; i++)
            {
                if (table[i, column].Cur_rank == rnk) count++;

                if (table[i, column].Cur_rank == rnk && max < table[i, column].Mark)
                {
                    max = table[i, column].Mark;
                }

            }
            return max;
        }

        void compare_with_max_same_rank(int max, int column, int rnk, ref int min, int max_rank)
        {

            min = int.MaxValue;

            for (int i = 0; i < Rows; i++)
            {

                if (max > table[i, column].Mark && table[i, column].Cur_rank == rnk)
                {
                    table[i, column].Cur_rank++;

                }


            }

        }

        public int[] GetLastColumn()
        {

            int[] rankArray = new int[Rows];
             for (int i = 0; i< Rows; i++)
            {
                rankArray[i] = table[i, Columns-1].Cur_rank;
            }
            return rankArray;
        }

        public void CopyValue(ref int[,] ValueArray)
        {


            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    ValueArray[i, j] = table[i, j].Mark;
                }


            }
        }

        public void CopyRank(ref int[,] RankArray)
        {
            

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    RankArray[i,j] =  table[i, j].Cur_rank;
                }
                
                
            }
        }

    }

}


