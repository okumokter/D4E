using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using D4ECore.Monster;
using System.Data.SqlClient;
using System.Diagnostics;

namespace D4ECoreWPFTest
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadByKeyword(textBox.Text);
        }

        private void LoadById(int id)
        {
            SqlConnection con = new SqlConnection();
            var sb = new SqlConnectionStringBuilder()
            {
                DataSource = "C64",
                InitialCatalog = "D4E",
                IntegratedSecurity = true
            };
            con.ConnectionString = sb.ConnectionString;
            con.Open();

            var loader = new MonsterPatternLoader();
            var randomInt = id;
            var monster = loader.Load(con, randomInt);
            MonsterFrame.ViewModel.Model = monster;

            con.Close();
        }

        private void LoadRandom()
        {
            int id = new Random().Next(1, 489);
            LoadById(id);
        }

        private void LoadAll()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 1; i < 489; i++)
            {
                LoadById(i);
            }
            MessageBox.Show($"Loading all monsters took {sw.ElapsedMilliseconds}ms of time.");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            LoadRandom();
        }

        private void LoadByKeyword(string keyword)
        {
            SqlConnection con = new SqlConnection();
            var sb = new SqlConnectionStringBuilder()
            {
                DataSource = "C64",
                InitialCatalog = "D4E",
                IntegratedSecurity = true
            };
            con.ConnectionString = sb.ConnectionString;
            con.Open();

            var loader = new MonsterPatternLoader();
            var monster = loader.Load(con, keyword);
            if (monster == null)
                MessageBox.Show($"Monster '{keyword}' not found.");
            else
                MonsterFrame.ViewModel.Model = monster;

            con.Close();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            LoadByKeyword(textBox.Text);
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (new Key[] { Key.Return, Key.Enter }.Contains(e.Key))
            {
                LoadByKeyword(textBox.Text);
                textBox.SelectAll();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            LoadAll();
        }
    }
}
