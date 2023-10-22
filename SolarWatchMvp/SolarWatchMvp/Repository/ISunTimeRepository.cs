using SolarWatchMvp.Model;

namespace SolarWatchMvp.Repository;

public interface ISunTimeRepository
{
    IEnumerable<SunTime> GetAll();
    SunTime? GetByName(int id);
    void Add(SunTime time);
    void Delete(SunTime time);
    void Update(SunTime time);
}