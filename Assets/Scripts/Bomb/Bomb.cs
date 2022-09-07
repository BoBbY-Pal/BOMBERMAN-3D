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
        
        private MeshRenderer _bombMeshRenderer;
        private SphereCollider _bombSphereCollider;

        [SerializeField] private ParticleSystem explosionParticlePrefab;
        
        [SerializeField] private bool canBeUsed;
        
        private BoardManager _boardManager;
        private void Start()
        {
            _boardManager = BoardManager.Instance;
            
            _bombMeshRenderer = gameObject.GetComponent<MeshRenderer>();
            _bombSphereCollider = gameObject.GetComponent<SphereCollider>();

            // Disabling specific components is much efficient than disabling whole GameObject...
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
            
            // Disabling the mesh and collider instead of destroying the GameObject so that we can reuse the same object..
            _bombMeshRenderer.enabled = false;
            _bombSphereCollider.enabled = false;
            _bombSphereCollider.isTrigger = true;
            

            Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity);
            GameLogManager.CustomLog("1st Particle spawned");
            Vector3 bombPosition = transform.position;

            int noOfDirections = Enum.GetValues(typeof(Direction)).Length;
            for (int j = 1; j < noOfDirections; j++)
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

            canBeUsed = false;
        }

        private void Explosion(Vector3 bombPosition, Vector3 directionToCheck)
        {
            DestructibleWall destructibleWall;
            
            for (int i = 1; i < explosionImpactArea; i++)
            {
                var tempPos = bombPosition + (directionToCheck * i);
                if (_boardManager.IsGridOutOfBound((int) tempPos.x, (int) tempPos.z)) 
                    return;
                
                Wall wall = _boardManager._cellGrid[(int) tempPos.x, (int) tempPos.z];
                
                if (wall != null)
                {
                    destructibleWall = wall.GetComponent<DestructibleWall>();
                
                    if (destructibleWall != null)
                    {
                        Instantiate(explosionParticlePrefab, new Vector3(tempPos.x, tempPos.y,
                            tempPos.z), Quaternion.identity);
                        
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