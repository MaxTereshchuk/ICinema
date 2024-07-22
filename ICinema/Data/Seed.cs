using ICinema.Models;

using Microsoft.AspNetCore.Identity;

using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                            Schedules = new List<Schedule>()
                            {
                                new Schedule() { Day = DateTime.Now.AddDays(1),
                                Date = "21 july",
                                Time = "16:30",
                                Hall = "1"},
                                new Schedule() { Day = DateTime.Now.AddDays(3),
                                Date = "21 july",
                                Time = "19:30",
                                Hall = "1" }
                            }
                         },
                        new Film()
                        {
                            Title = "Willi wonka",
                            Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRuM4TZfM_suitPIG2QVfhUop5ZFM2XYNjgfQ&s?quality=82&strip=1&resize=640%2C360",
                            Schedules = new List<Schedule>()
                            {
                                new Schedule() { Day = DateTime.Now.AddDays(2),
                                Date = "21 july",
                                Time = "11:30",
                                Hall = "2" },
                                new Schedule() { Day = DateTime.Now.AddDays(4),
                                Date = "21 july",
                                Time = "12:30",
                                Hall = "3" }
                            }
                         },
                        new Film()
                        {
                            Title = "Interstellar",
                            Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcScDZabZA1tsfV5Tr4_u4-7jftTOgch-7621w&s?quality=82&strip=1&resize=640%2C360",
                            Schedules = new List<Schedule>()
                            {
                                new Schedule() { Day = DateTime.Now.AddDays(5),
                                Date = "21 july",
                                Time = "18:30",
                                Hall = "3" },
                                new Schedule() { Day = DateTime.Now.AddDays(6),
                                Date = "21 july",
                                Time = "11:30",
                                Hall = "1" }
                            }
                         }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
