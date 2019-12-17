using System.Collections.Generic;

using TrackerLib.Entities;

namespace TrackerLib.Interfaces
{
    public interface IHoursDAL : IDAL<HoursEntity>, IIdDAL<HoursEntity>
    {
        IEnumerable<HoursEntity> GetForClient(int cid);
        IEnumerable<HoursEntity> Extract();
        IEnumerable<HoursEntity> ExtractYear(int year);
        IEnumerable<HoursEntity> ExtractYear(int cid, int year);
        IEnumerable<HoursEntity> ExtractRange(int start, int end);
        IEnumerable<HoursEntity> ExtractRange(int cid, int start, int end);
        bool ClientHasHours(int cid);
        decimal TotalHours();
        decimal TotalHours(int cid);
    }
}
