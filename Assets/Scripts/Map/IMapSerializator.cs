
namespace com.example.map
{
    public interface IMapSerializator
    {
        void SaveMap(IMap map);
        IMap LoadMap();
    }
}