using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using TrackerCommon;

namespace TrackerLib.Entities
{
    [HasNullableMembers]
    public class ClientEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ClientTypeId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        public string Address { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }
        [Required, MaxLength(50)]
        public string State { get; set; }
        [Required, MaxLength(50)]
        public string PostalCode { get; set; }
        [Required, MaxLength(50)]
        public string PrimaryContact { get; set; }
        [Required]
        public string Comments { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [NullOnUpdate]
        public ClientTypeEntity ClientType { get; set; }

        [NullOnUpdate]
        public List<PhoneEntity> Phones { get; set; }

        [NullOnUpdate]
        public List<NoteEntity> Notes { get; set; }

        public override string ToString() => $"{Name ?? "Unknown"} ({PrimaryContact ?? "Unknown"})";

    }
}
