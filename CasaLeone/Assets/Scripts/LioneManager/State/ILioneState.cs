namespace LioneManager.State
{
    public interface ILioneState
    {
        void Enter(LioneController lioneController);
        void Exit(LioneController lioneController);
        void Update(LioneController lioneController, float deltaTime);
    }
}