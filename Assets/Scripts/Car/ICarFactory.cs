
namespace com.example.car
{
    public interface ICarFactory
    {
        ICar CreateCar(float maxMoveSpeed, float rotataeSpeed, ISurfaceInfo surface);
    }
}