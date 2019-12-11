using HotelLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RoyalHotelsRestService.DBUtil
{
    public class HotelTempsManager
    {
            //thomas
            //private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ThomasTestRoyalHotels;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //christian
            private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RoyalHotel2019;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //azure
            //private const string ConnectionString = @"Server=tcp:thofoserver.database.windows.net,1433;Initial Catalog = thofodatabase; Persist Security Info=False;User ID = adminthofo; Password=grt45Lde; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";



            private const string INSERT = "INSERT INTO Hoteltemps (Hotel_Id, Tempe_Value) VALUES (@Hotel_Id, @Tempe_Value)";
            private const string GETALL = "SELECT * FROM Hoteltemps";
            private const string GETRECENT = "SELECT * FROM Hoteltemps WHERE Tempe_Date=(SELECT max(Tempe_Date) FROM Hoteltemps) AND Hotel_Id=0";
            private const string DELETE = "DELETE FROM Hoteltemps WHERE Tempe_Date = @Tempe_Date AND Hotel_Id = @Hotel_Id";

        //API: API/HotelTemps
        public bool Post(TemperaturData maaling)
            {
                bool retValue;

                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand(INSERT, connection);
                cmd.Parameters.AddWithValue("@Hotel_Id", maaling.hotelID);
                cmd.Parameters.AddWithValue("@Tempe_Value", maaling.temperature);


                int rowsAffected = cmd.ExecuteNonQuery();
                retValue = rowsAffected == 1;
                connection.Close();


                return retValue;
            }
        //API: API/HotelTemps
        public IEnumerable<Temperaturmaaling> Get()
        {
            List<Temperaturmaaling> liste = new List<Temperaturmaaling>();

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(GETALL, connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Temperaturmaaling maaling = ReadMaaling(reader);
                liste.Add(maaling);
            }
            connection.Close();
            return liste;
        }

        // Delete api/ProcessOrdre/5
        public bool Delete(DateTime date)
        {
            bool sucesss = true;

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(DELETE, connection);
            command.Parameters.AddWithValue("@Tempe_Date", date);
            command.Parameters.AddWithValue("@Hotel_Id", 0);

            int rowsAffected = command.ExecuteNonQuery();
            sucesss = rowsAffected == 1;

            return sucesss;
        }
        //API: API/HotelTemps/Recent
        public Temperaturmaaling GetRecent()
        {
            Temperaturmaaling maaling = new Temperaturmaaling();

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(GETRECENT, connection);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                maaling = ReadMaaling(reader);
            }
            connection.Close();
            return maaling;

        }

        //public IEnumerable<Temperaturmaaling> Get(DateTime date)
        //{
        //    List<Temperaturmaaling> liste = new List<Temperaturmaaling>();

        //    SqlConnection connection = new SqlConnection(ConnectionString);
        //    connection.Open();
        //    SqlCommand cmd = new SqlCommand(GETDATE, connection);
        //    cmd.Parameters.AddWithValue("@Date", date);

        //    SqlDataReader reader = cmd.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        ProcessOrdre processOrdre = ReadProcessOrdre(reader);
        //        liste.Add(processOrdre);
        //    }
        //    connection.Close();
        //    return liste;
        //}

        private Temperaturmaaling ReadMaaling(SqlDataReader reader)
        {
            Temperaturmaaling maaling = new Temperaturmaaling(reader.GetInt32(1), reader.GetDateTime(0), reader.GetDouble(2));
            return maaling;
        }
    }
}
