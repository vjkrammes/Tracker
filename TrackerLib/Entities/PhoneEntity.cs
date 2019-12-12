using System.ComponentModel.DataAnnotations;

using TrackerCommon;

namespace TrackerLib.Entities
{
    [HasNullableMembers]
    public class PhoneEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PhoneTypeId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required, MaxLength(50)]
        public string Number { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [NullOnUpdate]
        public PhoneTypeEntity PhoneType { get; set; }

        public override string ToString() => Number;
    }
}
