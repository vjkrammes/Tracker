using System;
using System.ComponentModel.DataAnnotations;
using TrackerCommon;

namespace TrackerLib.Entities
{
    [HasNullableMembers]
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

        [NullOnInsert, NullOnUpdate]
        public ClientEntity Client { get; set; }
        public override string ToString() => Date.ToString();
    }
}
