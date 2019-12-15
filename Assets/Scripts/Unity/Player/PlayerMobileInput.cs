using com.example.game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace com.example.unity.player
{
    public class PlayerMobileInput : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        public GameObject InputRoot;
        public Image InputImage;
        public Image Nitro;
        public float InputXOffset = 50;

        private UnityCar m_CarObj;
        private IGame m_Game;
        private float m_StartX;
        private bool m_IsNitro = false;

        public void Init(UnityCar carObj, IGame game)
        {
            m_CarObj = carObj;
            m_Game = game;
        }

        void Start()
        {
            m_StartX = InputImage.transform.position.x;
        }

        public void OnDrag(PointerEventData eventData)
        {
            InputImage.transform.position = new Vector3(Mathf.Min(Mathf.Max(eventData.position.x, m_StartX - InputXOffset), m_StartX + InputXOffset),
                                                                            InputImage.transform.position.y,
                                                                            InputImage.transform.position.z);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            InputImage.transform.position = new Vector3(m_StartX, InputImage.transform.position.y, InputImage.transform.position.z);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            InputRoot.transform.position = eventData.position;
            m_StartX = InputImage.transform.position.x;
        }

        public void ClickNitro()
        {
            m_IsNitro = true;
        }

        public void ReleaseNitro()
        {
            m_IsNitro = false;
        }

        void Update()
        {
            if (m_Game.IsGameStarted() && !m_Game.IsFinish())
            {
                float axis = (InputImage.transform.position.x - m_StartX) / InputXOffset;

                m_CarObj.SetInput(axis);
                m_CarObj.Accelerate(m_IsNitro);
            }
        }
    }
}