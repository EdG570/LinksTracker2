using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LinksTracker2.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public int Par { get; set; }
        public decimal? Rating { get; set; }
        public decimal? Slope { get; set; }
        public int TotalHoles { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<Hole> Holes { get; set; }
    }
}