
namespace com.example.car
{
    public interface ISpeedProvider
    {
        float GetMaxSpeed();
        void SetSpeed(float maxSpeed);
        float GetSpeed();
        void IncreaseSpeed(float amount);
        void DecreaseSpeed(float amount);
    }
}