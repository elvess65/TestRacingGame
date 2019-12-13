using UnityEngine;

namespace com.example.unity.map
{
    public interface ICheckPointIsReached
    {
        bool IsReached(Vector3 checkPointPos);
    }
}