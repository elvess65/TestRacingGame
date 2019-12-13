
namespace com.example.map
{
    [System.Serializable]
    public class MapCell : IMapCell
    {
        private byte m_Type = 0;
        private byte m_SurfaceType = 0;

        public MapCell(byte type, byte surfaceType)
        {
            m_Type = type;
            m_SurfaceType = surfaceType;
        }

        public void SetCellType(byte type)
        {
            m_Type = type;
        }

        public byte GetCellType()
        {
            return m_Type;
        }

        public byte GetSurfaceType()
        {
            return m_SurfaceType;
        }

        public void SetSurfaceType(byte surfaceType)
        {
            m_SurfaceType = surfaceType;
        }
    }
}