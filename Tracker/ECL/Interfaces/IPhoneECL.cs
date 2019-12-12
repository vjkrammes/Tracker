using System.Collections.Generic;

using Tracker.ECL.DTO;

using TrackerLib.Entities;

namespace Tracker.ECL.Interfaces
{
    public interface IPhoneECL : IECL<PhoneEntity, Phone>
    {
        void Delete(int id);
        IEnumerable<Phone> GetForClient(int cid);
        IEnumerable<Phone> GetForPhoneType(int ptid);
        Phone Read(int id);
        bool ClientHasPhones(int cid);
        bool PhoneTypeHasPhones(int ptid);
    }
}
