
using Restaurants;

public interface IStressBar
{
    public void Enter(Restaurant restaurant);
    public void Update(Restaurant restaurant, float deltaTime);
    public void Exit(Restaurant restaurant);
}
