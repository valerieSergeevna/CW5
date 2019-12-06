using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW5
{
    public partial class Form1 : Form
    {
        int count = 0;
        Lorry car = null;
        public Form1()
        {
            InitializeComponent();
        }

        bool CheckField()
        {

            if (!double.TryParse(textBox4.Text, out double num_d) || !int.TryParse(textBox2.Text, out int num_i)
                 || !double.TryParse(textBox3.Text, out double num))
            { label5.Text = $"Некорректно введены данные"; return false; }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (!CheckField()) return;
            if (count == 0)

            {
                car = new Lorry(Convert.ToDouble(textBox4.Text), Convert.ToInt32(textBox2.Text), textBox1.Text, Convert.ToDouble(textBox3.Text));
                label5.Text = $"Был создан объект\n родительский класс\n автомобиль с параметрами:\n количество цилиндров: {car.CylilndersCount} \n марка: {car.Model} \n мощность: {car.Power} \n производный класс грузовик:\n грузоподъемность: {car.CarryingCapacity}";
            }
            else
            {
                car.ChangeCarryingCapacity(Convert.ToDouble(textBox4.Text)); car.ChangeModel(textBox1.Text);
                label5.Text = $"Был изменен объект\n родительский класс автомобиль с параметрами:\n количество цилиндров: {car.CylilndersCount} \n марка: {car.Model} \n мощность: {car.Power} \n производный класс грузовик:\n грузоподъемность: {car.CarryingCapacity}";
            }
            count++;
        }
    }
}
