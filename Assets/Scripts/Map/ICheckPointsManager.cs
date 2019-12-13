
using System.Collections.Generic;

namespace com.example.map
{
    public interface ICheckPointsManager
    {
        void AddCheckPoint(ICheckPoint checkPoint);
        void RemoveCheckPoint(int index);
        ICollection<ICheckPoint> GetCheckPoints();
    }
}