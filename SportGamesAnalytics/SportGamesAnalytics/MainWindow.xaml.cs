using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using static System.Convert;
using System.Security.AccessControl;


namespace SportGamesAnalytics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        
     


       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var configuration = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile("appsettings.json")
     .Build();
            string connString = configuration.GetConnectionString("DefaultConnection");
            using (var conn = new NpgsqlConnection(connString))
            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT name_sports FROM politech.data", conn))
                using (var reader = command.ExecuteReader())
                {
                        using (StreamWriter writer = new StreamWriter($"{Environment.CurrentDirectory}\\name_sports.txt", false))
                        {
                    while (reader.Read())
                    {
                             writer.WriteLine(reader.GetString(0));
                    }
                        }
                }
                using (var command = new NpgsqlCommand("SELECT complexity_sports FROM politech.data", conn))
                using (var reader = command.ExecuteReader())
                {
                    using (StreamWriter writer = new StreamWriter($"{Environment.CurrentDirectory}\\complexity_sports.txt", false))
                    {
                        while (reader.Read())
                        {
                            writer.WriteLine(reader.GetInt32(0));
                        }
                    }
                }
                using (var command = new NpgsqlCommand("SELECT name_theme FROM politech.data", conn))
                using (var reader = command.ExecuteReader())
                {
                    using (StreamWriter writer = new StreamWriter($"{Environment.CurrentDirectory}\\name_theme.txt", false))
                    {
                        while (reader.Read())
                        {
                            writer.WriteLine(reader.GetString(0));
                        }
                    }
                }
                using (var command = new NpgsqlCommand("SELECT complexity_theme FROM politech.data", conn))
                using (var reader = command.ExecuteReader())
                {
                    using (StreamWriter writer = new StreamWriter($"{Environment.CurrentDirectory}\\complexity_theme.txt", false))
                    {
                        while (reader.Read())
                        {
                            writer.WriteLine(reader.GetInt32(0));
                        }
                    }
                }
                name_sports.ItemsSource = (File.ReadAllLines($"{Environment.CurrentDirectory}\\name_sports.txt", Encoding.Default));
                name_theme.ItemsSource= (File.ReadAllLines($"{Environment.CurrentDirectory}\\name_theme.txt", Encoding.Default));
            }
        }
        private void dropBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        #region Логика ужасного алгоритма
        string com_spo;
        string com_the;
        int dific_sports=0;
        int dific_theme = 0;
        string dific_string;
        string text_output;
        int dific = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           dific= dific_sports + dific_theme;

            if (dific<=5)
            {
                dific_string = "низкая";
            }
            else if(dific>5&&dific<=7)
            {
                dific_string = "средняя";
            }
            else
            {
                dific_string = "высокая";
            }
            text_output = $"Проанализировав данные, учитовая, что нагрузка по вашему виду спорту для вас {com_spo}, а нагрузка по придмету вашему {com_the}, учитывая, что от сессии вас отделяет {(date_session.SelectedDate - date_games.SelectedDate).Value.Days} дней, из-за всёго этого вероятность неудачи студентов из-за усталости {dific_string}";

            if((date_session.SelectedDate - date_games.SelectedDate).Value.Days<0)
            {
                text_output = "К счастью для вас сессия прошла, поэтому теперь сессия не влияет на игру студентов";
            }
            tect_output.Text =text_output;
            dific = 0;
        }




        #endregion



        #region логика смайлов
        private void very_sad_emoji_sports_MouseDown(object sender, MouseButtonEventArgs e)
        {
            color_changes();
            dific_sports =5;
            com_spo = "высокая";
            very_sad_emoji_sports.Fill=new SolidColorBrush(Colors.Red);
       
        }
      
       
        private void sad_emoji_sports_MouseDown(object sender, MouseButtonEventArgs e)
        {
            color_changes();
            dific_sports = 4;
            com_spo = "выше среднего";
            sad_emoji_sports.Fill = new SolidColorBrush(Colors.Orange);
         
        }
       
     
        private void neutral_emoji_sports_MouseDown(object sender, MouseButtonEventArgs e)
        {
            color_changes();
            dific_sports = 3;
            com_spo = "средняя";
            neutral_emoji_sports.Fill = new SolidColorBrush(Colors.Yellow);
           
        }
     
        private void joy_emoji_sports_MouseDown(object sender, MouseButtonEventArgs e)
        {
            color_changes();
            dific_sports = 2;
            com_spo = "ниже среднего";
            joy_emoji_sports.Fill = new SolidColorBrush(Colors.LightGreen);
          
        }
      
        private void very_joy_emoji_sports_MouseDown(object sender, MouseButtonEventArgs e)
        {
            color_changes();
            com_spo = "низкая";
            dific_sports = 1;
            very_joy_emoji_sports.Fill = new SolidColorBrush(Colors.Green);
           
        }
        //theme
      
      
        private void very_sad_emoji_theme_MouseDown(object sender, MouseButtonEventArgs e)
        {
            color_changes_theme();
            com_the = "высока";
            dific_theme = 5;
            very_sad_emoji_theme.Fill = new SolidColorBrush(Colors.Red);
        }
     

        private void sad_emoji_theme_MouseDown(object sender, MouseButtonEventArgs e)
        {
            color_changes_theme();
            com_the = "выше среднего";
            dific_theme = 4;
            sad_emoji_theme.Fill = new SolidColorBrush(Colors.Orange);
        }
       
        private void neutral_emoji_theme_MouseDown(object sender, MouseButtonEventArgs e)
        {
            color_changes_theme();
            dific_theme = 3;
            com_the = "средняя";
            neutral_emoji_theme.Fill = new SolidColorBrush(Colors.Yellow);
        }
      

        private void joy_emoji_theme_MouseDown(object sender, MouseButtonEventArgs e)
        {
            color_changes_theme();
            dific_theme = 2;
            com_the = "ниже среднего";
            joy_emoji_theme.Fill = new SolidColorBrush(Colors.LightGreen);
        }
       

        private void very_joy_emoji_theme_MouseDown(object sender, MouseButtonEventArgs e)
        {
            color_changes_theme();
            dific_theme = 1;
            com_the = "низкая";
            very_joy_emoji_theme.Fill = new SolidColorBrush(Colors.Green);
        }
        void color_changes()
        {
            very_sad_emoji_sports.Fill = new SolidColorBrush(Colors.White);
            sad_emoji_sports.Fill = new SolidColorBrush(Colors.White);
            neutral_emoji_sports.Fill = new SolidColorBrush(Colors.White);
            joy_emoji_sports.Fill = new SolidColorBrush(Colors.White);
            very_joy_emoji_sports.Fill = new SolidColorBrush(Colors.White);
          
        }
        void color_changes_theme()
        {
            very_sad_emoji_theme.Fill = new SolidColorBrush(Colors.White);
            sad_emoji_theme.Fill = new SolidColorBrush(Colors.White);
            neutral_emoji_theme.Fill = new SolidColorBrush(Colors.White);
            joy_emoji_theme.Fill = new SolidColorBrush(Colors.White);
            very_joy_emoji_theme.Fill = new SolidColorBrush(Colors.White);
        }

        #endregion
    }
}