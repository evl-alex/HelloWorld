using System;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using HelloWorld.Models;
using System.Globalization;
using HelloWorld.Data;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataContextDappter dapper = new DataContextDappter();

            DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
            // Console.WriteLine(rightNow.ToString());

            Computer myComputer = new()
            {
                Motherboard = "Z690",
                HasWifi = false,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX 2060"
            };

            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES ('" + myComputer.Motherboard
                    + "','" + myComputer.HasWifi
                    + "','" + myComputer.HasLTE
                    + "','" + myComputer.ReleaseDate
                    + "','" + myComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                    + "','" + myComputer.VideoCard
            + "')";

            // Console.WriteLine(sql);

            // int result = dapper.ExecuteSqlWithRowCount(sql);
            bool result = dapper.ExecuteSql(sql);

            // Console.WriteLine(result);

            string sqlSelect = @"
            SELECT 
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
            FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            Console.WriteLine("'Motherboard', 'HasWifi', 'HasLTE', 'ReleaseDate', 'Price', 'VideoCard'");

            foreach(Computer singleComputer in computers)
            {
                Console.WriteLine("'"  + singleComputer.Motherboard
                    + "','" + singleComputer.HasWifi
                    + "','" + singleComputer.HasLTE
                    + "','" + singleComputer.ReleaseDate
                    + "','" + singleComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                    + "','" + singleComputer.VideoCard
                + "'");
            }
        }
    }
}