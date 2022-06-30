using DataLayer.Models;
public interface IMuseumRepository : IRepository<Museum>
{
    int GetMuseumIdByName(string name);

}
