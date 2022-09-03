using Interfaces;

namespace Wall
{
    public class DestructibleWall : Wall, IDestructible
    {
        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}