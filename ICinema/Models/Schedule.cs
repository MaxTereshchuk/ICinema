﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICinema.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public ICollection<Screaning> Screanings { get; set; }
        [ForeignKey("Film")]
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public string Title { get; set; }
        
        [ForeignKey("Hall")]
        public int HallId { get; set; }
        public string Hall { get; set; }
    }
}
