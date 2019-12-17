using System.Collections.Generic;

using TrackerLib.Entities;

namespace TrackerLib.Interfaces
{
    public interface IMileageDAL : IDAL<MileageEntity>, IIdDAL<MileageEntity>
    {
        IEnumerable<MileageEntity> GetForClient(int cid);
        IEnumerable<MileageEntity> Extract();
        IEnumerable<MileageEntity> ExtractYear(int year);
        IEnumerable<MileageEntity> ExtractYear(int cid, int year);
        IEnumerable<MileageEntity> ExtractRange(int start, int end);
        IEnumerable<MileageEntity> ExtractRange(int cid, int start, int end);
        bool ClientHasMileage(int cid);
        decimal TotalMiles();
        decimal TotalMiles(int cid);
    }
}
