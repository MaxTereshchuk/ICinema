using ICinema.Models;

using Microsoft.AspNetCore.Identity;

using System.Net;

namespace ICinema.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDBContext>();

                context.Database.EnsureCreated();

                if (!context.Films.Any())
                {
                    context.Films.AddRange(new List<Film>()
                    {
                        new Film()
                        {
                            Title = "Forsage",
                            Image = "https://itc.ua/wp-content/uploads/2023/04/InShot_20230423_231506532.jpg?quality=82&strip=1&resize=640%2C360",
                            Time = "16:30",
                         },
                        new Film()
                        {
                            Title = "Willi wonka",
                            Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRuM4TZfM_suitPIG2QVfhUop5ZFM2XYNjgfQ&s?quality=82&strip=1&resize=640%2C360",
                            Time = "12:30",
                         },
                        new Film()
                        {
                            Title = "Interstellar",
                            Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcScDZabZA1tsfV5Tr4_u4-7jftTOgch-7621w&s?quality=82&strip=1&resize=640%2C360",
                            Time = "10:00",
                         }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
<<<<<<< HEAD
=======

>>>>>>> main
