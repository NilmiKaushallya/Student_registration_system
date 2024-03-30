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

using System.Data.SqlClient;

namespace Student_registration_system
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
           private string connectionString = "Data Source=.;Initial Catalog=YourDatabaseName;Integrated Security=True";

            public LoginWindow()
            {
                InitializeComponent();
            }

            private void LoginButton_Click(object sender, RoutedEventArgs e)
            {
                string username = UsernameTextBox.Text;
                string password = PasswordBox.Password;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Password FROM Users WHERE Username = @Username";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        string storedPassword = reader.GetString(0);

                        if (storedPassword == password)
                        {
                            // Login successful, open student registration system
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            Close();
                        }
                        else
                        {
                            ErrorMessageLabel.Content = "Incorrect password.";
                        }
                    }
                    else
                    {
                        ErrorMessageLabel.Content = "User not found.";
                    }

                    reader.Close();
                }
            }
        }
 }
