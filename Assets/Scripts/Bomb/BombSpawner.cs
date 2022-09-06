using System.Collections;
using Core;
using UnityEngine;
using Utilities;
using Walls;

namespace Bomb
{
    public class BombSpawner : MonoGenericSingleton<BombSpawner>
    {
        [SerializeField] private Bomb bombPrefab;
        private Bomb _bombObject;
        private bool b_canSpawnBomb = true;
        
        
        

        
        
        // private MeshRenderer _bombMeshRenderer;
        // private SphereCollider _bombSphereCollider;
        void Start()
        {
            // Spawning bomb at start of the game so we don't need to spawn bomb in the middle of gameplay
            // because instantiation is a expensive task that can lead to lag in the gameplay...
            // Disabling mesh and collider is a much efficient than disabling the whole GameObject...
            _bombObject = Instantiate(bombPrefab);
            
        }

        public void SpawnBomb(Vector3 position)
        {
            if (b_canSpawnBomb)
            {
                b_canSpawnBomb = false;
                
                int x = Mathf.RoundToInt(position.x);
                int z = Mathf.RoundToInt(position.z);
              
                
                _bombObject.PlaceBomb(new Vector3(x, position.y, z));
                Explode();     
            }
        }

        private void Explode()
        {
            // Disabling the mesh and collider instead of destroying the GameObject so that we can reuse the same object..
            // _bombMeshRenderer.enabled = false;
            // _bombSphereCollider.enabled = false;
            
            StartCoroutine(_bombObject.Explode());
            GameLogManager.CustomLog("Bomb Exploded");

            b_canSpawnBomb = true;
        }
    }
}
