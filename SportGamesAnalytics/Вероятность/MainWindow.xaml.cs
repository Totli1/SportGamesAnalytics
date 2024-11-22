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
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Вероятность
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
     

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var configuration = new ConfigurationBuilder()
   .SetBasePath(Directory.GetCurrentDirectory())
   .AddJsonFile("appsettings.json")
   .Build();
            string connString = configuration.GetConnectionString("DefaultConnection");
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(
                    @$"INSERT INTO Politech.{teamName.SelectedItem} (price_place, date_win, date_session) 
          VALUES (@p1, @d1, @da1)", conn))
                {
                    command.Parameters.AddWithValue("t1", price_place.Text);
                    command.Parameters.AddWithValue("a1", date_win.SelectedDate);
                    command.Parameters.AddWithValue("da1", date_session);
                 
                
                   
                }
            }
        }

        private void date_session_CalendarClosed(object sender, RoutedEventArgs e)
        {
            
        
        }
    }
}