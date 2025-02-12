using Dapper;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

class Program
{
    static string? connection;
    static void Main()
    {
        var builder = new ConfigurationBuilder();
        string path = Directory.GetCurrentDirectory();
        builder.SetBasePath(path);
        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();
        connection = config.GetConnectionString("DefaultConnection");

        //using (IDbConnection db = new SqlConnection(connection))
        //{
        //    db.Open();
        //    Console.WriteLine("База данных подключена:) ");
        //}

        try { 

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Отображение всех покупателей");
                Console.WriteLine("2. Отображение email всех покупателей");
                Console.WriteLine("3. Отображение списка разделов");
                Console.WriteLine("4. Отображение списка акционных товаров");
                Console.WriteLine("5. Отображение всех городов");
                Console.WriteLine("6. Отображение всех стран");
                Console.WriteLine("7. Отображение всех покупателей из конкретного города");
                Console.WriteLine("8. Отображение всех покупателей из конкретной страны;");
                Console.WriteLine("9. Отображение всех акций для конкретной страны");
                Console.WriteLine("0. Выход");

                int res = int.Parse(Console.ReadLine()!);
                switch (res)
                {
                    case 1:
                        ShowAll();
                        break;
                    case 2:
                        ShowEmail();
                        break;
                    case 3:
                        ShowSection();
                        break;
                    case 4:
                        Showpromotion();
                        break;
                    case 5:
                        ShowCity();
                        break;
                    case 6:
                        ShowCountry();
                        break;
                    case 7:
                        ShowCities();
                        break;
                    case 8:
                        ShowCountries();
                        break;
                    case 9:
                        ShowPromo();
                        break;
                    case 0:
                        return;
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void ShowAll()
    {
        Console.Clear();
        using(IDbConnection db = new SqlConnection(connection))
        {
            var groups = db.Query("Select * from Customers");
            int iter = 0;
            foreach(var group in groups)
            {
                Console.WriteLine($"{++iter} - {group.FullName}");
            }
  
        }
        Console.ReadKey();
    }

    static void ShowEmail()
    {
        Console.Clear();
        using (IDbConnection db = new SqlConnection(connection))
        {
            var emails = db.Query("Select Email from Customers");
            int iter = 0;
            foreach(var email in emails)
            {
                Console.WriteLine($" {++iter} = {email.Email}");
            }
        }
        Console.ReadKey();
    }
    static void ShowSection()
    {
        Console.Clear();
        using (IDbConnection db = new SqlConnection(connection))
        {
            var emails = db.Query("Select * from Customerinterests");
            int iter = 0;
            foreach (var email in emails)
            {
                Console.WriteLine($" {++iter} = {email.CategoryID}");
            }
        }
        Console.ReadKey();
    }

    static void Showpromotion()
    {
        Console.Clear();
        using (IDbConnection db = new SqlConnection(connection))
        {
            var emails = db.Query("Select * from Promotions");
            int iter = 0;
            foreach (var email in emails)
            {
                Console.WriteLine($" {++iter} = {email.PromotionDetails}");
            }
        }
        Console.ReadKey();
    }
    static void ShowCity()
    {
        Console.Clear();
        using (IDbConnection db = new SqlConnection(connection))
        {
            var emails = db.Query("Select City from Customers");
            int iter = 0;
            foreach (var email in emails)
            {
                Console.WriteLine($" {++iter} = {email.City}");
            }
        }
        Console.ReadKey();
    }
    static void ShowCountry()
    {
        Console.Clear();
        using (IDbConnection db = new SqlConnection(connection))
        {
            var c = db.Query("Select Country from Customers");
            int iter = 0;
            foreach (var email in c)
            {
                Console.WriteLine($" {++iter} = {email.Country}");
            }
        }
        Console.ReadKey();
    }
    static void ShowCities()
    {
        Console.Clear();
        using (IDbConnection db = new SqlConnection(connection))
        {
            Console.WriteLine($"Enter your city -> ");
            string city = Console.ReadLine();
            var c = db.Query("Select * from Customers").Where(x => x.City == city);
            int iter = 0;
            foreach (var email in c)
            {
                Console.WriteLine($" {++iter} = {email.FullName} {email.City}");
            }
        }
        Console.ReadKey();
    }
    static void ShowCountries()
    {
        Console.Clear();
        using (IDbConnection db = new SqlConnection(connection))
        {
            Console.WriteLine($"Enter your country -> ");
            string country = Console.ReadLine();
            var c = db.Query("Select * from Customers").Where(x => x.Country == country);
            int iter = 0;
            foreach (var email in c)
            {
                Console.WriteLine($" {++iter} = {email.FullName} {email.City}");
            }
        }
        Console.ReadKey();
    }
    static void ShowPromo()
    {
        Console.Clear();
        using (IDbConnection db = new SqlConnection(connection))
        {
            Console.WriteLine($"Enter your country -> ");
            string country = Console.ReadLine();
            var c = db.Query("Select * from Promotions").Where(x => x.Country == country);
            int iter = 0;
            foreach (var email in c)
            {
                Console.WriteLine($" {++iter} = {email.PromotionDetails} {email.Country}");
            }
        }
        Console.ReadKey();
    }
}
