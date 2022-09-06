using System;
using System.Collections;
using Core;
using Enums;
using Player;
using UnityEngine;
using Utilities;
using Walls;

namespace Bomb
{
    public class Bomb : MonoBehaviour
    {
        [Header("Bomb Properties")]
        [Tooltip("Time after which bomb explodes.")]
        [SerializeField] private float explosionTime;
        
        [Tooltip("Impact of explosion from the position of the bomb.")]
        [SerializeField] private int explosionImpactArea;
        
        public MeshRenderer _bombMeshRenderer;
        public SphereCollider _bombSphereCollider;
        [SerializeField] private ParticleSystem explosionParticle;
        
        [SerializeField] private ParticleSystem explosionParticlePrefab;
        
        [SerializeField] private bool canBeUsed;
        
        private BoardManager _boardManager;
        private void Start()
        {
            _boardManager = BoardManager.Instance;
            
            _bombMeshRenderer = gameObject.GetComponent<MeshRenderer>();
            _bombSphereCollider = gameObject.GetComponent<SphereCollider>();

            // explosionParticle.gameObject.SetActive(false);
            _bombMeshRenderer.enabled = false;
            _bombSphereCollider.enabled = false;
            
            
        }

        public void PlaceBomb(Vector3 position)
        {
            canBeUsed = true;
            transform.position = position;
            _bombMeshRenderer.enabled = true;
            _bombSphereCollider.enabled = true;
        }

        public IEnumerator Explode()
        {
            if (!canBeUsed)
            {
                yield break;
            }
            
            yield return new WaitForSeconds(explosionTime);
            _bombMeshRenderer.enabled = false;
            _bombSphereCollider.enabled = false;
            _bombSphereCollider.isTrigger = true;
            // explosionParticle.gameObject.SetActive(true);

            Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity);
            GameLogManager.CustomLog("1st Particle spawned");
            Vector3 bombPosition = transform.position;

            int noOfDirections = Enum.GetValues(typeof(Direction)).Length;
            for (int j = 0; j < noOfDirections; j++)
            {
                Direction direction = (Direction) j;

                switch (direction)
                {
                    case Direction.Forward:
                        Explosion(bombPosition, Vector3.forward);
                        break;
                    case Direction.Backward:
                        Explosion(bombPosition, Vector3.back);
                        break;
                    case Direction.Left:
                        Explosion(bombPosition, Vector3.left);
                        break;
                    case Direction.Right:
                        Explosion(bombPosition, Vector3.right);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void Explosion(Vector3 bombPosition, Vector3 directionToCheck)
        {
            DestructibleWall destructibleWall;
            
            for (int i = 1; i < explosionImpactArea; i++)
            {
                var tempPos = bombPosition + (directionToCheck * i);
                
                Wall wall = _boardManager._cellGrid[(int) tempPos.x, (int) tempPos.z];
                if (wall != null)
                {
                    destructibleWall = wall.GetComponent<DestructibleWall>();
                
                    if (destructibleWall != null)
                    {
                        Instantiate(explosionParticlePrefab, new Vector3(tempPos.x, tempPos.y,
                            tempPos.z), Quaternion.identity);
                        GameLogManager.CustomLog("wall Particle spawned");
                        _boardManager._cellGrid[(int) tempPos.x, (int) tempPos.z] = null;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Instantiate(explosionParticlePrefab, new Vector3(tempPos.x, tempPos.y,
                        tempPos.z), Quaternion.identity);
                    GameLogManager.CustomLog("null Particle spawned");
                }
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                _bombSphereCollider.isTrigger = false;
            }
        }
    }
}