using System.Collections;
using UnityEngine;
using Utilities;

public class BombSpawner : MonoGenericSingleton<BombSpawner>
{
    [SerializeField] private GameObject bombPrefab;
    private GameObject _bombObject;
    private bool b_canSpawnBomb = true;
    [SerializeField] private ParticleSystem explosionParticle;
    
    private MeshRenderer _bombMeshRenderer;
    private SphereCollider _bombSphereCollider;
    void Start()
    {
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
            _bombObject.transform.position = bombSpawnPosition;
            _bombMeshRenderer.enabled = true;
            _bombSphereCollider.enabled = true;
            StartCoroutine(Explode());     
        }
        
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(3f);
        _bombMeshRenderer.enabled = false;
        _bombSphereCollider.enabled = false;
    }
}
