using ICinema.Models;

namespace ICinema.ViewModels
{
    public class HallVM
    {
        public List<List<Seat>> Seats { get; set; }
        public int Rows 
        {
            get 
            {
                return Seats.Count;
            }
        }
    }
}
