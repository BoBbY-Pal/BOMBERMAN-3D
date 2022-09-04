using Enemy.EnemyAI;
using Enums;
using Interfaces;
using Player;
using UnityEngine;
using Utilities;

namespace Enemy
{
    public class EnemyView : MonoBehaviour, IDestructible
    {
        public EnemyController enemyController;
        private Rigidbody _rigidbody;
        
        [SerializeField] private EnemyState initialState;
        [HideInInspector] public EnemyState activeState;
        [HideInInspector] public EnemyStateBase currentEnemyState;
        
        public Patrolling patrollingState;
        public Hiding hidingState;
        
        private void Awake()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        private void Start()
        {
            InitialiseState();
        }

        private void InitialiseState()
        {
            switch (initialState)
            {
                case EnemyState.Patrolling:
                {
                    currentEnemyState = patrollingState;
                    break;
                }

                case EnemyState.Hiding:
                {
                    currentEnemyState = hidingState;
                    break;
                }
                default:
                {
                    currentEnemyState = null;
                    break;
                }
            }
            currentEnemyState.OnStateEnter();
        }
        
        public void Initialise(EnemyController enemyController)
        {
            this.enemyController = enemyController;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void Move(Vector3 direction)
        {
            _rigidbody.velocity = direction * enemyController.GetModel().MovementSpeed;
        }

        // private void OnCollisionEnter(Collision collisionCollider)
        // {
        //     GameLogManager.CustomLog("Collision Happened.");
        //     if (collisionCollider.gameObject.GetComponent<PlayerController>())
        //     {
        //         IDestructible destructible = collisionCollider.gameObject.GetComponent<IDestructible>();
        //         destructible?.DestroyObject();
        //     }
        // }
        private void OnTriggerEnter(Collider collisionCollider)
        {
            GameLogManager.CustomLog("Collision Happened.");
            if (collisionCollider.gameObject.GetComponent<PlayerController>())
            {
                IDestructible destructible = collisionCollider.gameObject.GetComponent<IDestructible>();
                destructible?.DestroyObject();
            }
        }
        public void DestroyObject()
        {
            Destroy(gameObject);
            EnemyService.Instance.enemies.Remove(enemyController);
            
            if (EnemyService.Instance.enemies.Count <= 2)
            {
                currentEnemyState.ChangeCurrentState(hidingState);
            }
        }
    }
}