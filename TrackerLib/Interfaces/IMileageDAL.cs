using System.Collections.Generic;

using TrackerLib.Entities;

namespace TrackerLib.Interfaces
{
    public interface IMileageDAL : IDAL<MileageEntity>, IIdDAL<MileageEntity>
    {
        IEnumerable<MileageEntity> GetForClient(int cid);
        bool ClientHasMileage(int cid);
        decimal TotalMiles();
        decimal TotalMiles(int cid);
    }
}
