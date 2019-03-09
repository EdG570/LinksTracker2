using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinksTracker2.Models
{
    public class Stats
    {
        [Key]
        public int Id { get; set; }
        public bool FIR { get; set; }
        public bool GIR { get; set; }
        public bool UpAndDown { get; set; }
        public int Putts { get; set; }
        public int Penalties { get; set; }
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int HoleId { get; set; }
        [Required]
        public string UserId { get; set; }

        [ForeignKey("HoleId")]
        public virtual Hole Hole { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}