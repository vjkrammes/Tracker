using Tracker.ECL.DTO;

using TrackerLib.Entities;

namespace Tracker.ECL.Interfaces
{
    public interface IPhoneTypeECL : IECL<PhoneTypeEntity, PhoneType>
    {
        void Delete(int id);
        PhoneType Read(int id);
        PhoneType Read(string name);
    }
}
