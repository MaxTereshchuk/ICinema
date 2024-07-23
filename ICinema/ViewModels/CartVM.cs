using ICinema.Models;

namespace ICinema.ViewModels
{
    public class CartVM
    {


        public Screaning Screaning { get; set; }
        public List<Ticket> Tickets { get; set; }
        public decimal Sum { get
            {
                decimal sum = 0;
                foreach (Ticket ticket in Tickets)
                    sum += ticket.Price;
                return sum;
            }
            
        }
        public string ImageUrl { get 
            {
                return Screaning.Schedule.Film.Image;

            }
        }
        public string Title
        {
            get
            {
                return Screaning.Schedule.Film.Title;
            }
        }
        public DateTime Time
        {
            get
            {
                return Screaning.Day;
            }
        }
        
    }
}
