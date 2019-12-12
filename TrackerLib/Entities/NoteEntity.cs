using System;
using System.ComponentModel.DataAnnotations;

namespace TrackerLib.Entities
{
    public class NoteEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Text { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public override string ToString() => Date.ToString();
    }
}
