using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Part4
{
    public partial class Form1 : Form
    {
        double Time;
        private double StopTime = 7300;
        private double processingTime = 0;
        SystemImitation RequestaImmitation;
        public Form1()
        {
            InitializeComponent();
            RequestaImmitation = new SystemImitation();
        }
        void show(int isReq)
        {
            if (isReq == 1)
                richTextBox1.Text +=  "Заявка "+ RequestaImmitation.Id + " принята " + Time + '\n';
            else if  (isReq == 2) richTextBox1.Text += "Заявка " + RequestaImmitation.Id +" откланена " + Time + '\n';

            label1.Text = "Наблюдаемые параметры:\n" + " валовая прибыль: " + RequestaImmitation.Profit + "\nиздержки: "+ RequestaImmitation.Costs;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Time += (double)timer1.Interval / 1000;
            
            if (processingTime < StopTime)
            {
                processingTime += Time;
                RequestaImmitation.Imitate((double)timer1.Interval / 1000, out int isReq);
                show(isReq);
            
            label2.Text = "Время: " + ((int)Time).ToString() + " д.";
            }
            else timer1.Enabled = false;
            //panel1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
