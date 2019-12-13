
namespace com.example.car
{
    public interface ICar
    {
        ISpeedProvider GetMoveSpeedProvider();
        ISpeedProvider GetRotateSpeedProvider();
        IAcceleration GetAcceleration();
        ISurfaceInfo GetSurfaceInfo();
    }
}