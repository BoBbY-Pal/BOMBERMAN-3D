using System.Collections;
using UnityEngine;
using Utilities;

public class BombSpawner : MonoGenericSingleton<BombSpawner>
{
    [SerializeField] private GameObject bombPrefab;
    private GameObject _bombObject;
    private bool b_canSpawnBomb = true;
    [SerializeField] private float explosionTime;
    [SerializeField] private ParticleSystem explosionParticle;
    
    private MeshRenderer _bombMeshRenderer;
    private SphereCollider _bombSphereCollider;
    void Start()
    {
        // Spawning bomb at start of the game so we don't need to spawn bomb in the middle of gameplay
        // because instantiation is a expensive task that can lead to lag in the gameplay...
        // Disabling mesh and collider is a much efficient than disabling the whole GameObject...
        _bombObject = Instantiate(bombPrefab);
        _bombMeshRenderer = _bombObject.GetComponent<MeshRenderer>();
        _bombSphereCollider = _bombObject.GetComponent<SphereCollider>();
        _bombMeshRenderer.enabled = false;
        _bombSphereCollider.enabled = false;
    }

    public void SpawnBomb(Vector3 bombSpawnPosition)
    {
        if (b_canSpawnBomb)
        {
            b_canSpawnBomb = false;
            _bombObject.transform.position = bombSpawnPosition;
            _bombMeshRenderer.enabled = true;
            _bombSphereCollider.enabled = true;
            StartCoroutine(Explode());     
        }
        
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionTime);
        
        // Disabling the mesh and collider instead of destroying the GameObject so that we can reuse the same object..
        _bombMeshRenderer.enabled = false;
        _bombSphereCollider.enabled = false;
        Instantiate(explosionParticle, new Vector3(_bombObject.transform.position.x, _bombObject.transform.position.y,
                _bombObject.transform.position.z+1),
            Quaternion.identity);
        b_canSpawnBomb = true;
    }
}
