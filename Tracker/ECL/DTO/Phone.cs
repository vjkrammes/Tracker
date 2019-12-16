using System;

using TrackerCommon;

namespace Tracker.ECL.DTO
{
    public class Phone : NotifyBase, IEquatable<Phone>, IComparable<Phone>
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private int _phoneTypeId;
        public int PhoneTypeId
        {
            get => _phoneTypeId;
            set => SetProperty(ref _phoneTypeId, value);
        }

        private int _clientId;
        public int ClientId
        {
            get => _clientId;
            set => SetProperty(ref _clientId, value);
        }

        private string _number;
        public string Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }

        private byte[] _rowVersion;
        public byte[] RowVersion
        {
            get => _rowVersion;
            set => SetProperty(ref _rowVersion, value);
        }

        private PhoneType _phoneType;
        public PhoneType PhoneType
        {
            get => _phoneType;
            set => SetProperty(ref _phoneType, value);
        }

        public Phone()
        {
            Id = 0;
            PhoneTypeId = 0;
            ClientId = 0;
            Number = string.Empty;
        }

        public Phone Clone() => new Phone
        {
            Id = Id,
            PhoneTypeId = PhoneTypeId,
            ClientId = ClientId,
            Number = Number ?? string.Empty,
            PhoneType = PhoneType?.Clone(),
            RowVersion = RowVersion.ArrayCopy()
        };

        public override string ToString() => Number;

        public override bool Equals(object obj)
        {
            if (!(obj is Phone p))
            {
                return false;
            }
            return p.Id == Id;
        }

        public bool Equals(Phone p) => p is null ? false : p.Id == Id;

        public override int GetHashCode() => Id;

        public static bool operator ==(Phone a, Phone b) => (a, b) switch
        {
            (null, null) => true,
            (null, _) => false,
            (_, null) => false,
            (_, _) => a.Id == b.Id
        };

        public static bool operator !=(Phone a, Phone b) => !(a == b);

        public int CompareTo(Phone other) => Number.CompareTo(other.Number);

        public static bool operator >(Phone a, Phone b) => a.CompareTo(b) > 0;

        public static bool operator <(Phone a, Phone b) => a.CompareTo(b) < 0;

        public static bool operator >=(Phone a, Phone b) => a.CompareTo(b) >= 0;

        public static bool operator <=(Phone a, Phone b) => a.CompareTo(b) <= 0;
    }
}
