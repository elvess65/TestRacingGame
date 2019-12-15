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
        public GameObject PlayerMobileInput;

        public UnityCar CreateCar(IGame game, ICar car, Vector3 pos, Vector3 scale)
        {
            GameObject carGo = Instantiate(CarPrefab, pos, Quaternion.identity);

            UnityCar unityCar = carGo.GetComponent<UnityCar>();
            unityCar.transform.localScale = scale;
            unityCar.Init(game, car.GetMoveSpeedProvider(), car.GetRotateSpeedProvider(), car.GetAcceleration(), car.GetSurfaceInfo());

#if UNITY_STANDALONE
            PlayerPCInput input = unityCar.gameObject.AddComponent<PlayerPCInput>();
            input.Init(unityCar, game);
#elif UNITY_ANDROID || UNITY_IOS

            GameObject inputObj = Instantiate(PlayerMobileInput);
            PlayerMobileInput mobileInput = inputObj.GetComponent<PlayerMobileInput>();
            mobileInput.Init(unityCar, game);
#endif

            CameraFollow cameraFollow = unityCar.gameObject.AddComponent<CameraFollow>();
            cameraFollow.Init(Camera.main, unityCar.gameObject, car);

            GameObject hudGo = Instantiate(HUDPrefab);
            UIController uIController = hudGo.GetComponent<UIController>();
            uIController.Init(car);

            return unityCar;
        }
    }
}