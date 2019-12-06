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
        private int mark;
        private int cur_rank;
        private int prev_rank;
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
    class TPR
    {
        private Element[,] table;
        private int Rows, Columns;




        public TPR(int rows, int colums, int[,] mark_table, int[] rankArray)
        {
            //на вводе сделать массив приоритетов(от исходной матрицы), относительно которго забъем рабочую матрицу
          /*  int[,] mark_table = new int[,] { { 4, 5, 4, 3 }, { 3, 3, 5, 4 }, { 4, 4, 3, 3 }, { 3, 4, 3, 5 }, { 4, 5, 3, 4 }, { 3, 3, 5, 5 } };
            int[] rankArray = new int[] { 1, 2, 3, 4 };*/
            this.Rows = rows;
            this.Columns = colums;

            Element[,] array = new Element[rows, colums];

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
            InitFirstColumn();



            // 

            for (int i = 1; i < Columns; i++)
            {

                int current_rank = 0;
                SetFirstRank(i);

                for (int r = 1; r <= Rows; r++)
                {

                    SetRank(i, r, ref current_rank);
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

                if (column != 0)
                { table[i, column].Prev_rank = table[i, column - 1].Cur_rank; table[i, column].Cur_rank = table[i, column].Prev_rank; }
                else { table[i, column].Prev_rank = 1; table[i, column].Cur_rank = 1; }
            }
        }

        void SetRank(int column, int rnk, ref int current_rank)
        {

            int min = 0;
            int count = 0;
            int max = find_max(column, rnk, ref count);
            if (count > 1)
            {
                DegAnotherRank(column, min, rnk, max);
                compare_with_max_same_rank(max, column, rnk, ref min, count);
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


