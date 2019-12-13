
namespace com.example.car
{
    public class SurfaceInfo
    {
        public SurfaceInfo(float acceleration, float speedMultyplayer)
        {
            Acceleration = acceleration;
            SpeedMultyplayer = speedMultyplayer;
        }

        public float Acceleration
        {
            get; private set;
        }

        public float SpeedMultyplayer
        {
            get; private set;
        }
    }
}