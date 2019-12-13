
using UnityEngine;

namespace com.example.unity.map.creator
{
    [System.Serializable]
    public class MapCellParam : MonoBehaviour
    {
        public MapCellTypes CellType;

        public int X
        {
            get; set;
        }

        public int Y
        {
            get; set;
        }
    }
}