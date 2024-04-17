using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WorkWearOnFabric
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {

            InitializeComponent();
            this.passField.AutoSize = false;
            this.passField.Size = new Size(this.passField.Size.Width, 26);
            userNameField.Text = "Введите имя";
            userNameField.ForeColor = Color.Gray;
            userSurnameField.Text = "Введите фамилию";
            userSurnameField.ForeColor = Color.Gray;
            loginField.Text = "Введите логин";
            loginField.ForeColor = Color.Gray;

          
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void userNameField_Enter(object sender, EventArgs e)
        {
            if (userNameField.Text == "Введите имя")
            {
                userNameField.Text = "";
                userNameField.ForeColor = Color.Black;

            }
        }

        private void userNameField_Leave(object sender, EventArgs e)
        {
            if (userNameField.Text == "")
            {
                userNameField.Text = "Введите имя";
                userNameField.ForeColor = Color.Gray;
            }

        }

        private void userSurnameField_Enter(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "Введите фамилию")
            {
                userSurnameField.Text = "";
                userSurnameField.ForeColor = Color.Black;

            }

        }

        private void userSurnameField_Leave(object sender, EventArgs e)
        {
            
                if (userSurnameField.Text == "")
                {
                    userSurnameField.Text = "Введите фамилию";
                    userSurnameField.ForeColor = Color.Gray;
                }
            
        }

        private void loginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "Введите логин")
            {
                loginField.Text = "";
                loginField.ForeColor = Color.Black;

            }
        }

        private void loginField_Leave(object sender, EventArgs e)
        {
             if (loginField.Text == "")
            {
                loginField.Text = "Введите логин";
                loginField.ForeColor = Color.Gray;

            }
        }


        private void buttonRegister_Click(object sender, EventArgs e)

        {
            string password = "123";


            if (userNameField.Text == "Введите имя")
            {
                MessageBox.Show("Введите имя");
                return;
            }

            if (userSurnameField.Text == "Введите фамилию")
            {
                MessageBox.Show("Введите фамилию");
                return;
            }

            if (passField.Text == "")
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            if (loginField.Text == "Введите логин")
            {
                MessageBox.Show("введите логин");
                return;
            }
            if (isUserExists())
                return;

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`, `name`, `surename`, `role`) VALUES (@login, @pass, @name, @surename, @role)", db.getConnection());

            command.Parameters.AddWithValue("@login", loginField.Text);
            command.Parameters.AddWithValue("@pass", passField.Text);
            command.Parameters.AddWithValue("@name", userNameField.Text);
            command.Parameters.AddWithValue("@surename", userSurnameField.Text);
            command.Parameters.AddWithValue("@role", "user");

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт был создан");

                DialogResult dialogResult = MessageBox.Show("Вы администратор?", "Подтверждение администратора", MessageBoxButtons.YesNo);

                string roleValue = "user";

                if (dialogResult == DialogResult.Yes)
                {
                    string enteredPassword = "";

                    while (true)
                    {
                        enteredPassword = Microsoft.VisualBasic.Interaction.InputBox("Введите пароль:", "Подтверждение администратора", "", -1, -1);

                        if (enteredPassword == password)
                        {
                            roleValue = "admin";
                            break;
                        }
                        else if (enteredPassword == "") // Пользователь нажал "Отмена"
                        {
                            
                            roleValue = "user";
                            break;
                        }
                        else
                        {
                            DialogResult retryResult = MessageBox.Show("Неверный пароль. Попробовать снова?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                            if (retryResult == DialogResult.No)
                            {
                                roleValue = "user";
                                break;
                            }
                        }
                    }
                }

                MySqlCommand command1 = new MySqlCommand("UPDATE `users` SET `role` = @roleValue WHERE `login` = @login", db.getConnection());
                command1.Parameters.AddWithValue("@roleValue", roleValue);
                command1.Parameters.AddWithValue("@login", loginField.Text);

                if (command1.ExecuteNonQuery() == 1)
                {
                    if (roleValue == "admin")
                    {
                        MessageBox.Show("Здравствуй, администратор!");
                    }
                    else
                    {
                        MessageBox.Show("Здравствуй, пользователь");
                    }
                }
            }
            else
            {
                MessageBox.Show("Аккаунт не был создан");
            }

            db.closeConnection();
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }



        public Boolean isUserExists()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE login = @uL", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginField.Text;


            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("такой логин занят");
                return true;
            }
               
            else
                return false;
        }

        private void loginLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Red;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.White;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    
}
