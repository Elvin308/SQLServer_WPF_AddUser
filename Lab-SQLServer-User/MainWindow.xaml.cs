using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Lab_SQLServer_User
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string connectionString = "server=(LocalDB)\\MSSQLLocalDB; integrated security = SSPI; database=AdminDB";

        public MainWindow()
        {
            InitializeComponent();
            GetData();
        }

        #region ButtonClick
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstTextBox.Text.Trim() == "" || LastTextBox.Text.Trim() == "" || EmailTextBox.Text.Trim() == "" || PasswordBox.Password.Trim() == "")
            {
                MessageBox.Show("All fields must be filled out");
            }
            else
            {
                try
                {

                    SqlConnection sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();

                    string sql = "INSERT INTO Users (First, Last, Email, Password) Values" + "(@param1, @param2, @param3, @param4)";

                    SqlCommand command = new SqlCommand(sql, sqlConn);

                    command.Parameters.Add(new SqlParameter("@param1", FirstTextBox.Text));
                    command.Parameters.Add(new SqlParameter("@param2", LastTextBox.Text));
                    command.Parameters.Add(new SqlParameter("@param3", EmailTextBox.Text));
                    command.Parameters.Add(new SqlParameter("@param4", PasswordBox.Password.ToString()));
                    int rowsAffected = command.ExecuteNonQuery();

                    GetData();
                    FirstTextBox.Text = "";
                    LastTextBox.Text = "";
                    EmailTextBox.Text = "";
                    PasswordBox.Password = "";
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.ToString());
                }
            }
        }
        #endregion

        #region Methods
        public void GetData()
        {
            try
            {
                SqlConnection sqlConn;
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();

                ConnStringTextBox.Text = connectionString;
                string sqlStatement = "Select * from Users;";

                SqlCommand command = new SqlCommand(sqlStatement, sqlConn);
                SqlDataReader reader = command.ExecuteReader();

                UsersDataGrid.ItemsSource = reader;
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not connect to database...");
                //MessageBox.Show(e.ToString());
            }
        }
        #endregion
    }
}
