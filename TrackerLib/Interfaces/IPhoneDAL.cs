using System.Collections.Generic;

using TrackerLib.Entities;

namespace TrackerLib.Interfaces
{
    public interface IPhoneDAL : IDAL<PhoneEntity>, IIdDAL<PhoneEntity>
    {
        IEnumerable<PhoneEntity> GetForClient(int cid);
        IEnumerable<PhoneEntity> GetForPhoneType(int ptid);
        bool ClientHasPhones(int cid);
        bool PhoneTypeHasPhones(int ptid);
    }
}
