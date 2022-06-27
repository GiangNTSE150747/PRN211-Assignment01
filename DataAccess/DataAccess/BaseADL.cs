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

            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
            var strConnection = config["ConnectionStrings: FStoreDB"];
            if (strConnection == null)
            { System.Console.WriteLine("Null"); }
            else { System.Console.WriteLine("OK"); }

            return strConnection;

        }

        public void CloseConnection() => dataProvider.CloseConnection(connection);
    }
}
