
namespace com.example.car
{
    public class DefaultSpeed : ISpeedProvider
    {
        private float m_MaxSpeed;
        private float m_CurrentSpeed;

        public DefaultSpeed(float maxSpeed)
        {
            m_MaxSpeed = maxSpeed;
            m_CurrentSpeed = 0;
        }

        public float GetMaxSpeed()
        {
            return m_MaxSpeed;
        }

        public void SetSpeed(float speed)
        {
            m_CurrentSpeed = speed;
        }

        public float GetSpeed()
        {
            return m_CurrentSpeed;
        }

        public void IncreaseSpeed(float amount)
        {
            if (m_CurrentSpeed + amount > m_MaxSpeed)
                m_CurrentSpeed = m_MaxSpeed;
            else
                m_CurrentSpeed = m_CurrentSpeed + amount;
        }

        public void DecreaseSpeed(float amount)
        {
            if (m_CurrentSpeed - amount < 0)
                m_CurrentSpeed = 0;
            else
                m_CurrentSpeed = m_CurrentSpeed - amount;
        }
    }
}