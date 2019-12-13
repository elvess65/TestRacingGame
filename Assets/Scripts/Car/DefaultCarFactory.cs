
namespace com.example.car
{
    public class DefaultCarFactory : ICarFactory
    {
        public ICar CreateCar(float maxMoveSpeed, float rotataeSpeed, ISurfaceInfo surface)
        {
            ISpeedProvider moveSpeed = new DefaultSpeed(maxMoveSpeed);
            ISpeedProvider speedOfRotate = new DefaultSpeed(rotataeSpeed);
            speedOfRotate.SetSpeed(rotataeSpeed);

            IAcceleration acceleration = new DefaultAcceleration(moveSpeed, surface);

            return new Car(moveSpeed, speedOfRotate, acceleration, surface);
        }
    }
}