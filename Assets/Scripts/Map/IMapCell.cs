
namespace com.example.map
{
    public interface IMapCell
    {
        byte GetCellType();
        void SetCellType(byte type);
        byte GetSurfaceType();
        void SetSurfaceType(byte surfaceType);
    }
}