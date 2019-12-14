using System.ComponentModel.DataAnnotations;

namespace TrackerLib.Entities
{
    public class ClientTypeEntity
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        public string Background { get; set; }
        [Required]
        public long ARGB { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public override string ToString() => Name;
    }
}
