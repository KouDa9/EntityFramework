using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EntityFramework
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

 
public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public ApplicationContext()
    {
        Database.EnsureDeleted(); 
        Database.EnsureCreated(); 
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=firstappdb;Trusted_Connection=True;");
    }
}

    class Program
    {
        static void Main(string[] args)
        {
            ApplicationContext db = new ApplicationContext();
            User Daniil = new User { Name = "Daniil", Age = 19 };
            User Ivan = new User { Name = "Ivan", Age = 25 };

            db.Users.AddRange(Daniil, Ivan);
            db.SaveChanges();
            Console.WriteLine("Объекты добавлены");

            var users = db.Users.ToList();
            Console.WriteLine("Список объектов: ");
            foreach (User u in users)
            {
                Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            }

            User user = db.Users.FirstOrDefault();
            if (user != null)
            {
                user.Name = "Bob";
                user.Age = 33;
                db.SaveChanges();
            }
            Console.WriteLine("Отредактировано: ");
            foreach (User u in users)
            {
                Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            }

            Console.ReadLine();
        }
    }
}
