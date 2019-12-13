
namespace com.example.map
{
    public class DefaultMapFactory : IMapFactory
    {
        public IMap CreateMap(int width, int height)
        {
            return new DefaultMap(width, height, new CheckPointsManager());
        }
    }
}