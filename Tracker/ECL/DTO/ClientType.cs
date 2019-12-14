using System;

using TrackerCommon;

namespace Tracker.ECL.DTO
{
    public class ClientType : NotifyBase, IEquatable<ClientType>, IComparable<ClientType>
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

        private string _background;
        public string Background
        {
            get => _background;
            set => SetProperty(ref _background, value);
        }

        private long _argb;
        public long ARGB
        {
            get => _argb;
            set => SetProperty(ref _argb, value);
        }

        private byte[] _rowVersion;
        public byte[] RowVersion
        {
            get => _rowVersion;
            set => SetProperty(ref _rowVersion, value);
        }

        public ClientType()
        {
            Id = 0;
            Name = string.Empty;
            Background = "White";
            ARGB = 0xFFFFFFFF;
        }

        public ClientType Clone() => new ClientType
        {
            Id = Id,
            Name = Name ?? string.Empty,
            Background = Background ?? "White",
            ARGB = ARGB,
            RowVersion = RowVersion.ArrayCopy()
        };

        public override string ToString() => Name;

        public override bool Equals(object obj)
        {
            if (!(obj is ClientType c))
            {
                return false;
            }
            return c.Id == Id;
        }

        public bool Equals(ClientType c) => c is null ? false : c.Id == Id;

        public override int GetHashCode() => Id;

        public static bool operator ==(ClientType a, ClientType b) => (a, b) switch
        {
            (null, null) => true,
            (null, _) => false,
            (_, null) => false,
            (_, _) => a.Id == b.Id
        };

        public static bool operator !=(ClientType a, ClientType b) => !(a == b);

        public int CompareTo(ClientType other) => Name.CompareTo(other.Name);

        public static bool operator >(ClientType a, ClientType b) => a.CompareTo(b) > 0;

        public static bool operator <(ClientType a, ClientType b) => a.CompareTo(b) < 0;

        public static bool operator >=(ClientType a, ClientType b) => a.CompareTo(b) >= 0;

        public static bool operator <=(ClientType a, ClientType b) => a.CompareTo(b) <= 0;
    }
}
