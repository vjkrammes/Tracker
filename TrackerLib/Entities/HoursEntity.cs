using System;
using System.ComponentModel.DataAnnotations;

namespace TrackerLib.Entities
{
    public class HoursEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Time { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public override string ToString() => $"{Date.ToShortDateString()} - {Time:n2} hours";
    }
}
