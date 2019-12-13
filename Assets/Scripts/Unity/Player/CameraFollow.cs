using UnityEngine;

namespace com.example.unity.player
{
    public class CameraFollow : MonoBehaviour
    {
        public Vector3 Offset = new Vector3(0, 12.7f, -7.59f);
        public Vector3 Rotation = new Vector3(46.4f, 0, 0);

        private Camera m_Cam;
        private GameObject m_FollowObj;

        public void Init(Camera cam, GameObject followObj)
        {
            m_Cam = cam;
            m_FollowObj = followObj;
            m_Cam.transform.localEulerAngles = Rotation;
        }

        void Update()
        {
            if (m_FollowObj != null)
                m_Cam.transform.position = m_FollowObj.transform.position + Offset;
        }
    }
}