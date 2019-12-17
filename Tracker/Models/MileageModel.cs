using System;
using System.Collections.Generic;
using System.Linq;

using Tracker.ECL.DTO;

using TrackerCommon;

namespace Tracker.Models
{
    public class MileageModel : NotifyBase, IEquatable<MileageModel>, IComparable<MileageModel>
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _client;
        public string Client
        {
            get => _client;
            set => SetProperty(ref _client, value);
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private decimal _miles;
        public decimal Miles
        {
            get => _miles;
            set => SetProperty(ref _miles, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private bool _isSummary;
        public bool IsSummary
        {
            get => _isSummary;
            set => SetProperty(ref _isSummary, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public MileageModel()
        {
            Id = Guid.NewGuid();
            Client = String.Empty;
            Date = default;
            Miles = 0M;
            Description = string.Empty;
            IsSummary = false;
            IsSelected = false;
        }

        public MileageModel(Client client, Mileage mileage) : this()
        {
            Client = client.Name;
            Date = mileage.Date;
            Miles = mileage.Miles;
            Description = mileage.Description ?? string.Empty;
        }

        public MileageModel(Client client, IEnumerable<Mileage> mileage) : this()
        {
            Client = client.Name;
            Miles = mileage.Sum(x => x.Miles);
            Description = "Summary";
            IsSummary = true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MileageModel m))
            {
                return false;
            }
            return m.Id == Id;
        }

        public bool Equals(MileageModel m) => m is null ? false : m.Id == Id;

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(MileageModel a, MileageModel b) => (a, b) switch
        {
            (null, null) => true,
            (null, _) => false,
            (_, null) => false,
            (_, _) => a.Id == b.Id
        };

        public static bool operator !=(MileageModel a, MileageModel b) => !(a == b);

        public int CompareTo(MileageModel other) => Date.CompareTo(other.Date);

        public static bool operator >(MileageModel a, MileageModel b) => a.CompareTo(b) > 0;

        public static bool operator <(MileageModel a, MileageModel b) => a.CompareTo(b) < 0;

        public static bool operator >=(MileageModel a, MileageModel b) => a.CompareTo(b) >= 0;

        public static bool operator <=(MileageModel a, MileageModel b) => a.CompareTo(b) <= 0;
    }
}
