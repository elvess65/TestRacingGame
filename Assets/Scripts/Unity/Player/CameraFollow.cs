using com.example.car;
using UnityEngine;

namespace com.example.unity.player
{
    public class CameraFollow : MonoBehaviour
    {
        public Vector3 Offset = new Vector3(0, 12.7f, -7.59f);
        public Vector3 Rotation = new Vector3(46.4f, 0, 0);
        public float MaxSpeedOffset = 2;

        private Camera m_Cam;
        private GameObject m_FollowObj;
        private ICar m_Car;

        public void Init(Camera cam, GameObject followObj, ICar car)
        {
            m_Cam = cam;
            m_FollowObj = followObj;
            m_Car = car;
            m_Cam.transform.localEulerAngles = Rotation;
        }

        void Update()
        {
            if (m_FollowObj != null)
            {
                Vector3 speedOffset = m_FollowObj.transform.forward * MaxSpeedOffset * m_Car.GetMoveSpeedProvider().GetSpeed() / m_Car.GetMoveSpeedProvider().GetMaxSpeed();
                m_Cam.transform.position = m_FollowObj.transform.position + Offset + speedOffset;
            }
        }
    }
}