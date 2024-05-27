using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace ProjLab6V2
{
    public partial class Form1 : Form
    {
        Player player=new Player("User_");
        List<int> Spots = new List<int>();
        List<Round> allRound;
        int number_round;
        public void makeSpot(int spot)
        {
            if (Spots.Count < 15 && !Spots.Contains(spot)) Spots.Add(spot);
            else Spots.Remove(spot);
        }
        public void updateDataGridView(SqlConnection connection)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT TOP 8 name, the_biggest_win FROM Players WHERE the_biggest_win>0 ORDER BY the_biggest_win DESC", connection.ConnectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        public Form1()
        {
            InitializeComponent();
            
        }
        public void makeButton(object sender)
        {
            Button button = sender as Button;
            string num = button.Text;
            makeSpot(int.Parse(num));
            if (button.BackColor == SystemColors.Control) button.BackColor = Color.Green;
                else button.BackColor = SystemColors.Control;
            //label3.Text = player.Spots.Count().ToString();
        }
        public void makeRoundButton(object sender)
        {
            Button button = sender as Button;
            string name = button.Text;
            var res = name.ToCharArray().Where(n => char.IsDigit(n)).ToArray();
            var res1 = int.Parse(new string(res));
            if (Spots.Count() != 0)
            {
                var connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\rtippo\ProjLab6V2\ProjLab6V2\DatabasePlayers.mdf; Integrated Security = True");
                connection.Open();
                var match = player.makeMatch(int.Parse(textBox1.Text), res1, Spots.ToArray());
                var spots = string.Join(" ", Spots);
                SqlCommand command = new SqlCommand("SELECT TOP 1 Id FROM Players ORDER BY Id DESC", connection);
                // Выполнение команды и получение результата
                int lastId = Convert.ToInt32(command.ExecuteScalar());
                SqlCommand command1 = new SqlCommand($"INSERT INTO [Matchs] (bet, number_round, list_spot, player_id) VALUES ({int.Parse(textBox1.Text)}, 1, N'{spots}', {lastId})", connection);
                command1.ExecuteNonQuery();
                var rounds = match.makeRound(int.Parse(textBox1.Text), res1, Spots.ToArray());
                var win = 0;
                for (int i = 0; rounds.Count > i; i++) win += rounds[i].win_amount;
                label2.Text = win.ToString();
                label4.Text = (int.Parse(label4.Text) + int.Parse(label2.Text) - int.Parse(textBox1.Text)*res1).ToString();
                SqlCommand command2 = new SqlCommand($"UPDATE Players SET money_amount={label4.Text} WHERE id={lastId}", connection);
                command2.ExecuteNonQuery();
                SqlCommand command4 = new SqlCommand("SELECT TOP 1 the_biggest_win FROM Players ORDER BY Id DESC", connection);
                // Выполнение команды и получение результата
                int last_the_biggest_win = Convert.ToInt32(command4.ExecuteScalar());
                if (last_the_biggest_win < win)
                {
                    SqlCommand command3 = new SqlCommand($"UPDATE Players SET the_biggest_win={int.Parse(label2.Text)} WHERE id={lastId}", connection);
                    command3.ExecuteNonQuery();
                }
                SqlCommand command6 = new SqlCommand("SELECT TOP 1 Id FROM Matchs ORDER BY Id DESC", connection);
                // Выполнение команды и получение результата
                int lastIdMatch = Convert.ToInt32(command6.ExecuteScalar());
                
                foreach (Round indexer in rounds)
                {
                    SqlCommand command5 = new SqlCommand($"INSERT INTO [Rounds] (balls, win_amount, match_id) VALUES (N'{string.Join(" ", indexer.balls)}', '{indexer.win_amount}', {lastIdMatch})", connection);
                    command5.ExecuteNonQuery();
                    
                }
                number_round=rounds.Count;
                allRound = rounds;
                button81.Enabled = false;
                button82.Enabled = false;
                button_83.Enabled = false;
                button84.Enabled = false;
                button85.Enabled = false;
                button_87.Enabled = false;
                timer1.Start();
                updateDataGridView(connection);
                
                //player.updateDataPlayer(int.Parse(label4.Text), win);//запросы через эту строку
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string result = Microsoft.VisualBasic.Interaction.InputBox(Prompt:"Введите свой Nickname:",Title:"Кено");
            var connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\rtippo\ProjLab6V2\ProjLab6V2\DatabasePlayers.mdf; Integrated Security = True");
            connection.Open();
            if (result != "") label7.Text = "Игрок : " + result;
            else
            {
                SqlCommand command = new SqlCommand("SELECT TOP 1 Id FROM Players ORDER BY Id DESC", connection);
                // Выполнение команды и получение результата
                int lastId = Convert.ToInt32(command.ExecuteScalar());
                label7.Text = "Игрок : User_"+(lastId+1);
                result= "User_"+(lastId+1).ToString();
            }
            updateDataGridView(connection);
            SqlCommand command1 = new SqlCommand($"INSERT INTO [Players] (name, money_amount, the_biggest_win) VALUES (N'{result}', 1000, 0)", connection);
            player.name = result;
            command1.ExecuteNonQuery();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            makeButton(sender);

        }

        private void button11_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            makeButton(sender);

        }

        private void button17_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button42_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button43_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button44_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button45_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button46_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button47_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button48_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button49_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button50_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button51_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button52_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button53_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button54_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button55_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button56_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button57_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button58_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button59_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button60_Click(object sender, EventArgs e)
        {
            makeButton(sender);

        }

        private void button61_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button62_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button63_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button64_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button65_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button66_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button67_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button68_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button69_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button70_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button71_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button72_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button73_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button74_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button75_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button76_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button77_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button78_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button79_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button80_Click(object sender, EventArgs e)
        {
            makeButton(sender);
        }

        private void button83_Click(object sender, EventArgs e)
        {
            makeRoundButton(sender);
        }

        private void button87_Click(object sender, EventArgs e)
        {
            Spots.Clear();
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Button) // Проверяем, что элемент управления типа Button
                {
                    Button button = (Button)control;
                    button.BackColor = SystemColors.Control; // Меняем цвет фона кнопки
                }
            }
            //label3.Text = player.Spots.Count().ToString();
        }

        private void button84_Click(object sender, EventArgs e)
        {
            makeRoundButton(sender);
        }

        private void button85_Click(object sender, EventArgs e)
        {
            makeRoundButton(sender);
        }

        private void button81_Click(object sender, EventArgs e)
        {
            textBox1.Text = (int.Parse(textBox1.Text) + 1).ToString();
        }

        private void button82_Click(object sender, EventArgs e)
        {
            if(int.Parse(textBox1.Text)>1) textBox1.Text = (int.Parse(textBox1.Text)-1).ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (number_round == 0)
            {
                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    Button button2 = (Button)control;
                    button2.ForeColor = SystemColors.ControlText;
                }
                button81.Enabled = true;
                button82.Enabled = true;
                button_83.Enabled = true;
                button84.Enabled = true;
                button85.Enabled = true;
                button_87.Enabled = true;
                timer1.Stop();
            }
            else
            {
                number_round--;
                var arr1 = allRound[number_round].balls;
                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    Button button2 = (Button)control;
                    button2.ForeColor = SystemColors.ControlText;
                }
                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    if (control is Button)
                    {
                        Button button1 = (Button)control;
                        if (Array.IndexOf(arr1, int.Parse(button1.Text)) != -1)
                            button1.ForeColor = Color.Magenta;
                    }
                }
            }
            
        }
    }
}
