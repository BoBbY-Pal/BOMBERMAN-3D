using Interfaces;

namespace Walls
{
    public class DestructibleWall : Wall, IDestructible
    {
        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}