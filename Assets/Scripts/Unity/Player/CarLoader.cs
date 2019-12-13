using com.example.car;
using com.example.game;
using com.example.unity.ui;
using UnityEngine;

namespace com.example.unity.player
{
    public class CarLoader : MonoBehaviour
    {
        public GameObject HUDPrefab;
        public GameObject CarPrefab;

        public UnityCar CreateCar(IGame game, ICar car, Vector3 pos, Vector3 scale)
        {
            GameObject carGo = Instantiate(CarPrefab, pos, Quaternion.identity);

            UnityCar unityCar = carGo.GetComponent<UnityCar>();
            unityCar.transform.localScale = scale;
            unityCar.Init(game, car.GetMoveSpeedProvider(), car.GetRotateSpeedProvider(), car.GetAcceleration(), car.GetSurfaceInfo());

            PlayerInput input = unityCar.gameObject.AddComponent<PlayerInput>();
            input.Init(unityCar, game);

            CameraFollow cameraFollow = unityCar.gameObject.AddComponent<CameraFollow>();
            cameraFollow.Init(Camera.main, unityCar.gameObject);

            GameObject hudGo = Instantiate(HUDPrefab);
            UIController uIController = hudGo.GetComponent<UIController>();
            uIController.Init(car);

            return unityCar;
        }
    }
}