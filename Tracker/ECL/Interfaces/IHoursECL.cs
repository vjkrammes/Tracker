using System.Collections.Generic;

using Tracker.ECL.DTO;

using TrackerLib.Entities;

namespace Tracker.ECL.Interfaces
{
    public interface IHoursECL : IECL<HoursEntity, Hours>
    {
        void Delete(int id);
        IEnumerable<Hours> GetForClient(int id);
        IEnumerable<Hours> Extract();
        IEnumerable<Hours> ExtractYear(int year);
        IEnumerable<Hours> ExtractYear(int cid, int year);
        IEnumerable<Hours> ExtractRange(int start, int end);
        IEnumerable<Hours> ExtractRange(int cid, int start, int end);
        Hours Read(int id);
        bool ClientHasHours(int cid);
        decimal TotalHours();
        decimal TotalHours(int cid);
    }
}
