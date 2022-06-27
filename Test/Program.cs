using System;
using System.IO;
using System.Data.Common;
using Microsoft.Data.SqlClient; //provider for create connect to SQL Server
using Microsoft.Extensions.Configuration;

namespace Prn.Se1622;

public class Program
{
    public static void Main()
    {
        ShowEmployees();
        Console.ReadLine();
    }

    //return connection string
    private static string GetConnectionString()
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
        Console.WriteLine(config["ConnectionStrings:FptEduDBConn"]);
        return config["ConnectionStrings:FptEduDBConn"];
    }
    public static void ShowEmployees()
    {
        string vSQL = "select * from Members";
        //1. init connection to SQL Server
        DbProviderFactory factory = SqlClientFactory.Instance;
        using DbConnection? conn = factory.CreateConnection();
        if (conn == null)
        {
            Console.WriteLine($"Init connection fail...");
            return;
        }
        conn.ConnectionString = GetConnectionString();
        conn.Open();
        //2. build command (SQL command)
        using DbCommand cmd = conn.CreateCommand();
        if (cmd == null)
        {
            Console.WriteLine($"Init connection fail...");
            return;
        }
        cmd.CommandText = vSQL;
        cmd.Connection = conn;
        //3. execute command
        using DbDataReader reader = cmd.ExecuteReader();
        //4. read data and display in console...
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Console.WriteLine($"{reader["MemberID"]} - {reader["MemberName"]}");
            }
        }
    }
}