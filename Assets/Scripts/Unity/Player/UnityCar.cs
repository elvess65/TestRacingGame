using com.example.car;
using com.example.game;
using UnityEngine;

namespace com.example.unity.player
{
    public class UnityCar : MonoBehaviour
    {
        public Rigidbody Body;
        public float NitroAccelerationMultiplayer = 2;
        public static float DumpSpeedTurnTime = 10;

        private IGame m_Game;
        private ISpeedProvider m_MoveSpeed;
        private ISpeedProvider m_RotateSpeed;
        private IAcceleration m_Acceleration;
        private ISurfaceInfo m_SurfaceInfo;
        private float m_PrevSpeed = 0;
        private bool m_IsInputAvailable = false;
        private float m_LastInputTime = 0;

        public ISurfaceInfo GetSurface()
        {
            return m_SurfaceInfo;
        }

        public void Init(IGame game, ISpeedProvider moveSpeed, ISpeedProvider rotateSpeed, IAcceleration acceleration, ISurfaceInfo surfaceInfo)
        {
            m_Game = game;
            m_MoveSpeed = moveSpeed;
            m_RotateSpeed = rotateSpeed;
            m_Acceleration = acceleration;
            m_SurfaceInfo = surfaceInfo;
        }

        public void SetInput(float inputHorizontalAxis)
        {
            if (inputHorizontalAxis > 0.5f || inputHorizontalAxis < -0.5f)
            {
                if (!m_IsInputAvailable)
                {
                    m_LastInputTime = Time.time;
                    m_IsInputAvailable = true;
                }
            }
            else
                m_IsInputAvailable = false;

            float turnAngle = Body.transform.eulerAngles.y + inputHorizontalAxis * m_RotateSpeed.GetSpeed() * Time.deltaTime;
            Body.transform.rotation = Quaternion.Euler(0, turnAngle, 0);
        }

        public void Accelerate(bool nitro)
        {
            if (nitro)
                m_Acceleration.Accelerate(Time.deltaTime * NitroAccelerationMultiplayer);
            else
                m_Acceleration.Accelerate(Time.deltaTime);
        }

        void Update()
        {
            if (!m_Game.IsFinish())
            {
                if (m_Game.IsGameStarted())
                {
                    // for resolve collisions
                    float curVelocity = Body.velocity.magnitude;
                    if (m_PrevSpeed > curVelocity)
                        m_MoveSpeed.SetSpeed(curVelocity);

                    float speed = Mathf.Min(m_MoveSpeed.GetSpeed(), m_MoveSpeed.GetMaxSpeed() * m_SurfaceInfo.GetSurfaceSpeedMultiplayer());

                    float dumpSpeed = 1;
                    if (m_IsInputAvailable)
                        dumpSpeed = 1 - ((Time.time - m_LastInputTime) / DumpSpeedTurnTime * (speed / m_MoveSpeed.GetMaxSpeed()));

                    float resultSpeed = speed * dumpSpeed;
                    m_MoveSpeed.SetSpeed(resultSpeed);

                    Body.velocity = Body.transform.forward * resultSpeed;
                    m_PrevSpeed = resultSpeed;
                }
            }
            else
            {
                if (m_MoveSpeed.GetSpeed() > 0)
                {
                    m_MoveSpeed.SetSpeed(m_MoveSpeed.GetSpeed() * 0.9f);
                    Body.velocity = Body.transform.forward * m_MoveSpeed.GetSpeed();
                }
            }
        }
    }
}