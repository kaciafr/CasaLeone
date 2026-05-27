namespace Players.Interaction
{
    public interface IInteractable
    {
        int Priority { get; }
        void Interact(GlobalPlayer globalPlayer);
        void OnPlayerEnter(GlobalPlayer player);
        void OnPlayerExit(GlobalPlayer player);
    }
}
