using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part5
{
    public class SimplexMethod
    {
        private struct MainElem
        {
            public int Row, Column;
        }
        private const int Precision = 10;

        private double[,] simplexTable;//симплекс таблица
        private int[] columns;
        private int[] basis;
        private List<Variable> variableList;//таблица пременных
        private bool isMaximize_;//найти минимум или максимум
        private StringBuilder outputText = new StringBuilder();//вывод в форму информации
        private StringBuilder outputFileText = new StringBuilder();//вывод в файл
        private Expression expression;//само выражение

        public SimplexMethod(string[] lines)
        {
            try
            {
                expression = new Expression(lines);
                isMaximize_ = lines[1].Contains("<=");//определяем, находим минимум или максимум
                FillSimplexMatrix(expression, expression.constraints);//заполняем симплекс-таблицу
                foreach (var v in variableList)
                {
                    v.Value = 0;
                }

                for (int i = 0; i < lines.Length; i++)
                {
                    if (i == 0)
                    {
                        outputText.AppendLine((isMaximize_ ? "Найти максимум :" : "Найти минимум :") + lines[i]);
                        outputFileText.AppendLine((isMaximize_ ? "Найти максимум :" : "Найти минимум :") + lines[i]);
                    }
                    else
                    {
                        outputText.AppendLine(lines[i]);
                        outputFileText.AppendLine(lines[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void FillSimplexMatrix(Expression targetFunc, List<Constraint> constraints)
        {
            int row = 0;
            int col;
            int basisInd = 0;
            variableList = targetFunc.Variables;
            if (isMaximize_)//если находим максимум функции
            {
                simplexTable = new double[1 + constraints.Count, 1 + variableList.Count + constraints.Count];
                basis = new int[constraints.Count];
                columns = new int[variableList.Count];
                foreach (Constraint c in constraints)//приводим таблицу к виду, необходимую для алгоритма
                {
                    col = 0;
                    foreach (Variable v in variableList)
                    {
                        simplexTable[row, col] = c.Сoefficient(v);
                        col++;
                    }
                    basis[basisInd++] = col + row;
                    simplexTable[row, col + row] = 1;//заполняем столбец базисной переменной
                    simplexTable[row, simplexTable.GetLength(1) - 1] = c.Value;
                    row++;
                }
                col = 0;
                foreach (Variable v in variableList)
                {
                    simplexTable[row, col++] = targetFunc.Сoefficient(v);
                }
            }
            else
            {
                // если задача на минимум, то алгоритм поменяется лишь транспонированием матрицы
                simplexTable = new double[1 + variableList.Count, 1 + variableList.Count + constraints.Count];
                basis = new int[variableList.Count];
                columns = new int[constraints.Count];
                foreach (Variable v in variableList)
                {
                    col = 0;
                    foreach (Constraint c in constraints)
                    {
                        simplexTable[row, col] = c.Сoefficient(v);
                        col++;
                    }
                    basis[basisInd++] = col + row;
                    simplexTable[row, col + row] = 1;
                    simplexTable[row, simplexTable.GetLength(1) - 1] = targetFunc.Сoefficient(v);
                    row++;
                }
                col = 0;
                foreach (Constraint c in constraints)
                {
                    simplexTable[row, col++] = c.Value;
                }
            }
            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] = i;
            }
        }

        public string Show()
        {
            return outputText.ToString();
        }

        public string WriteToFile()
        {
            return outputFileText.ToString();
        }

        private string GetSimplexMatrixStepOutput()//вывод матрицы во время вычислений
        {
            StringBuilder Matrix = new StringBuilder();

            for (int i = 0; i < simplexTable.GetLength(0); i++)
            {
                for (int j = 0; j < simplexTable.GetLength(1); j++)
                {
                    Matrix.Append($"{simplexTable[i, j],7:0.###}");
                }
                Matrix.Append("\n");
            }

            return Matrix.ToString();
        }



        public double SimplexCalculate()
        {
            outputText.AppendLine($"{GetSimplexMatrixStepOutput()}");
            while (DoAlgorithm())
            {
                outputText.AppendLine($"{GetSimplexMatrixStepOutput()}");
            };

            outputText.AppendLine($"{GetSimplexMatrixStepOutput()}");

            if (isMaximize_)
            {
                for (int i = 0; i < basis.Length; i++)
                {
                    if (basis[i] < variableList.Count)
                    {
                        variableList[basis[i]].Value = simplexTable[i, simplexTable.GetLength(1) - 1];
                    }
                }
            }
            else
            {
                for (int i = 0; i < variableList.Count; i++)
                {
                    variableList[i].Value = -simplexTable[simplexTable.GetLength(0) - 1, i + columns.Length];
                }
            }

            var result = -Math.Round(simplexTable[simplexTable.GetLength(0) - 1, simplexTable.GetLength(1) - 1], Precision);

            outputText.AppendLine("Решение:");
            outputFileText.AppendLine("Решение:");
            foreach (Variable v in expression.Variables)
            {
                outputFileText.AppendLine($"{v} = {v.Value:0.###}");
                outputText.AppendLine($"{v} = {v.Value:0.###}");
            }
            outputFileText.AppendLine($"Значение целевой функции:  {result:0.###}");
            outputText.AppendLine($"Значение целевой функции:  {result:0.###}");

            return result;
        }



        private bool DoAlgorithm()
        {
            MainElem pivot = SelectMainElement();//находим ведущий элемент

            if (pivot.Column < 0 && pivot.Row < 0) return false;
            for (int col = 0; col < simplexTable.GetLength(1); col++)
            {
                if (col != pivot.Column)
                {
                    simplexTable[pivot.Row, col] =
                        Math.Round(simplexTable[pivot.Row, col] / simplexTable[pivot.Row, pivot.Column], Precision);//делим ведущую строку на ведущий элемент
                }
            }

            simplexTable[pivot.Row, pivot.Column] = 1;

            //преобразование матрицы
            for (int row = 0; row < simplexTable.GetLength(0); row++)
            {
                if (row == pivot.Row) continue;
                for (int col = 0; col < simplexTable.GetLength(1); col++)
                {
                    if (col != pivot.Column)
                    {
                        simplexTable[row, col] =
                            Math.Round(simplexTable[row, col] - (simplexTable[pivot.Row, col] * simplexTable[row, pivot.Column]),
                                Precision);
                    }
                }

                simplexTable[row, pivot.Column] = 0;
            }

            int temp = basis[pivot.Row];
            basis[pivot.Row] = pivot.Column;
            for (int i = 0; i < columns.Length; i++)
            {
                if (columns[i] != pivot.Column) continue;
                columns[i] = temp;
                break;
            }

            for (int col = 0; col < simplexTable.GetLength(1) - 1; col++)
            {
                if (simplexTable[simplexTable.GetLength(0) - 1, col] > 0)
                {
                    return true;
                }
            }

            return false;
        }

        private MainElem SelectMainElement()//находим максимальный по модулю элемент  запоминаем строку со столбцом
        {
            MainElem mainElem = new MainElem();
            mainElem.Column = -1;
            mainElem.Row = -1;
            double minValue = double.MinValue;
            foreach (var column in columns)
            {
                if (simplexTable[simplexTable.GetLength(0) - 1, column] < 0) continue;
                double coef = Math.Round(GetCoef(column, out var row) * simplexTable[simplexTable.GetLength(0) - 1, column], Precision);
                if (coef < minValue) continue;
                minValue = coef;
                mainElem = new MainElem();
                mainElem.Column = column;
                mainElem.Row = row;
            }
            return mainElem;
        }

        private double GetCoef(int col, out int row)
        {
            row = -1;
            double min = double.MaxValue;
            for (int r = 0; r < simplexTable.GetLength(0) - 1; r++)
            {
                if (simplexTable[r, col] < 0) continue;
                double m = Math.Round(simplexTable[r, simplexTable.GetLength(1) - 1] / simplexTable[r, col], Precision);
                if (m > min) continue;
                min = m;
                row = r;
            }
            return min;
        }
    }
}
