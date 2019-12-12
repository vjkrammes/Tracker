using System;

using TrackerCommon;

namespace Tracker.ECL.DTO
{
    public class PhoneType : NotifyBase, IEquatable<PhoneType>, IComparable<PhoneType>
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _imageUri;
        public string ImageUri
        {
            get => _imageUri;
            set => SetProperty(ref _imageUri, value);
        }

        private byte[] _rowVersion;
        public byte[] RowVersion
        {
            get => _rowVersion;
            set => SetProperty(ref _rowVersion, value);
        }

        public PhoneType()
        {
            Id = 0;
            Name = string.Empty;
            ImageUri = string.Empty;
        }

        public PhoneType Clone() => new PhoneType
        {
            Id = Id,
            Name = Name ?? string.Empty,
            ImageUri = ImageUri ?? string.Empty,
            RowVersion = RowVersion.ArrayCopy()
        };

        public override string ToString() => Name;

        public override bool Equals(object obj)
        {
            if (!(obj is PhoneType p))
            {
                return false;
            }
            return p.Id == Id;
        }

        public bool Equals(PhoneType p) => p is null ? false : p.Id == Id;

        public override int GetHashCode() => Id;

        public static bool operator ==(PhoneType a, PhoneType b) => (a, b) switch
        {
            (null, null) => true,
            (null, _) => false,
            (_, null) => false,
            (_, _) => a.Id == b.Id
        };

        public static bool operator !=(PhoneType a, PhoneType b) => !(a == b);

        public int CompareTo(PhoneType other) => Name.CompareTo(other.Name);

        public static bool operator >(PhoneType a, PhoneType b) => a.CompareTo(b) > 0;

        public static bool operator <(PhoneType a, PhoneType b) => a.CompareTo(b) < 0;

        public static bool operator >=(PhoneType a, PhoneType b) => a.CompareTo(b) >= 0;

        public static bool operator <=(PhoneType a, PhoneType b) => a.CompareTo(b) <= 0;
    }
}
