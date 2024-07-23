namespace ICinema.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public Screaning Screaning { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
