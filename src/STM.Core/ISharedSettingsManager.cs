using STM.Core.Data;

namespace STM.Core
{
    public interface ISharedSettingsManager
    {
        void Delete(string name);
        SharedConnectionSettings GetOrCreate(string name);
        void Save(SharedConnectionSettings profile);
    }
}