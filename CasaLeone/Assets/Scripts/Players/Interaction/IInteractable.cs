namespace Players.Interaction
{
    public interface IInteractable
    {
        void Interact(GlobalPlayer globalPlayer);
        void EndInteraction();
    }
}
