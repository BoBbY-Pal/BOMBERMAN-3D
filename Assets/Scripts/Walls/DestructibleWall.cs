using Interfaces;

namespace Walls
{
    public class DestructibleWall : Wall, IDestructible
    {
        public void DestroyObject()
        {
            EventService.UpdateScore?.Invoke(10);
            Destroy(gameObject);
        }
    }
}