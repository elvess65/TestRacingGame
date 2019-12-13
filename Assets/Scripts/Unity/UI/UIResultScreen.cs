using UnityEngine;

namespace com.example.unity.ui
{
    public class UIResultScreen : MonoBehaviour
    {
        public UnityEngine.UI.Image[] Stars;
        public UnityEngine.UI.Text TimeText;

        public void ShowStars(int count)
        {
            for (int i = 0; i < Mathf.Min(count, Stars.Length); ++i)
                Stars[i].gameObject.SetActive(true);
        }

        public void SetTime(float time)
        {
            TimeText.text = time.ToString();
        }
    }
}