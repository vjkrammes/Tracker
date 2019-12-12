using System.Collections.Generic;

using TrackerLib.Entities;

namespace TrackerLib.Interfaces
{
    public interface IHoursDAL : IDAL<HoursEntity>, IIdDAL<HoursEntity>
    {
        IEnumerable<HoursEntity> GetForClient(int cid);
        bool ClientHasHours(int cid);
        decimal TotalHours();
        decimal TotalHours(int cid);
    }
}
