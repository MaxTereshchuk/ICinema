using ICinema.Models;

namespace ICinema.ViewModels
{
    public class CartVM
    {
       
        
        
        public ICollection<Ticket> Tickets { get; set; }
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
                if(Tickets!=null && Tickets.Count > 0)
                    return Tickets[0].ImageUrl;
                return null;

            }
        }
        public string Title
        {
            get
            {
                if (Tickets != null && Tickets.Count > 0)
                    return Tickets[0].Title;
                return null;
            }
        }
        public DateTime Time
        {
            get
            {
                if (Tickets != null && Tickets.Count > 0)
                    return Tickets[0].Time;
                return DateTime.Now;
            }
        }
    }
}
