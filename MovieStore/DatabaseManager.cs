using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieStore
{
    public class DatabaseManager
    {
        public string constring = @"Data Source = WT135-826LSW\SQLEXPRESS;Initial Catalog = MOVIE_STORE; Integrated Security = True";
        SqlConnection SqlConn = new SqlConnection("Data Source = WT135-826LSW\\SQLEXPRESS;Initial Catalog = MOVIE_STORE; Integrated Security = True");
        SqlCommand SqlStr = new SqlCommand();
        SqlDataReader SqlReader;
        String SqlStmt;

        public DataTable ListMovies()
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Select * from Movies";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlConn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }

                    SqlConn.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                SqlConn.Close();
                return null;
            }

        }
        
        public bool AddMovies(string Rating, string Title, string Year, int Rental_Cost,int Copies, string plot,string Genre)
        {
            bool MovieAdded = false;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Insert into Movies(Rating, Title, Year, Rental_Cost, Copies, plot, Genre) values(@Rating,@MovieTitle,@MovieYear,@Rental,@MvCopies,@plots,@Genres)";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlStr.Parameters.AddWithValue("@Rating", Rating);
                    SqlStr.Parameters.AddWithValue("@MovieYear", Year);
                    SqlStr.Parameters.AddWithValue("@MovieTitle", Title);
                    SqlStr.Parameters.AddWithValue("@Rental", Rental_Cost);
                    SqlStr.Parameters.AddWithValue("@MvCopies", Copies);
                    SqlStr.Parameters.AddWithValue("@plots", plot);
                    SqlStr.Parameters.AddWithValue("@Genres", Genre);


                    SqlConn.Open();
                    int rowsadded = SqlStr.ExecuteNonQuery();

                    if (rowsadded > 0)
                    {
                        MovieAdded = true;
                    }

                    SqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                SqlConn.Close();
            }
            return MovieAdded;
        }
        public bool DeleteMovies(int MovieID)
        {
            bool MovieDeleted = false;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Delete from Movies where MovieID = @MovieID";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlStr.Parameters.AddWithValue("@MovieID", MovieID);

                    SqlConn.Open();
                    int rowsadded = SqlStr.ExecuteNonQuery();

                    if (rowsadded > 0)
                    {
                        MovieDeleted = true;
                    }

                    SqlConn.Close();
                }
                return MovieDeleted;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception" + ex.Message);
                SqlConn.Close();
                return false;
            }

        }
        public DataTable SearchMovies(string Moviename)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Select * from Movies where Title like '%" + Moviename + "%'";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlConn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }

                    SqlConn.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                SqlConn.Close();
                return null;
            }

        }
        public bool EditMovies(string Rating, string Title, string Year, double Rental_Cost, int Copies, string plot, string Genre, int MovieID)
        {
            bool MovieEdited = false;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Update Movies set Title = @MovieTitle, Rating = @Rating, Year = @MovieYear, Rental_Cost = @Rental, Copies = @MvCopies, plot = @plots, Genre = @Genres where MovieID = @MovieID";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlStr.Parameters.AddWithValue("@Rating", Rating);
                    SqlStr.Parameters.AddWithValue("@MovieYear", Year);
                    SqlStr.Parameters.AddWithValue("@MovieTitle", Title);
                    SqlStr.Parameters.AddWithValue("@Rental", Rental_Cost);
                    SqlStr.Parameters.AddWithValue("@MvCopies", Copies);
                    SqlStr.Parameters.AddWithValue("@plots", plot);
                    SqlStr.Parameters.AddWithValue("@Genres", Genre);
                    SqlStr.Parameters.AddWithValue("@MovieID", MovieID);

                    SqlConn.Open();
                    int rowsadded = SqlStr.ExecuteNonQuery();

                    if (rowsadded > 0)
                    {
                        MovieEdited = true;
                    }

                    SqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception" + ex.Message);
                SqlConn.Close();
            }
            return MovieEdited;
        }
        public DataTable ListCustomers()
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Select * from Customer";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlConn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }

                    SqlConn.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                SqlConn.Close();
                return null;
            }

        }
        public DataTable SearchCustomers(string Custname)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Select * from Customer where FirstName like '%" + Custname + "%'";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlConn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }

                    SqlConn.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                SqlConn.Close();
                return null;
            }

        }
        public bool AddCustomers(string FirstName, string LastName, string Address, string Phone)
        {
            bool CustomerAdded = false;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Insert into Customer(FirstName, LastName, Address, Phone) values(@Name,@LastNm,@Address,@Phone)";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlStr.Parameters.AddWithValue("@Name", FirstName);
                    SqlStr.Parameters.AddWithValue("@LastNm", LastName);
                    SqlStr.Parameters.AddWithValue("@Address", Address);
                    SqlStr.Parameters.AddWithValue("@Phone", Phone);

                    //txtName.Clear();
                    //txtLastNm.Clear();
                    //txtAddress.Clear();
                    //txtPhone.Clear();

                    SqlConn.Open();
                    int rowsadded = SqlStr.ExecuteNonQuery();

                    if (rowsadded > 0)
                    {
                        CustomerAdded = true;
                    }

                    SqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                SqlConn.Close();
            }
            return CustomerAdded;
        }
        public bool DeleteCustomers(int CustID)
        {
            bool CustomerDeleted = false;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Delete from Customer where CustID = @CustID";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlStr.Parameters.AddWithValue("@CustID", CustID);

                    SqlConn.Open();
                    int rowsadded = SqlStr.ExecuteNonQuery();

                    if (rowsadded > 0)
                    {
                        CustomerDeleted = true;
                    }

                    SqlConn.Close();
                }
                return CustomerDeleted;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception" + ex.Message);
                SqlConn.Close();
                return false;
            }

        }
        public bool EditCustomer(string FirstName, string LastName, string Address, string Phone, int CustID)
        {
            bool CustomerEdited = false;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Update Customer set FirstName = @Name, LastName = @LastName, Address = @Address, Phone = @Phone where CustID = @CustID";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlStr.Parameters.AddWithValue("@Name", FirstName);
                    SqlStr.Parameters.AddWithValue("@LastName", LastName);
                    SqlStr.Parameters.AddWithValue("@Address", Address);
                    SqlStr.Parameters.AddWithValue("@Phone", Phone);
                    SqlStr.Parameters.AddWithValue("@CustID", CustID);

                    SqlConn.Open();
                    int rowsadded = SqlStr.ExecuteNonQuery();

                    if (rowsadded > 0)
                    {
                        CustomerEdited = true;
                    }

                    SqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception" + ex.Message);
                SqlConn.Close();
            }
            return CustomerEdited;
        }
        public bool AddRentedMovie(string CustID, string MovieID, DateTime DateRented)
        {
            bool RentedMovieAdded = false;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Insert into RentedMovies(CustIDFK, MovieIDFK, DateRented) values(@CustID,@MovieID,@DateRented)";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlStr.Parameters.AddWithValue("@CustID", CustID);
                    SqlStr.Parameters.AddWithValue("@MovieID", MovieID);
                    SqlStr.Parameters.AddWithValue("@DateRented", DateRented);
                    //SqlStr.Parameters.AddWithValue("@DateReturned", DateReturned);

                    SqlConn.Open();
                    int rowsadded = SqlStr.ExecuteNonQuery();

                    if (rowsadded > 0)
                    {
                        RentedMovieAdded = true;
                    }

                    SqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                SqlConn.Close();
            }
            return RentedMovieAdded;
        }
        public DataTable ListRentedMovies()
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Select RMID, FirstName, LastName, Title, DateRented, MovieID, CustID, DateReturned from RentedMovies e inner join Movies p ON e.MovieIDFK = p.MovieID inner join Customer c ON e.CustIDFK = c.CustID where DateReturned is Null";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlConn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }

                    SqlConn.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                SqlConn.Close();
                return null;
            }

        }

        public DataTable ListRentedAllMovies()
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Select RMID, FirstName, LastName, Title, DateRented, MovieID, CustID, DateReturned from RentedMovies e inner join Movies p ON e.MovieIDFK = p.MovieID inner join Customer c ON e.CustIDFK = c.CustID";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlConn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }

                    SqlConn.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                SqlConn.Close();
                return null;
            }

        }


        public bool UpdateRentedMovies(int RMID, DateTime DateReturned)
        {
            bool updRentedMovie = false;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Update RentedMovies set DateReturned = @DateReturned where RMID = @RMID";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlStr.Parameters.AddWithValue("@RMID", RMID);
                    SqlStr.Parameters.AddWithValue("@DateReturned", DateReturned);

                    SqlConn.Open();
                    int rowsadded = SqlStr.ExecuteNonQuery();

                    if (rowsadded > 0)
                    {
                        updRentedMovie = true;
                    }

                    SqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception" + ex.Message);
                SqlConn.Close();
            }
            return updRentedMovie;
        }
        public DataTable Total_Customers()
        {
           
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Select * from CustCount";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlConn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }

                    SqlConn.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                SqlConn.Close();
                return null;
            }
        }
        public DataTable Total_Movies()
        {
           DataTable dt = new DataTable();
           SqlDataReader SqlReader;

                try
                {
                    SqlStr.Connection = SqlConn;
                    SqlStmt = "Select * from MovieCount";

                    using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                    {
                        SqlConn.Open();
                        SqlReader = SqlStr.ExecuteReader();

                        if (SqlReader.HasRows)
                        {
                            dt.Load(SqlReader);
                        }

                        SqlConn.Close();
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception" + ex.Message);
                    SqlConn.Close();
                    return null;
                }
        }
        public DataTable MostRented()
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "  Select Top 10 count(*) as borrowed, title from RentedMovies rm inner join movies m on rm.MovieIDFK = m.MovieID group by title order by borrowed desc";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlConn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }

                    SqlConn.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                SqlConn.Close();
                return null;
            }
        }
        public DataTable MostCustRented()
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Select Top 10 count(*) as borrowed, FirstName, LastName, CustID from RentedMovies rm inner join Customer m on rm.CustIDFK = m.CustID group by FirstName, LastName, CustID order by borrowed desc";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlConn.Open();
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }

                    SqlConn.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                SqlConn.Close();
                return null;
            }
        }
        public bool IssueFix(int movieid)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;

            try
            {
                using (SqlStr = SqlConn.CreateCommand())
                {

                    SqlStr.CommandText = "RentedMovie";
                    SqlStr.CommandType = CommandType.StoredProcedure;

                    SqlConn.Open();

                    SqlStr.Parameters.AddWithValue("@movieid", movieid);
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        return false;
                    }

                    SqlConn.Close();
                    return true; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                SqlConn.Close();
                return false;
            }
        }
        public bool MovieFix(int CustIDFK)
        {
            DataTable dt = new DataTable();
            SqlDataReader SqlReader;

            try
            {
                SqlStr.Connection = SqlConn;
                SqlStmt = "Select * from RentedMovies where CustIDFK = @CustIDFK and DateReturned is null";

                using (SqlStr = new SqlCommand(SqlStmt, SqlConn))
                {
                    SqlConn.Open();

                    SqlStr.Parameters.AddWithValue("@CustIDFK", CustIDFK);
                    SqlReader = SqlStr.ExecuteReader();

                    if (SqlReader.HasRows)
                    {
                        return false;
                    }

                    SqlConn.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
                SqlConn.Close();
                return false;
            }
        }
    }
}
