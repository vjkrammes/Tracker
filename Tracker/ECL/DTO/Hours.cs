using System;

using TrackerCommon;

namespace Tracker.ECL.DTO
{
    public class Hours : NotifyBase, IEquatable<Hours>, IComparable<Hours>
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private int _clientId;
        public int ClientId
        {
            get => _clientId;
            set => SetProperty(ref _clientId, value);
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private decimal _time;
        public decimal Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }

        private byte[] _rowVersion;
        public byte[] RowVersion
        {
            get => _rowVersion;
            set => SetProperty(ref _rowVersion, value);
        }

        public Hours()
        {
            Id = 0;
            ClientId = 0;
            Date = default;
            Description = string.Empty;
            Time = 0M;
        }

        public Hours Clone() => new Hours
        {
            Id = Id,
            ClientId = ClientId,
            Date = Date,
            Description = Description ?? string.Empty,
            Time = Time,
            RowVersion = RowVersion.ArrayCopy()
        };

        public override string ToString() => $"{Date.ToShortDateString()} - {Time:n2} hours";

        public override bool Equals(object obj)
        {
            if (!(obj is Hours h))
            {
                return false;
            }
            return h.Id == Id;
        }

        public bool Equals(Hours h) => h is null ? false : h.Id == Id;

        public override int GetHashCode() => Id;

        public static bool operator ==(Hours a, Hours b) => (a, b) switch
        {
            (null, null) => true,
            (null, _) => false,
            (_, null) => false,
            (_, _) => a.Id == b.Id
        };

        public static bool operator !=(Hours a, Hours b) => !(a == b);

        public int CompareTo(Hours other) => Date.CompareTo(other.Date);

        public static bool operator >(Hours a, Hours b) => a.CompareTo(b) > 0;

        public static bool operator <(Hours a, Hours b) => a.CompareTo(b) < 0;

        public static bool operator >=(Hours a, Hours b) => a.CompareTo(b) >= 0;

        public static bool operator <=(Hours a, Hours b) => a.CompareTo(b) <= 0;
    }
}
