using System;
using System.Collections.Generic;
using System.Linq;

using Tracker.ECL.DTO;

using TrackerCommon;

namespace Tracker.Models
{
    public class HoursModel : NotifyBase, IEquatable<HoursModel>, IComparable<HoursModel>
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

        private decimal _time;
        public decimal Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
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

        public HoursModel()
        {
            Id = Guid.NewGuid();
            Client = string.Empty;
            Date = default;
            Time = 0M;
            Description = string.Empty;
            IsSummary = false;
            IsSelected = false;
        }

        public HoursModel(Client client, Hours hours) : this()
        {
            Client = client.Name;
            Date = hours.Date;
            Time = hours.Time;
            Description = hours.Description ?? string.Empty;
        }

        public HoursModel(Client client, IEnumerable<Hours> hours) : this()
        {
            Client = client.Name;
            Time = hours.Sum(x => x.Time);
            Description = "Summary";
            IsSummary = true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is HoursModel h))
            {
                return false;
            }
            return h.Id == Id;
        }

        public bool Equals(HoursModel h) => h is null ? false : h.Id == Id;

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(HoursModel a, HoursModel b) => (a, b) switch
        {
            (null, null) => true,
            (null, _) => false,
            (_, null) => false,
            (_, _) => a.Id == b.Id
        };

        public static bool operator !=(HoursModel a, HoursModel b) => !(a == b);

        public int CompareTo(HoursModel other) => Date.CompareTo(other.Date);

        public static bool operator >(HoursModel a, HoursModel b) => a.CompareTo(b) > 0;

        public static bool operator <(HoursModel a, HoursModel b) => a.CompareTo(b) < 0;

        public static bool operator >=(HoursModel a, HoursModel b) => a.CompareTo(b) >= 0;

        public static bool operator <=(HoursModel a, HoursModel b) => a.CompareTo(b) <= 0;
    }
}
