using com.example.game;
using UnityEngine;

namespace com.example.unity.player
{
    public class PlayerPCInput : MonoBehaviour
    {
        private UnityCar m_CarObj;
        private IGame m_Game;

        public void Init(UnityCar carObj, IGame game)
        {
            m_CarObj = carObj;
            m_Game = game;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_Game.IsGameStarted() && !m_Game.IsFinish())
            {
                m_CarObj.SetInput(Input.GetAxisRaw("Horizontal"));
                m_CarObj.Accelerate(Input.GetKey(KeyCode.Space));
            }
        }
    }
}