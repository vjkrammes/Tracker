using System;
using System.Collections.ObjectModel;

using TrackerCommon;

namespace Tracker.ECL.DTO
{
    public class Client : NotifyBase, IEquatable<Client>, IComparable<Client>
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

        private string _address;
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private string _city;
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        private string _state;
        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        private string _postalCode;
        public string PostalCode
        {
            get => _postalCode;
            set => SetProperty(ref _postalCode, value);
        }

        private string _primaryContact;
        public string PrimaryContact
        {
            get => _primaryContact;
            set => SetProperty(ref _primaryContact, value);
        }

        private string _comments;
        public string Comments
        {
            get => _comments;
            set => SetProperty(ref _comments, value);
        }

        private byte[] _rowVersion;
        public byte[] RowVersion
        {
            get => _rowVersion;
            set => SetProperty(ref _rowVersion, value);
        }

        private ObservableCollection<Phone> _phones;
        public ObservableCollection<Phone> Phones
        {
            get => _phones;
            set => SetProperty(ref _phones, value);
        }

        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        public Client()
        {
            Id = 0;
            Name = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            State = string.Empty;
            PostalCode = string.Empty;
            PrimaryContact = string.Empty;
            Comments = string.Empty;
        }

        public Client Clone() => new Client
        {
            Id = Id,
            Name = Name ?? string.Empty,
            Address = Address ?? string.Empty,
            City = City ?? string.Empty,
            State = State ?? string.Empty,
            PostalCode = PostalCode ?? string.Empty,
            PrimaryContact = PrimaryContact ?? string.Empty,
            Comments = Comments ?? string.Empty,
            RowVersion = RowVersion.ArrayCopy()
        };

        public override string ToString() => Name;

        public override bool Equals(object obj)
        {
            if (!(obj is Client c))
            {
                return false;
            }
            return c.Id == Id;
        }

        public bool Equals(Client c) => c is null ? false : c.Id == Id;

        public override int GetHashCode() => Id;

        public static bool operator ==(Client a, Client b) => (a, b) switch
        {
            (null, null) => true,
            (null, _) => false,
            (_, null) => false,
            (_, _) => a.Id == b.Id
        };

        public static bool operator !=(Client a, Client b) => !(a == b);

        public int CompareTo(Client other) => Name.CompareTo(other.Name);

        public static bool operator >(Client a, Client b) => a.CompareTo(b) > 0;

        public static bool operator <(Client a, Client b) => a.CompareTo(b) < 0;

        public static bool operator >=(Client a, Client b) => a.CompareTo(b) >= 0;

        public static bool operator <=(Client a, Client b) => a.CompareTo(b) <= 0;
    }
}
