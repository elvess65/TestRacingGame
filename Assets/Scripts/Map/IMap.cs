
namespace com.example.map
{
    public interface IMap
    {
        int GetWidth();
        int GetHeight();
        IMapCell GetCell(int x, int y);
        ICheckPointsManager GetCheckPointsManager();
    }
}