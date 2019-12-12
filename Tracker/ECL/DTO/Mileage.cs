using System;

using TrackerCommon;

namespace Tracker.ECL.DTO
{
    public class Mileage : NotifyBase, IEquatable<Mileage>, IComparable<Mileage>
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

        private decimal _miles;
        public decimal Miles
        {
            get => _miles;
            set => SetProperty(ref _miles, value);
        }

        private byte[] _rowVersion;
        public byte[] RowVersion
        {
            get => _rowVersion;
            set => SetProperty(ref _rowVersion, value);
        }

        public Mileage()
        {
            Id = 0;
            ClientId = 0;
            Date = default;
            Description = string.Empty;
            Miles = 0M;
        }

        public Mileage Clone() => new Mileage
        {
            Id = Id,
            ClientId = ClientId,
            Date = Date,
            Description = Description ?? string.Empty,
            Miles = Miles,
            RowVersion = RowVersion.ArrayCopy()
        };

        public override string ToString() => $"{Date.ToShortDateString()} - {Miles:n2} miles";

        public override bool Equals(object obj)
        {
            if (!(obj is Mileage m))
            {
                return false;
            }
            return m.Id == Id;
        }

        public bool Equals(Mileage m) => m is null ? false : m.Id == Id;

        public override int GetHashCode() => Id;

        public static bool operator ==(Mileage a, Mileage b) => (a, b) switch
        {
            (null, null) => true,
            (null, _) => false,
            (_, null) => false,
            (_, _) => a.Id == b.Id
        };

        public static bool operator !=(Mileage a, Mileage b) => !(a == b);

        public int CompareTo(Mileage other) => Date.CompareTo(other.Date);

        public static bool operator >(Mileage a, Mileage b) => a.CompareTo(b) > 0;

        public static bool operator <(Mileage a, Mileage b) => a.CompareTo(b) < 0;

        public static bool operator >=(Mileage a, Mileage b) => a.CompareTo(b) >= 0;

        public static bool operator <=(Mileage a, Mileage b) => a.CompareTo(b) <= 0;
    }
}
