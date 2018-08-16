using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepository
{
    public static class DBInitializer
    {
        public static void Initialize(RepositoryContext context)
        {
            context.Database.Migrate();

            // var userCount = await context.Owners.CountAsync().ConfigureAwait(false);



            if (context.Owners.Count() == 0)
            {
                var owners = new[]
                {
                new Owner()
                    {
                        Login = "admin",
                        Password = "admin",
                        Name = "Admin",
                        Surname = "Adminovich",
                        BornDate = new DateTime(1999, 5, 5),
                        DrivingExperience = 10,

                    },
                    new Owner()
                    {
                         Login = "adminRe",
                        Password = "admin",
                        Name = "Admin",
                        Surname = "Deputy",
                        BornDate = new DateTime(1999, 5, 5),
                        DrivingExperience = 10
                    }
                };

                var cars = new[]
                {
                    new Car()
                     {
                        Model = "Reno",
                        Series = "Cargo",
                        Price = 13000,
                        ReleaseDate = new DateTime(1990,10,10),
                        Type = CarType.Passenger,
                     },
                     new Car()
                     {
                         Model = "Reno",
                         Series = "Tribiani",
                         Price = 12000,
                         ReleaseDate = new DateTime(1995, 7, 21),
                         Type = CarType.Passenger,
                      },
                     new Car()
                      {
                         Model = "KAMAZ",
                         Series = "T-34",
                         Price = 5000,
                         ReleaseDate = new DateTime(1960, 7, 21),
                         Type = CarType.Freight
                      }
                };
                context.AddRange(
                    new CarOwner() { Owner = owners[0], Car = cars[0] },
                    new CarOwner() { Owner = owners[1], Car = cars[1] },
                    new CarOwner() { Owner = owners[1], Car = cars[2] });
            }
            context.SaveChanges();
        }
    }
}

