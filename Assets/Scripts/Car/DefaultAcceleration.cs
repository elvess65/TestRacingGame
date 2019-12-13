
namespace com.example.car
{
    public class DefaultAcceleration : IAcceleration
    {
        private ISpeedProvider m_Speed;
        private ISurfaceInfo m_Surface;

        public DefaultAcceleration(ISpeedProvider moveSpeed, ISurfaceInfo surface)
        {
            m_Speed = moveSpeed;
            m_Surface = surface;
        }

        public void Accelerate(float t)
        {
            float acceleration = m_Surface.GetSurfaceAcceleration();
            m_Speed.IncreaseSpeed(acceleration * t);
        }
    }
}