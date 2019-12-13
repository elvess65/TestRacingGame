using com.example.car;
using UnityEngine;

namespace com.example.unity.ui
{
    public class UIController : MonoBehaviour
    {
        public SpeedometerUI Speedometer;

        private ICar m_Car;

        public void Init(ICar car)
        {
            m_Car = car;
        }

        void Update()
        {
            float t = m_Car.GetMoveSpeedProvider().GetSpeed() / m_Car.GetMoveSpeedProvider().GetMaxSpeed();
            Speedometer.SetSpeedValue(t);
        }
    }
}