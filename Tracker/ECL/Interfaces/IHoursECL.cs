using System.Collections.Generic;

using Tracker.ECL.DTO;

using TrackerLib.Entities;

namespace Tracker.ECL.Interfaces
{
    public interface IHoursECL : IECL<HoursEntity, Hours>
    {
        void Delete(int id);
        IEnumerable<Hours> GetForClient(int id);
        Hours Read(int id);
        bool ClientHasHours(int cid);
        decimal TotalHours();
        decimal TotalHours(int cid);
    }
}
