
namespace com.example.map
{
    [System.Serializable]
    public class DefaultMap : IMap
    {
        private int m_Width;
        private int m_Height;
        private IMapCell[,] m_MapCells;
        private ICheckPointsManager m_CheckPointManager;

        public DefaultMap(int width, int height, ICheckPointsManager checkPointsManager)
        {
            m_Width = width;
            m_Height = height;
            m_MapCells = new IMapCell[width, height];
            m_CheckPointManager = checkPointsManager;

            Init();
        }

        private void Init()
        {
            for (int x = 0; x < m_Width; ++x)
                for (int y = 0; y < m_Height; ++y)
                    m_MapCells[x, y] = new MapCell(0, 0);
        }

        public int GetWidth()
        {
            return m_Width;
        }

        public int GetHeight()
        {
            return m_Height;
        }

        public IMapCell GetCell(int x, int y)
        {
            return m_MapCells[x, y];
        }

        public ICheckPointsManager GetCheckPointsManager()
        {
            return m_CheckPointManager;
        }
    }
}