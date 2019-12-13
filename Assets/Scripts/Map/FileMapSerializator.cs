using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace com.example.map
{
    public class FileMapSerializator : IMapSerializator
    {
        private string m_Path;
        private IFormatter m_Formatter;

        public FileMapSerializator(string filePath)
        {
            m_Path = filePath;
            m_Formatter = new BinaryFormatter();
        }

        public void SaveMap(IMap map)
        {
            using (Stream fileStream = new FileStream(m_Path, FileMode.Create, FileAccess.Write))
            {
                m_Formatter.Serialize(fileStream, map);
            }
        }

        public IMap LoadMap()
        {
            IMap retVal = null;
            using (Stream fileStream = new FileStream(m_Path, FileMode.Open, FileAccess.Read))
            {
                retVal = (IMap)m_Formatter.Deserialize(fileStream);
            }

            return retVal;
        }
    }
}