namespace TrackerLib.Interfaces
{
    public interface IIdDAL<T> where T : class, new()
    {
        void Delete(int id);
        T Read(int id);
    }
}
