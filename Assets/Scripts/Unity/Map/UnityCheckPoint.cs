using com.example.map;
using UnityEngine;

namespace com.example.unity.map
{
    public class UnityCheckPoint : MonoBehaviour
    {
        public Renderer[] ObjectsChangeColors;

        private bool m_IsReached = false;
        private float m_ReachTime = 0;
        private ICheckPoint m_CheckPoint;

        public ICheckPoint GetCheckPoint()
        {
            return m_CheckPoint;
        }

        public void Init(ICheckPoint checkPoint)
        {
            m_CheckPoint = checkPoint;
        }

        public bool CheckReached(ICheckPointIsReached reachedChecker)
        {
            return reachedChecker.IsReached(transform.position);
        }

        public void SetReached(bool val, float reachTime)
        {
            m_IsReached = val;

            if (val)
                m_ReachTime = reachTime;
            else
                m_ReachTime = 0;

            foreach (Renderer rend in ObjectsChangeColors)
                rend.material.color = Color.green;
        }

        public bool IsReached()
        {
            return m_IsReached;
        }

        public float GetReachTime()
        {
            return m_ReachTime;
        }
    }
}