
namespace com.example.game
{
    public class Game : IGame
    {
        private bool m_IsFinish = false;
        private float m_StartTime = 0;

        public void Start(float time)
        {
            m_StartTime = time;
        }

        public void SetIsFinish(bool val)
        {
            m_IsFinish = val;
        }

        public bool IsGameStarted()
        {
            return true;
        }

        public bool IsFinish()
        {
            return m_IsFinish;
        }

        public float GetStartTime()
        {
            return m_StartTime;
        }
    }
}
