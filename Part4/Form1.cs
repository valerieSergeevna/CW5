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

namespace Part4
{
    public partial class Form1 : Form
    {
        int Length = 0;
        double Time;
        string inputDeclined;
        string inputAccept;
        string inputComplited;
        private double StopTime = 10;
        private double processingTime = 0;
        private string req, type, reqType;
        SystemImitation RequestaImmitation;
        bool check = true;

        

        public Form1()
        {
            InitializeComponent();
            RequestaImmitation = new SystemImitation();

            
        }

        //вывод принятых заявок и откланенных с их идентификаторами
        void show(int isReq)
        {

            if (isReq == 1)
            {
                
                richTextBox1.AppendText("Заявка " + RequestaImmitation.Id + " принята\nтип: " + reqType + " \nДень " + Time + '\n' + '\n');
                inputAccept += "Заявка " + RequestaImmitation.Id + " принята\nтип: " + reqType + " \nДень " + Time + '\n'+ '\n';
               richTextBox1.Select(Length, this.richTextBox1.Text.Length);
                richTextBox1.SelectionColor = Color.Green;
                Length = this.richTextBox1.Text.Length ;
            }
            else if (isReq == 2) {
                richTextBox1.AppendText("Заявка " + RequestaImmitation.Id + " отклонена\nДень " + Time + '\n'+ '\n');
                richTextBox1.Select(Length, this.richTextBox1.Text.Length);
                inputDeclined += "Заявка " + RequestaImmitation.Id + " отклонена\nДень " + Time + '\n' + '\n';
                richTextBox1.SelectionColor = Color.Red;
                Length = this.richTextBox1.Text.Length ;
            }

            label1.Text = "Наблюдаемые параметры:\n" + " валовая прибыль: " + RequestaImmitation.Profit + "\nиздержки: "+ RequestaImmitation.Costs;
        }

        //вывод выпоненных заявок
        public void showComlete(string req)
        {
            
            if (req.Length > 0)
            {
                richTextBox1.AppendText("Заявка " + req + "\nтип: " + type + " ВЫПОЛНЕНА " + '\n' + '\n');
                richTextBox1.Select(Length, this.richTextBox1.Text.Length);
                richTextBox1.SelectionColor = Color.Blue;
                Length = this.richTextBox1.Text.Length;
                inputComplited += "Заявка " + req + "\nтип: " + type + " ВЫПОЛНЕНА\n " + "Стоимость: " + RequestaImmitation.CurCost + '\n' + '\n';
            }
        }

        //при каждом изменении таймер, срабатывает метод иммитации
        private void timer1_Tick(object sender, EventArgs e)
        {
            Time += (double)timer1.Interval/1000 ;
            label2.Text = "Дни: " + ((int)Time).ToString() ;

            if (processingTime < StopTime)
            {
                req = "";
                type = "";
                reqType = "";
                processingTime += ((double)timer1.Interval / 1000);
                RequestaImmitation.Imitate((double)timer1.Interval / 1000, out int isReq, ref req, ref type, ref reqType);
                if (processingTime % 30 == 0)//если прошел месяц, то выплачиваем 70000(аренда, охранные системы и тд прибовляем эту сумму к издержкам и вычитаем из прибыли )
                {
                    RequestaImmitation.Costs += 70000;
                    RequestaImmitation.Profit -= 70000;
                }
                showComlete(req);
                show(isReq);




            }
            else
            {
                timer1.Enabled = false;
                //вывод в файл результатов
                using (StreamWriter AcceptedInput = new StreamWriter(@"C:\Users\Lera\source\repos\CW5\Accepted.txt"))
                {
                    AcceptedInput.WriteLine(inputAccept);
                }

                using (StreamWriter DeclinedInput = new StreamWriter(@"C:\Users\Lera\source\repos\CW5\Declined.txt"))
                {
                    DeclinedInput.WriteLine(inputDeclined);
                }

                using (StreamWriter ComplitedInput = new StreamWriter(@"C:\Users\Lera\source\repos\CW5\Complited.txt"))
                {
                    ComplitedInput.WriteLine(inputComplited);
                }

                richTextBox1.AppendText("\nПринято " + RequestaImmitation.Accepted + " заявок");
                richTextBox1.AppendText("\nВыполнено " + RequestaImmitation.Compl + " заявок");
                richTextBox1.AppendText("\nОткланено " + RequestaImmitation.Declined + " заявок");
            }
            //panel1.Refresh();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (check)

                {
                    StopTime = Convert.ToInt32(textBox1.Text);
                    check = false;
                    richTextBox1.Clear();
                }
                timer1.Enabled = true;
                
            }
            catch (Exception ex)
            {
                richTextBox1.Text = "Введите количество наблюдаемых дней!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
