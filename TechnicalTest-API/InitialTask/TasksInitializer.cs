using DataBase.Context;
using DataBase.Models;
using TechnicalTest_API.Controllers;

namespace TechnicalTest_API.InitialTask
{
    public static class TasksInitializer
    {
        public static WebApplication Seed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using var ctx = scope.ServiceProvider.GetRequiredService<TechnicalTestContext>();
                try
                {
                    ctx.Database.EnsureCreated();

                    var users = ctx.Users.FirstOrDefault();
                    if (users == null)
                    {
                        ctx.Items.AddRange(
                            new Items { Description = "Laptop", Price = 1000, Stock = 2 },
                            new Items { Description = "Mouse", Price = 25, Stock = 2 }
                        );

                        ctx.Sells.AddRange(
                            new Sells { ItemId = 2, Quantity = 1 }
                        );

                        ctx.Rols.AddRange(
                            new Rols { RolDescription = "Admin" },
                            new Rols { RolDescription = "Noob" }
                            );

                        RegistrerController.CreatePasswordHash("Admin",out byte[] passwordHash, out byte[] passwordSalt);
                        RegistrerController.CreatePasswordHash("Noob", out byte[] passwordHash2, out byte[] passwordSalt2);
                        ctx.Users.AddRange( 
                            new Users { Name = "Admin", HashPassword = passwordHash, SaltPassword = passwordSalt, IdRole = 1 },
                            new Users { Name = "Noob", HashPassword = passwordHash2, SaltPassword = passwordSalt2, IdRole = 2 }
                            );

                        ctx.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    throw new Exception();
                }
                return app;
            }
        }

    }
}
