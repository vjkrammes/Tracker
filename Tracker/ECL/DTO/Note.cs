using System;

using TrackerCommon;

namespace Tracker.ECL.DTO
{
    public class Note : NotifyBase, IEquatable<Note>, IComparable<Note>
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

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private byte[] _rowVersion;
        public byte[] RowVersion
        {
            get => _rowVersion;
            set => SetProperty(ref _rowVersion, value);
        }

        public Note()
        {
            Id = 0;
            ClientId = 0;
            Date = default;
            Text = string.Empty;
        }

        public Note Clone() => new Note
        {
            Id = Id,
            ClientId = ClientId,
            Date = Date,
            Text = Text ?? string.Empty,
            RowVersion = RowVersion.ArrayCopy()
        };

        public override string ToString() => Date.ToString();

        public override bool Equals(object obj)
        {
            if (!(obj is Note n))
            {
                return false;
            }
            return n.Id == Id;
        }

        public bool Equals(Note n) => n is null ? false : n.Id == Id;

        public override int GetHashCode() => Id;

        public static bool operator ==(Note a, Note b) => (a, b) switch
        {
            (null, null) => true,
            (null, _) => false,
            (_, null) => false,
            (_, _) => a.Id == b.Id
        };

        public static bool operator !=(Note a, Note b) => !(a == b);

        public int CompareTo(Note other) => Date.CompareTo(other.Date);

        public static bool operator >(Note a, Note b) => a.CompareTo(b) > 0;

        public static bool operator <(Note a, Note b) => a.CompareTo(b) < 0;

        public static bool operator >=(Note a, Note b) => a.CompareTo(b) >= 0;

        public static bool operator <=(Note a, Note b) => a.CompareTo(b) <= 0;
    }
}
