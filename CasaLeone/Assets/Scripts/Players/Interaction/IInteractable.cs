namespace Players.Interaction
{
    public interface IInteractable
    {
        void Interact(GlobalPlayer globalPlayer);
        int Priotity { get; }
    }
}
