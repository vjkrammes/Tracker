using Tracker.ECL.DTO;

using TrackerLib.Entities;

namespace Tracker.ECL.Interfaces
{
    public interface IClientTypeECL : IECL<ClientTypeEntity, ClientType>
    {
        void Delete(int id);
        ClientType Read(int id);
        ClientType Read(string name);
    }
}
