using com.example.map;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace com.example.unity.map
{
    public class FileMapSerializator : IMapSerializator
    {
        private string m_MapName;
        private IFormatter m_Formatter;

        public FileMapSerializator(string mapName)
        {
            m_MapName = mapName;
            m_Formatter = new BinaryFormatter();
        }

        public void SaveMap(IMap map)
        {
            using (Stream fileStream = new FileStream("Assets/Resources/Maps/" + m_MapName + ".bytes", FileMode.Create, FileAccess.Write))
            {
                m_Formatter.Serialize(fileStream, map);
            }
        }

        public IMap LoadMap()
        {
            TextAsset asset = Resources.Load("Maps/" + m_MapName) as TextAsset;
            IMap retVal = null;

            using (Stream fileStream = new MemoryStream(asset.bytes))
            {
                retVal = (IMap)m_Formatter.Deserialize(fileStream);
            }

            return retVal;
        }
    }
}