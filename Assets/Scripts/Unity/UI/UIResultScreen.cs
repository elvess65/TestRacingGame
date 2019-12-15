using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace com.example.unity.ui
{
    public class UIResultScreen : MonoBehaviour
    {
        public Image[] Stars;
        public Text TimeText;
        public Button RestartButton;

        private void Awake()
        {
            RestartButton.onClick.AddListener(RestartClickHandler);
        }

        private void RestartClickHandler()
        {
            SceneManager.LoadScene("RoadMap");
        }

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