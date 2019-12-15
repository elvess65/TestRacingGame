using com.example.car;
using com.example.game;
using com.example.map;
using com.example.unity.map;
using com.example.unity.player;
using com.example.unity.ui;
using UnityEngine;

namespace com.example.unity
{
    public class LevelManager : MonoBehaviour
    {
        public MapLoader LoaderMap;
        public CarLoader LoaderCar;
        public GameObject ResultScreenPrefab;

        private IMap m_Map;
        private Game m_Game;
        private UnityCar m_UnityCar;
        private IRewardManager m_RewardManager;
        private ICheckPointIsReached m_CheckPointReachChecker;

        private void Awake()
        {
            CreateLevel("Level_1");
        }

        public void CreateLevel(string levelName)
        {
            IMapSerializator serializator = new FileMapSerializator(levelName);
            m_Map = serializator.LoadMap();

            LoaderMap.CreateMap(m_Map, false);

            CarSurface surface = new CarSurface();
            surface.AddSurface(0, 5f, 1);
            surface.AddSurface(1, 3f, 0.7f);

            ICarFactory carFactory = new DefaultCarFactory();
            ICar car = carFactory.CreateCar(10, 90, surface);

            m_Game = new Game();
            m_RewardManager = new RewardManager(30, 40);

            Vector3 carPos = LoaderMap.GetCheckPoint(0).transform.position;
            m_UnityCar = LoaderCar.CreateCar(m_Game, car, carPos, Vector3.one * 0.5f);

            m_CheckPointReachChecker = new CheckPointReachChecker(m_UnityCar.transform);

            m_Game.Start(Time.time);
        }

        private void Finish()
        {
            m_Game.SetIsFinish(true);

            GameObject resultGo = Instantiate(ResultScreenPrefab);
            UIResultScreen resultScreen = resultGo.GetComponent<UIResultScreen>();

            float finishTime = Time.time - m_Game.GetStartTime();

            resultScreen.SetTime(finishTime);
            resultScreen.ShowStars(m_RewardManager.GetRewardStars(finishTime));
        }

        private bool IsAllCheckPointReached()
        {
            foreach (UnityCheckPoint checkPoint in LoaderMap.GetCheckPoints())
                if (!checkPoint.IsReached())
                    return false;

            return true;
        }

        private void CheckPassedChechpoints()
        {
            if (m_CheckPointReachChecker != null)
            {
                foreach (UnityCheckPoint checkPoint in LoaderMap.GetCheckPoints())
                {
                    if (!checkPoint.IsReached())
                    {
                        if (checkPoint.CheckReached(m_CheckPointReachChecker))
                        {
                            checkPoint.SetReached(true, Time.time);

                            if (IsAllCheckPointReached())
                                Finish();
                        }
                        else
                            break;
                    }
                }
            }
        }

        private void CheckMapSurface()
        {
            byte surface = LoaderMap.GetSurfaceTypeAt(m_Map, m_UnityCar.transform.position);
            m_UnityCar.GetSurface().SetCurrentSurface(surface);
        }

        void Update()
        {
            if (m_Game.IsGameStarted() && !m_Game.IsFinish())
            {
                CheckPassedChechpoints();
                CheckMapSurface();
            }
        }
    }
}