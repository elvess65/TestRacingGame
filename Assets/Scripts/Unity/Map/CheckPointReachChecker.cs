
using UnityEngine;

namespace com.example.unity.map
{
    public class CheckPointReachChecker : ICheckPointIsReached
    {
        private Transform m_ObjectToTrack;

        private static float m_DISTANCE_THRESHOLD = 6;

        public CheckPointReachChecker(Transform objToTrack)
        {
            m_ObjectToTrack = objToTrack;
        }

        public bool IsReached(Vector3 checkPointPos)
        {
            return (m_ObjectToTrack.position - checkPointPos).sqrMagnitude <= m_DISTANCE_THRESHOLD;
        }
    }
}