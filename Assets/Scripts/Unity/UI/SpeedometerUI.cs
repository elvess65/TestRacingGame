using UnityEngine;

namespace com.example.unity.ui
{
    public class SpeedometerUI : MonoBehaviour
    {
        public GameObject ArrowObj;
        public float MinRotation = 135;
        public float MaxRotation = -135;

        public void SetSpeedValue(float t)
        {
            ArrowObj.transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(MinRotation, MaxRotation, t), Vector3.forward);
        }
    }
}