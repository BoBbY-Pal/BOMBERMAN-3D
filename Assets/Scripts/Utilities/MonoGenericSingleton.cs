using UnityEngine;

namespace Utilities
{
    public class MonoGenericSingleton<T> : MonoBehaviour where T : MonoGenericSingleton<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = (T) this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}