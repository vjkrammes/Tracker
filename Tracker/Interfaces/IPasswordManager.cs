namespace Tracker.Interfaces
{
    public interface IPasswordManager
    {
        void Set(string password, int index = 0);
        string Get(int index = 0);
    }
}
