
using System.Collections.Generic;

namespace com.example.map
{
    [System.Serializable]
    public class CheckPointsManager : ICheckPointsManager
    {
        private SortedList<int, ICheckPoint> m_CheckPoints = new SortedList<int, ICheckPoint>();

        public void AddCheckPoint(ICheckPoint checkPoint)
        {
            m_CheckPoints.Add(checkPoint.GetIndex(), checkPoint);
        }

        public void RemoveCheckPoint(int index)
        {
            m_CheckPoints.Remove(index);
        }

        public ICollection<ICheckPoint> GetCheckPoints()
        {
            return m_CheckPoints.Values;
        }
    }
}