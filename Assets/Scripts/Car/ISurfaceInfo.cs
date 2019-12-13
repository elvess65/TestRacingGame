
namespace com.example.car
{
    public interface ISurfaceInfo
    {
        void SetCurrentSurface(byte surface);
        byte GetCurrentSurface();
        float GetSurfaceAcceleration();
        float GetSurfaceSpeedMultiplayer();
    }
}