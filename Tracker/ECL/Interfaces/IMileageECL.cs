using System.Collections.Generic;

using Tracker.ECL.DTO;

using TrackerLib.Entities;

namespace Tracker.ECL.Interfaces
{
    public interface IMileageECL : IECL<MileageEntity, Mileage>
    {
        void Delete(int id);
        IEnumerable<Mileage> GetForClient(int cid);
        IEnumerable<Mileage> Extract();
        IEnumerable<Mileage> ExtractYear(int year);
        IEnumerable<Mileage> ExtractYear(int cid, int year);
        IEnumerable<Mileage> ExtractRange(int start, int end);
        IEnumerable<Mileage> ExtractRange(int cid, int start, int end);
        Mileage Read(int id);
        bool ClientHasMileage(int cid);
        decimal TotalMiles();
        decimal TotalMiles(int cid);
    }
}
