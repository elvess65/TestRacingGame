
namespace com.example.map
{
    [System.Serializable]
    public class DefaultCheckPoint : ICheckPoint
    {
        private int m_X;
        private int m_Y;
        private byte m_Type;
        private int m_Index;

        public DefaultCheckPoint(int x, int y, byte type, int index)
        {
            m_X = x;
            m_Y = y;
            m_Type = type;
            m_Index = index;
        }

        public int GetX()
        {
            return m_X;
        }

        public int GetY()
        {
            return m_Y;
        }

        public byte GetPointType()
        {
            return m_Type;
        }

        public int GetIndex()
        {
            return m_Index;
        }
    }
}