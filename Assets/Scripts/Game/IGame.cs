
namespace com.example.game
{
    public interface IGame
    {
        void Start(float time);
        bool IsGameStarted();
        bool IsFinish();
        float GetStartTime();
    }
}