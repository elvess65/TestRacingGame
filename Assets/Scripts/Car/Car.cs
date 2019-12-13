
namespace com.example.car
{
    public class Car : ICar
    {
        private ISpeedProvider m_MoveSpeed;
        private ISpeedProvider m_RotateSpeed;
        private IAcceleration m_Acceleration;
        private ISurfaceInfo m_SurfaceInfo;

        public Car(ISpeedProvider moveSpeed, ISpeedProvider rotateSpeed, IAcceleration acceleration, ISurfaceInfo surfaceInfo)
        {
            m_MoveSpeed = moveSpeed;
            m_RotateSpeed = rotateSpeed;
            m_Acceleration = acceleration;
            m_SurfaceInfo = surfaceInfo;
        }

        public ISpeedProvider GetMoveSpeedProvider()
        {
            return m_MoveSpeed;
        }

        public ISpeedProvider GetRotateSpeedProvider()
        {
            return m_RotateSpeed;
        }

        public IAcceleration GetAcceleration()
        {
            return m_Acceleration;
        }

        public ISurfaceInfo GetSurfaceInfo()
        {
            return m_SurfaceInfo;
        }
    }
}