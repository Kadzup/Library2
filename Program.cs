using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;

namespace Library2
{
    class Program
    {
        const string DataPath = "Data";

        static List<T> ReadObjects<T>(string filename)
        {
            string json = File.ReadAllText($"{DataPath}\\{filename}");
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        static void ImportData(ApplicationContext context)
        {
            context.AddRange(ReadObjects<User>("user.json"));
            context.AddRange(ReadObjects<Book>("book.json"));
            context.AddRange(ReadObjects<BookOrder>("book_order.json"));
            context.SaveChanges();
        }

        static void PrintTable<T>(string name, List<T> items) where T : class
        {
            Console.WriteLine($"\n------- {name} -------");
            items.ForEach(Console.WriteLine);
        }

        static void PrintData(ApplicationContext context)
        {
            PrintTable("Users", context.Users.ToList());
            PrintTable("Books", context.Books.ToList());
            PrintTable("Orders", context.BookOrders.ToList());
        }

        static void PrintAggregatedData(ApplicationContext context)
        {
            var data = context.BookOrders
                .Join(context.Users, o => o.UserTicket, c => c.Id,
                    (o, c) => new { BookId = o.BookId, FirstName = c.FirstName, LastName = c.LastName })
                .Join(context.Books, oc => oc.BookId, p => p.Id,
                    (oc, p) => new { Book = p.Title, FirstName = oc.FirstName, LastName = oc.LastName })
                .ToList()
                .GroupBy(t => new { t.FirstName, t.LastName })
                .Where(g => g.Count() >= 2);

            Console.WriteLine("\n------- Aggregated data -------");
            foreach (var el in data)
            {
                string[] books = el.Select(q => q.Book).ToArray();

                Console.WriteLine($"'{el.Key.FirstName} {el.Key.LastName}' " +
                    $"ordered books [{string.Join(", ", books)}], " +
                    $"Count: {el.Count()}");
            }
        }

        static void DeleteData(ApplicationContext context)
        {
            context.BookOrders.RemoveRange(context.BookOrders);
            context.Users.RemoveRange(context.Users);
            context.Books.RemoveRange(context.Books);
            context.SaveChanges();
        }

        static void Main(string[] args)
        {
            using (var context = new ApplicationContext())
            {
                ImportData(context);
                PrintData(context);
                PrintAggregatedData(context);
                DeleteData(context);
            }
        }
    }
}
