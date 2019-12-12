using System.Collections.Generic;

using Tracker.ECL.DTO;

using TrackerLib.Entities;

namespace Tracker.ECL.Interfaces
{
    public interface IMileageECL : IECL<MileageEntity, Mileage>
    {
        void Delete(int id);
        IEnumerable<Mileage> GetForClient(int cid);
        Mileage Read(int id);
        bool ClientHasMileage(int cid);
        decimal TotalMiles();
        decimal TotalMiles(int cid);
    }
}
