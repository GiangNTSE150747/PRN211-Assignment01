using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DataAccess
{
    public class BaseADL
    {
        public StockDataProvider dataProvider { get; set; } = null;
        public SqlConnection connection = null;

        public BaseADL()
        {
            var connectionString = GetConnectionString();
            dataProvider = new StockDataProvider(connectionString);
        }

        static string GetConnectionString()
        {

            //help us to read appsettings.json
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json")
                .Build();
            if (config["ConnectionStrings:FptEduDBConn"] == null)
            {
                Console.WriteLine("Null");
            }
            return config["ConnectionStrings:FptEduDBConn"];

        }

        public void CloseConnection() => dataProvider.CloseConnection(connection);
    }
}
