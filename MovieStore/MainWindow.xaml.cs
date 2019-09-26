using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace MovieStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DatabaseManager objDb = new DatabaseManager();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {  //This shows grid items
           dgMovies.ItemsSource = objDb.ListMovies().DefaultView;
           dgCustomer.ItemsSource = objDb.ListCustomers().DefaultView;
           dgMoviesR.ItemsSource = objDb.SearchMovies(txtSrchMovies.Text).DefaultView;
           dgCustR.ItemsSource = objDb.SearchCustomers(txtSrchCust.Text).DefaultView;
           dgRented.ItemsSource = objDb.ListRentedMovies().DefaultView;
        }

        private void btnAddM_Click(object sender, RoutedEventArgs e)
        {
            if (txtMovie.Text == "")
            {
                MessageBox.Show("You must enter a valid name.");
                return;
            }

            if (txtRating.Text != "" && txtGenre.Text != "" && txtCost.Text != "" && txtDesc.Text != "" && txtStock.Text != "" && txtYear.Text != "" && txtMovie.Text != "")
            {
                bool MovieAdded = objDb.AddMovies(txtRating.Text, txtMovie.Text, txtYear.Text, Convert.ToInt16(txtCost.Text),Convert.ToInt16(txtStock.Text), txtDesc.Text, txtGenre.Text);

                if (MovieAdded == true)
                {
                    MessageBox.Show("Movie Added");
                    txtRating.Clear();
                    txtGenre.Clear();
                    txtDesc.Clear();
                    txtYear.Clear();
                    txtCost.Clear();
                    txtStock.Clear();
                    txtMovie.Clear();
                    txtSearch.Clear();
                    txtMovID.Clear();

                    dgMovies.ItemsSource = objDb.ListMovies().DefaultView;
                }
            }
            else
            {
                MessageBox.Show("Enter Movie details");
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgMovies.ItemsSource = objDb.SearchMovies(txtSearch.Text).DefaultView;
        }

        private void btnDele_Click(object sender, RoutedEventArgs e)
        {
            if (objDb.IssueFix(Convert.ToInt16(txtMovID.Text)) == false)
            {
                MessageBox.Show("Movie is rented out, cannot delete the Movie.");
                return;
            }

            if (dgMovies.SelectedIndex > -1)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to delete the Movie ?", " Omega", MessageBoxButton.YesNo);

                if (dialogResult.ToString() == "Yes")
                {
                    DataRowView row = (DataRowView)dgMovies.SelectedItems[0];
                    Int32 MovieID = Convert.ToInt32(row["MovieID"]);
               
                        objDb.DeleteMovies(MovieID);
                        MessageBox.Show("Movie Deleted");
                        txtMovie.Clear();
                        txtCost.Clear();
                        txtGenre.Clear();
                        txtRating.Clear();
                        txtYear.Clear();
                        txtStock.Clear();
                        txtMovID.Clear();
                        txtDesc.Clear();

                    dgMovies.ItemsSource = objDb.ListMovies().DefaultView;
                }
            }
            else
            {
                MessageBox.Show("Select Movie to delete");
            }
        }

        private void dgMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgMovies.SelectedIndex > -1)
            {
                DataRowView row = (DataRowView)dgMovies.SelectedItems[0];
                txtMovie.Text = Convert.ToString(row["Title"]);
                txtYear.Text = Convert.ToString(row["Year"]);
                txtGenre.Text = Convert.ToString(row["Genre"]);
                txtDesc.Text = Convert.ToString(row["Plot"]);
                txtRating.Text = Convert.ToString(row["Rating"]);
                txtCost.Text = Convert.ToString(row["Rental_Cost"]);
                txtStock.Text = Convert.ToString(row["Copies"]);
                txtMovID.Text = Convert.ToString(row["MovieID"]);
            }
        }

        private void btnUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dgMovies.SelectedIndex > -1)
            {
                DataRowView row = (DataRowView)dgMovies.SelectedItems[0];
                string Movie = txtMovie.Text;
                string Rating = txtRating.Text;
                string Year = txtYear.Text;
                double Cost = Convert.ToDouble(txtCost.Text);
                int Stock = Convert.ToInt16(txtStock.Text);
                string Desc = txtDesc.Text;
                string Genre = txtGenre.Text;
                int MovieID = Convert.ToInt16(row["MovieID"]);
                objDb.EditMovies(Rating, Movie, Year, Convert.ToInt16(Cost), Stock, Desc, Genre, MovieID);


                bool MovieEdited = objDb.EditMovies(txtRating.Text, txtMovie.Text, txtYear.Text, Convert.ToDouble(txtCost.Text), Convert.ToInt16(txtStock.Text), txtDesc.Text, txtGenre.Text, Convert.ToInt32(MovieID));

                if (MovieEdited == true)
                {
                    MessageBox.Show("Movie Edited");
                    txtMovie.Clear();
                    txtCost.Clear();
                    txtGenre.Clear();
                    txtRating.Clear();
                    txtYear.Clear();
                    txtStock.Clear();
                    txtMovID.Clear();
                    txtDesc.Clear();

                    dgMovies.ItemsSource = objDb.ListMovies().DefaultView;
                }
                else
                {
                    MessageBox.Show("Complete all fields");
                }
            }
            else
            {
                MessageBox.Show("Select Movie to edit");
            }
        }

        private void btnAddC_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "" && txtLastNm.Text == "")
            {
                MessageBox.Show("You must enter a valid name.");
                return;
            }

            if (txtName.Text != "" && txtLastNm.Text != "" && txtAddress.Text != "" && txtPhone.Text != "")
            {
                bool CustomerAdded = objDb.AddCustomers(txtName.Text, txtLastNm.Text, txtAddress.Text, txtPhone.Text);

                if (CustomerAdded == true)
                {
                    MessageBox.Show("Customer Added");
                    txtName.Clear();
                    txtLastNm.Clear();
                    txtAddress.Clear();
                    txtPhone.Clear();

                    dgMovies.ItemsSource = objDb.ListMovies().DefaultView;
                }
            }
            else
            {
                MessageBox.Show("Enter Customer details");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (objDb.MovieFix(Convert.ToInt16(txtCusID.Text)) == false)
            {
                MessageBox.Show("Customer has a movie out, cannot delete the Customer.");
                return;
            }

            if (dgCustomer.SelectedIndex > -1)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to delete this Customer?", " Omega", MessageBoxButton.YesNo);

                if (dialogResult.ToString() == "Yes")
                {
                    DataRowView row = (DataRowView)dgCustomer.SelectedItems[0];
                    Int32 CustID = Convert.ToInt32(row["CustID"]);

                    objDb.DeleteCustomers(CustID);
                    MessageBox.Show("Customer Deleted");
                    txtName.Clear();
                    txtLastNm.Clear();
                    txtAddress.Clear();
                    txtPhone.Clear();

                    dgCustomer.ItemsSource = objDb.ListCustomers().DefaultView;
                }
            }
            else
            {
                MessageBox.Show("Select Movie to delete");
            }
        }

        private void txtSearchC_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgCustomer.ItemsSource = objDb.SearchCustomers(txtSearchC.Text).DefaultView;
        }

        private void btnUpdC_Click(object sender, RoutedEventArgs e)
        {
                if (dgCustomer.SelectedIndex > -1)
                {
                    DataRowView row = (DataRowView)dgCustomer.SelectedItems[0];
                    string FirstName = txtName.Text;
                    string LastName = txtLastNm.Text;
                    string Address = txtAddress.Text;
                    string Phone = txtPhone.Text;
                    int CustID = Convert.ToInt16(row["CustID"]);
                    objDb.EditCustomer(FirstName, LastName, Address, Phone, CustID);

                    bool CustomerEdited = objDb.EditCustomer(txtName.Text, txtLastNm.Text, txtAddress.Text, txtPhone.Text, Convert.ToInt32(CustID));

                    if (CustomerEdited == true)
                    {
                        MessageBox.Show("Customer Edited");
                        txtName.Clear();
                        txtLastNm.Clear();
                        txtAddress.Clear();
                        txtPhone.Clear();
                        txtCusID.Clear();
                        
                        dgCustomer.ItemsSource = objDb.ListCustomers().DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("Complete all fields");
                    }
                }
                else
                {
                    MessageBox.Show("Select Customer to edit");
                }
        }

        private void dgCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCustomer.SelectedIndex > -1)
            {
                DataRowView row = (DataRowView)dgCustomer.SelectedItems[0];
                txtName.Text = Convert.ToString(row["FirstName"]);
                txtLastNm.Text = Convert.ToString(row["LastName"]);
                txtAddress.Text = Convert.ToString(row["Address"]);
                txtPhone.Text = Convert.ToString(row["Phone"]);
                txtCusID.Text = Convert.ToString(row["CustID"]);
            }
        }

        private void btnIssue_Click(object sender, RoutedEventArgs e)
        {
            if(objDb.IssueFix(Convert.ToInt16(txtMovieMID.Text))== false)
            {
                MessageBox.Show("Movie out, cannot issue");
                return;

            }

            bool RentedMovieAdded = objDb.AddRentedMovie(txtCustCID.Text, txtMovieMID.Text, DateTime.Today);

            if (RentedMovieAdded == true)
            {
                MessageBox.Show("Movie Issued");

                dgRented.ItemsSource = objDb.ListRentedMovies().DefaultView;
            }

        }

        private void dgCustR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCustR.SelectedIndex > -1)
            {
                DataRowView row = (DataRowView)dgCustR.SelectedItems[0];

                txtCustCID.Text = Convert.ToString(row["CustID"]);
            }
        }

        private void txtSrchMovies_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgMoviesR.ItemsSource = objDb.SearchMovies(txtSrchMovies.Text).DefaultView;
        }

        private void txtSrchCust_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgCustR.ItemsSource = objDb.SearchCustomers(txtSrchCust.Text).DefaultView;
        }

        private void dgMoviesR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgMoviesR.SelectedIndex > -1)
            {
                DataRowView row = (DataRowView)dgMoviesR.SelectedItems[0];

                txtMovieMID.Text = Convert.ToString(row["MovieID"]);
            }
        }

        private void dgRented_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRented.SelectedIndex > -1)
            {
                DataRowView row = (DataRowView)dgRented.SelectedItems[0];
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if (dgRented.SelectedIndex > -1)
            {
                DataRowView row = (DataRowView)dgRented.SelectedItems[0];

                int RMID = Convert.ToInt16(row["RMID"]);


                bool UpdateRentedMovies = objDb.UpdateRentedMovies(RMID, DateTime.Today);

                if (UpdateRentedMovies == true)
                {
                    MessageBox.Show("Customer Edited");

                    dgRented.ItemsSource = objDb.ListRentedMovies().DefaultView;
                }
                else
                {
                    MessageBox.Show("Complete all fields");
                }
            }
            else
            {
                MessageBox.Show("Select Customer to edit");
            }
        }

        private void rdTotalMovies_Checked(object sender, RoutedEventArgs e)
        {
            if (rdTotalMovies.IsChecked == true)
            {
                DgStat2.ItemsSource = objDb.Total_Movies().DefaultView;
            }
        }

        private void rdTotalCust_Checked(object sender, RoutedEventArgs e)
        {
            if (rdTotalCust.IsChecked == true)
            {
                DgStat2.ItemsSource = objDb.Total_Customers().DefaultView;
            }
        }

        private void rdHighestRated_Checked(object sender, RoutedEventArgs e)
        {
            //DgStat2.ItemsSource = objDb.MostRented().DefaultView;
        }

        private void rdMostRented_Checked(object sender, RoutedEventArgs e)
        {
            DgStat2.ItemsSource = objDb.MostRented().DefaultView;
        }

        private void rdMoviesCustRented_Checked(object sender, RoutedEventArgs e)
        {
            DgStat2.ItemsSource = objDb.MostCustRented().DefaultView;
        }

        private void txtName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]"); //only accepts letters from alphabet
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtGenre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]"); 
            e.Handled = regex.IsMatch(e.Text);
        }

        private void rdCurrent_Rented(object sender, RoutedEventArgs e)
        {
            dgRented.ItemsSource = objDb.ListRentedMovies().DefaultView;
        }

        private void rdAllR_movie(object sender, RoutedEventArgs e)
        {
            dgRented.ItemsSource = objDb.ListRentedAllMovies().DefaultView;
        }
    }
}
