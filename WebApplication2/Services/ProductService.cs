using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class ProductService
    {
        private static string db_source = "test-serverdb-tiendz.database.windows.net";
        private static string db_user = "sqladmin1";
        private static string db_password = "T123456789@@";
        private static string db_database = "appdb";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;

            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();

            List<Product> products = new List<Product>();

            string statement = "SELECT ProductID, ProductName, Quantity FROM Products";

            conn.Open();

            SqlCommand command = new SqlCommand(statement, conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    products.Add(product);
                }

                return products;
            }
        }
    }
}
