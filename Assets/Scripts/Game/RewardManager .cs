
namespace com.example.game
{
    public class RewardManager : IRewardManager
    {
        private float[] m_TimeForStars = new float[2];

        public RewardManager(float timeFor3Star, float timeFor2Star)
        {
            m_TimeForStars[0] = timeFor3Star;
            m_TimeForStars[1] = timeFor2Star;
        }

        public int GetRewardStars(float finishTime)
        {
            int stars = 1;
            for (int i = 0; i < m_TimeForStars.Length; ++i)
                if (finishTime < m_TimeForStars[i])
                    ++stars;

            return stars;
        }
    }
}