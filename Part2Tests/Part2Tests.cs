using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Part2;

namespace Part2Tests
{
    [TestClass]
    public class Part2Tests
    {
        [TestMethod]
        public void RankTest()
        {
            int[,] mark_table = new int[,] { { 4, 5, 4, 3 }, { 3, 3, 5, 4 }, { 4, 4, 3, 3 }, { 3, 4, 3, 5 }, { 4, 5, 3, 4 }, { 3, 3, 5, 5 } };
            int[] rankArray = new int[] { 1, 2, 3, 4 };
            TPR table = new TPR(6, 4, mark_table, rankArray);
            table.PriorityAlgorythm();

            int[] rankCheck = { 1, 6, 3, 4, 2, 5 };
           
            int[] rankCurArray = table.GetLastColumn();
            for (int i = 0; i< 6; i++)
                Assert.AreEqual(rankCurArray[i], rankCheck[i]);

        }

        [TestMethod]
        public void ObjectSortTest()
        {
            int[,] mark_table = new int[,] { { 3,3 }, { 2,1 }, { 1,1} };
            int[] rankArray = new int[] { 2,1 };
            TPR table = new TPR(3, 2, mark_table, rankArray);
            table.PriorityAlgorythm();

            int[] sortCheck = { 1,2,3};

            int[] objCurArray = new int[3];

            int[] rankCurArray = table.GetLastColumn();
            for (int rank = 1; rank <= 3; rank++)
                for (int j = 0; j < 3; j++)
                {
                    if (rankCurArray[j] == rank)
                        objCurArray[rank-1] = j + 1;
                }
            for (int i = 0; i < 3; i++)
                Assert.AreEqual(sortCheck[i], objCurArray[i]);

        }

        [TestMethod]
        public void PriorityExp()
        {
            try
            {
                int[,] mark_table = new int[,] { { 4, 5, 4, 3 }, { 3, 3, 5, 4 }, { 4, 4, 3, 3 }, { 3, 4, 3, 5 }, { 4, 5, 3, 4 }, { 3, 3, 5, 5 } };
                int[] rankArray = new int[] { 1, 2, 3, 7 };
                TPR table = new TPR(6, 4, mark_table, rankArray);
                table.PriorityAlgorythm();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Индекс находился вне границ массива.");
            }
        }
        



        }
}
