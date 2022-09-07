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
        public RunningAway runningAwayState;
        
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

                case EnemyState.RunningAway:
                {
                    currentEnemyState = runningAwayState;
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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                IDestructible destructible = collision.gameObject.GetComponent<IDestructible>();
                destructible?.DestroyObject();
            }
        }

        public void DestroyObject()
        {
            EnemyService.Instance.enemies.Remove(enemyController);
            GameLogManager.CustomLog("Enemy died.");
            
            if(EnemyService.Instance.enemies.Count <= 0)
                EventService.GameWon?.Invoke();
            EventService.UpdateScore?.Invoke(50);
            Destroy(gameObject);
        }
    }
}