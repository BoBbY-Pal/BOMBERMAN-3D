using UnityEngine;
using Utilities;


namespace Bomb
{
    public class BombSpawner : MonoGenericSingleton<BombSpawner>
    {
        [SerializeField] private Bomb bombPrefab;
        private Bomb _bombObject;
        private bool b_canSpawnBomb = true;
        
        private void Start()
        {
            // Spawning bomb at start of the game so we don't need to spawn bomb everytime
            // because instantiation is a expensive task that can lead to lag in the gameplay...
            
            _bombObject = Instantiate(bombPrefab);
        }

        public void SpawnBomb(Vector3 position)
        {
            if (b_canSpawnBomb)
            {
                b_canSpawnBomb = false;
                
                int x = Mathf.RoundToInt(position.x);
                int z = Mathf.RoundToInt(position.z);

                _bombObject.PlaceBomb(new Vector3(x, 0.5f, z));
                StartCoroutine(_bombObject.Explode());
                b_canSpawnBomb = true;
            }
        }
    }
}
