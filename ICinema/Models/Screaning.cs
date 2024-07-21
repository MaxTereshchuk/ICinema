﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICinema.Models
{
    public class Screaning
    {
        [Key]
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        [ForeignKey("Hall")]
        public int HallId { get; set; }
        public Hall Hall { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

    }
}
