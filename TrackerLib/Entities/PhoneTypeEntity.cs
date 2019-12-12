using System.ComponentModel.DataAnnotations;

namespace TrackerLib.Entities
{
    public class PhoneTypeEntity
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(256)]
        public string ImageUri { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public override string ToString() => Name;
    }
}
