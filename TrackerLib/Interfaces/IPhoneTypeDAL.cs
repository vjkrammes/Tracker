using TrackerLib.Entities;

namespace TrackerLib.Interfaces
{
    public interface IPhoneTypeDAL : IDAL<PhoneTypeEntity>, IIdDAL<PhoneTypeEntity>
    {
        PhoneTypeEntity Read(string name);
    }
}
