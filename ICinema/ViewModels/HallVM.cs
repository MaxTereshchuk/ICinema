using ICinema.Models;

namespace ICinema.ViewModels
{
    public class HallVM
    {
        public int Id { get; set; }
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
