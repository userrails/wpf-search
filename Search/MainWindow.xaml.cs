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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Search
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GetAllData();
        }

        DataTable dt;

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView DV = new DataView(dt);
            DV.RowFilter = String.Format("fn LIKE '%{0}%'", txtSearch.Text);
            lvCus.ItemsSource = DV;
        
        }

        // Display all the data to ListView
        public void GetAllData()
        {
            string ConString = System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                try
                {
                    CmdString = "SELECT * FROM TbCus";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt = new DataTable("Gov");
                    da.Fill(dt);
                    lvCus.ItemsSource = dt.DefaultView;
                    da.Update(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        // Display all the data to ListView
    }
}
