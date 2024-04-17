using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkWearOnFabric
{
    public partial class DocRedactorForm : Form
    {
        public DocRedactorForm()
        {
            InitializeComponent();
        }

      

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Red;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.White;
        }
        Point lastPoint;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void buttonPostuplenie_Click(object sender, EventArgs e)
        {
            if (textBoxDate.Text == "")
            {
                MessageBox.Show("Введите бортовой номер");
                return;
            }

            if (textBoxDocNum.Text == "")
            {
                MessageBox.Show("Введите модель");
                return;
            }

            if (textBoxProvider.Text == "")
            {
                MessageBox.Show("Введите авиакомпанию");
                return;
            }


            if (textBoxQuantity.Text == "")
            {
                MessageBox.Show("Введите вместимость");
                return;
            }
            if (textBoxPrice.Text == "")
            {
                MessageBox.Show("Введите год выпуска");
                return;
            }

            DB db = new DB();

            // Проверяем корректность введенной даты
            DateTime dateValue;
            /*if (!DateTime.TryParse(textBoxDate.Text, out dateValue))
            {
                MessageBox.Show("Неверный формат даты. Введите дату в формате ДД.ММ.ГГГГ");
                return;
            }
            int quantityValue;
            if (!int.TryParse(textBoxQuantity.Text, out quantityValue))
            {
                MessageBox.Show("Количество должно быть целым числом.");
                return;
            }*/
            int priceValue;
            if (!int.TryParse(textBoxPrice.Text, out priceValue))
            {
                MessageBox.Show("Год выпуска должен быть пример: 2024.");
                return;
            }/*
            int docNumValue;
            if (!int.TryParse(textBoxDocNum.Text, out docNumValue))
            {
                MessageBox.Show("Номер документа должно быть целым числом.");
                return;
            }*/


            MySqlCommand command = new MySqlCommand("INSERT INTO `infoaircraft`(`onboardnum`, `model`, `airline`, `maxcapacity`, `release`) VALUES (@onboardnum, @model, @airline, @maxcapacity, @release)", db.getConnection());

            command.Parameters.AddWithValue("@onboardnum", textBoxDate.Text);
            command.Parameters.AddWithValue("@model", textBoxDocNum.Text);
            command.Parameters.AddWithValue("@airline", textBoxProvider.Text);
            command.Parameters.AddWithValue("@maxcapacity", textBoxQuantity.Text);
            command.Parameters.AddWithValue("@release", textBoxPrice.Text);
            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Данные внесены");

                // Очистка содержимого всех TextBox
                textBoxDate.Text = "";
                textBoxDocNum.Text = "";
                textBoxProvider.Text = "";
                textBoxQuantity.Text = "";
                textBoxPrice.Text = "";
            }
            else
            {
                MessageBox.Show("Ошибка");
            }

            db.closeConnection();
        }

        private void buttonInfoOfForm_Click(object sender, EventArgs e)
        {
            if (textBoxNameForm.Text == "")
            {
                MessageBox.Show("Введите дату");
                return;
            }

            if (textBoxType.Text == "")
            {
                MessageBox.Show("Введите номер рейса");
                return;
            }

            if (textBoxPriceOfUnit.Text == "")
            {
                MessageBox.Show("Введите маршрут");
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите время вылета");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Введите время прибытия");
                return;
            }
            if (textBox3.Text == "")
            {
                MessageBox.Show("Введите цену");
                return;
            }
            DateTime dateValue;
            if (!DateTime.TryParse(textBoxNameForm.Text, out dateValue))
            {
                MessageBox.Show("Неверный формат даты. Введите дату в формате ГГГГ.ММ.ДД");
                return;
            }
            DateTime timeValue;
            if (!DateTime.TryParse(textBox1.Text, out timeValue))
            {
                MessageBox.Show("Неверный формат времени. Введите дату в формате 12:00:00");
                return;
            }
            DateTime time1Value;
            if (!DateTime.TryParse(textBox2.Text, out time1Value))
            {
                MessageBox.Show("Неверный формат времени. Введите дату в формате 12:00:00");
                return;
            }



            int priceOfUnitValue;

            /*if (!int.TryParse(textBoxNameForm.Text, out priceOfUnitValue))
            {
                MessageBox.Show("Дата должно быть целым числом.");
                return;
            }
            int t1Value;

            if (!int.TryParse(textBox1.Text, out t1Value))
            {
                MessageBox.Show("Время вылета пример:12:00:00.");
                return;
            }
            int t2Value;

            if (!int.TryParse(textBox2.Text, out t2Value))
            {
                MessageBox.Show("Время прибытия пример:12:00:00.");
                return;
            }*/
            int t3Value;

            if (!int.TryParse(textBox3.Text, out t3Value))
            {
                MessageBox.Show("Цена должно быть целым числом.");
                return;
            }



            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `flyroute`(`date`, `numflight`, `route`, `departuretime`,`arrivaltime`,`price`) VALUES (@date, @numflight, @route, @departuretime, @arrivaltime, @price)", db.getConnection());

            command.Parameters.AddWithValue("@date", textBoxNameForm.Text);
            command.Parameters.AddWithValue("@numflight", textBoxType.Text);
            command.Parameters.AddWithValue("@route", textBoxPriceOfUnit.Text);
            command.Parameters.AddWithValue("@departuretime", textBox1.Text);
            command.Parameters.AddWithValue("@arrivaltime", textBox2.Text);
            command.Parameters.AddWithValue("@price", textBox3.Text);

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Данные внесены");

                // Очистка содержимого всех TextBox
                textBoxNameForm.Text = "";
                textBoxType.Text = "";
                textBoxPriceOfUnit.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";

            }
            else
            {
                MessageBox.Show("Ошибка");
            }



            db.closeConnection();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
