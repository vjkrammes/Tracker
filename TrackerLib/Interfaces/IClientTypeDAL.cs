using TrackerLib.Entities;

namespace TrackerLib.Interfaces
{
    public interface IClientTypeDAL : IDAL<ClientTypeEntity>, IIdDAL<ClientTypeEntity>
    {
        ClientTypeEntity Read(string name);
    }
}
